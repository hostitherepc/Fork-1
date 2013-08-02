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
using CallButler.Telecom;
using WOSI.IVR.IML;
using WOSI.IVR.IML.Classes;
using WOSI.IVR.IML.Classes.ScriptActions;
using WOSI.IVR.IML.Classes.ScriptEvents;
using System.IO;
using CallButler.Service.Services;
using WOSI.CallButler.ManagementInterface;
//using T2.Kinesis.Gidgets;

namespace CallButler.Service.ScriptProcessing
{
    internal class TelecomScriptInterface
    {
        public const string InternalProviderProfileName = "**INTERNAL**";

        public event EventHandler<WOSI.IVR.IML.TransferEventArgs> TransferCall;

        private WOSI.IVR.IML.IMLInterpreter imlInterp;
        private ScriptProcessorBase scriptProcessor;
        private string imlScriptEventToken = "";
        private CallButler.Telecom.TelecomProviderBase telecomProvider;
        private WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider;
        private int lineNumber = 0;
        private string lastRecording = "";
        private bool recording = false;
        private bool locked = false;
        private bool autoRunScript = true;
        private DateTime callStartTime;
        private Utilities.PluginManagement.PluginManager pluginManager;
        private WOSI.CallButler.Data.CallButlerDataset.ProvidersRow defaultInternalProvider;
        //private ExtensionStateService extStateService;
        private PBXRegistrarService pbxRegistrar;
        private WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension;
        private Guid callID;

        public TelecomScriptInterface(CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider, Utilities.PluginManagement.PluginManager pluginManager, PBXRegistrarService pbxRegistrar/*, ExtensionStateService extStateService*/, int lineNumber)
        {
            this.telecomProvider = telecomProvider;
            this.dataProvider = dataProvider;
            this.lineNumber = lineNumber;
            this.pluginManager = pluginManager;
            this.pbxRegistrar = pbxRegistrar;
            //this.extStateService = extStateService;

            // Attach to our telecom provider events
            telecomProvider.CallEnded += new EventHandler<LineEventArgs>(telecomProvider_CallEnded);
            telecomProvider.DTMFToneRecognized += new EventHandler<CallInputEventArgs>(telecomProvider_DTMFToneRecognized);
            telecomProvider.FinishedSpeaking += new EventHandler<LineEventArgs>(telecomProvider_FinishedSpeaking);
            telecomProvider.SoundFinishedPlaying += new EventHandler<LineEventArgs>(telecomProvider_SoundFinishedPlaying);
            telecomProvider.TransferFailed += new EventHandler<LineEventArgs>(telecomProvider_TransferFailed);
            telecomProvider.TransferSucceeded += new EventHandler<LineEventArgs>(telecomProvider_TransferSucceeded);
            telecomProvider.CallConnected += new EventHandler<CallEventArgs>(telecomProvider_CallConnected);
            telecomProvider.CallFailed += new EventHandler<CallFailureEventArgs>(telecomProvider_CallFailed);
            telecomProvider.FaxToneDetected += new EventHandler<LineEventArgs>(telecomProvider_FaxToneDetected);
            telecomProvider.RemoteOnHold += new EventHandler<LineEventArgs>(telecomProvider_RemoteOnHold);
            telecomProvider.RemoteOffHold += new EventHandler<LineEventArgs>(telecomProvider_RemoteOffHold);
            telecomProvider.SpeechRecognized += new EventHandler<CallInputEventArgs>(telecomProvider_SpeechRecognized);
            telecomProvider.IncomingTransfer += new EventHandler<CallButler.Telecom.TransferEventArgs>(telecomProvider_IncomingTransfer);
            telecomProvider.CallTemporarilyMoved += new EventHandler<CallEventArgs>(telecomProvider_CallTemporarilyMoved);
            telecomProvider.AnswerDetectHuman += new EventHandler<LineEventArgs>(telecomProvider_AnswerDetectHuman);
            telecomProvider.AnswerDetectMachine += new EventHandler<LineEventArgs>(telecomProvider_AnswerDetectMachine);
            telecomProvider.AnswerDetectMachineGreetingFinished += new EventHandler<LineEventArgs>(telecomProvider_AnswerDetectMachineGreetingFinished);

            imlInterp = new WOSI.IVR.IML.IMLInterpreter(WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.ImlPluginsFolder));

            // Attach to our interpreter events
            imlInterp.AsyncExternalAction += new EventHandler<AsyncExternalActionEventArgs>(imlInterp_AsyncExternalAction);
            imlInterp.DeleteLastRecording += new EventHandler(imlInterp_DeleteLastRecording);
            imlInterp.CopyLastRecording += new EventHandler<SoundFileEventArgs>(imlInterp_CopyLastRecording);
            imlInterp.HangUp += new EventHandler(imlInterp_HangUp);
            imlInterp.PlaySound += new EventHandler<PlaySoundEventArgs>(imlInterp_PlaySound);
            imlInterp.ScriptError += new EventHandler<ScriptErrorEventArgs>(imlInterp_ScriptError);
            imlInterp.ScriptFinished += new EventHandler(imlInterp_ScriptFinished);
            imlInterp.SpeakText += new EventHandler<SpeakTextEventArgs>(imlInterp_SpeakText);
            imlInterp.StartRecording += new EventHandler<SoundFileEventArgs>(imlInterp_StartRecording);
            imlInterp.StopRecording += new EventHandler<StopRecordingEventArgs>(imlInterp_StopRecording);
            imlInterp.SyncExternalAction += new EventHandler<SyncExternalActionEventArgs>(imlInterp_SyncExternalAction);
            imlInterp.TransferCall += new EventHandler<WOSI.IVR.IML.TransferEventArgs>(imlInterp_TransferCall);
            imlInterp.Call += new EventHandler<TelephoneNumberEventArgs>(imlInterp_Call);
            imlInterp.StopAllSounds += new EventHandler(imlInterp_StopAllSounds);
            imlInterp.ScriptStarted += new EventHandler(imlInterp_ScriptStarted);
            imlInterp.NewSpeechPhrases += new EventHandler<SpeechPhraseEventArgs>(imlInterp_NewSpeechPhrases);
            imlInterp.TraceMessage += new EventHandler<TraceEventArgs>(imlInterp_TraceMessage);
            imlInterp.JoinConference += new EventHandler<ConferenceEventArgs>(imlInterp_JoinConference);
            imlInterp.LeaveConference += new EventHandler(imlInterp_LeaveConference);

            // Create our default internal provider
            WOSI.CallButler.Data.CallButlerDataset.ProvidersDataTable defaultInternalProviderTable = new WOSI.CallButler.Data.CallButlerDataset.ProvidersDataTable();

            defaultInternalProvider = defaultInternalProviderTable.NewProvidersRow();

            defaultInternalProvider.CustomerID = Properties.Settings.Default.CustomerID;
            defaultInternalProvider.AutoDetectAudio = true;
            defaultInternalProvider.EnableRegistration = false;
            defaultInternalProvider.SupressOutboundUsername = false;

            if (Properties.Settings.Default.InternalSIPDomain != null && Properties.Settings.Default.InternalSIPDomain.Length > 0)
                defaultInternalProvider.Domain = Properties.Settings.Default.InternalSIPDomain;
            else
            {
                // Get the IP of this machine
                try
                {
                    System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

                    defaultInternalProvider.Domain = telecomProvider.LocalIPAddress;
                }
                catch
                {
                    defaultInternalProvider.Domain = Environment.MachineName;
                }
            }

            defaultInternalProvider.IsDefault = false;
        }

        #region Properties
        public int LineNumber
        {
            get
            {
                return lineNumber;
            }
            set
            {
                lineNumber = value;
            }
        }

        public IMLInterpreter IMLInterpreter
        {
            get
            {
                return imlInterp;
            }
        }

        public ScriptProcessorBase ScriptProcessor
        {
            get
            {
                return scriptProcessor;
            }
            set
            {
                if (scriptProcessor != null)
                {
                    StopSounds(lineNumber);
                    imlInterp.StopScript();
                }

                scriptProcessor = null;

                scriptProcessor = value;
            }
        }

        public bool Locked
        {
            get
            {
                return locked;
            }
            set
            {
                locked = value;

                if (locked)
                    telecomProvider.LockLine(lineNumber);
                else
                    telecomProvider.UnlockLine(lineNumber);
            }
        }

        public bool AutoRunScript
        {
            get
            {
                return autoRunScript;
            }
            set
            {
                autoRunScript = value;
            }
        }

        public bool IsAvailable
        {
            get
            {
                if (locked || imlInterp.ScriptIsRunning || telecomProvider.IsLineInUse(lineNumber))
                    return false;
                else
                    return true;
            }
        }

        public DateTime CallStartTime
        {
            get
            {
                return callStartTime;
            }
        }

        public WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow Extension
        {
            get
            {
                return extension;
            }
            set
            {
                extension = value;
            }
        }

        public Guid CurrentCallID
        {
            get
            {
                return callID;
            }
        }

        public TelecomProviderBase TelecomProvider
        {
            get
            {
                return telecomProvider;
            }
        }
        #endregion

        private void StopSounds(int lineNumber)
        {
            telecomProvider.StopSound(lineNumber);
            telecomProvider.StopSpeaking(lineNumber);

            try
            {
                
                //telecomProvider.ClearSpeechRecoPhrases(lineNumber);
            }
            catch
            {
            }
        }

        private WOSI.CallButler.Data.CallButlerDataset.ProvidersRow GetDefaultProvider()
        {
            WOSI.CallButler.Data.CallButlerDataset.ProvidersRow provider = defaultInternalProvider;

            WOSI.CallButler.Data.CallButlerDataset.ProvidersRow[] providers = (WOSI.CallButler.Data.CallButlerDataset.ProvidersRow[])dataProvider.GetProviders(Properties.Settings.Default.CustomerID).Select("IsDefault = true");

            if (providers.Length > 0)
                provider = providers[0];

            return provider;
        }

        /*public WOSI.CallButler.Data.CallButlerDataset.ProvidersRow GetCallProfile(string callingTo)
        {
            WOSI.CallButler.Data.CallButlerDataset.ProvidersRow provider = GetDefaultProvider();

            // Check to see if this is an extension
            try
            {
                WOSI.NET.SIP.SIPURI callURI = new WOSI.NET.SIP.SIPURI(callingTo);

                if (callURI.User != null && callURI.User.Length > 0 && callURI.Host != null && callURI.Host.Length > 0)
                {

                }
            }
            catch
            {
            }

            /*if (nameOrID != null && nameOrID.Length > 0)
            {
                if (nameOrID == InternalProviderProfileName)
                {
                    provider = defaultInternalProvider;
                }
                else
                {
                    WOSI.CallButler.Data.CallButlerDataset.ProvidersRow[] providers = (Data.CallButlerDataset.ProvidersRow[])dataProvider.GetProviders(Properties.Settings.Default.CustomerID).Select(string.Format("Name = '{0}' OR ProviderID = '{0}'", nameOrID));

                    if (providers.Length > 0)
                        provider = providers[0];
                }
            }

            return provider;
        }*/

        public WOSI.CallButler.Data.CallButlerDataset.ProvidersRow FindProvider(string nameOrID)
        {
            WOSI.CallButler.Data.CallButlerDataset.ProvidersRow provider = GetDefaultProvider();

            if (nameOrID != null && nameOrID.Length > 0)
            {
                if (nameOrID == InternalProviderProfileName)
                {
                    provider = defaultInternalProvider;
                }
                else
                {
                    WOSI.CallButler.Data.CallButlerDataset.ProvidersRow[] providers = (WOSI.CallButler.Data.CallButlerDataset.ProvidersRow[])dataProvider.GetProviders(Properties.Settings.Default.CustomerID).Select(string.Format("Name = '{0}' OR ProviderID = '{0}'", nameOrID));

                    if (providers.Length > 0)
                        provider = providers[0];
                }
            }

            return provider;
        }

        public void ProcessOnHold(int conferenceID, bool hold)
        {
            telecomProvider.MuteConferenceLine(conferenceID, lineNumber, hold);
            //UpdateExtensionCall(lineNumber, hold ? CallStatus.OnHold : CallStatus.OnCall, null, null);
        }

        #region IML Interpreter Events
        void imlInterp_NewSpeechPhrases(object sender, SpeechPhraseEventArgs e)
        {
            telecomProvider.ListenForSpeech(lineNumber, e.Phrases.ToArray());
        }

        void imlInterp_LeaveConference(object sender, EventArgs e)
        {
            LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Removing line from conference", false);
            telecomProvider.RemoveLineFromConference(lineNumber);
        }

        void imlInterp_JoinConference(object sender, ConferenceEventArgs e)
        {
            // If the line isn't in use, don't do anything
            if (telecomProvider.IsLineInUse(lineNumber))
            {
                LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Joining conference ID " + e.ConferenceID, false);
                telecomProvider.AddLineToConference(e.ConferenceID, lineNumber);
            }
        }

        void imlInterp_StopAllSounds(object sender, EventArgs e)
        {
            StopSounds(lineNumber);
        }

        void imlInterp_Call(object sender, TelephoneNumberEventArgs e)
        {
            PlaceCall(e);
        }

        public void PlaceCall(TelephoneNumberEventArgs e)
        {
            if (!telecomProvider.IsLineInUse(lineNumber) && e.Number.Length > 0)
            {
                WOSI.CallButler.Data.CallButlerDataset.ProvidersRow profile = FindProvider(e.Profile);

                telecomProvider.Call(lineNumber, e.Number, e.FromCallerID, e.FromCallerNumber, e.Replaces, e.ReferredBy, e.RequestAutoAnswer, profile);

                LoggingService.AddLogEntry(LogLevel.Basic, "(Line " + lineNumber + ") Making an outgoing call to " + WOSI.Utilities.StringUtils.FormatPhoneNumber(e.Number), false);

                imlInterp.SignalEventCallback(e.EventToken);
            }
            else
            {
                imlInterp.SignalCallFailure();
            }
        }

        void imlInterp_TransferCall(object sender, WOSI.IVR.IML.TransferEventArgs e)
        {
            if (TransferCall != null)
            {
                LoggingService.AddLogEntry(LogLevel.Basic, "(Line " + lineNumber + ") Transferring call to " + WOSI.Utilities.StringUtils.FormatPhoneNumber(e.TransferTo), false);
                TransferCall(this, e);
            }
        }

        void imlInterp_SyncExternalAction(object sender, SyncExternalActionEventArgs e)
        {
            imlScriptEventToken = e.EventToken;

            if (scriptProcessor != null)
            {
                scriptProcessor.ProcessExternalCommand(e.Action, e.ParameterData, e.EventToken, this, telecomProvider, dataProvider, pluginManager, pbxRegistrar);
            }
        }

        void imlInterp_StopRecording(object sender, StopRecordingEventArgs e)
        {
            telecomProvider.StopRecording(lineNumber, e.Comments);

            LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Stopped recording", false);

            recording = false;
        }

        void imlInterp_StartRecording(object sender, SoundFileEventArgs e)
        {
            telecomProvider.StartRecording(lineNumber, e.SoundFilename, e.Format);

            LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Started recording to " + e.SoundFilename, false);

            lastRecording = e.SoundFilename;
            recording = true;
        }

        void imlInterp_SpeakText(object sender, SpeakTextEventArgs e)
        {
            LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Speaking '" + e.TextToSpeak + "'", false);

            // If the line isn't in use, don't do anything
            if (!telecomProvider.IsLineInUse(lineNumber))
            {
                imlInterp.SignalEventCallback(e.EventToken);
                return;
            }

            try
            {
                imlScriptEventToken = e.EventToken;
                telecomProvider.SpeakText(lineNumber, e.TextToSpeak);
            }
            catch (Exception ex)
            {
                LoggingService.AddLogEntry(LogLevel.ErrorsOnly, "(Line " + lineNumber + ") Unable to speak text\r\n\r\n" + ex.Message + "\r\n" + ex.StackTrace , true);
                imlInterp.SignalEventCallback(e.EventToken);
            }
        }

        void imlInterp_ScriptStarted(object sender, EventArgs e)
        {
            // Automatically lock our line
            Locked = true;
        }

        void imlInterp_ScriptFinished(object sender, EventArgs e)
        {
            try
            {
                if (telecomProvider.IsLineInUse(lineNumber))
                    telecomProvider.EndCall(lineNumber);
            }
            catch
            {
            }

            // Automatically unlock our line
            Locked = false;

            extension = null;
            callID = Guid.Empty;

            LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Script finished processing", false);

            CheckAvailability();
        }

        void imlInterp_ScriptError(object sender, ScriptErrorEventArgs e)
        {
            LoggingService.AddLogEntry(LogLevel.ErrorsOnly, "(Line " + lineNumber + ") Script error:\r\n\r\n" + e.ErrorString, true);
        }

        void imlInterp_PlaySound(object sender, PlaySoundEventArgs e)
        {
            LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Playing sound at " + e.SoundFilename, false);

            // If the line isn't in use, don't do anything
            if (!telecomProvider.IsLineInUse(lineNumber))
            {
                imlInterp.SignalEventCallback(e.EventToken);
                return;
            }

            imlScriptEventToken = e.EventToken;

            try
            {
                telecomProvider.PlaySound(lineNumber, e.SoundFilename, e.Loop);
            }
            catch (Exception ex)
            {
                LoggingService.AddLogEntry(LogLevel.ErrorsOnly, "(Line " + lineNumber + ") Unable to play sound at " + e.SoundFilename, true);
                imlInterp.SignalEventCallback(e.EventToken);
            }
        }

        void imlInterp_HangUp(object sender, EventArgs e)
        {
            telecomProvider.EndCall(lineNumber);
        }

        void imlInterp_DeleteLastRecording(object sender, EventArgs e)
        {
            try
            {
                if (!recording && lastRecording != null && lastRecording.Length > 0 && File.Exists(lastRecording))
                {
                    File.Delete(lastRecording);

                    lastRecording = null;

                    LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Deleted last recording at " + lastRecording, false);
                }
            }
            catch (Exception ex)
            {
                LoggingService.AddLogEntry(LogLevel.ErrorsOnly, "(Line " + lineNumber + ") Unable to delete recording at " + lastRecording, true);
            }
        }

        void imlInterp_CopyLastRecording(object sender, SoundFileEventArgs e)
        {
            try
            {
                if (!recording && lastRecording != null && lastRecording.Length > 0 && File.Exists(lastRecording))
                {
                    File.Copy(lastRecording, e.SoundFilename);

                    LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Move last recording at " + lastRecording + " to " + e.SoundFilename, false);
                }
            }
            catch (Exception ex)
            {
                LoggingService.AddLogEntry(LogLevel.ErrorsOnly, "(Line " + lineNumber + ") Unable to move last recording at " + lastRecording + " to " + e.SoundFilename, true);
            }
        }

        void imlInterp_AsyncExternalAction(object sender, AsyncExternalActionEventArgs e)
        {
        }

        void imlInterp_TraceMessage(object sender, TraceEventArgs e)
        {
            LoggingService.AddLogEntry(LogLevel.Basic, "(Line " + lineNumber + ") " + e.Message, e.IsError);
        }
        #endregion

        #region Telecom Provider Events
        void telecomProvider_IncomingTransfer(object sender, CallButler.Telecom.TransferEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                if (scriptProcessor != null)
                    scriptProcessor.OnIncomingTransfer(telecomProvider, e);
            }
        }

        void telecomProvider_SpeechRecognized(object sender, CallInputEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Heard the phrase '" + e.InputString + "'", false);
                imlInterp.SignalSpeech(e.InputString);
            }
        }

        void telecomProvider_CallConnected(object sender, CallEventArgs e)
        {
            if (e.LineNumber == lineNumber && scriptProcessor != null)
            {
                LoggingService.AddLogEntry(LogLevel.Basic, "(Line " + e.LineNumber + ") Call Connected", false);

                callStartTime = DateTime.Now;

                // Set our language
                imlInterp.DefaultSpeechVoice = Properties.Settings.Default.DefaultTTSVoice;
                imlInterp.SetLocalVariable("LanguageID", Properties.Settings.Default.DefaultLanguage);

                // Set our volumes
                telecomProvider.SetRecordVolume(e.LineNumber, Properties.Settings.Default.RecordVolume);
                telecomProvider.SetSoundVolume(e.LineNumber, Properties.Settings.Default.SoundVolume);
                telecomProvider.SetSpeechVolume(e.LineNumber, Properties.Settings.Default.SpeechVolume);

                // Set our call info
                imlInterp.CallerDisplayName = e.CallerDisplayName;
                imlInterp.CallerHost = e.CallerMiscInfo;
                imlInterp.CallerUsername = e.CallerPhoneNumber;
                imlInterp.DialedUsername = e.CallingToNumber;
                imlInterp.DialedHost = e.CallingToMiscInfo;

                if (autoRunScript && !imlInterp.ScriptIsRunning)
                {
                    scriptProcessor.StartProcessing(this, telecomProvider, dataProvider);
                }
                
                imlInterp.SignalCallConnected();

                if (scriptProcessor != null)
                    scriptProcessor.OnCallConnected(telecomProvider, e);

                // Check to see if this is an extension making or receiving a call. If so, update the status of the extension.
                if (Properties.Settings.Default.EnableKinesisServer && this.Extension == null)
                {
                    int extNumber = 0;

                    if (e.Outbound)
                    {
                        if (int.TryParse(e.CallingToNumber, out extNumber))
                        {
                            this.Extension = dataProvider.GetExtensionNumber(Properties.Settings.Default.CustomerID, extNumber);
                        }
                    }
                    else
                    {
                        if (int.TryParse(e.CallerPhoneNumber, out extNumber))
                        {
                            this.Extension = dataProvider.GetExtensionNumber(Properties.Settings.Default.CustomerID, extNumber);
                        }
                    }
                }

                if (callID == null)
                    callID = Guid.NewGuid();

                //UpdateExtensionCallStatus(CallStatus.OnCall);
                //UpdateExtensionCall(e.LineNumber, CallStatus.OnCall, e.CallerDisplayName, e.CallerPhoneNumber);
            }
        }

        void telecomProvider_CallTemporarilyMoved(object sender, CallEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                LoggingService.AddLogEntry(LogLevel.Basic, "(Line " + e.LineNumber + ") Call Temporarily Moved to " + e.CallingToNumber, false);

                if (scriptProcessor != null)
                {
                    scriptProcessor.OnCallTemporarilyMoved(telecomProvider, e);
                }
            }
        }

        void telecomProvider_TransferSucceeded(object sender, LineEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Transfer succeeded", false);
                imlInterp.SignalTransferSuccess();
            }
        }

        void telecomProvider_TransferFailed(object sender, LineEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Transfer failed", true);
                imlInterp.SignalTransferFailure();
            }
        }

        void telecomProvider_SoundFinishedPlaying(object sender, LineEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Finished playing sound", false);
                imlInterp.SignalEventCallback(imlScriptEventToken);
            }
        }

        void telecomProvider_FinishedSpeaking(object sender, LineEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Finished speaking", false);
                imlInterp.SignalEventCallback(imlScriptEventToken);
            }
        }

        void telecomProvider_DTMFToneRecognized(object sender, CallInputEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                string typeStr = "OOB";

                if (e.InAudio)
                    typeStr = "In Audio";

                LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Heard a DTMF tone '" + e.InputString + "' (" + typeStr + ")", false);
                imlInterp.SignalDTMFDigit(e.InputString);

                if (scriptProcessor != null)
                    scriptProcessor.OnDTMFDigit(telecomProvider, e);
            }
        }

        void telecomProvider_FaxToneDetected(object sender, LineEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Detected a fax tone", false);

                if (Properties.Settings.Default.AutoForwardFaxTo != null && Properties.Settings.Default.AutoForwardFaxTo.Length > 0)
                {
                    telecomProvider.TransferCall(e.LineNumber, Properties.Settings.Default.AutoForwardFaxTo);
                }
                else
                {
                    imlInterp.SignalFaxTone();
                }
            }
        }

        void telecomProvider_CallEnded(object sender, LineEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                try
                {
                    LoggingService.AddLogEntry(LogLevel.Basic, "(Line " + e.LineNumber + ") Call Ended", false);

                    StopSounds(e.LineNumber);

                    // Add a call history item
                    if (Properties.Settings.Default.EnableCallHistory)
                    {
                        WOSI.CallButler.Data.CallButlerDataset.CallHistoryDataTable callHistoryTable = new WOSI.CallButler.Data.CallButlerDataset.CallHistoryDataTable();
                        WOSI.CallButler.Data.CallButlerDataset.CallHistoryRow callHistoryItem = callHistoryTable.NewCallHistoryRow();

                        callHistoryItem.CallDuration = (TimeSpan)(DateTime.Now - callStartTime);
                        callHistoryItem.CallerDisplayName = imlInterp.CallerDisplayName;
                        callHistoryItem.CallerHost = imlInterp.CallerHost;
                        callHistoryItem.CallerUsername = imlInterp.CallerUsername;
                        callHistoryItem.ToUsername = imlInterp.DialedUsername;
                        callHistoryItem.ToHost = imlInterp.DialedHost;
                        callHistoryItem.CallID = Guid.NewGuid();
                        callHistoryItem.CustomerID = Properties.Settings.Default.CustomerID;
                        callHistoryItem.Timestamp = callStartTime;

                        callHistoryTable.AddCallHistoryRow(callHistoryItem);

                        dataProvider.PersistCallHistory(callHistoryItem);
                    }
                }
                catch(Exception ex)
                {
                    LoggingService.AddLogEntry(LogLevel.ErrorsOnly, "(Line " + lineNumber + ") Unable to add a call history entry: " + ex.ToString(), true);
                }

                //UpdateExtensionCall(e.LineNumber, CallStatus.NotOnCall, null, null);
                //UpdateExtensionCallStatus(CallStatus.NotOnCall);

                try
                {
                    if (imlInterp == null)
                    {
                        LoggingService.AddLogEntry(LogLevel.ErrorsOnly, "**** Iml Interpreter is NULL", true);
                    }

                    imlInterp.SignalHangup();
                }
                catch(Exception ex)
                {
                    LoggingService.AddLogEntry(LogLevel.ErrorsOnly, "(Line " + lineNumber + ") Unable to signal the end of a script: " + ex.ToString(), true);
                }

                if (imlInterp.ImlScript == null)
                {
                    // Automatically unlock our line
                    Locked = false;
                }

                callID = Guid.Empty;
                extension = null;

                CheckAvailability();
            }
        }

        /*public void UpdateExtensionCall(int lineNumber, CallStatus status, string callerName, string callerPhoneNumber)
        {
            if (Properties.Settings.Default.EnableKinesisServer)
            {
                if (this.Extension != null)
                {
                    CallInfo callInfo = new CallInfo(callID, lineNumber, status, callerName, callerPhoneNumber);
                    extStateService.UpdateCallState(this.Extension.ExtensionID, callInfo);

                    if (status == CallStatus.NotOnCall)
                        extStateService.RemoveCallState(this.extension.ExtensionID, lineNumber);
                }
            }
        }

        public void UpdateExtensionCallStatus(CallStatus callStatus)
        {
            if (Properties.Settings.Default.EnableKinesisServer)
            {
                if (this.Extension != null)
                {
                    extStateService.UpdateExtensionState(true, this.Extension.ExtensionID, StateEventType.CallStatusUpdate, new ExtensionStateParameter(StateParameterType.CallStatus, (int)callStatus));
                }
            }
        }*/

        private void CheckAvailability()
        {
            if (!IsAvailable)
            {
                string errorMessage = "(Line " + lineNumber + ") The call is finished but the line still appears to be locked. You may want to check the script to make sure it is exiting gracefully.\r\n";

                errorMessage += "Locked: " + locked.ToString() + "\r\n";
                errorMessage += "Script Is Running: " + imlInterp.ScriptIsRunning.ToString() + "\r\n";
                errorMessage += "Line In Use: " + telecomProvider.IsLineInUse(lineNumber).ToString() + "\r\n";

                if (imlInterp.ImlScript != null)
                {
                    errorMessage += "Script: " + imlInterp.ImlScript.Description;
                }

                LoggingService.AddLogEntry(LogLevel.Basic, errorMessage, false);
            }
        }

        void telecomProvider_CallFailed(object sender, CallFailureEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Call failed\r\n" + e.ReasonCode + " - " + e.Reason, false);

                imlInterp.SignalCallFailure();

                if (scriptProcessor != null)
                    scriptProcessor.OnCallFailed(telecomProvider, e);

                //UpdateExtensionCall(e.LineNumber, CallStatus.NotOnCall, null, null);
                //UpdateExtensionCallStatus(CallStatus.NotOnCall);

                extension = null;
                callID = Guid.Empty;

                CheckAvailability();
            }
        }

        void telecomProvider_RemoteOffHold(object sender, LineEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Call taken off hold from remote caller.", false);
                imlInterp.SignalCallOffHold();
            }
        }

        void telecomProvider_RemoteOnHold(object sender, LineEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + lineNumber + ") Call placed on hold from remote caller.", false);
                imlInterp.SignalCallOnHold();
            }
        }

        void telecomProvider_AnswerDetectMachineGreetingFinished(object sender, LineEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                if (scriptProcessor != null)
                    scriptProcessor.OnAnswerDetectMachineGreetingFinished(telecomProvider, e);
            }
        }

        void telecomProvider_AnswerDetectMachine(object sender, LineEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                if (scriptProcessor != null)
                    scriptProcessor.OnAnswerDetectMachine(telecomProvider, e);
            }
        }

        void telecomProvider_AnswerDetectHuman(object sender, LineEventArgs e)
        {
            if (e.LineNumber == lineNumber)
            {
                if (scriptProcessor != null)
                    scriptProcessor.OnAnswerDetectHuman(telecomProvider, e);
            }
        }
        #endregion
    }
}