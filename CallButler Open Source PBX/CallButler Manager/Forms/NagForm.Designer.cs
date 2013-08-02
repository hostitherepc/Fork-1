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
    partial class NagForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NagForm));
            this.smoothLabel1 = new global::Controls.SmoothLabel();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnBuyLicense = new global::Controls.LinkButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEnterLicense = new global::Controls.LinkButton();
            this.lblStatus = new global::Controls.SmoothLabel();
            this.dividerLine1 = new global::Controls.DividerLine();
            this.SuspendLayout();
            // 
            // smoothLabel1
            // 
            this.smoothLabel1.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.smoothLabel1, "smoothLabel1");
            this.smoothLabel1.ForeColor = System.Drawing.Color.Gray;
            this.smoothLabel1.Name = "smoothLabel1";
            // 
            // btnContinue
            // 
            resources.ApplyResources(this.btnContinue, "btnContinue");
            this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.UseVisualStyleBackColor = true;
            // 
            // btnBuyLicense
            // 
            this.btnBuyLicense.AntiAliasText = false;
            resources.ApplyResources(this.btnBuyLicense, "btnBuyLicense");
            this.btnBuyLicense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuyLicense.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnBuyLicense.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuyLicense.LinkImage = global::CallButler.Manager.Properties.Resources.shoppingcart_full_16;
            this.btnBuyLicense.Name = "btnBuyLicense";
            this.btnBuyLicense.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuyLicense.UnderlineOnHover = true;
            this.btnBuyLicense.Click += new System.EventHandler(this.btnBuyLicense_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnEnterLicense
            // 
            this.btnEnterLicense.AntiAliasText = false;
            resources.ApplyResources(this.btnEnterLicense, "btnEnterLicense");
            this.btnEnterLicense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnterLicense.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnEnterLicense.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnterLicense.LinkImage = global::CallButler.Manager.Properties.Resources.keys_16;
            this.btnEnterLicense.Name = "btnEnterLicense";
            this.btnEnterLicense.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnterLicense.UnderlineOnHover = true;
            this.btnEnterLicense.Click += new System.EventHandler(this.btnEnterLicense_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.ForeColor = System.Drawing.Color.YellowGreen;
            this.lblStatus.Name = "lblStatus";
            // 
            // dividerLine1
            // 
            resources.ApplyResources(this.dividerLine1, "dividerLine1");
            this.dividerLine1.BackColor = System.Drawing.Color.Transparent;
            this.dividerLine1.ForeColor = System.Drawing.Color.Silver;
            this.dividerLine1.GradientWidth = 10;
            this.dividerLine1.LineWidth = 1;
            this.dividerLine1.Name = "dividerLine1";
            this.dividerLine1.Vertical = false;
            // 
            // NagForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::CallButler.Manager.Properties.Resources.cb_header;
            this.Controls.Add(this.dividerLine1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnEnterLicense);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBuyLicense);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.smoothLabel1);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NagForm";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private global::Controls.SmoothLabel smoothLabel1;
        private System.Windows.Forms.Button btnContinue;
        private global::Controls.LinkButton btnBuyLicense;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private global::Controls.LinkButton btnEnterLicense;
        private global::Controls.SmoothLabel lblStatus;
        private global::Controls.DividerLine dividerLine1;

    }
}