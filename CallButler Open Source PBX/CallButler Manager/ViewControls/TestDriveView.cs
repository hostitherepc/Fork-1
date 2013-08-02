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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WOSI.NET.inTELIPhone;

namespace CallButler.Manager.ViewControls
{
    public partial class TestDriveView : CallButler.Manager.ViewControls.ViewControlBase
    {
        private SIPProfile profile;

        private inTELIPhoneClient ipClient = null;
        private int sipPort = 5060;

        public TestDriveView()
        {
            InitializeComponent();

            lblNumbers.Text = "";
            this.HelpRTFText = Properties.Resources.TestDriveHelp;

            UpdateTime();
        }

        void ipClient_IncomingTransfer(object sender, IncomingTransferEventArgs e)
        {
            ipClient.DeclineIncomingTransfer(e.LineNumber);
            ipClient.EndCall(e.LineNumber);

            MessageBox.Show(this, String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.TestDriveView_TransferSim), WOSI.Utilities.StringUtils.FormatPhoneNumber(e.RemoteUserName)), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.TestDriveView_CallTransfer), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void ipClient_CallConnected(object sender, IncomingCallEventArgs e)
        {
            lblStatus.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.TestDriveView_Connected);
        }

        void ipClient_CallFailed(object sender, CallFailedEventArgs e)
        {
            HangUp();
            lblStatus.Text = e.ReasonDescription;
        }

        void ipClient_CallEnded(object sender, CallStateEventArgs e)
        {
            HangUp();
            lblStatus.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.TestDriveView_ReadyForCall);
            lblNumbers.Text = "";
        }

        protected override void OnLoadData()
        {
            if (ipClient == null)
            {
                profile = new SIPProfile();
                
                profile.DomainRealm = "callbutler.com";
                if (Properties.Settings.Default.ManagementInterfaceType == WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceType.Hosted)
                {
                    ipClient = new inTELIPhoneClient(5060, 1, NATTraversalType.PartialSTUN);
                }
                else
                {
                    ipClient = new inTELIPhoneClient(5060, 1, NATTraversalType.None);
                    ipClient.SetRtpPort(1, 7850);
                }
                ipClient.EnableTracing = false;
                ipClient.CallEnded += new CallStateEventHandler(ipClient_CallEnded);
                ipClient.CallFailed += new CallFailedEventHandler(ipClient_CallFailed);
                ipClient.CallConnected += new IncomingCallEventHandler(ipClient_CallConnected);
                ipClient.IncomingTransfer += new EventHandler<IncomingTransferEventArgs>(ipClient_IncomingTransfer);
                
                //ipClient.SipPort = 8996;

                //trkMicVolume.Value = ipClient.GetMicrophoneVolume(1);
                trkSpeakerVolume.Value = ipClient.GetOutputVolume(1);

                sipPort = ManagementInterfaceClient.ManagementInterface.SIPPort;
            }
        }

        protected override void OnSaveData()
        {
            if(ipClient.LineInUse(1))
                ipClient.EndCall(1);

            ipClient.Dispose();
            ipClient = null;
            GC.Collect();
        }

        private void trkMicVolume_ValueChanged(object sender, EventArgs e)
        {
            //ipClient.SetMicrophoneVolume(1, (short)trkMicVolume.Value);
        }

        private void trkSpeakerVolume_ValueChanged(object sender, EventArgs e)
        {
            ipClient.SetOutputVolume(1, (short)trkSpeakerVolume.Value);
        }

        public void Call()
        {
            if (!ipClient.LineInUse(1))
            {
                profile.DisplayName = txtCallerID.Text;
                profile.Username = WOSI.Utilities.StringUtils.CleanTelephoneNumber(txtNumber.Text);

                string callInfo = "";
                if (Properties.Settings.Default.ManagementInterfaceType == WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceType.Hosted)
                {
                    callInfo = ManagementInterfaceClient.ManagementInterface.GetHostedTestAddress(ManagementInterfaceClient.AuthInfo);
                }
                else
                {
                    callInfo = Properties.Settings.Default.CallButlerServer + ":" + sipPort;
                }

                PickUp();

                ipClient.Call(1, profile, callInfo, false);
            }
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            NumberPressed(((Control)sender).Tag.ToString());
        }

        private void NumberPressed(string number)
        {
            if (ipClient.LineInUse(1))
            {
                ipClient.SendSIPOutofBandDTMF(1, number);


                lblNumbers.Text += number;

                if (lblNumbers.Text.Length > 18)
                    lblNumbers.Text = lblNumbers.Text.Remove(0, 1);
            }
        }

        private void UpdateTime()
        {
            lblTime.Text = DateTime.Now.ToString("h:mm t");
        }

        private void PickUp()
        {
            pnlPhone.BackgroundImage = Properties.Resources.phone_back_up;
            lblHook.Text = "< Click Receiver to End Call";
        }

        private void HangUp()
        {
            pnlPhone.BackgroundImage = Properties.Resources.Phone_back_down;
            lblHook.Text = "< Click Receiver to Begin Call";
        }

        private void pnlPickup_Click(object sender, EventArgs e)
        {
            if (ipClient.LineInUse(1))
            {
                HangUp();
                Application.DoEvents();
                ipClient.EndCall(1);
            }
            else
            {
                Call();
            }
        }

        private void tmrTime_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }

        private void TestDriveView_Load(object sender, EventArgs e)
        {
            if (this.ParentForm != null)
                this.ParentForm.ActiveControl = this;
        }

        private void TestDriveView_KeyDown(object sender, KeyEventArgs e)
        {
            int x = 1;
        }

        void Control_KeyDown(object sender, KeyEventArgs e)
        {
            int x = 1;
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (m.Msg == 0x100)
            {
                char number = (char)m.WParam;

                if(Char.IsDigit(number) || number == '*' || number == '#')
                    NumberPressed(((char)m.WParam).ToString());
            }

            return base.ProcessKeyPreview(ref m);
        }
    }
}

