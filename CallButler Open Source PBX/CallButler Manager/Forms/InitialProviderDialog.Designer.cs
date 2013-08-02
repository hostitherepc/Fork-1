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
    partial class InitialProviderDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitialProviderDialog));
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddNewProvider = new global::Controls.LinkButton();
            this.btnExistingProvider = new global::Controls.LinkButton();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackgroundImage = null;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnAddNewProvider
            // 
            this.btnAddNewProvider.AccessibleDescription = null;
            this.btnAddNewProvider.AccessibleName = null;
            resources.ApplyResources(this.btnAddNewProvider, "btnAddNewProvider");
            this.btnAddNewProvider.AntiAliasText = false;
            this.btnAddNewProvider.BackgroundImage = null;
            this.btnAddNewProvider.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNewProvider.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAddNewProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewProvider.LinkImage = ((System.Drawing.Image)(resources.GetObject("btnAddNewProvider.LinkImage")));
            this.btnAddNewProvider.Name = "btnAddNewProvider";
            this.btnAddNewProvider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewProvider.UnderlineOnHover = true;
            this.btnAddNewProvider.Click += new System.EventHandler(this.btnAddNewProvider_Click);
            // 
            // btnExistingProvider
            // 
            this.btnExistingProvider.AccessibleDescription = null;
            this.btnExistingProvider.AccessibleName = null;
            resources.ApplyResources(this.btnExistingProvider, "btnExistingProvider");
            this.btnExistingProvider.AntiAliasText = false;
            this.btnExistingProvider.BackgroundImage = null;
            this.btnExistingProvider.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExistingProvider.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnExistingProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExistingProvider.LinkImage = ((System.Drawing.Image)(resources.GetObject("btnExistingProvider.LinkImage")));
            this.btnExistingProvider.Name = "btnExistingProvider";
            this.btnExistingProvider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExistingProvider.UnderlineOnHover = true;
            this.btnExistingProvider.Click += new System.EventHandler(this.btnExistingProvider_Click);
            // 
            // InitialProviderDialog
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.btnExistingProvider);
            this.Controls.Add(this.btnAddNewProvider);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.Icon = null;
            this.Name = "InitialProviderDialog";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnAddNewProvider, 0);
            this.Controls.SetChildIndex(this.btnExistingProvider, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private global::Controls.LinkButton btnAddNewProvider;
        private global::Controls.LinkButton btnExistingProvider;
    }
}