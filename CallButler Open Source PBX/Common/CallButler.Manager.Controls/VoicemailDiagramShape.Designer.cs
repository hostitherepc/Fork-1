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
    partial class VoicemailDiagramShape
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
            this.roundedCornerPanel1 = new global::Controls.RoundedCornerPanel();
            this.lblNumber = new global::Controls.SmoothLabel();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.roundedCornerPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // roundedCornerPanel1
            // 
            this.roundedCornerPanel1.BorderSize = 1F;
            this.roundedCornerPanel1.Controls.Add(this.lblNumber);
            this.roundedCornerPanel1.Controls.Add(this.pbIcon);
            this.roundedCornerPanel1.CornerRadius = 10;
            this.roundedCornerPanel1.DisplayShadow = true;
            this.roundedCornerPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundedCornerPanel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundedCornerPanel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.roundedCornerPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundedCornerPanel1.Name = "roundedCornerPanel1";
            this.roundedCornerPanel1.Padding = new System.Windows.Forms.Padding(5, 5, 6, 6);
            this.roundedCornerPanel1.PanelColor = System.Drawing.Color.White;
            this.roundedCornerPanel1.ShadowColor = System.Drawing.Color.LightGray;
            this.roundedCornerPanel1.ShadowOffset = 3;
            this.roundedCornerPanel1.Size = new System.Drawing.Size(407, 54);
            this.roundedCornerPanel1.TabIndex = 3;
            this.roundedCornerPanel1.Text = "roundedCornerPanel1";
            // 
            // lblNumber
            // 
            this.lblNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumber.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblNumber.Location = new System.Drawing.Point(43, 14);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(184, 24);
            this.lblNumber.TabIndex = 25;
            this.lblNumber.Text = "Take a voicemail";
            this.lblNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbIcon
            // 
            this.pbIcon.BackColor = System.Drawing.Color.Transparent;
            this.pbIcon.Image = global::CallButler.Manager.Controls.Properties.Resources.call_greeting_24;
            this.pbIcon.Location = new System.Drawing.Point(13, 14);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(24, 24);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbIcon.TabIndex = 1;
            this.pbIcon.TabStop = false;
            // 
            // VoicemailDiagramShape
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.roundedCornerPanel1);
            this.Name = "VoicemailDiagramShape";
            this.Size = new System.Drawing.Size(407, 54);
            this.roundedCornerPanel1.ResumeLayout(false);
            this.roundedCornerPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private global::Controls.RoundedCornerPanel roundedCornerPanel1;
        private global::Controls.SmoothLabel lblNumber;
        private System.Windows.Forms.PictureBox pbIcon;
    }
}
