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

namespace CallButler.Manager.ViewControls
{
    public partial class SettingsView : CallButler.Manager.ViewControls.ViewControlBase
    {
        private const string blankPassword = "        ";
        bool requiresRestart = false;

        public SettingsView()
        {
            InitializeComponent();

            cbSTUN.CheckedChanged += new EventHandler(cbSTUN_CheckedChanged);

            numLineCount.Maximum = 1000;

            LoadSettings();

            wizard.PageIndex = 0;

            if (Properties.Settings.Default.ManagementInterfaceType != WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceType.Hosted)
            {
                cbEnableManagement.Visible = true;
                lblRemoteManagement.Visible = true;
            }
            else
            {
                cbEnableManagement.Visible = false;
                lblRemoteManagement.Visible = false;
            }
            

            // Update our permissions
            pgSecurity.Enabled = true;
            cbEnableManagement.Visible = true;
            lblRemoteManagement.Visible = cbEnableManagement.Visible;
            pgEmailSettings.Enabled = true;
            pgMusicSettings.Enabled = true;
            pgSpeechSettings.Enabled = true;
            pgAudioSettings.Enabled = true;
            pgCodecSettings.Enabled = true;
            pgNetworkSettings.Enabled = true;
            pgUpdateSettings.Enabled = true;
            pgLoggingSettings.Enabled = true;

            lblBusyRedirectServer.Visible = true;
            txtBusyRedirectServer.Visible = true;
        }

        void cbSTUN_CheckedChanged(object sender, EventArgs e)
        {
            pnlSTUN.Enabled = cbSTUN.Checked;
        }

        private void LoadSettings()
        {
            txtFromEmailAddress.Text = ManagementInterfaceClient.ManagementInterface.SMTPFromEmail;
            txtSMTPServer.Text = ManagementInterfaceClient.ManagementInterface.SMTPServer;
            numSMTPPort.Value = ManagementInterfaceClient.ManagementInterface.SMTPPort;
            cbSMTPSSL.Checked = ManagementInterfaceClient.ManagementInterface.SMTPUseSSL;
            txtSMTPUsername.Text = ManagementInterfaceClient.ManagementInterface.SMTPUsername;
            txtSMTPPassword.Text = WOSI.Utilities.CryptoUtils.Decrypt(ManagementInterfaceClient.ManagementInterface.SMTPPassword, WOSI.CallButler.Data.Constants.EncryptionPassword);
            numLineCount.Value = ManagementInterfaceClient.ManagementInterface.LineCount;
            numLineCount.Tag = "*";
            trkRecordVolume.Value = ManagementInterfaceClient.ManagementInterface.GetRecordVolume(ManagementInterfaceClient.AuthInfo);
            trkSoundVolume.Value = ManagementInterfaceClient.ManagementInterface.GetSoundVolume(ManagementInterfaceClient.AuthInfo);
            trkSpeechVolume.Value = ManagementInterfaceClient.ManagementInterface.GetSpeechVolume(ManagementInterfaceClient.AuthInfo);

            numSIPPort.Value = ManagementInterfaceClient.ManagementInterface.SIPPort;
            numSIPPort.Tag = "*";
            cbUseInternalIPForSIP.Checked = ManagementInterfaceClient.ManagementInterface.UseInternalAddressForSIP;
            cbUseInternalIPForSIP.Tag = "*";
            cbSTUN.Checked = ManagementInterfaceClient.ManagementInterface.EnableSTUN;
            cbSTUN.Tag = "*";
            txtSTUNServer.Text = ManagementInterfaceClient.ManagementInterface.STUNServer;
            txtSTUNServer.Tag = "*";
            txtBusyRedirectServer.Text = ManagementInterfaceClient.ManagementInterface.GetBusyRedirectServer(ManagementInterfaceClient.AuthInfo);

            chkSendErrorReports.Checked = ManagementInterfaceClient.ManagementInterface.ReportErrors;

            cboLogStorage.SelectedIndex = (int)ManagementInterfaceClient.ManagementInterface.LogStorage;
            cboLogLevel.SelectedIndex = (int)ManagementInterfaceClient.ManagementInterface.LogLevel;
            cbSendLogErrorEmail.Checked = ManagementInterfaceClient.ManagementInterface.GetSendLogErrorEmail(ManagementInterfaceClient.AuthInfo);
            txtLogErrorEmail.Text = ManagementInterfaceClient.ManagementInterface.GetLogErrorEmail(ManagementInterfaceClient.AuthInfo);

            // Load our on hold music
            //if(Licensing.Management.AppPermissions.StatIsPermitted("Settings.MusicSettings"))
                LoadHoldMusic();

            cboDefaultVoice.Items.Add("");
            foreach (string voice in ManagementInterfaceClient.ManagementInterface.GetTTSVoices())
            {
                cboDefaultVoice.Items.Add(voice);
            }
            
            cboDefaultVoice.Text = ManagementInterfaceClient.ManagementInterface.GetDefaultVoice(ManagementInterfaceClient.AuthInfo);

            if (ManagementInterfaceClient.ManagementInterface.GetManagementPassword(ManagementInterfaceClient.AuthInfo).Length > 0)
            {
                txtManagementPassword.Text = blankPassword;
                txtManagementConfirmPassword.Text = blankPassword;
            }

            cbEnableManagement.Checked = ManagementInterfaceClient.ManagementInterface.AllowRemoteManagement;
            cbEnableManagement.Tag = "*";
            chkNotifyofProductVersions.Checked = Properties.Settings.Default.UpdateNotification;

            // Load our codecs
            lbCodecs.Items.Clear();
            WOSI.CallButler.ManagementInterface.AudioCodecInformation[] audioCodecs = ManagementInterfaceClient.ManagementInterface.GetAudioCodecs(ManagementInterfaceClient.AuthInfo);

            foreach (WOSI.CallButler.ManagementInterface.AudioCodecInformation audioCodec in audioCodecs)
            {
                lbCodecs.Items.Add(audioCodec, audioCodec.Enabled);
            }

            btnApply.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void SaveSettings()
        {
            ManagementInterfaceClient.ManagementInterface.AllowRemoteManagement = cbEnableManagement.Checked;
            ManagementInterfaceClient.ManagementInterface.SMTPFromEmail = txtFromEmailAddress.Text;
            ManagementInterfaceClient.ManagementInterface.SMTPServer = txtSMTPServer.Text;
            ManagementInterfaceClient.ManagementInterface.SMTPPort = (int)numSMTPPort.Value;
            ManagementInterfaceClient.ManagementInterface.SMTPUseSSL = cbSMTPSSL.Checked;
            ManagementInterfaceClient.ManagementInterface.SMTPUsername = txtSMTPUsername.Text.Trim();
            ManagementInterfaceClient.ManagementInterface.SMTPPassword = WOSI.Utilities.CryptoUtils.Encrypt(txtSMTPPassword.Text.Trim(), WOSI.CallButler.Data.Constants.EncryptionPassword);
            ManagementInterfaceClient.ManagementInterface.LineCount = (int)numLineCount.Value;

            ManagementInterfaceClient.ManagementInterface.SetSoundVolume(ManagementInterfaceClient.AuthInfo, (byte)trkSoundVolume.Value);
            ManagementInterfaceClient.ManagementInterface.SetRecordVolume(ManagementInterfaceClient.AuthInfo, (byte)trkRecordVolume.Value);
            ManagementInterfaceClient.ManagementInterface.SetSpeechVolume(ManagementInterfaceClient.AuthInfo, (byte)trkSpeechVolume.Value);
            

            ManagementInterfaceClient.ManagementInterface.SIPPort = (int)numSIPPort.Value;
            ManagementInterfaceClient.ManagementInterface.UseInternalAddressForSIP = cbUseInternalIPForSIP.Checked;
            ManagementInterfaceClient.ManagementInterface.EnableSTUN = cbSTUN.Checked;
            ManagementInterfaceClient.ManagementInterface.STUNServer = txtSTUNServer.Text;
            ManagementInterfaceClient.ManagementInterface.SetBusyRedirectServer(ManagementInterfaceClient.AuthInfo, txtBusyRedirectServer.Text);

            ManagementInterfaceClient.ManagementInterface.LogStorage = (WOSI.CallButler.ManagementInterface.LogStorage)cboLogStorage.SelectedIndex;
            ManagementInterfaceClient.ManagementInterface.LogLevel = (WOSI.CallButler.ManagementInterface.LogLevel)cboLogLevel.SelectedIndex;
            ManagementInterfaceClient.ManagementInterface.SetSendLogErrorEmail(ManagementInterfaceClient.AuthInfo, cbSendLogErrorEmail.Checked);
            ManagementInterfaceClient.ManagementInterface.SetLogErrorEmail(ManagementInterfaceClient.AuthInfo, txtLogErrorEmail.Text);

            ManagementInterfaceClient.ManagementInterface.SetDefaultVoice(ManagementInterfaceClient.AuthInfo, cboDefaultVoice.Text);

            ManagementInterfaceClient.ManagementInterface.ReportErrors = chkSendErrorReports.Checked;


            // Check to make sure our management passwords match
            if (txtManagementPassword.Text != txtManagementConfirmPassword.Text)
            {
                MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_PasswordsDoNotMatch), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_PasswordMismatch), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtManagementPassword.Text = "";
                txtManagementConfirmPassword.Text = "";
                wizard.PageIndex = 0;
                txtManagementPassword.Select();
            }
            else if(txtManagementPassword.Text != blankPassword)
            {
                string password = txtManagementPassword.Text;

                ManagementInterfaceClient.ManagementInterface.SetManagementPassword(ManagementInterfaceClient.AuthInfo, password);
              
                ManagementInterfaceClient.Connect(ManagementInterfaceClient.CurrentServer, Properties.Settings.Default.TcpManagementPort, password);
            }

            // Save audio codec settings
            List<WOSI.CallButler.ManagementInterface.AudioCodecInformation> audioCodecs = new List<WOSI.CallButler.ManagementInterface.AudioCodecInformation>();

            for (int index = 0; index < lbCodecs.Items.Count; index++)
            {
                WOSI.CallButler.ManagementInterface.AudioCodecInformation acInfo = (WOSI.CallButler.ManagementInterface.AudioCodecInformation)lbCodecs.Items[index];

                acInfo.Enabled = lbCodecs.GetItemChecked(index);

                audioCodecs.Add(acInfo);
            }

            ManagementInterfaceClient.ManagementInterface.SetAudioCodecs(ManagementInterfaceClient.AuthInfo, audioCodecs.ToArray());

            Properties.Settings.Default.UpdateNotification = chkNotifyofProductVersions.Checked;

            btnApply.Enabled = false;
            btnCancel.Enabled = false;
            
            Properties.Settings.Default.Save();

            if (requiresRestart)
            {
                if (MessageBox.Show(this, "You have changed a setting that requires a restart of the CallButler Service. Would you like to do this now?", "Restart Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CallButler.Manager.Utils.ServiceUtils.RestartCallButlerService(ManagementInterfaceClient.CurrentServer);
                }

                requiresRestart = false;
                
                //Note: I really hate this, but the only way i can get the IpcChannel restart to work. Blah!
                
                string exmsg = "";
                int tries = 0;
                do
                {
                    try
                    {
                        ManagementInterfaceClient.Connect(ManagementInterfaceClient.CurrentServer, Properties.Settings.Default.TcpManagementPort, Properties.Settings.Default.ManagementPassword);
                        exmsg = "";
                    }
                    catch (Exception ex)
                    {
                        tries++;
                        if (ex.Message.Equals("Failed to write to an IPC Port: The pipe is being closed.\r\n"))
                        {
                            exmsg = ex.ToString();
                            
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                } while (exmsg.Length > 0 && tries < 5);
            }
            
        }

        
        private void SettingChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                Control c = sender as Control;
                if (c != null)
                {
                    if (c.Tag != null)
                    {
                        if (c.Tag.ToString().Equals("*"))
                        {
                            requiresRestart = true;
                        }
                    }
                }
            }
            btnApply.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void btnSendTestEmail_Click(object sender, EventArgs e)
        {
            Utils.EmailUtils.SendTestEmail(null, txtSMTPServer.Text, (int)numSMTPPort.Value, cbSMTPSSL.Checked, txtSMTPUsername.Text.Trim(), txtSMTPPassword.Text.Trim());
        }

        private void lbCodecs_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            SettingChanged(null, EventArgs.Empty);
        }

        private void lbCodecs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbCodecs.SelectedIndex == 0)
            {
                btnMoveCodecDown.Enabled = true;
                btnMoveCodecUp.Enabled = false;
            }
            else if (lbCodecs.SelectedIndex >= lbCodecs.Items.Count - 1)
            {
                btnMoveCodecDown.Enabled = false;
                btnMoveCodecUp.Enabled = true;
            }
            else
            {
                btnMoveCodecDown.Enabled = true;
                btnMoveCodecUp.Enabled = true;
            }
        }

        private void btnMoveCodecUp_Click(object sender, EventArgs e)
        {
            object item = lbCodecs.SelectedItem;
            bool chkd = lbCodecs.GetItemChecked(lbCodecs.SelectedIndex);
            int index = lbCodecs.Items.IndexOf(item);

            lbCodecs.Items.Remove(item);
            lbCodecs.Items.Insert(index - 1, item);

            lbCodecs.SelectedItem = item;
            lbCodecs.SetItemChecked(lbCodecs.SelectedIndex, chkd);

            SettingChanged(null, EventArgs.Empty);
        }

        private void btnMoveCodecDown_Click(object sender, EventArgs e)
        {
            object item = lbCodecs.SelectedItem;
            bool chkd = lbCodecs.GetItemChecked(lbCodecs.SelectedIndex);
            int index = lbCodecs.Items.IndexOf(item);

            lbCodecs.Items.Remove(item);
            lbCodecs.Items.Insert(index + 1, item);

            lbCodecs.SelectedItem = item;
            lbCodecs.SetItemChecked(lbCodecs.SelectedIndex, chkd);

            SettingChanged(null, EventArgs.Empty);
        }

        private void chkSendErrorReports_CheckedChanged(object sender, EventArgs e)
        {
            SettingChanged(null, EventArgs.Empty);
        }

        private void lblSSL_Click(object sender, EventArgs e)
        {
            cbSMTPSSL.Checked = !cbSMTPSSL.Checked;
        }

        private void lblSTUN_Click(object sender, EventArgs e)
        {
            cbSTUN.Checked = !cbSTUN.Checked;
        }

        private void lblErrors_Click(object sender, EventArgs e)
        {
            chkSendErrorReports.Checked = !chkSendErrorReports.Checked;
        }

        private void lblRemoteManagement_Click(object sender, EventArgs e)
        {
            cbEnableManagement.Checked = !cbEnableManagement.Checked;
        }

        private void cbEnableManagement_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnableManagement.Checked && txtManagementPassword.Text.Length == 0)
            {
                MessageBox.Show(this, "Enabling remote management without a password can expose your computer to potential security risks\r\nand allow unauthorized users to make changes to your CallButler system.\r\n\r\nTo cut down on these risks, please specify a management password.", "Management Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                wizard.PageIndex = 0;
                txtManagementPassword.Select();
            }

            SettingChanged(sender, EventArgs.Empty);
        }

        private void LoadHoldMusic()
        {
            // Load our on hold music
            lbMusic.Items.Clear();
            foreach (string musicFile in ManagementInterfaceClient.ManagementInterface.GetHoldMusic(ManagementInterfaceClient.AuthInfo))
            {
                global::Controls.ListBoxExItem lbItem = new global::Controls.ListBoxExItem();

                lbItem.Text = System.IO.Path.GetFileNameWithoutExtension(musicFile);
                lbItem.Tag = musicFile;
                lbItem.Image = Properties.Resources.music_16;

                lbMusic.Items.Add(lbItem);
            }
        }

        private void btnAddMusic_Click(object sender, EventArgs e)
        {
            if (openMusicDialog.ShowDialog(this) == DialogResult.OK)
            {
                global::Controls.LoadingDialog.ShowDialog(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.SettingsView_UploadingMusic), Properties.Resources.loading, false, 1000);

                foreach (string filename in openMusicDialog.FileNames)
                {
                    ManagementInterfaceClient.ManagementInterface.PersistHoldMusic(ManagementInterfaceClient.AuthInfo, System.IO.Path.GetFileName(filename), WOSI.Utilities.FileUtils.GetFileBytes(filename));
                }

                global::Controls.LoadingDialog.HideDialog();
                LoadHoldMusic();
            }
        }

        private void lbMusic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbMusic.SelectedItems.Count > 0)
                btnRemoveMusic.Enabled = true;
            else
                btnRemoveMusic.Enabled = false;
        }

        private void btnRemoveMusic_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MusicSettings_ConfirmRemoveMusic), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (global::Controls.ListBoxExItem lbItem in lbMusic.SelectedItems)
                {
                    ManagementInterfaceClient.ManagementInterface.DeleteHoldMusic(ManagementInterfaceClient.AuthInfo, (string)lbItem.Tag);
                }

                LoadHoldMusic();
            }
        }

        private void cbSendLogErrorEmail_CheckedChanged(object sender, EventArgs e)
        {
            pnlLogErrorEmail.Enabled = cbSendLogErrorEmail.Checked;
            SettingChanged(sender, e);
        }
    }
}

