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
    partial class CallHistoryView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CallHistoryView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bsCallHistory = new System.Windows.Forms.BindingSource(this.components);
            this.callHistoryDataset = new WOSI.CallButler.Data.CallButlerDataset();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnSaveCallHistory = new System.Windows.Forms.ToolStripButton();
            this.btnPrintHistory = new System.Windows.Forms.ToolStripButton();
            this.btnImportOutlook = new System.Windows.Forms.ToolStripButton();
            this.btnClearAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnClearCriteria = new System.Windows.Forms.ToolStripButton();
            this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.cboFilterBy = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.dgCalls = new CallButler.Manager.Controls.CallButlerDataGrid();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.customerIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timestampDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callerDisplayNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callerUsernameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToUsername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callerHostDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callDurationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ViewInOutlook = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsCallHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.callHistoryDataset)).BeginInit();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCalls)).BeginInit();
            this.SuspendLayout();
            // 
            // bsCallHistory
            // 
            this.bsCallHistory.DataMember = "CallHistory";
            this.bsCallHistory.DataSource = this.callHistoryDataset;
            this.bsCallHistory.Sort = "Timestamp DESC";
            // 
            // callHistoryDataset
            // 
            this.callHistoryDataset.DataSetName = "CallButlerDataset";
            this.callHistoryDataset.RemotingFormat = System.Data.SerializationFormat.Binary;
            this.callHistoryDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveCallHistory,
            this.btnPrintHistory,
            this.btnImportOutlook,
            this.btnClearAll,
            this.toolStripSeparator1,
            this.btnClearCriteria,
            this.txtSearch,
            this.cboFilterBy,
            this.toolStripLabel1});
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Name = "toolStrip";
            // 
            // btnSaveCallHistory
            // 
            this.btnSaveCallHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveCallHistory.Image = global::CallButler.Manager.Properties.Resources.disk_blue_16;
            resources.ApplyResources(this.btnSaveCallHistory, "btnSaveCallHistory");
            this.btnSaveCallHistory.Name = "btnSaveCallHistory";
            this.btnSaveCallHistory.Click += new System.EventHandler(this.btnSaveCallHistory_Click);
            // 
            // btnPrintHistory
            // 
            this.btnPrintHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrintHistory.Image = global::CallButler.Manager.Properties.Resources.printer;
            resources.ApplyResources(this.btnPrintHistory, "btnPrintHistory");
            this.btnPrintHistory.Name = "btnPrintHistory";
            this.btnPrintHistory.Click += new System.EventHandler(this.btnPrintHistory_Click);
            // 
            // btnImportOutlook
            // 
            this.btnImportOutlook.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImportOutlook.Image = global::CallButler.Manager.Properties.Resources.outlook_16;
            resources.ApplyResources(this.btnImportOutlook, "btnImportOutlook");
            this.btnImportOutlook.Name = "btnImportOutlook";
            this.btnImportOutlook.Click += new System.EventHandler(this.btnImportOutlook_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClearAll.Image = global::CallButler.Manager.Properties.Resources.delete_16;
            resources.ApplyResources(this.btnClearAll, "btnClearAll");
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // btnClearCriteria
            // 
            this.btnClearCriteria.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnClearCriteria.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnClearCriteria.Image = global::CallButler.Manager.Properties.Resources.delete_16;
            resources.ApplyResources(this.btnClearCriteria, "btnClearCriteria");
            this.btnClearCriteria.Name = "btnClearCriteria";
            this.btnClearCriteria.Click += new System.EventHandler(this.btnClearCriteria_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Name = "txtSearch";
            resources.ApplyResources(this.txtSearch, "txtSearch");
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Click += new System.EventHandler(this.txtSearch_Click);
            // 
            // cboFilterBy
            // 
            this.cboFilterBy.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cboFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.cboFilterBy.Items.AddRange(new object[] {
            resources.GetString("cboFilterBy.Items"),
            resources.GetString("cboFilterBy.Items1")});
            this.cboFilterBy.Name = "cboFilterBy";
            resources.ApplyResources(this.cboFilterBy, "cboFilterBy");
            this.cboFilterBy.SelectedIndexChanged += new System.EventHandler(this.cboFilterBy_SelectedIndexChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            // 
            // dgCalls
            // 
            this.dgCalls.AllowUserToAddRows = false;
            this.dgCalls.AllowUserToDeleteRows = false;
            this.dgCalls.AllowUserToResizeRows = false;
            this.dgCalls.AutoGenerateColumns = false;
            this.dgCalls.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgCalls.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgCalls.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgCalls.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgCalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCalls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customerIDDataGridViewTextBoxColumn,
            this.callIDDataGridViewTextBoxColumn,
            this.timestampDataGridViewTextBoxColumn,
            this.callerDisplayNameDataGridViewTextBoxColumn,
            this.callerUsernameDataGridViewTextBoxColumn,
            this.ToUsername,
            this.callerHostDataGridViewTextBoxColumn,
            this.callDurationDataGridViewTextBoxColumn,
            this.ViewInOutlook});
            this.dgCalls.DataSource = this.bsCallHistory;
            resources.ApplyResources(this.dgCalls, "dgCalls");
            this.dgCalls.Name = "dgCalls";
            this.dgCalls.ReadOnly = true;
            this.dgCalls.RowHeadersVisible = false;
            this.dgCalls.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgCalls.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(225)))), ((int)(((byte)(244)))));
            this.dgCalls.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.dgCalls.RowTemplate.Height = 32;
            this.dgCalls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCalls.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgCalls_ColumnHeaderMouseClick);
            this.dgCalls.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgCalls_CellFormatting);
            this.dgCalls.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCalls_CellContentClick);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xml";
            this.saveFileDialog.FileName = "CallHistory.xml";
            resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
            // 
            // customerIDDataGridViewTextBoxColumn
            // 
            this.customerIDDataGridViewTextBoxColumn.DataPropertyName = "CustomerID";
            resources.ApplyResources(this.customerIDDataGridViewTextBoxColumn, "customerIDDataGridViewTextBoxColumn");
            this.customerIDDataGridViewTextBoxColumn.Name = "customerIDDataGridViewTextBoxColumn";
            this.customerIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // callIDDataGridViewTextBoxColumn
            // 
            this.callIDDataGridViewTextBoxColumn.DataPropertyName = "CallID";
            resources.ApplyResources(this.callIDDataGridViewTextBoxColumn, "callIDDataGridViewTextBoxColumn");
            this.callIDDataGridViewTextBoxColumn.Name = "callIDDataGridViewTextBoxColumn";
            this.callIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // timestampDataGridViewTextBoxColumn
            // 
            this.timestampDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.timestampDataGridViewTextBoxColumn.DataPropertyName = "Timestamp";
            resources.ApplyResources(this.timestampDataGridViewTextBoxColumn, "timestampDataGridViewTextBoxColumn");
            this.timestampDataGridViewTextBoxColumn.Name = "timestampDataGridViewTextBoxColumn";
            this.timestampDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // callerDisplayNameDataGridViewTextBoxColumn
            // 
            this.callerDisplayNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.callerDisplayNameDataGridViewTextBoxColumn.DataPropertyName = "CallerDisplayName";
            resources.ApplyResources(this.callerDisplayNameDataGridViewTextBoxColumn, "callerDisplayNameDataGridViewTextBoxColumn");
            this.callerDisplayNameDataGridViewTextBoxColumn.Name = "callerDisplayNameDataGridViewTextBoxColumn";
            this.callerDisplayNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // callerUsernameDataGridViewTextBoxColumn
            // 
            this.callerUsernameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.callerUsernameDataGridViewTextBoxColumn.DataPropertyName = "CallerUsername";
            resources.ApplyResources(this.callerUsernameDataGridViewTextBoxColumn, "callerUsernameDataGridViewTextBoxColumn");
            this.callerUsernameDataGridViewTextBoxColumn.Name = "callerUsernameDataGridViewTextBoxColumn";
            this.callerUsernameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ToUsername
            // 
            this.ToUsername.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ToUsername.DataPropertyName = "ToUsername";
            resources.ApplyResources(this.ToUsername, "ToUsername");
            this.ToUsername.Name = "ToUsername";
            this.ToUsername.ReadOnly = true;
            // 
            // callerHostDataGridViewTextBoxColumn
            // 
            this.callerHostDataGridViewTextBoxColumn.DataPropertyName = "CallerHost";
            resources.ApplyResources(this.callerHostDataGridViewTextBoxColumn, "callerHostDataGridViewTextBoxColumn");
            this.callerHostDataGridViewTextBoxColumn.Name = "callerHostDataGridViewTextBoxColumn";
            this.callerHostDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // callDurationDataGridViewTextBoxColumn
            // 
            this.callDurationDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.callDurationDataGridViewTextBoxColumn.DataPropertyName = "CallDuration";
            dataGridViewCellStyle1.Format = "000:00";
            dataGridViewCellStyle1.NullValue = null;
            this.callDurationDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.callDurationDataGridViewTextBoxColumn, "callDurationDataGridViewTextBoxColumn");
            this.callDurationDataGridViewTextBoxColumn.Name = "callDurationDataGridViewTextBoxColumn";
            this.callDurationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ViewInOutlook
            // 
            resources.ApplyResources(this.ViewInOutlook, "ViewInOutlook");
            this.ViewInOutlook.Name = "ViewInOutlook";
            this.ViewInOutlook.ReadOnly = true;
            this.ViewInOutlook.Text = "View in Outlook";
            this.ViewInOutlook.UseColumnTextForLinkValue = true;
            // 
            // CallHistoryView
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.dgCalls);
            this.Controls.Add(this.toolStrip);
            this.HeaderIcon = global::CallButler.Manager.Properties.Resources.history2_shadow_42;
            this.Name = "CallHistoryView";
            this.Controls.SetChildIndex(this.toolStrip, 0);
            this.Controls.SetChildIndex(this.dgCalls, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bsCallHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.callHistoryDataset)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCalls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private WOSI.CallButler.Data.CallButlerDataset callHistoryDataset;
        private System.Windows.Forms.BindingSource bsCallHistory;
      private CallButler.Manager.Controls.CallButlerDataGrid dgCalls;
        private System.Windows.Forms.ToolStripButton btnSaveCallHistory;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cboFilterBy;
        private System.Windows.Forms.ToolStripTextBox txtSearch;
        private System.Windows.Forms.ToolStripButton btnClearCriteria;
      private System.Windows.Forms.ToolStripButton btnPrintHistory;
        private System.Windows.Forms.ToolStripButton btnImportOutlook;
        private System.Windows.Forms.ToolStripButton btnClearAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callerDisplayNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callerUsernameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToUsername;
        private System.Windows.Forms.DataGridViewTextBoxColumn callerHostDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callDurationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn ViewInOutlook;
    }
}
