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
    partial class DepartmentForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepartmentForm));
            this.wizard = new global::Controls.Wizard.Wizard();
            this.pgSpecificSettings = new global::Controls.Wizard.WizardPage();
            this.wzdDepartmentDetails = new global::Controls.Wizard.Wizard();
            this.pgAddonModule = new global::Controls.Wizard.WizardPage();
            this.addOnModuleChooserControl = new CallButler.Manager.Controls.AddOnModuleChooserControl();
            this.wizardHeader6 = new CallButler.Manager.Controls.WizardHeader();
            this.wizardPage7 = new global::Controls.Wizard.WizardPage();
            this.smoothLabel1 = new global::Controls.SmoothLabel();
            this.wizardHeader4 = new CallButler.Manager.Controls.WizardHeader();
            this.txtScriptFile = new System.Windows.Forms.TextBox();
            this.btnScriptBrowse = new global::Controls.LinkButton();
            this.wizardPage5 = new global::Controls.Wizard.WizardPage();
            this.lblNumberDescription = new global::Controls.SmoothLabel();
            this.wizardHeader3 = new CallButler.Manager.Controls.WizardHeader();
            this.btnImportOutlook = new global::Controls.LinkButton();
            this.txtTelephoneNumber = new System.Windows.Forms.TextBox();
            this.pgExtensionSelector = new global::Controls.Wizard.WizardPage();
            this.extensionsView = new CallButler.Manager.ViewControls.ExtensionsView();
            this.wizardHeader2 = new CallButler.Manager.Controls.WizardHeader();
            this.wizardPage3 = new global::Controls.Wizard.WizardPage();
            this.wizardHeader1 = new CallButler.Manager.Controls.WizardHeader();
            this.greetingControl = new CallButler.Manager.Controls.GreetingControl();
            this.pgGeneralSettings = new global::Controls.Wizard.WizardPage();
            this.numOptionNumber = new System.Windows.Forms.NumericUpDown();
            this.smoothLabel2 = new global::Controls.SmoothLabel();
            this.lblDepartmentName = new global::Controls.SmoothLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAddon = new global::Controls.SmoothLabel();
            this.lblScript = new global::Controls.SmoothLabel();
            this.rbScript = new System.Windows.Forms.RadioButton();
            this.rbAddon = new System.Windows.Forms.RadioButton();
            this.lblNumber = new global::Controls.SmoothLabel();
            this.lblExtension = new global::Controls.SmoothLabel();
            this.lblMessage = new global::Controls.SmoothLabel();
            this.rbTransferNumber = new System.Windows.Forms.RadioButton();
            this.rbTransferExtension = new System.Windows.Forms.RadioButton();
            this.rbPlayMessage = new System.Windows.Forms.RadioButton();
            this.wizardHeader5 = new CallButler.Manager.Controls.WizardHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDepartmentName = new System.Windows.Forms.TextBox();
            this.callButlerDataset = new WOSI.CallButler.Data.CallButlerDataset();
            this.bsMailboxes = new System.Windows.Forms.BindingSource(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.wizard.SuspendLayout();
            this.pgSpecificSettings.SuspendLayout();
            this.wzdDepartmentDetails.SuspendLayout();
            this.pgAddonModule.SuspendLayout();
            this.wizardPage7.SuspendLayout();
            this.wizardPage5.SuspendLayout();
            this.pgExtensionSelector.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            this.pgGeneralSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOptionNumber)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.callButlerDataset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMailboxes)).BeginInit();
            this.SuspendLayout();
            // 
            // wizard
            // 
            this.wizard.AlwaysShowFinishButton = true;
            this.wizard.CloseOnCancel = true;
            this.wizard.CloseOnFinish = true;
            this.wizard.Controls.Add(this.pgSpecificSettings);
            this.wizard.Controls.Add(this.pgGeneralSettings);
            this.wizard.DisplayButtons = true;
            resources.ApplyResources(this.wizard, "wizard");
            this.wizard.Name = "wizard";
            this.wizard.PageIndex = 1;
            this.wizard.Pages.AddRange(new global::Controls.Wizard.WizardPage[] {
            this.pgGeneralSettings,
            this.pgSpecificSettings});
            this.wizard.ShowTabs = true;
            this.wizard.TabBackColor = System.Drawing.Color.WhiteSmoke;
            this.wizard.TabBackgroundImage = global::CallButler.Manager.Properties.Resources.wizard_header;
            this.wizard.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.wizard.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.Cutout;
            this.wizard.WizardFinished += new System.EventHandler<global::Controls.Wizard.PageChangedEventArgs>(this.wizard_WizardFinished);
            // 
            // pgSpecificSettings
            // 
            this.pgSpecificSettings.Controls.Add(this.wzdDepartmentDetails);
            resources.ApplyResources(this.pgSpecificSettings, "pgSpecificSettings");
            this.pgSpecificSettings.IsFinishPage = false;
            this.pgSpecificSettings.Name = "pgSpecificSettings";
            this.pgSpecificSettings.TabLinkColor = System.Drawing.SystemColors.ControlText;
            this.pgSpecificSettings.ShowFromNext += new System.EventHandler(this.wizardPage2_ShowFromNext);
            // 
            // wzdDepartmentDetails
            // 
            this.wzdDepartmentDetails.AlwaysShowFinishButton = false;
            this.wzdDepartmentDetails.CloseOnCancel = false;
            this.wzdDepartmentDetails.CloseOnFinish = false;
            this.wzdDepartmentDetails.Controls.Add(this.pgExtensionSelector);
            this.wzdDepartmentDetails.Controls.Add(this.wizardPage5);
            this.wzdDepartmentDetails.Controls.Add(this.wizardPage7);
            this.wzdDepartmentDetails.Controls.Add(this.pgAddonModule);
            this.wzdDepartmentDetails.Controls.Add(this.wizardPage3);
            this.wzdDepartmentDetails.DisplayButtons = false;
            resources.ApplyResources(this.wzdDepartmentDetails, "wzdDepartmentDetails");
            this.wzdDepartmentDetails.Name = "wzdDepartmentDetails";
            this.wzdDepartmentDetails.PageIndex = 1;
            this.wzdDepartmentDetails.Pages.AddRange(new global::Controls.Wizard.WizardPage[] {
            this.wizardPage3,
            this.pgExtensionSelector,
            this.wizardPage5,
            this.wizardPage7,
            this.pgAddonModule});
            this.wzdDepartmentDetails.ShowTabs = false;
            this.wzdDepartmentDetails.TabBackColor = System.Drawing.Color.WhiteSmoke;
            this.wzdDepartmentDetails.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.wzdDepartmentDetails.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.SingleLine;
            this.wzdDepartmentDetails.PageChanged += new System.EventHandler(this.wzdDepartmentDetails_PageChanged);
            // 
            // pgAddonModule
            // 
            this.pgAddonModule.Controls.Add(this.addOnModuleChooserControl);
            this.pgAddonModule.Controls.Add(this.wizardHeader6);
            resources.ApplyResources(this.pgAddonModule, "pgAddonModule");
            this.pgAddonModule.IsFinishPage = false;
            this.pgAddonModule.Name = "pgAddonModule";
            this.pgAddonModule.TabLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            // 
            // addOnModuleChooserControl
            // 
            resources.ApplyResources(this.addOnModuleChooserControl, "addOnModuleChooserControl");
            this.addOnModuleChooserControl.Name = "addOnModuleChooserControl";
            this.addOnModuleChooserControl.SelectedAddOnModule = new System.Guid("00000000-0000-0000-0000-000000000000");
            // 
            // wizardHeader6
            // 
            resources.ApplyResources(this.wizardHeader6, "wizardHeader6");
            this.wizardHeader6.Image = global::CallButler.Manager.Properties.Resources.office_building_48_shadow;
            this.wizardHeader6.Name = "wizardHeader6";
            // 
            // wizardPage7
            // 
            this.wizardPage7.Controls.Add(this.smoothLabel1);
            this.wizardPage7.Controls.Add(this.wizardHeader4);
            this.wizardPage7.Controls.Add(this.txtScriptFile);
            this.wizardPage7.Controls.Add(this.btnScriptBrowse);
            resources.ApplyResources(this.wizardPage7, "wizardPage7");
            this.wizardPage7.IsFinishPage = false;
            this.wizardPage7.Name = "wizardPage7";
            this.wizardPage7.TabLinkColor = System.Drawing.SystemColors.ControlText;
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
            // wizardHeader4
            // 
            resources.ApplyResources(this.wizardHeader4, "wizardHeader4");
            this.wizardHeader4.Image = global::CallButler.Manager.Properties.Resources.office_building_48_shadow;
            this.wizardHeader4.Name = "wizardHeader4";
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
            // wizardPage5
            // 
            this.wizardPage5.Controls.Add(this.lblNumberDescription);
            this.wizardPage5.Controls.Add(this.wizardHeader3);
            this.wizardPage5.Controls.Add(this.btnImportOutlook);
            this.wizardPage5.Controls.Add(this.txtTelephoneNumber);
            resources.ApplyResources(this.wizardPage5, "wizardPage5");
            this.wizardPage5.IsFinishPage = false;
            this.wizardPage5.Name = "wizardPage5";
            this.wizardPage5.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // lblNumberDescription
            // 
            this.lblNumberDescription.AntiAliasText = false;
            resources.ApplyResources(this.lblNumberDescription, "lblNumberDescription");
            this.lblNumberDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblNumberDescription.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNumberDescription.EnableHelp = true;
            this.lblNumberDescription.Name = "lblNumberDescription";
            // 
            // wizardHeader3
            // 
            resources.ApplyResources(this.wizardHeader3, "wizardHeader3");
            this.wizardHeader3.Image = global::CallButler.Manager.Properties.Resources.office_building_48_shadow;
            this.wizardHeader3.Name = "wizardHeader3";
            // 
            // btnImportOutlook
            // 
            this.btnImportOutlook.AntiAliasText = false;
            this.btnImportOutlook.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportOutlook.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnImportOutlook.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportOutlook.LinkImage = global::CallButler.Manager.Properties.Resources.outlook_16;
            resources.ApplyResources(this.btnImportOutlook, "btnImportOutlook");
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
            // pgExtensionSelector
            // 
            this.pgExtensionSelector.Controls.Add(this.extensionsView);
            this.pgExtensionSelector.Controls.Add(this.wizardHeader2);
            resources.ApplyResources(this.pgExtensionSelector, "pgExtensionSelector");
            this.pgExtensionSelector.IsFinishPage = false;
            this.pgExtensionSelector.Name = "pgExtensionSelector";
            this.pgExtensionSelector.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // extensionsView
            // 
            resources.ApplyResources(this.extensionsView, "extensionsView");
            this.extensionsView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.extensionsView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.extensionsView.HeaderIcon = ((System.Drawing.Image)(resources.GetObject("extensionsView.HeaderIcon")));
            this.extensionsView.Name = "extensionsView";
            this.extensionsView.ShowHelpPanel = false;
            this.extensionsView.ShowVoicemailColumn = false;
            this.extensionsView.VisibleChanged += new System.EventHandler(this.extensionsView_VisibleChanged);
            // 
            // wizardHeader2
            // 
            resources.ApplyResources(this.wizardHeader2, "wizardHeader2");
            this.wizardHeader2.Image = global::CallButler.Manager.Properties.Resources.office_building_48_shadow;
            this.wizardHeader2.Name = "wizardHeader2";
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.wizardHeader1);
            this.wizardPage3.Controls.Add(this.greetingControl);
            resources.ApplyResources(this.wizardPage3, "wizardPage3");
            this.wizardPage3.IsFinishPage = false;
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // wizardHeader1
            // 
            resources.ApplyResources(this.wizardHeader1, "wizardHeader1");
            this.wizardHeader1.Image = global::CallButler.Manager.Properties.Resources.office_building_48_shadow;
            this.wizardHeader1.Name = "wizardHeader1";
            // 
            // greetingControl
            // 
            this.greetingControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.greetingControl.CallToNumber = "";
            resources.ApplyResources(this.greetingControl, "greetingControl");
            this.greetingControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.greetingControl.Name = "greetingControl";
            // 
            // pgGeneralSettings
            // 
            this.pgGeneralSettings.Controls.Add(this.numOptionNumber);
            this.pgGeneralSettings.Controls.Add(this.smoothLabel2);
            this.pgGeneralSettings.Controls.Add(this.lblDepartmentName);
            this.pgGeneralSettings.Controls.Add(this.panel1);
            this.pgGeneralSettings.Controls.Add(this.wizardHeader5);
            this.pgGeneralSettings.Controls.Add(this.label1);
            this.pgGeneralSettings.Controls.Add(this.txtDepartmentName);
            resources.ApplyResources(this.pgGeneralSettings, "pgGeneralSettings");
            this.pgGeneralSettings.IsFinishPage = false;
            this.pgGeneralSettings.Name = "pgGeneralSettings";
            this.pgGeneralSettings.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // numOptionNumber
            // 
            resources.ApplyResources(this.numOptionNumber, "numOptionNumber");
            this.numOptionNumber.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numOptionNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numOptionNumber.Name = "numOptionNumber";
            this.numOptionNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            // lblDepartmentName
            // 
            this.lblDepartmentName.AntiAliasText = false;
            resources.ApplyResources(this.lblDepartmentName, "lblDepartmentName");
            this.lblDepartmentName.BackColor = System.Drawing.Color.Transparent;
            this.lblDepartmentName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDepartmentName.EnableHelp = true;
            this.lblDepartmentName.Name = "lblDepartmentName";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblAddon);
            this.panel1.Controls.Add(this.lblScript);
            this.panel1.Controls.Add(this.rbScript);
            this.panel1.Controls.Add(this.rbAddon);
            this.panel1.Controls.Add(this.lblNumber);
            this.panel1.Controls.Add(this.lblExtension);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Controls.Add(this.rbTransferNumber);
            this.panel1.Controls.Add(this.rbTransferExtension);
            this.panel1.Controls.Add(this.rbPlayMessage);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // lblAddon
            // 
            this.lblAddon.AntiAliasText = false;
            resources.ApplyResources(this.lblAddon, "lblAddon");
            this.lblAddon.BackColor = System.Drawing.Color.Transparent;
            this.lblAddon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAddon.EnableHelp = true;
            this.lblAddon.Name = "lblAddon";
            this.lblAddon.Click += new System.EventHandler(this.lblAddon_Click);
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
            // rbScript
            // 
            resources.ApplyResources(this.rbScript, "rbScript");
            this.rbScript.Name = "rbScript";
            this.rbScript.UseVisualStyleBackColor = true;
            this.rbScript.CheckedChanged += new System.EventHandler(this.rbDepartmentType_CheckedChanged);
            // 
            // rbAddon
            // 
            resources.ApplyResources(this.rbAddon, "rbAddon");
            this.rbAddon.Name = "rbAddon";
            this.rbAddon.UseVisualStyleBackColor = true;
            // 
            // lblNumber
            // 
            this.lblNumber.AntiAliasText = false;
            resources.ApplyResources(this.lblNumber, "lblNumber");
            this.lblNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNumber.EnableHelp = true;
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Click += new System.EventHandler(this.lblNumber_Click);
            // 
            // lblExtension
            // 
            this.lblExtension.AntiAliasText = false;
            resources.ApplyResources(this.lblExtension, "lblExtension");
            this.lblExtension.BackColor = System.Drawing.Color.Transparent;
            this.lblExtension.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblExtension.EnableHelp = true;
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Click += new System.EventHandler(this.lblExtension_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AntiAliasText = false;
            resources.ApplyResources(this.lblMessage, "lblMessage");
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMessage.EnableHelp = true;
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Click += new System.EventHandler(this.lblMessage_Click);
            // 
            // rbTransferNumber
            // 
            resources.ApplyResources(this.rbTransferNumber, "rbTransferNumber");
            this.rbTransferNumber.Name = "rbTransferNumber";
            this.rbTransferNumber.UseVisualStyleBackColor = true;
            this.rbTransferNumber.CheckedChanged += new System.EventHandler(this.rbDepartmentType_CheckedChanged);
            // 
            // rbTransferExtension
            // 
            resources.ApplyResources(this.rbTransferExtension, "rbTransferExtension");
            this.rbTransferExtension.Name = "rbTransferExtension";
            this.rbTransferExtension.UseVisualStyleBackColor = true;
            this.rbTransferExtension.CheckedChanged += new System.EventHandler(this.rbDepartmentType_CheckedChanged);
            // 
            // rbPlayMessage
            // 
            this.rbPlayMessage.Checked = true;
            resources.ApplyResources(this.rbPlayMessage, "rbPlayMessage");
            this.rbPlayMessage.Name = "rbPlayMessage";
            this.rbPlayMessage.TabStop = true;
            this.rbPlayMessage.UseVisualStyleBackColor = true;
            this.rbPlayMessage.CheckedChanged += new System.EventHandler(this.rbDepartmentType_CheckedChanged);
            // 
            // wizardHeader5
            // 
            resources.ApplyResources(this.wizardHeader5, "wizardHeader5");
            this.wizardHeader5.Image = global::CallButler.Manager.Properties.Resources.office_building_48_shadow;
            this.wizardHeader5.Name = "wizardHeader5";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtDepartmentName
            // 
            resources.ApplyResources(this.txtDepartmentName, "txtDepartmentName");
            this.txtDepartmentName.Name = "txtDepartmentName";
            // 
            // callButlerDataset
            // 
            this.callButlerDataset.DataSetName = "CallButlerDataset";
            this.callButlerDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bsMailboxes
            // 
            this.bsMailboxes.DataSource = this.callButlerDataset;
            this.bsMailboxes.Position = 0;
            // 
            // openFileDialog
            // 
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            // 
            // DepartmentForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.wizard);
            this.Name = "DepartmentForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DepartmentForm_FormClosing);
            this.wizard.ResumeLayout(false);
            this.pgSpecificSettings.ResumeLayout(false);
            this.wzdDepartmentDetails.ResumeLayout(false);
            this.pgAddonModule.ResumeLayout(false);
            this.wizardPage7.ResumeLayout(false);
            this.wizardPage7.PerformLayout();
            this.wizardPage5.ResumeLayout(false);
            this.wizardPage5.PerformLayout();
            this.pgExtensionSelector.ResumeLayout(false);
            this.wizardPage3.ResumeLayout(false);
            this.pgGeneralSettings.ResumeLayout(false);
            this.pgGeneralSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOptionNumber)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.callButlerDataset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMailboxes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private global::Controls.Wizard.Wizard wizard;
        private global::Controls.Wizard.WizardPage pgGeneralSettings;
        private System.Windows.Forms.RadioButton rbScript;
        private System.Windows.Forms.RadioButton rbTransferNumber;
        private System.Windows.Forms.RadioButton rbTransferExtension;
        private System.Windows.Forms.RadioButton rbPlayMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDepartmentName;
        private global::Controls.Wizard.WizardPage pgSpecificSettings;
        private global::Controls.Wizard.Wizard wzdDepartmentDetails;
        private global::Controls.Wizard.WizardPage wizardPage3;
        private CallButler.Manager.Controls.GreetingControl greetingControl;
        private global::Controls.Wizard.WizardPage pgExtensionSelector;
        private WOSI.CallButler.Data.CallButlerDataset callButlerDataset;
        private global::Controls.Wizard.WizardPage wizardPage5;
        private System.Windows.Forms.TextBox txtTelephoneNumber;
        private global::Controls.LinkButton btnImportOutlook;
        private System.Windows.Forms.BindingSource bsMailboxes;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerIDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn mailboxIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn emailNotificationDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailAddressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn emailAttachmentDataGridViewCheckBoxColumn;
        private global::Controls.Wizard.WizardPage wizardPage7;
        private System.Windows.Forms.TextBox txtScriptFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private global::Controls.LinkButton btnScriptBrowse;
        private CallButler.Manager.Controls.WizardHeader wizardHeader5;
        private CallButler.Manager.Controls.WizardHeader wizardHeader3;
        private CallButler.Manager.Controls.WizardHeader wizardHeader2;
        private CallButler.Manager.Controls.WizardHeader wizardHeader1;
        private CallButler.Manager.Controls.WizardHeader wizardHeader4;
        private System.Windows.Forms.Panel panel1;
        private global::Controls.SmoothLabel lblDepartmentName;
        private global::Controls.SmoothLabel lblMessage;
        private global::Controls.SmoothLabel lblExtension;
        private global::Controls.SmoothLabel lblNumber;
        private global::Controls.SmoothLabel lblScript;
        private global::Controls.SmoothLabel smoothLabel1;
        private global::Controls.SmoothLabel lblNumberDescription;
        private CallButler.Manager.ViewControls.ExtensionsView extensionsView;
        private global::Controls.SmoothLabel lblAddon;
        private System.Windows.Forms.RadioButton rbAddon;
        private global::Controls.Wizard.WizardPage pgAddonModule;
        private CallButler.Manager.Controls.WizardHeader wizardHeader6;
        private System.Windows.Forms.NumericUpDown numOptionNumber;
        private global::Controls.SmoothLabel smoothLabel2;
        private CallButler.Manager.Controls.AddOnModuleChooserControl addOnModuleChooserControl;

    }
}
