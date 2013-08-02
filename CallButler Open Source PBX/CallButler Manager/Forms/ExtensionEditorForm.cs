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
using Controls;
using CallButler.Manager.ViewControls;

namespace CallButler.Manager.Forms
{
    public partial class ExtensionEditorForm : CallButler.Manager.Forms.EditorWizardFormBase
    {
        private const string blankPassword = "     ";
        private WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension;
        private WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable extensionContacts;
        private WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensions;
        private bool loading = true;
        
        public ExtensionEditorForm(WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensions, WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension, WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable extensionContacts, WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow voicemailGreeting)
        {
            InitializeComponent();

            this.extensions = extensions;
            this.extension = extension;
            this.extensionContacts = extensionContacts;

          
           CallButler.Manager.Plugin.CallButlerManagementPlugin plugin = PluginManager.GetPluginFromID(new Guid(Properties.Settings.Default.DefaultFindMePluginID));

           if (plugin != null)
           {
               CallButler.Manager.Plugin.CallButlerManagementPluginViewControl c = plugin.GetNewViewControl();
               c.Load(new object[] { extension, extensionContacts, ManagementInterfaceClient.ManagementInterface.TelephoneNumberDescription, true, true, extensions });
               c.Dock = DockStyle.Fill;
               pnlFindMe.Controls.Add(c);
           }
           else
           {
               NoPluginFoundView c = new NoPluginFoundView();
               c.PluginType = "Find me/Follow me plugin";

               c.Dock = DockStyle.Fill;
               pnlFindMe.Controls.Add(c);
           }

            wizard.PageIndex = 0;

            // Update our UI
            numExtNum.Value = extension.ExtensionNumber;
            txtFirstName.Text = extension.FirstName;
            txtLastName.Text = extension.LastName;
            cbEnableSearch.Checked = extension.EnableSearch;
            cbEnableManagement.Checked = extension.EnableManagement;
            cbEmailNotification.Checked = extension.EmailNotification;
            cbAttach.Checked = extension.EmailAttachment;
            txtEmailAddress.Text = extension.EmailAddress;
            cbCallScreening.Checked = extension.EnableCallScreening;
            cbEnableOutbound.Checked = extension.EnableOutboundCalls;

            if (!extension.IsUseConferenceTransferNull())
                cbHandOff.Checked = !extension.UseConferenceTransfer;
            else
                cbHandOff.Checked = true;

            greetingControl.LoadGreeting(voicemailGreeting, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache));

            if (!extension.IsPasswordNull() && extension.Password.Length > 0)
            {
                txtPassword.Text = blankPassword;
                txtConfirmPassword.Text = blankPassword;
            }

            if (!extension.IsPBXPasswordNull() && extension.PBXPassword.Length > 0)
            {
                txtPBXPassword.Text = blankPassword;
                txtConfirmPBXPassword.Text = blankPassword;
            }

            txtFirstName.Select();

            btnImportOutlook.Enabled = Utilities.ContactManagement.ContactManagerFactory.CreateContactManager(Utilities.ContactManagement.ContactType.Outlook).IsInstalled;

            loading = false;
            greetingControl.LoadVoices(ManagementInterfaceClient.ManagementInterface.GetTTSVoices());

            pgFindme.Enabled = true;
            pnlHandoff.Visible = true;
            pnlPBXPassword.Visible = true;

            Utils.PrivateLabelUtils.ReplaceProductNameControl(this);
        }

        private void UpdateData()
        {
            extension.ExtensionNumber = (short)numExtNum.Value;
            extension.FirstName = txtFirstName.Text;
            extension.LastName = txtLastName.Text;
            extension.EnableSearch = cbEnableSearch.Checked;
            extension.EnableManagement = cbEnableManagement.Checked;
            extension.EmailNotification = cbEmailNotification.Checked;
            extension.EmailAttachment = cbAttach.Checked;
            extension.EmailAddress = txtEmailAddress.Text;
            extension.EnableCallScreening = cbCallScreening.Checked;
            extension.EnableOutboundCalls = cbEnableOutbound.Checked;

            if (pnlHandoff.Visible)
                extension.UseConferenceTransfer = !cbHandOff.Checked;

            greetingControl.SaveGreeting(WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache));


            if (txtPassword.Text != blankPassword)
            {
                if (txtPassword.Text.Length == 0)
                {
                    extension.Password = "";
                }
                else
                {
                    // Hash the password
                    extension.Password = WOSI.Utilities.CryptoUtils.CreateMD5Hash(txtPassword.Text.Trim());
                }
            }

            if (txtPBXPassword.Text != blankPassword)
            {
                if (txtPBXPassword.Text.Length == 0)
                {
                    extension.PBXPassword = "";
                }
                else
                {
                    // Encrypt the password
                    extension.PBXPassword = WOSI.Utilities.CryptoUtils.Encrypt(txtPBXPassword.Text.Trim(), WOSI.CallButler.Data.Constants.EncryptionPassword);
                }
            }
        }

        private void CheckSMTPSettings()
        {
            // If our SMTP Server settings haven't been setup, ask the user to set them up
            if (ManagementInterfaceClient.ManagementInterface.SMTPServer.Length == 0 && Properties.Settings.Default.ManagementInterfaceType != WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceType.Hosted)
            {
                if (MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ExtensionEditorForm_EnterSMTP), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ExtensionEditorForm_EmailConfiguration), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SMTPServerForm smtpForm = new SMTPServerForm();

                    smtpForm.ShowDialog(this);
                }
            }
        }

        private void btnImportOutlook_Click(object sender, EventArgs e)
        {
            OutlookContactForm ocForm = new OutlookContactForm();

            ocForm.MultiSelect = false;

            if (ocForm.ShowDialog(this) == DialogResult.OK)
            {
                if (ocForm.SelectedContacts.Length > 0)
                {
                    txtFirstName.Text = ocForm.SelectedContacts[0].FirstName;
                    txtLastName.Text = ocForm.SelectedContacts[0].LastName;
                    txtEmailAddress.Text = ocForm.SelectedContacts[0].Email1Address;
                }
            }
        }

        private void pgManagement_CloseFromNext(object sender, global::Controls.Wizard.PageEventArgs e)
        {
            if (!CheckPasswords())
                e.Cancel = true;
        }

        private bool CheckPasswords()
        {
            // Make sure our passwords match
            if (txtPassword.Text != txtConfirmPassword.Text || txtPBXPassword.Text != txtConfirmPBXPassword.Text)
            {
                MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_PasswordsDoNotMatch), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_PasswordMismatch), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    txtPassword.Text = "";
                    txtConfirmPassword.Text = "";
                    txtPassword.Select();
                }
                else if (txtPBXPassword.Text != txtConfirmPBXPassword.Text)
                {
                    txtPBXPassword.Text = "";
                    txtConfirmPBXPassword.Text = "";
                    txtPBXPassword.Select();
                }

                return false;
            }

            return true;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void cbEmailNotification_CheckedChanged(object sender, EventArgs e)
        {
            pnlEmailNotifications.Enabled = cbEmailNotification.Checked;

            if (!loading && cbEmailNotification.Checked)
                CheckSMTPSettings();
        }

        private void wizard_WizardFinished(object sender, EventArgs e)
        {
            if (!CheckPasswords())
            {
                wizard.NextTo(pgManagement);
            }
            else
            {
                UpdateData();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSendTestEmail_Click(object sender, EventArgs e)
        {
            CheckSMTPSettings();
            //Utils.EmailUtils.SendTEmail(txtEmailAddress.Text, ManagementInterfaceClient.ManagementInterface.SMTPServer, ManagementInterfaceClient.ManagementInterface.SMTPPort, ManagementInterfaceClient.ManagementInterface.SMTPUseSSL, ManagementInterfaceClient.ManagementInterface.SMTPUsername, smtpPassword);
            Utils.EmailUtils.SendTestEmail(txtEmailAddress.Text);
        }

        private void btnSetupInstructions_Click(object sender, EventArgs e)
        {
            CheckSMTPSettings();

            // Decrypt our SMTP password
            //string smtpPassword = ManagementInterfaceClient.ManagementInterface.SMTPPassword;

            //if (smtpPassword.Length > 0)
            //    smtpPassword = WOSI.Utilities.CryptoUtils.Decrypt(smtpPassword, WOSI.CallButler.Data.Constants.EncryptionPassword);

            string messageString = Properties.Settings.Default.ExtensionClientSetupMail;
            messageString = messageString.Replace("#ExtNumber#", numExtNum.Value.ToString());

            if (txtPassword.Text == blankPassword)
                messageString = messageString.Replace("#ExtPasscode#", CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ExtensionEditorForm_ContactAdmin));
            else
                messageString = messageString.Replace("#ExtPasscode#", txtPassword.Text.Trim());

            messageString = messageString.Replace("#CallButlerServer#", "");
            messageString = messageString.Replace("#DownloadURL#", Properties.Settings.Default.ExtensionClientDownloadURL);

            string sendTo = txtEmailAddress.Text;

            if(sendTo.Length == 0)
            {
                global::Controls.InputBoxResult result = global::Controls.InputBox.ShowDialog(null, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ExtensionEditorForm_PleaseSpecifyEmail), "", null);

                if (result.DialogResult == System.Windows.Forms.DialogResult.OK)
                    sendTo = result.Value;
                else
                    return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //WOSI.Utilities.EmailUtils.SendEmail("mailer@callbutler.com", sendTo, Properties.LocalizedStrings.ExtensionEditorForm_YourNewExtension, messageString, ManagementInterfaceClient.ManagementInterface.SMTPServer, ManagementInterfaceClient.ManagementInterface.SMTPPort, ManagementInterfaceClient.ManagementInterface.SMTPUseSSL, ManagementInterfaceClient.ManagementInterface.SMTPUsername, smtpPassword);
                ManagementInterfaceClient.ManagementInterface.SendEmail(ManagementInterfaceClient.AuthInfo, sendTo, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ExtensionEditorForm_YourNewExtension), messageString);
                MessageBox.Show(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_EmailSentTo) + sendTo, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_EmailSent), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_EmailProblem), sendTo, exception.Message), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_EmailError), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Cursor.Current = Cursors.Default;
        }

        private void ExtensionEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            greetingControl.StopSounds();
        }

        private void lblDialByName_Click(object sender, EventArgs e)
        {
            cbEnableSearch.Checked = !cbEnableSearch.Checked;
        }

        private void lblRemoteManagement_Click(object sender, EventArgs e)
        {
            cbEnableManagement.Checked = !cbEnableManagement.Checked;
        }

        private void lblEmail_Click(object sender, EventArgs e)
        {
            cbEmailNotification.Checked = !cbEmailNotification.Checked;
        }

        private void lblAttachment_Click(object sender, EventArgs e)
        {
            cbAttach.Checked = !cbAttach.Checked;
        }

        private void lblHandoff_Click(object sender, EventArgs e)
        {
            cbHandOff.Checked = !cbHandOff.Checked;
        }

        private void pgManagement_CloseFromBack(object sender, global::Controls.Wizard.PageEventArgs e)
        {
            if (!CheckPasswords())
                e.Cancel = true;
        }

        private void lblCallScreen_Click(object sender, EventArgs e)
        {
            cbCallScreening.Checked = !cbCallScreening.Checked;
        }

        private void lblEnableOutbound_Click(object sender, EventArgs e)
        {
            cbEnableOutbound.Checked = !cbEnableOutbound.Checked;
        }
    }
}

