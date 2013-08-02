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

using Controls;

namespace CallButler.Manager.Controls
{
    partial class ExtensionContactControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtensionContactControl));
            this.btnAdd = new global::Controls.LinkButton();
            this.pnlFlow = new System.Windows.Forms.Panel();
            this.lblCallBlast = new global::Controls.SmoothLabel();
            this.cbCallBlast = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleDescription = null;
            this.btnAdd.AccessibleName = null;
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.AntiAliasText = false;
            this.btnAdd.BackgroundImage = null;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.LinkImage = global::CallButler.Manager.Controls.Properties.Resources.user1_mobilephone_16;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.UnderlineOnHover = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // pnlFlow
            // 
            this.pnlFlow.AccessibleDescription = null;
            this.pnlFlow.AccessibleName = null;
            resources.ApplyResources(this.pnlFlow, "pnlFlow");
            this.pnlFlow.BackgroundImage = null;
            this.pnlFlow.Font = null;
            this.pnlFlow.Name = "pnlFlow";
            // 
            // lblCallBlast
            // 
            this.lblCallBlast.AccessibleDescription = null;
            this.lblCallBlast.AccessibleName = null;
            resources.ApplyResources(this.lblCallBlast, "lblCallBlast");
            this.lblCallBlast.AntiAliasText = false;
            this.lblCallBlast.BackColor = System.Drawing.Color.Transparent;
            this.lblCallBlast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCallBlast.EnableHelp = true;
            this.lblCallBlast.Font = null;
            this.lblCallBlast.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lblCallBlast.Name = "lblCallBlast";
            this.lblCallBlast.Click += new System.EventHandler(this.lblCallBlast_Click);
            // 
            // cbCallBlast
            // 
            this.cbCallBlast.AccessibleDescription = null;
            this.cbCallBlast.AccessibleName = null;
            resources.ApplyResources(this.cbCallBlast, "cbCallBlast");
            this.cbCallBlast.BackgroundImage = null;
            this.cbCallBlast.Name = "cbCallBlast";
            this.cbCallBlast.UseVisualStyleBackColor = true;
            this.cbCallBlast.CheckedChanged += new System.EventHandler(this.cbCallBlast_CheckedChanged);
            // 
            // ExtensionContactControl
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = null;
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblCallBlast);
            this.Controls.Add(this.cbCallBlast);
            this.Controls.Add(this.pnlFlow);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.Name = "ExtensionContactControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkButton btnAdd;
        private System.Windows.Forms.Panel pnlFlow;
        private SmoothLabel lblCallBlast;
        private System.Windows.Forms.CheckBox cbCallBlast;
    }
}
