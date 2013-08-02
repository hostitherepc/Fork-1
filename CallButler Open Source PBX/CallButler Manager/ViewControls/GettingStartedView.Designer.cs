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
    partial class GettingStartedView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GettingStartedView));
            this.wizard = new global::Controls.Wizard.Wizard();
            this.pgWelcome = new global::Controls.Wizard.WizardPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new global::Controls.SmoothLabel();
            this.pgAboutExtensions = new global::Controls.Wizard.WizardPage();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.smoothLabel1 = new global::Controls.SmoothLabel();
            this.pgExtensions = new global::Controls.Wizard.WizardPage();
            this.extensionsView = new CallButler.Manager.ViewControls.ExtensionsView();
            this.pgAboutCallFlow = new global::Controls.Wizard.WizardPage();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.smoothLabel2 = new global::Controls.SmoothLabel();
            this.pgCallFlow = new global::Controls.Wizard.WizardPage();
            this.callFlowView = new CallButler.Manager.ViewControls.CallFlowView();
            this.pgTestDrive = new global::Controls.Wizard.WizardPage();
            this.testDriveView = new CallButler.Manager.ViewControls.TestDriveView();
            this.pgProviders = new global::Controls.Wizard.WizardPage();
            this.btnOtherVoIP = new global::Controls.LinkButton();
            this.btnCallButlerVoIP = new global::Controls.LinkButton();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.smoothLabel3 = new global::Controls.SmoothLabel();
            this.pgFinish = new global::Controls.Wizard.WizardPage();
            this.btnEmailSetup = new global::Controls.LinkButton();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSetupPassword = new global::Controls.LinkButton();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.smoothLabel4 = new global::Controls.SmoothLabel();
            this.wizard.SuspendLayout();
            this.pgWelcome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pgAboutExtensions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pgExtensions.SuspendLayout();
            this.pgAboutCallFlow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.pgCallFlow.SuspendLayout();
            this.pgTestDrive.SuspendLayout();
            this.pgProviders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.pgFinish.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // wizard
            // 
            this.wizard.AlwaysShowFinishButton = false;
            this.wizard.BackColor = System.Drawing.Color.WhiteSmoke;
            this.wizard.CloseOnCancel = false;
            this.wizard.CloseOnFinish = false;
            this.wizard.Controls.Add(this.pgWelcome);
            this.wizard.Controls.Add(this.pgAboutExtensions);
            this.wizard.Controls.Add(this.pgExtensions);
            this.wizard.Controls.Add(this.pgAboutCallFlow);
            this.wizard.Controls.Add(this.pgCallFlow);
            this.wizard.Controls.Add(this.pgTestDrive);
            this.wizard.Controls.Add(this.pgProviders);
            this.wizard.Controls.Add(this.pgFinish);
            this.wizard.DisplayButtons = true;
            resources.ApplyResources(this.wizard, "wizard");
            this.wizard.Name = "wizard";
            this.wizard.PageIndex = 0;
            this.wizard.Pages.AddRange(new global::Controls.Wizard.WizardPage[] {
            this.pgWelcome,
            this.pgAboutExtensions,
            this.pgExtensions,
            this.pgAboutCallFlow,
            this.pgCallFlow,
            this.pgTestDrive,
            this.pgProviders,
            this.pgFinish});
            this.wizard.ShowTabs = true;
            this.wizard.TabBackColor = System.Drawing.Color.WhiteSmoke;
            this.wizard.TabBackgroundImage = global::CallButler.Manager.Properties.Resources.wizard_header;
            this.wizard.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.wizard.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.Cutout;
            this.wizard.WizardFinished += new System.EventHandler<global::Controls.Wizard.PageChangedEventArgs>(this.wizard_WizardFinished);
            this.wizard.CloseFromCancel += new System.ComponentModel.CancelEventHandler(this.wizard_CloseFromCancel);
            // 
            // pgWelcome
            // 
            this.pgWelcome.Controls.Add(this.pictureBox1);
            this.pgWelcome.Controls.Add(this.label2);
            this.pgWelcome.Controls.Add(this.label1);
            this.pgWelcome.Controls.Add(this.lblTitle);
            resources.ApplyResources(this.pgWelcome, "pgWelcome");
            this.pgWelcome.Icon = global::CallButler.Manager.Properties.Resources.hand_offer_16;
            this.pgWelcome.IsFinishPage = false;
            this.pgWelcome.Name = "pgWelcome";
            this.pgWelcome.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CallButler.Manager.Properties.Resources.hand_offer_48;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblTitle.Name = "lblTitle";
            // 
            // pgAboutExtensions
            // 
            this.pgAboutExtensions.Controls.Add(this.label3);
            this.pgAboutExtensions.Controls.Add(this.pictureBox2);
            this.pgAboutExtensions.Controls.Add(this.smoothLabel1);
            resources.ApplyResources(this.pgAboutExtensions, "pgAboutExtensions");
            this.pgAboutExtensions.Icon = global::CallButler.Manager.Properties.Resources.about_16;
            this.pgAboutExtensions.IsFinishPage = false;
            this.pgAboutExtensions.Name = "pgAboutExtensions";
            this.pgAboutExtensions.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CallButler.Manager.Properties.Resources.telephone_48_shadow;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // smoothLabel1
            // 
            resources.ApplyResources(this.smoothLabel1, "smoothLabel1");
            this.smoothLabel1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.smoothLabel1.Name = "smoothLabel1";
            // 
            // pgExtensions
            // 
            this.pgExtensions.Controls.Add(this.extensionsView);
            resources.ApplyResources(this.pgExtensions, "pgExtensions");
            this.pgExtensions.Icon = global::CallButler.Manager.Properties.Resources.telephone_16;
            this.pgExtensions.IsFinishPage = false;
            this.pgExtensions.Name = "pgExtensions";
            this.pgExtensions.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // extensionsView
            // 
            this.extensionsView.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.extensionsView, "extensionsView");
            this.extensionsView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.extensionsView.HeaderIcon = ((System.Drawing.Image)(resources.GetObject("extensionsView.HeaderIcon")));
            this.extensionsView.Name = "extensionsView";
            this.extensionsView.SelectedExensionID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.extensionsView.ShowVoicemailColumn = true;
            // 
            // pgAboutCallFlow
            // 
            this.pgAboutCallFlow.Controls.Add(this.label4);
            this.pgAboutCallFlow.Controls.Add(this.pictureBox3);
            this.pgAboutCallFlow.Controls.Add(this.smoothLabel2);
            resources.ApplyResources(this.pgAboutCallFlow, "pgAboutCallFlow");
            this.pgAboutCallFlow.Icon = global::CallButler.Manager.Properties.Resources.about_16;
            this.pgAboutCallFlow.IsFinishPage = false;
            this.pgAboutCallFlow.Name = "pgAboutCallFlow";
            this.pgAboutCallFlow.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::CallButler.Manager.Properties.Resources.branch_48_shadow;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // smoothLabel2
            // 
            resources.ApplyResources(this.smoothLabel2, "smoothLabel2");
            this.smoothLabel2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.smoothLabel2.Name = "smoothLabel2";
            // 
            // pgCallFlow
            // 
            this.pgCallFlow.Controls.Add(this.callFlowView);
            resources.ApplyResources(this.pgCallFlow, "pgCallFlow");
            this.pgCallFlow.Icon = global::CallButler.Manager.Properties.Resources.branch_16;
            this.pgCallFlow.IsFinishPage = false;
            this.pgCallFlow.Name = "pgCallFlow";
            this.pgCallFlow.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // callFlowView
            // 
            this.callFlowView.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.callFlowView, "callFlowView");
            this.callFlowView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.callFlowView.HeaderIcon = ((System.Drawing.Image)(resources.GetObject("callFlowView.HeaderIcon")));
            this.callFlowView.Name = "callFlowView";
            // 
            // pgTestDrive
            // 
            this.pgTestDrive.Controls.Add(this.testDriveView);
            resources.ApplyResources(this.pgTestDrive, "pgTestDrive");
            this.pgTestDrive.Icon = global::CallButler.Manager.Properties.Resources.gauge_16;
            this.pgTestDrive.IsFinishPage = false;
            this.pgTestDrive.Name = "pgTestDrive";
            this.pgTestDrive.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // testDriveView
            // 
            this.testDriveView.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.testDriveView, "testDriveView");
            this.testDriveView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.testDriveView.HeaderIcon = ((System.Drawing.Image)(resources.GetObject("testDriveView.HeaderIcon")));
            this.testDriveView.Name = "testDriveView";
            // 
            // pgProviders
            // 
            this.pgProviders.Controls.Add(this.btnOtherVoIP);
            this.pgProviders.Controls.Add(this.btnCallButlerVoIP);
            this.pgProviders.Controls.Add(this.label5);
            this.pgProviders.Controls.Add(this.pictureBox4);
            this.pgProviders.Controls.Add(this.smoothLabel3);
            resources.ApplyResources(this.pgProviders, "pgProviders");
            this.pgProviders.Icon = global::CallButler.Manager.Properties.Resources.phone_conference_16;
            this.pgProviders.IsFinishPage = false;
            this.pgProviders.Name = "pgProviders";
            this.pgProviders.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // btnOtherVoIP
            // 
            this.btnOtherVoIP.AntiAliasText = false;
            this.btnOtherVoIP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOtherVoIP.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnOtherVoIP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOtherVoIP.LinkImage = global::CallButler.Manager.Properties.Resources.media_play_16;
            resources.ApplyResources(this.btnOtherVoIP, "btnOtherVoIP");
            this.btnOtherVoIP.Name = "btnOtherVoIP";
            this.btnOtherVoIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOtherVoIP.UnderlineOnHover = true;
            this.btnOtherVoIP.Click += new System.EventHandler(this.btnOtherVoIP_Click);
            // 
            // btnCallButlerVoIP
            // 
            this.btnCallButlerVoIP.AntiAliasText = false;
            this.btnCallButlerVoIP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCallButlerVoIP.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnCallButlerVoIP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCallButlerVoIP.LinkImage = global::CallButler.Manager.Properties.Resources.media_play_16;
            resources.ApplyResources(this.btnCallButlerVoIP, "btnCallButlerVoIP");
            this.btnCallButlerVoIP.Name = "btnCallButlerVoIP";
            this.btnCallButlerVoIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCallButlerVoIP.UnderlineOnHover = true;
            this.btnCallButlerVoIP.Click += new System.EventHandler(this.btnCallButlerVoIP_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::CallButler.Manager.Properties.Resources.phone_conference_48_shadow;
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            // 
            // smoothLabel3
            // 
            resources.ApplyResources(this.smoothLabel3, "smoothLabel3");
            this.smoothLabel3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.smoothLabel3.Name = "smoothLabel3";
            // 
            // pgFinish
            // 
            this.pgFinish.Controls.Add(this.btnEmailSetup);
            this.pgFinish.Controls.Add(this.label7);
            this.pgFinish.Controls.Add(this.btnSetupPassword);
            this.pgFinish.Controls.Add(this.label6);
            this.pgFinish.Controls.Add(this.pictureBox5);
            this.pgFinish.Controls.Add(this.smoothLabel4);
            resources.ApplyResources(this.pgFinish, "pgFinish");
            this.pgFinish.Icon = global::CallButler.Manager.Properties.Resources.flag_checkered_16;
            this.pgFinish.IsFinishPage = false;
            this.pgFinish.Name = "pgFinish";
            this.pgFinish.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // btnEmailSetup
            // 
            this.btnEmailSetup.AntiAliasText = false;
            this.btnEmailSetup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmailSetup.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnEmailSetup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmailSetup.LinkImage = global::CallButler.Manager.Properties.Resources.mail_earth_16;
            resources.ApplyResources(this.btnEmailSetup, "btnEmailSetup");
            this.btnEmailSetup.Name = "btnEmailSetup";
            this.btnEmailSetup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmailSetup.UnderlineOnHover = true;
            this.btnEmailSetup.Click += new System.EventHandler(this.btnEmailSetup_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // btnSetupPassword
            // 
            this.btnSetupPassword.AntiAliasText = false;
            this.btnSetupPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetupPassword.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSetupPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSetupPassword.LinkImage = global::CallButler.Manager.Properties.Resources.lock_16;
            resources.ApplyResources(this.btnSetupPassword, "btnSetupPassword");
            this.btnSetupPassword.Name = "btnSetupPassword";
            this.btnSetupPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSetupPassword.UnderlineOnHover = true;
            this.btnSetupPassword.Click += new System.EventHandler(this.btnSetupPassword_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::CallButler.Manager.Properties.Resources.flag_checkered_48_shadow;
            resources.ApplyResources(this.pictureBox5, "pictureBox5");
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.TabStop = false;
            // 
            // smoothLabel4
            // 
            resources.ApplyResources(this.smoothLabel4, "smoothLabel4");
            this.smoothLabel4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.smoothLabel4.Name = "smoothLabel4";
            // 
            // GettingStartedView
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.wizard);
            this.HeaderIcon = global::CallButler.Manager.Properties.Resources.sportscar_48_shadow;
            this.Name = "GettingStartedView";
            this.ShowHelpPanel = false;
            this.Controls.SetChildIndex(this.wizard, 0);
            this.wizard.ResumeLayout(false);
            this.pgWelcome.ResumeLayout(false);
            this.pgWelcome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pgAboutExtensions.ResumeLayout(false);
            this.pgAboutExtensions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pgExtensions.ResumeLayout(false);
            this.pgAboutCallFlow.ResumeLayout(false);
            this.pgAboutCallFlow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.pgCallFlow.ResumeLayout(false);
            this.pgTestDrive.ResumeLayout(false);
            this.pgProviders.ResumeLayout(false);
            this.pgProviders.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.pgFinish.ResumeLayout(false);
            this.pgFinish.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private global::Controls.Wizard.Wizard wizard;
        private global::Controls.Wizard.WizardPage pgWelcome;
        private global::Controls.Wizard.WizardPage pgAboutExtensions;
        private global::Controls.Wizard.WizardPage pgExtensions;
        private global::Controls.Wizard.WizardPage pgAboutCallFlow;
        private global::Controls.Wizard.WizardPage pgCallFlow;
        private global::Controls.Wizard.WizardPage pgTestDrive;
        private global::Controls.Wizard.WizardPage pgProviders;
        private global::Controls.Wizard.WizardPage pgFinish;
        private global::Controls.SmoothLabel lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private global::Controls.SmoothLabel smoothLabel1;
        private ExtensionsView extensionsView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private global::Controls.SmoothLabel smoothLabel2;
        private CallFlowView callFlowView;
        private TestDriveView testDriveView;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private global::Controls.SmoothLabel smoothLabel3;
        private global::Controls.LinkButton btnCallButlerVoIP;
        private global::Controls.LinkButton btnOtherVoIP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private global::Controls.SmoothLabel smoothLabel4;
        private global::Controls.LinkButton btnSetupPassword;
        private System.Windows.Forms.Label label7;
        private global::Controls.LinkButton btnEmailSetup;

    }
}
