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
using WOSI.IVR.IML.Classes;
using WOSI.IVR.IML.Classes.ScriptActions;
using WOSI.IVR.IML.Classes.ScriptEvents;
using CallButler.Service.ScriptProcessing.ScriptCompilers;
using CallButler.Service.Services;

namespace CallButler.Service.ScriptProcessing
{
    internal class Click2CallScriptProcessor : ScriptProcessorBase
    {
        private enum Click2CallExternalCommands
        {
            CALLBUTLERINTERNAL_ExtensionNotAvailable,
            CALLBUTLERINTERNAL_ConnectCalls
        }

        private string numberToDial = "";
        TelecomScriptInterface callMakerInterface;
        ScriptService scriptService;
        WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow callMakerExtension;

        public Click2CallScriptProcessor(ScriptService scriptService, string numberToDial, WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow callMakerExtension, TelecomScriptInterface callMakerInterface)
        {
            this.callMakerExtension = callMakerExtension;
            this.scriptService = scriptService;
            this.numberToDial = numberToDial;
            this.callMakerInterface = callMakerInterface;
        }

        protected override void OnStartProcessing(TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            tsInterface.IMLInterpreter.CallerDisplayName = string.Format("Calling {0}", numberToDial);
            tsInterface.IMLInterpreter.CallerHost = "";
            tsInterface.IMLInterpreter.CallerUsername = "";
            tsInterface.IMLInterpreter.DialedHost = "";
            tsInterface.IMLInterpreter.DialedUsername = "";

            string click2CallScriptLocation = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory) + "\\Click2Call Handler.xml";

            if (System.IO.File.Exists(click2CallScriptLocation))
            {
                IMLScript imlScript = IMLScript.OpenScript(click2CallScriptLocation);
                tsInterface.IMLInterpreter.StartScript(imlScript, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory));
            }
        }

        protected override void OnExternalCommand(string command, string commandData, string eventToken, TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            // Parse out our external event action
            if (Enum.IsDefined(typeof(Click2CallExternalCommands), command))
            {
                Click2CallExternalCommands externalCommand = WOSI.Utilities.EnumUtils<Click2CallExternalCommands>.Parse(command);

                switch (externalCommand)
                {
                    case Click2CallExternalCommands.CALLBUTLERINTERNAL_ExtensionNotAvailable:

                        // Allow this to answer calls again
                        tsInterface.Locked = false;

                        break;

                    case Click2CallExternalCommands.CALLBUTLERINTERNAL_ConnectCalls:

                        // Check to see if this is a call to another extension
                        int extensionNumber = 0;
                        bool disableCallScreening = callMakerExtension == null ? false : true;

                        if (int.TryParse(numberToDial, out extensionNumber))
                        {
                            // If we're calling our own extension, send us to the voicemail management menu
                            if (extensionNumber == callMakerExtension.ExtensionNumber)
                            {
                                callMakerInterface.ScriptProcessor = new VoicemailManagementScriptProcessor(callMakerExtension, scriptService.registrarService);
                                callMakerInterface.ScriptProcessor.StartProcessing(callMakerInterface, telecomProvider, dataProvider);

                                // Allow this to answer calls again
                                tsInterface.Locked = false;

                                break;
                            }
                            else
                            {
                                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = dataProvider.GetExtensionNumber(Properties.Settings.Default.CustomerID, extensionNumber);

                                if (extension != null)
                                {
                                    scriptService.TransferToExtension(extensionNumber.ToString(), callMakerInterface, disableCallScreening);

                                    // Allow this to answer calls again
                                    tsInterface.Locked = false;

                                    break;
                                }
                            }
                        }

                        // Send the caller to main menu
                        if (numberToDial == "*")
                        {
                            scriptService.SetupAutoAttendantAnswer(callMakerInterface.LineNumber, callMakerInterface);
                            callMakerInterface.ScriptProcessor.StartProcessing(callMakerInterface, telecomProvider, dataProvider);
                            
                            // Allow this to answer calls again
                            tsInterface.Locked = false;

                            break;
                        }

                        // If we get here, we make an outbound call to an external number
                        if(callMakerExtension != null)
                            scriptService.MakeOutboundCall(callMakerInterface, numberToDial, string.Format("{0} {1}", callMakerExtension.FirstName, callMakerExtension.LastName), null, null, callMakerExtension.ExtensionNumber, false, true);

                        // Allow this to answer calls again
                        tsInterface.Locked = false;

                        break;
                }

                tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
            }
        }
    }
}
