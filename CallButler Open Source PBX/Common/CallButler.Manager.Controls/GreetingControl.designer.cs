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



namespace CallButler.Manager.Controls
{
    partial class GreetingControl
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
                this.StopSounds();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GreetingControl));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.wzdGreeting = new global::Controls.Wizard.Wizard();
            this.wizardPage1 = new global::Controls.Wizard.WizardPage();
            this.smoothLabel2 = new global::Controls.SmoothLabel();
            this.txtCallTo = new System.Windows.Forms.TextBox();
            this.smoothLabel1 = new global::Controls.SmoothLabel();
            this.pgTextGreeting = new global::Controls.Wizard.WizardPage();
            this.speechControl = new WOSI.Utilities.Sound.SpeechControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnVoices = new global::Controls.LinkButton();
            this.label2 = new System.Windows.Forms.Label();
            this.pgRecordedGreeting = new global::Controls.Wizard.WizardPage();
            this.recordingControl = new WOSI.Utilities.Sound.RecordingControl();
            this.gradientPanel1 = new global::Controls.GradientPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.mnuGreetingType = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCall = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSpeak = new System.Windows.Forms.ToolStripMenuItem();
            this.gradientPanel2 = new global::Controls.GradientPanel();
            this.btnPlaceCall = new System.Windows.Forms.Button();
            this.wzdGreeting.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.pgTextGreeting.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pgRecordedGreeting.SuspendLayout();
            this.gradientPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.gradientPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            // 
            // wzdGreeting
            // 
            this.wzdGreeting.AlwaysShowFinishButton = false;
            this.wzdGreeting.BackColor = System.Drawing.Color.Transparent;
            this.wzdGreeting.CloseOnCancel = false;
            this.wzdGreeting.CloseOnFinish = false;
            this.wzdGreeting.Controls.Add(this.wizardPage1);
            this.wzdGreeting.Controls.Add(this.pgTextGreeting);
            this.wzdGreeting.Controls.Add(this.pgRecordedGreeting);
            this.wzdGreeting.DisplayButtons = false;
            resources.ApplyResources(this.wzdGreeting, "wzdGreeting");
            this.wzdGreeting.Name = "wzdGreeting";
            this.wzdGreeting.PageIndex = 2;
            this.wzdGreeting.Pages.AddRange(new global::Controls.Wizard.WizardPage[] {
            this.pgRecordedGreeting,
            this.pgTextGreeting,
            this.wizardPage1});
            this.wzdGreeting.ShowTabs = false;
            this.wzdGreeting.TabBackColor = System.Drawing.Color.WhiteSmoke;
            this.wzdGreeting.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.wzdGreeting.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.SingleLine;
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.btnPlaceCall);
            this.wizardPage1.Controls.Add(this.smoothLabel2);
            this.wizardPage1.Controls.Add(this.txtCallTo);
            this.wizardPage1.Controls.Add(this.smoothLabel1);
            resources.ApplyResources(this.wizardPage1, "wizardPage1");
            this.wizardPage1.IsFinishPage = false;
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.TabLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            // 
            // smoothLabel2
            // 
            this.smoothLabel2.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel2, "smoothLabel2");
            this.smoothLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.smoothLabel2.Name = "smoothLabel2";
            // 
            // txtCallTo
            // 
            resources.ApplyResources(this.txtCallTo, "txtCallTo");
            this.txtCallTo.Name = "txtCallTo";
            // 
            // smoothLabel1
            // 
            this.smoothLabel1.AntiAliasText = false;
            resources.ApplyResources(this.smoothLabel1, "smoothLabel1");
            this.smoothLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.smoothLabel1.Name = "smoothLabel1";
            // 
            // pgTextGreeting
            // 
            this.pgTextGreeting.Controls.Add(this.speechControl);
            this.pgTextGreeting.Controls.Add(this.panel1);
            resources.ApplyResources(this.pgTextGreeting, "pgTextGreeting");
            this.pgTextGreeting.IsFinishPage = false;
            this.pgTextGreeting.Name = "pgTextGreeting";
            this.pgTextGreeting.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // speechControl
            // 
            resources.ApplyResources(this.speechControl, "speechControl");
            this.speechControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.speechControl.Name = "speechControl";
            this.speechControl.ShowSuggestTextButton = false;
            this.speechControl.ShowVoiceSelection = false;
            this.speechControl.SpeechText = "";
            this.speechControl.SuggestedText = "";
            this.speechControl.Voice = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnVoices);
            this.panel1.Controls.Add(this.label2);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.panel1.Name = "panel1";
            // 
            // btnVoices
            // 
            this.btnVoices.AntiAliasText = false;
            this.btnVoices.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVoices.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnVoices.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVoices.LinkImage = global::CallButler.Manager.Controls.Properties.Resources.text_loudspeaker_16;
            resources.ApplyResources(this.btnVoices, "btnVoices");
            this.btnVoices.Name = "btnVoices";
            this.btnVoices.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVoices.UnderlineOnHover = true;
            this.btnVoices.Click += new System.EventHandler(this.btnVoices_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // pgRecordedGreeting
            // 
            this.pgRecordedGreeting.Controls.Add(this.recordingControl);
            resources.ApplyResources(this.pgRecordedGreeting, "pgRecordedGreeting");
            this.pgRecordedGreeting.IsFinishPage = false;
            this.pgRecordedGreeting.Name = "pgRecordedGreeting";
            this.pgRecordedGreeting.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // recordingControl
            // 
            resources.ApplyResources(this.recordingControl, "recordingControl");
            this.recordingControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.recordingControl.Name = "recordingControl";
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gradientPanel1.BorderColor = System.Drawing.Color.DarkGray;
            this.gradientPanel1.BorderWidth = 1F;
            this.gradientPanel1.Controls.Add(this.toolStrip1);
            resources.ApplyResources(this.gradientPanel1, "gradientPanel1");
            this.gradientPanel1.DrawBorder = true;
            this.gradientPanel1.ForeColor = System.Drawing.Color.LightGray;
            this.gradientPanel1.GradientAngle = 90F;
            this.gradientPanel1.Name = "gradientPanel1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.mnuGreetingType});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // mnuGreetingType
            // 
            this.mnuGreetingType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRecord,
            this.mnuCall,
            this.mnuSpeak});
            this.mnuGreetingType.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.mnuGreetingType, "mnuGreetingType");
            this.mnuGreetingType.Name = "mnuGreetingType";
            // 
            // mnuRecord
            // 
            this.mnuRecord.CheckOnClick = true;
            this.mnuRecord.Image = global::CallButler.Manager.Controls.Properties.Resources.loudspeaker_16;
            this.mnuRecord.Name = "mnuRecord";
            resources.ApplyResources(this.mnuRecord, "mnuRecord");
            this.mnuRecord.CheckedChanged += new System.EventHandler(this.mnuRecord_CheckedChanged);
            // 
            // mnuCall
            // 
            this.mnuCall.CheckOnClick = true;
            this.mnuCall.Image = global::CallButler.Manager.Controls.Properties.Resources.telephone_16;
            this.mnuCall.Name = "mnuCall";
            resources.ApplyResources(this.mnuCall, "mnuCall");
            this.mnuCall.CheckedChanged += new System.EventHandler(this.mnuCall_CheckedChanged);
            // 
            // mnuSpeak
            // 
            this.mnuSpeak.CheckOnClick = true;
            this.mnuSpeak.Image = global::CallButler.Manager.Controls.Properties.Resources.text_loudspeaker_16;
            this.mnuSpeak.Name = "mnuSpeak";
            resources.ApplyResources(this.mnuSpeak, "mnuSpeak");
            this.mnuSpeak.CheckedChanged += new System.EventHandler(this.mnuSpeak_CheckedChanged);
            // 
            // gradientPanel2
            // 
            this.gradientPanel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gradientPanel2.BorderColor = System.Drawing.Color.DarkGray;
            this.gradientPanel2.BorderWidth = 1F;
            this.gradientPanel2.Controls.Add(this.wzdGreeting);
            this.gradientPanel2.Controls.Add(this.gradientPanel1);
            resources.ApplyResources(this.gradientPanel2, "gradientPanel2");
            this.gradientPanel2.DrawBorder = true;
            this.gradientPanel2.ForeColor = System.Drawing.Color.LightGray;
            this.gradientPanel2.GradientAngle = 90F;
            this.gradientPanel2.Name = "gradientPanel2";
            // 
            // btnPlaceCall
            // 
            this.btnPlaceCall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            resources.ApplyResources(this.btnPlaceCall, "btnPlaceCall");
            this.btnPlaceCall.Name = "btnPlaceCall";
            this.btnPlaceCall.UseVisualStyleBackColor = true;
            this.btnPlaceCall.Click += new System.EventHandler(this.btnPlaceCall_Click);
            // 
            // GreetingControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gradientPanel2);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.Name = "GreetingControl";
            this.wzdGreeting.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.pgTextGreeting.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pgRecordedGreeting.ResumeLayout(false);
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gradientPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolTip toolTip;
        private global::Controls.Wizard.Wizard wzdGreeting;
        private global::Controls.Wizard.WizardPage pgTextGreeting;
        private WOSI.Utilities.Sound.SpeechControl speechControl;
        private global::Controls.Wizard.WizardPage pgRecordedGreeting;
        private WOSI.Utilities.Sound.RecordingControl recordingControl;
        private global::Controls.GradientPanel gradientPanel1;
        private global::Controls.GradientPanel gradientPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private global::Controls.LinkButton btnVoices;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripDropDownButton mnuGreetingType;
        private System.Windows.Forms.ToolStripMenuItem mnuRecord;
        private System.Windows.Forms.ToolStripMenuItem mnuCall;
        private System.Windows.Forms.ToolStripMenuItem mnuSpeak;
        private global::Controls.Wizard.WizardPage wizardPage1;
        private global::Controls.SmoothLabel smoothLabel2;
        private System.Windows.Forms.TextBox txtCallTo;
        private global::Controls.SmoothLabel smoothLabel1;
        private System.Windows.Forms.Button btnPlaceCall;
    }
}
