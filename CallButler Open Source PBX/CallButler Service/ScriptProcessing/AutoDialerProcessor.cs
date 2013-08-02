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
    class AutoDialerProcessor : ScriptProcessorBase
    {
        string scriptLocation = "";
        string answeringMachineScriptLocation = null;
        string answeringMachineDetectionSettings = null;
        string callID = "";
        string jobID = "";
        CallButler.Service.Plugin.CallButlerDialerPlugin dialerPlugin;
        System.Collections.Specialized.NameValueCollection scriptVariables;
        TelecomScriptInterface tsInterface;
        Telecom.TelecomProviderBase telecomProvider;
        private bool connected = false;
        private System.Threading.Timer timeoutTimer;

        public AutoDialerProcessor(Telecom.TelecomProviderBase telecomProvider, TelecomScriptInterface tsInterface, string jobID, string callID, string scriptToRun, string answeringMachineScriptToRun, string answeringMachineDetectionSettings, int timeout, System.Collections.Specialized.NameValueCollection scriptVariables, CallButler.Service.Plugin.CallButlerDialerPlugin dialerPlugin)
        {
            this.callID = callID;
            this.jobID = jobID;
            this.scriptLocation = scriptToRun;
            this.answeringMachineScriptLocation = answeringMachineScriptToRun;
            this.answeringMachineDetectionSettings = answeringMachineDetectionSettings;
            this.scriptVariables = scriptVariables;
            this.dialerPlugin = dialerPlugin;
            this.tsInterface = tsInterface;
            this.telecomProvider = telecomProvider;

            timeoutTimer = new System.Threading.Timer(new System.Threading.TimerCallback(TimeoutTimerProc), this, timeout * 1000, System.Threading.Timeout.Infinite);
        }

        protected override void OnStartProcessing(TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            telecomProvider.EnableAnsweringMachineDetection(tsInterface.LineNumber, false);

            if (answeringMachineScriptLocation == null || answeringMachineScriptLocation.Length == 0)
                StartScript(scriptLocation);
        }

        private void StartScript(string scriptLocation)
        {
            IMLScript imlScript = IMLScript.OpenScript(scriptLocation);

            // Set our script variables
            if (scriptVariables != null)
            {
                foreach (string name in scriptVariables.AllKeys)
                {
                    this.tsInterface.IMLInterpreter.SetLocalVariable(name, scriptVariables[name]);
                }
            }

            this.tsInterface.IMLInterpreter.StartScript(imlScript, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory));
        }

        public override void OnAnswerDetectHuman(CallButler.Telecom.TelecomProviderBase telecomProvider, CallButler.Telecom.LineEventArgs e)
        {
            telecomProvider.EnableAnsweringMachineDetection(tsInterface.LineNumber, false);

            StartScript(scriptLocation);

            if (dialerPlugin != null)
                dialerPlugin.OnCallStatus(jobID, callID, CallButler.Service.Plugin.CallButlerDialerPlugin.CallStatus.HumanAnswered);
        }

        public override void OnAnswerDetectMachine(CallButler.Telecom.TelecomProviderBase telecomProvider, CallButler.Telecom.LineEventArgs e)
        {
            if (dialerPlugin != null)
                dialerPlugin.OnCallStatus(jobID, callID, CallButler.Service.Plugin.CallButlerDialerPlugin.CallStatus.MachineAnswered);
        }

        public override void OnAnswerDetectMachineGreetingFinished(CallButler.Telecom.TelecomProviderBase telecomProvider, CallButler.Telecom.LineEventArgs e)
        {
            telecomProvider.EnableAnsweringMachineDetection(tsInterface.LineNumber, false);
            StartScript(answeringMachineScriptLocation);
        }

        private static void TimeoutTimerProc(object state)
        {
            AutoDialerProcessor adp = (AutoDialerProcessor)state;

            if (!adp.connected)
            {
                if(adp.telecomProvider.IsLineInUse(adp.tsInterface.LineNumber))
                    adp.telecomProvider.EndCall(adp.tsInterface.LineNumber);

                adp.dialerPlugin.OnCallStatus(adp.jobID, adp.callID, CallButler.Service.Plugin.CallButlerDialerPlugin.CallStatus.NotAnswered);
            }

            adp.timeoutTimer.Dispose();
        }

        public override void OnCallFailed(CallButler.Telecom.TelecomProviderBase telecomProvider, CallButler.Telecom.LineEventArgs e)
        {
            connected = true;
            tsInterface.Locked = false;

            if (dialerPlugin != null)
                dialerPlugin.OnCallStatus(jobID, callID, CallButler.Service.Plugin.CallButlerDialerPlugin.CallStatus.Failed);
        }

        public override void OnCallConnected(CallButler.Telecom.TelecomProviderBase telecomProvider, CallButler.Telecom.LineEventArgs e)
        {
            connected = true;

            if (answeringMachineScriptLocation != null && answeringMachineScriptLocation.Length > 0)
            {
                if (answeringMachineDetectionSettings != null && answeringMachineDetectionSettings.Length > 0)
                    telecomProvider.SetAnsweringMachineDetectionSettings(tsInterface.LineNumber, answeringMachineDetectionSettings);

                telecomProvider.EnableAnsweringMachineDetection(tsInterface.LineNumber, true);
            }

            if(dialerPlugin != null && (answeringMachineScriptLocation == null || answeringMachineScriptLocation.Length == 0))
                dialerPlugin.OnCallStatus(jobID, callID, CallButler.Service.Plugin.CallButlerDialerPlugin.CallStatus.Answered);
        }
    }
}
