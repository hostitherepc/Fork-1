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
//using T2.Kinesis.Gidgets;

namespace CallButler.Service.ScriptProcessing
{
    class ExtensionScriptProcessor : ScriptProcessorBase
    {
        private enum ExtensionExternalCommands
        {
            CALLBUTLERINTERNAL_GetNextNumber,
            CALLBUTLERINTERNAL_SendToVoicemail,
            CALLBUTLERINTERNAL_ConnectCalls,
            CALLBUTLERINTERNAL_ConfirmingTransfer,
            CALLBUTLERINTERNAL_ForwardCall
        }

        public enum ExtensionExternalEvents
        {
            CALLBUTLERINTERNAL_NoMoreNumbers,
            CALLBUTLERINTERNAL_OtherCallerHungUp,
            CALLBUTLERINTERNAL_SkipConfirmation,
            CALLBUTLERINTERNAL_GetNextNumber,
            CALLBUTLERINTERNAL_SendToVoicemail,
            CALLBUTLERINTERNAL_ForwardCall
        }

        private enum VoicemailExternalCommands
        {
            CALLBUTLERINTERNAL_EndExtensionFinder
        }

        public enum VoicemailExternalEvents
        {
            CALLBUTLERINTERNAL_ExtensionNotAvailable,
            CALLBUTLERINTERNAL_CallForwarded
        }

        private WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension;
        private WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow parentExtension = null;
        private TelecomScriptInterface tsInterface = null;
        private TelecomScriptInterface onholdTsInterface;
        private VoicemailMailerService vmMailerService;
        private PBXRegistrarService registrarService;
        private int extensionNumberIndex = -1;
        private int parentExtensionIndex = -1;
        private bool disableCallScreening = false;
        private bool autoConnect = true;
        private bool autoAnswer = false;
        ScriptService scriptService;
        //ExtensionStateService extStateService;
        Call callScriptElement;

        public ExtensionScriptProcessor(ScriptService scriptService, TelecomScriptInterface onholdTsInterface, WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension, VoicemailMailerService vmMailerService, PBXRegistrarService registrarService/*, ExtensionStateService extStateService*/, bool disableCallScreening, bool autoConnect, bool autoAnswer)
        {
            this.autoAnswer = autoAnswer;
            this.scriptService = scriptService;
            this.registrarService = registrarService;
            this.onholdTsInterface = onholdTsInterface;
            this.extension = extension;
            this.vmMailerService = vmMailerService;
            //this.extStateService = extStateService;
            this.disableCallScreening = disableCallScreening;
            this.autoConnect = autoConnect;
        }

        public WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow Extension
        {
            get
            {
                return this.extension;
            }
        }

        protected override void OnStartProcessing(TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            tsInterface.IMLInterpreter.DefaultSpeechVoice = Properties.Settings.Default.DefaultTTSVoice;

            // Set our volumes
            telecomProvider.SetRecordVolume(tsInterface.LineNumber, Properties.Settings.Default.RecordVolume);
            telecomProvider.SetSoundVolume(tsInterface.LineNumber, Properties.Settings.Default.SoundVolume);
            telecomProvider.SetSpeechVolume(tsInterface.LineNumber, Properties.Settings.Default.SpeechVolume);

            string extensionFinderScriptLocation = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory) + "\\Extension Finder.xml";

            if (File.Exists(extensionFinderScriptLocation))
            {
                onholdTsInterface.IMLInterpreter.SyncExternalAction += new EventHandler<WOSI.IVR.IML.SyncExternalActionEventArgs>(IMLInterpreter_SyncExternalAction);

                IMLScript imlScript = IMLScript.OpenScript(extensionFinderScriptLocation);

                // Get our call script element
                ScriptElement[] callElements = imlScript.GetAllElementsOfType(typeof(Call));

                if (callElements != null && callElements.Length > 0)
                {
                    callScriptElement = (Call)callElements[0];
                }

                extensionNumberIndex = -1;

                this.tsInterface = tsInterface;

                // Copy our variables
                tsInterface.IMLInterpreter.MergeLocalVariables(onholdTsInterface.IMLInterpreter);

                tsInterface.IMLInterpreter.SetLocalVariable("ExtensionTimeout", "20");
                
                tsInterface.IMLInterpreter.StartScript(imlScript, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory));

                // Tell our extension that they have an incoming call
                //CallInfo callInfo = new CallInfo(tsInterface.CurrentCallID, tsInterface.LineNumber, CallStatus.Incoming, onholdTsInterface.IMLInterpreter.CallerDisplayName, onholdTsInterface.IMLInterpreter.CallerUsername);
                //extStateService.UpdateCallState(extension.ExtensionID, callInfo);
            }
        }

        void IMLInterpreter_SyncExternalAction(object sender, WOSI.IVR.IML.SyncExternalActionEventArgs e)
        {
            // Parse out our external event action
            if (Enum.IsDefined(typeof(VoicemailExternalCommands), e.Action))
            {
                VoicemailExternalCommands externalCommand = WOSI.Utilities.EnumUtils<VoicemailExternalCommands>.Parse(e.Action);

                switch (externalCommand)
                {
                    case VoicemailExternalCommands.CALLBUTLERINTERNAL_EndExtensionFinder:
                        {
                            // Allow this line to answer calls again
                            tsInterface.Locked = false;

                            tsInterface.IMLInterpreter.SignalExternalEvent(ExtensionExternalEvents.CALLBUTLERINTERNAL_OtherCallerHungUp.ToString());

                            // Don't subscribe to this event anymore after the call has ended
                            ((WOSI.IVR.IML.IMLInterpreter)sender).SyncExternalAction -= IMLInterpreter_SyncExternalAction;
                            break;
                        }
                }

                ((WOSI.IVR.IML.IMLInterpreter)sender).SignalEventCallback(e.EventToken);
            }
        }

        private void TryContactNumber(TelecomScriptInterface tsInterface, string numberToCall, string fromCallerID, string fromCallerNumber, string callProfile, string timeout, string eventToken)
        {
            // If we get here, try the number
            tsInterface.IMLInterpreter.SetLocalVariable("NumberToCall", numberToCall);

            if (Properties.Settings.Default.CustomIncomingCallerID != null && Properties.Settings.Default.CustomIncomingCallerID.Length > 0)
            {
                tsInterface.IMLInterpreter.SetLocalVariable("FromCallerID", Properties.Settings.Default.CustomIncomingCallerID);
            }
            else
            {
                tsInterface.IMLInterpreter.SetLocalVariable("FromCallerID", fromCallerID);
            }

            if (Properties.Settings.Default.CustomIncomingCallerNumber != null && Properties.Settings.Default.CustomIncomingCallerNumber.Length > 0)
            {
                tsInterface.IMLInterpreter.SetLocalVariable("FromNumber", Properties.Settings.Default.CustomIncomingCallerNumber);
            }
            else
            {
                tsInterface.IMLInterpreter.SetLocalVariable("FromNumber", fromCallerNumber);
            }

            tsInterface.IMLInterpreter.SetLocalVariable("CallProfile", callProfile);

            SetupScriptForCall(tsInterface, timeout);

            tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
        }

        private void TryCallBlast(CallButler.Telecom.TelecomProviderBase telecomProvider, TelecomScriptInterface tsInterface, string[] numbersToCall, string[] profileNames, string fromCallerID, string fromCallerNumber, string timeout)
        {
            List<WOSI.CallButler.Data.CallButlerDataset.ProvidersRow> providers = new List<WOSI.CallButler.Data.CallButlerDataset.ProvidersRow>();

            foreach (string profileName in profileNames)
            {
                providers.Add(tsInterface.FindProvider(profileName));
            }

            if (Properties.Settings.Default.CustomIncomingCallerID != null && Properties.Settings.Default.CustomIncomingCallerID.Length > 0)
            {
                fromCallerID = Properties.Settings.Default.CustomIncomingCallerID;
            }

            if (Properties.Settings.Default.CustomIncomingCallerNumber != null && Properties.Settings.Default.CustomIncomingCallerNumber.Length > 0)
            {
                fromCallerNumber = Properties.Settings.Default.CustomIncomingCallerNumber;
            }

            telecomProvider.CallBlast(tsInterface.LineNumber, numbersToCall, fromCallerID, fromCallerNumber, providers.ToArray());

            SetupScriptForCall(tsInterface, timeout);
        }

        private void SetupScriptForCall(TelecomScriptInterface tsInterface, string timeout)
        {
            string callFrom = "";

            if (Properties.Settings.Default.CustomCallScreeningPrompt != null && Properties.Settings.Default.CustomCallScreeningPrompt.Length > 0)
            {
                callFrom = Properties.Settings.Default.CustomCallScreeningPrompt;
            }
            else
            {
                if (onholdTsInterface.IMLInterpreter.CallerDisplayName.Length > 0)
                    callFrom = WOSI.Utilities.StringUtils.FormatPhoneNumber(onholdTsInterface.IMLInterpreter.CallerDisplayName);
                else if (onholdTsInterface.IMLInterpreter.CallerUsername.Length > 0)
                    callFrom = WOSI.Utilities.StringUtils.FormatPhoneNumber(onholdTsInterface.IMLInterpreter.CallerUsername);
                else
                    callFrom = "An Unknown Caller.";
            }

            tsInterface.IMLInterpreter.SetLocalVariable("CallFrom", callFrom);
            tsInterface.IMLInterpreter.SetLocalVariable("ExtensionTimeout", timeout);

            // Set our call element to autodial or not
            if (callScriptElement != null)
            {
                callScriptElement.RequestAutoAnswer = autoAnswer;
            }
        }

        public override void OnCallTemporarilyMoved(CallButler.Telecom.TelecomProviderBase telcomProvider, CallButler.Telecom.CallEventArgs e)
        {
            onholdTsInterface.IMLInterpreter.SetLocalVariable("TransferNumber", e.CallingToNumber);
            onholdTsInterface.IMLInterpreter.SignalExternalEvent(VoicemailExternalEvents.CALLBUTLERINTERNAL_CallForwarded.ToString());
        }

        protected override void OnExternalCommand(string command, string commandData, string eventToken, TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            // Parse out our external event action
            if (Enum.IsDefined(typeof(ExtensionExternalCommands), command))
            {
                ExtensionExternalCommands externalCommand = WOSI.Utilities.EnumUtils<ExtensionExternalCommands>.Parse(command);

                switch (externalCommand)
                {
                    case ExtensionExternalCommands.CALLBUTLERINTERNAL_ForwardCall:
                        {

                            onholdTsInterface.IMLInterpreter.SetLocalVariable("TransferNumber", commandData);
                            onholdTsInterface.IMLInterpreter.SignalExternalEvent(VoicemailExternalEvents.CALLBUTLERINTERNAL_CallForwarded.ToString());

                            break;
                        }
                    case ExtensionExternalCommands.CALLBUTLERINTERNAL_ConfirmingTransfer:
                        {
                            if (disableCallScreening || !extension.EnableCallScreening)
                            {
                                tsInterface.IMLInterpreter.SignalExternalEvent(ExtensionExternalEvents.CALLBUTLERINTERNAL_SkipConfirmation.ToString());
                            }
                            else
                            {
                                tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
                            }   

                            break;
                        }
                    case ExtensionExternalCommands.CALLBUTLERINTERNAL_GetNextNumber:
                        {
                            string callerID = onholdTsInterface.IMLInterpreter.CallerDisplayName;
                            string callerNumber = onholdTsInterface.IMLInterpreter.CallerUsername;

                            if (callerID == null || callerID.Length == 0)
                                callerID = "Unknown Caller";

                            if (callerNumber == null || callerNumber.Length == 0)
                                callerNumber = "";

                            // If we have a previous call, end it
                            if (telecomProvider.IsLineInUse(tsInterface.LineNumber))
                            {
                                telecomProvider.EndCall(tsInterface.LineNumber);
                            }
                            else
                            {
                                // Get our extension contact numbers
                                List<WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow> contactNumbers = new List<WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow>((WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow[])dataProvider.GetExtensionContactNumbers(extension.ExtensionID).Select("", "Priority ASC"));

                                if (extensionNumberIndex + 1 >= contactNumbers.Count && parentExtension != null)
                                {
                                    extensionNumberIndex = parentExtensionIndex;
                                    parentExtensionIndex = -1;
                                    
                                    extension = parentExtension;
                                    parentExtension = null;

                                    contactNumbers.Clear();
                                    contactNumbers.AddRange((WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow[])dataProvider.GetExtensionContactNumbers(extension.ExtensionID).Select("", "Priority ASC"));
                                }

                                extensionNumberIndex++;

                                List<string> callBlastNumbers = new List<string>();
                                List<string> callBlastProfiles = new List<string>();
                                int callBlastTimeout = Properties.Settings.Default.CallBlastTimeout;

                                while (extensionNumberIndex < contactNumbers.Count)
                                {
                                    WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow contactNumber = contactNumbers[extensionNumberIndex];

                                    // Is the number online?
                                    if (contactNumber.Online)
                                    {
                                        // Does the number have hours?
                                        TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);

                                        if (!contactNumber.IsHoursOfOperationUTCOffsetNull())
                                            utcOffset = contactNumber.HoursOfOperationUTCOffset;

                                        if (!contactNumber.HasHoursOfOperation || (contactNumber.HasHoursOfOperation && ScriptUtils.IsInHoursOfOperation(contactNumber.HoursOfOperation, utcOffset)))
                                        {
                                            // Check to see if this number is a PBX IP line
                                            if ((contactNumber.CallPBXPhone || (WOSI.CallButler.Data.ExtensionContactNumberType)contactNumber.Type == WOSI.CallButler.Data.ExtensionContactNumberType.IPPhone) && registrarService != null)
                                            {
                                                int extNumber = extension.ExtensionNumber;

                                                // If this was filled in from another extension, we'll need to check the status of that extension
                                                if (contactNumber.ExtensionID != extension.ExtensionID)
                                                {
                                                    WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow tmpExtension = dataProvider.GetExtension(Properties.Settings.Default.CustomerID, contactNumber.ExtensionID);

                                                    if (tmpExtension != null)
                                                        extNumber = tmpExtension.ExtensionNumber;
                                                }

                                                // Check to see if this pbx phone is online
                                                PBXPresenceInfo[] presInfos = registrarService.GetPresenceInfoForExtension(extNumber);

                                                if (presInfos != null && presInfos.Length > 0)
                                                {
                                                    foreach (PBXPresenceInfo presInfo in presInfos)
                                                    {
                                                        if (presInfo.Status == PBXPresenceStatus.Online)
                                                        {
                                                            if (presInfos.Length > 1 || extension.UseCallBlast)
                                                            {
                                                                if (contactNumbers.Count == 1)
                                                                    callBlastTimeout = contactNumber.Timeout;

                                                                string callBlastNumber = string.Format("sip:{0}@{1}:{2}", presInfo.ExtensionNumber, presInfo.RemoteAddress, presInfo.RemotePort);

                                                                if (!callBlastNumbers.Contains(callBlastNumber))
                                                                {
                                                                    callBlastNumbers.Add(callBlastNumber);
                                                                    callBlastProfiles.Add(TelecomScriptInterface.InternalProviderProfileName);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                TryContactNumber(tsInterface, string.Format("sip:{0}@{1}:{2}", presInfo.ExtensionNumber, presInfo.RemoteAddress, presInfo.RemotePort), callerID, callerNumber, TelecomScriptInterface.InternalProviderProfileName, contactNumber.Timeout.ToString(), eventToken);
                                                                return;
                                                            }
                                                        }
                                                    }

                                                    if (!extension.UseCallBlast && callBlastNumbers.Count > 0)
                                                        break;
                                                }
                                            }
                                            else if ((WOSI.CallButler.Data.ExtensionContactNumberType)contactNumber.Type == WOSI.CallButler.Data.ExtensionContactNumberType.TelephoneNumber)
                                            {
                                                if (extension.UseCallBlast)
                                                {
                                                    if (!callBlastNumbers.Contains(contactNumber.ContactNumber))
                                                    {
                                                        callBlastNumbers.Add(contactNumber.ContactNumber);
                                                        callBlastProfiles.Add("");
                                                    }
                                                }
                                                else
                                                {
                                                    TryContactNumber(tsInterface, contactNumber.ContactNumber, callerID, callerNumber, "", contactNumber.Timeout.ToString(), eventToken);
                                                    return;
                                                }
                                            }
                                            else if ((WOSI.CallButler.Data.ExtensionContactNumberType)contactNumber.Type == WOSI.CallButler.Data.ExtensionContactNumberType.Extension && parentExtension == null)
                                            {
                                                try
                                                {
                                                    // Get our new extension
                                                    WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow newExtension = dataProvider.GetExtension(Properties.Settings.Default.CustomerID, new Guid(contactNumber.ContactNumber));

                                                    if (newExtension != null)
                                                    {
                                                        if (extension.UseCallBlast)
                                                        {
                                                            WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow[] newContacts = (WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow[])dataProvider.GetExtensionContactNumbers(newExtension.ExtensionID).Select("Type <> " + (int)WOSI.CallButler.Data.ExtensionContactNumberType.Extension, "Priority ASC");
                                                            contactNumbers.AddRange(newContacts);
                                                        }
                                                        else
                                                        {
                                                            parentExtension = extension;
                                                            parentExtensionIndex = extensionNumberIndex;

                                                            extensionNumberIndex = -1;
                                                            extension = newExtension;

                                                            tsInterface.IMLInterpreter.SignalExternalEvent(ExtensionExternalEvents.CALLBUTLERINTERNAL_GetNextNumber.ToString());
                                                            return;
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                            }
                                        }
                                    }

                                    extensionNumberIndex++;
                                }

                                if (callBlastNumbers.Count > 0)
                                {
                                    TryCallBlast(telecomProvider, tsInterface, callBlastNumbers.ToArray(), callBlastProfiles.ToArray(), callerID, callerNumber, callBlastTimeout.ToString());
                                    return;
                                }
                                else
                                {
                                    tsInterface.IMLInterpreter.SignalExternalEvent(ExtensionExternalEvents.CALLBUTLERINTERNAL_NoMoreNumbers.ToString());
                                }
                            }

                            break;
                        }
                    case ExtensionExternalCommands.CALLBUTLERINTERNAL_SendToVoicemail:
                        {
                            onholdTsInterface.IMLInterpreter.SyncExternalAction -= IMLInterpreter_SyncExternalAction;

                            // Allow this line to answer calls again
                            tsInterface.Locked = false;

                            if (telecomProvider.IsLineInUse(onholdTsInterface.LineNumber))
                            {
                                telecomProvider.SendingToVoicemail(onholdTsInterface.LineNumber);
                            }

                            onholdTsInterface.IMLInterpreter.SignalExternalEvent(VoicemailExternalEvents.CALLBUTLERINTERNAL_ExtensionNotAvailable.ToString());
                            tsInterface.IMLInterpreter.SignalEventCallback(eventToken);

                            break;
                        }
                    case ExtensionExternalCommands.CALLBUTLERINTERNAL_ConnectCalls:
                        {
                            onholdTsInterface.IMLInterpreter.SyncExternalAction -= IMLInterpreter_SyncExternalAction;

                            onholdTsInterface.IMLInterpreter.SignalExternalEvent(ExtensionExternalCommands.CALLBUTLERINTERNAL_ConnectCalls.ToString());

                            // Allow this line to answer calls again
                            tsInterface.Locked = false;

                            if (autoConnect)
                            {
                                if (telecomProvider.IsLineInUse(tsInterface.LineNumber) && telecomProvider.IsLineInUse(onholdTsInterface.LineNumber))
                                {
                                    if (extension.IsUseConferenceTransferNull() || !extension.UseConferenceTransfer /*|| !Licensing.Management.AppPermissions.StatIsPermitted("Handoff")*/)
                                    {
                                        telecomProvider.TransferCallAttended(onholdTsInterface.LineNumber, tsInterface.LineNumber, Properties.Settings.Default.UseBridgedTransfers);
                                        tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
                                    }
                                    else
                                    {
                                        telecomProvider.StopSound(tsInterface.LineNumber);
                                        telecomProvider.StopSound(onholdTsInterface.LineNumber);
                                        int conferenceID = telecomProvider.ConferenceLines(tsInterface.LineNumber, onholdTsInterface.LineNumber);

                                        // Check to see if the person calling is an internal extension
                                        if (onholdTsInterface.Extension != null)
                                        {
                                            onholdTsInterface.ScriptProcessor = new TransferConferenceScriptProcessor(conferenceID, scriptService, tsInterface, registrarService, extension, vmMailerService);
                                        }
                                        else
                                        {
                                            onholdTsInterface.ScriptProcessor = new TransferConferenceParticipantScriptProcessor(conferenceID, tsInterface, extension, vmMailerService);
                                        }

                                        tsInterface.ScriptProcessor = new TransferConferenceScriptProcessor(conferenceID, scriptService, onholdTsInterface, registrarService, extension, vmMailerService);

                                        onholdTsInterface.ScriptProcessor.StartProcessing(onholdTsInterface, telecomProvider, dataProvider);
                                        tsInterface.ScriptProcessor.StartProcessing(tsInterface, telecomProvider, dataProvider);
                                    }
                                }
                                else
                                {
                                    tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
                                }
                            }

                            break;
                        }
                }
            }
        }
    }
}
