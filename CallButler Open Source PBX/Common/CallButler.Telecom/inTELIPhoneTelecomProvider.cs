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
using WOSI.NET.inTELIPhone;
using System.Windows.Forms;

namespace CallButler.Telecom
{
    public class inTELIPhoneTelecomProvider : TelecomProviderBase
    {
        private class DTMFEventData
        {
            public bool InAudio = false;
            public DateTime Time;
            public string DTMFString = "";
        }

        private inTELIPhoneClient ipClient;
        private SIPProfile defaultProfile = null;
        private Dictionary<Guid, SIPProfile> currentProfiles;
        private readonly object profileLock = new object();

        private Dictionary<int, List<DTMFEventData>> dtmfEvents;

        private Dictionary<int, List<int>> conferences;
        private readonly object conferenceLock = new object();

        public inTELIPhoneTelecomProvider(System.Windows.Forms.ToolStripMenuItem contextMenu, System.Windows.Forms.NotifyIcon notifyIcon, params object[] initializationParams)
            : base(contextMenu, notifyIcon, initializationParams)
        {
            if (contextMenu != null)
            {
                // Add a new menu item for SIP tracing
                ToolStripMenuItem tracingMenuItem = new ToolStripMenuItem("Show SIP Diagnostics");
                tracingMenuItem.Click += new EventHandler(tracingMenuItem_Click);
                contextMenu.DropDownItems.Insert(0, tracingMenuItem);
            }

            currentProfiles = new Dictionary<Guid, SIPProfile>();
            conferences = new Dictionary<int, List<int>>();

            // Load our input parameters
            int lineCount = (int)initializationParams[0];
            NATTraversalType natTraversalType = NATTraversalType.PartialSTUN;

            if (!(bool)initializationParams[1])
                natTraversalType = NATTraversalType.None;

            string stunServer = (string)initializationParams[2];
            int sipPort = (int)initializationParams[3];

            // Create our ip client component and register it
            ipClient = new inTELIPhoneClient(sipPort, lineCount, natTraversalType, stunServer);

            //ipClient.ShowTracingUI();
            ipClient.EnableTracing = false;

            try
            {
                ipClient.InitializeSpeech();
            }
            catch(Exception e)
            {
            }

            ipClient.UseLocalAddressForInterNetworkCalls = true;
            ipClient.UseLocalAddressForSipMessages = (bool)initializationParams[4];

            ipClient.SetAudioInputDeviceIDAll(-1);
            ipClient.SetAudioOutputDeviceIDAll(-1);

            // Attach to our ipClient Events
            ipClient.IncomingCall += new IncomingCallEventHandler(ipClient_IncomingCall);
            ipClient.CallConnected += new IncomingCallEventHandler(ipClient_CallConnected);
            ipClient.CallEnded += new CallStateEventHandler(ipClient_CallEnded);
            ipClient.DTMFToneRecognized += new DTMFToneEventHandler(ipClient_DTMFToneRecognized);
            ipClient.FaxToneDetected += new CallStateEventHandler(ipClient_FaxToneDetected);
            ipClient.FinishedSpeaking += new CallStateEventHandler(ipClient_FinishedSpeaking);
            ipClient.SoundFinishedPlaying += new CallStateEventHandler(ipClient_SoundFinishedPlaying);
            ipClient.SpeechRecognized += new SpeechRecognitionEventHandler(ipClient_SpeechRecognized);
            ipClient.TransferFailed += new CallStateEventHandler(ipClient_TransferFailed);
            ipClient.TransferSucceeded += new CallStateEventHandler(ipClient_TransferSucceeded);
            ipClient.CallFailed += new CallFailedEventHandler(ipClient_CallFailed);
            ipClient.ProfileRegistrationError += new SIPProfileEventHandler(ipClient_ProfileRegistrationError);
            ipClient.CallOnHold += new CallStateEventHandler(ipClient_CallOnHold);
            ipClient.CallOffHold += new CallStateEventHandler(ipClient_CallOffHold);
            ipClient.SpeechRecognized += new SpeechRecognitionEventHandler(ipClient_SpeechRecognized);
            ipClient.IncomingBusyCall += new IncomingBusyCallEventHandler(ipClient_IncomingBusyCall);
            ipClient.IncomingTransfer += new EventHandler<IncomingTransferEventArgs>(ipClient_IncomingTransfer);
            ipClient.CallTemporarilyMoved += new IncomingCallEventHandler(ipClient_CallTemporarilyMoved);
            ipClient.AnsweringMachineDetection += new EventHandler<AnsweringMachineDetectionEventArgs>(ipClient_AnsweringMachineDetection);
            ipClient.AnsweringMachineGreetingFinished += new EventHandler<CallStateEventArgs>(ipClient_AnsweringMachineGreetingFinished);
            ipClient.EndCallOnRTPTimeout = false;

            dtmfEvents = new Dictionary<int, List<DTMFEventData>>();

            for (int index = 1; index <= ipClient.LineCount; index++)
            {
                // Enable DTMF recognition for each line
                ipClient.StartInBandDTMFRecognition(index);

                // Create a DTMF event holder to cut down on echos
                dtmfEvents[index] = new List<DTMFEventData>(10);
            }
        }

        void tracingMenuItem_Click(object sender, EventArgs e)
        {
            ipClient.EnableTracing = true;
            ipClient.ShowTracingUI();
        }

        #region Startup / Shutdown Functions
        protected override void OnStartup()
        {
            
        }

        protected override void OnShutdown()
        {
            ipClient.Dispose();
            ipClient = null;
        }
        #endregion

        #region Properties
        public override int LineCount
        {
            get
            {
                return ipClient.LineCount;
            }
        }

        public new WOSI.NET.inTELIPhone.inTELIPhoneClient BaseProviderObject
        {
            get
            {
                return ipClient;
            }
        }

        public override string LocalIPAddress
        {
            get
            {
                return ipClient.LocalIPAddress;
            }
        }
        #endregion

        #region Call Control Functions
        public override void Call(int lineNumber, string number, string fromCallerID, string fromCallerNumber, string replacesID, string referredBy, bool requestAutoAnswer, WOSI.CallButler.Data.CallButlerDataset.ProvidersRow provider)
        {
            string callerID = null;
            string callerNumber = null;

            SIPProfile callProfile = defaultProfile;

            if (provider != null)
            {
                callProfile = ConvertProfile(provider);
            }

            if (fromCallerID != null && fromCallerID.Length > 0)
                callerID = fromCallerID;

            if (provider != null && !provider.SupressOutboundUsername && fromCallerNumber != null && fromCallerNumber.Length > 0)
            {
                callerNumber = fromCallerNumber;

                if (provider.OutboundUsernamePrefix != null && provider.OutboundUsernamePrefix.Length > 0 && !callerNumber.StartsWith(provider.OutboundUsernamePrefix))
                    callerNumber = provider.OutboundUsernamePrefix + callerNumber;
            }

            // Make the call
            if (callProfile != null)
            {
                try
                {
                    ipClient.Call(lineNumber, callerID, callerNumber, callProfile, number, replacesID, referredBy, requestAutoAnswer);

                    return;
                }
                catch(Exception e)
                {
                }
            }
            
            RaiseCallFailed(new CallFailureEventArgs(lineNumber, 0, "No profile defined"));
        }

        public override void Call(int lineNumber, string number, string fromCallerID, string fromCallerNumber, bool requestAutoAnswer, WOSI.CallButler.Data.CallButlerDataset.ProvidersRow provider)
        {
            Call(lineNumber, number, fromCallerID, fromCallerNumber, null, null, requestAutoAnswer, provider);
        }

        private SIPProfile ConvertProfile(WOSI.CallButler.Data.CallButlerDataset.ProvidersRow provider)
        {
            SIPProfile callProfile = new SIPProfile();

            callProfile = new SIPProfile();
            callProfile.AuthPassword = WOSI.Utilities.CryptoUtils.Decrypt(provider.AuthPassword, WOSI.CallButler.Data.Constants.EncryptionPassword);
            callProfile.AuthUsername = provider.AuthUsername;
            callProfile.DisplayName = provider.DisplayName;
            callProfile.DomainRealm = provider.Domain;
            callProfile.SIPProxyServer = provider.SIPProxy;
            callProfile.SIPRegistrarServer = provider.SIPRegistrar;
            callProfile.Username = provider.Username;
            callProfile.AutoDetectAudio = provider.AutoDetectAudio;

            return callProfile;
        }

        public override void RedirectCall(int lineNumber, string redirectLocation)
        {
            try
            {
                WOSI.NET.SIP.SIPURI redirURI = new WOSI.NET.SIP.SIPURI(redirectLocation);

                if (redirURI.Host == null || redirURI.Host.Length == 0)
                {
                    redirURI.Host = redirectLocation;
                    //redirURI.ParseURI(string.Format("sip:unknown@{0}", redirectLocation));
                }

                ipClient.RedirectCall(lineNumber, redirURI);
            }
            catch
            {
            }
        }

        public override void RedirectBusyCall(string callID, string redirectLocation)
        {
            try
            {
                WOSI.NET.SIP.SIPURI redirURI = new WOSI.NET.SIP.SIPURI(redirectLocation);

                if (redirURI.Host == null || redirURI.Host.Length == 0)
                {
                    redirURI.Host = redirectLocation;
                    //redirURI.ParseURI(string.Format("sip:unknown@{0}", redirectLocation));
                }

                ipClient.RedirectBusyCall(callID, redirURI);
            }
            catch
            {
            }
        }

        public override void DeclineBusyCall(string callID)
        {
            try
            {
                ipClient.DeclineBusyCall(callID);
            }
            catch
            {
            }
        }

        public override void DeclineCall(int lineNumber)
        {
            if (ipClient.LineInUse(lineNumber))
            {
                ipClient.DeclineCall(lineNumber);
            }
        }

        public override void CallBlast(int lineNumber, string[] numbers, string fromCallerID, string fromCallerNumber, WOSI.CallButler.Data.CallButlerDataset.ProvidersRow[] providers)
        {
            string callerID = null;
            string callerNumber = null;

            if (fromCallerID != null && fromCallerID.Length > 0)
                callerID = fromCallerID;

            if (!providers[0].SupressOutboundUsername && fromCallerNumber != null && fromCallerNumber.Length > 0)
                callerNumber = fromCallerNumber;

            SIPProfile[] sipProfiles = new SIPProfile[numbers.Length];

            for(int index = 0; index < numbers.Length; index++)
            {
                if (providers == null || index >= providers.Length)
                    sipProfiles[index] = defaultProfile;
                else
                    sipProfiles[index] = ConvertProfile(providers[index]);
            }

            ipClient.CallBlast(lineNumber, callerID, callerNumber, sipProfiles, numbers);
        }

        public override void AnswerCall(int lineNumber, bool autoDetectAudio)
        {
            if (ipClient.LineInUse(lineNumber))
            {
                ipClient.AnswerCall(lineNumber, autoDetectAudio);
            }
        }

        public override void EndCall(int lineNumber)
        {
            if (ipClient.LineInUse(lineNumber))
            {
                ipClient.EndCall(lineNumber);
            }
        }

        public override void TransferCall(int lineNumber, string transferToNumber)
        {
            ipClient.TransferCall(lineNumber, transferToNumber);
        }

        public override void TransferCallAttended(int lineNumber1, int lineNumber2, bool useBridge)
        {
            if(useBridge)
                ipClient.BridgeLines(lineNumber1, lineNumber2);
            else
                ipClient.AttendedTransferCall(lineNumber1, lineNumber2);
        }

        public override int ConferenceLines(params int[] lines)
        {
            // Create a new conference ID
            Random rand = new Random();

            int conferenceID = 0;

            do
            {
                conferenceID = -rand.Next(int.MaxValue);
            }
            while (conferences.ContainsKey(conferenceID));

            List<int> lineList = new List<int>(lines);

            lock (conferenceLock)
            {
                conferences.Add(conferenceID, lineList);
            }

            foreach (int lineNumber in lines)
            {
                ipClient.AddLineToConference(conferenceID, lineNumber);
            }

            return conferenceID;
        }

        public override void EndConference(int conferenceID, bool hangUp)
        {
            if (conferences.ContainsKey(conferenceID))
            {
                foreach (int lineNumber in conferences[conferenceID])
                {
                    ipClient.RemoveLineFromConference(lineNumber);

                    if (hangUp && ipClient.LineInUse(lineNumber))
                    {
                        EndCall(lineNumber);
                    }
                }

                lock (conferenceLock)
                {
                    conferences.Remove(conferenceID);
                }
            }
        }

        public override void MuteConferenceLine(int conferenceID, int lineNumber, bool mute)
        {
            if (conferences.ContainsKey(conferenceID))
            {
                foreach (int ln in conferences[conferenceID])
                {
                    if (ln == lineNumber)
                    {
                        if (mute)
                        {
                            ipClient.RemoveLineFromConference(lineNumber);
                        }
                        else
                        {
                            ipClient.AddLineToConference(conferenceID, lineNumber);
                        }

                        return;
                    }
                }
            }
        }

        public override void AddLineToConference(int conferenceID, int lineNumber)
        {
            if (conferences.ContainsKey(conferenceID))
            {
                if (!conferences[conferenceID].Contains(lineNumber))
                {
                    lock (conferenceLock)
                    {
                        conferences[conferenceID].Add(lineNumber);
                    }
                }
            }

            ipClient.AddLineToConference(conferenceID, lineNumber);
        }

        public override void RemoveLineFromConference(int lineNumber)
        {
            lock (conferenceLock)
            {
                Dictionary<int, List<int>>.Enumerator confEnum = conferences.GetEnumerator();

                List<int> removeConfs = new List<int>();

                while (confEnum.MoveNext())
                {
                    if (confEnum.Current.Value.Contains(lineNumber))
                    {
                        confEnum.Current.Value.Remove(lineNumber);
                    }

                    if (confEnum.Current.Value.Count == 0)
                        removeConfs.Add(confEnum.Current.Key);
                }

                foreach (int confID in removeConfs)
                    conferences.Remove(confID);
            }

            ipClient.RemoveLineFromConference(lineNumber);
        }

        public override bool IsLineInUse(int lineNumber)
        {
            return ipClient.LineInUse(lineNumber);
        }

        public override void LockLine(int lineNumber)
        {
            ipClient.LockLine(lineNumber);
        }

        public override void UnlockLine(int lineNumber)
        {
            ipClient.UnlockLine(lineNumber);
        }
        #endregion

        #region Codec Functions
        public override AudioCodecInformation[] GetAudioCodecs()
        {
            List<AudioCodecInformation> audioCodecs = new List<AudioCodecInformation>();

            for (int index = 0; index < ipClient.AudioCodecs.Count; index++)
            {
                AudioCodecInformation acInfo = new AudioCodecInformation();

                acInfo.Name = ipClient.AudioCodecs[index].ToString();
                acInfo.Enabled = ipClient.AudioCodecs.GetEnabled(index);
                acInfo.Tag = ipClient.AudioCodecs[index];

                audioCodecs.Add(acInfo);
            }

            return audioCodecs.ToArray();
        }

        public override void SetAudioCodecs(AudioCodecInformation[] codecs)
        {
            ipClient.AudioCodecs.DisableAllCodecs();

            foreach (AudioCodecInformation acInfo in codecs)
            {
                AudioCodecType codecType = WOSI.Utilities.EnumUtils<AudioCodecType>.Parse(acInfo.Name);

                ipClient.AudioCodecs.SetToLowestPriority(codecType);
                ipClient.AudioCodecs.SetEnabled(codecType, acInfo.Enabled);
            }
        }
        #endregion

        #region Sound Functions
        public override void PlaySound(int lineNumber, byte[] soundBytes)
        {
            ipClient.PlaySoundBytes(lineNumber, soundBytes);
        }

        public override void PlaySound(int lineNumber, string filename, bool loop)
        {
            ipClient.PlaySound(lineNumber, filename, loop);
        }

        public override void StopSound(int lineNumber)
        {
            ipClient.StopSound(lineNumber);
        }

        public override void StartRecording(int lineNumber, string filename, string format)
        {
            if (string.Compare(format, "WAV", true) == 0)
            {
                ipClient.StartRecordingWAV(lineNumber, filename);
            }
            else
            {
                ipClient.StartRecordingMP3(lineNumber, filename);
            }
        }

        public override void StopRecording(int lineNumber, string comments)
        {
            ipClient.StopRecording(lineNumber, comments, "", "", 0, "", DateTime.Now.Year);
        }

        public override void SetRecordVolume(int lineNumber, int volume)
        {
            ipClient.SetRecordVolume(lineNumber, (short)volume);
        }

        public override void SetSoundVolume(int lineNumber, int volume)
        {
            ipClient.SetSoundVolume(lineNumber, (short)volume);
        }

        public override void SetSpeechVolume(int lineNumber, int volume)
        {
            ipClient.SetSpeechVolume(lineNumber, (short)volume);
        }
        #endregion

        #region Speech Functions
        public override void SpeakText(int lineNumber, string textToSpeak)
        {
            ipClient.SpeakText(lineNumber, textToSpeak);
        }

        public override void StopSpeaking(int lineNumber)
        {
            ipClient.StopSpeakingText(lineNumber);
        }

        public override void ListenForSpeech(int lineNumber, string[] phrases)
        {
            foreach (string speechPhrase in phrases)
                ipClient.AddSpeechRecognitionPhrase(lineNumber, speechPhrase);
        }

        public override void StopListeningForSpeech(int lineNumber)
        {
            ipClient.ClearSpeechRecognitionPhrases(lineNumber);
        }

        public override void ClearSpeechRecoPhrases(int lineNumber)
        {
            ipClient.ClearSpeechRecognitionPhrases(lineNumber);
        }
        #endregion

        #region Misc Functions
        public override void SendDTMF(int lineNumber, string dtmfString, bool inAudio)
        {
            if (inAudio)
            {
                ipClient.SendInBandDTMF(lineNumber, dtmfString, 150, 100);
            }
            else
            {
                ipClient.SendRTPOutofBandDTMF(lineNumber, dtmfString);
            }
        }

        public override void EnableAnsweringMachineDetection(int lineNumber, bool enable)
        {
            if (enable)
                ipClient.StartAnsweringMachineDetection(lineNumber);
            else
                ipClient.StopAnsweringMachineDetection(lineNumber);
        }

        public override void SetAnsweringMachineDetectionSettings(int lineNumber, string settingsData)
        {
            ipClient.SetAnsweringMachineDetectionParameters(lineNumber, settingsData);
        }
        #endregion

        #region IpClient Events
        void ipClient_CallTemporarilyMoved(object sender, IncomingCallEventArgs e)
        {
            RaiseCallTemporarilyMoved(new CallEventArgs(e.LineNumber, e.LocalUserName, e.LocalHost, e.RemoteDisplayName, e.RemoteUserName, e.RemoteIP, e.RemotePort, e.RemoteHost, e.Outbound, e.SIPProfile));
        }

        void ipClient_IncomingBusyCall(object sender, IncomingBusyCallEventArgs e)
        {
            RaiseIncomingBusyCall(new BusyCallEventArgs(e.CallID, e.LocalUserName, e.LocalHost, e.RemoteDisplayName, e.RemoteUserName, e.RemoteIP, e.RemotePort, e.RemoteHost, e.Outbound, e.SIPProfile));
        }

        void ipClient_CallOffHold(object sender, CallStateEventArgs e)
        {
            RaiseRemoteOffHold(new LineEventArgs(e.LineNumber));
        }

        void ipClient_CallOnHold(object sender, CallStateEventArgs e)
        {
            RaiseRemoteOnHold(new LineEventArgs(e.LineNumber));
        }

        void ipClient_CallEnded(object sender, CallStateEventArgs e)
        {
            // Clear our DTMF events
            dtmfEvents[e.LineNumber].Clear();

            RaiseCallEnded(new LineEventArgs(e.LineNumber));
        }

        void ipClient_CallFailed(object sender, CallFailedEventArgs e)
        {
            // Clear our DTMF events
            dtmfEvents[e.LineNumber].Clear();

            RaiseCallFailed(new CallFailureEventArgs(e.LineNumber, e.ReasonCode, e.ReasonDescription));
        }

        void ipClient_CallConnected(object sender, IncomingCallEventArgs e)
        {
            // Clear our DTMF events
            dtmfEvents[e.LineNumber].Clear();

            if(e.Outbound)
                RaiseCallConnected(new CallEventArgs(e.LineNumber, e.RemoteUserName, e.RemoteHost, e.LocalDisplayName, e.LocalUserName, e.RemoteIP, e.RemotePort, e.LocalHost, e.Outbound, e.SIPProfile));
            else
                RaiseCallConnected(new CallEventArgs(e.LineNumber, e.LocalUserName, e.LocalHost, e.RemoteDisplayName, e.RemoteUserName, e.RemoteIP, e.RemotePort, e.RemoteHost, e.Outbound, e.SIPProfile));
        }

        void ipClient_IncomingTransfer(object sender, IncomingTransferEventArgs e)
        {
            ipClient.AcceptIncomingTransfer(e.LineNumber, false);
            RaiseIncomingTransfer(new TransferEventArgs(e.LineNumber, e.RemoteUserName, e.RemoteHost, e.RemoteDisplayName, e.LocalUserName, e.RemoteIP, e.RemotePort, e.LocalHost, e.ReplacementLine, e.ReplacesID, e.Outbound, e.SIPProfile));
        }

        void ipClient_IncomingCall(object sender, IncomingCallEventArgs e)
        {
            RaiseIncomingCall(new CallEventArgs(e.LineNumber, e.LocalUserName, e.LocalHost, e.RemoteDisplayName, e.RemoteUserName, e.RemoteIP, e.RemotePort, e.RemoteHost, e.Outbound, e.SIPProfile));
        }

        void ipClient_TransferSucceeded(object sender, CallStateEventArgs e)
        {
            RaiseTransferSucceeded(new LineEventArgs(e.LineNumber));
        }

        void ipClient_TransferFailed(object sender, CallStateEventArgs e)
        {
            RaiseTransferFailed(new LineEventArgs(e.LineNumber));
        }

        void ipClient_SpeechRecognized(object sender, SpeechRecognitionEventArgs e)
        {
            RaiseSpeechRecognized(new CallInputEventArgs(e.LineNumber, e.Phrase));
        }

        void ipClient_SoundFinishedPlaying(object sender, CallStateEventArgs e)
        {
            RaiseSoundFinishedPlaying(new LineEventArgs(e.LineNumber));
        }

        void ipClient_FinishedSpeaking(object sender, CallStateEventArgs e)
        {
            RaiseFinishedSpeaking(new LineEventArgs(e.LineNumber));
        }

        void ipClient_DTMFToneRecognized(object sender, DTMFToneEventArgs e)
        {
            bool inAudio = e.DTMFType == DTMFType.InAudio ? true : false;

            // Check to make sure this DTMF event isn't some sort of an echo.
            if (IsDTMFEcho(e))
                return;

            AddDTMFEvent(e);
            
            RaiseDTMFToneRecognized(new CallInputEventArgs(e.LineNumber, e.DTMFString, inAudio));
        }

        private bool IsDTMFEcho(DTMFToneEventArgs e)
        {
            try
            {
                bool inAudio = e.DTMFType == DTMFType.InAudio ? true : false;

                foreach (DTMFEventData dtmfData in dtmfEvents[e.LineNumber])
                {
                    // Are the digits the same?
                    if (dtmfData.DTMFString == e.DTMFString)
                    {
                        // Is it a different type of dtmf event?
                        if (dtmfData.InAudio != inAudio)
                        {
                            // Is the timestamp difference within 3 seconds?
                            if (((TimeSpan)(DateTime.Now - dtmfData.Time)).TotalSeconds <= 3)
                            {
                                // If we get here, this is probably an echo
                                return true;
                            }
                        }
                        else
                        {
                            // Is the timestamp differnce within 50 ms?
                            if (((TimeSpan)(DateTime.Now - dtmfData.Time)).TotalMilliseconds <= 50)
                            {
                                // If we get here, this is probably an echo
                                return true;
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            return false;
        }

        private void AddDTMFEvent(DTMFToneEventArgs e)
        {
            try
            {
                // Update our DTMF event data for this line
                DTMFEventData newDTMFData = new DTMFEventData();
                newDTMFData.DTMFString = e.DTMFString;
                newDTMFData.InAudio = e.DTMFType == DTMFType.InAudio ? true : false;
                newDTMFData.Time = DateTime.Now;

                dtmfEvents[e.LineNumber].Add(newDTMFData);

                // Only hold a maximum of the last 10 digits
                if (dtmfEvents[e.LineNumber].Count > 10)
                    dtmfEvents[e.LineNumber].RemoveAt(0);
            }
            catch
            {
            }
        }

        void ipClient_FaxToneDetected(object sender, CallStateEventArgs e)
        {
            RaiseFaxToneDetected(new LineEventArgs(e.LineNumber));
        }

        void ipClient_ProfileRegistrationError(object sender, SIPProfileEventArgs e)
        {
            string erDetail = string.Format("Provider: {0}\r\nError Code:{1}\r\n\r\n{2}", e.SIPProfile.DomainRealm, e.ResponseCode, e.ResponsePhrase);

            RaiseError(new ErrorEventArgs("Provider Registration Error", erDetail));
        }

        void ipClient_AnsweringMachineGreetingFinished(object sender, CallStateEventArgs e)
        {
            RaiseAnswerDetectMachineGreetingFinished(new LineEventArgs(e.LineNumber));
        }

        void ipClient_AnsweringMachineDetection(object sender, AnsweringMachineDetectionEventArgs e)
        {
            switch (e.Result)
            {
                case AnsweringMachineDetectionResult.AnsweringMachine:
                    RaiseAnswerDetectMachine(new LineEventArgs(e.LineNumber));
                    break;

                case AnsweringMachineDetectionResult.Human:
                    RaiseAnswerDetectHuman(new LineEventArgs(e.LineNumber));
                    break;
            }
        }
        #endregion

        #region Network Registration Functions
        public override void Register(Guid registrationID, object registrationParams)
        {
            // If this profile exists already, unregsiter it
            if (currentProfiles.ContainsKey(registrationID) && currentProfiles[registrationID].ProfileState == SIPProfileState.Registered)
                Unregister(registrationID);

            WOSI.CallButler.Data.CallButlerDataset.ProvidersRow provider = registrationParams as WOSI.CallButler.Data.CallButlerDataset.ProvidersRow;

            if (provider != null && provider.IsEnabled)
            {
                // Create a new SIP Profile
                SIPProfile profile = new SIPProfile();

                profile.DisplayName = provider.DisplayName;
                profile.Username = provider.Username;
                profile.AuthUsername = provider.AuthUsername;

                if (provider.AuthPassword.Length > 0)
                    profile.AuthPassword = WOSI.Utilities.CryptoUtils.Decrypt(provider.AuthPassword, WOSI.CallButler.Data.Constants.EncryptionPassword);

                profile.DomainRealm = provider.Domain;
                profile.SIPProxyServer = provider.SIPProxy;
                profile.SIPRegistrarServer = provider.SIPRegistrar;

                if (provider.EnableRegistration)
                {
                    ipClient.RegisterProfile(profile, provider.Expires);
                }

                lock (profileLock)
                {
                    currentProfiles.Add(registrationID, profile);
                }

                if(provider.IsDefault)
                    defaultProfile = profile;
            }
        }

        public override void Unregister(Guid registrationID)
        {
            if (currentProfiles.ContainsKey(registrationID))
            {
                SIPProfile profile = currentProfiles[registrationID];

                if (profile == defaultProfile)
                    defaultProfile = null;

                ipClient.UnregisterProfile(profile);

                lock (profileLock)
                {
                    currentProfiles.Remove(registrationID);
                }
            }
        }

        public override string GetRegistrationState(Guid registrationID)
        {
            if (currentProfiles.ContainsKey(registrationID))
            {
                SIPProfile profile = currentProfiles[registrationID];

                switch (profile.ProfileState)
                {
                    case SIPProfileState.Registered:
                        return "Registered";
                    case SIPProfileState.RegistrationError:
                        {
                            return string.Format("Registration Error - {0}", profile.ProfileStateDescription);
                        }
                    case SIPProfileState.Unregistered:
                        return "Unregistered";
                }
            }

            return "Disabled";
        }
        #endregion
    }
}
