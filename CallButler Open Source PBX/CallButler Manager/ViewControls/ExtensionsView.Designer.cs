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
    partial class ExtensionsView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtensionsView));
            this.callButlerDataset = new WOSI.CallButler.Data.CallButlerDataset();
            this.bsExtensions = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddNewExtension = new global::Controls.LinkButton();
            this.dgExtensions = new CallButler.Manager.Controls.CallButlerEditDataGrid();
            this.customerIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extensionIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extensionNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enableSearchDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.passwordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewMessagesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlayMessagesColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.callButlerDataset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsExtensions)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExtensions)).BeginInit();
            this.SuspendLayout();
            // 
            // callButlerDataset
            // 
            this.callButlerDataset.DataSetName = "CallButlerDataset";
            this.callButlerDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bsExtensions
            // 
            this.bsExtensions.DataMember = "Extensions";
            this.bsExtensions.DataSource = this.callButlerDataset;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAddNewExtension);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnAddNewExtension
            // 
            this.btnAddNewExtension.AntiAliasText = false;
            resources.ApplyResources(this.btnAddNewExtension, "btnAddNewExtension");
            this.btnAddNewExtension.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNewExtension.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAddNewExtension.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewExtension.LinkImage = global::CallButler.Manager.Properties.Resources.user1_telephone_16;
            this.btnAddNewExtension.Name = "btnAddNewExtension";
            this.btnAddNewExtension.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewExtension.UnderlineOnHover = true;
            this.btnAddNewExtension.Click += new System.EventHandler(this.btnAddNewExtension_Click);
            // 
            // dgExtensions
            // 
            this.dgExtensions.AllowUserToAddRows = false;
            this.dgExtensions.AllowUserToDeleteRows = false;
            this.dgExtensions.AllowUserToResizeRows = false;
            this.dgExtensions.AutoGenerateColumns = false;
            this.dgExtensions.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgExtensions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgExtensions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgExtensions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgExtensions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgExtensions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customerIDDataGridViewTextBoxColumn,
            this.extensionIDDataGridViewTextBoxColumn,
            this.extensionNumberDataGridViewTextBoxColumn,
            this.firstNameDataGridViewTextBoxColumn,
            this.lastNameDataGridViewTextBoxColumn,
            this.searchNumberDataGridViewTextBoxColumn,
            this.enableSearchDataGridViewCheckBoxColumn,
            this.passwordDataGridViewTextBoxColumn,
            this.NewMessagesColumn,
            this.PlayMessagesColumn});
            this.dgExtensions.DataSource = this.bsExtensions;
            resources.ApplyResources(this.dgExtensions, "dgExtensions");
            this.dgExtensions.Name = "dgExtensions";
            this.dgExtensions.ReadOnly = true;
            this.dgExtensions.RowHeadersVisible = false;
            this.dgExtensions.RowImage = global::CallButler.Manager.Properties.Resources.user1_telephone_24;
            this.dgExtensions.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgExtensions.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(225)))), ((int)(((byte)(244)))));
            this.dgExtensions.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.dgExtensions.RowTemplate.Height = 32;
            this.dgExtensions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgExtensions.ShowDeleteColumn = true;
            this.dgExtensions.ShowEditColumn = true;
            this.dgExtensions.EditDataRow += new System.EventHandler<CallButler.Manager.Controls.DataRowEventArgs>(this.dgExtensions_EditDataRow);
            this.dgExtensions.DeleteDataRow += new System.EventHandler<CallButler.Manager.Controls.DataRowEventArgs>(this.dgExtensions_DeleteDataRow);
            this.dgExtensions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgExtensions_CellFormatting);
            this.dgExtensions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgExtensions_CellContentClick);
            // 
            // customerIDDataGridViewTextBoxColumn
            // 
            this.customerIDDataGridViewTextBoxColumn.DataPropertyName = "CustomerID";
            resources.ApplyResources(this.customerIDDataGridViewTextBoxColumn, "customerIDDataGridViewTextBoxColumn");
            this.customerIDDataGridViewTextBoxColumn.Name = "customerIDDataGridViewTextBoxColumn";
            this.customerIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // extensionIDDataGridViewTextBoxColumn
            // 
            this.extensionIDDataGridViewTextBoxColumn.DataPropertyName = "ExtensionID";
            resources.ApplyResources(this.extensionIDDataGridViewTextBoxColumn, "extensionIDDataGridViewTextBoxColumn");
            this.extensionIDDataGridViewTextBoxColumn.Name = "extensionIDDataGridViewTextBoxColumn";
            this.extensionIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // extensionNumberDataGridViewTextBoxColumn
            // 
            this.extensionNumberDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.extensionNumberDataGridViewTextBoxColumn.DataPropertyName = "ExtensionNumber";
            resources.ApplyResources(this.extensionNumberDataGridViewTextBoxColumn, "extensionNumberDataGridViewTextBoxColumn");
            this.extensionNumberDataGridViewTextBoxColumn.Name = "extensionNumberDataGridViewTextBoxColumn";
            this.extensionNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // firstNameDataGridViewTextBoxColumn
            // 
            this.firstNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
            resources.ApplyResources(this.firstNameDataGridViewTextBoxColumn, "firstNameDataGridViewTextBoxColumn");
            this.firstNameDataGridViewTextBoxColumn.Name = "firstNameDataGridViewTextBoxColumn";
            this.firstNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastNameDataGridViewTextBoxColumn
            // 
            this.lastNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
            resources.ApplyResources(this.lastNameDataGridViewTextBoxColumn, "lastNameDataGridViewTextBoxColumn");
            this.lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
            this.lastNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // searchNumberDataGridViewTextBoxColumn
            // 
            this.searchNumberDataGridViewTextBoxColumn.DataPropertyName = "SearchNumber";
            resources.ApplyResources(this.searchNumberDataGridViewTextBoxColumn, "searchNumberDataGridViewTextBoxColumn");
            this.searchNumberDataGridViewTextBoxColumn.Name = "searchNumberDataGridViewTextBoxColumn";
            this.searchNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // enableSearchDataGridViewCheckBoxColumn
            // 
            this.enableSearchDataGridViewCheckBoxColumn.DataPropertyName = "EnableSearch";
            resources.ApplyResources(this.enableSearchDataGridViewCheckBoxColumn, "enableSearchDataGridViewCheckBoxColumn");
            this.enableSearchDataGridViewCheckBoxColumn.Name = "enableSearchDataGridViewCheckBoxColumn";
            this.enableSearchDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // passwordDataGridViewTextBoxColumn
            // 
            this.passwordDataGridViewTextBoxColumn.DataPropertyName = "Password";
            resources.ApplyResources(this.passwordDataGridViewTextBoxColumn, "passwordDataGridViewTextBoxColumn");
            this.passwordDataGridViewTextBoxColumn.Name = "passwordDataGridViewTextBoxColumn";
            this.passwordDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // NewMessagesColumn
            // 
            this.NewMessagesColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(this.NewMessagesColumn, "NewMessagesColumn");
            this.NewMessagesColumn.Name = "NewMessagesColumn";
            this.NewMessagesColumn.ReadOnly = true;
            // 
            // PlayMessagesColumn
            // 
            this.PlayMessagesColumn.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.PlayMessagesColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(this.PlayMessagesColumn, "PlayMessagesColumn");
            this.PlayMessagesColumn.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.PlayMessagesColumn.LinkColor = System.Drawing.Color.RoyalBlue;
            this.PlayMessagesColumn.Name = "PlayMessagesColumn";
            this.PlayMessagesColumn.ReadOnly = true;
            this.PlayMessagesColumn.Text = "Play Voicemail...";
            this.PlayMessagesColumn.TrackVisitedState = false;
            this.PlayMessagesColumn.UseColumnTextForLinkValue = true;
            // 
            // ExtensionsView
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.dgExtensions);
            this.Controls.Add(this.panel1);
            this.HeaderIcon = global::CallButler.Manager.Properties.Resources.user1_telephone_48_shadow;
            this.Name = "ExtensionsView";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.dgExtensions, 0);
            ((System.ComponentModel.ISupportInitialize)(this.callButlerDataset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsExtensions)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgExtensions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private WOSI.CallButler.Data.CallButlerDataset callButlerDataset;
        private System.Windows.Forms.BindingSource bsExtensions;
        private System.Windows.Forms.Panel panel1;
        private global::Controls.LinkButton btnAddNewExtension;
        private CallButler.Manager.Controls.CallButlerEditDataGrid dgExtensions;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn extensionIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn extensionNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn searchNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enableSearchDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn passwordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewMessagesColumn;
        private System.Windows.Forms.DataGridViewLinkColumn PlayMessagesColumn;
    }
}
