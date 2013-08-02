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
    partial class HelpView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpView));
            this.roundedCornerPanel1 = new global::Controls.RoundedCornerPanel();
            this.txtHelp = new System.Windows.Forms.RichTextBox();
            this.btnDetach = new global::Controls.LinkButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new global::Controls.LinkButton();
            this.dividerLine1 = new global::Controls.DividerLine();
            this.roundedCornerPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // roundedCornerPanel1
            // 
            this.roundedCornerPanel1.AccessibleDescription = null;
            this.roundedCornerPanel1.AccessibleName = null;
            resources.ApplyResources(this.roundedCornerPanel1, "roundedCornerPanel1");
            this.roundedCornerPanel1.BackgroundImage = null;
            this.roundedCornerPanel1.BorderSize = 1F;
            this.roundedCornerPanel1.Controls.Add(this.txtHelp);
            this.roundedCornerPanel1.Controls.Add(this.btnDetach);
            this.roundedCornerPanel1.Controls.Add(this.pictureBox1);
            this.roundedCornerPanel1.Controls.Add(this.btnClose);
            this.roundedCornerPanel1.Controls.Add(this.dividerLine1);
            this.roundedCornerPanel1.CornerRadius = 10;
            this.roundedCornerPanel1.DisplayShadow = false;
            this.roundedCornerPanel1.Font = null;
            this.roundedCornerPanel1.ForeColor = System.Drawing.Color.DarkGray;
            this.roundedCornerPanel1.Name = "roundedCornerPanel1";
            this.roundedCornerPanel1.PanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(230)))));
            this.roundedCornerPanel1.ShadowColor = System.Drawing.Color.Gray;
            this.roundedCornerPanel1.ShadowOffset = 5;
            // 
            // txtHelp
            // 
            this.txtHelp.AccessibleDescription = null;
            this.txtHelp.AccessibleName = null;
            resources.ApplyResources(this.txtHelp, "txtHelp");
            this.txtHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(230)))));
            this.txtHelp.BackgroundImage = null;
            this.txtHelp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHelp.Font = null;
            this.txtHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.txtHelp.Name = "txtHelp";
            this.txtHelp.ReadOnly = true;
            // 
            // btnDetach
            // 
            this.btnDetach.AccessibleDescription = null;
            this.btnDetach.AccessibleName = null;
            resources.ApplyResources(this.btnDetach, "btnDetach");
            this.btnDetach.AntiAliasText = false;
            this.btnDetach.BackColor = System.Drawing.Color.Transparent;
            this.btnDetach.BackgroundImage = null;
            this.btnDetach.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDetach.Font = null;
            this.btnDetach.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnDetach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetach.LinkImage = global::CallButler.Manager.Properties.Resources.windows_16;
            this.btnDetach.Name = "btnDetach";
            this.btnDetach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetach.UnderlineOnHover = true;
            this.btnDetach.Click += new System.EventHandler(this.btnDetach_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleDescription = null;
            this.pictureBox1.AccessibleName = null;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = null;
            this.pictureBox1.Font = null;
            this.pictureBox1.Image = global::CallButler.Manager.Properties.Resources.help2_32;
            this.pictureBox1.ImageLocation = null;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleDescription = null;
            this.btnClose.AccessibleName = null;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.AntiAliasText = false;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = null;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = null;
            this.btnClose.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.LinkImage = global::CallButler.Manager.Properties.Resources.window_error_16;
            this.btnClose.Name = "btnClose";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.UnderlineOnHover = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dividerLine1
            // 
            this.dividerLine1.AccessibleDescription = null;
            this.dividerLine1.AccessibleName = null;
            resources.ApplyResources(this.dividerLine1, "dividerLine1");
            this.dividerLine1.BackColor = System.Drawing.Color.Transparent;
            this.dividerLine1.BackgroundImage = null;
            this.dividerLine1.Font = null;
            this.dividerLine1.ForeColor = System.Drawing.Color.Silver;
            this.dividerLine1.GradientWidth = 10;
            this.dividerLine1.LineWidth = 1;
            this.dividerLine1.Name = "dividerLine1";
            this.dividerLine1.Vertical = false;
            // 
            // HelpView
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.roundedCornerPanel1);
            this.Name = "HelpView";
            this.roundedCornerPanel1.ResumeLayout(false);
            this.roundedCornerPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private global::Controls.RoundedCornerPanel roundedCornerPanel1;
        private global::Controls.LinkButton btnClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private global::Controls.DividerLine dividerLine1;
        private global::Controls.LinkButton btnDetach;
        private System.Windows.Forms.RichTextBox txtHelp;
    }
}
