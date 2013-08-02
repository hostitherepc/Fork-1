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

namespace Controls
{
    partial class TrackBarEx
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
            this.pbTracker = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbTracker)).BeginInit();
            this.SuspendLayout();
            // 
            // pbTracker
            // 
            this.pbTracker.BackColor = System.Drawing.Color.Transparent;
            this.pbTracker.Location = new System.Drawing.Point(3, 3);
            this.pbTracker.Name = "pbTracker";
            this.pbTracker.Size = new System.Drawing.Size(27, 26);
            this.pbTracker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbTracker.TabIndex = 0;
            this.pbTracker.TabStop = false;
            this.pbTracker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbTracker_MouseDown);
            this.pbTracker.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbTracker_MouseMove);
            this.pbTracker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbTracker_MouseUp);
            // 
            // TrackBarEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbTracker);
            this.Name = "TrackBarEx";
            this.Size = new System.Drawing.Size(324, 33);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TrackBarEx_MouseClick);
            this.Resize += new System.EventHandler(this.TrackBarEx_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbTracker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbTracker;
    }
}
