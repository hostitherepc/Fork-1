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
    internal class TransferConferenceScriptProcessor : ScriptProcessorBase
    {
        private enum TransferConferenceExternalCommands
        {
            CALLBUTLERINTERNAL_EndConference,
            CALLBUTLERINTERNAL_RecordingStarted,
            CALLBUTLERINTERNAL_RecordingFinished,
            CALLBUTLERINTERNAL_TransferCaller,
            CALLBUTLERINTERNAL_HoldCaller,
            CALLBUTLERINTERNAL_UnholdCaller
        }

        public enum TransferConferenceExternalEvents
        {
            CALLBUTLERINTERNAL_TransferCall,
            CALLBUTLERINTERNAL_HoldCall,
            CALLBUTLERINTERNAL_UnholdCall,
            CALLBUTLERINTERNAL_RecordingStarted,
            CALLBUTLERINTERNAL_RecordingFinished,
            CALLBUTLERINTERNAL_TransferCaller,
        }

        private VoicemailMailerService vmMailerService;
        private WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension;
        private ScriptProcessing.TelecomScriptInterface callerTsInterface;
        private ScriptProcessing.TelecomScriptInterface tsInterface;
        private PBXRegistrarService pbxRegistrar;
        private ScriptService scriptService;
        private int conferenceID;

        public TransferConferenceScriptProcessor(int conferenceID, ScriptService scriptService, ScriptProcessing.TelecomScriptInterface callerTsInterface, PBXRegistrarService pbxRegistrar, WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension, VoicemailMailerService vmMailerService)
        {
            this.callerTsInterface = callerTsInterface;
            this.vmMailerService = vmMailerService;
            this.scriptService = scriptService;
            this.conferenceID = conferenceID;
            this.pbxRegistrar = pbxRegistrar;
            this.extension = extension;
        }

        protected override void OnStartProcessing(TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            this.tsInterface = tsInterface;

            string scriptLocation = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory) + "\\Transfer Conference.xml";

            if (File.Exists(scriptLocation))
            {
                IMLScript imlScript = IMLScript.OpenScript(scriptLocation);

                tsInterface.IMLInterpreter.SetLocalVariable("HoldMusicLocation", WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.HoldMusicRootDirectory));

                // Set our recording path
                tsInterface.IMLInterpreter.SetLocalVariable("RecordingPath", WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.CallRecordingSoundPath));
                tsInterface.IMLInterpreter.SetLocalVariable("ExtensionID", extension.ExtensionID.ToString());

                tsInterface.IMLInterpreter.StartScript(imlScript, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory));
            } 
        }

        public override void OnIncomingTransfer(CallButler.Telecom.TelecomProviderBase telcomProvider, CallButler.Telecom.TransferEventArgs e)
        {
            if (e.ReplacementLineNumber != 0)
            {               
                // Find the ts interface for the call from this extension to our remote caller
                TelecomScriptInterface remoteTsInterface = scriptService.TelecomScriptInterfaces[e.ReplacementLineNumber];

                // Find the ts interface for the remote caller
                if (remoteTsInterface.ScriptProcessor is TransferConferenceScriptProcessor)
                {
                    TransferConferenceScriptProcessor remoteProcessor = (TransferConferenceScriptProcessor)remoteTsInterface.ScriptProcessor;

                    int remoteLineNumber = remoteProcessor.callerTsInterface.LineNumber;

                    remoteProcessor.callerTsInterface = null;

                    if (scriptService.TelecomScriptInterfaces[remoteLineNumber].ScriptProcessor is TransferConferenceScriptProcessor)
                    {
                        remoteTsInterface = scriptService.TelecomScriptInterfaces[remoteLineNumber];
                        remoteProcessor = (TransferConferenceScriptProcessor)remoteTsInterface.ScriptProcessor;

                        // Conference the two lines together
                        remoteProcessor.callerTsInterface = callerTsInterface;

                        if (callerTsInterface.ScriptProcessor is TransferConferenceScriptProcessor)
                            ((TransferConferenceScriptProcessor)callerTsInterface.ScriptProcessor).callerTsInterface = remoteProcessor.tsInterface;

                        remoteProcessor.conferenceID = conferenceID;

                        telcomProvider.AddLineToConference(conferenceID, remoteProcessor.tsInterface.LineNumber);
                        telcomProvider.RemoveLineFromConference(tsInterface.LineNumber);

                        // Tell the other caller to stop hold music
                        callerTsInterface.IMLInterpreter.SignalExternalEvent(TransferConferenceExternalEvents.CALLBUTLERINTERNAL_UnholdCall.ToString());

                        //this.conferenceID = 0;

                        this.callerTsInterface = null;

                        telcomProvider.EndCall(e.ReplacementLineNumber);
                        telcomProvider.EndCall(tsInterface.LineNumber);

                        return;
                    }
                }
                
                callerTsInterface.IMLInterpreter.SignalTransferFailure();
            }
            else
            {
                TransferCaller(telcomProvider, e.CallingToNumber);
            }
        }

        private void TransferCaller(Telecom.TelecomProviderBase telecomProvider, string transferTo)
        {
            // Tell the other caller to transfer
            callerTsInterface.IMLInterpreter.SetLocalVariable("TransferNumber", transferTo);
            callerTsInterface.IMLInterpreter.SignalExternalEvent(TransferConferenceExternalEvents.CALLBUTLERINTERNAL_TransferCall.ToString());


            // And end the conferece
            telecomProvider.EndConference(conferenceID, false);
        }

        protected override void OnExternalCommand(string command, string commandData, string eventToken, TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            // Parse out our external event action
            if (callerTsInterface != null && Enum.IsDefined(typeof(TransferConferenceExternalCommands), command))
            {
                TransferConferenceExternalCommands externalCommand = WOSI.Utilities.EnumUtils<TransferConferenceExternalCommands>.Parse(command);

                switch (externalCommand)
                {
                    case TransferConferenceExternalCommands.CALLBUTLERINTERNAL_EndConference:

                        telecomProvider.EndConference(conferenceID, true);
                        
                        break;

                    case TransferConferenceExternalCommands.CALLBUTLERINTERNAL_RecordingStarted:

                        // Notify the other caller that recording has started
                        callerTsInterface.IMLInterpreter.SignalExternalEvent(TransferConferenceExternalEvents.CALLBUTLERINTERNAL_RecordingStarted.ToString());

                        break;

                    case TransferConferenceExternalCommands.CALLBUTLERINTERNAL_RecordingFinished:

                        // Notify the other caller that recording has finished
                        callerTsInterface.IMLInterpreter.SignalExternalEvent(TransferConferenceExternalEvents.CALLBUTLERINTERNAL_RecordingFinished.ToString());

                        vmMailerService.QueueVoicemailEmail(extension, "Your Call Recording", "Call Recording", tsInterface.IMLInterpreter.GetLocalVariable("RecordingFilename"));

                        break;

                    case TransferConferenceExternalCommands.CALLBUTLERINTERNAL_TransferCaller:

                        TransferCaller(telecomProvider, commandData);

                        break;

                    case TransferConferenceExternalCommands.CALLBUTLERINTERNAL_HoldCaller:

                        // Remove this call from the conference
                        tsInterface.ProcessOnHold(conferenceID, true);

                        // Tell the other caller to play hold music
                        callerTsInterface.IMLInterpreter.SignalExternalEvent(TransferConferenceExternalEvents.CALLBUTLERINTERNAL_HoldCall.ToString());

                        break;

                    case TransferConferenceExternalCommands.CALLBUTLERINTERNAL_UnholdCaller:

                        // Add this caller back into the conference
                        tsInterface.ProcessOnHold(conferenceID, false);

                        // Tell the other caller to stop hold music
                        callerTsInterface.IMLInterpreter.SignalExternalEvent(TransferConferenceExternalEvents.CALLBUTLERINTERNAL_UnholdCall.ToString());

                        break;
                }
            }

            tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
        }
    }
}
