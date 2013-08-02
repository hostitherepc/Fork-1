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
    class OutboundCalleeScriptProcessor : ScriptProcessorBase
    {
        public enum OutboundCallExternalCommands
        {
            CALLBUTLERINTERNAL_Reset,
            CALLBUTLERINTERNAL_CallFailed,
            CALLBUTLERINTERNAL_CallConnected,
            CALLBUTLERINTERNAL_CallEnded,
            CALLBUTLERINTERNAL_HoldCaller,
            CALLBUTLERINTERNAL_UnholdCaller,
            CALLBUTLERINTERNAL_TransferCaller
        }

        private string numberToCall = "";
        private string fromCallerID = "";
        private string fromCallerNumber = "";
        private string outboundProfileName = "";

        public OutboundCalleeScriptProcessor(string numberToCall, string fromCallerID, string fromCallerNumber, string outboundProfileName)
        {
            this.numberToCall = numberToCall;
            this.fromCallerID = fromCallerID;
            this.fromCallerNumber = fromCallerNumber;
            this.outboundProfileName = outboundProfileName;
        }

        protected override void OnStartProcessing(TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            string scriptLocation = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory) + "\\Outbound Callee.xml";

            if (File.Exists(scriptLocation))
            {
                IMLScript imlScript = IMLScript.OpenScript(scriptLocation);

                tsInterface.IMLInterpreter.SetLocalVariable("HoldMusicLocation", WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.HoldMusicRootDirectory));
                tsInterface.IMLInterpreter.SetLocalVariable("NumberToCall", numberToCall);
                tsInterface.IMLInterpreter.SetLocalVariable("FromCallerID", fromCallerID);
                tsInterface.IMLInterpreter.SetLocalVariable("FromCallerNumber", fromCallerNumber);
                tsInterface.IMLInterpreter.SetLocalVariable("CallProfile", outboundProfileName);

                tsInterface.IMLInterpreter.StartScript(imlScript, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory));
            }
        }

        protected override void OnExternalCommand(string command, string commandData, string eventToken, TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            // Parse out our external event action
            if (Enum.IsDefined(typeof(OutboundCallExternalCommands), command))
            {
                OutboundCallExternalCommands externalCommand = WOSI.Utilities.EnumUtils<OutboundCallExternalCommands>.Parse(command);

                switch (externalCommand)
                {
                    case OutboundCallExternalCommands.CALLBUTLERINTERNAL_Reset:
                        {
                            tsInterface.Locked = false;
                            break;
                        }
                }
            }

            tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
        }
    }
}
