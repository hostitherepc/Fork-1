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
    internal class TransferConferenceParticipantScriptProcessor : ScriptProcessorBase
    {
        private enum TransferConferenceParticipantExternalCommands
        {
            CALLBUTLERINTERNAL_EndConference,
            CALLBUTLERINTERNAL_MainMenu
        }

        private VoicemailMailerService vmMailerService;
        private WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension;
        private TelecomScriptInterface extensionTsInterface;
        private int conferenceID;

        public TransferConferenceParticipantScriptProcessor(int conferenceID, TelecomScriptInterface extensionTsInterface, WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension, VoicemailMailerService vmMailerService)
        {
            this.extensionTsInterface = extensionTsInterface;
            this.vmMailerService = vmMailerService;
            this.conferenceID = conferenceID;
            this.extension = extension;
        }

        protected override void OnStartProcessing(TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            string scriptLocation = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory) + "\\Transfer Conference Participant.xml";

            if (File.Exists(scriptLocation))
            {
                IMLScript imlScript = IMLScript.OpenScript(scriptLocation);

                tsInterface.IMLInterpreter.SetLocalVariable("HoldMusicLocation", WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.HoldMusicRootDirectory));

                tsInterface.IMLInterpreter.StartScript(imlScript, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory));
            }
        }

        protected override void OnExternalCommand(string command, string commandData, string eventToken, TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            // Parse out our external event action
            if (Enum.IsDefined(typeof(TransferConferenceParticipantExternalCommands), command))
            {
                TransferConferenceParticipantExternalCommands externalCommand = WOSI.Utilities.EnumUtils<TransferConferenceParticipantExternalCommands>.Parse(command);

                switch (externalCommand)
                {
                    case TransferConferenceParticipantExternalCommands.CALLBUTLERINTERNAL_EndConference:

                        telecomProvider.EndConference(conferenceID, true);

                        break;

                    case TransferConferenceParticipantExternalCommands.CALLBUTLERINTERNAL_MainMenu:

                        break;
                }
            }

            tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
        }

        public override void OnDTMFDigit(CallButler.Telecom.TelecomProviderBase telecomProvider, CallButler.Telecom.CallInputEventArgs e)
        {
            if (!e.InAudio)
            {
                telecomProvider.SendDTMF(extensionTsInterface.LineNumber, e.InputString, Properties.Settings.Default.SendDTMFInAudio);
            }
        }
    }
}
