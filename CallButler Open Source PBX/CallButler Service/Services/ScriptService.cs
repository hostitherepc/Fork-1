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
using CallButler.Telecom;
using WOSI.CallButler.Data.DataProviders;
using WOSI.IVR.IML;
using WOSI.IVR.IML.Classes;
using CallButler.Service.ScriptProcessing;
using WOSI.CallButler.ManagementInterface;
using WOSI.CallButler.Data;
//using T2.Kinesis.Gidgets;

namespace CallButler.Service.Services
{
    class ScriptService : CallButler.Service.Plugin.ICallButlerDialerProvider
    {
        private class AnswerTimeoutStruct
        {
            public AnswerTimeoutStruct()
            {

            }

            public TelecomProviderBase telecomProvider;
            public int lineNumber;
            public ScriptService scriptService;
            public System.Threading.Timer answerTimer;
            public bool IsInternalCaller;
        }

        private Dictionary<int, TelecomScriptInterface> tsInterfaces;
        private VoicemailService vmService;
        private VoicemailMailerService vmMailerService;
        private WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider;
        private CallButler.Telecom.TelecomProviderBase telecomProvider;
        private Utilities.PluginManagement.PluginManager pluginManager;
        internal PBXRegistrarService registrarService;
        //private ExtensionStateService extStateService;

        public ScriptService(TelecomProviderBase telecomProvider, CallButlerDataProviderBase dataProvider, VoicemailService vmService, VoicemailMailerService vmMailerService, Utilities.PluginManagement.PluginManager pluginManager, PBXRegistrarService registrarService/*, ExtensionStateService extStateService*/)
        {
            this.registrarService = registrarService;
            this.dataProvider = dataProvider;
            this.telecomProvider = telecomProvider;
            this.pluginManager = pluginManager;
            //this.extStateService = extStateService;

            // Create our services
            this.vmService = vmService;
            this.vmMailerService = vmMailerService;

            // Create a new script processor for each of our inbound lines
            tsInterfaces = new Dictionary<int, TelecomScriptInterface>();

            int lineCount = Properties.Settings.Default.LineCount; // Math.Min(Properties.Settings.Default.LineCount, Licensing.Management.AppPermissions.StatGetPermissionScalar("MaxLineCount"));

            for (int index = 1; index <= lineCount; index++)
            {
                TelecomScriptInterface tsInterface = new TelecomScriptInterface(telecomProvider, dataProvider, pluginManager, registrarService/*, extStateService*/, index);
                tsInterfaces.Add(index, tsInterface);

                tsInterface.TransferCall += new EventHandler<WOSI.IVR.IML.TransferEventArgs>(tsInterface_TransferCall);
                tsInterface.IMLInterpreter.ScriptStarted += new EventHandler(IMLInterpreter_ScriptStarted);
                tsInterface.IMLInterpreter.ScriptFinished += new EventHandler(IMLInterpreter_ScriptFinished);
                tsInterface.IMLInterpreter.DtmfTimeoutInMs = Properties.Settings.Default.DTMFTimeoutInMs;
            }

            // Add a startup entry in our log
            LoggingService.AddLogEntry(LogLevel.Basic, Services.PrivateLabelService.ReplaceProductName(Services.PrivateLabelService.ReplaceProductName("CallButler Service Started")), false);

            // Attach to our telecom provider events
            telecomProvider.IncomingCall += new EventHandler<CallEventArgs>(telecomProvider_IncomingCall);
            telecomProvider.Error += new EventHandler<CallButler.Telecom.ErrorEventArgs>(telecomProvider_Error);
            telecomProvider.CallConnected += new EventHandler<CallEventArgs>(telecomProvider_CallConnected);
            telecomProvider.CallEnded += new EventHandler<LineEventArgs>(telecomProvider_CallEnded);
            telecomProvider.CallFailed += new EventHandler<CallFailureEventArgs>(telecomProvider_CallFailed);
            telecomProvider.IncomingBusyCall += new EventHandler<BusyCallEventArgs>(telecomProvider_IncomingBusyCall);
        }

        public void Shutdown()
        {
            telecomProvider.IncomingCall -= telecomProvider_IncomingCall;
            telecomProvider.Error -= telecomProvider_Error;
            telecomProvider.CallConnected -= telecomProvider_CallConnected;
            telecomProvider.CallEnded -= telecomProvider_CallEnded;
            telecomProvider.CallFailed -= telecomProvider_CallFailed;

            for (int index = 1; index <= telecomProvider.LineCount; index++)
            {
                if (telecomProvider.IsLineInUse(index))
                    telecomProvider.EndCall(index);
            }

            LoggingService.AddLogEntry(LogLevel.Basic, Services.PrivateLabelService.ReplaceProductName("CallButler Service Stopped"), false);
        }

        #region Outbound Call Functions
        public void PlaceScheduleReminderCall(Guid extensionID, OutlookReminder [] reminders)
        {
            try
            {
                // If the transfer is an extension, find the extension
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = dataProvider.GetExtension(Properties.Settings.Default.CustomerID, extensionID);

                if (extension != null)
                {
                    if (!extension.DoNotDisturb)
                    {
                        int openLineNumber = 0;

                        // Find an open line
                        for (int index = 1; index <= telecomProvider.LineCount; index++)
                        {
                            if (!telecomProvider.IsLineInUse(index))
                            {
                                // Disable the line for incoming calls
                                tsInterfaces[index].Locked = true;
                                openLineNumber = index;
                                break;
                            }
                        }

                        if (openLineNumber != 0)
                        {
                            // If we get here, we have an open line and we should start processing our schedule reminder
                            tsInterfaces[openLineNumber].ScriptProcessor = new ScheduleReminderScriptProcessor(extension, reminders );
                            tsInterfaces[openLineNumber].ScriptProcessor.StartProcessing(tsInterfaces[openLineNumber], telecomProvider, dataProvider);

                            return;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void PlaceGreetingRecordCall(string numberToCall, Guid greetingID, string languageID)
        {
            /*string recordGreetingScriptLocation = Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory) + "\\Greeting Recording.xml";
            string greetingDirectory = Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingRecordingTempPath) + "\\" + languageID;
            string greetingFilename = greetingDirectory + "\\" + greetingID.ToString() + ".snd";

            if (!Directory.Exists(greetingDirectory))
                Directory.CreateDirectory(greetingDirectory);

            System.Collections.Specialized.NameValueCollection scriptVars = new System.Collections.Specialized.NameValueCollection();

            scriptVars["GreetingFileLocation"] = greetingFilename;

            PlaceOutboundCall(Guid.Empty, numberToCall, "CallButler Greeting Recording", recordGreetingScriptLocation, scriptVars);*/
        }

        #region ICallButlerDialerProvider Implementation
        public bool PlaceOutboundCall(string jobID, string callID, Guid providerID, string callTo, string fromCallerID, string fromCallerNumber, string scriptToRun, string answeringMachineScriptToRun, string answeringMachineDetectionSettings, System.Collections.Specialized.NameValueCollection scriptVariables, int timeout, CallButler.Service.Plugin.CallButlerDialerPlugin dialerPlugin)
        {
            WOSI.CallButler.Data.CallButlerDataset.ProvidersRow provider = dataProvider.GetProviders(Properties.Settings.Default.CustomerID).FindByProviderID(providerID);

            return PlaceOutboundCall(jobID, callID, provider, callTo, fromCallerID, fromCallerNumber, scriptToRun, answeringMachineScriptToRun, answeringMachineDetectionSettings, scriptVariables, timeout, dialerPlugin);
        }

        public bool PlaceOutboundCall(string jobID, string callID, string providerName, string callTo, string fromCallerID, string fromCallerNumber, string scriptToRun, string answeringMachineScriptToRun, string answeringMachineDetectionSettings, System.Collections.Specialized.NameValueCollection scriptVariables, int timeout, CallButler.Service.Plugin.CallButlerDialerPlugin dialerPlugin)
        {
            WOSI.CallButler.Data.CallButlerDataset.ProvidersRow[] providers = (WOSI.CallButler.Data.CallButlerDataset.ProvidersRow[])dataProvider.GetProviders(Properties.Settings.Default.CustomerID).Select("Name = '" + providerName + "'");
            WOSI.CallButler.Data.CallButlerDataset.ProvidersRow provider = null;

            if (providers.Length > 0)
                provider = providers[0];

            return PlaceOutboundCall(jobID, callID, provider, callTo, fromCallerID, fromCallerNumber, scriptToRun, answeringMachineScriptToRun, answeringMachineDetectionSettings, scriptVariables, timeout, dialerPlugin);
        }

        public int AvailableLines
        {
            get
            {
                int availableLineCount = 0;

                for (int index = 1; index <= telecomProvider.LineCount; index++)
                {
                    if (tsInterfaces[index].IsAvailable)
                    {
                        availableLineCount++;
                    }
                }

                return availableLineCount;
            }
        }

        public int TotalLines
        {
            get
            {
                return tsInterfaces.Count;
            }
        }
        #endregion

        public bool PlaceOutboundCall(string jobID, string callID, WOSI.CallButler.Data.CallButlerDataset.ProvidersRow provider, string callTo, string fromCallerID, string fromCallerNumber, string scriptToRun, string answeringMachineScriptToRun, string answeringMachineDetectionSettings, System.Collections.Specialized.NameValueCollection scriptVariables, int timeout, CallButler.Service.Plugin.CallButlerDialerPlugin dialerPlugin)
        {
            if (File.Exists(scriptToRun))
            {
                int openLine = FindAndHoldOpenLine();

                if (openLine != 0)
                {
                    tsInterfaces[openLine].ScriptProcessor = new AutoDialerProcessor(telecomProvider, tsInterfaces[openLine], jobID, callID, scriptToRun, answeringMachineScriptToRun, answeringMachineDetectionSettings, timeout, scriptVariables, dialerPlugin);

                    // Place our outbound call
                    telecomProvider.Call(openLine, callTo, fromCallerID, fromCallerNumber, false, provider);

                    return true;
                }
                else
                {
                    if (dialerPlugin != null)
                        dialerPlugin.OnCallStatus(jobID, callID, CallButler.Service.Plugin.CallButlerDialerPlugin.CallStatus.NoLinesAvailable);
                }
            }
            else
            {
                if (dialerPlugin != null)
                    dialerPlugin.OnError(jobID, callID, "Could not find the script located at '" + scriptToRun + "'");
            }

            return false;
        }
        #endregion

        #region General Script Functions
        /*public bool ExecuteScript(string scriptToRun, System.Collections.Specialized.NameValueCollection scriptVariables)
        {
            if (File.Exists(scriptToRun))
            {
                int openLine = FindAndHoldOpenLine();

                if (openLine != 0)
                {
                    tsInterfaces[openLine].ScriptProcessor = new AutoScriptProcessor(scriptToRun, scriptVariables);
                    tsInterfaces[openLine].ScriptProcessor.StartProcessing(tsInterfaces[openLine], telecomProvider, dataProvider);

                    return true;
                }
            }

            return false;
        }*/
        #endregion

        #region Click to Call Functions
        public bool Click2Call(WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension, string toNumber, bool autoAnswer)
        {
            // Check to see if there is an open line first
            int lineNumber1 = FindAndHoldOpenLine();

            if (lineNumber1 > 0)
            {
                int lineNumber2 = FindAndHoldOpenLine();

                if (lineNumber2 == 0)
                {
                    ReleaseHeldLine(lineNumber1);
                }
                else
                {
                    tsInterfaces[lineNumber1].ScriptProcessor = new Click2CallScriptProcessor(this, toNumber, extension, tsInterfaces[lineNumber2]);
                    tsInterfaces[lineNumber1].ScriptProcessor.StartProcessing(tsInterfaces[lineNumber1], telecomProvider, dataProvider);

                    tsInterfaces[lineNumber2].ScriptProcessor = new ExtensionScriptProcessor(this, tsInterfaces[lineNumber1], extension, vmMailerService, registrarService/*, extStateService*/, true, false, autoAnswer);
                    tsInterfaces[lineNumber2].ScriptProcessor.StartProcessing(tsInterfaces[lineNumber2], telecomProvider, dataProvider);
                    return true;
                }
            }
            
            return false;
        }
        #endregion

        /*public bool PlaceConnectedCall(string number, int extensionNumber)
        {
            int lineNumber = FindAndHoldOpenLine();

            if (lineNumber > 0)
            {
                tsInterfaces[lineNumber].ScriptProcessor = new CallConnectorScriptProcessor(number, extensionNumber);
                tsInterfaces[lineNumber].ScriptProcessor.StartProcessing(tsInterfaces[openLineNumber], telecomProvider, dataProvider);
                return true;
            }

            return false;
        }

        public bool PlaceConnectedCall(string number1, bool enablePBXFeatures1, string number2, bool enablePBXFeatures2)
        {
            bool canPlaceCall = false;

            int lineNumber1 = FindAndHoldOpenLine();

            if (lineNumber1 > 0)
            {
                int lineNumber2 = FindAndHoldOpenLine();

                if (lineNumber2 > 0)
                {
                    tsInterfaces[lineNumber1].ScriptProcessor = new CallConnectorScriptProcessor(number1, tsInterfaces[lineNumber1], tsInterfaces[lineNumber2], enablePBXFeatures1);
                    tsInterfaces[lineNumber1].ScriptProcessor.StartProcessing(tsInterfaces[openLineNumber], telecomProvider, dataProvider);

                    tsInterfaces[lineNumber2].ScriptProcessor = new CallConnectorScriptProcessor(number2, tsInterfaces[lineNumber2], tsInterfaces[lineNumber1], enablePBXFeatures2);
                    tsInterfaces[lineNumber2].ScriptProcessor.StartProcessing(tsInterfaces[openLineNumber], telecomProvider, dataProvider);
                }
                else
                {
                    // If we cant find another line, allow the first to be used again.
                    tsInterfaces[lineNumber1].AutoAnswer = true;
                }
            }

            return canPlaceCall;
        }*/

        public Dictionary<int, TelecomScriptInterface> TelecomScriptInterfaces
        {
            get
            {
                return tsInterfaces;
            }
        }

        public void UpdatePerformanceCounters()
        {
            if (Properties.Settings.Default.EnablePerformanceCounters)
            {
                int linesInUse = 0;
                int lockedLines = 0;
                int scriptsRunningCount = 0;

                for (int index = 1; index <= telecomProvider.LineCount; index++)
                {
                    if (telecomProvider.IsLineInUse(index))
                        linesInUse++;

                    if (tsInterfaces[index].Locked)
                        lockedLines++;

                    if (tsInterfaces[index].IMLInterpreter.ScriptIsRunning)
                        scriptsRunningCount++;
                }

                PerformanceCounterService.ConcurrentCalls = linesInUse;
                PerformanceCounterService.LockedLines = lockedLines;
                PerformanceCounterService.ScriptsRunning = scriptsRunningCount;
            }
        }

        void IMLInterpreter_ScriptFinished(object sender, EventArgs e)
        {
            UpdatePerformanceCounters();
        }

        void IMLInterpreter_ScriptStarted(object sender, EventArgs e)
        {
            UpdatePerformanceCounters();
        }

        private int FindAndHoldOpenLine()
        {
            int openLineNumber = 0;

            for (int index = 1; index <= telecomProvider.LineCount; index++)
            {
                if (tsInterfaces[index].IsAvailable)
                {
                    // Disable the line for incoming calls
                    tsInterfaces[index].Locked = true;

                    openLineNumber = index;
                    break;
                }
            }

            if (openLineNumber == 0)
                LoggingService.AddLogEntry(LogLevel.ErrorsOnly, "Unable to find an open line", true);

            return openLineNumber;
        }

        private void ReleaseHeldLine(int lineNumber)
        {
            tsInterfaces[lineNumber].Locked = false;
        }

        public void TransferToExtension(string extensionNumber, TelecomScriptInterface tsInterface, bool disableCallScreening)
        {
            TransferToExtension(extensionNumber, null, tsInterface, disableCallScreening);
        }

        public void TransferToExtension(string extensionNumber, ScriptProcessorBase onHoldScriptProcessor, TelecomScriptInterface tsInterface, bool disableCallScreening)
        {
            try
            {
                int extNumber = Convert.ToInt32(extensionNumber);
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension;

                // If the transfer is an extension, find the extension
                if (extNumber == 0)
                {
                    // Get our receptionist extension
                    extension = dataProvider.GetExtension(Properties.Settings.Default.CustomerID, Properties.Settings.Default.ReceptionistExtensionID);
                }
                else
                {
                    extension = dataProvider.GetExtensionNumber(Properties.Settings.Default.CustomerID, extNumber);
                }

                if (extension != null)
                {
                    // Start the voicemail processing script
                    if (onHoldScriptProcessor == null)
                        tsInterface.ScriptProcessor = new VoicemailScriptProcessor(extension, vmService , registrarService, pluginManager);
                    else
                        tsInterface.ScriptProcessor = onHoldScriptProcessor;

                    tsInterface.ScriptProcessor.StartProcessing(tsInterface, telecomProvider, dataProvider);

                    if (!extension.DoNotDisturb /*&& Licensing.Management.AppPermissions.StatIsPermitted("Extensions.TelephoneNumbers")*/)
                    {
                        int openLineNumber = FindAndHoldOpenLine();

                        if (openLineNumber != 0)
                        {       
                            // If we get here, we have an open line and we should start processing our find me follow me
                            tsInterfaces[openLineNumber].ScriptProcessor = new ExtensionScriptProcessor(this, tsInterface, extension, vmMailerService, registrarService/*, extStateService*/, disableCallScreening, true, false);
                            tsInterfaces[openLineNumber].ScriptProcessor.StartProcessing(tsInterfaces[openLineNumber], telecomProvider, dataProvider);
                            
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingService.AddLogEntry(LogLevel.ErrorsOnly, Utils.ErrorUtils.FormatErrorString(ex), true);
            }
          
            // If we get here, the transfer has failed
            tsInterface.IMLInterpreter.SignalTransferFailure();
        }

        void tsInterface_TransferCall(object sender, WOSI.IVR.IML.TransferEventArgs e)
        {
            TelecomScriptInterface tsInterface = (TelecomScriptInterface)sender;

            if (e.UseBridge)
            {
                // Bridge the call with an outbound call
                if (MakeOutboundCall(tsInterface, e.TransferTo, e.FromCallerName, e.FromCallerNumber, e.OutboundBridgeProfileName, -1, false, false))
                {
                    tsInterface.ScriptProcessor.StartProcessing(tsInterface, telecomProvider, dataProvider);
                }
                else
                {
                    tsInterface.IMLInterpreter.SignalTransferFailure();
                }
            }
            else if (!e.IsExtension)
            {
                // If the call is a call to telephone number, blind transfer it
                telecomProvider.TransferCall(tsInterface.LineNumber, e.TransferTo);
                tsInterface.IMLInterpreter.SignalEventCallback(e.EventToken);
            }
            else
            {
                TransferToExtension(e.TransferTo, tsInterface, false);
                tsInterface.IMLInterpreter.SignalEventCallback(e.EventToken);
            }
        }

        void telecomProvider_IncomingBusyCall(object sender, BusyCallEventArgs e)
        {
            if (/*Licensing.Management.AppPermissions.StatIsPermitted("Settings.FailoverServer") && */ Properties.Settings.Default.BusyRedirectServer != null && Properties.Settings.Default.BusyRedirectServer.Length > 0)
            {
                // The line is busy and we should redirect the call to a different server
                telecomProvider.RedirectBusyCall(e.CallID, Properties.Settings.Default.BusyRedirectServer);
            }
            else
            {
                // The line is busy and we should end the call
                telecomProvider.DeclineBusyCall(e.CallID);
            }
        }

        void telecomProvider_Error(object sender, CallButler.Telecom.ErrorEventArgs e)
        {
            LoggingService.AddLogEntry(LogLevel.ErrorsOnly, string.Format("Telecom Provider Error\r\n\r\n{0}\r\n\r\n{1}", e.ErrorMessage, e.ErrorDetail), true);
        }

        void telecomProvider_IncomingCall(object sender, CallEventArgs e)
        {
            UpdatePerformanceCounters();

            // Add a log entry
            LoggingService.AddLogEntry(LogLevel.Basic, "(Line " + e.LineNumber + ") Incoming call from " + e.CallerDisplayName + " " + e.CallerPhoneNumber + " to " + e.CallingToNumber + " " + e.CallingToMiscInfo, false);

            // Check to see if the service is supposed to be running and that we have a valid license.
            if (/*(LicenseService.IsLicensed() || LicenseService.IsTrialLicense() || Properties.Settings.Default.IsFreeVersion) &&*/ Properties.Settings.Default.ServiceEnabled)
            {
                TelecomScriptInterface tsInterface = tsInterfaces[e.LineNumber];

                // Populate our caller variables into the IML Interpreter so they can be used in our script
                tsInterface.IMLInterpreter.CallerDisplayName = e.CallerDisplayName;
                tsInterface.IMLInterpreter.CallerHost = e.CallerMiscInfo;
                tsInterface.IMLInterpreter.CallerUsername = e.CallerPhoneNumber;
                tsInterface.IMLInterpreter.DialedUsername = e.CallingToNumber;
                tsInterface.IMLInterpreter.DialedHost = e.CallingToMiscInfo;

                // Set the profile parameter of our tsInterface. This is usually the profile for the incoming call.
                //tsInterface.Profile = e.Tag;

                // Check to see if this caller is asking for a direct extension number
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = null;
                tsInterface.Extension = null;
                int internalCallerExtension = 0;

                // Check to see if this is an internal caller
                if (registrarService != null)
                {
                    try
                    {
                        WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow internalExtensionRow = dataProvider.GetExtensionNumber(Properties.Settings.Default.CustomerID, Convert.ToInt32(e.CallerPhoneNumber));

                        if (internalExtensionRow != null)
                        {
                            tsInterface.Extension = internalExtensionRow;
                            internalCallerExtension = internalExtensionRow.ExtensionNumber;

                            // Update our extension status
                            /*if (Properties.Settings.Default.EnableKinesisServer)
                            {
                                tsInterface.UpdateExtensionCallStatus(CallStatus.Dialing);
                            }*/
                        }
                    }
                    catch(Exception ex)
                    {
                    }
                }

                try
                {
                    int extensionNumber = Convert.ToInt32(e.CallingToNumber);
                    extension = dataProvider.GetExtensionNumber(Properties.Settings.Default.CustomerID, extensionNumber);
                }
                catch
                {
                }

                if (extension != null)
                {
                    // If this extension is calling itself, send it to the voicemail management script, otherwise send it to the extension.
                    if (e.CallerPhoneNumber == e.CallingToNumber)
                    {
                        // Start the voicemail processing script
                        tsInterface.ScriptProcessor = new VoicemailManagementScriptProcessor(extension, registrarService);

                        if (telecomProvider.IsLineInUse(e.LineNumber))
                        {
                            telecomProvider.AnswerCall(e.LineNumber, tsInterface.Extension == null ? false : true);
                        }
                    }
                    else
                    {
                        // Send the caller to the requested extension
                        if (telecomProvider.IsLineInUse(e.LineNumber))
                        {
                            telecomProvider.AnswerCall(e.LineNumber, tsInterface.Extension == null ? false : true);
                        }

                        TransferToExtension(extension.ExtensionNumber.ToString(), tsInterface, true);
                    }

                    return;
                }

                if (tsInterface.Extension != null)
                {
                    // If dialing "*", send the internal caller to the main menu
                    if (e.CallingToNumber == "*" || e.CallingToNumber.Trim() == "")
                    {
                    }
                    // This is an internal caller trying to make an outbound call
                    else if (Properties.Settings.Default.AllowOutboundDialing && e.CallingToNumber.StartsWith(Properties.Settings.Default.OutboundDialingPrefix))
                    {
                        if (MakeOutboundCall(tsInterface, e.CallingToNumber, e.CallerDisplayName, null, null, internalCallerExtension, true, false))
                        {
                            telecomProvider.AnswerCall(e.LineNumber, tsInterface.Extension == null ? false : true);

                            return;
                        }
                    }
                }

                // Should we be trying the receptionist first?
                if (Properties.Settings.Default.TryReceptionistFirst)
                {
                    WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow recepExtension = dataProvider.GetExtension(Properties.Settings.Default.CustomerID, Properties.Settings.Default.ReceptionistExtensionID);

                    if (recepExtension != null)
                    {
                        ReceptionistFinderScriptProcessor recepSp = new ReceptionistFinderScriptProcessor(recepExtension, this);
                        TransferToExtension(recepExtension.ExtensionNumber.ToString(), recepSp, tsInterface, true);
                        return;
                    }
                }

                ProcessAutoAttendantAnswer(e.LineNumber, tsInterface, true);
            }
        }

        public bool MakeOutboundCall(TelecomScriptInterface tsInterface, string numberToDial, string callerDisplayName, string callerNumber, string profileName, int internalCallerExtension, bool trimPrefix, bool autoStart)
        {
            string outboundNumber = numberToDial;

            // Trim the prefix off the dialed number
            if (trimPrefix && Properties.Settings.Default.OutboundDialingPrefix != null && Properties.Settings.Default.OutboundDialingPrefix.Length > 0)
            {
                outboundNumber = outboundNumber.Substring(Properties.Settings.Default.OutboundDialingPrefix.Length);
            }

            // Look for an available line
            int openLineNumber = FindAndHoldOpenLine();

            TelecomScriptInterface outboundTsInterface = null;

            // If there is a line available, start our outbound call.
            if (openLineNumber != 0)
            {
                outboundTsInterface = tsInterfaces[openLineNumber];
                outboundTsInterface.ScriptProcessor = new OutboundCalleeScriptProcessor(outboundNumber, callerDisplayName, callerNumber, profileName);
            }

            if (telecomProvider.IsLineInUse(tsInterface.LineNumber))
            {
                tsInterface.ScriptProcessor = new OutboundScriptProcessor(tsInterface, outboundTsInterface, internalCallerExtension);

                if (autoStart)
                {
                    tsInterface.ScriptProcessor.StartProcessing(tsInterface, telecomProvider, dataProvider);
                }
            }
            else
            {
                tsInterfaces[openLineNumber].Locked = false;
                return false;
            }

            return true;
        }

        void telecomProvider_CallFailed(object sender, CallFailureEventArgs e)
        {
            UpdatePerformanceCounters();
        }

        void telecomProvider_CallEnded(object sender, LineEventArgs e)
        {
            UpdatePerformanceCounters();
        }

        void telecomProvider_CallConnected(object sender, CallEventArgs e)
        {
            PerformanceCounterService.IncrementTotalCalls();
            UpdatePerformanceCounters();
        }

        public void ProcessAutoAttendantAnswer(int lineNumber, TelecomScriptInterface tsInterface, bool enableAnswerDelay)
        {
            SetupAutoAttendantAnswer(lineNumber, tsInterface);
            ProcessAnswerCall(lineNumber, tsInterface.Extension == null ? false : true, enableAnswerDelay);
        }

        internal void SetupAutoAttendantAnswer(int lineNumber, TelecomScriptInterface tsInterface)
        {
            // Should we be using our custom script schedule?
            if (Properties.Settings.Default.ExpertMode /*&& Licensing.Management.AppPermissions.StatIsPermitted("CustomScripting")*/)
            {
                string scriptLocation = GetCurrentExpertScript();

                if (scriptLocation != null && File.Exists(scriptLocation))
                {
                    tsInterface.ScriptProcessor = new ExpertScriptProcessor(scriptLocation);
                    return;
                }
            }

            // If we get here we should attach our standard script processor to this script interface
            tsInterface.ScriptProcessor = new StandardScriptProcessor(pluginManager, registrarService);
        }

        private void ProcessAnswerCall(int lineNumber, bool isInternalCaller, bool enableAnswerDelay)
        {
            //if (!telecomProvider.IsLineInUse(lineNumber))
            //    return;

            if (!enableAnswerDelay || Properties.Settings.Default.AnswerTimeout == 0)
            {
                try
                {
                    if (telecomProvider.IsLineInUse(lineNumber))
                    {
                        telecomProvider.AnswerCall(lineNumber, isInternalCaller);
                    }
                }
                catch(Exception e)
                {
                    LoggingService.AddLogEntry(LogLevel.ErrorsOnly, Utils.ErrorUtils.FormatErrorString(e), true);
                }
            }
            else
            {
                if (Properties.Settings.Default.UseSleepForAnswerDelay)
                {
                    try
                    {
                        System.Threading.Thread.Sleep(Properties.Settings.Default.AnswerTimeout);

                        if (telecomProvider.IsLineInUse(lineNumber))
                        {
                            telecomProvider.AnswerCall(lineNumber, isInternalCaller);
                        }
                    }
                    catch (Exception e)
                    {
                        LoggingService.AddLogEntry(LogLevel.ErrorsOnly, Utils.ErrorUtils.FormatErrorString(e), true);
                    }
                }
                else
                {
                    AnswerTimeoutStruct ats = new AnswerTimeoutStruct();
                    ats.lineNumber = lineNumber;
                    ats.telecomProvider = telecomProvider;
                    ats.scriptService = this;
                    ats.IsInternalCaller = isInternalCaller;

                    ats.answerTimer = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessAnswerTimeoutProc), ats, Properties.Settings.Default.AnswerTimeout, System.Threading.Timeout.Infinite);
                }
            }
        }

        private static void ProcessAnswerTimeoutProc(object state)
        {
            AnswerTimeoutStruct ats = (AnswerTimeoutStruct)state;

            ats.answerTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            ats.answerTimer.Dispose();

            try
            {
                ats.telecomProvider.AnswerCall(ats.lineNumber, ats.IsInternalCaller);
            }
            catch (Exception e)
            {
                LoggingService.AddLogEntry(LogLevel.ErrorsOnly, Utils.ErrorUtils.FormatErrorString(e), true);
            }
        }

        private string GetCurrentExpertScript()
        {
            WOSI.CallButler.Data.CallButlerDataset.ScriptSchedulesRow[] scriptSchedules = (WOSI.CallButler.Data.CallButlerDataset.ScriptSchedulesRow[])dataProvider.GetScriptSchedules(Properties.Settings.Default.CustomerID).Select("Enabled = True", "Priority ASC");

            foreach (WOSI.CallButler.Data.CallButlerDataset.ScriptSchedulesRow scriptSchedule in scriptSchedules)
            {
                if (scriptSchedule.HasHoursOfOperation)
                {
                    // Check to see if we're within our hours of operation
                    if (ScriptProcessing.ScriptCompilers.ScriptUtils.IsInHoursOfOperation(scriptSchedule.HoursOfOperation, scriptSchedule.HoursOfOperationUTCOffset))
                        return scriptSchedule.ScriptLocation;
                }
                else
                {
                    return scriptSchedule.ScriptLocation;
                }
            }

            return null;
        }
    }
}
