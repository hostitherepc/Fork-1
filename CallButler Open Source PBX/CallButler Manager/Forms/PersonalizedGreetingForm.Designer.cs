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
    partial class PersonalizedGreetingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonalizedGreetingForm));
            this.wizard = new global::Controls.Wizard.Wizard();
            this.wizardPage2 = new global::Controls.Wizard.WizardPage();
            this.lblModule = new global::Controls.SmoothLabel();
            this.rbModule = new System.Windows.Forms.RadioButton();
            this.lblScript = new global::Controls.SmoothLabel();
            this.lblHangUp = new global::Controls.SmoothLabel();
            this.lblTransfer = new global::Controls.SmoothLabel();
            this.lblContinueCall = new global::Controls.SmoothLabel();
            this.wizType = new global::Controls.Wizard.Wizard();
            this.wizardPage5 = new global::Controls.Wizard.WizardPage();
            this.addOnModuleChooserControl = new CallButler.Manager.Controls.AddOnModuleChooserControl();
            this.wizardPage4 = new global::Controls.Wizard.WizardPage();
            this.extensionsView = new CallButler.Manager.ViewControls.ExtensionsView();
            this.label6 = new System.Windows.Forms.Label();
            this.wizardPage3 = new global::Controls.Wizard.WizardPage();
            this.smoothLabel3 = new global::Controls.SmoothLabel();
            this.txtScriptFile = new System.Windows.Forms.TextBox();
            this.btnScriptBrowse = new global::Controls.LinkButton();
            this.rbExtension = new System.Windows.Forms.RadioButton();
            this.wizardHeader3 = new CallButler.Manager.Controls.WizardHeader();
            this.rbScript = new System.Windows.Forms.RadioButton();
            this.rbHangup = new System.Windows.Forms.RadioButton();
            this.rbContinue = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.wizardPage1 = new global::Controls.Wizard.WizardPage();
            this.greetingControl = new CallButler.Manager.Controls.GreetingControl();
            this.wizardHeader2 = new CallButler.Manager.Controls.WizardHeader();
            this.pgGeneral = new global::Controls.Wizard.WizardPage();
            this.txtDialedNumber = new System.Windows.Forms.TextBox();
            this.smoothLabel5 = new global::Controls.SmoothLabel();
            this.smoothLabel4 = new global::Controls.SmoothLabel();
            this.smoothLabel2 = new global::Controls.SmoothLabel();
            this.lblOneTime = new global::Controls.SmoothLabel();
            this.lblRegex = new global::Controls.SmoothLabel();
            this.smoothLabel1 = new global::Controls.SmoothLabel();
            this.lblCallerID = new global::Controls.SmoothLabel();
            this.cbPlayOnce = new System.Windows.Forms.CheckBox();
            this.cbRegex = new System.Windows.Forms.CheckBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnImportOutlook = new global::Controls.LinkButton();
            this.txtTelephoneNumber = new System.Windows.Forms.TextBox();
            this.txtCallerID = new System.Windows.Forms.TextBox();
            this.wizardHeader1 = new CallButler.Manager.Controls.WizardHeader();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.wizard.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.wizType.SuspendLayout();
            this.wizardPage5.SuspendLayout();
            this.wizardPage4.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.pgGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard
            // 
            this.wizard.AlwaysShowFinishButton = true;
            this.wizard.CloseOnCancel = true;
            this.wizard.CloseOnFinish = true;
            this.wizard.Controls.Add(this.wizardPage2);
            this.wizard.Controls.Add(this.wizardPage1);
            this.wizard.Controls.Add(this.pgGeneral);
            this.wizard.DisplayButtons = true;
            resources.ApplyResources(this.wizard, "wizard");
            this.wizard.Name = "wizard";
            this.wizard.PageIndex = 2;
            this.wizard.Pages.AddRange(new global::Controls.Wizard.WizardPage[] {
            this.pgGeneral,
            this.wizardPage1,
            this.wizardPage2});
            this.wizard.ShowTabs = true;
            this.wizard.TabBackColor = System.Drawing.Color.WhiteSmoke;
            this.wizard.TabBackgroundImage = global::CallButler.Manager.Properties.Resources.wizard_header;
            this.wizard.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.wizard.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.Cutout;
            this.wizard.WizardFinished += new System.EventHandler<global::Controls.Wizard.PageChangedEventArgs>(this.wizard_WizardFinished);
            this.wizard.PageChanged += new System.EventHandler(this.wizard_PageChanged);
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.lblModule);
            this.wizardPage2.Controls.Add(this.rbModule);
            this.wizardPage2.Controls.Add(this.lblScript);
            this.wizardPage2.Controls.Add(this.lblHangUp);
            this.wizardPage2.Controls.Add(this.lblTransfer);
            this.wizardPage2.Controls.Add(this.lblContinueCall);
            this.wizardPage2.Controls.Add(this.wizType);
            this.wizardPage2.Controls.Add(this.rbExtension);
            this.wizardPage2.Controls.Add(this.wizardHeader3);
            this.wizardPage2.Controls.Add(this.rbScript);
            this.wizardPage2.Controls.Add(this.rbHangup);
            this.wizardPage2.Controls.Add(this.rbContinue);
            this.wizardPage2.Controls.Add(this.label4);
            resources.ApplyResources(this.wizardPage2, "wizardPage2");
            this.wizardPage2.IsFinishPage = false;
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // lblModule
            // 
            this.lblModule.AntiAliasText = false;
            resources.ApplyResources(this.lblModule, "lblModule");
            this.lblModule.BackColor = System.Drawing.Color.Transparent;
            this.lblModule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblModule.EnableHelp = true;
            this.lblModule.Name = "lblModule";
            this.lblModule.Click += new System.EventHandler(this.lblModule_Click);
            // 
            // rbModule
            // 
            resources.ApplyResources(this.rbModule, "rbModule");
            this.rbModule.Name = "rbModule";
            this.rbModule.TabStop = true;
            this.rbModule.UseVisualStyleBackColor = true;
            this.rbModule.CheckedChanged += new System.EventHandler(this.Type_CheckedChanged);
            // 
            // lblScript
            // 
            this.lblScript.AntiAliasText = false;
            resources.ApplyResources(this.lblScript, "lblScript");
            this.lblScript.BackColor = System.Drawing.Color.Transparent;
            this.lblScript.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblScript.EnableHelp = true;
            this.lblScript.Name = "lblScript";
            this.lblScript.Click += new System.EventHandler(this.lblScript_Click);
            // 
            // lblHangUp
            // 
            this.lblHangUp.AntiAliasText = false;
            resources.ApplyResources(this.lblHangUp, "lblHangUp");
            this.lblHangUp.BackColor = System.Drawing.Color.Transparent;
            this.lblHangUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHangUp.EnableHelp = true;
            this.lblHangUp.Name = "lblHangUp";
            this.lblHangUp.Click += new System.EventHandler(this.lblHangUp_Click);
            // 
            // lblTransfer
            // 
            this.lblTransfer.AntiAliasText = false;
            resources.ApplyResources(this.lblTransfer, "lblTransfer");
            this.lblTransfer.BackColor = System.Drawing.Color.Transparent;
            this.lblTransfer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTransfer.EnableHelp = true;
            this.lblTransfer.Name = "lblTransfer";
            this.lblTransfer.Click += new System.EventHandler(this.lblTransfer_Click);
            // 
            // lblContinueCall
            // 
            this.lblContinueCall.AntiAliasText = false;
            resources.ApplyResources(this.lblContinueCall, "lblContinueCall");
            this.lblContinueCall.BackColor = System.Drawing.Color.Transparent;
            this.lblContinueCall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblContinueCall.EnableHelp = true;
            this.lblContinueCall.Name = "lblContinueCall";
            this.lblContinueCall.Click += new System.EventHandler(this.lblContinueCall_Click);
            // 
            // wizType
            // 
            this.wizType.AlwaysShowFinishButton = false;
            this.wizType.CloseOnCancel = false;
            this.wizType.CloseOnFinish = false;
            this.wizType.Controls.Add(this.wizardPage5);
            this.wizType.Controls.Add(this.wizardPage4);
            this.wizType.Controls.Add(this.wizardPage3);
            this.wizType.DisplayButtons = false;
            resources.ApplyResources(this.wizType, "wizType");
            this.wizType.Name = "wizType";
            this.wizType.PageIndex = 2;
            this.wizType.Pages.AddRange(new global::Controls.Wizard.WizardPage[] {
            this.wizardPage3,
            this.wizardPage4,
            this.wizardPage5});
            this.wizType.ShowTabs = false;
            this.wizType.TabBackColor = System.Drawing.Color.WhiteSmoke;
            this.wizType.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.wizType.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.SingleLine;
            // 
            // wizardPage5
            // 
            this.wizardPage5.Controls.Add(this.addOnModuleChooserControl);
            resources.ApplyResources(this.wizardPage5, "wizardPage5");
            this.wizardPage5.IsFinishPage = false;
            this.wizardPage5.Name = "wizardPage5";
            this.wizardPage5.TabLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            // 
            // addOnModuleChooserControl
            // 
            resources.ApplyResources(this.addOnModuleChooserControl, "addOnModuleChooserControl");
            this.addOnModuleChooserControl.Name = "addOnModuleChooserControl";
            this.addOnModuleChooserControl.SelectedAddOnModule = new System.Guid("00000000-0000-0000-0000-000000000000");
            // 
            // wizardPage4
            // 
            this.wizardPage4.Controls.Add(this.extensionsView);
            this.wizardPage4.Controls.Add(this.label6);
            resources.ApplyResources(this.wizardPage4, "wizardPage4");
            this.wizardPage4.IsFinishPage = false;
            this.wizardPage4.Name = "wizardPage4";
            this.wizardPage4.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // extensionsView
            // 
            this.extensionsView.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.extensionsView, "extensionsView");
            this.extensionsView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.extensionsView.HeaderIcon = ((System.Drawing.Image)(resources.GetObject("extensionsView.HeaderIcon")));
            this.extensionsView.Name = "extensionsView";
            this.extensionsView.ShowHelpPanel = false;
            this.extensionsView.ShowVoicemailColumn = false;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.smoothLabel3);
            this.wizardPage3.Controls.Add(this.txtScriptFile);
            this.wizardPage3.Controls.Add(this.btnScriptBrowse);
            resources.ApplyResources(this.wizardPage3, "wizardPage3");
            this.wizardPage3.IsFinishPage = false;
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.TabLinkColor = System.Drawing.SystemColors.ControlText;
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
            // txtScriptFile
            // 
            resources.ApplyResources(this.txtScriptFile, "txtScriptFile");
            this.txtScriptFile.Name = "txtScriptFile";
            // 
            // btnScriptBrowse
            // 
            this.btnScriptBrowse.AntiAliasText = false;
            this.btnScriptBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScriptBrowse.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnScriptBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScriptBrowse.LinkImage = global::CallButler.Manager.Properties.Resources.folder_16;
            resources.ApplyResources(this.btnScriptBrowse, "btnScriptBrowse");
            this.btnScriptBrowse.Name = "btnScriptBrowse";
            this.btnScriptBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScriptBrowse.UnderlineOnHover = true;
            this.btnScriptBrowse.Click += new System.EventHandler(this.btnScriptBrowse_Click);
            // 
            // rbExtension
            // 
            resources.ApplyResources(this.rbExtension, "rbExtension");
            this.rbExtension.Name = "rbExtension";
            this.rbExtension.TabStop = true;
            this.rbExtension.UseVisualStyleBackColor = true;
            this.rbExtension.CheckedChanged += new System.EventHandler(this.Type_CheckedChanged);
            // 
            // wizardHeader3
            // 
            resources.ApplyResources(this.wizardHeader3, "wizardHeader3");
            this.wizardHeader3.Image = global::CallButler.Manager.Properties.Resources.toolbox_48_shadow;
            this.wizardHeader3.Name = "wizardHeader3";
            // 
            // rbScript
            // 
            resources.ApplyResources(this.rbScript, "rbScript");
            this.rbScript.Name = "rbScript";
            this.rbScript.TabStop = true;
            this.rbScript.UseVisualStyleBackColor = true;
            this.rbScript.CheckedChanged += new System.EventHandler(this.Type_CheckedChanged);
            // 
            // rbHangup
            // 
            resources.ApplyResources(this.rbHangup, "rbHangup");
            this.rbHangup.Name = "rbHangup";
            this.rbHangup.TabStop = true;
            this.rbHangup.UseVisualStyleBackColor = true;
            this.rbHangup.CheckedChanged += new System.EventHandler(this.Type_CheckedChanged);
            // 
            // rbContinue
            // 
            resources.ApplyResources(this.rbContinue, "rbContinue");
            this.rbContinue.Name = "rbContinue";
            this.rbContinue.TabStop = true;
            this.rbContinue.UseVisualStyleBackColor = true;
            this.rbContinue.CheckedChanged += new System.EventHandler(this.Type_CheckedChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.greetingControl);
            this.wizardPage1.Controls.Add(this.wizardHeader2);
            resources.ApplyResources(this.wizardPage1, "wizardPage1");
            this.wizardPage1.IsFinishPage = false;
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // greetingControl
            // 
            this.greetingControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.greetingControl.CallToNumber = "";
            resources.ApplyResources(this.greetingControl, "greetingControl");
            this.greetingControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.greetingControl.Name = "greetingControl";
            // 
            // wizardHeader2
            // 
            resources.ApplyResources(this.wizardHeader2, "wizardHeader2");
            this.wizardHeader2.Image = global::CallButler.Manager.Properties.Resources.toolbox_48_shadow;
            this.wizardHeader2.Name = "wizardHeader2";
            // 
            // pgGeneral
            // 
            this.pgGeneral.Controls.Add(this.txtDialedNumber);
            this.pgGeneral.Controls.Add(this.smoothLabel5);
            this.pgGeneral.Controls.Add(this.smoothLabel4);
            this.pgGeneral.Controls.Add(this.smoothLabel2);
            this.pgGeneral.Controls.Add(this.lblOneTime);
            this.pgGeneral.Controls.Add(this.lblRegex);
            this.pgGeneral.Controls.Add(this.smoothLabel1);
            this.pgGeneral.Controls.Add(this.lblCallerID);
            this.pgGeneral.Controls.Add(this.cbPlayOnce);
            this.pgGeneral.Controls.Add(this.cbRegex);
            this.pgGeneral.Controls.Add(this.txtNotes);
            this.pgGeneral.Controls.Add(this.btnImportOutlook);
            this.pgGeneral.Controls.Add(this.txtTelephoneNumber);
            this.pgGeneral.Controls.Add(this.txtCallerID);
            this.pgGeneral.Controls.Add(this.wizardHeader1);
            resources.ApplyResources(this.pgGeneral, "pgGeneral");
            this.pgGeneral.IsFinishPage = false;
            this.pgGeneral.Name = "pgGeneral";
            this.pgGeneral.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // txtDialedNumber
            // 
            resources.ApplyResources(this.txtDialedNumber, "txtDialedNumber");
            this.txtDialedNumber.Name = "txtDialedNumber";
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
            // lblOneTime
            // 
            this.lblOneTime.AntiAliasText = false;
            resources.ApplyResources(this.lblOneTime, "lblOneTime");
            this.lblOneTime.BackColor = System.Drawing.Color.Transparent;
            this.lblOneTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblOneTime.EnableHelp = true;
            this.lblOneTime.Name = "lblOneTime";
            this.lblOneTime.Click += new System.EventHandler(this.lblOneTime_Click);
            // 
            // lblRegex
            // 
            this.lblRegex.AntiAliasText = false;
            resources.ApplyResources(this.lblRegex, "lblRegex");
            this.lblRegex.BackColor = System.Drawing.Color.Transparent;
            this.lblRegex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblRegex.EnableHelp = true;
            this.lblRegex.Name = "lblRegex";
            this.lblRegex.Click += new System.EventHandler(this.lblRegex_Click);
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
            // lblCallerID
            // 
            this.lblCallerID.AntiAliasText = false;
            resources.ApplyResources(this.lblCallerID, "lblCallerID");
            this.lblCallerID.BackColor = System.Drawing.Color.Transparent;
            this.lblCallerID.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCallerID.EnableHelp = true;
            this.lblCallerID.Name = "lblCallerID";
            // 
            // cbPlayOnce
            // 
            resources.ApplyResources(this.cbPlayOnce, "cbPlayOnce");
            this.cbPlayOnce.Name = "cbPlayOnce";
            this.cbPlayOnce.UseVisualStyleBackColor = true;
            // 
            // cbRegex
            // 
            resources.ApplyResources(this.cbRegex, "cbRegex");
            this.cbRegex.Name = "cbRegex";
            this.cbRegex.UseVisualStyleBackColor = true;
            // 
            // txtNotes
            // 
            resources.ApplyResources(this.txtNotes, "txtNotes");
            this.txtNotes.Name = "txtNotes";
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
            // txtTelephoneNumber
            // 
            resources.ApplyResources(this.txtTelephoneNumber, "txtTelephoneNumber");
            this.txtTelephoneNumber.Name = "txtTelephoneNumber";
            // 
            // txtCallerID
            // 
            resources.ApplyResources(this.txtCallerID, "txtCallerID");
            this.txtCallerID.Name = "txtCallerID";
            // 
            // wizardHeader1
            // 
            resources.ApplyResources(this.wizardHeader1, "wizardHeader1");
            this.wizardHeader1.Image = global::CallButler.Manager.Properties.Resources.toolbox_48_shadow;
            this.wizardHeader1.Name = "wizardHeader1";
            // 
            // openFileDialog
            // 
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            // 
            // PersonalizedGreetingForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.wizard);
            this.Name = "PersonalizedGreetingForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PersonalizedGreetingForm_FormClosing);
            this.wizard.ResumeLayout(false);
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            this.wizType.ResumeLayout(false);
            this.wizardPage5.ResumeLayout(false);
            this.wizardPage4.ResumeLayout(false);
            this.wizardPage3.ResumeLayout(false);
            this.wizardPage3.PerformLayout();
            this.wizardPage1.ResumeLayout(false);
            this.pgGeneral.ResumeLayout(false);
            this.pgGeneral.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private global::Controls.Wizard.Wizard wizard;
        private global::Controls.Wizard.WizardPage pgGeneral;
        private CallButler.Manager.Controls.WizardHeader wizardHeader1;
        private global::Controls.Wizard.WizardPage wizardPage1;
        private CallButler.Manager.Controls.WizardHeader wizardHeader2;
        private System.Windows.Forms.TextBox txtTelephoneNumber;
        private System.Windows.Forms.TextBox txtCallerID;
        private global::Controls.Wizard.WizardPage wizardPage2;
        private global::Controls.LinkButton btnImportOutlook;
        private System.Windows.Forms.TextBox txtScriptFile;
        private global::Controls.LinkButton btnScriptBrowse;
        private System.Windows.Forms.RadioButton rbScript;
        private System.Windows.Forms.RadioButton rbHangup;
        private System.Windows.Forms.RadioButton rbContinue;
        private System.Windows.Forms.Label label4;
        private CallButler.Manager.Controls.WizardHeader wizardHeader3;
        private CallButler.Manager.Controls.GreetingControl greetingControl;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.RadioButton rbExtension;
        private global::Controls.Wizard.Wizard wizType;
        private global::Controls.Wizard.WizardPage wizardPage3;
        private global::Controls.Wizard.WizardPage wizardPage4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbRegex;
        private System.Windows.Forms.CheckBox cbPlayOnce;
        private global::Controls.SmoothLabel lblCallerID;
        private global::Controls.SmoothLabel smoothLabel1;
        private global::Controls.SmoothLabel lblRegex;
        private global::Controls.SmoothLabel lblOneTime;
        private global::Controls.SmoothLabel smoothLabel2;
        private global::Controls.SmoothLabel lblContinueCall;
        private global::Controls.SmoothLabel lblTransfer;
        private global::Controls.SmoothLabel lblHangUp;
        private global::Controls.SmoothLabel lblScript;
        private global::Controls.SmoothLabel smoothLabel3;
        private global::Controls.SmoothLabel smoothLabel4;
        private System.Windows.Forms.TextBox txtDialedNumber;
        private global::Controls.SmoothLabel smoothLabel5;
        private CallButler.Manager.ViewControls.ExtensionsView extensionsView;
        private System.Windows.Forms.RadioButton rbModule;
        private global::Controls.SmoothLabel lblModule;
        private global::Controls.Wizard.WizardPage wizardPage5;
        private CallButler.Manager.Controls.AddOnModuleChooserControl addOnModuleChooserControl;
    }
}
