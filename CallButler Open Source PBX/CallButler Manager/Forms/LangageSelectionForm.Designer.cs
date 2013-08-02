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
    partial class LangageSelectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LangageSelectionForm));
            this.lbAllLanguages = new System.Windows.Forms.ListBox();
            this.lbSelectedLanguages = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRemoveSelectedLanguage = new System.Windows.Forms.Button();
            this.btnRemoveAllLanguages = new System.Windows.Forms.Button();
            this.btnAddAllLanguages = new System.Windows.Forms.Button();
            this.btnAddSelectedLanguage = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbAllLanguages
            // 
            this.lbAllLanguages.AccessibleDescription = null;
            this.lbAllLanguages.AccessibleName = null;
            resources.ApplyResources(this.lbAllLanguages, "lbAllLanguages");
            this.lbAllLanguages.BackgroundImage = null;
            this.lbAllLanguages.Font = null;
            this.lbAllLanguages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lbAllLanguages.FormattingEnabled = true;
            this.lbAllLanguages.Name = "lbAllLanguages";
            this.lbAllLanguages.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAllLanguages.Sorted = true;
            this.lbAllLanguages.SelectedIndexChanged += new System.EventHandler(this.lbAllLanguages_SelectedIndexChanged);
            // 
            // lbSelectedLanguages
            // 
            this.lbSelectedLanguages.AccessibleDescription = null;
            this.lbSelectedLanguages.AccessibleName = null;
            resources.ApplyResources(this.lbSelectedLanguages, "lbSelectedLanguages");
            this.lbSelectedLanguages.BackgroundImage = null;
            this.lbSelectedLanguages.Font = null;
            this.lbSelectedLanguages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lbSelectedLanguages.FormattingEnabled = true;
            this.lbSelectedLanguages.Name = "lbSelectedLanguages";
            this.lbSelectedLanguages.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbSelectedLanguages.Sorted = true;
            this.lbSelectedLanguages.SelectedIndexChanged += new System.EventHandler(this.lbSelectedLanguages_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleDescription = null;
            this.btnOK.AccessibleName = null;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackgroundImage = null;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = null;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
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
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = null;
            this.label2.Name = "label2";
            // 
            // btnRemoveSelectedLanguage
            // 
            this.btnRemoveSelectedLanguage.AccessibleDescription = null;
            this.btnRemoveSelectedLanguage.AccessibleName = null;
            resources.ApplyResources(this.btnRemoveSelectedLanguage, "btnRemoveSelectedLanguage");
            this.btnRemoveSelectedLanguage.BackgroundImage = null;
            this.btnRemoveSelectedLanguage.Font = null;
            this.btnRemoveSelectedLanguage.Image = global::CallButler.Manager.Properties.Resources.navigate_left_16;
            this.btnRemoveSelectedLanguage.Name = "btnRemoveSelectedLanguage";
            this.btnRemoveSelectedLanguage.UseVisualStyleBackColor = true;
            this.btnRemoveSelectedLanguage.Click += new System.EventHandler(this.btnRemoveSelectedLanguage_Click);
            // 
            // btnRemoveAllLanguages
            // 
            this.btnRemoveAllLanguages.AccessibleDescription = null;
            this.btnRemoveAllLanguages.AccessibleName = null;
            resources.ApplyResources(this.btnRemoveAllLanguages, "btnRemoveAllLanguages");
            this.btnRemoveAllLanguages.BackgroundImage = null;
            this.btnRemoveAllLanguages.Font = null;
            this.btnRemoveAllLanguages.Image = global::CallButler.Manager.Properties.Resources.navigate_left2_16;
            this.btnRemoveAllLanguages.Name = "btnRemoveAllLanguages";
            this.btnRemoveAllLanguages.UseVisualStyleBackColor = true;
            this.btnRemoveAllLanguages.Click += new System.EventHandler(this.btnRemoveAllLanguages_Click);
            // 
            // btnAddAllLanguages
            // 
            this.btnAddAllLanguages.AccessibleDescription = null;
            this.btnAddAllLanguages.AccessibleName = null;
            resources.ApplyResources(this.btnAddAllLanguages, "btnAddAllLanguages");
            this.btnAddAllLanguages.BackgroundImage = null;
            this.btnAddAllLanguages.Font = null;
            this.btnAddAllLanguages.Image = global::CallButler.Manager.Properties.Resources.navigate_right2_16;
            this.btnAddAllLanguages.Name = "btnAddAllLanguages";
            this.btnAddAllLanguages.UseVisualStyleBackColor = true;
            this.btnAddAllLanguages.Click += new System.EventHandler(this.btnAddAllLanguages_Click);
            // 
            // btnAddSelectedLanguage
            // 
            this.btnAddSelectedLanguage.AccessibleDescription = null;
            this.btnAddSelectedLanguage.AccessibleName = null;
            resources.ApplyResources(this.btnAddSelectedLanguage, "btnAddSelectedLanguage");
            this.btnAddSelectedLanguage.BackgroundImage = null;
            this.btnAddSelectedLanguage.Font = null;
            this.btnAddSelectedLanguage.Image = global::CallButler.Manager.Properties.Resources.navigate_right_16;
            this.btnAddSelectedLanguage.Name = "btnAddSelectedLanguage";
            this.btnAddSelectedLanguage.UseVisualStyleBackColor = true;
            this.btnAddSelectedLanguage.Click += new System.EventHandler(this.btnAddSelectedLanguage_Click);
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = null;
            this.label3.Name = "label3";
            // 
            // LangageSelectionForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = null;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRemoveSelectedLanguage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRemoveAllLanguages);
            this.Controls.Add(this.btnAddAllLanguages);
            this.Controls.Add(this.btnAddSelectedLanguage);
            this.Controls.Add(this.lbSelectedLanguages);
            this.Controls.Add(this.lbAllLanguages);
            this.HeaderImage = global::CallButler.Manager.Properties.Resources.earth2_32_shadow;
            this.Icon = null;
            this.Name = "LangageSelectionForm";
            this.Controls.SetChildIndex(this.lbAllLanguages, 0);
            this.Controls.SetChildIndex(this.lbSelectedLanguages, 0);
            this.Controls.SetChildIndex(this.btnAddSelectedLanguage, 0);
            this.Controls.SetChildIndex(this.btnAddAllLanguages, 0);
            this.Controls.SetChildIndex(this.btnRemoveAllLanguages, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnRemoveSelectedLanguage, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbAllLanguages;
        private System.Windows.Forms.ListBox lbSelectedLanguages;
        private System.Windows.Forms.Button btnAddSelectedLanguage;
        private System.Windows.Forms.Button btnAddAllLanguages;
        private System.Windows.Forms.Button btnRemoveAllLanguages;
        private System.Windows.Forms.Button btnRemoveSelectedLanguage;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}