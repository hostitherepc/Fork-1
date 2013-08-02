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
using CallButler.Manager.Plugin;
using Controls;

namespace CallButler.ManagementPlugins.GenericSIPProviderPlugin
{
    public partial class ProviderEditorForm : Form
    {
        private ProviderData providerData;
        private WOSI.NET.inTELIPhone.inTELIPhoneClient ipClient;
        private bool testing = false;

        public ProviderEditorForm()
        {
            providerData = new ProviderData();

            InitializeComponent();

            this.wizard.PageIndex = 0;

            ipClient = new WOSI.NET.inTELIPhone.inTELIPhoneClient(5064, 1, WOSI.NET.inTELIPhone.NATTraversalType.None);
            ipClient.ProfileRegistered += new WOSI.NET.inTELIPhone.SIPProfileEventHandler(ipClient_ProfileRegistered);
            ipClient.ProfileRegistrationError += new WOSI.NET.inTELIPhone.SIPProfileEventHandler(ipClient_ProfileRegistrationError);
        }

        public ProviderData ProviderData
        {
            get
            {
                return providerData;
            }
            set
            {
                providerData = value;
                ReadData();
            }
        }

        private void ReadData()
        {
            txtPassword.Text = providerData.AuthPassword;
            txtAuthUsername.Text = providerData.AuthUsername;
            cboProviderName.Text = providerData.Name;
            txtProxyServer.Text = providerData.SIPProxy;
            txtRegistrarServer.Text = providerData.SIPRegistrar;
            txtPhoneNumber.Text = providerData.Username;
            txtCallerID.Text = providerData.DisplayName;
            txtDomain.Text = providerData.Domain;
            cbEnable.Checked = providerData.IsEnabled;
            cbEnableReg.Checked = providerData.EnableRegistration;
            cbCustomCallerID.Checked = !providerData.SupressOutboundUsername;
            txtCallerIDPrefix.Text = providerData.OutboundUsernamePrefix;

            if (providerData.Expires == 0)
                numExpires.Value = 30;
            else
                numExpires.Value = providerData.Expires;
        }

        private void UpdateData()
        {
            providerData.AuthPassword = txtPassword.Text.Trim();
            providerData.AuthUsername = txtAuthUsername.Text.Trim();
            providerData.Name = cboProviderName.Text.Trim();
            providerData.SIPProxy = txtProxyServer.Text.Trim();
            providerData.SIPRegistrar = txtRegistrarServer.Text.Trim();
            providerData.Username = txtPhoneNumber.Text.Trim();
            providerData.DisplayName = txtCallerID.Text;
            providerData.Domain = txtDomain.Text.Trim();
            providerData.IsEnabled = cbEnable.Checked;
            providerData.EnableRegistration = cbEnableReg.Checked;
            providerData.Expires = (int)numExpires.Value;
            providerData.SupressOutboundUsername = !cbCustomCallerID.Checked;
            providerData.OutboundUsernamePrefix = txtCallerIDPrefix.Text;
        }

        private void wizard_WizardFinished(object sender, EventArgs e)
        {
            UpdateData();
            this.DialogResult = DialogResult.OK;
        }

        private void lblEnableProvider_Click(object sender, EventArgs e)
        {
            cbEnable.Checked = !cbEnable.Checked;
        }

        private void lblSipRegistration_Click(object sender, EventArgs e)
        {
            cbEnableReg.Checked = !cbEnableReg.Checked;
        }

        private void btnBeginTest_Click(object sender, EventArgs e)
        {
            pnlResults.Visible = true;

            // Create our SIP profile
            WOSI.NET.inTELIPhone.SIPProfile sipProfile = new WOSI.NET.inTELIPhone.SIPProfile();

            sipProfile.AuthPassword = txtPassword.Text.Trim();
            sipProfile.AuthUsername = txtAuthUsername.Text.Trim();
            sipProfile.DisplayName = txtCallerID.Text;
            sipProfile.DomainRealm = txtDomain.Text;
            sipProfile.SIPProxyServer = txtProxyServer.Text;
            sipProfile.SIPRegistrarServer = txtRegistrarServer.Text;
            sipProfile.Username = txtPhoneNumber.Text;

            testing = true;

            ipClient.RegisterProfile(sipProfile, (int)numExpires.Value);
        }

        void ipClient_ProfileRegistrationError(object sender, WOSI.NET.inTELIPhone.SIPProfileEventArgs e)
        {
            if (testing)
            {
                testing = false;

                pnlResults.Visible = false;

                MessageBox.Show(this, string.Format(Properties.LocalizedStrings.ProviderEditorForm_TestFailure, e.ResponseCode + " " + e.ResponsePhrase), Properties.LocalizedStrings.ProviderEditorForm_TestFailureTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Unregister the profile
                ipClient.UnregisterProfile(e.SIPProfile);
            }
        }

        void ipClient_ProfileRegistered(object sender, WOSI.NET.inTELIPhone.SIPProfileEventArgs e)
        {
            if (testing)
            {
                testing = false;

                pnlResults.Visible = false;

                MessageBox.Show(this, Properties.LocalizedStrings.ProviderEditorForm_TestSuccess, Properties.LocalizedStrings.ProviderEditorForm_TestSuccessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Unregister the profile
                ipClient.UnregisterProfile(e.SIPProfile);
            }
        }

        private void ProviderEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            testing = false;

            ipClient.ProfileRegistered -= this.ipClient_ProfileRegistered;
            ipClient.ProfileRegistrationError -= this.ipClient_ProfileRegistrationError;

            ipClient.Dispose();
            ipClient = null;
        }
    }
}