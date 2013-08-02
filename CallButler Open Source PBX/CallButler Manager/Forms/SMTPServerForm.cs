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

namespace CallButler.Manager.Forms
{
    public partial class SMTPServerForm : CallButler.Manager.Controls.CallButlerDialogFormBase
    {
        public SMTPServerForm()
        {
            InitializeComponent();

            // Load our settings
            txtServerName.Text = ManagementInterfaceClient.ManagementInterface.SMTPServer;
            numSMTPPort.Value = ManagementInterfaceClient.ManagementInterface.SMTPPort;
            txtUsername.Text = ManagementInterfaceClient.ManagementInterface.SMTPUsername;
            cbSSL.Checked = ManagementInterfaceClient.ManagementInterface.SMTPUseSSL;

            string smtpPassword = ManagementInterfaceClient.ManagementInterface.SMTPPassword;

            if (smtpPassword.Length > 0)
                txtPassword.Text = WOSI.Utilities.CryptoUtils.Decrypt(smtpPassword, WOSI.CallButler.Data.Constants.EncryptionPassword);

            txtServerName.Select();

            Utils.PrivateLabelUtils.ReplaceProductNameControl(this);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Save our settings
            ManagementInterfaceClient.ManagementInterface.SMTPServer = txtServerName.Text;
            ManagementInterfaceClient.ManagementInterface.SMTPPort = (int)numSMTPPort.Value;
            ManagementInterfaceClient.ManagementInterface.SMTPUseSSL = cbSSL.Checked;
            ManagementInterfaceClient.ManagementInterface.SMTPUsername = txtUsername.Text.Trim();

            if (txtPassword.Text.Length > 0)
                ManagementInterfaceClient.ManagementInterface.SMTPPassword = WOSI.Utilities.CryptoUtils.Encrypt(txtPassword.Text.Trim(), WOSI.CallButler.Data.Constants.EncryptionPassword);
            else
                ManagementInterfaceClient.ManagementInterface.SMTPPassword = "";
        }

        private void btnSendTestEmail_Click(object sender, EventArgs e)
        {
            Utils.EmailUtils.SendTestEmail(txtServerName.Text, (int)numSMTPPort.Value, cbSSL.Checked, txtUsername.Text.Trim(), txtPassword.Text.Trim());
        }

        private void lblSSL_Click(object sender, EventArgs e)
        {
            cbSSL.Checked = !cbSSL.Checked;
        }
    }
}

