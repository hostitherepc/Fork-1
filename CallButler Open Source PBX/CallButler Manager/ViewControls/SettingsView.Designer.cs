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



namespace CallButler.Manager.ViewControls
{
    partial class SettingsView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsView));
            this.wizard = new global::Controls.Wizard.Wizard();
            this.pgSecurity = new global::Controls.Wizard.WizardPage();
            this.lblRemoteManagement = new global::Controls.SmoothLabel();
            this.cbEnableManagement = new System.Windows.Forms.CheckBox();
            this.smoothLabel13 = new global::Controls.SmoothLabel();
            this.txtManagementConfirmPassword = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtManagementPassword = new System.Windows.Forms.TextBox();
            this.header4 = new global::Controls.Wizard.Header();
            this.pgEmailSettings = new global::Controls.Wizard.WizardPage();
            this.txtFromEmailAddress = new System.Windows.Forms.TextBox();
            this.smoothLabel17 = new global::Controls.SmoothLabel();
            this.lblSSL = new global::Controls.SmoothLabel();
            this.smoothLabel4 = new global::Controls.SmoothLabel();
            this.smoothLabel2 = new global::Controls.SmoothLabel();
            this.smoothLabel1 = new global::Controls.SmoothLabel();
            this.smoothLabel3 = new global::Controls.SmoothLabel();
            this.btnSendTestEmail = new global::Controls.LinkButton();
            this.cbSMTPSSL = new System.Windows.Forms.CheckBox();
            this.numSMTPPort = new System.Windows.Forms.NumericUpDown();
            this.txtSMTPPassword = new System.Windows.Forms.TextBox();
            this.txtSMTPUsername = new System.Windows.Forms.TextBox();
            this.txtSMTPServer = new System.Windows.Forms.TextBox();
            this.header1 = new global::Controls.Wizard.Header();
            this.pgMusicSettings = new global::Controls.Wizard.WizardPage();
            this.btnAddMusic = new global::Controls.LinkButton();
            this.btnRemoveMusic = new global::Controls.LinkButton();
            this.lbMusic = new global::Controls.ListBoxEx();
            this.header5 = new global::Controls.Wizard.Header();
            this.pgSpeechSettings = new global::Controls.Wizard.WizardPage();
            this.smoothLabel5 = new global::Controls.SmoothLabel();
            this.cboDefaultVoice = new System.Windows.Forms.ComboBox();
            this.header7 = new global::Controls.Wizard.Header();
            this.pgAudioSettings = new global::Controls.Wizard.WizardPage();
            this.smoothLabel8 = new global::Controls.SmoothLabel();
            this.smoothLabel7 = new global::Controls.SmoothLabel();
            this.smoothLabel6 = new global::Controls.SmoothLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.trkSpeechVolume = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.trkRecordVolume = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.trkSoundVolume = new System.Windows.Forms.TrackBar();
            this.header2 = new global::Controls.Wizard.Header();
            this.pgCodecSettings = new global::Controls.Wizard.WizardPage();
            this.btnMoveCodecDown = new global::Controls.LinkButton();
            this.btnMoveCodecUp = new global::Controls.LinkButton();
            this.lbCodecs = new System.Windows.Forms.CheckedListBox();
            this.label19 = new System.Windows.Forms.Label();
            this.header9 = new global::Controls.Wizard.Header();
            this.pgNetworkSettings = new global::Controls.Wizard.WizardPage();
            this.txtBusyRedirectServer = new System.Windows.Forms.TextBox();
            this.lblBusyRedirectServer = new global::Controls.SmoothLabel();
            this.smoothLabel15 = new global::Controls.SmoothLabel();
            this.cbUseInternalIPForSIP = new System.Windows.Forms.CheckBox();
            this.lblSTUN = new global::Controls.SmoothLabel();
            this.smoothLabel14 = new global::Controls.SmoothLabel();
            this.smoothLabel9 = new global::Controls.SmoothLabel();
            this.pnlSTUN = new System.Windows.Forms.Panel();
            this.smoothLabel10 = new global::Controls.SmoothLabel();
            this.txtSTUNServer = new System.Windows.Forms.TextBox();
            this.cbSTUN = new System.Windows.Forms.CheckBox();
            this.numLineCount = new System.Windows.Forms.NumericUpDown();
            this.numSIPPort = new System.Windows.Forms.NumericUpDown();
            this.header3 = new global::Controls.Wizard.Header();
            this.pgUpdateSettings = new global::Controls.Wizard.WizardPage();
            this.chkNotifyofProductVersions = new System.Windows.Forms.CheckBox();
            this.header8 = new global::Controls.Wizard.Header();
            this.pgLoggingSettings = new global::Controls.Wizard.WizardPage();
            this.pnlLogErrorEmail = new System.Windows.Forms.Panel();
            this.txtLogErrorEmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.smoothLabel16 = new global::Controls.SmoothLabel();
            this.cbSendLogErrorEmail = new System.Windows.Forms.CheckBox();
            this.lblErrors = new global::Controls.SmoothLabel();
            this.smoothLabel12 = new global::Controls.SmoothLabel();
            this.smoothLabel11 = new global::Controls.SmoothLabel();
            this.chkSendErrorReports = new System.Windows.Forms.CheckBox();
            this.cboLogLevel = new System.Windows.Forms.ComboBox();
            this.cboLogStorage = new System.Windows.Forms.ComboBox();
            this.header6 = new global::Controls.Wizard.Header();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.openMusicDialog = new System.Windows.Forms.OpenFileDialog();
            this.wizard.SuspendLayout();
            this.pgSecurity.SuspendLayout();
            this.pgEmailSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSMTPPort)).BeginInit();
            this.pgMusicSettings.SuspendLayout();
            this.pgSpeechSettings.SuspendLayout();
            this.pgAudioSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpeechVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRecordVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSoundVolume)).BeginInit();
            this.pgCodecSettings.SuspendLayout();
            this.pgNetworkSettings.SuspendLayout();
            this.pnlSTUN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLineCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSIPPort)).BeginInit();
            this.pgUpdateSettings.SuspendLayout();
            this.pgLoggingSettings.SuspendLayout();
            this.pnlLogErrorEmail.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard
            // 
            this.wizard.AlwaysShowFinishButton = false;
            this.wizard.BackColor = System.Drawing.Color.WhiteSmoke;
            this.wizard.CloseOnCancel = false;
            this.wizard.CloseOnFinish = false;
            this.wizard.Controls.Add(this.pgNetworkSettings);
            this.wizard.Controls.Add(this.pgUpdateSettings);
            this.wizard.Controls.Add(this.pgLoggingSettings);
            this.wizard.Controls.Add(this.pgCodecSettings);
            this.wizard.Controls.Add(this.pgAudioSettings);
            this.wizard.Controls.Add(this.pgSpeechSettings);
            this.wizard.Controls.Add(this.pgMusicSettings);
            this.wizard.Controls.Add(this.pgEmailSettings);
            this.wizard.Controls.Add(this.pgSecurity);
            this.wizard.DisplayButtons = false;
            resources.ApplyResources(this.wizard, "wizard");
            this.wizard.Name = "wizard";
            this.wizard.PageIndex = 6;
            this.wizard.Pages.AddRange(new global::Controls.Wizard.WizardPage[] {
            this.pgSecurity,
            this.pgEmailSettings,
            this.pgMusicSettings,
            this.pgSpeechSettings,
            this.pgAudioSettings,
            this.pgCodecSettings,
            this.pgNetworkSettings,
            this.pgUpdateSettings,
            this.pgLoggingSettings});
            this.wizard.ShowTabs = true;
            this.wizard.TabBackColor = System.Drawing.Color.WhiteSmoke;
            this.wizard.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.wizard.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.SingleLine;
            // 
            // pgSecurity
            // 
            this.pgSecurity.Controls.Add(this.lblRemoteManagement);
            this.pgSecurity.Controls.Add(this.cbEnableManagement);
            this.pgSecurity.Controls.Add(this.smoothLabel13);
            this.pgSecurity.Controls.Add(this.txtManagementConfirmPassword);
            this.pgSecurity.Controls.Add(this.label14);
            this.pgSecurity.Controls.Add(this.txtManagementPassword);
            this.pgSecurity.Controls.Add(this.header4);
            resources.ApplyResources(this.pgSecurity, "pgSecurity");
            this.pgSecurity.Icon = global::CallButler.Manager.Properties.Resources.lock_16;
            this.pgSecurity.IsFinishPage = false;
            this.pgSecurity.Name = "pgSecurity";
            this.pgSecurity.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // lblRemoteManagement
            // 
            this.lblRemoteManagement.AntiAliasText = false;
            resources.ApplyResources(this.lblRemoteManagement, "lblRemoteManagement");
            this.lblRemoteManagement.BackColor = System.Drawing.Color.Transparent;
            this.lblRemoteManagement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblRemoteManagement.EnableHelp = true;
            this.lblRemoteManagement.Name = "lblRemoteManagement";
            this.lblRemoteManagement.Click += new System.EventHandler(this.lblRemoteManagement_Click);
            // 
            // cbEnableManagement
            // 
            resources.ApplyResources(this.cbEnableManagement, "cbEnableManagement");
            this.cbEnableManagement.Name = "cbEnableManagement";
            this.cbEnableManagement.UseVisualStyleBackColor = true;
            this.cbEnableManagement.CheckedChanged += new System.EventHandler(this.cbEnableManagement_CheckedChanged);
            // 
            // smoothLabel13
            // 
            this.smoothLabel13.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel13, "smoothLabel13");
            this.smoothLabel13.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel13.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel13.EnableHelp = true;
            this.smoothLabel13.Name = "smoothLabel13";
            // 
            // txtManagementConfirmPassword
            // 
            resources.ApplyResources(this.txtManagementConfirmPassword, "txtManagementConfirmPassword");
            this.txtManagementConfirmPassword.Name = "txtManagementConfirmPassword";
            this.txtManagementConfirmPassword.UseSystemPasswordChar = true;
            this.txtManagementConfirmPassword.TextChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // txtManagementPassword
            // 
            resources.ApplyResources(this.txtManagementPassword, "txtManagementPassword");
            this.txtManagementPassword.Name = "txtManagementPassword";
            this.txtManagementPassword.UseSystemPasswordChar = true;
            this.txtManagementPassword.TextChanged += new System.EventHandler(this.SettingChanged);
            // 
            // header4
            // 
            this.header4.CausesValidation = false;
            resources.ApplyResources(this.header4, "header4");
            this.header4.Image = global::CallButler.Manager.Properties.Resources.lock_32_shadow;
            this.header4.Name = "header4";
            // 
            // pgEmailSettings
            // 
            this.pgEmailSettings.Controls.Add(this.txtFromEmailAddress);
            this.pgEmailSettings.Controls.Add(this.smoothLabel17);
            this.pgEmailSettings.Controls.Add(this.lblSSL);
            this.pgEmailSettings.Controls.Add(this.smoothLabel4);
            this.pgEmailSettings.Controls.Add(this.smoothLabel2);
            this.pgEmailSettings.Controls.Add(this.smoothLabel1);
            this.pgEmailSettings.Controls.Add(this.smoothLabel3);
            this.pgEmailSettings.Controls.Add(this.btnSendTestEmail);
            this.pgEmailSettings.Controls.Add(this.cbSMTPSSL);
            this.pgEmailSettings.Controls.Add(this.numSMTPPort);
            this.pgEmailSettings.Controls.Add(this.txtSMTPPassword);
            this.pgEmailSettings.Controls.Add(this.txtSMTPUsername);
            this.pgEmailSettings.Controls.Add(this.txtSMTPServer);
            this.pgEmailSettings.Controls.Add(this.header1);
            resources.ApplyResources(this.pgEmailSettings, "pgEmailSettings");
            this.pgEmailSettings.Icon = global::CallButler.Manager.Properties.Resources.mail_earth_16;
            this.pgEmailSettings.IsFinishPage = false;
            this.pgEmailSettings.Name = "pgEmailSettings";
            this.pgEmailSettings.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // txtFromEmailAddress
            // 
            resources.ApplyResources(this.txtFromEmailAddress, "txtFromEmailAddress");
            this.txtFromEmailAddress.Name = "txtFromEmailAddress";
            this.txtFromEmailAddress.TextChanged += new System.EventHandler(this.SettingChanged);
            // 
            // smoothLabel17
            // 
            this.smoothLabel17.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel17, "smoothLabel17");
            this.smoothLabel17.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel17.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel17.EnableHelp = true;
            this.smoothLabel17.Name = "smoothLabel17";
            // 
            // lblSSL
            // 
            this.lblSSL.AntiAliasText = false;
            resources.ApplyResources(this.lblSSL, "lblSSL");
            this.lblSSL.BackColor = System.Drawing.Color.Transparent;
            this.lblSSL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSSL.EnableHelp = true;
            this.lblSSL.Name = "lblSSL";
            this.lblSSL.Click += new System.EventHandler(this.lblSSL_Click);
            // 
            // smoothLabel4
            // 
            this.smoothLabel4.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel4, "smoothLabel4");
            this.smoothLabel4.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel4.EnableHelp = true;
            this.smoothLabel4.Name = "smoothLabel4";
            // 
            // smoothLabel2
            // 
            this.smoothLabel2.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel2, "smoothLabel2");
            this.smoothLabel2.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel2.EnableHelp = true;
            this.smoothLabel2.Name = "smoothLabel2";
            // 
            // smoothLabel1
            // 
            this.smoothLabel1.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel1, "smoothLabel1");
            this.smoothLabel1.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel1.EnableHelp = true;
            this.smoothLabel1.Name = "smoothLabel1";
            // 
            // smoothLabel3
            // 
            this.smoothLabel3.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel3, "smoothLabel3");
            this.smoothLabel3.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel3.EnableHelp = true;
            this.smoothLabel3.Name = "smoothLabel3";
            // 
            // btnSendTestEmail
            // 
            this.btnSendTestEmail.AntiAliasText = false;
            this.btnSendTestEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendTestEmail.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSendTestEmail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendTestEmail.LinkImage = global::CallButler.Manager.Properties.Resources.mail_earth_16;
            resources.ApplyResources(this.btnSendTestEmail, "btnSendTestEmail");
            this.btnSendTestEmail.Name = "btnSendTestEmail";
            this.btnSendTestEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendTestEmail.UnderlineOnHover = true;
            this.btnSendTestEmail.Click += new System.EventHandler(this.btnSendTestEmail_Click);
            // 
            // cbSMTPSSL
            // 
            resources.ApplyResources(this.cbSMTPSSL, "cbSMTPSSL");
            this.cbSMTPSSL.Name = "cbSMTPSSL";
            this.cbSMTPSSL.UseVisualStyleBackColor = true;
            this.cbSMTPSSL.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // numSMTPPort
            // 
            resources.ApplyResources(this.numSMTPPort, "numSMTPPort");
            this.numSMTPPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numSMTPPort.Name = "numSMTPPort";
            this.numSMTPPort.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numSMTPPort.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // txtSMTPPassword
            // 
            resources.ApplyResources(this.txtSMTPPassword, "txtSMTPPassword");
            this.txtSMTPPassword.Name = "txtSMTPPassword";
            this.txtSMTPPassword.UseSystemPasswordChar = true;
            this.txtSMTPPassword.TextChanged += new System.EventHandler(this.SettingChanged);
            // 
            // txtSMTPUsername
            // 
            resources.ApplyResources(this.txtSMTPUsername, "txtSMTPUsername");
            this.txtSMTPUsername.Name = "txtSMTPUsername";
            this.txtSMTPUsername.TextChanged += new System.EventHandler(this.SettingChanged);
            // 
            // txtSMTPServer
            // 
            resources.ApplyResources(this.txtSMTPServer, "txtSMTPServer");
            this.txtSMTPServer.Name = "txtSMTPServer";
            this.txtSMTPServer.TextChanged += new System.EventHandler(this.SettingChanged);
            // 
            // header1
            // 
            this.header1.CausesValidation = false;
            resources.ApplyResources(this.header1, "header1");
            this.header1.Image = global::CallButler.Manager.Properties.Resources.mail_earth_32_shadow;
            this.header1.Name = "header1";
            // 
            // pgMusicSettings
            // 
            this.pgMusicSettings.Controls.Add(this.btnAddMusic);
            this.pgMusicSettings.Controls.Add(this.btnRemoveMusic);
            this.pgMusicSettings.Controls.Add(this.lbMusic);
            this.pgMusicSettings.Controls.Add(this.header5);
            resources.ApplyResources(this.pgMusicSettings, "pgMusicSettings");
            this.pgMusicSettings.Icon = global::CallButler.Manager.Properties.Resources.music_16;
            this.pgMusicSettings.IsFinishPage = false;
            this.pgMusicSettings.Name = "pgMusicSettings";
            this.pgMusicSettings.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // btnAddMusic
            // 
            resources.ApplyResources(this.btnAddMusic, "btnAddMusic");
            this.btnAddMusic.AntiAliasText = false;
            this.btnAddMusic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddMusic.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAddMusic.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddMusic.LinkImage = global::CallButler.Manager.Properties.Resources.music_16;
            this.btnAddMusic.Name = "btnAddMusic";
            this.btnAddMusic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddMusic.UnderlineOnHover = true;
            this.btnAddMusic.Click += new System.EventHandler(this.btnAddMusic_Click);
            // 
            // btnRemoveMusic
            // 
            resources.ApplyResources(this.btnRemoveMusic, "btnRemoveMusic");
            this.btnRemoveMusic.AntiAliasText = false;
            this.btnRemoveMusic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemoveMusic.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnRemoveMusic.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveMusic.LinkImage = global::CallButler.Manager.Properties.Resources.delete_16;
            this.btnRemoveMusic.Name = "btnRemoveMusic";
            this.btnRemoveMusic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveMusic.UnderlineOnHover = true;
            this.btnRemoveMusic.Click += new System.EventHandler(this.btnRemoveMusic_Click);
            // 
            // lbMusic
            // 
            resources.ApplyResources(this.lbMusic, "lbMusic");
            this.lbMusic.AntiAliasText = false;
            this.lbMusic.BorderColor = System.Drawing.Color.Gray;
            this.lbMusic.CaptionColor = System.Drawing.Color.Silver;
            this.lbMusic.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMusic.DrawBorder = false;
            this.lbMusic.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbMusic.FormattingEnabled = true;
            this.lbMusic.ItemMargin = 5;
            this.lbMusic.Name = "lbMusic";
            this.lbMusic.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(225)))), ((int)(((byte)(244)))));
            this.lbMusic.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbMusic.SelectedIndexChanged += new System.EventHandler(this.lbMusic_SelectedIndexChanged);
            // 
            // header5
            // 
            this.header5.CausesValidation = false;
            resources.ApplyResources(this.header5, "header5");
            this.header5.Image = global::CallButler.Manager.Properties.Resources.music_32_shadow;
            this.header5.Name = "header5";
            // 
            // pgSpeechSettings
            // 
            this.pgSpeechSettings.Controls.Add(this.smoothLabel5);
            this.pgSpeechSettings.Controls.Add(this.cboDefaultVoice);
            this.pgSpeechSettings.Controls.Add(this.header7);
            resources.ApplyResources(this.pgSpeechSettings, "pgSpeechSettings");
            this.pgSpeechSettings.Icon = global::CallButler.Manager.Properties.Resources.text_loudspeaker_16;
            this.pgSpeechSettings.IsFinishPage = false;
            this.pgSpeechSettings.Name = "pgSpeechSettings";
            this.pgSpeechSettings.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // smoothLabel5
            // 
            this.smoothLabel5.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel5, "smoothLabel5");
            this.smoothLabel5.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel5.EnableHelp = true;
            this.smoothLabel5.Name = "smoothLabel5";
            // 
            // cboDefaultVoice
            // 
            this.cboDefaultVoice.FormattingEnabled = true;
            resources.ApplyResources(this.cboDefaultVoice, "cboDefaultVoice");
            this.cboDefaultVoice.Name = "cboDefaultVoice";
            this.cboDefaultVoice.SelectedIndexChanged += new System.EventHandler(this.SettingChanged);
            // 
            // header7
            // 
            this.header7.CausesValidation = false;
            resources.ApplyResources(this.header7, "header7");
            this.header7.Image = global::CallButler.Manager.Properties.Resources.text_loudspeaker_32_shadow;
            this.header7.Name = "header7";
            // 
            // pgAudioSettings
            // 
            this.pgAudioSettings.Controls.Add(this.smoothLabel8);
            this.pgAudioSettings.Controls.Add(this.smoothLabel7);
            this.pgAudioSettings.Controls.Add(this.smoothLabel6);
            this.pgAudioSettings.Controls.Add(this.label9);
            this.pgAudioSettings.Controls.Add(this.trkSpeechVolume);
            this.pgAudioSettings.Controls.Add(this.label5);
            this.pgAudioSettings.Controls.Add(this.trkRecordVolume);
            this.pgAudioSettings.Controls.Add(this.label3);
            this.pgAudioSettings.Controls.Add(this.trkSoundVolume);
            this.pgAudioSettings.Controls.Add(this.header2);
            resources.ApplyResources(this.pgAudioSettings, "pgAudioSettings");
            this.pgAudioSettings.Icon = global::CallButler.Manager.Properties.Resources.loudspeaker_16;
            this.pgAudioSettings.IsFinishPage = false;
            this.pgAudioSettings.Name = "pgAudioSettings";
            this.pgAudioSettings.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // smoothLabel8
            // 
            this.smoothLabel8.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel8, "smoothLabel8");
            this.smoothLabel8.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel8.EnableHelp = true;
            this.smoothLabel8.Name = "smoothLabel8";
            // 
            // smoothLabel7
            // 
            this.smoothLabel7.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel7, "smoothLabel7");
            this.smoothLabel7.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel7.EnableHelp = true;
            this.smoothLabel7.Name = "smoothLabel7";
            // 
            // smoothLabel6
            // 
            this.smoothLabel6.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel6, "smoothLabel6");
            this.smoothLabel6.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel6.EnableHelp = true;
            this.smoothLabel6.Name = "smoothLabel6";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // trkSpeechVolume
            // 
            resources.ApplyResources(this.trkSpeechVolume, "trkSpeechVolume");
            this.trkSpeechVolume.Maximum = 255;
            this.trkSpeechVolume.Name = "trkSpeechVolume";
            this.trkSpeechVolume.TickFrequency = 5;
            this.trkSpeechVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trkSpeechVolume.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // trkRecordVolume
            // 
            resources.ApplyResources(this.trkRecordVolume, "trkRecordVolume");
            this.trkRecordVolume.Maximum = 255;
            this.trkRecordVolume.Name = "trkRecordVolume";
            this.trkRecordVolume.TickFrequency = 5;
            this.trkRecordVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trkRecordVolume.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // trkSoundVolume
            // 
            resources.ApplyResources(this.trkSoundVolume, "trkSoundVolume");
            this.trkSoundVolume.Maximum = 255;
            this.trkSoundVolume.Name = "trkSoundVolume";
            this.trkSoundVolume.TickFrequency = 5;
            this.trkSoundVolume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trkSoundVolume.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // header2
            // 
            this.header2.CausesValidation = false;
            resources.ApplyResources(this.header2, "header2");
            this.header2.Image = global::CallButler.Manager.Properties.Resources.loudspeaker_32_shadow;
            this.header2.Name = "header2";
            // 
            // pgCodecSettings
            // 
            this.pgCodecSettings.Controls.Add(this.btnMoveCodecDown);
            this.pgCodecSettings.Controls.Add(this.btnMoveCodecUp);
            this.pgCodecSettings.Controls.Add(this.lbCodecs);
            this.pgCodecSettings.Controls.Add(this.label19);
            this.pgCodecSettings.Controls.Add(this.header9);
            resources.ApplyResources(this.pgCodecSettings, "pgCodecSettings");
            this.pgCodecSettings.Icon = global::CallButler.Manager.Properties.Resources.oszillograph_16;
            this.pgCodecSettings.IsFinishPage = false;
            this.pgCodecSettings.Name = "pgCodecSettings";
            this.pgCodecSettings.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // btnMoveCodecDown
            // 
            this.btnMoveCodecDown.AntiAliasText = false;
            this.btnMoveCodecDown.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnMoveCodecDown, "btnMoveCodecDown");
            this.btnMoveCodecDown.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnMoveCodecDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMoveCodecDown.LinkImage = global::CallButler.Manager.Properties.Resources.arrow_down_blue_16;
            this.btnMoveCodecDown.Name = "btnMoveCodecDown";
            this.btnMoveCodecDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMoveCodecDown.UnderlineOnHover = true;
            this.btnMoveCodecDown.Click += new System.EventHandler(this.btnMoveCodecDown_Click);
            // 
            // btnMoveCodecUp
            // 
            this.btnMoveCodecUp.AntiAliasText = false;
            this.btnMoveCodecUp.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnMoveCodecUp, "btnMoveCodecUp");
            this.btnMoveCodecUp.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnMoveCodecUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMoveCodecUp.LinkImage = global::CallButler.Manager.Properties.Resources.arrow_up_blue_16;
            this.btnMoveCodecUp.Name = "btnMoveCodecUp";
            this.btnMoveCodecUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMoveCodecUp.UnderlineOnHover = true;
            this.btnMoveCodecUp.Click += new System.EventHandler(this.btnMoveCodecUp_Click);
            // 
            // lbCodecs
            // 
            this.lbCodecs.FormattingEnabled = true;
            resources.ApplyResources(this.lbCodecs, "lbCodecs");
            this.lbCodecs.Name = "lbCodecs";
            this.lbCodecs.SelectedIndexChanged += new System.EventHandler(this.lbCodecs_SelectedIndexChanged);
            this.lbCodecs.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lbCodecs_ItemCheck);
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // header9
            // 
            this.header9.CausesValidation = false;
            resources.ApplyResources(this.header9, "header9");
            this.header9.Image = global::CallButler.Manager.Properties.Resources.oszillograph_32_shadow;
            this.header9.Name = "header9";
            // 
            // pgNetworkSettings
            // 
            this.pgNetworkSettings.Controls.Add(this.txtBusyRedirectServer);
            this.pgNetworkSettings.Controls.Add(this.lblBusyRedirectServer);
            this.pgNetworkSettings.Controls.Add(this.smoothLabel15);
            this.pgNetworkSettings.Controls.Add(this.cbUseInternalIPForSIP);
            this.pgNetworkSettings.Controls.Add(this.lblSTUN);
            this.pgNetworkSettings.Controls.Add(this.smoothLabel14);
            this.pgNetworkSettings.Controls.Add(this.smoothLabel9);
            this.pgNetworkSettings.Controls.Add(this.pnlSTUN);
            this.pgNetworkSettings.Controls.Add(this.cbSTUN);
            this.pgNetworkSettings.Controls.Add(this.numLineCount);
            this.pgNetworkSettings.Controls.Add(this.numSIPPort);
            this.pgNetworkSettings.Controls.Add(this.header3);
            resources.ApplyResources(this.pgNetworkSettings, "pgNetworkSettings");
            this.pgNetworkSettings.Icon = global::CallButler.Manager.Properties.Resources.earth_network_16;
            this.pgNetworkSettings.IsFinishPage = false;
            this.pgNetworkSettings.Name = "pgNetworkSettings";
            this.pgNetworkSettings.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // txtBusyRedirectServer
            // 
            resources.ApplyResources(this.txtBusyRedirectServer, "txtBusyRedirectServer");
            this.txtBusyRedirectServer.Name = "txtBusyRedirectServer";
            this.txtBusyRedirectServer.TextChanged += new System.EventHandler(this.SettingChanged);
            // 
            // lblBusyRedirectServer
            // 
            this.lblBusyRedirectServer.AntiAliasText = false;
            resources.ApplyResources(this.lblBusyRedirectServer, "lblBusyRedirectServer");
            this.lblBusyRedirectServer.BackColor = System.Drawing.Color.Transparent;
            this.lblBusyRedirectServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBusyRedirectServer.EnableHelp = true;
            this.lblBusyRedirectServer.Name = "lblBusyRedirectServer";
            // 
            // smoothLabel15
            // 
            this.smoothLabel15.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel15, "smoothLabel15");
            this.smoothLabel15.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel15.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel15.EnableHelp = true;
            this.smoothLabel15.Name = "smoothLabel15";
            // 
            // cbUseInternalIPForSIP
            // 
            resources.ApplyResources(this.cbUseInternalIPForSIP, "cbUseInternalIPForSIP");
            this.cbUseInternalIPForSIP.Name = "cbUseInternalIPForSIP";
            this.cbUseInternalIPForSIP.UseVisualStyleBackColor = true;
            this.cbUseInternalIPForSIP.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // lblSTUN
            // 
            this.lblSTUN.AntiAliasText = false;
            resources.ApplyResources(this.lblSTUN, "lblSTUN");
            this.lblSTUN.BackColor = System.Drawing.Color.Transparent;
            this.lblSTUN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSTUN.EnableHelp = true;
            this.lblSTUN.Name = "lblSTUN";
            this.lblSTUN.Click += new System.EventHandler(this.lblSTUN_Click);
            // 
            // smoothLabel14
            // 
            this.smoothLabel14.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel14, "smoothLabel14");
            this.smoothLabel14.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel14.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel14.EnableHelp = true;
            this.smoothLabel14.Name = "smoothLabel14";
            // 
            // smoothLabel9
            // 
            this.smoothLabel9.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel9, "smoothLabel9");
            this.smoothLabel9.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel9.EnableHelp = true;
            this.smoothLabel9.Name = "smoothLabel9";
            // 
            // pnlSTUN
            // 
            this.pnlSTUN.Controls.Add(this.smoothLabel10);
            this.pnlSTUN.Controls.Add(this.txtSTUNServer);
            resources.ApplyResources(this.pnlSTUN, "pnlSTUN");
            this.pnlSTUN.Name = "pnlSTUN";
            // 
            // smoothLabel10
            // 
            this.smoothLabel10.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel10, "smoothLabel10");
            this.smoothLabel10.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel10.EnableHelp = true;
            this.smoothLabel10.Name = "smoothLabel10";
            // 
            // txtSTUNServer
            // 
            resources.ApplyResources(this.txtSTUNServer, "txtSTUNServer");
            this.txtSTUNServer.Name = "txtSTUNServer";
            this.txtSTUNServer.TextChanged += new System.EventHandler(this.SettingChanged);
            // 
            // cbSTUN
            // 
            resources.ApplyResources(this.cbSTUN, "cbSTUN");
            this.cbSTUN.Name = "cbSTUN";
            this.cbSTUN.UseVisualStyleBackColor = true;
            this.cbSTUN.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // numLineCount
            // 
            resources.ApplyResources(this.numLineCount, "numLineCount");
            this.numLineCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLineCount.Name = "numLineCount";
            this.numLineCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numLineCount.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // numSIPPort
            // 
            resources.ApplyResources(this.numSIPPort, "numSIPPort");
            this.numSIPPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numSIPPort.Name = "numSIPPort";
            this.numSIPPort.Value = new decimal(new int[] {
            5060,
            0,
            0,
            0});
            this.numSIPPort.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // header3
            // 
            this.header3.CausesValidation = false;
            resources.ApplyResources(this.header3, "header3");
            this.header3.Image = global::CallButler.Manager.Properties.Resources.earth_network_32_shadow;
            this.header3.Name = "header3";
            // 
            // pgUpdateSettings
            // 
            this.pgUpdateSettings.Controls.Add(this.chkNotifyofProductVersions);
            this.pgUpdateSettings.Controls.Add(this.header8);
            resources.ApplyResources(this.pgUpdateSettings, "pgUpdateSettings");
            this.pgUpdateSettings.Icon = global::CallButler.Manager.Properties.Resources.import_16;
            this.pgUpdateSettings.IsFinishPage = false;
            this.pgUpdateSettings.Name = "pgUpdateSettings";
            this.pgUpdateSettings.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // chkNotifyofProductVersions
            // 
            resources.ApplyResources(this.chkNotifyofProductVersions, "chkNotifyofProductVersions");
            this.chkNotifyofProductVersions.Name = "chkNotifyofProductVersions";
            this.chkNotifyofProductVersions.UseVisualStyleBackColor = true;
            this.chkNotifyofProductVersions.CheckedChanged += new System.EventHandler(this.SettingChanged);
            // 
            // header8
            // 
            this.header8.CausesValidation = false;
            resources.ApplyResources(this.header8, "header8");
            this.header8.Image = global::CallButler.Manager.Properties.Resources.import1;
            this.header8.Name = "header8";
            // 
            // pgLoggingSettings
            // 
            this.pgLoggingSettings.Controls.Add(this.pnlLogErrorEmail);
            this.pgLoggingSettings.Controls.Add(this.smoothLabel16);
            this.pgLoggingSettings.Controls.Add(this.cbSendLogErrorEmail);
            this.pgLoggingSettings.Controls.Add(this.lblErrors);
            this.pgLoggingSettings.Controls.Add(this.smoothLabel12);
            this.pgLoggingSettings.Controls.Add(this.smoothLabel11);
            this.pgLoggingSettings.Controls.Add(this.chkSendErrorReports);
            this.pgLoggingSettings.Controls.Add(this.cboLogLevel);
            this.pgLoggingSettings.Controls.Add(this.cboLogStorage);
            this.pgLoggingSettings.Controls.Add(this.header6);
            resources.ApplyResources(this.pgLoggingSettings, "pgLoggingSettings");
            this.pgLoggingSettings.Icon = global::CallButler.Manager.Properties.Resources.cabinet_16;
            this.pgLoggingSettings.IsFinishPage = false;
            this.pgLoggingSettings.Name = "pgLoggingSettings";
            this.pgLoggingSettings.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // pnlLogErrorEmail
            // 
            this.pnlLogErrorEmail.Controls.Add(this.txtLogErrorEmail);
            this.pnlLogErrorEmail.Controls.Add(this.label1);
            resources.ApplyResources(this.pnlLogErrorEmail, "pnlLogErrorEmail");
            this.pnlLogErrorEmail.Name = "pnlLogErrorEmail";
            // 
            // txtLogErrorEmail
            // 
            resources.ApplyResources(this.txtLogErrorEmail, "txtLogErrorEmail");
            this.txtLogErrorEmail.Name = "txtLogErrorEmail";
            this.txtLogErrorEmail.TextChanged += new System.EventHandler(this.SettingChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // smoothLabel16
            // 
            this.smoothLabel16.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel16, "smoothLabel16");
            this.smoothLabel16.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel16.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel16.EnableHelp = true;
            this.smoothLabel16.Name = "smoothLabel16";
            // 
            // cbSendLogErrorEmail
            // 
            resources.ApplyResources(this.cbSendLogErrorEmail, "cbSendLogErrorEmail");
            this.cbSendLogErrorEmail.Name = "cbSendLogErrorEmail";
            this.cbSendLogErrorEmail.UseVisualStyleBackColor = true;
            this.cbSendLogErrorEmail.CheckedChanged += new System.EventHandler(this.cbSendLogErrorEmail_CheckedChanged);
            // 
            // lblErrors
            // 
            this.lblErrors.AntiAliasText = false;
            resources.ApplyResources(this.lblErrors, "lblErrors");
            this.lblErrors.BackColor = System.Drawing.Color.Transparent;
            this.lblErrors.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblErrors.EnableHelp = true;
            this.lblErrors.Name = "lblErrors";
            this.lblErrors.Click += new System.EventHandler(this.lblErrors_Click);
            // 
            // smoothLabel12
            // 
            this.smoothLabel12.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel12, "smoothLabel12");
            this.smoothLabel12.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel12.EnableHelp = true;
            this.smoothLabel12.Name = "smoothLabel12";
            // 
            // smoothLabel11
            // 
            this.smoothLabel11.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel11, "smoothLabel11");
            this.smoothLabel11.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel11.EnableHelp = true;
            this.smoothLabel11.Name = "smoothLabel11";
            // 
            // chkSendErrorReports
            // 
            resources.ApplyResources(this.chkSendErrorReports, "chkSendErrorReports");
            this.chkSendErrorReports.Name = "chkSendErrorReports";
            this.chkSendErrorReports.UseVisualStyleBackColor = true;
            this.chkSendErrorReports.CheckedChanged += new System.EventHandler(this.chkSendErrorReports_CheckedChanged);
            // 
            // cboLogLevel
            // 
            this.cboLogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLogLevel.FormattingEnabled = true;
            this.cboLogLevel.Items.AddRange(new object[] {
            resources.GetString("cboLogLevel.Items"),
            resources.GetString("cboLogLevel.Items1"),
            resources.GetString("cboLogLevel.Items2")});
            resources.ApplyResources(this.cboLogLevel, "cboLogLevel");
            this.cboLogLevel.Name = "cboLogLevel";
            this.cboLogLevel.SelectedIndexChanged += new System.EventHandler(this.SettingChanged);
            // 
            // cboLogStorage
            // 
            this.cboLogStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLogStorage.FormattingEnabled = true;
            this.cboLogStorage.Items.AddRange(new object[] {
            resources.GetString("cboLogStorage.Items"),
            resources.GetString("cboLogStorage.Items1"),
            resources.GetString("cboLogStorage.Items2")});
            resources.ApplyResources(this.cboLogStorage, "cboLogStorage");
            this.cboLogStorage.Name = "cboLogStorage";
            this.cboLogStorage.SelectedIndexChanged += new System.EventHandler(this.SettingChanged);
            // 
            // header6
            // 
            this.header6.CausesValidation = false;
            resources.ApplyResources(this.header6, "header6");
            this.header6.Image = global::CallButler.Manager.Properties.Resources.cabinet_32_shadow;
            this.header6.Name = "header6";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnApply);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // openMusicDialog
            // 
            resources.ApplyResources(this.openMusicDialog, "openMusicDialog");
            this.openMusicDialog.Multiselect = true;
            // 
            // SettingsView
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.wizard);
            this.Controls.Add(this.panel1);
            this.EnableHelpIcon = false;
            this.HeaderIcon = global::CallButler.Manager.Properties.Resources.nut_and_bolt_48_shadow;
            this.Name = "SettingsView";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.wizard, 0);
            this.wizard.ResumeLayout(false);
            this.pgSecurity.ResumeLayout(false);
            this.pgSecurity.PerformLayout();
            this.pgEmailSettings.ResumeLayout(false);
            this.pgEmailSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSMTPPort)).EndInit();
            this.pgMusicSettings.ResumeLayout(false);
            this.pgSpeechSettings.ResumeLayout(false);
            this.pgSpeechSettings.PerformLayout();
            this.pgAudioSettings.ResumeLayout(false);
            this.pgAudioSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpeechVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRecordVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSoundVolume)).EndInit();
            this.pgCodecSettings.ResumeLayout(false);
            this.pgNetworkSettings.ResumeLayout(false);
            this.pgNetworkSettings.PerformLayout();
            this.pnlSTUN.ResumeLayout(false);
            this.pnlSTUN.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLineCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSIPPort)).EndInit();
            this.pgUpdateSettings.ResumeLayout(false);
            this.pgUpdateSettings.PerformLayout();
            this.pgLoggingSettings.ResumeLayout(false);
            this.pgLoggingSettings.PerformLayout();
            this.pnlLogErrorEmail.ResumeLayout(false);
            this.pnlLogErrorEmail.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private global::Controls.Wizard.Wizard wizard;
        private global::Controls.Wizard.WizardPage pgEmailSettings;
        private global::Controls.Wizard.WizardPage pgAudioSettings;
        private global::Controls.Wizard.Header header1;
        private System.Windows.Forms.TextBox txtSMTPServer;
        private System.Windows.Forms.CheckBox cbSMTPSSL;
        private System.Windows.Forms.NumericUpDown numSMTPPort;
        private System.Windows.Forms.TextBox txtSMTPPassword;
        private System.Windows.Forms.TextBox txtSMTPUsername;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private global::Controls.Wizard.WizardPage pgNetworkSettings;
        private global::Controls.LinkButton btnSendTestEmail;
        private global::Controls.Wizard.Header header2;
        private System.Windows.Forms.TrackBar trkSoundVolume;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar trkSpeechVolume;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trkRecordVolume;
        private System.Windows.Forms.Label label3;
        private global::Controls.Wizard.Header header3;
        private global::Controls.Wizard.WizardPage pgLoggingSettings;
        private System.Windows.Forms.NumericUpDown numSIPPort;
        private System.Windows.Forms.CheckBox cbSTUN;
        private System.Windows.Forms.Panel pnlSTUN;
        private System.Windows.Forms.TextBox txtSTUNServer;
        private global::Controls.Wizard.WizardPage pgSecurity;
        private global::Controls.Wizard.Header header4;
        private System.Windows.Forms.TextBox txtManagementConfirmPassword;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtManagementPassword;
        private global::Controls.Wizard.WizardPage pgMusicSettings;
        private global::Controls.Wizard.Header header5;
        private global::Controls.Wizard.Header header6;
        private System.Windows.Forms.ComboBox cboLogStorage;
        private System.Windows.Forms.ComboBox cboLogLevel;
        private global::Controls.Wizard.WizardPage pgSpeechSettings;
        private global::Controls.Wizard.Header header7;
        private System.Windows.Forms.ComboBox cboDefaultVoice;
        private global::Controls.Wizard.WizardPage pgUpdateSettings;
        private global::Controls.Wizard.Header header8;
        private System.Windows.Forms.CheckBox chkNotifyofProductVersions;
        private global::Controls.Wizard.WizardPage pgCodecSettings;
        private global::Controls.Wizard.Header header9;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckedListBox lbCodecs;
        private global::Controls.LinkButton btnMoveCodecDown;
        private global::Controls.LinkButton btnMoveCodecUp;
        private System.Windows.Forms.CheckBox chkSendErrorReports;
        private global::Controls.SmoothLabel smoothLabel3;
        private global::Controls.SmoothLabel smoothLabel1;
        private global::Controls.SmoothLabel smoothLabel2;
        private global::Controls.SmoothLabel smoothLabel4;
        private global::Controls.SmoothLabel lblSSL;
        private global::Controls.SmoothLabel smoothLabel5;
        private global::Controls.SmoothLabel smoothLabel6;
        private global::Controls.SmoothLabel smoothLabel7;
        private global::Controls.SmoothLabel smoothLabel8;
        private global::Controls.SmoothLabel smoothLabel9;
        private global::Controls.SmoothLabel lblSTUN;
        private global::Controls.SmoothLabel smoothLabel10;
        private global::Controls.SmoothLabel smoothLabel11;
        private global::Controls.SmoothLabel smoothLabel12;
        private global::Controls.SmoothLabel lblErrors;
        private global::Controls.SmoothLabel smoothLabel13;
        private global::Controls.SmoothLabel lblRemoteManagement;
        private System.Windows.Forms.CheckBox cbEnableManagement;
        private global::Controls.SmoothLabel smoothLabel14;
        private System.Windows.Forms.NumericUpDown numLineCount;
        private global::Controls.SmoothLabel smoothLabel15;
        private System.Windows.Forms.CheckBox cbUseInternalIPForSIP;
        private global::Controls.ListBoxEx lbMusic;
        private global::Controls.LinkButton btnRemoveMusic;
        private global::Controls.LinkButton btnAddMusic;
        private System.Windows.Forms.OpenFileDialog openMusicDialog;
        private global::Controls.SmoothLabel smoothLabel16;
        private System.Windows.Forms.CheckBox cbSendLogErrorEmail;
        private System.Windows.Forms.Panel pnlLogErrorEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLogErrorEmail;
        private System.Windows.Forms.TextBox txtFromEmailAddress;
        private global::Controls.SmoothLabel smoothLabel17;
        private System.Windows.Forms.TextBox txtBusyRedirectServer;
        private global::Controls.SmoothLabel lblBusyRedirectServer;

    }
}
