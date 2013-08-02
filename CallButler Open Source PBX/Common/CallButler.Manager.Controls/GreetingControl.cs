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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WOSI.Utilities.Sound;
using WOSI.CallButler.Data;

namespace CallButler.Manager.Controls
{
    public partial class GreetingControl : UserControl
    {
        private bool lockCheck = false;

        WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceClientBase managementClient;
        WOSI.CallButler.ManagementInterface.CallButlerAuthInfo authInfo;
        //public event EventHandler<EventArgs> OnSoundChanged;
        public event EventHandler<TextChangedEventArgs> OnTextChanged;

        private WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow greetingRow;

        public GreetingControl()
        {
            Initialize();
        }

        public GreetingControl(WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceClientBase managementClient, WOSI.CallButler.ManagementInterface.CallButlerAuthInfo authInfo)
        {
            this.managementClient = managementClient;
            this.authInfo = authInfo;

            Initialize();
        }

        private void Initialize()
        {
            InitializeComponent();

            //if (this.managementClient == null)
                mnuCall.Visible = false;

            GreetingType = GreetingType.SoundGreeting;
            wzdGreeting.PageIndex = 0;

            speechControl.ShowSuggestTextButton = true;
            speechControl.ShowVoiceSelection = true;
            speechControl.TextChanged += new EventHandler(speechControl_TextChanged);
        }

        void speechControl_TextChanged(object sender, EventArgs e)
        {
            FireOnTextChangedEvent();
        }
       
        private void FireOnSoundChangedEvent()
        {
            /*if (OnSoundChanged != null)
            {
                OnSoundChanged(this, new EventArgs());
            }*/
        }

        private void FireOnTextChangedEvent()
        {
            if (OnTextChanged != null)
            {
                OnTextChanged(this, new TextChangedEventArgs(speechControl.SpeechText));
            }
        }

        public void LoadGreeting(WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow greeting, string greetingSoundCache)
        {
            recordingControl.Reset();
            speechControl.SpeechText = "";

            greetingRow = greeting;

            if (greeting != null)
            {
                this.GreetingType = (WOSI.CallButler.Data.GreetingType)greetingRow.Type;

                if (this.GreetingType == GreetingType.TextGreeting)
                {
                    speechControl.SpeechText = greetingRow.Data;
                    speechControl.Voice = greetingRow.Voice;
                }
                else if (this.GreetingType == GreetingType.SoundGreeting)
                {
                    recordingControl.LoadSoundFile(greetingSoundCache + "\\" + greetingRow.LanguageID + "\\" + greetingRow.GreetingID.ToString() + ".snd");
                }
            }
        }

        public void SaveGreeting(string greetingSoundCache, WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow localizedGreeting)
        {
            StopSounds();

            localizedGreeting.Type = (short)this.GreetingType;
            
            if (this.GreetingType == GreetingType.TextGreeting)
            {
                localizedGreeting.Data = speechControl.SpeechText;
                localizedGreeting.Voice = speechControl.Voice;
                
            }
            else if (this.GreetingType == GreetingType.SoundGreeting)
            {
                if (mnuCall.Checked)
                {
                    localizedGreeting.Voice = "";
                    localizedGreeting.Data = "CallRecording";
                }
                else if (recordingControl.NewFile && File.Exists(recordingControl.WorkingFile))
                {
                    localizedGreeting.Voice = "";

                    string greetingDirectory = greetingSoundCache + "\\" + localizedGreeting.LanguageID;
                    string greetingSoundFile = greetingDirectory + "\\" + localizedGreeting.GreetingID + ".snd";

                    if (!Directory.Exists(greetingDirectory))
                        Directory.CreateDirectory(greetingDirectory);

                    if (string.Compare(recordingControl.WorkingFile, greetingSoundFile, true) != 0)
                    {
                        File.Copy(recordingControl.WorkingFile, greetingSoundFile, true);
                        File.SetAttributes(greetingSoundFile, FileAttributes.Normal);
                    }
                    
                    // Fill in our file checksum
                    localizedGreeting.Data = WOSI.Utilities.CryptoUtils.GetFileChecksum(greetingSoundFile);
                }
            }
        }

        public void StopSounds()
        {
            recordingControl.StopSounds();
            speechControl.StopSpeaking();
        }

        public void LoadVoices(string[] voices)
        {
            speechControl.LoadVoices(voices);
        }

        public void SaveGreeting( string greetingSoundCache)
        {
            SaveGreeting(greetingSoundCache, greetingRow);
        }

        [Browsable(false), DefaultValue("")]
        public string SuggestedText
        {
            get
            {
                return speechControl.SuggestedText;
            }
            set
            {
                speechControl.SuggestedText = value;
                recordingControl.SuggestedText = value;
            }
        }

        [DefaultValue(typeof(GreetingType), "GreetingType.SoundGreeting")]
        private GreetingType GreetingType
        {
            get
            {
                if (mnuSpeak.Checked)
                    return GreetingType.TextGreeting;
                else
                    return GreetingType.SoundGreeting;
            }
            set
            {
                if (value == GreetingType.SoundGreeting)
                {
                    mnuRecord.Checked = true;
                }
                else if (value == GreetingType.TextGreeting)
                {
                    mnuSpeak.Checked = true;
                }
            }
        }

        [Browsable(false)]
        public bool CallForRecording
        {
            get
            {
                return mnuCall.Checked;
            }
        }

        [Browsable(false)]
        public string CallToNumber
        {
            get
            {
                return txtCallTo.Text;
            }
            set
            {
                txtCallTo.Text = value;
            }
        }

        private void btnVoices_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.VoicesURL);
        }

        private void mnuGreetingType_CheckedChanged(object sender, EventArgs e)
        {
            if (!lockCheck)
            {
                lockCheck = true;

                StopSounds();

                ToolStripMenuItem senderItem = (ToolStripMenuItem)sender;

                foreach (ToolStripMenuItem item in mnuGreetingType.DropDownItems)
                {
                    if (item != senderItem && item.Checked)
                        item.Checked = false;
                }

                if (senderItem.Checked)
                {
                    mnuGreetingType.Image = senderItem.Image;
                    mnuGreetingType.Text = senderItem.Text;
                }

                lockCheck = false;
            }
        }

        private void mnuRecord_CheckedChanged(object sender, EventArgs e)
        {
            mnuGreetingType_CheckedChanged(sender, e);

            if(((ToolStripMenuItem)sender).Checked)
                wzdGreeting.PageIndex = 0;
        }

        private void mnuCall_CheckedChanged(object sender, EventArgs e)
        {
            mnuGreetingType_CheckedChanged(sender, e);

            if (((ToolStripMenuItem)sender).Checked)
                wzdGreeting.PageIndex = 2;
        }

        private void mnuSpeak_CheckedChanged(object sender, EventArgs e)
        {
            mnuGreetingType_CheckedChanged(sender, e);

            if (((ToolStripMenuItem)sender).Checked)
                wzdGreeting.PageIndex = 1;
        }

        private void btnPlaceCall_Click(object sender, EventArgs e)
        {
            managementClient.ManagementInterface.PlaceGreetingRecordCall(authInfo, txtCallTo.Text, greetingRow.GreetingID, greetingRow.LanguageID);
        }
    }

    public class GreetingInfo
    {
        public bool NewRecording = false;
        public string GreetingID = "";
        public string SoundFilePath = "";
        public string SpeechText = "";
        public string SpeechVoice = "";
        public GreetingType GreetingType;
    }

    public class TextChangedEventArgs : System.EventArgs
    {
        private string _text;

        public TextChangedEventArgs( string text )
        {
            Text = text;
        }

        public string Text
        {
            get
            {
                return _text;
            }
            private set
            {
                _text = value;
            }
        }
    }
}
