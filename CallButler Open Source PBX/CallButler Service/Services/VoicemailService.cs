///////////////////////////////////////////////////////////////////////////////////////////////
//
//    This File is Part of the CallButler Open Source PBX (http://www.codeplex.com/callbutler
//
//    Copyright (c) 2005-2008, Jim Heising
//    All rights reserved.
//
//    Redistribution and use in source and binary forms, with or without modification,
//    are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice,
//      this list of conditions and the following disclaimer.
//
//    * Redistributions in binary form must reproduce the above copyright notice,
//      this list of conditions and the following disclaimer in the documentation and/or
//      other materials provided with the distribution.
//
//    * Neither the name of Jim Heising nor the names of its contributors may be
//      used to endorse or promote products derived from this software without specific prior
//      written permission.
//
//    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//    ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
//    IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
//    INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
//    NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//    PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
//    WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
//    ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
//    POSSIBILITY OF SUCH DAMAGE.
//
///////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using WOSI.CallButler.Data;
using System.IO;

namespace CallButler.Service.Services
{
    class VoicemailEventArgs : EventArgs
    {
        private WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail;

        public VoicemailEventArgs(WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail)
        {
            this.voicemail = voicemail;
        }

        public WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow Voicemail
        {
            get
            {
                return voicemail;
            }
        }
    }

    class VoicemailService
    {
        private WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider;
        private PBXRegistrarService registrarService;
        private VoicemailMailerService vmMailerService;
        private Utilities.PluginManagement.PluginManager pluginManager;

        public event EventHandler<VoicemailEventArgs> NewVoicemail;

        public VoicemailService(WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider, PBXRegistrarService registrarService, Utilities.PluginManagement.PluginManager pluginManager, VoicemailMailerService vmMailerService)
        {
            this.dataProvider = dataProvider;
            this.registrarService = registrarService;
            this.vmMailerService = vmMailerService;
            this.pluginManager = pluginManager;
        }

        public void DeleteVoicemail(Guid extensionID, Guid voicemailID)
        {
            CallButlerDataset.VoicemailsRow vRow = dataProvider.GetVoicemail(extensionID, voicemailID);

            if (vRow != null)
            {
                string voicemailPath = String.Format("{0}\\{1}\\{2}.snd", WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.VoicemailRootDirectory), vRow.ExtensionID.ToString(), voicemailID.ToString());

                dataProvider.DeleteVoicemail(extensionID, voicemailID);

                if (File.Exists(voicemailPath))
                    File.Delete(voicemailPath);

                // Send a message waiting notification to our PBX phone
                if (registrarService != null)
                    registrarService.SendMessageWaitingNotification(extensionID);
            }
        }

        public void CreateVoicemail(Guid voicemailID, Guid extensionID, string callerDisplayName, string callerHost, string callerUsername)
        {
            // Get our extension
            WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = dataProvider.GetExtension(Properties.Settings.Default.CustomerID, extensionID);

            if (extension != null)
            {
                // Create a new voicemail record
                WOSI.CallButler.Data.CallButlerDataset.VoicemailsDataTable voicemailsTable = new WOSI.CallButler.Data.CallButlerDataset.VoicemailsDataTable();
                WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail = voicemailsTable.NewVoicemailsRow();
                voicemail.CallerDisplayName = callerDisplayName;
                voicemail.CallerHost = callerHost;
                voicemail.CallerUsername = callerUsername;
                voicemail.ExtensionID = extensionID;
                voicemail.VoicemailID = voicemailID;
                voicemail.Timestamp = DateTime.Now;
                voicemailsTable.AddVoicemailsRow(voicemail);

                dataProvider.PersistVoicemail(voicemail);

                // Run the voicemail through any VM plugins
                CallButler.Service.Plugin.CallButlerVoicemailHandlerPlugin[] vmHandlers = pluginManager.GetAllPluginsOfType<CallButler.Service.Plugin.CallButlerVoicemailHandlerPlugin>();
                foreach (CallButler.Service.Plugin.CallButlerVoicemailHandlerPlugin vmHandler in vmHandlers)
                {
                    try
                    {
                        vmHandler.OnNewVoicemail(voicemail.CallerDisplayName, voicemail.CallerHost, voicemail.ExtensionID.ToString(), voicemail.VoicemailID.ToString(), WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.VoicemailRootDirectory) + "\\" + voicemail.ExtensionID.ToString() + "\\" + voicemail.VoicemailID.ToString() + ".snd");
                    }
                    catch (Exception e)
                    {
                        LoggingService.AddLogEntry(WOSI.CallButler.ManagementInterface.LogLevel.ErrorsOnly, string.Format("Voicemail Handler Plugin Failed\r\n\r\n{0} ({1})\r\n\r\n{2}", vmHandler.PluginName, vmHandler.PluginID.ToString(), e.Message), true);
                    }
                }

                // Queue our voicemail Email
                vmMailerService.QueueVoicemailEmail(extension, voicemail);

                // Notify the any PBX phones of a new message
                if (registrarService != null)
                    registrarService.SendMessageWaitingNotification(extension.ExtensionNumber);

                // Notify anyone else of the new message
                WOSI.Utilities.EventUtils.FireAsyncEvent(NewVoicemail, this, new VoicemailEventArgs(voicemail));
            }
        }

        public void ForwardVoicemail(Guid extensionID, Guid voicemailID, Guid toExtensionID)
        {
            CallButlerDataset.VoicemailsRow voicemail = dataProvider.GetVoicemail(extensionID, voicemailID);

            if (voicemail != null)
            {
                Guid newVoicemailID = Guid.NewGuid();
                
                // Get our original voicemail name
                string origVoicemailFilename = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.VoicemailRootDirectory) + "\\" + voicemail.ExtensionID.ToString() + "\\" + voicemail.VoicemailID + ".snd";
                string newVoicemailDirectory = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.VoicemailRootDirectory) + "\\" + toExtensionID.ToString();
                string newVoicemailFilename = newVoicemailDirectory + "\\" + newVoicemailID.ToString() + ".snd";

                if (!Directory.Exists(newVoicemailDirectory))
                    Directory.CreateDirectory(newVoicemailDirectory);

                // Copy our voicemail sound file
                File.Copy(origVoicemailFilename, newVoicemailFilename);   

                // Create a new voicemail
                CreateVoicemail(newVoicemailID, toExtensionID, voicemail.CallerDisplayName, voicemail.CallerHost, voicemail.CallerUsername);
            }
        }

        public byte[] GetVoicemailBytes(Guid extensionID, Guid voicemailId)
        {
            // Get our voicemail row first
            WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail = dataProvider.GetVoicemail(extensionID, voicemailId);

            if (voicemail != null)
            {
                string path = @"{0}\{1}\{2}.snd";
                string voicemailPath = String.Format(path, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.VoicemailRootDirectory), voicemail.ExtensionID.ToString(), voicemailId.ToString());
                return WOSI.Utilities.FileUtils.GetFileBytes(voicemailPath);
            }
            else
                return null;
        }

        public void MarkVoicemailAsRead(Guid extensionID, Guid voicemailID)
        {
            dataProvider.MarkVoicemailRead(extensionID, voicemailID);

            // Send a message waiting notification to our PBX phone
            if (registrarService != null)
                registrarService.SendMessageWaitingNotification(extensionID);
        }
    }
}
