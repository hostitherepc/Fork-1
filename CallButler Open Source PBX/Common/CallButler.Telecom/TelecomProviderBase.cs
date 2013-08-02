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
using WOSI.CallButler.Data;

namespace CallButler.Telecom
{
    public struct AudioCodecInformation
    {
        public string Name;
        public bool Enabled;
        public object Tag;
    }

    public class LineEventArgs : EventArgs
    {
        public int LineNumber = 0;
        public object Tag;

        public LineEventArgs(int lineNumber)
        {
            this.LineNumber = lineNumber;
        }

        public LineEventArgs(int lineNumber, object tag)
        {
            this.LineNumber = lineNumber;
            this.Tag = tag;
        }
    }

    public class CallFailureEventArgs : LineEventArgs
    {
        public int ReasonCode = 0;
        public string Reason = "";

        public CallFailureEventArgs(int lineNumber, int reasonCode, string reason)
            : base(lineNumber)
        {
            this.ReasonCode = reasonCode;
            this.Reason = reason;
        }
    }

    public class CallEventArgs : LineEventArgs
    {
        public string CallerDisplayName = "";
        public string CallerPhoneNumber = "";
        public string CallerIPAddress = "";
        public int CallerPort = 0;
        public string CallerMiscInfo = "";
        public string CallingToNumber = "";
        public string CallingToMiscInfo = "";
        public object Tag = null;
        public bool Outbound = false;

        public CallEventArgs(int lineNumber, string callingToNumber, string callingToMiscInfo, string callerDisplayName, string callerPhoneNumber, string callerIPAddress, int callerPort, string callerMiscInfo, bool outbound, object tag)
            : base(lineNumber)
        {
            CallerDisplayName = callerDisplayName;
            CallerPhoneNumber = callerPhoneNumber;
            CallerIPAddress = callerIPAddress;
            CallerPort = callerPort;
            CallerMiscInfo = callerMiscInfo;
            CallingToNumber = callingToNumber;
            CallingToMiscInfo = callingToMiscInfo;
            Tag = tag;
            Outbound = outbound;
        }
    }

    public class TransferEventArgs : CallEventArgs
    {
        public string ReplacesID = null;
        public int ReplacementLineNumber = 0;

        public TransferEventArgs(int lineNumber, string callingToNumber, string callingToMiscInfo, string callerDisplayName, string callerPhoneNumber, string callerIPAddress, int callerPort, string callerMiscInfo, int replacementLineNumber, string replacesID, bool outbound, object tag)
            : base(lineNumber, callingToNumber, callingToMiscInfo, callerDisplayName, callerPhoneNumber, callerIPAddress, callerPort, callerMiscInfo, outbound, tag)
        {
            this.ReplacesID = replacesID;
            this.ReplacementLineNumber = replacementLineNumber;
        }
    }

    public class BusyCallEventArgs : CallEventArgs
    {
        public string CallID = "";

        public BusyCallEventArgs(string callID, string callingToNumber, string callingToMiscInfo, string callerDisplayName, string callerPhoneNumber, string callerIPAddress, int callerPort, string callerMiscInfo, bool outbound, object tag)
            : base(0, callingToNumber, callingToMiscInfo, callerDisplayName, callerPhoneNumber, callerIPAddress, callerPort, callerMiscInfo, outbound, tag)
        {
            CallID = callID;
        }
    }

    public class CallInputEventArgs : LineEventArgs
    {
        public string InputString = "";
        public bool InAudio = false;

        public CallInputEventArgs(int lineNumber, string inputString)
            : base(lineNumber)
        {
            InputString = inputString;
        }

        public CallInputEventArgs(int lineNumber, string inputString, bool inAudio)
            : base(lineNumber)
        {
            InputString = inputString;
            InAudio = inAudio;
        }
    }

    public class ErrorEventArgs : EventArgs
    {
        public string ErrorMessage = "";
        public string ErrorDetail = "";

        public ErrorEventArgs(string errorMessage, string errorDetail)
        {
            this.ErrorMessage = errorMessage;
            this.ErrorDetail = errorDetail;
        }
    }

    public abstract class TelecomProviderBase
    {
        public event EventHandler<CallEventArgs> IncomingCall;
        public event EventHandler<BusyCallEventArgs> IncomingBusyCall;
        public event EventHandler<CallEventArgs> CallConnected;
        public event EventHandler<LineEventArgs> CallEnded;
        public event EventHandler<CallFailureEventArgs> CallFailed;
        public event EventHandler<CallInputEventArgs> DTMFToneRecognized;
        public event EventHandler<LineEventArgs> FaxToneDetected;
        public event EventHandler<LineEventArgs> FinishedSpeaking;
        public event EventHandler<LineEventArgs> SoundFinishedPlaying;
        public event EventHandler<CallInputEventArgs> SpeechRecognized;
        public event EventHandler<LineEventArgs> TransferFailed;
        public event EventHandler<LineEventArgs> TransferSucceeded;
        public event EventHandler<LineEventArgs> RemoteOnHold;
        public event EventHandler<LineEventArgs> RemoteOffHold;
        public event EventHandler<ErrorEventArgs> Error;
        public event EventHandler<TransferEventArgs> IncomingTransfer;
        public event EventHandler<CallEventArgs> CallTemporarilyMoved;
        public event EventHandler<LineEventArgs> AnswerDetectHuman;
        public event EventHandler<LineEventArgs> AnswerDetectMachine;
        public event EventHandler<LineEventArgs> AnswerDetectMachineGreetingFinished;

        protected System.Windows.Forms.ToolStripMenuItem contextMenu;
        protected System.Windows.Forms.NotifyIcon notifyIcon;

        public TelecomProviderBase(System.Windows.Forms.ToolStripMenuItem contextMenu, System.Windows.Forms.NotifyIcon notifyIcon, params object[] initializationParams)
        {
            this.contextMenu = contextMenu;
            this.notifyIcon = notifyIcon;
        }

        #region Startup/Shutdown Methods
        public void Startup()
        {
            OnStartup();
        }

        public void Shutdown()
        {
            OnShutdown();
        }

        protected virtual void OnStartup()
        {
        }

        protected virtual void OnShutdown()
        {
        }
        #endregion

        #region Properties
        public virtual int LineCount
        {
            get
            {
                return 0;
            }
        }

        public virtual object BaseProviderObject
        {
            get
            {
                return null;
            }
        }

        public virtual int AudioInputRate
        {
            get
            {
                return 8000;
            }
        }

        public virtual string LocalIPAddress
        {
            get
            {
                return WOSI.Utilities.NetworkUtils.GetHostIPAddress(System.Net.Dns.GetHostName(), System.Net.Sockets.AddressFamily.InterNetwork).ToString();
            }
        }
        #endregion

        #region Call Control Functions
        /*public virtual void Call(int lineNumber, string number, string fromCallerID, object profile)
        {
        }*/

        public virtual void Call(int lineNumber, string number, string fromCallerID, string fromCallerNumber, bool requestAutoAnswer, WOSI.CallButler.Data.CallButlerDataset.ProvidersRow provider)
        {
        }

        public virtual void Call(int lineNumber, string number, string fromCallerID, string fromCallerNumber, string replacesID, string referredBy, bool requestAutoAnswer, WOSI.CallButler.Data.CallButlerDataset.ProvidersRow provider)
        {
        }

        public virtual void CallBlast(int lineNumber, string[] numbers, string fromCallerID, string fromCallerNumber, WOSI.CallButler.Data.CallButlerDataset.ProvidersRow[] providers)
        {
        }

        public virtual void AnswerCall(int lineNumber, bool autoDetectAudio)
        {
        }

        public virtual void DeclineCall(int lineNumber)
        {
        }

        public virtual void RedirectCall(int lineNumber, string redirectLocation)
        {
        }

        public virtual void RedirectBusyCall(string callID, string redirectLocation)
        {
        }

        public virtual void DeclineBusyCall(string callID)
        {
        }

        public virtual void EndCall(int lineNumber)
        {
        }

        public virtual void TransferCall(int lineNumber, string transferToNumber)
        {
        }

        public virtual void TransferCallAttended(int lineNumber1, int lineNumber2, bool useBridge)
        {
        }

        public virtual bool IsLineInUse(int lineNumber)
        {
            return false;
        }

        public virtual void CallHold(int lineNumber, bool putOnHold, bool keepAudioAlive)
        {
        }

        public virtual int ConferenceLines(params int[] lines)
        {
            return -1;
        }

        public virtual void MuteConferenceLine(int conferenceID, int lineNumber, bool mute)
        {
        }

        public virtual void EndConference(int conferenceID, bool hangUp)
        {
        }

        public virtual void AddLineToConference(int conferenceID, int lineNumber)
        {
        }

        public virtual void RemoveLineFromConference(int lineNumber)
        {
        }

        public virtual void LockLine(int lineNumber)
        {
        }

        public virtual void UnlockLine(int lineNumber)
        {
        }
        #endregion

        #region Sound Functions
        public virtual void PlaySound(int lineNumber, byte[] soundBytes)
        {
        }

        public virtual void PlaySound(int lineNumber, string filename, bool loop)
        {
        }

        public virtual void StopSound(int lineNumber)
        {
        }

        public virtual void StartRecording(int lineNumber, string filename, string format)
        {
        }

        public virtual void StopRecording(int lineNumber, string comments)
        {
        }

        public virtual void SetSoundVolume(int lineNumber, int volume)
        {
        }

        public virtual void SetSpeechVolume(int lineNumber, int volume)
        {
        }

        public virtual void SetRecordVolume(int lineNumber, int volume)
        {
        }

        public virtual void Hold(int lineNumber, bool onHold)
        {
        }

        #endregion

        #region Codec Functions
        public virtual AudioCodecInformation[] GetAudioCodecs()
        {
            return new AudioCodecInformation[0];
        }

        public virtual void SetAudioCodecs(AudioCodecInformation[] codecs)
        {
        }
        #endregion

        #region Misc Functions
        public virtual void SendDTMF(int lineNumber, string dtmfString, bool inAudio)
        {
        }

        public virtual void EnableAnsweringMachineDetection(int lineNumber, bool enable)
        {
        }

        public virtual void SetAnsweringMachineDetectionSettings(int lineNumber, string settingsData)
        {
        }
        #endregion

        #region Speech Functions
        public virtual void SpeakText(int lineNumber, string textToSpeak)
        {
        }

        public virtual void StopSpeaking(int lineNumber)
        {
        }

        public virtual void ListenForSpeech(int lineNumber, string[] phrases)
        {
        }

        public virtual void StopListeningForSpeech(int lineNumber)
        {
        }

        public virtual void ClearSpeechRecoPhrases(int lineNumber)
        {
        }
        #endregion

        #region Event Raisers
        protected void RaiseIncomingTransfer(TransferEventArgs e)
        {
            if (IncomingTransfer != null)
                IncomingTransfer(this, e);
        }

        protected void RaiseCallTemporarilyMoved(CallEventArgs e)
        {
            if (CallTemporarilyMoved != null)
                CallTemporarilyMoved(this, e);
        }

        protected void RaiseIncomingCall(CallEventArgs e)
        {
            if (IncomingCall != null)
                IncomingCall(this, e);
        }

        protected void RaiseIncomingBusyCall(BusyCallEventArgs e)
        {
            if (IncomingBusyCall != null)
                IncomingBusyCall(this, e);
        }

        protected void RaiseCallConnected(CallEventArgs e)
        {
            if (CallConnected != null)
                CallConnected(this, e);
        }

        protected void RaiseCallEnded(LineEventArgs e)
        {
            if (CallEnded != null)
                CallEnded(this, e);
        }

        protected void RaiseDTMFToneRecognized(CallInputEventArgs e)
        {
            if (DTMFToneRecognized != null)
                DTMFToneRecognized(this, e);
        }

        protected void RaiseFaxToneDetected(LineEventArgs e)
        {
            if(FaxToneDetected != null)
                FaxToneDetected(this, e);
        }

        protected void RaiseFinishedSpeaking(LineEventArgs e)
        {
            if (FinishedSpeaking != null)
                FinishedSpeaking(this, e);
        }

        protected void RaiseSoundFinishedPlaying(LineEventArgs e)
        {
            if (SoundFinishedPlaying != null)
                SoundFinishedPlaying(this, e);
        }

        protected void RaiseSpeechRecognized(CallInputEventArgs e)
        {
            if (SpeechRecognized != null)
                SpeechRecognized(this, e);
        }

        protected void RaiseTransferFailed(LineEventArgs e)
        {
            if (TransferFailed != null)
                TransferFailed(this, e);
        }

        protected void RaiseTransferSucceeded(LineEventArgs e)
        {
            if (TransferSucceeded != null)
                TransferSucceeded(this, e);
        }

        protected void RaiseCallFailed(CallFailureEventArgs e)
        {
            if (CallFailed != null)
                CallFailed(this, e);
        }

        protected void RaiseError(ErrorEventArgs e)
        {
            if (Error != null)
                Error(this, e);
        }

        protected void RaiseRemoteOnHold(LineEventArgs e)
        {
            if (RemoteOnHold != null)
                RemoteOnHold(this, e);
        }

        protected void RaiseRemoteOffHold(LineEventArgs e)
        {
            if (RemoteOffHold != null)
                RemoteOffHold(this, e);
        }

        protected void RaiseAnswerDetectHuman(LineEventArgs e)
        {
            if (AnswerDetectHuman != null)
                AnswerDetectHuman(this, e);
        }

        protected void RaiseAnswerDetectMachine(LineEventArgs e)
        {
            if (AnswerDetectMachine != null)
                AnswerDetectMachine(this, e);
        }

        protected void RaiseAnswerDetectMachineGreetingFinished(LineEventArgs e)
        {
            if (AnswerDetectMachineGreetingFinished != null)
                AnswerDetectMachineGreetingFinished(this, e);
        }
        #endregion

        #region Network Registration Functions
        public virtual void Register(Guid registrationID, object registrationParams)
        {
        }

        public virtual void Unregister(Guid registrationID)
        {
        }

        public virtual string GetRegistrationState(Guid registrationID)
        {
            return "";
        }
        #endregion

        #region Misc Functions
        public virtual void SendingToVoicemail(int lineNumber)
        {
        }
        #endregion
    }
}
