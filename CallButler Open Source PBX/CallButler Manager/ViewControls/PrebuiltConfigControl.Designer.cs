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
    partial class PrebuiltConfigControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrebuiltConfigControl));
            this.label1 = new System.Windows.Forms.Label();
            this.bsConfigData = new System.Windows.Forms.BindingSource(this.components);
            this.prebuiltConfigData = new CallButler.Manager.Data.PrebuiltConfigData();
            this.label2 = new System.Windows.Forms.Label();
            this.fKConfigurationCategoryConfigurationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lbCategories = new global::Controls.ListBoxEx();
            this.lbConfiguration = new global::Controls.ListBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.bsConfigData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prebuiltConfigData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKConfigurationCategoryConfigurationBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // bsConfigData
            // 
            this.bsConfigData.DataMember = "ConfigurationCategory";
            this.bsConfigData.DataSource = this.prebuiltConfigData;
            // 
            // prebuiltConfigData
            // 
            this.prebuiltConfigData.DataSetName = "PrebuiltConfigData";
            this.prebuiltConfigData.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // fKConfigurationCategoryConfigurationBindingSource
            // 
            this.fKConfigurationCategoryConfigurationBindingSource.DataMember = "FK_ConfigurationCategory_Configuration";
            this.fKConfigurationCategoryConfigurationBindingSource.DataSource = this.bsConfigData;
            // 
            // lbCategories
            // 
            resources.ApplyResources(this.lbCategories, "lbCategories");
            this.lbCategories.AntiAliasText = false;
            this.lbCategories.BorderColor = System.Drawing.Color.Gray;
            this.lbCategories.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lbCategories.CaptionFont = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCategories.DataSource = this.bsConfigData;
            this.lbCategories.DisplayMember = "Name";
            this.lbCategories.DrawBorder = false;
            this.lbCategories.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbCategories.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lbCategories.FormattingEnabled = true;
            this.lbCategories.ItemImage = global::CallButler.Manager.Properties.Resources.office_24;
            this.lbCategories.ItemMargin = 5;
            this.lbCategories.Name = "lbCategories";
            this.lbCategories.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(225)))), ((int)(((byte)(244)))));
            this.lbCategories.ValueMember = "CategoryID";
            // 
            // lbConfiguration
            // 
            resources.ApplyResources(this.lbConfiguration, "lbConfiguration");
            this.lbConfiguration.AntiAliasText = false;
            this.lbConfiguration.BorderColor = System.Drawing.Color.Gray;
            this.lbConfiguration.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lbConfiguration.CaptionFont = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConfiguration.DataSource = this.fKConfigurationCategoryConfigurationBindingSource;
            this.lbConfiguration.DisplayMember = "Name";
            this.lbConfiguration.DrawBorder = false;
            this.lbConfiguration.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbConfiguration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lbConfiguration.FormattingEnabled = true;
            this.lbConfiguration.ItemMargin = 5;
            this.lbConfiguration.Name = "lbConfiguration";
            this.lbConfiguration.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(225)))), ((int)(((byte)(244)))));
            this.lbConfiguration.ValueMember = "ConfigurationID";
            this.lbConfiguration.SelectedIndexChanged += new System.EventHandler(this.lbConfiguration_SelectedIndexChanged);
            // 
            // PrebuiltConfigControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbConfiguration);
            this.Controls.Add(this.lbCategories);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.Name = "PrebuiltConfigControl";
            ((System.ComponentModel.ISupportInitialize)(this.bsConfigData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prebuiltConfigData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKConfigurationCategoryConfigurationBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private CallButler.Manager.Data.PrebuiltConfigData prebuiltConfigData;
        private System.Windows.Forms.BindingSource bsConfigData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource fKConfigurationCategoryConfigurationBindingSource;
        private global::Controls.ListBoxEx lbCategories;
        private global::Controls.ListBoxEx lbConfiguration;
    }
}
