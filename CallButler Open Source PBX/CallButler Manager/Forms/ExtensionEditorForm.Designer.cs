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



namespace CallButler.Manager.Forms
{
    partial class ExtensionEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtensionEditorForm));
            this.wizard = new global::Controls.Wizard.Wizard();
            this.pgGeneral = new global::Controls.Wizard.WizardPage();
            this.lblEnableOutbound = new global::Controls.SmoothLabel();
            this.cbEnableOutbound = new System.Windows.Forms.CheckBox();
            this.lblCallScreen = new global::Controls.SmoothLabel();
            this.cbCallScreening = new System.Windows.Forms.CheckBox();
            this.pnlHandoff = new System.Windows.Forms.Panel();
            this.lblHandoff = new global::Controls.SmoothLabel();
            this.cbHandOff = new System.Windows.Forms.CheckBox();
            this.lblDialByName = new global::Controls.SmoothLabel();
            this.smoothLabel3 = new global::Controls.SmoothLabel();
            this.smoothLabel2 = new global::Controls.SmoothLabel();
            this.smoothLabel1 = new global::Controls.SmoothLabel();
            this.numExtNum = new System.Windows.Forms.NumericUpDown();
            this.cbEnableSearch = new System.Windows.Forms.CheckBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.btnImportOutlook = new global::Controls.LinkButton();
            this.wizardHeader1 = new CallButler.Manager.Controls.WizardHeader();
            this.pgFindme = new global::Controls.Wizard.WizardPage();
            this.pnlFindMe = new System.Windows.Forms.Panel();
            this.wizardHeader3 = new CallButler.Manager.Controls.WizardHeader();
            this.pgVoicemailGreeting = new global::Controls.Wizard.WizardPage();
            this.wizardHeader6 = new CallButler.Manager.Controls.WizardHeader();
            this.greetingControl = new CallButler.Manager.Controls.GreetingControl(ManagementInterfaceClient.ManagementClient, ManagementInterfaceClient.AuthInfo);
            this.pgEmailSettings = new global::Controls.Wizard.WizardPage();
            this.lblEmail = new global::Controls.SmoothLabel();
            this.pnlEmailNotifications = new System.Windows.Forms.Panel();
            this.lblAttachment = new global::Controls.SmoothLabel();
            this.smoothLabel5 = new global::Controls.SmoothLabel();
            this.btnSendTestEmail = new global::Controls.LinkButton();
            this.cbAttach = new System.Windows.Forms.CheckBox();
            this.txtEmailAddress = new System.Windows.Forms.TextBox();
            this.cbEmailNotification = new System.Windows.Forms.CheckBox();
            this.wizardHeader4 = new CallButler.Manager.Controls.WizardHeader();
            this.pgManagement = new global::Controls.Wizard.WizardPage();
            this.pnlPBXPassword = new System.Windows.Forms.Panel();
            this.smoothLabel6 = new global::Controls.SmoothLabel();
            this.txtConfirmPBXPassword = new System.Windows.Forms.TextBox();
            this.txtPBXPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.smoothLabel4 = new global::Controls.SmoothLabel();
            this.lblRemoteManagement = new global::Controls.SmoothLabel();
            this.cbEnableManagement = new System.Windows.Forms.CheckBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.wizardHeader2 = new CallButler.Manager.Controls.WizardHeader();
            this.pgFinish = new global::Controls.Wizard.WizardPage();
            this.btnSetupInstructions = new global::Controls.LinkButton();
            this.label6 = new System.Windows.Forms.Label();
            this.wizardHeader5 = new CallButler.Manager.Controls.WizardHeader();
            this.wizard.SuspendLayout();
            this.pgGeneral.SuspendLayout();
            this.pnlHandoff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExtNum)).BeginInit();
            this.pgFindme.SuspendLayout();
            this.pgVoicemailGreeting.SuspendLayout();
            this.pgEmailSettings.SuspendLayout();
            this.pnlEmailNotifications.SuspendLayout();
            this.pgManagement.SuspendLayout();
            this.pnlPBXPassword.SuspendLayout();
            this.pgFinish.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard
            // 
            this.wizard.AlwaysShowFinishButton = true;
            this.wizard.CloseOnCancel = true;
            this.wizard.CloseOnFinish = false;
            this.wizard.Controls.Add(this.pgGeneral);
            this.wizard.Controls.Add(this.pgFindme);
            this.wizard.Controls.Add(this.pgVoicemailGreeting);
            this.wizard.Controls.Add(this.pgEmailSettings);
            this.wizard.Controls.Add(this.pgManagement);
            this.wizard.Controls.Add(this.pgFinish);
            this.wizard.DisplayButtons = true;
            resources.ApplyResources(this.wizard, "wizard");
            this.wizard.Name = "wizard";
            this.wizard.PageIndex = 0;
            this.wizard.Pages.AddRange(new global::Controls.Wizard.WizardPage[] {
            this.pgGeneral,
            this.pgFindme,
            this.pgVoicemailGreeting,
            this.pgEmailSettings,
            this.pgManagement,
            this.pgFinish});
            this.wizard.ShowTabs = true;
            this.wizard.TabBackColor = System.Drawing.Color.WhiteSmoke;
            this.wizard.TabBackgroundImage = global::CallButler.Manager.Properties.Resources.wizard_header;
            this.wizard.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.wizard.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.Cutout;
            this.wizard.WizardFinished += new System.EventHandler<global::Controls.Wizard.PageChangedEventArgs>(this.wizard_WizardFinished);
            // 
            // pgGeneral
            // 
            this.pgGeneral.Controls.Add(this.lblEnableOutbound);
            this.pgGeneral.Controls.Add(this.cbEnableOutbound);
            this.pgGeneral.Controls.Add(this.lblCallScreen);
            this.pgGeneral.Controls.Add(this.cbCallScreening);
            this.pgGeneral.Controls.Add(this.pnlHandoff);
            this.pgGeneral.Controls.Add(this.lblDialByName);
            this.pgGeneral.Controls.Add(this.smoothLabel3);
            this.pgGeneral.Controls.Add(this.smoothLabel2);
            this.pgGeneral.Controls.Add(this.smoothLabel1);
            this.pgGeneral.Controls.Add(this.numExtNum);
            this.pgGeneral.Controls.Add(this.cbEnableSearch);
            this.pgGeneral.Controls.Add(this.txtFirstName);
            this.pgGeneral.Controls.Add(this.txtLastName);
            this.pgGeneral.Controls.Add(this.btnImportOutlook);
            this.pgGeneral.Controls.Add(this.wizardHeader1);
            resources.ApplyResources(this.pgGeneral, "pgGeneral");
            this.pgGeneral.IsFinishPage = false;
            this.pgGeneral.Name = "pgGeneral";
            this.pgGeneral.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // lblEnableOutbound
            // 
            this.lblEnableOutbound.AntiAliasText = false;
            resources.ApplyResources(this.lblEnableOutbound, "lblEnableOutbound");
            this.lblEnableOutbound.BackColor = System.Drawing.Color.Transparent;
            this.lblEnableOutbound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblEnableOutbound.EnableHelp = true;
            this.lblEnableOutbound.Name = "lblEnableOutbound";
            this.lblEnableOutbound.Click += new System.EventHandler(this.lblEnableOutbound_Click);
            // 
            // cbEnableOutbound
            // 
            resources.ApplyResources(this.cbEnableOutbound, "cbEnableOutbound");
            this.cbEnableOutbound.Name = "cbEnableOutbound";
            this.cbEnableOutbound.UseVisualStyleBackColor = true;
            // 
            // lblCallScreen
            // 
            this.lblCallScreen.AntiAliasText = false;
            resources.ApplyResources(this.lblCallScreen, "lblCallScreen");
            this.lblCallScreen.BackColor = System.Drawing.Color.Transparent;
            this.lblCallScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCallScreen.EnableHelp = true;
            this.lblCallScreen.Name = "lblCallScreen";
            this.lblCallScreen.Click += new System.EventHandler(this.lblCallScreen_Click);
            // 
            // cbCallScreening
            // 
            resources.ApplyResources(this.cbCallScreening, "cbCallScreening");
            this.cbCallScreening.Name = "cbCallScreening";
            this.cbCallScreening.UseVisualStyleBackColor = true;
            // 
            // pnlHandoff
            // 
            this.pnlHandoff.Controls.Add(this.lblHandoff);
            this.pnlHandoff.Controls.Add(this.cbHandOff);
            resources.ApplyResources(this.pnlHandoff, "pnlHandoff");
            this.pnlHandoff.Name = "pnlHandoff";
            // 
            // lblHandoff
            // 
            this.lblHandoff.AntiAliasText = false;
            resources.ApplyResources(this.lblHandoff, "lblHandoff");
            this.lblHandoff.BackColor = System.Drawing.Color.Transparent;
            this.lblHandoff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHandoff.EnableHelp = true;
            this.lblHandoff.Name = "lblHandoff";
            this.lblHandoff.Click += new System.EventHandler(this.lblHandoff_Click);
            // 
            // cbHandOff
            // 
            resources.ApplyResources(this.cbHandOff, "cbHandOff");
            this.cbHandOff.Name = "cbHandOff";
            this.cbHandOff.UseVisualStyleBackColor = true;
            // 
            // lblDialByName
            // 
            this.lblDialByName.AntiAliasText = false;
            resources.ApplyResources(this.lblDialByName, "lblDialByName");
            this.lblDialByName.BackColor = System.Drawing.Color.Transparent;
            this.lblDialByName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDialByName.EnableHelp = true;
            this.lblDialByName.Name = "lblDialByName";
            this.lblDialByName.Click += new System.EventHandler(this.lblDialByName_Click);
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
            // numExtNum
            // 
            resources.ApplyResources(this.numExtNum, "numExtNum");
            this.numExtNum.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numExtNum.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numExtNum.Name = "numExtNum";
            this.numExtNum.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // cbEnableSearch
            // 
            resources.ApplyResources(this.cbEnableSearch, "cbEnableSearch");
            this.cbEnableSearch.Name = "cbEnableSearch";
            this.cbEnableSearch.UseVisualStyleBackColor = true;
            // 
            // txtFirstName
            // 
            resources.ApplyResources(this.txtFirstName, "txtFirstName");
            this.txtFirstName.Name = "txtFirstName";
            // 
            // txtLastName
            // 
            resources.ApplyResources(this.txtLastName, "txtLastName");
            this.txtLastName.Name = "txtLastName";
            // 
            // btnImportOutlook
            // 
            this.btnImportOutlook.AntiAliasText = false;
            this.btnImportOutlook.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnImportOutlook, "btnImportOutlook");
            this.btnImportOutlook.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnImportOutlook.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportOutlook.LinkImage = global::CallButler.Manager.Properties.Resources.outlook_16;
            this.btnImportOutlook.Name = "btnImportOutlook";
            this.btnImportOutlook.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportOutlook.UnderlineOnHover = true;
            this.btnImportOutlook.Click += new System.EventHandler(this.btnImportOutlook_Click);
            // 
            // wizardHeader1
            // 
            resources.ApplyResources(this.wizardHeader1, "wizardHeader1");
            this.wizardHeader1.Image = global::CallButler.Manager.Properties.Resources.user1_telephone_48_shadow;
            this.wizardHeader1.Name = "wizardHeader1";
            // 
            // pgFindme
            // 
            this.pgFindme.Controls.Add(this.pnlFindMe);
            this.pgFindme.Controls.Add(this.wizardHeader3);
            resources.ApplyResources(this.pgFindme, "pgFindme");
            this.pgFindme.IsFinishPage = false;
            this.pgFindme.Name = "pgFindme";
            this.pgFindme.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // pnlFindMe
            // 
            resources.ApplyResources(this.pnlFindMe, "pnlFindMe");
            this.pnlFindMe.Name = "pnlFindMe";
            // 
            // wizardHeader3
            // 
            resources.ApplyResources(this.wizardHeader3, "wizardHeader3");
            this.wizardHeader3.Image = global::CallButler.Manager.Properties.Resources.user1_telephone_48_shadow;
            this.wizardHeader3.Name = "wizardHeader3";
            // 
            // pgVoicemailGreeting
            // 
            this.pgVoicemailGreeting.Controls.Add(this.wizardHeader6);
            this.pgVoicemailGreeting.Controls.Add(this.greetingControl);
            resources.ApplyResources(this.pgVoicemailGreeting, "pgVoicemailGreeting");
            this.pgVoicemailGreeting.IsFinishPage = false;
            this.pgVoicemailGreeting.Name = "pgVoicemailGreeting";
            this.pgVoicemailGreeting.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // wizardHeader6
            // 
            resources.ApplyResources(this.wizardHeader6, "wizardHeader6");
            this.wizardHeader6.Image = global::CallButler.Manager.Properties.Resources.user1_telephone_48_shadow;
            this.wizardHeader6.Name = "wizardHeader6";
            // 
            // greetingControl
            // 
            this.greetingControl.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.greetingControl, "greetingControl");
            this.greetingControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.greetingControl.Name = "greetingControl";
            // 
            // pgEmailSettings
            // 
            this.pgEmailSettings.Controls.Add(this.lblEmail);
            this.pgEmailSettings.Controls.Add(this.pnlEmailNotifications);
            this.pgEmailSettings.Controls.Add(this.cbEmailNotification);
            this.pgEmailSettings.Controls.Add(this.wizardHeader4);
            resources.ApplyResources(this.pgEmailSettings, "pgEmailSettings");
            this.pgEmailSettings.IsFinishPage = false;
            this.pgEmailSettings.Name = "pgEmailSettings";
            this.pgEmailSettings.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // lblEmail
            // 
            this.lblEmail.AntiAliasText = false;
            resources.ApplyResources(this.lblEmail, "lblEmail");
            this.lblEmail.BackColor = System.Drawing.Color.Transparent;
            this.lblEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblEmail.EnableHelp = true;
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Click += new System.EventHandler(this.lblEmail_Click);
            // 
            // pnlEmailNotifications
            // 
            this.pnlEmailNotifications.Controls.Add(this.lblAttachment);
            this.pnlEmailNotifications.Controls.Add(this.smoothLabel5);
            this.pnlEmailNotifications.Controls.Add(this.btnSendTestEmail);
            this.pnlEmailNotifications.Controls.Add(this.cbAttach);
            this.pnlEmailNotifications.Controls.Add(this.txtEmailAddress);
            resources.ApplyResources(this.pnlEmailNotifications, "pnlEmailNotifications");
            this.pnlEmailNotifications.Name = "pnlEmailNotifications";
            // 
            // lblAttachment
            // 
            this.lblAttachment.AntiAliasText = false;
            resources.ApplyResources(this.lblAttachment, "lblAttachment");
            this.lblAttachment.BackColor = System.Drawing.Color.Transparent;
            this.lblAttachment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAttachment.EnableHelp = true;
            this.lblAttachment.Name = "lblAttachment";
            this.lblAttachment.Click += new System.EventHandler(this.lblAttachment_Click);
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
            // cbAttach
            // 
            resources.ApplyResources(this.cbAttach, "cbAttach");
            this.cbAttach.Name = "cbAttach";
            this.cbAttach.UseVisualStyleBackColor = true;
            // 
            // txtEmailAddress
            // 
            resources.ApplyResources(this.txtEmailAddress, "txtEmailAddress");
            this.txtEmailAddress.Name = "txtEmailAddress";
            // 
            // cbEmailNotification
            // 
            resources.ApplyResources(this.cbEmailNotification, "cbEmailNotification");
            this.cbEmailNotification.Name = "cbEmailNotification";
            this.cbEmailNotification.UseVisualStyleBackColor = true;
            this.cbEmailNotification.CheckedChanged += new System.EventHandler(this.cbEmailNotification_CheckedChanged);
            // 
            // wizardHeader4
            // 
            resources.ApplyResources(this.wizardHeader4, "wizardHeader4");
            this.wizardHeader4.Image = global::CallButler.Manager.Properties.Resources.user1_telephone_48_shadow;
            this.wizardHeader4.Name = "wizardHeader4";
            // 
            // pgManagement
            // 
            this.pgManagement.Controls.Add(this.pnlPBXPassword);
            this.pgManagement.Controls.Add(this.smoothLabel4);
            this.pgManagement.Controls.Add(this.lblRemoteManagement);
            this.pgManagement.Controls.Add(this.cbEnableManagement);
            this.pgManagement.Controls.Add(this.txtConfirmPassword);
            this.pgManagement.Controls.Add(this.txtPassword);
            this.pgManagement.Controls.Add(this.label11);
            this.pgManagement.Controls.Add(this.label10);
            this.pgManagement.Controls.Add(this.wizardHeader2);
            resources.ApplyResources(this.pgManagement, "pgManagement");
            this.pgManagement.IsFinishPage = false;
            this.pgManagement.Name = "pgManagement";
            this.pgManagement.TabLinkColor = System.Drawing.SystemColors.ControlText;
            this.pgManagement.CloseFromNext += new global::Controls.Wizard.PageEventHandler(this.pgManagement_CloseFromNext);
            this.pgManagement.CloseFromBack += new global::Controls.Wizard.PageEventHandler(this.pgManagement_CloseFromBack);
            // 
            // pnlPBXPassword
            // 
            this.pnlPBXPassword.Controls.Add(this.smoothLabel6);
            this.pnlPBXPassword.Controls.Add(this.txtConfirmPBXPassword);
            this.pnlPBXPassword.Controls.Add(this.txtPBXPassword);
            this.pnlPBXPassword.Controls.Add(this.label1);
            resources.ApplyResources(this.pnlPBXPassword, "pnlPBXPassword");
            this.pnlPBXPassword.Name = "pnlPBXPassword";
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
            // txtConfirmPBXPassword
            // 
            resources.ApplyResources(this.txtConfirmPBXPassword, "txtConfirmPBXPassword");
            this.txtConfirmPBXPassword.Name = "txtConfirmPBXPassword";
            this.txtConfirmPBXPassword.UseSystemPasswordChar = true;
            // 
            // txtPBXPassword
            // 
            resources.ApplyResources(this.txtPBXPassword, "txtPBXPassword");
            this.txtPBXPassword.Name = "txtPBXPassword";
            this.txtPBXPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // 
            // txtConfirmPassword
            // 
            resources.ApplyResources(this.txtConfirmPassword, "txtConfirmPassword");
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            this.txtConfirmPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // wizardHeader2
            // 
            resources.ApplyResources(this.wizardHeader2, "wizardHeader2");
            this.wizardHeader2.Image = global::CallButler.Manager.Properties.Resources.user1_telephone_48_shadow;
            this.wizardHeader2.Name = "wizardHeader2";
            // 
            // pgFinish
            // 
            this.pgFinish.Controls.Add(this.btnSetupInstructions);
            this.pgFinish.Controls.Add(this.label6);
            this.pgFinish.Controls.Add(this.wizardHeader5);
            resources.ApplyResources(this.pgFinish, "pgFinish");
            this.pgFinish.IsFinishPage = false;
            this.pgFinish.Name = "pgFinish";
            this.pgFinish.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // btnSetupInstructions
            // 
            this.btnSetupInstructions.AntiAliasText = false;
            this.btnSetupInstructions.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnSetupInstructions, "btnSetupInstructions");
            this.btnSetupInstructions.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSetupInstructions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSetupInstructions.LinkImage = global::CallButler.Manager.Properties.Resources.mail_earth_16;
            this.btnSetupInstructions.Name = "btnSetupInstructions";
            this.btnSetupInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSetupInstructions.UnderlineOnHover = true;
            this.btnSetupInstructions.Click += new System.EventHandler(this.btnSetupInstructions_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // wizardHeader5
            // 
            resources.ApplyResources(this.wizardHeader5, "wizardHeader5");
            this.wizardHeader5.Image = global::CallButler.Manager.Properties.Resources.user1_telephone_48_shadow;
            this.wizardHeader5.Name = "wizardHeader5";
            // 
            // ExtensionEditorForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.wizard);
            this.Name = "ExtensionEditorForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExtensionEditorForm_FormClosing);
            this.wizard.ResumeLayout(false);
            this.pgGeneral.ResumeLayout(false);
            this.pgGeneral.PerformLayout();
            this.pnlHandoff.ResumeLayout(false);
            this.pnlHandoff.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExtNum)).EndInit();
            this.pgFindme.ResumeLayout(false);
            this.pgVoicemailGreeting.ResumeLayout(false);
            this.pgEmailSettings.ResumeLayout(false);
            this.pgEmailSettings.PerformLayout();
            this.pnlEmailNotifications.ResumeLayout(false);
            this.pnlEmailNotifications.PerformLayout();
            this.pgManagement.ResumeLayout(false);
            this.pgManagement.PerformLayout();
            this.pnlPBXPassword.ResumeLayout(false);
            this.pnlPBXPassword.PerformLayout();
            this.pgFinish.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private global::Controls.Wizard.Wizard wizard;
        private global::Controls.Wizard.WizardPage pgGeneral;
        private global::Controls.Wizard.WizardPage pgManagement;
        private global::Controls.Wizard.WizardPage pgFinish;
        private global::Controls.Wizard.WizardPage pgEmailSettings;
        private global::Controls.Wizard.WizardPage pgFindme;
        private CallButler.Manager.Controls.WizardHeader wizardHeader1;
        private CallButler.Manager.Controls.WizardHeader wizardHeader2;
        private CallButler.Manager.Controls.WizardHeader wizardHeader3;
        private CallButler.Manager.Controls.WizardHeader wizardHeader4;
        private CallButler.Manager.Controls.WizardHeader wizardHeader5;
        private System.Windows.Forms.NumericUpDown numExtNum;
        private System.Windows.Forms.CheckBox cbEnableSearch;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private global::Controls.LinkButton btnImportOutlook;
        private System.Windows.Forms.CheckBox cbEnableManagement;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private global::Controls.Wizard.WizardPage pgVoicemailGreeting;
        private CallButler.Manager.Controls.GreetingControl greetingControl;
        private CallButler.Manager.Controls.WizardHeader wizardHeader6;
        private System.Windows.Forms.Panel pnlEmailNotifications;
        private global::Controls.LinkButton btnSendTestEmail;
        private System.Windows.Forms.CheckBox cbAttach;
        private System.Windows.Forms.TextBox txtEmailAddress;
        private System.Windows.Forms.CheckBox cbEmailNotification;
        private System.Windows.Forms.Label label6;
        private global::Controls.LinkButton btnSetupInstructions;
        private global::Controls.SmoothLabel smoothLabel2;
        private global::Controls.SmoothLabel smoothLabel1;
        private global::Controls.SmoothLabel smoothLabel3;
        private global::Controls.SmoothLabel lblDialByName;
        private global::Controls.SmoothLabel lblRemoteManagement;
        private global::Controls.SmoothLabel smoothLabel4;
        private global::Controls.SmoothLabel lblEmail;
        private global::Controls.SmoothLabel smoothLabel5;
        private global::Controls.SmoothLabel lblAttachment;
        private System.Windows.Forms.Panel pnlFindMe;
        private global::Controls.SmoothLabel lblHandoff;
        private System.Windows.Forms.CheckBox cbHandOff;
        private System.Windows.Forms.Panel pnlHandoff;
        private System.Windows.Forms.TextBox txtConfirmPBXPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPBXPassword;
        private global::Controls.SmoothLabel smoothLabel6;
        private System.Windows.Forms.Panel pnlPBXPassword;
        private global::Controls.SmoothLabel lblCallScreen;
        private System.Windows.Forms.CheckBox cbCallScreening;
        private global::Controls.SmoothLabel lblEnableOutbound;
        private System.Windows.Forms.CheckBox cbEnableOutbound;
    }
}
