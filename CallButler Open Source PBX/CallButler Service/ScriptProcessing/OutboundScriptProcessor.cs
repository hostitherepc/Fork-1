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
    class OutboundScriptProcessor : ScriptProcessorBase
    {
        private enum OutboundCallExternalEvents
        {
            CALLBUTLERINTERNAL_ConferenceStarted,
            CALLBUTLERINTERNAL_HoldCaller,
            CALLBUTLERINTERNAL_UnholdCaller,
            CALLBUTLERINTERNAL_TransferCall
        }

        TelecomScriptInterface outboundTsInterface;
        TelecomScriptInterface thisTsInterface;
        int conferenceID = 0;
        int outboundExtensionNumber = 0;

        public OutboundScriptProcessor(TelecomScriptInterface tsInterface, TelecomScriptInterface outboundTsInterface, int outboundExtensionNumber)
        {
            this.thisTsInterface = tsInterface;
            this.outboundTsInterface = outboundTsInterface;
            this.outboundExtensionNumber = outboundExtensionNumber;
        }

        protected override void OnStartProcessing(TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            string scriptLocation = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory) + "\\Outbound Call.xml";

            if (File.Exists(scriptLocation))
            {
                IMLScript imlScript = IMLScript.OpenScript(scriptLocation);

                tsInterface.IMLInterpreter.SetLocalVariable("HoldMusicLocation", WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.HoldMusicRootDirectory));

                tsInterface.IMLInterpreter.StartScript(imlScript, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory));

                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = dataProvider.GetExtensionNumber(Properties.Settings.Default.CustomerID, outboundExtensionNumber);

                // If the outbound ts interface is null, then this means we don't have any available lines to make outbound calls.
                // Also check to see if this extension is allowed to make outbound calls
                if (outboundTsInterface == null || (outboundExtensionNumber >=0 && extension != null && !extension.EnableOutboundCalls))
                {
                    tsInterface.IMLInterpreter.SignalCallFailure();
                    outboundTsInterface.Locked = false;
                }
                else
                {
                    // Link to our outbound script processor and start it
                    outboundTsInterface.ScriptProcessor.LinkScriptProcessor(this);
                    outboundTsInterface.ScriptProcessor.StartProcessing(outboundTsInterface, telecomProvider, dataProvider);
                }
            }
        }

        protected override void OnExternalCommand(string command, string commandData, string eventToken, TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            // Parse out our external event action
            if (Enum.IsDefined(typeof(OutboundCalleeScriptProcessor.OutboundCallExternalCommands), command))
            {
                OutboundCalleeScriptProcessor.OutboundCallExternalCommands externalCommand = WOSI.Utilities.EnumUtils<OutboundCalleeScriptProcessor.OutboundCallExternalCommands>.Parse(command);

                switch (externalCommand)
                {
                    case OutboundCalleeScriptProcessor.OutboundCallExternalCommands.CALLBUTLERINTERNAL_CallEnded:

                        ProcessEndCall(telecomProvider);

                        break;

                    case OutboundCalleeScriptProcessor.OutboundCallExternalCommands.CALLBUTLERINTERNAL_HoldCaller:

                        // Remove this call from the conference
                        tsInterface.ProcessOnHold(conferenceID, true);

                        // Tell the other caller to play hold music
                        outboundTsInterface.IMLInterpreter.SignalExternalEvent(OutboundCalleeScriptProcessor.OutboundCallExternalCommands.CALLBUTLERINTERNAL_HoldCaller.ToString());

                        break;

                    case OutboundCalleeScriptProcessor.OutboundCallExternalCommands.CALLBUTLERINTERNAL_UnholdCaller:

                        // Add this caller back into the conference
                        tsInterface.ProcessOnHold(conferenceID, false);

                        // Tell the other caller to stop hold music
                        outboundTsInterface.IMLInterpreter.SignalExternalEvent(OutboundCalleeScriptProcessor.OutboundCallExternalCommands.CALLBUTLERINTERNAL_UnholdCaller.ToString());

                        break;

                    case OutboundCalleeScriptProcessor.OutboundCallExternalCommands.CALLBUTLERINTERNAL_TransferCaller:

                        TransferCaller(telecomProvider, commandData);

                        break;
                }
            }

            tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
        }

        private void TransferCaller(Telecom.TelecomProviderBase telecomProvider, string transferTo)
        {
            // Tell the other caller to transfer
            outboundTsInterface.IMLInterpreter.SetLocalVariable("TransferNumber", transferTo);
            outboundTsInterface.IMLInterpreter.SignalExternalEvent(OutboundCallExternalEvents.CALLBUTLERINTERNAL_TransferCall.ToString());

            // And end the conferece
            telecomProvider.EndConference(conferenceID, false);
        }

        private void ProcessEndCall(CallButler.Telecom.TelecomProviderBase telecomProvider)
        {
            if (outboundTsInterface != null && conferenceID == 0)
            {
                telecomProvider.EndCall(outboundTsInterface.LineNumber);
            }
            else
            {
                telecomProvider.EndConference(conferenceID, true);
            }
        }

        protected override void OnLinkedExternalCommand(string command, string commandData, string eventToken, TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            // Parse out our external event action
            if (Enum.IsDefined(typeof(OutboundCalleeScriptProcessor.OutboundCallExternalCommands), command))
            {
                OutboundCalleeScriptProcessor.OutboundCallExternalCommands externalCommand = WOSI.Utilities.EnumUtils<OutboundCalleeScriptProcessor.OutboundCallExternalCommands>.Parse(command);

                switch (externalCommand)
                {
                    case OutboundCalleeScriptProcessor.OutboundCallExternalCommands.CALLBUTLERINTERNAL_CallFailed:
                        {
                            this.thisTsInterface.IMLInterpreter.SignalCallFailure();
                            break;
                        }
                    case OutboundCalleeScriptProcessor.OutboundCallExternalCommands.CALLBUTLERINTERNAL_CallConnected:
                        {
                            // Conference the two callers
                            conferenceID = telecomProvider.ConferenceLines(this.thisTsInterface.LineNumber, tsInterface.LineNumber);

                            this.thisTsInterface.IMLInterpreter.SignalExternalEvent(OutboundCallExternalEvents.CALLBUTLERINTERNAL_ConferenceStarted.ToString());

                            break;
                        }
                    case OutboundCalleeScriptProcessor.OutboundCallExternalCommands.CALLBUTLERINTERNAL_CallEnded:
                        {
                            ProcessEndCall(telecomProvider);

                            break;
                        }
                }
            }

            //tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
        }

        public override void OnDTMFDigit(CallButler.Telecom.TelecomProviderBase telecomProvider, CallButler.Telecom.CallInputEventArgs e)
        {
            // If we're making an outbound call, make sure we relay any DTMF on to our remote caller if it is out of band DTMF
            if (!e.InAudio)
            {
                telecomProvider.SendDTMF(outboundTsInterface.LineNumber, e.InputString, Properties.Settings.Default.SendDTMFInAudio);
            }
        }
    }
}
