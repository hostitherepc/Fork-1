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
    partial class CallAnswerSettingsForm
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
            this.wizard = new global::Controls.Wizard.Wizard();
            this.pgGeneralSettings = new global::Controls.Wizard.WizardPage();
            this.label2 = new System.Windows.Forms.Label();
            this.numAnswerSeconds = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.wizardHeader5 = new CallButler.Manager.Controls.WizardHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numWelcomeGreetingWaitTime = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.wizard.SuspendLayout();
            this.pgGeneralSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAnswerSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWelcomeGreetingWaitTime)).BeginInit();
            this.SuspendLayout();
            // 
            // wizard
            // 
            this.wizard.AlwaysShowFinishButton = true;
            this.wizard.CloseOnCancel = true;
            this.wizard.CloseOnFinish = true;
            this.wizard.Controls.Add(this.pgGeneralSettings);
            this.wizard.DisplayButtons = true;
            this.wizard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.wizard.Location = new System.Drawing.Point(0, 0);
            this.wizard.Name = "wizard";
            this.wizard.PageIndex = 0;
            this.wizard.Pages.AddRange(new global::Controls.Wizard.WizardPage[] {
            this.pgGeneralSettings});
            this.wizard.ShowTabs = true;
            this.wizard.Size = new System.Drawing.Size(550, 300);
            this.wizard.TabBackColor = System.Drawing.Color.WhiteSmoke;
            this.wizard.TabBackgroundImage = global::CallButler.Manager.Properties.Resources.wizard_header;
            this.wizard.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.wizard.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.Cutout;
            this.wizard.TabIndex = 5;
            this.wizard.TablPanelTopMargin = 125;
            this.wizard.TabPanelWidth = 160;
            this.wizard.TabWidth = 135;
            // 
            // pgGeneralSettings
            // 
            this.pgGeneralSettings.Controls.Add(this.label5);
            this.pgGeneralSettings.Controls.Add(this.numWelcomeGreetingWaitTime);
            this.pgGeneralSettings.Controls.Add(this.label4);
            this.pgGeneralSettings.Controls.Add(this.label3);
            this.pgGeneralSettings.Controls.Add(this.label2);
            this.pgGeneralSettings.Controls.Add(this.numAnswerSeconds);
            this.pgGeneralSettings.Controls.Add(this.label1);
            this.pgGeneralSettings.Controls.Add(this.wizardHeader5);
            this.pgGeneralSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgGeneralSettings.IsFinishPage = false;
            this.pgGeneralSettings.Location = new System.Drawing.Point(160, 0);
            this.pgGeneralSettings.Name = "pgGeneralSettings";
            this.pgGeneralSettings.Size = new System.Drawing.Size(390, 252);
            this.pgGeneralSettings.TabIndex = 1;
            this.pgGeneralSettings.TabLinkColor = System.Drawing.SystemColors.ControlText;
            this.pgGeneralSettings.Text = "General Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "second(s)";
            // 
            // numAnswerSeconds
            // 
            this.numAnswerSeconds.Location = new System.Drawing.Point(170, 127);
            this.numAnswerSeconds.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numAnswerSeconds.Name = "numAnswerSeconds";
            this.numAnswerSeconds.Size = new System.Drawing.Size(57, 21);
            this.numAnswerSeconds.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Answer incoming calls after";
            // 
            // wizardHeader5
            // 
            this.wizardHeader5.Description = "General settings for answering incoming calls.";
            this.wizardHeader5.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardHeader5.Image = global::CallButler.Manager.Properties.Resources.phone_pick_up_48;
            this.wizardHeader5.Location = new System.Drawing.Point(0, 0);
            this.wizardHeader5.Name = "wizardHeader5";
            this.wizardHeader5.Padding = new System.Windows.Forms.Padding(20, 10, 20, 0);
            this.wizardHeader5.Size = new System.Drawing.Size(390, 105);
            this.wizardHeader5.SubTitle = "General Settings";
            this.wizardHeader5.TabIndex = 18;
            this.wizardHeader5.Title = "Call Answer Settings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "After answering...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Wait";
            // 
            // numWelcomeGreetingWaitTime
            // 
            this.numWelcomeGreetingWaitTime.Location = new System.Drawing.Point(62, 184);
            this.numWelcomeGreetingWaitTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numWelcomeGreetingWaitTime.Name = "numWelcomeGreetingWaitTime";
            this.numWelcomeGreetingWaitTime.Size = new System.Drawing.Size(57, 21);
            this.numWelcomeGreetingWaitTime.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(122, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(231, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "seconds before playing the Welcome Greeting.";
            // 
            // CallAnswerSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(550, 300);
            this.Controls.Add(this.wizard);
            this.Name = "CallAnswerSettingsForm";
            this.Text = "Call Answer Settings";
            this.wizard.ResumeLayout(false);
            this.pgGeneralSettings.ResumeLayout(false);
            this.pgGeneralSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAnswerSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWelcomeGreetingWaitTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private global::Controls.Wizard.Wizard wizard;
        private global::Controls.Wizard.WizardPage pgGeneralSettings;
        private CallButler.Manager.Controls.WizardHeader wizardHeader5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numAnswerSeconds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numWelcomeGreetingWaitTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
    }
}
