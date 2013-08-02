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
    partial class ScriptScheduleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptScheduleForm));
            this.wizard = new global::Controls.Wizard.Wizard();
            this.pgHours = new global::Controls.Wizard.WizardPage();
            this.lblHours = new global::Controls.SmoothLabel();
            this.pnlSchedule = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.cboTimeZone = new System.Windows.Forms.ComboBox();
            this.scheduleControl = new CallButler.Manager.Controls.ScheduleControl();
            this.btnSelectInverse = new global::Controls.LinkButton();
            this.btnSelectAll = new global::Controls.LinkButton();
            this.btnSelectNone = new global::Controls.LinkButton();
            this.cbHasHours = new System.Windows.Forms.CheckBox();
            this.wizardHeader3 = new CallButler.Manager.Controls.WizardHeader();
            this.pgGeneral = new global::Controls.Wizard.WizardPage();
            this.smoothLabel3 = new global::Controls.SmoothLabel();
            this.smoothLabel5 = new global::Controls.SmoothLabel();
            this.cbEnabled = new System.Windows.Forms.CheckBox();
            this.txtScriptFile = new System.Windows.Forms.TextBox();
            this.btnScriptBrowse = new global::Controls.LinkButton();
            this.txtName = new System.Windows.Forms.TextBox();
            this.wizardHeader1 = new CallButler.Manager.Controls.WizardHeader();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.wizard.SuspendLayout();
            this.pgHours.SuspendLayout();
            this.pnlSchedule.SuspendLayout();
            this.pgGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard
            // 
            this.wizard.AccessibleDescription = null;
            this.wizard.AccessibleName = null;
            this.wizard.AlwaysShowFinishButton = true;
            resources.ApplyResources(this.wizard, "wizard");
            this.wizard.BackgroundImage = null;
            this.wizard.CloseOnCancel = true;
            this.wizard.CloseOnFinish = true;
            this.wizard.Controls.Add(this.pgGeneral);
            this.wizard.Controls.Add(this.pgHours);
            this.wizard.DisplayButtons = true;
            this.wizard.Name = "wizard";
            this.wizard.PageIndex = 0;
            this.wizard.Pages.AddRange(new global::Controls.Wizard.WizardPage[] {
            this.pgGeneral,
            this.pgHours});
            this.wizard.ShowTabs = true;
            this.wizard.TabBackColor = System.Drawing.Color.WhiteSmoke;
            this.wizard.TabBackgroundImage = global::CallButler.Manager.Properties.Resources.wizard_header;
            this.wizard.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.wizard.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.Cutout;
            this.wizard.WizardFinished += new System.EventHandler<global::Controls.Wizard.PageChangedEventArgs>(this.wizard_WizardFinished);
            // 
            // pgHours
            // 
            this.pgHours.AccessibleDescription = null;
            this.pgHours.AccessibleName = null;
            resources.ApplyResources(this.pgHours, "pgHours");
            this.pgHours.BackgroundImage = null;
            this.pgHours.Controls.Add(this.lblHours);
            this.pgHours.Controls.Add(this.pnlSchedule);
            this.pgHours.Controls.Add(this.cbHasHours);
            this.pgHours.Controls.Add(this.wizardHeader3);
            this.pgHours.Font = null;
            this.pgHours.IsFinishPage = false;
            this.pgHours.Name = "pgHours";
            this.pgHours.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // lblHours
            // 
            this.lblHours.AccessibleDescription = null;
            this.lblHours.AccessibleName = null;
            resources.ApplyResources(this.lblHours, "lblHours");
            this.lblHours.AntiAliasText = false;
            this.lblHours.BackColor = System.Drawing.Color.Transparent;
            this.lblHours.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHours.EnableHelp = true;
            this.lblHours.Font = null;
            this.lblHours.Name = "lblHours";
            this.lblHours.Click += new System.EventHandler(this.lblHours_Click);
            // 
            // pnlSchedule
            // 
            this.pnlSchedule.AccessibleDescription = null;
            this.pnlSchedule.AccessibleName = null;
            resources.ApplyResources(this.pnlSchedule, "pnlSchedule");
            this.pnlSchedule.BackgroundImage = null;
            this.pnlSchedule.Controls.Add(this.label12);
            this.pnlSchedule.Controls.Add(this.cboTimeZone);
            this.pnlSchedule.Controls.Add(this.scheduleControl);
            this.pnlSchedule.Controls.Add(this.btnSelectInverse);
            this.pnlSchedule.Controls.Add(this.btnSelectAll);
            this.pnlSchedule.Controls.Add(this.btnSelectNone);
            this.pnlSchedule.Font = null;
            this.pnlSchedule.Name = "pnlSchedule";
            // 
            // label12
            // 
            this.label12.AccessibleDescription = null;
            this.label12.AccessibleName = null;
            resources.ApplyResources(this.label12, "label12");
            this.label12.Font = null;
            this.label12.Name = "label12";
            // 
            // cboTimeZone
            // 
            this.cboTimeZone.AccessibleDescription = null;
            this.cboTimeZone.AccessibleName = null;
            resources.ApplyResources(this.cboTimeZone, "cboTimeZone");
            this.cboTimeZone.BackgroundImage = null;
            this.cboTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeZone.Font = null;
            this.cboTimeZone.FormattingEnabled = true;
            this.cboTimeZone.Name = "cboTimeZone";
            // 
            // scheduleControl
            // 
            this.scheduleControl.AccessibleDescription = null;
            this.scheduleControl.AccessibleName = null;
            resources.ApplyResources(this.scheduleControl, "scheduleControl");
            this.scheduleControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.scheduleControl.BackgroundImage = null;
            this.scheduleControl.BorderColor = System.Drawing.Color.Silver;
            this.scheduleControl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scheduleControl.DrawOutsideBorder = true;
            this.scheduleControl.Name = "scheduleControl";
            this.scheduleControl.SelectionColor = System.Drawing.Color.CornflowerBlue;
            // 
            // btnSelectInverse
            // 
            this.btnSelectInverse.AccessibleDescription = null;
            this.btnSelectInverse.AccessibleName = null;
            resources.ApplyResources(this.btnSelectInverse, "btnSelectInverse");
            this.btnSelectInverse.AntiAliasText = false;
            this.btnSelectInverse.BackgroundImage = null;
            this.btnSelectInverse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectInverse.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSelectInverse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectInverse.LinkImage = global::CallButler.Manager.Properties.Resources.select_inverse_16;
            this.btnSelectInverse.Name = "btnSelectInverse";
            this.btnSelectInverse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectInverse.UnderlineOnHover = true;
            this.btnSelectInverse.Click += new System.EventHandler(this.btnSelectInverse_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.AccessibleDescription = null;
            this.btnSelectAll.AccessibleName = null;
            resources.ApplyResources(this.btnSelectAll, "btnSelectAll");
            this.btnSelectAll.AntiAliasText = false;
            this.btnSelectAll.BackgroundImage = null;
            this.btnSelectAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectAll.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSelectAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectAll.LinkImage = global::CallButler.Manager.Properties.Resources.select_all_16;
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectAll.UnderlineOnHover = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.AccessibleDescription = null;
            this.btnSelectNone.AccessibleName = null;
            resources.ApplyResources(this.btnSelectNone, "btnSelectNone");
            this.btnSelectNone.AntiAliasText = false;
            this.btnSelectNone.BackgroundImage = null;
            this.btnSelectNone.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectNone.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSelectNone.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectNone.LinkImage = global::CallButler.Manager.Properties.Resources.select_none_16;
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectNone.UnderlineOnHover = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // cbHasHours
            // 
            this.cbHasHours.AccessibleDescription = null;
            this.cbHasHours.AccessibleName = null;
            resources.ApplyResources(this.cbHasHours, "cbHasHours");
            this.cbHasHours.BackgroundImage = null;
            this.cbHasHours.Font = null;
            this.cbHasHours.Name = "cbHasHours";
            this.cbHasHours.UseVisualStyleBackColor = true;
            this.cbHasHours.CheckedChanged += new System.EventHandler(this.cbHasHours_CheckedChanged);
            // 
            // wizardHeader3
            // 
            this.wizardHeader3.AccessibleDescription = null;
            this.wizardHeader3.AccessibleName = null;
            resources.ApplyResources(this.wizardHeader3, "wizardHeader3");
            this.wizardHeader3.BackgroundImage = null;
            this.wizardHeader3.Font = null;
            this.wizardHeader3.Image = global::CallButler.Manager.Properties.Resources.date_time_48_shadow;
            this.wizardHeader3.Name = "wizardHeader3";
            // 
            // pgGeneral
            // 
            this.pgGeneral.AccessibleDescription = null;
            this.pgGeneral.AccessibleName = null;
            resources.ApplyResources(this.pgGeneral, "pgGeneral");
            this.pgGeneral.BackgroundImage = null;
            this.pgGeneral.Controls.Add(this.smoothLabel3);
            this.pgGeneral.Controls.Add(this.smoothLabel5);
            this.pgGeneral.Controls.Add(this.cbEnabled);
            this.pgGeneral.Controls.Add(this.txtScriptFile);
            this.pgGeneral.Controls.Add(this.btnScriptBrowse);
            this.pgGeneral.Controls.Add(this.txtName);
            this.pgGeneral.Controls.Add(this.wizardHeader1);
            this.pgGeneral.Font = null;
            this.pgGeneral.IsFinishPage = false;
            this.pgGeneral.Name = "pgGeneral";
            this.pgGeneral.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // smoothLabel3
            // 
            this.smoothLabel3.AccessibleDescription = null;
            this.smoothLabel3.AccessibleName = null;
            resources.ApplyResources(this.smoothLabel3, "smoothLabel3");
            this.smoothLabel3.AntiAliasText = false;
            this.smoothLabel3.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel3.EnableHelp = true;
            this.smoothLabel3.Font = null;
            this.smoothLabel3.Name = "smoothLabel3";
            // 
            // smoothLabel5
            // 
            this.smoothLabel5.AccessibleDescription = null;
            this.smoothLabel5.AccessibleName = null;
            resources.ApplyResources(this.smoothLabel5, "smoothLabel5");
            this.smoothLabel5.AntiAliasText = false;
            this.smoothLabel5.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel5.EnableHelp = true;
            this.smoothLabel5.Font = null;
            this.smoothLabel5.Name = "smoothLabel5";
            // 
            // cbEnabled
            // 
            this.cbEnabled.AccessibleDescription = null;
            this.cbEnabled.AccessibleName = null;
            resources.ApplyResources(this.cbEnabled, "cbEnabled");
            this.cbEnabled.BackgroundImage = null;
            this.cbEnabled.Font = null;
            this.cbEnabled.Name = "cbEnabled";
            this.cbEnabled.UseVisualStyleBackColor = true;
            // 
            // txtScriptFile
            // 
            this.txtScriptFile.AccessibleDescription = null;
            this.txtScriptFile.AccessibleName = null;
            resources.ApplyResources(this.txtScriptFile, "txtScriptFile");
            this.txtScriptFile.BackgroundImage = null;
            this.txtScriptFile.Font = null;
            this.txtScriptFile.Name = "txtScriptFile";
            // 
            // btnScriptBrowse
            // 
            this.btnScriptBrowse.AccessibleDescription = null;
            this.btnScriptBrowse.AccessibleName = null;
            resources.ApplyResources(this.btnScriptBrowse, "btnScriptBrowse");
            this.btnScriptBrowse.AntiAliasText = false;
            this.btnScriptBrowse.BackgroundImage = null;
            this.btnScriptBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScriptBrowse.Font = null;
            this.btnScriptBrowse.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnScriptBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScriptBrowse.LinkImage = global::CallButler.Manager.Properties.Resources.folder_16;
            this.btnScriptBrowse.Name = "btnScriptBrowse";
            this.btnScriptBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScriptBrowse.UnderlineOnHover = true;
            this.btnScriptBrowse.Click += new System.EventHandler(this.btnScriptBrowse_Click);
            // 
            // txtName
            // 
            this.txtName.AccessibleDescription = null;
            this.txtName.AccessibleName = null;
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.BackgroundImage = null;
            this.txtName.Font = null;
            this.txtName.Name = "txtName";
            // 
            // wizardHeader1
            // 
            this.wizardHeader1.AccessibleDescription = null;
            this.wizardHeader1.AccessibleName = null;
            resources.ApplyResources(this.wizardHeader1, "wizardHeader1");
            this.wizardHeader1.BackgroundImage = null;
            this.wizardHeader1.Font = null;
            this.wizardHeader1.Image = global::CallButler.Manager.Properties.Resources.date_time_48_shadow;
            this.wizardHeader1.Name = "wizardHeader1";
            // 
            // openFileDialog
            // 
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            // 
            // ScriptScheduleForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.Controls.Add(this.wizard);
            this.Icon = null;
            this.Name = "ScriptScheduleForm";
            this.wizard.ResumeLayout(false);
            this.pgHours.ResumeLayout(false);
            this.pgHours.PerformLayout();
            this.pnlSchedule.ResumeLayout(false);
            this.pnlSchedule.PerformLayout();
            this.pgGeneral.ResumeLayout(false);
            this.pgGeneral.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private global::Controls.Wizard.Wizard wizard;
        private global::Controls.Wizard.WizardPage pgHours;
        private CallButler.Manager.Controls.WizardHeader wizardHeader3;
        private global::Controls.Wizard.WizardPage pgGeneral;
        private CallButler.Manager.Controls.WizardHeader wizardHeader1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtScriptFile;
        private global::Controls.LinkButton btnScriptBrowse;
        private System.Windows.Forms.CheckBox cbEnabled;
        private System.Windows.Forms.CheckBox cbHasHours;
        private System.Windows.Forms.Panel pnlSchedule;
        private CallButler.Manager.Controls.ScheduleControl scheduleControl;
        private global::Controls.LinkButton btnSelectInverse;
        private global::Controls.LinkButton btnSelectAll;
        private global::Controls.LinkButton btnSelectNone;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboTimeZone;
        private global::Controls.SmoothLabel smoothLabel5;
        private global::Controls.SmoothLabel smoothLabel3;
        private global::Controls.SmoothLabel lblHours;
    }
}
