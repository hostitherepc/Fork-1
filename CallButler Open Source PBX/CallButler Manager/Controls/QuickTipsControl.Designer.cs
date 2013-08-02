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
    partial class QuickTipsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickTipsControl));
            this.wizard1 = new global::Controls.Wizard.Wizard();
            this.wizardPage3 = new global::Controls.Wizard.WizardPage();
            this.btnDownloadMessageCenter = new global::Controls.LinkButton();
            this.label3 = new System.Windows.Forms.Label();
            this.wizardPage2 = new global::Controls.Wizard.WizardPage();
            this.smoothLabel1 = new global::Controls.SmoothLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.wizardPage1 = new global::Controls.Wizard.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNextTip = new global::Controls.LinkButton();
            this.wizard1.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard1
            // 
            this.wizard1.AlwaysShowFinishButton = false;
            this.wizard1.CloseOnCancel = true;
            this.wizard1.CloseOnFinish = true;
            this.wizard1.Controls.Add(this.wizardPage3);
            this.wizard1.Controls.Add(this.wizardPage2);
            this.wizard1.Controls.Add(this.wizardPage1);
            this.wizard1.DisplayButtons = false;
            this.wizard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizard1.Location = new System.Drawing.Point(0, 0);
            this.wizard1.Name = "wizard1";
            this.wizard1.PageIndex = 2;
            this.wizard1.Pages.AddRange(new global::Controls.Wizard.WizardPage[] {
            this.wizardPage1,
            this.wizardPage2,
            this.wizardPage3});
            this.wizard1.ShowTabs = false;
            this.wizard1.Size = new System.Drawing.Size(715, 375);
            this.wizard1.TabBackColor = System.Drawing.SystemColors.Control;
            this.wizard1.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.wizard1.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.SingleLine;
            this.wizard1.TabIndex = 0;
            this.wizard1.TablPanelTopMargin = 5;
            this.wizard1.TabPanelWidth = 100;
            this.wizard1.TabWidth = 50;
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.btnDownloadMessageCenter);
            this.wizardPage3.Controls.Add(this.label3);
            this.wizardPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage3.IsFinishPage = false;
            this.wizardPage3.Location = new System.Drawing.Point(0, 0);
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.Size = new System.Drawing.Size(715, 327);
            this.wizardPage3.TabIndex = 4;
            this.wizardPage3.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // btnDownloadMessageCenter
            // 
            this.btnDownloadMessageCenter.AntiAliasText = false;
            this.btnDownloadMessageCenter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDownloadMessageCenter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDownloadMessageCenter.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnDownloadMessageCenter.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnDownloadMessageCenter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDownloadMessageCenter.LinkImage = global::CallButler.Manager.Properties.Resources.download_16;
            this.btnDownloadMessageCenter.Location = new System.Drawing.Point(3, 149);
            this.btnDownloadMessageCenter.Name = "btnDownloadMessageCenter";
            this.btnDownloadMessageCenter.Size = new System.Drawing.Size(290, 25);
            this.btnDownloadMessageCenter.TabIndex = 3;
            this.btnDownloadMessageCenter.Text = "Download and try CallButler Add-on Modules...";
            this.btnDownloadMessageCenter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDownloadMessageCenter.UnderlineOnHover = true;
            this.btnDownloadMessageCenter.Click += new System.EventHandler(this.btnDownloadMessageCenter_Click);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(715, 133);
            this.label3.TabIndex = 2;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.smoothLabel1);
            this.wizardPage2.Controls.Add(this.label2);
            this.wizardPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage2.IsFinishPage = false;
            this.wizardPage2.Location = new System.Drawing.Point(0, 0);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(715, 327);
            this.wizardPage2.TabIndex = 3;
            this.wizardPage2.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // smoothLabel1
            // 
            this.smoothLabel1.AntiAliasText = false;
            this.smoothLabel1.AutoSize = true;
            this.smoothLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel1.EnableHelp = true;
            this.smoothLabel1.HelpText = "See how easy that was!";
            this.smoothLabel1.HelpTitle = "Help";
            this.smoothLabel1.Location = new System.Drawing.Point(0, 91);
            this.smoothLabel1.Name = "smoothLabel1";
            this.smoothLabel1.Size = new System.Drawing.Size(189, 13);
            this.smoothLabel1.TabIndex = 2;
            this.smoothLabel1.Text = "Hold your mouse over me and click.";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(715, 78);
            this.label2.TabIndex = 1;
            this.label2.Text = "Did you know that you can get help almost anywhere in CallButler?\r\n\r\nJust hold yo" +
                "ur mouse over any text. If you see a blue ? icon, click on the text to get help." +
                "";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.label1);
            this.wizardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage1.IsFinishPage = false;
            this.wizardPage1.Location = new System.Drawing.Point(0, 0);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(715, 352);
            this.wizardPage1.TabIndex = 2;
            this.wizardPage1.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(715, 217);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnNextTip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 375);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(715, 25);
            this.panel1.TabIndex = 1;
            // 
            // btnNextTip
            // 
            this.btnNextTip.AntiAliasText = false;
            this.btnNextTip.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNextTip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextTip.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNextTip.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnNextTip.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnNextTip.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNextTip.LinkImage = global::CallButler.Manager.Properties.Resources.media_play_16;
            this.btnNextTip.Location = new System.Drawing.Point(646, 0);
            this.btnNextTip.Name = "btnNextTip";
            this.btnNextTip.Size = new System.Drawing.Size(69, 25);
            this.btnNextTip.TabIndex = 1;
            this.btnNextTip.Text = "Next Tip";
            this.btnNextTip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNextTip.UnderlineOnHover = true;
            this.btnNextTip.Click += new System.EventHandler(this.btnNextTip_Click);
            // 
            // QuickTipsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizard1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.Name = "QuickTipsControl";
            this.Size = new System.Drawing.Size(715, 400);
            this.wizard1.ResumeLayout(false);
            this.wizardPage3.ResumeLayout(false);
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            this.wizardPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private global::Controls.Wizard.Wizard wizard1;
        private global::Controls.Wizard.WizardPage wizardPage1;
        private System.Windows.Forms.Label label1;
        private global::Controls.Wizard.WizardPage wizardPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private global::Controls.LinkButton btnNextTip;
        private global::Controls.SmoothLabel smoothLabel1;
        private global::Controls.Wizard.WizardPage wizardPage3;
        private System.Windows.Forms.Label label3;
        private global::Controls.LinkButton btnDownloadMessageCenter;
    }
}
