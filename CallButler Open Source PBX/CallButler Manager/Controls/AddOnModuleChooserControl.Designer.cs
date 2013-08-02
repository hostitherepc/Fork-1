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
    partial class AddOnModuleChooserControl
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
            this.btnConfigureModule = new global::Controls.LinkButton();
            this.smoothLabel3 = new global::Controls.SmoothLabel();
            this.lbModules = new global::Controls.ListBoxEx();
            this.SuspendLayout();
            // 
            // btnConfigureModule
            // 
            this.btnConfigureModule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfigureModule.AntiAliasText = false;
            this.btnConfigureModule.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConfigureModule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfigureModule.Enabled = false;
            this.btnConfigureModule.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnConfigureModule.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnConfigureModule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfigureModule.LinkImage = global::CallButler.Manager.Properties.Resources.gear_connection_24;
            this.btnConfigureModule.Location = new System.Drawing.Point(382, 324);
            this.btnConfigureModule.Name = "btnConfigureModule";
            this.btnConfigureModule.Size = new System.Drawing.Size(171, 29);
            this.btnConfigureModule.TabIndex = 34;
            this.btnConfigureModule.Text = "Configure Selected Module...";
            this.btnConfigureModule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfigureModule.UnderlineOnHover = true;
            this.btnConfigureModule.Click += new System.EventHandler(this.btnConfigureModule_Click);
            // 
            // smoothLabel3
            // 
            this.smoothLabel3.AntiAliasText = false;
            this.smoothLabel3.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel3.EnableHelp = true;
            this.smoothLabel3.HelpText = "Choose a module from the list below.";
            this.smoothLabel3.HelpTitle = "Choose a Module";
            this.smoothLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.smoothLabel3.Location = new System.Drawing.Point(3, 0);
            this.smoothLabel3.Name = "smoothLabel3";
            this.smoothLabel3.Size = new System.Drawing.Size(102, 13);
            this.smoothLabel3.TabIndex = 33;
            this.smoothLabel3.Text = "Choose a Module";
            // 
            // lbModules
            // 
            this.lbModules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbModules.AntiAliasText = false;
            this.lbModules.BackColor = System.Drawing.Color.White;
            this.lbModules.BorderColor = System.Drawing.Color.Gray;
            this.lbModules.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lbModules.CaptionFont = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbModules.DisplayMember = "Name";
            this.lbModules.DrawBorder = false;
            this.lbModules.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbModules.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lbModules.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lbModules.FormattingEnabled = true;
            this.lbModules.ItemMargin = 5;
            this.lbModules.Location = new System.Drawing.Point(3, 16);
            this.lbModules.Name = "lbModules";
            this.lbModules.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(225)))), ((int)(((byte)(244)))));
            this.lbModules.Size = new System.Drawing.Size(547, 308);
            this.lbModules.TabIndex = 32;
            this.lbModules.SelectedIndexChanged += new System.EventHandler(this.lbModules_SelectedIndexChanged);
            // 
            // AddOnModuleChooserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnConfigureModule);
            this.Controls.Add(this.smoothLabel3);
            this.Controls.Add(this.lbModules);
            this.Name = "AddOnModuleChooserControl";
            this.Size = new System.Drawing.Size(553, 356);
            this.ResumeLayout(false);

        }

        #endregion

        private global::Controls.LinkButton btnConfigureModule;
        private global::Controls.SmoothLabel smoothLabel3;
        private global::Controls.ListBoxEx lbModules;
    }
}
