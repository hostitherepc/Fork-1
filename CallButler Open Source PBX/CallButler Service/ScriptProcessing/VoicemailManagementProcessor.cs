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
    internal class VoicemailManagementScriptProcessor : ScriptProcessorBase
    {
        private enum VoicemailExternalCommands
        {
            CALLBUTLERINTERNAL_AuthenticatePasscode,
            CALLBUTLERINTERNAL_SaveNewGreeting,
            CALLBUTLERINTERNAL_FetchNextVoicemail,
            CALLBUTLERINTERNAL_DeleteVoicemail
        }

        private enum VoicemailExternalEvents
        {
            CALLBUTLERINTERNAL_InvalidPasscode,
            CALLBUTLERINTERNAL_ValidPasscode,
            CALLBUTLERINTERNAL_EndOfMessages
        }

        private WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension;
        private PBXRegistrarService pbxRegistrar;

        public VoicemailManagementScriptProcessor(WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension, PBXRegistrarService pbxRegistrar)
        {
            this.extension = extension;
            this.pbxRegistrar = pbxRegistrar;
        }

        protected override void OnStartProcessing(TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            string voicemailScriptLocation = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory) + "\\Voicemail Management.xml";

            if (File.Exists(voicemailScriptLocation))
            {
                IMLScript imlScript = IMLScript.OpenScript(voicemailScriptLocation);

                // Set our script variables
                tsInterface.IMLInterpreter.SetLocalVariable("ExtensionID", extension.ExtensionID.ToString());
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
                    case VoicemailExternalCommands.CALLBUTLERINTERNAL_AuthenticatePasscode:
                        {
                            // Check to make sure our passcode matches our extension
                            string enteredPasscodeHash = WOSI.Utilities.CryptoUtils.CreateMD5Hash(commandData);

                            if (enteredPasscodeHash != extension.Password)
                            {
                                tsInterface.IMLInterpreter.SignalExternalEvent(VoicemailExternalEvents.CALLBUTLERINTERNAL_InvalidPasscode.ToString());
                            }
                            else
                            {
                                // Get our new voicemail count
                                int newVoicemailCount = dataProvider.GetNewVoicemailCount(extension.ExtensionID);

                                tsInterface.IMLInterpreter.SetLocalVariable("NewVoicemailCount", newVoicemailCount.ToString());

                                tsInterface.IMLInterpreter.SignalExternalEvent(VoicemailExternalEvents.CALLBUTLERINTERNAL_ValidPasscode.ToString());
                            }

                            break;
                        }
                    case VoicemailExternalCommands.CALLBUTLERINTERNAL_SaveNewGreeting:
                        {
                            WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow voicemailGreeting = dataProvider.GetLocalizedGreeting(Properties.Settings.Default.CustomerID, extension.ExtensionID, Properties.Settings.Default.DefaultLanguage);
                            string tmpGreetingFilename = commandData;

                            if (File.Exists(tmpGreetingFilename) && voicemailGreeting != null)
                            {
                                // Change our voicemail greeting to a sound file
                                voicemailGreeting.Type = (short)WOSI.CallButler.Data.GreetingType.SoundGreeting;

                                // Move our greeting sound over
                                string greetingDirectory = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingSoundRootDirectory) + "\\" + Properties.Settings.Default.DefaultLanguage;
                                string greetingFilename = greetingDirectory + "\\" + voicemailGreeting.GreetingID.ToString() + ".snd";

                                if (!Directory.Exists(greetingDirectory))
                                    Directory.CreateDirectory(greetingDirectory);

                                File.Copy(tmpGreetingFilename, greetingFilename, true);
                                File.Delete(tmpGreetingFilename);

                                voicemailGreeting.Data = WOSI.Utilities.CryptoUtils.GetFileChecksum(greetingFilename);

                                dataProvider.PersistLocalizedGreeting(Properties.Settings.Default.CustomerID, voicemailGreeting);
                            }

                            tsInterface.IMLInterpreter.SignalEventCallback(eventToken);

                            break;
                        }
                    case VoicemailExternalCommands.CALLBUTLERINTERNAL_FetchNextVoicemail:
                        {
                            // Get our voicemail rows
                            WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow[] voicemails = (WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow[])dataProvider.GetVoicemails( extension.ExtensionID).Select("", "Timestamp DESC");

                            // Get our voicemail message index
                            int voicemailIndex = Convert.ToInt32(tsInterface.IMLInterpreter.GetLocalVariable("VoicemailIndex"));
                            voicemailIndex++;

                            if (voicemailIndex < voicemails.Length)
                            {
                                WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail = voicemails[voicemailIndex];

                                // Create our voicemail intro
                                string voicemailIntro = "";

                                if (voicemailIndex == 0)
                                    voicemailIntro = "First ";
                                else
                                    voicemailIntro = "Next ";

                                if (voicemail.IsNew)
                                    voicemailIntro += "New ";

                                voicemailIntro += "Message received on " + voicemail.Timestamp.ToShortDateString() + " " + voicemail.Timestamp.ToShortTimeString();

                                tsInterface.IMLInterpreter.SetLocalVariable("VoicemailIntro", voicemailIntro);

                                string voicemailFilename = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.VoicemailRootDirectory) + "\\" + voicemail.ExtensionID.ToString() + "\\" + voicemail.VoicemailID + ".snd";
                                tsInterface.IMLInterpreter.SetLocalVariable("VoicemailSound", voicemailFilename);

                                tsInterface.IMLInterpreter.SetLocalVariable("VoicemailIndex", voicemailIndex.ToString());

                                // Mark the voicemail as read
                                dataProvider.MarkVoicemailRead(voicemail.ExtensionID, voicemail.VoicemailID);

                                if (pbxRegistrar != null)
                                    pbxRegistrar.SendMessageWaitingNotification(voicemail.ExtensionID);

                                tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
                            }
                            else
                            {
                                tsInterface.IMLInterpreter.SignalExternalEvent(VoicemailExternalEvents.CALLBUTLERINTERNAL_EndOfMessages.ToString());
                            }

                            break;
                        }
                    case VoicemailExternalCommands.CALLBUTLERINTERNAL_DeleteVoicemail:
                        {
                            // Get our voicemail rows
                            WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow[] voicemails = (WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow[])dataProvider.GetVoicemails(extension.ExtensionID).Select("", "Timestamp DESC");

                            // Get our voicemail message index
                            int voicemailIndex = Convert.ToInt32(tsInterface.IMLInterpreter.GetLocalVariable("VoicemailIndex"));

                            if (voicemailIndex < voicemails.Length)
                            {
                                WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail = voicemails[voicemailIndex];

                                // Delete our voicemail
                                dataProvider.DeleteVoicemail(voicemail.ExtensionID, voicemail.VoicemailID);

                                // Delete our voicemail sound
                                string voicemailFilename = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.VoicemailRootDirectory) + "\\" + voicemail.ExtensionID.ToString() + "\\" + voicemail.VoicemailID + ".snd";
                                if (File.Exists(voicemailFilename))
                                    File.Delete(voicemailFilename);

                                voicemailIndex--;
                                tsInterface.IMLInterpreter.SetLocalVariable("VoicemailIndex", voicemailIndex.ToString());

                                if(pbxRegistrar != null)
                                    pbxRegistrar.SendMessageWaitingNotification(voicemail.ExtensionID);
                            }

                            tsInterface.IMLInterpreter.SignalEventCallback(eventToken);

                            break;
                        }
                }
            }
        }
    }
}
