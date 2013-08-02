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
    partial class ProvidersView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProvidersView));
            this.callButlerDataset = new WOSI.CallButler.Data.CallButlerDataset();
            this.bsProviders = new System.Windows.Forms.BindingSource(this.components);
            this.providerGrid = new CallButler.Manager.Controls.CallButlerEditDataGrid();
            this.customerIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.displayNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usernameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.authUsernameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.authPasswordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.domainDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sIPProxyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sIPRegistrarDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expiresDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isEnabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsDefault = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddNewProvider = new global::Controls.LinkButton();
            ((System.ComponentModel.ISupportInitialize)(this.callButlerDataset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProviders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.providerGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // callButlerDataset
            // 
            this.callButlerDataset.DataSetName = "CallButlerDataset";
            this.callButlerDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bsProviders
            // 
            this.bsProviders.DataMember = "Providers";
            this.bsProviders.DataSource = this.callButlerDataset;
            // 
            // providerGrid
            // 
            this.providerGrid.AllowUserToAddRows = false;
            this.providerGrid.AllowUserToDeleteRows = false;
            this.providerGrid.AllowUserToResizeRows = false;
            this.providerGrid.AutoGenerateColumns = false;
            this.providerGrid.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.providerGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.providerGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.providerGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.providerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.providerGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customerIDDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn1,
            this.nameDataGridViewTextBoxColumn,
            this.displayNameDataGridViewTextBoxColumn,
            this.usernameDataGridViewTextBoxColumn,
            this.authUsernameDataGridViewTextBoxColumn,
            this.authPasswordDataGridViewTextBoxColumn,
            this.domainDataGridViewTextBoxColumn,
            this.sIPProxyDataGridViewTextBoxColumn,
            this.sIPRegistrarDataGridViewTextBoxColumn,
            this.expiresDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.isEnabledDataGridViewCheckBoxColumn,
            this.IsDefault});
            this.providerGrid.DataSource = this.bsProviders;
            resources.ApplyResources(this.providerGrid, "providerGrid");
            this.providerGrid.MultiSelect = false;
            this.providerGrid.Name = "providerGrid";
            this.providerGrid.ReadOnly = true;
            this.providerGrid.RowHeadersVisible = false;
            this.providerGrid.RowImage = global::CallButler.Manager.Properties.Resources.earth2_16;
            this.providerGrid.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.providerGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(225)))), ((int)(((byte)(244)))));
            this.providerGrid.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.providerGrid.RowTemplate.Height = 32;
            this.providerGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.providerGrid.ShowDeleteColumn = true;
            this.providerGrid.ShowEditColumn = true;
            this.providerGrid.DeleteDataRow += new System.EventHandler<CallButler.Manager.Controls.DataRowEventArgs>(this.providerGrid_DeleteDataRow);
            this.providerGrid.EditDataRow += new System.EventHandler<CallButler.Manager.Controls.DataRowEventArgs>(this.providerGrid_EditDataRow);
            this.providerGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.providerGrid_CellFormatting);
            // 
            // customerIDDataGridViewTextBoxColumn
            // 
            this.customerIDDataGridViewTextBoxColumn.DataPropertyName = "CustomerID";
            resources.ApplyResources(this.customerIDDataGridViewTextBoxColumn, "customerIDDataGridViewTextBoxColumn");
            this.customerIDDataGridViewTextBoxColumn.Name = "customerIDDataGridViewTextBoxColumn";
            this.customerIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ProviderID";
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            resources.ApplyResources(this.nameDataGridViewTextBoxColumn, "nameDataGridViewTextBoxColumn");
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // displayNameDataGridViewTextBoxColumn
            // 
            this.displayNameDataGridViewTextBoxColumn.DataPropertyName = "DisplayName";
            resources.ApplyResources(this.displayNameDataGridViewTextBoxColumn, "displayNameDataGridViewTextBoxColumn");
            this.displayNameDataGridViewTextBoxColumn.Name = "displayNameDataGridViewTextBoxColumn";
            this.displayNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // usernameDataGridViewTextBoxColumn
            // 
            this.usernameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.usernameDataGridViewTextBoxColumn.DataPropertyName = "Username";
            resources.ApplyResources(this.usernameDataGridViewTextBoxColumn, "usernameDataGridViewTextBoxColumn");
            this.usernameDataGridViewTextBoxColumn.Name = "usernameDataGridViewTextBoxColumn";
            this.usernameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // authUsernameDataGridViewTextBoxColumn
            // 
            this.authUsernameDataGridViewTextBoxColumn.DataPropertyName = "AuthUsername";
            resources.ApplyResources(this.authUsernameDataGridViewTextBoxColumn, "authUsernameDataGridViewTextBoxColumn");
            this.authUsernameDataGridViewTextBoxColumn.Name = "authUsernameDataGridViewTextBoxColumn";
            this.authUsernameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // authPasswordDataGridViewTextBoxColumn
            // 
            this.authPasswordDataGridViewTextBoxColumn.DataPropertyName = "AuthPassword";
            resources.ApplyResources(this.authPasswordDataGridViewTextBoxColumn, "authPasswordDataGridViewTextBoxColumn");
            this.authPasswordDataGridViewTextBoxColumn.Name = "authPasswordDataGridViewTextBoxColumn";
            this.authPasswordDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // domainDataGridViewTextBoxColumn
            // 
            this.domainDataGridViewTextBoxColumn.DataPropertyName = "Domain";
            resources.ApplyResources(this.domainDataGridViewTextBoxColumn, "domainDataGridViewTextBoxColumn");
            this.domainDataGridViewTextBoxColumn.Name = "domainDataGridViewTextBoxColumn";
            this.domainDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sIPProxyDataGridViewTextBoxColumn
            // 
            this.sIPProxyDataGridViewTextBoxColumn.DataPropertyName = "SIPProxy";
            resources.ApplyResources(this.sIPProxyDataGridViewTextBoxColumn, "sIPProxyDataGridViewTextBoxColumn");
            this.sIPProxyDataGridViewTextBoxColumn.Name = "sIPProxyDataGridViewTextBoxColumn";
            this.sIPProxyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sIPRegistrarDataGridViewTextBoxColumn
            // 
            this.sIPRegistrarDataGridViewTextBoxColumn.DataPropertyName = "SIPRegistrar";
            resources.ApplyResources(this.sIPRegistrarDataGridViewTextBoxColumn, "sIPRegistrarDataGridViewTextBoxColumn");
            this.sIPRegistrarDataGridViewTextBoxColumn.Name = "sIPRegistrarDataGridViewTextBoxColumn";
            this.sIPRegistrarDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // expiresDataGridViewTextBoxColumn
            // 
            this.expiresDataGridViewTextBoxColumn.DataPropertyName = "Expires";
            resources.ApplyResources(this.expiresDataGridViewTextBoxColumn, "expiresDataGridViewTextBoxColumn");
            this.expiresDataGridViewTextBoxColumn.Name = "expiresDataGridViewTextBoxColumn";
            this.expiresDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            resources.ApplyResources(this.statusDataGridViewTextBoxColumn, "statusDataGridViewTextBoxColumn");
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isEnabledDataGridViewCheckBoxColumn
            // 
            this.isEnabledDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.isEnabledDataGridViewCheckBoxColumn.DataPropertyName = "IsEnabled";
            resources.ApplyResources(this.isEnabledDataGridViewCheckBoxColumn, "isEnabledDataGridViewCheckBoxColumn");
            this.isEnabledDataGridViewCheckBoxColumn.Name = "isEnabledDataGridViewCheckBoxColumn";
            this.isEnabledDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // IsDefault
            // 
            this.IsDefault.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.IsDefault.DataPropertyName = "IsDefault";
            resources.ApplyResources(this.IsDefault, "IsDefault");
            this.IsDefault.Name = "IsDefault";
            this.IsDefault.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAddNewProvider);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnAddNewProvider
            // 
            this.btnAddNewProvider.AntiAliasText = false;
            resources.ApplyResources(this.btnAddNewProvider, "btnAddNewProvider");
            this.btnAddNewProvider.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNewProvider.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAddNewProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewProvider.LinkImage = ((System.Drawing.Image)(resources.GetObject("btnAddNewProvider.LinkImage")));
            this.btnAddNewProvider.Name = "btnAddNewProvider";
            this.btnAddNewProvider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewProvider.UnderlineOnHover = true;
            this.btnAddNewProvider.Click += new System.EventHandler(this.btnAddNewProvider_Click);
            // 
            // ProvidersView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.providerGrid);
            this.Controls.Add(this.panel1);
            this.HeaderIcon = global::CallButler.Manager.Properties.Resources.earth_connection_48_shadow;
            this.Name = "ProvidersView";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.providerGrid, 0);
            ((System.ComponentModel.ISupportInitialize)(this.callButlerDataset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsProviders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.providerGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WOSI.CallButler.Data.CallButlerDataset callButlerDataset;
        private System.Windows.Forms.BindingSource bsProviders;
        private CallButler.Manager.Controls.CallButlerEditDataGrid providerGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn providerIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priorityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uRLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceLowDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceHighDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceRangeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tollFreeSupportDataGridViewTextBoxColumn;
        private System.Windows.Forms.Panel panel1;
        private global::Controls.LinkButton btnAddNewProvider;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn displayNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usernameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn authUsernameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn authPasswordDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn domainDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sIPProxyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sIPRegistrarDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expiresDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isEnabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsDefault;
    }
}
