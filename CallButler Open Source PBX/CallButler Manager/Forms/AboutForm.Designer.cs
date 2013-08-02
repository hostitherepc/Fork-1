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
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.lblClientVersion = new global::Controls.SmoothLabel();
            this.lblCopyright = new global::Controls.SmoothLabel();
            this.lblDescription = new global::Controls.SmoothLabel();
            this.lblRegInfo = new global::Controls.SmoothLabel();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblServerVersion = new global::Controls.SmoothLabel();
            this.lblAdditionalCopyright = new global::Controls.SmoothLabel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblClientVersion
            // 
            this.lblClientVersion.AntiAliasText = false;
            this.lblClientVersion.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblClientVersion, "lblClientVersion");
            this.lblClientVersion.ForeColor = System.Drawing.Color.DimGray;
            this.lblClientVersion.Name = "lblClientVersion";
            // 
            // lblCopyright
            // 
            resources.ApplyResources(this.lblCopyright, "lblCopyright");
            this.lblCopyright.AntiAliasText = false;
            this.lblCopyright.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lblCopyright.Name = "lblCopyright";
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.ForeColor = System.Drawing.Color.DimGray;
            this.lblDescription.Name = "lblDescription";
            // 
            // lblRegInfo
            // 
            resources.ApplyResources(this.lblRegInfo, "lblRegInfo");
            this.lblRegInfo.AntiAliasText = false;
            this.lblRegInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblRegInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lblRegInfo.Name = "lblRegInfo";
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblServerVersion
            // 
            this.lblServerVersion.AntiAliasText = false;
            this.lblServerVersion.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblServerVersion, "lblServerVersion");
            this.lblServerVersion.ForeColor = System.Drawing.Color.DimGray;
            this.lblServerVersion.Name = "lblServerVersion";
            // 
            // lblAdditionalCopyright
            // 
            resources.ApplyResources(this.lblAdditionalCopyright, "lblAdditionalCopyright");
            this.lblAdditionalCopyright.AntiAliasText = false;
            this.lblAdditionalCopyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lblAdditionalCopyright.Name = "lblAdditionalCopyright";
            // 
            // pbLogo
            // 
            this.pbLogo.BackColor = System.Drawing.Color.Transparent;
            this.pbLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbLogo.Image = global::CallButler.Manager.Properties.Resources.t2logo;
            resources.ApplyResources(this.pbLogo, "pbLogo");
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.TabStop = false;
            this.pbLogo.Click += new System.EventHandler(this.pbLogo_Click);
            // 
            // AboutForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::CallButler.Manager.Properties.Resources.cb_header;
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.lblAdditionalCopyright);
            this.Controls.Add(this.lblServerVersion);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblRegInfo);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblClientVersion);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private global::Controls.SmoothLabel lblClientVersion;
        private global::Controls.SmoothLabel lblCopyright;
        private global::Controls.SmoothLabel lblDescription;
        private global::Controls.SmoothLabel lblRegInfo;
        private System.Windows.Forms.Button btnOK;
        private global::Controls.SmoothLabel lblServerVersion;
        private global::Controls.SmoothLabel lblAdditionalCopyright;
        private System.Windows.Forms.PictureBox pbLogo;
    }
}