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
using System.IO;
using WOSI.IVR.IML.Classes;
using WOSI.IVR.IML.Classes.ScriptActions;
using WOSI.IVR.IML.Classes.ScriptEvents;
using System.Globalization;
using System.Text.RegularExpressions;
using CallButler.Service.ScriptProcessing.ScriptCompilers;
using CallButler.Service.Services;

namespace CallButler.Service.ScriptProcessing
{
    internal class VoicemailScriptProcessor : ScriptProcessorBase
    {
        private enum VoicemailExternalCommands
        {
            CALLBUTLERINTERNAL_NewVoicemail,
            CALLBUTLERINTERNAL_VoicemailManagement
        }

        private WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension;
        private VoicemailService vmService;
        private Utilities.PluginManagement.PluginManager pluginManager;
        private PBXRegistrarService registrarService;

        public VoicemailScriptProcessor(WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension, VoicemailService vmService, PBXRegistrarService registrarService, Utilities.PluginManagement.PluginManager pluginManager)
        {
            this.extension = extension;
            this.vmService = vmService;
            this.registrarService = registrarService;
            this.pluginManager = pluginManager;
        }

        protected override void OnStartProcessing(TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            string voicemailScriptLocation = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory) + "\\Voicemail.xml";

            if (File.Exists(voicemailScriptLocation))
            {
                IMLScript imlScript = IMLScript.OpenScript(voicemailScriptLocation);

                // Set our script variables
                tsInterface.IMLInterpreter.SetLocalVariable("ExtensionID", extension.ExtensionID.ToString());

                //if (Licensing.Management.AppPermissions.StatIsPermitted("Settings.MusicSettings"))
                //{
                    tsInterface.IMLInterpreter.SetLocalVariable("HoldMusicLocation", WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.HoldMusicRootDirectory));
                //}
                /*else
                {
                    tsInterface.IMLInterpreter.SetLocalVariable("HoldMusicLocation", "");
                }*/

                tsInterface.IMLInterpreter.SetLocalVariable("VoicemailRootFolder", WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.VoicemailRootDirectory));

                tsInterface.IMLInterpreter.StartScript(imlScript, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory));
            }            
        }

        protected override void OnExternalCommand(string command, string commandData, string eventToken, TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            // Parse out our external event action
            if (Enum.IsDefined(typeof(VoicemailExternalCommands), command))
            {
                VoicemailExternalCommands externalCommand = WOSI.Utilities.EnumUtils<VoicemailExternalCommands>.Parse(command);

                switch (externalCommand)
                {
                    case VoicemailExternalCommands.CALLBUTLERINTERNAL_NewVoicemail:
                        {
                            // Create a new voicemail record
                            vmService.CreateVoicemail(new Guid(commandData), new Guid(tsInterface.IMLInterpreter.GetLocalVariable("ExtensionID")), tsInterface.IMLInterpreter.CallerDisplayName, tsInterface.IMLInterpreter.CallerHost, tsInterface.IMLInterpreter.CallerUsername);

                            break;
                        }
                    case VoicemailExternalCommands.CALLBUTLERINTERNAL_VoicemailManagement:
                        {
                            tsInterface.ScriptProcessor = new VoicemailManagementScriptProcessor(extension, registrarService);
                            tsInterface.ScriptProcessor.StartProcessing(tsInterface, telecomProvider, dataProvider);

                            break;
                        }
                }

                tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
            }
        }
    }
}
