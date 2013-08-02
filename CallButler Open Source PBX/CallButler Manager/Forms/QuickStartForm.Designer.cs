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
    partial class QuickStartForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickStartForm));
            this.lblTitle = new global::Controls.SmoothLabel();
            this.smoothLabel1 = new global::Controls.SmoothLabel();
            this.smoothLabel2 = new global::Controls.SmoothLabel();
            this.dividerLine1 = new global::Controls.DividerLine();
            this.Wizard = new global::Controls.Wizard.Wizard();
            this.pgInfo = new global::Controls.Wizard.WizardPage();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboCountry = new global::Controls.CountryComboBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPrivacy = new global::Controls.LinkButton();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.smoothLabel3 = new global::Controls.SmoothLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.reflectionPicture1 = new global::Controls.ReflectionPicture();
            this.pgTrialNumber = new global::Controls.Wizard.WizardPage();
            this.smoothLabel7 = new global::Controls.SmoothLabel();
            this.reflectionPicture4 = new global::Controls.ReflectionPicture();
            this.wizFreeNumber = new global::Controls.Wizard.Wizard();
            this.wizardPage3 = new global::Controls.Wizard.WizardPage();
            this.label12 = new System.Windows.Forms.Label();
            this.smoothLabel6 = new global::Controls.SmoothLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.wizardPage2 = new global::Controls.Wizard.WizardPage();
            this.wizardPage1 = new global::Controls.Wizard.WizardPage();
            this.btnTermsOfUse = new global::Controls.LinkButton();
            this.btnGetFreeNumber = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txtFreeEmail = new System.Windows.Forms.TextBox();
            this.smoothLabel8 = new global::Controls.SmoothLabel();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pgTest = new global::Controls.Wizard.WizardPage();
            this.label8 = new System.Windows.Forms.Label();
            this.testDriveView = new CallButler.Manager.ViewControls.TestDriveView();
            this.smoothLabel4 = new global::Controls.SmoothLabel();
            this.reflectionPicture2 = new global::Controls.ReflectionPicture();
            this.pgCustomize = new global::Controls.Wizard.WizardPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStartUsing = new global::Controls.LinkButton();
            this.btnTour = new global::Controls.LinkButton();
            this.btnHeadStart = new global::Controls.LinkButton();
            this.label2 = new System.Windows.Forms.Label();
            this.smoothLabel5 = new global::Controls.SmoothLabel();
            this.reflectionPicture3 = new global::Controls.ReflectionPicture();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.Wizard.SuspendLayout();
            this.pgInfo.SuspendLayout();
            this.pgTrialNumber.SuspendLayout();
            this.wizFreeNumber.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.pgTest.SuspendLayout();
            this.pgCustomize.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblTitle.Name = "lblTitle";
            // 
            // smoothLabel1
            // 
            resources.ApplyResources(this.smoothLabel1, "smoothLabel1");
            this.smoothLabel1.AntiAliasText = false;
            this.smoothLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.smoothLabel1.Name = "smoothLabel1";
            // 
            // smoothLabel2
            // 
            resources.ApplyResources(this.smoothLabel2, "smoothLabel2");
            this.smoothLabel2.ForeColor = System.Drawing.Color.Gray;
            this.smoothLabel2.Name = "smoothLabel2";
            // 
            // dividerLine1
            // 
            resources.ApplyResources(this.dividerLine1, "dividerLine1");
            this.dividerLine1.BackColor = System.Drawing.Color.Transparent;
            this.dividerLine1.ForeColor = System.Drawing.Color.Silver;
            this.dividerLine1.GradientWidth = 10;
            this.dividerLine1.LineWidth = 1;
            this.dividerLine1.Name = "dividerLine1";
            this.dividerLine1.Vertical = true;
            // 
            // Wizard
            // 
            this.Wizard.AlwaysShowFinishButton = false;
            resources.ApplyResources(this.Wizard, "Wizard");
            this.Wizard.BackColor = System.Drawing.Color.Transparent;
            this.Wizard.CloseOnCancel = true;
            this.Wizard.CloseOnFinish = true;
            this.Wizard.Controls.Add(this.pgInfo);
            this.Wizard.Controls.Add(this.pgTrialNumber);
            this.Wizard.Controls.Add(this.pgTest);
            this.Wizard.Controls.Add(this.pgCustomize);
            this.Wizard.DisplayButtons = false;
            this.Wizard.Name = "Wizard";
            this.Wizard.PageIndex = 0;
            this.Wizard.Pages.AddRange(new global::Controls.Wizard.WizardPage[] {
            this.pgInfo,
            this.pgTrialNumber,
            this.pgTest,
            this.pgCustomize});
            this.Wizard.ShowTabs = false;
            this.Wizard.TabBackColor = System.Drawing.Color.Transparent;
            this.Wizard.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.Wizard.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.SingleLine;
            this.Wizard.BeforePageChanged += new System.EventHandler<global::Controls.Wizard.PageChangedEventArgs>(this.Wizard_BeforePageChanged);
            this.Wizard.PageChanged += new System.EventHandler(this.Wizard_PageChanged);
            // 
            // pgInfo
            // 
            this.pgInfo.Controls.Add(this.txtCompanyName);
            this.pgInfo.Controls.Add(this.label7);
            this.pgInfo.Controls.Add(this.label6);
            this.pgInfo.Controls.Add(this.cboCountry);
            this.pgInfo.Controls.Add(this.txtPhone);
            this.pgInfo.Controls.Add(this.label5);
            this.pgInfo.Controls.Add(this.txtEmail);
            this.pgInfo.Controls.Add(this.label4);
            this.pgInfo.Controls.Add(this.btnPrivacy);
            this.pgInfo.Controls.Add(this.txtName);
            this.pgInfo.Controls.Add(this.label3);
            this.pgInfo.Controls.Add(this.smoothLabel3);
            this.pgInfo.Controls.Add(this.label1);
            this.pgInfo.Controls.Add(this.reflectionPicture1);
            resources.ApplyResources(this.pgInfo, "pgInfo");
            this.pgInfo.IsFinishPage = false;
            this.pgInfo.Name = "pgInfo";
            this.pgInfo.TabLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            // 
            // txtCompanyName
            // 
            resources.ApplyResources(this.txtCompanyName, "txtCompanyName");
            this.txtCompanyName.Name = "txtCompanyName";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // cboCountry
            // 
            resources.ApplyResources(this.cboCountry, "cboCountry");
            this.cboCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCountry.FormattingEnabled = true;
            this.cboCountry.Name = "cboCountry";
            // 
            // txtPhone
            // 
            resources.ApplyResources(this.txtPhone, "txtPhone");
            this.txtPhone.Name = "txtPhone";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtEmail
            // 
            resources.ApplyResources(this.txtEmail, "txtEmail");
            this.txtEmail.Name = "txtEmail";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // btnPrivacy
            // 
            this.btnPrivacy.AntiAliasText = false;
            this.btnPrivacy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrivacy.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnPrivacy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.btnPrivacy, "btnPrivacy");
            this.btnPrivacy.Name = "btnPrivacy";
            this.btnPrivacy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrivacy.UnderlineOnHover = true;
            this.btnPrivacy.Click += new System.EventHandler(this.btnPrivacy_Click);
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // smoothLabel3
            // 
            resources.ApplyResources(this.smoothLabel3, "smoothLabel3");
            this.smoothLabel3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.smoothLabel3.Name = "smoothLabel3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // reflectionPicture1
            // 
            resources.ApplyResources(this.reflectionPicture1, "reflectionPicture1");
            this.reflectionPicture1.Image = global::CallButler.Manager.Properties.Resources.user1_mobilephone_128_shadow;
            this.reflectionPicture1.Name = "reflectionPicture1";
            // 
            // pgTrialNumber
            // 
            this.pgTrialNumber.Controls.Add(this.smoothLabel7);
            this.pgTrialNumber.Controls.Add(this.reflectionPicture4);
            this.pgTrialNumber.Controls.Add(this.wizFreeNumber);
            resources.ApplyResources(this.pgTrialNumber, "pgTrialNumber");
            this.pgTrialNumber.IsFinishPage = false;
            this.pgTrialNumber.Name = "pgTrialNumber";
            this.pgTrialNumber.TabLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            // 
            // smoothLabel7
            // 
            resources.ApplyResources(this.smoothLabel7, "smoothLabel7");
            this.smoothLabel7.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.smoothLabel7.Name = "smoothLabel7";
            // 
            // reflectionPicture4
            // 
            resources.ApplyResources(this.reflectionPicture4, "reflectionPicture4");
            this.reflectionPicture4.Image = global::CallButler.Manager.Properties.Resources.phone_redirect_128_shadow;
            this.reflectionPicture4.Name = "reflectionPicture4";
            // 
            // wizFreeNumber
            // 
            this.wizFreeNumber.AlwaysShowFinishButton = false;
            this.wizFreeNumber.CloseOnCancel = false;
            this.wizFreeNumber.CloseOnFinish = false;
            this.wizFreeNumber.Controls.Add(this.wizardPage3);
            this.wizFreeNumber.Controls.Add(this.wizardPage2);
            this.wizFreeNumber.Controls.Add(this.wizardPage1);
            this.wizFreeNumber.DisplayButtons = false;
            resources.ApplyResources(this.wizFreeNumber, "wizFreeNumber");
            this.wizFreeNumber.Name = "wizFreeNumber";
            this.wizFreeNumber.PageIndex = 2;
            this.wizFreeNumber.Pages.AddRange(new global::Controls.Wizard.WizardPage[] {
            this.wizardPage1,
            this.wizardPage2,
            this.wizardPage3});
            this.wizFreeNumber.ShowTabs = false;
            this.wizFreeNumber.TabBackColor = System.Drawing.Color.Transparent;
            this.wizFreeNumber.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.wizFreeNumber.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.SingleLine;
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.label12);
            this.wizardPage3.Controls.Add(this.smoothLabel6);
            this.wizardPage3.Controls.Add(this.label11);
            this.wizardPage3.Controls.Add(this.label10);
            resources.ApplyResources(this.wizardPage3, "wizardPage3");
            this.wizardPage3.IsFinishPage = false;
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.TabLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // smoothLabel6
            // 
            resources.ApplyResources(this.smoothLabel6, "smoothLabel6");
            this.smoothLabel6.ForeColor = System.Drawing.Color.YellowGreen;
            this.smoothLabel6.Name = "smoothLabel6";
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
            // wizardPage2
            // 
            resources.ApplyResources(this.wizardPage2, "wizardPage2");
            this.wizardPage2.IsFinishPage = false;
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.TabLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.btnTermsOfUse);
            this.wizardPage1.Controls.Add(this.btnGetFreeNumber);
            this.wizardPage1.Controls.Add(this.label14);
            this.wizardPage1.Controls.Add(this.txtFreeEmail);
            this.wizardPage1.Controls.Add(this.smoothLabel8);
            this.wizardPage1.Controls.Add(this.label13);
            this.wizardPage1.Controls.Add(this.label9);
            resources.ApplyResources(this.wizardPage1, "wizardPage1");
            this.wizardPage1.IsFinishPage = false;
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.TabLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            // 
            // btnTermsOfUse
            // 
            this.btnTermsOfUse.AntiAliasText = false;
            resources.ApplyResources(this.btnTermsOfUse, "btnTermsOfUse");
            this.btnTermsOfUse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTermsOfUse.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnTermsOfUse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTermsOfUse.Name = "btnTermsOfUse";
            this.btnTermsOfUse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTermsOfUse.UnderlineOnHover = true;
            // 
            // btnGetFreeNumber
            // 
            this.btnGetFreeNumber.BackColor = System.Drawing.Color.Azure;
            resources.ApplyResources(this.btnGetFreeNumber, "btnGetFreeNumber");
            this.btnGetFreeNumber.Image = global::CallButler.Manager.Properties.Resources.navigate_right_16;
            this.btnGetFreeNumber.Name = "btnGetFreeNumber";
            this.btnGetFreeNumber.UseVisualStyleBackColor = false;
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // txtFreeEmail
            // 
            resources.ApplyResources(this.txtFreeEmail, "txtFreeEmail");
            this.txtFreeEmail.Name = "txtFreeEmail";
            // 
            // smoothLabel8
            // 
            this.smoothLabel8.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel8, "smoothLabel8");
            this.smoothLabel8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel8.EnableHelp = true;
            this.smoothLabel8.Name = "smoothLabel8";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // pgTest
            // 
            this.pgTest.Controls.Add(this.label8);
            this.pgTest.Controls.Add(this.testDriveView);
            this.pgTest.Controls.Add(this.smoothLabel4);
            this.pgTest.Controls.Add(this.reflectionPicture2);
            resources.ApplyResources(this.pgTest, "pgTest");
            this.pgTest.IsFinishPage = false;
            this.pgTest.Name = "pgTest";
            this.pgTest.TabLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // testDriveView
            // 
            this.testDriveView.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.testDriveView, "testDriveView");
            this.testDriveView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.testDriveView.HeaderIcon = ((System.Drawing.Image)(resources.GetObject("testDriveView.HeaderIcon")));
            this.testDriveView.Name = "testDriveView";
            // 
            // smoothLabel4
            // 
            resources.ApplyResources(this.smoothLabel4, "smoothLabel4");
            this.smoothLabel4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.smoothLabel4.Name = "smoothLabel4";
            // 
            // reflectionPicture2
            // 
            resources.ApplyResources(this.reflectionPicture2, "reflectionPicture2");
            this.reflectionPicture2.Image = global::CallButler.Manager.Properties.Resources.phone_redirect_128_shadow;
            this.reflectionPicture2.Name = "reflectionPicture2";
            // 
            // pgCustomize
            // 
            this.pgCustomize.Controls.Add(this.panel1);
            this.pgCustomize.Controls.Add(this.label2);
            this.pgCustomize.Controls.Add(this.smoothLabel5);
            this.pgCustomize.Controls.Add(this.reflectionPicture3);
            resources.ApplyResources(this.pgCustomize, "pgCustomize");
            this.pgCustomize.IsFinishPage = false;
            this.pgCustomize.Name = "pgCustomize";
            this.pgCustomize.TabLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnStartUsing);
            this.panel1.Controls.Add(this.btnTour);
            this.panel1.Controls.Add(this.btnHeadStart);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnStartUsing
            // 
            this.btnStartUsing.AntiAliasText = false;
            resources.ApplyResources(this.btnStartUsing, "btnStartUsing");
            this.btnStartUsing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartUsing.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnStartUsing.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStartUsing.LinkImage = global::CallButler.Manager.Properties.Resources.gear_connection_24;
            this.btnStartUsing.Name = "btnStartUsing";
            this.btnStartUsing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStartUsing.UnderlineOnHover = true;
            this.btnStartUsing.Click += new System.EventHandler(this.btnStartUsing_Click);
            // 
            // btnTour
            // 
            this.btnTour.AntiAliasText = false;
            resources.ApplyResources(this.btnTour, "btnTour");
            this.btnTour.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTour.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnTour.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTour.LinkImage = global::CallButler.Manager.Properties.Resources.sportscar_24;
            this.btnTour.Name = "btnTour";
            this.btnTour.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTour.UnderlineOnHover = true;
            this.btnTour.Click += new System.EventHandler(this.btnTour_Click);
            // 
            // btnHeadStart
            // 
            this.btnHeadStart.AntiAliasText = false;
            resources.ApplyResources(this.btnHeadStart, "btnHeadStart");
            this.btnHeadStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHeadStart.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnHeadStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHeadStart.LinkImage = global::CallButler.Manager.Properties.Resources.office_24;
            this.btnHeadStart.Name = "btnHeadStart";
            this.btnHeadStart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHeadStart.UnderlineOnHover = true;
            this.btnHeadStart.Click += new System.EventHandler(this.btnHeadStart_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // smoothLabel5
            // 
            resources.ApplyResources(this.smoothLabel5, "smoothLabel5");
            this.smoothLabel5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.smoothLabel5.Name = "smoothLabel5";
            // 
            // reflectionPicture3
            // 
            resources.ApplyResources(this.reflectionPicture3, "reflectionPicture3");
            this.reflectionPicture3.Image = global::CallButler.Manager.Properties.Resources.step_127_shadow;
            this.reflectionPicture3.Name = "reflectionPicture3";
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Name = "btnNext";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            resources.ApplyResources(this.btnBack, "btnBack");
            this.btnBack.Name = "btnBack";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnFinish, "btnFinish");
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.UseVisualStyleBackColor = true;
            // 
            // QuickStartForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::CallButler.Manager.Properties.Resources.cb_header;
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.Wizard);
            this.Controls.Add(this.dividerLine1);
            this.Controls.Add(this.smoothLabel1);
            this.Controls.Add(this.smoothLabel2);
            this.Controls.Add(this.lblTitle);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickStartForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuickStartForm_FormClosing);
            this.Wizard.ResumeLayout(false);
            this.pgInfo.ResumeLayout(false);
            this.pgInfo.PerformLayout();
            this.pgTrialNumber.ResumeLayout(false);
            this.pgTrialNumber.PerformLayout();
            this.wizFreeNumber.ResumeLayout(false);
            this.wizardPage3.ResumeLayout(false);
            this.wizardPage3.PerformLayout();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.pgTest.ResumeLayout(false);
            this.pgTest.PerformLayout();
            this.pgCustomize.ResumeLayout(false);
            this.pgCustomize.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private global::Controls.SmoothLabel lblTitle;
        private global::Controls.SmoothLabel smoothLabel1;
        private global::Controls.SmoothLabel smoothLabel2;
        private global::Controls.DividerLine dividerLine1;
        private global::Controls.Wizard.Wizard Wizard;
        private global::Controls.Wizard.WizardPage pgInfo;
        private global::Controls.ReflectionPicture reflectionPicture1;
        private global::Controls.SmoothLabel smoothLabel3;
        private System.Windows.Forms.Label label1;
        private global::Controls.Wizard.WizardPage pgTest;
        private global::Controls.SmoothLabel smoothLabel4;
        private global::Controls.ReflectionPicture reflectionPicture2;
        private global::Controls.Wizard.WizardPage pgCustomize;
        private global::Controls.SmoothLabel smoothLabel5;
        private global::Controls.ReflectionPicture reflectionPicture3;
        private System.Windows.Forms.Label label2;
        private global::Controls.LinkButton btnTour;
        private global::Controls.LinkButton btnStartUsing;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label4;
        private global::Controls.LinkButton btnPrivacy;
        private System.Windows.Forms.Label label6;
        private global::Controls.CountryComboBox cboCountry;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnFinish;
        private CallButler.Manager.ViewControls.TestDriveView testDriveView;
        private System.Windows.Forms.Label label8;
        private global::Controls.LinkButton btnHeadStart;
        private global::Controls.Wizard.WizardPage pgTrialNumber;
        private global::Controls.SmoothLabel smoothLabel6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private global::Controls.SmoothLabel smoothLabel7;
        private global::Controls.ReflectionPicture reflectionPicture4;
        private System.Windows.Forms.Label label12;
        private global::Controls.Wizard.Wizard wizFreeNumber;
        private global::Controls.Wizard.WizardPage wizardPage1;
        private global::Controls.SmoothLabel smoothLabel8;
        private System.Windows.Forms.Label label13;
        private global::Controls.Wizard.WizardPage wizardPage2;
        private global::Controls.Wizard.WizardPage wizardPage3;
        private System.Windows.Forms.TextBox txtFreeEmail;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnGetFreeNumber;
        private global::Controls.LinkButton btnTermsOfUse;
    }
}
