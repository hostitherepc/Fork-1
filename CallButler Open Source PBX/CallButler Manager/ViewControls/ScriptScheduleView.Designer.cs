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
    partial class ScriptScheduleView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptScheduleView));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddNewScriptSchedule = new global::Controls.LinkButton();
            this.dgScriptSchedule = new CallButler.Manager.Controls.CallButlerEditDataGrid();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customerIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scriptScheduleIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scriptLocationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priorityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hasHoursOfOperationDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.hoursOfOperationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsScriptSchedules = new System.Windows.Forms.BindingSource(this.components);
            this.callButlerDataset = new WOSI.CallButler.Data.CallButlerDataset();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgScriptSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsScriptSchedules)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.callButlerDataset)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAddNewScriptSchedule);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnAddNewScriptSchedule
            // 
            this.btnAddNewScriptSchedule.AntiAliasText = false;
            resources.ApplyResources(this.btnAddNewScriptSchedule, "btnAddNewScriptSchedule");
            this.btnAddNewScriptSchedule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNewScriptSchedule.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAddNewScriptSchedule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewScriptSchedule.LinkImage = global::CallButler.Manager.Properties.Resources.date_time_16;
            this.btnAddNewScriptSchedule.Name = "btnAddNewScriptSchedule";
            this.btnAddNewScriptSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewScriptSchedule.UnderlineOnHover = true;
            this.btnAddNewScriptSchedule.Click += new System.EventHandler(this.btnAddNewScriptSchedule_Click);
            // 
            // dgScriptSchedule
            // 
            this.dgScriptSchedule.AllowUserToAddRows = false;
            this.dgScriptSchedule.AllowUserToDeleteRows = false;
            this.dgScriptSchedule.AllowUserToResizeRows = false;
            this.dgScriptSchedule.AutoGenerateColumns = false;
            this.dgScriptSchedule.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgScriptSchedule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgScriptSchedule.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgScriptSchedule.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgScriptSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgScriptSchedule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.customerIDDataGridViewTextBoxColumn,
            this.scriptScheduleIDDataGridViewTextBoxColumn,
            this.scriptLocationDataGridViewTextBoxColumn,
            this.priorityDataGridViewTextBoxColumn,
            this.hasHoursOfOperationDataGridViewCheckBoxColumn,
            this.hoursOfOperationDataGridViewTextBoxColumn,
            this.enabledDataGridViewCheckBoxColumn});
            this.dgScriptSchedule.DataSource = this.bsScriptSchedules;
            resources.ApplyResources(this.dgScriptSchedule, "dgScriptSchedule");
            this.dgScriptSchedule.Name = "dgScriptSchedule";
            this.dgScriptSchedule.ReadOnly = true;
            this.dgScriptSchedule.RowHeadersVisible = false;
            this.dgScriptSchedule.RowImage = global::CallButler.Manager.Properties.Resources.date_time_24;
            this.dgScriptSchedule.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgScriptSchedule.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(225)))), ((int)(((byte)(244)))));
            this.dgScriptSchedule.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.dgScriptSchedule.RowTemplate.Height = 32;
            this.dgScriptSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgScriptSchedule.ShowDeleteColumn = true;
            this.dgScriptSchedule.ShowEditColumn = true;
            this.dgScriptSchedule.EditDataRow += new System.EventHandler<CallButler.Manager.Controls.DataRowEventArgs>(this.dgScriptSchedule_EditDataRow);
            this.dgScriptSchedule.DeleteDataRow += new System.EventHandler<CallButler.Manager.Controls.DataRowEventArgs>(this.dgScriptSchedule_DeleteDataRow);
            this.dgScriptSchedule.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgScriptSchedule_CellFormatting);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            resources.ApplyResources(this.nameDataGridViewTextBoxColumn, "nameDataGridViewTextBoxColumn");
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // customerIDDataGridViewTextBoxColumn
            // 
            this.customerIDDataGridViewTextBoxColumn.DataPropertyName = "CustomerID";
            resources.ApplyResources(this.customerIDDataGridViewTextBoxColumn, "customerIDDataGridViewTextBoxColumn");
            this.customerIDDataGridViewTextBoxColumn.Name = "customerIDDataGridViewTextBoxColumn";
            this.customerIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // scriptScheduleIDDataGridViewTextBoxColumn
            // 
            this.scriptScheduleIDDataGridViewTextBoxColumn.DataPropertyName = "ScriptScheduleID";
            resources.ApplyResources(this.scriptScheduleIDDataGridViewTextBoxColumn, "scriptScheduleIDDataGridViewTextBoxColumn");
            this.scriptScheduleIDDataGridViewTextBoxColumn.Name = "scriptScheduleIDDataGridViewTextBoxColumn";
            this.scriptScheduleIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // scriptLocationDataGridViewTextBoxColumn
            // 
            this.scriptLocationDataGridViewTextBoxColumn.DataPropertyName = "ScriptLocation";
            resources.ApplyResources(this.scriptLocationDataGridViewTextBoxColumn, "scriptLocationDataGridViewTextBoxColumn");
            this.scriptLocationDataGridViewTextBoxColumn.Name = "scriptLocationDataGridViewTextBoxColumn";
            this.scriptLocationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // priorityDataGridViewTextBoxColumn
            // 
            this.priorityDataGridViewTextBoxColumn.DataPropertyName = "Priority";
            resources.ApplyResources(this.priorityDataGridViewTextBoxColumn, "priorityDataGridViewTextBoxColumn");
            this.priorityDataGridViewTextBoxColumn.Name = "priorityDataGridViewTextBoxColumn";
            this.priorityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // hasHoursOfOperationDataGridViewCheckBoxColumn
            // 
            this.hasHoursOfOperationDataGridViewCheckBoxColumn.DataPropertyName = "HasHoursOfOperation";
            resources.ApplyResources(this.hasHoursOfOperationDataGridViewCheckBoxColumn, "hasHoursOfOperationDataGridViewCheckBoxColumn");
            this.hasHoursOfOperationDataGridViewCheckBoxColumn.Name = "hasHoursOfOperationDataGridViewCheckBoxColumn";
            this.hasHoursOfOperationDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // hoursOfOperationDataGridViewTextBoxColumn
            // 
            this.hoursOfOperationDataGridViewTextBoxColumn.DataPropertyName = "HoursOfOperation";
            resources.ApplyResources(this.hoursOfOperationDataGridViewTextBoxColumn, "hoursOfOperationDataGridViewTextBoxColumn");
            this.hoursOfOperationDataGridViewTextBoxColumn.Name = "hoursOfOperationDataGridViewTextBoxColumn";
            this.hoursOfOperationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // enabledDataGridViewCheckBoxColumn
            // 
            this.enabledDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
            resources.ApplyResources(this.enabledDataGridViewCheckBoxColumn, "enabledDataGridViewCheckBoxColumn");
            this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
            this.enabledDataGridViewCheckBoxColumn.ReadOnly = true;
            this.enabledDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.enabledDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bsScriptSchedules
            // 
            this.bsScriptSchedules.DataMember = "ScriptSchedules";
            this.bsScriptSchedules.DataSource = this.callButlerDataset;
            // 
            // callButlerDataset
            // 
            this.callButlerDataset.DataSetName = "CallButlerDataset";
            this.callButlerDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.ExcludeSchema;
            // 
            // ScriptScheduleView
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.dgScriptSchedule);
            this.Controls.Add(this.panel1);
            this.HeaderIcon = global::CallButler.Manager.Properties.Resources.date_time_48_shadow;
            this.Name = "ScriptScheduleView";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.dgScriptSchedule, 0);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgScriptSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsScriptSchedules)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.callButlerDataset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private global::Controls.LinkButton btnAddNewScriptSchedule;
        private CallButler.Manager.Controls.CallButlerEditDataGrid dgScriptSchedule;
        private WOSI.CallButler.Data.CallButlerDataset callButlerDataset;
        private System.Windows.Forms.BindingSource bsScriptSchedules;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scriptScheduleIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scriptLocationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priorityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasHoursOfOperationDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hoursOfOperationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn enabledDataGridViewCheckBoxColumn;
    }
}
