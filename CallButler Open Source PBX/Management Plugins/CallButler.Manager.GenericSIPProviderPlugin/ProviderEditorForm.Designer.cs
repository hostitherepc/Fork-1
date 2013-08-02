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

using Controls;

namespace CallButler.ManagementPlugins.GenericSIPProviderPlugin
{
    partial class ProviderEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProviderEditorForm));
            this.wizard = new Controls.Wizard.Wizard();
            this.pgAuth = new Controls.Wizard.WizardPage();
            this.smoothLabel9 = new Controls.SmoothLabel();
            this.txtCallerIDPrefix = new System.Windows.Forms.TextBox();
            this.cbCustomCallerID = new System.Windows.Forms.CheckBox();
            this.smoothLabel4 = new Controls.SmoothLabel();
            this.smoothLabel3 = new Controls.SmoothLabel();
            this.wizardHeader3 = new CallButler.Manager.Controls.WizardHeader();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtAuthUsername = new System.Windows.Forms.TextBox();
            this.pgNetwork = new Controls.Wizard.WizardPage();
            this.label2 = new System.Windows.Forms.Label();
            this.numExpires = new System.Windows.Forms.NumericUpDown();
            this.smoothLabel8 = new Controls.SmoothLabel();
            this.lblSipRegistration = new Controls.SmoothLabel();
            this.smoothLabel7 = new Controls.SmoothLabel();
            this.smoothLabel6 = new Controls.SmoothLabel();
            this.smoothLabel5 = new Controls.SmoothLabel();
            this.cbEnableReg = new System.Windows.Forms.CheckBox();
            this.txtRegistrarServer = new System.Windows.Forms.TextBox();
            this.txtProxyServer = new System.Windows.Forms.TextBox();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.wizardHeader2 = new CallButler.Manager.Controls.WizardHeader();
            this.wizardPage1 = new Controls.Wizard.WizardPage();
            this.pnlResults = new System.Windows.Forms.Panel();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnBeginTest = new Controls.LinkButton();
            this.label1 = new System.Windows.Forms.Label();
            this.wizardHeader4 = new CallButler.Manager.Controls.WizardHeader();
            this.pgGeneral = new Controls.Wizard.WizardPage();
            this.lblEnableProvider = new Controls.SmoothLabel();
            this.smoothLabel2 = new Controls.SmoothLabel();
            this.smoothLabel1 = new Controls.SmoothLabel();
            this.lblContinueCall = new Controls.SmoothLabel();
            this.cbEnable = new System.Windows.Forms.CheckBox();
            this.cboProviderName = new System.Windows.Forms.ComboBox();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.txtCallerID = new System.Windows.Forms.TextBox();
            this.wizardHeader1 = new CallButler.Manager.Controls.WizardHeader();
            this.wizard.SuspendLayout();
            this.pgAuth.SuspendLayout();
            this.pgNetwork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExpires)).BeginInit();
            this.wizardPage1.SuspendLayout();
            this.pnlResults.SuspendLayout();
            this.pnlLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pgGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard
            // 
            this.wizard.AlwaysShowFinishButton = true;
            this.wizard.BackColor = System.Drawing.Color.WhiteSmoke;
            this.wizard.CloseOnCancel = true;
            this.wizard.CloseOnFinish = true;
            this.wizard.Controls.Add(this.pgAuth);
            this.wizard.Controls.Add(this.pgNetwork);
            this.wizard.Controls.Add(this.wizardPage1);
            this.wizard.Controls.Add(this.pgGeneral);
            this.wizard.DisplayButtons = true;
            resources.ApplyResources(this.wizard, "wizard");
            this.wizard.Name = "wizard";
            this.wizard.PageIndex = 1;
            this.wizard.Pages.AddRange(new Controls.Wizard.WizardPage[] {
            this.pgGeneral,
            this.pgAuth,
            this.pgNetwork,
            this.wizardPage1});
            this.wizard.ShowTabs = true;
            this.wizard.TabBackColor = System.Drawing.Color.WhiteSmoke;
            this.wizard.TabBackgroundImage = global::CallButler.ManagementPlugins.GenericSIPProviderPlugin.Properties.Resources.wizard_header;
            this.wizard.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.wizard.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.Cutout;
            this.wizard.WizardFinished += new System.EventHandler<Controls.Wizard.PageChangedEventArgs>(this.wizard_WizardFinished);
            // 
            // pgAuth
            // 
            this.pgAuth.Controls.Add(this.smoothLabel9);
            this.pgAuth.Controls.Add(this.txtCallerIDPrefix);
            this.pgAuth.Controls.Add(this.cbCustomCallerID);
            this.pgAuth.Controls.Add(this.smoothLabel4);
            this.pgAuth.Controls.Add(this.smoothLabel3);
            this.pgAuth.Controls.Add(this.wizardHeader3);
            this.pgAuth.Controls.Add(this.txtPassword);
            this.pgAuth.Controls.Add(this.txtAuthUsername);
            resources.ApplyResources(this.pgAuth, "pgAuth");
            this.pgAuth.IsFinishPage = false;
            this.pgAuth.Name = "pgAuth";
            this.pgAuth.TabLinkColor = System.Drawing.SystemColors.ControlText;
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
            // txtCallerIDPrefix
            // 
            resources.ApplyResources(this.txtCallerIDPrefix, "txtCallerIDPrefix");
            this.txtCallerIDPrefix.Name = "txtCallerIDPrefix";
            // 
            // cbCustomCallerID
            // 
            resources.ApplyResources(this.cbCustomCallerID, "cbCustomCallerID");
            this.cbCustomCallerID.Name = "cbCustomCallerID";
            this.cbCustomCallerID.UseVisualStyleBackColor = true;
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
            // smoothLabel3
            // 
            this.smoothLabel3.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel3, "smoothLabel3");
            this.smoothLabel3.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel3.EnableHelp = true;
            this.smoothLabel3.Name = "smoothLabel3";
            // 
            // wizardHeader3
            // 
            resources.ApplyResources(this.wizardHeader3, "wizardHeader3");
            this.wizardHeader3.Name = "wizardHeader3";
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtAuthUsername
            // 
            resources.ApplyResources(this.txtAuthUsername, "txtAuthUsername");
            this.txtAuthUsername.Name = "txtAuthUsername";
            // 
            // pgNetwork
            // 
            this.pgNetwork.Controls.Add(this.label2);
            this.pgNetwork.Controls.Add(this.numExpires);
            this.pgNetwork.Controls.Add(this.smoothLabel8);
            this.pgNetwork.Controls.Add(this.lblSipRegistration);
            this.pgNetwork.Controls.Add(this.smoothLabel7);
            this.pgNetwork.Controls.Add(this.smoothLabel6);
            this.pgNetwork.Controls.Add(this.smoothLabel5);
            this.pgNetwork.Controls.Add(this.cbEnableReg);
            this.pgNetwork.Controls.Add(this.txtRegistrarServer);
            this.pgNetwork.Controls.Add(this.txtProxyServer);
            this.pgNetwork.Controls.Add(this.txtDomain);
            this.pgNetwork.Controls.Add(this.wizardHeader2);
            resources.ApplyResources(this.pgNetwork, "pgNetwork");
            this.pgNetwork.IsFinishPage = false;
            this.pgNetwork.Name = "pgNetwork";
            this.pgNetwork.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // numExpires
            // 
            resources.ApplyResources(this.numExpires, "numExpires");
            this.numExpires.Maximum = new decimal(new int[] {
            32000,
            0,
            0,
            0});
            this.numExpires.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numExpires.Name = "numExpires";
            this.numExpires.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
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
            // lblSipRegistration
            // 
            this.lblSipRegistration.AntiAliasText = false;
            resources.ApplyResources(this.lblSipRegistration, "lblSipRegistration");
            this.lblSipRegistration.BackColor = System.Drawing.Color.Transparent;
            this.lblSipRegistration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSipRegistration.EnableHelp = true;
            this.lblSipRegistration.Name = "lblSipRegistration";
            this.lblSipRegistration.Click += new System.EventHandler(this.lblSipRegistration_Click);
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
            // smoothLabel5
            // 
            this.smoothLabel5.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel5, "smoothLabel5");
            this.smoothLabel5.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel5.EnableHelp = true;
            this.smoothLabel5.Name = "smoothLabel5";
            // 
            // cbEnableReg
            // 
            resources.ApplyResources(this.cbEnableReg, "cbEnableReg");
            this.cbEnableReg.Checked = true;
            this.cbEnableReg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEnableReg.Name = "cbEnableReg";
            this.cbEnableReg.UseVisualStyleBackColor = true;
            // 
            // txtRegistrarServer
            // 
            resources.ApplyResources(this.txtRegistrarServer, "txtRegistrarServer");
            this.txtRegistrarServer.Name = "txtRegistrarServer";
            // 
            // txtProxyServer
            // 
            resources.ApplyResources(this.txtProxyServer, "txtProxyServer");
            this.txtProxyServer.Name = "txtProxyServer";
            // 
            // txtDomain
            // 
            resources.ApplyResources(this.txtDomain, "txtDomain");
            this.txtDomain.Name = "txtDomain";
            // 
            // wizardHeader2
            // 
            resources.ApplyResources(this.wizardHeader2, "wizardHeader2");
            this.wizardHeader2.Name = "wizardHeader2";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.pnlResults);
            this.wizardPage1.Controls.Add(this.btnBeginTest);
            this.wizardPage1.Controls.Add(this.label1);
            this.wizardPage1.Controls.Add(this.wizardHeader4);
            resources.ApplyResources(this.wizardPage1, "wizardPage1");
            this.wizardPage1.IsFinishPage = false;
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // pnlResults
            // 
            this.pnlResults.Controls.Add(this.pnlLoading);
            resources.ApplyResources(this.pnlResults, "pnlResults");
            this.pnlResults.Name = "pnlResults";
            // 
            // pnlLoading
            // 
            this.pnlLoading.Controls.Add(this.label3);
            this.pnlLoading.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.pnlLoading, "pnlLoading");
            this.pnlLoading.Name = "pnlLoading";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CallButler.ManagementPlugins.GenericSIPProviderPlugin.Properties.Resources.loading;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // btnBeginTest
            // 
            this.btnBeginTest.AntiAliasText = false;
            resources.ApplyResources(this.btnBeginTest, "btnBeginTest");
            this.btnBeginTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBeginTest.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnBeginTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBeginTest.LinkImage = ((System.Drawing.Image)(resources.GetObject("btnBeginTest.LinkImage")));
            this.btnBeginTest.Name = "btnBeginTest";
            this.btnBeginTest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBeginTest.UnderlineOnHover = true;
            this.btnBeginTest.Click += new System.EventHandler(this.btnBeginTest_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // wizardHeader4
            // 
            resources.ApplyResources(this.wizardHeader4, "wizardHeader4");
            this.wizardHeader4.Name = "wizardHeader4";
            // 
            // pgGeneral
            // 
            this.pgGeneral.Controls.Add(this.lblEnableProvider);
            this.pgGeneral.Controls.Add(this.smoothLabel2);
            this.pgGeneral.Controls.Add(this.smoothLabel1);
            this.pgGeneral.Controls.Add(this.lblContinueCall);
            this.pgGeneral.Controls.Add(this.cbEnable);
            this.pgGeneral.Controls.Add(this.cboProviderName);
            this.pgGeneral.Controls.Add(this.txtPhoneNumber);
            this.pgGeneral.Controls.Add(this.txtCallerID);
            this.pgGeneral.Controls.Add(this.wizardHeader1);
            resources.ApplyResources(this.pgGeneral, "pgGeneral");
            this.pgGeneral.IsFinishPage = false;
            this.pgGeneral.Name = "pgGeneral";
            this.pgGeneral.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // lblEnableProvider
            // 
            this.lblEnableProvider.AntiAliasText = false;
            resources.ApplyResources(this.lblEnableProvider, "lblEnableProvider");
            this.lblEnableProvider.BackColor = System.Drawing.Color.Transparent;
            this.lblEnableProvider.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblEnableProvider.EnableHelp = true;
            this.lblEnableProvider.Name = "lblEnableProvider";
            this.lblEnableProvider.Click += new System.EventHandler(this.lblEnableProvider_Click);
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
            // lblContinueCall
            // 
            this.lblContinueCall.AntiAliasText = false;
            resources.ApplyResources(this.lblContinueCall, "lblContinueCall");
            this.lblContinueCall.BackColor = System.Drawing.Color.Transparent;
            this.lblContinueCall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblContinueCall.EnableHelp = true;
            this.lblContinueCall.Name = "lblContinueCall";
            // 
            // cbEnable
            // 
            resources.ApplyResources(this.cbEnable, "cbEnable");
            this.cbEnable.Checked = true;
            this.cbEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEnable.Name = "cbEnable";
            this.cbEnable.UseVisualStyleBackColor = true;
            // 
            // cboProviderName
            // 
            this.cboProviderName.FormattingEnabled = true;
            resources.ApplyResources(this.cboProviderName, "cboProviderName");
            this.cboProviderName.Name = "cboProviderName";
            // 
            // txtPhoneNumber
            // 
            resources.ApplyResources(this.txtPhoneNumber, "txtPhoneNumber");
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            // 
            // txtCallerID
            // 
            resources.ApplyResources(this.txtCallerID, "txtCallerID");
            this.txtCallerID.Name = "txtCallerID";
            // 
            // wizardHeader1
            // 
            resources.ApplyResources(this.wizardHeader1, "wizardHeader1");
            this.wizardHeader1.Name = "wizardHeader1";
            // 
            // ProviderEditorForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizard);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProviderEditorForm";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProviderEditorForm_FormClosing);
            this.wizard.ResumeLayout(false);
            this.pgAuth.ResumeLayout(false);
            this.pgAuth.PerformLayout();
            this.pgNetwork.ResumeLayout(false);
            this.pgNetwork.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExpires)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.pnlResults.ResumeLayout(false);
            this.pnlLoading.ResumeLayout(false);
            this.pnlLoading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pgGeneral.ResumeLayout(false);
            this.pgGeneral.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private global::Controls.Wizard.Wizard wizard;
        private global::Controls.Wizard.WizardPage pgGeneral;
        private CallButler.Manager.Controls.WizardHeader wizardHeader1;
        private System.Windows.Forms.TextBox txtCallerID;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private global::Controls.Wizard.WizardPage pgNetwork;
        private CallButler.Manager.Controls.WizardHeader wizardHeader2;
        private System.Windows.Forms.TextBox txtProxyServer;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.TextBox txtRegistrarServer;
        private global::Controls.Wizard.WizardPage pgAuth;
        private CallButler.Manager.Controls.WizardHeader wizardHeader3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtAuthUsername;
        private System.Windows.Forms.ComboBox cboProviderName;
        private System.Windows.Forms.CheckBox cbEnable;
        private System.Windows.Forms.CheckBox cbEnableReg;
        private global::Controls.SmoothLabel smoothLabel1;
        private global::Controls.SmoothLabel lblContinueCall;
        private global::Controls.SmoothLabel smoothLabel2;
        private global::Controls.SmoothLabel lblEnableProvider;
        private global::Controls.SmoothLabel smoothLabel3;
        private global::Controls.SmoothLabel smoothLabel5;
        private global::Controls.SmoothLabel smoothLabel4;
        private global::Controls.SmoothLabel smoothLabel6;
        private global::Controls.SmoothLabel smoothLabel7;
        private global::Controls.SmoothLabel lblSipRegistration;
        private global::Controls.Wizard.WizardPage wizardPage1;
        private CallButler.Manager.Controls.WizardHeader wizardHeader4;
        private System.Windows.Forms.Label label1;
        private global::Controls.LinkButton btnBeginTest;
        private System.Windows.Forms.Panel pnlResults;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private global::Controls.SmoothLabel smoothLabel8;
        private System.Windows.Forms.NumericUpDown numExpires;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbCustomCallerID;
        private SmoothLabel smoothLabel9;
        private System.Windows.Forms.TextBox txtCallerIDPrefix;

    }
}