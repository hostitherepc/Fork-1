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
    partial class PersonalizedGreetingView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonalizedGreetingView));
            this.callButlerDataset = new WOSI.CallButler.Data.CallButlerDataset();
            this.bsPersonalizedGreetings = new System.Windows.Forms.BindingSource(this.components);
            this.dgPersonalizedGreetings = new CallButler.Manager.Controls.CallButlerEditDataGrid();
            this.customerIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.personalizedGreetingIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callerDisplayNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callerUsernameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callerHostDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playOnceDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddPersonalizedGreeting = new global::Controls.LinkButton();
            ((System.ComponentModel.ISupportInitialize)(this.callButlerDataset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPersonalizedGreetings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPersonalizedGreetings)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // callButlerDataset
            // 
            this.callButlerDataset.DataSetName = "CallButlerDataset";
            this.callButlerDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bsPersonalizedGreetings
            // 
            this.bsPersonalizedGreetings.DataMember = "PersonalizedGreetings";
            this.bsPersonalizedGreetings.DataSource = this.callButlerDataset;
            // 
            // dgPersonalizedGreetings
            // 
            this.dgPersonalizedGreetings.AllowUserToAddRows = false;
            this.dgPersonalizedGreetings.AllowUserToDeleteRows = false;
            this.dgPersonalizedGreetings.AllowUserToResizeRows = false;
            this.dgPersonalizedGreetings.AutoGenerateColumns = false;
            this.dgPersonalizedGreetings.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgPersonalizedGreetings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgPersonalizedGreetings.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgPersonalizedGreetings.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgPersonalizedGreetings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPersonalizedGreetings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customerIDDataGridViewTextBoxColumn,
            this.personalizedGreetingIDDataGridViewTextBoxColumn,
            this.callerDisplayNameDataGridViewTextBoxColumn,
            this.callerUsernameDataGridViewTextBoxColumn,
            this.callerHostDataGridViewTextBoxColumn,
            this.typeDataGridViewTextBoxColumn,
            this.playOnceDataGridViewCheckBoxColumn,
            this.Notes});
            this.dgPersonalizedGreetings.DataSource = this.bsPersonalizedGreetings;
            resources.ApplyResources(this.dgPersonalizedGreetings, "dgPersonalizedGreetings");
            this.dgPersonalizedGreetings.Name = "dgPersonalizedGreetings";
            this.dgPersonalizedGreetings.ReadOnly = true;
            this.dgPersonalizedGreetings.RowHeadersVisible = false;
            this.dgPersonalizedGreetings.RowImage = global::CallButler.Manager.Properties.Resources.toolbox_24;
            this.dgPersonalizedGreetings.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgPersonalizedGreetings.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(225)))), ((int)(((byte)(244)))));
            this.dgPersonalizedGreetings.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.dgPersonalizedGreetings.RowTemplate.Height = 32;
            this.dgPersonalizedGreetings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPersonalizedGreetings.ShowDeleteColumn = true;
            this.dgPersonalizedGreetings.ShowEditColumn = true;
            this.dgPersonalizedGreetings.EditDataRow += new System.EventHandler<CallButler.Manager.Controls.DataRowEventArgs>(this.dgPersonalizedGreetings_EditDataRow);
            this.dgPersonalizedGreetings.DeleteDataRow += new System.EventHandler<CallButler.Manager.Controls.DataRowEventArgs>(this.dgPersonalizedGreetings_DeleteDataRow);
            // 
            // customerIDDataGridViewTextBoxColumn
            // 
            this.customerIDDataGridViewTextBoxColumn.DataPropertyName = "CustomerID";
            resources.ApplyResources(this.customerIDDataGridViewTextBoxColumn, "customerIDDataGridViewTextBoxColumn");
            this.customerIDDataGridViewTextBoxColumn.Name = "customerIDDataGridViewTextBoxColumn";
            this.customerIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // personalizedGreetingIDDataGridViewTextBoxColumn
            // 
            this.personalizedGreetingIDDataGridViewTextBoxColumn.DataPropertyName = "PersonalizedGreetingID";
            resources.ApplyResources(this.personalizedGreetingIDDataGridViewTextBoxColumn, "personalizedGreetingIDDataGridViewTextBoxColumn");
            this.personalizedGreetingIDDataGridViewTextBoxColumn.Name = "personalizedGreetingIDDataGridViewTextBoxColumn";
            this.personalizedGreetingIDDataGridViewTextBoxColumn.ReadOnly = true;
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
            // callerHostDataGridViewTextBoxColumn
            // 
            this.callerHostDataGridViewTextBoxColumn.DataPropertyName = "CallerHost";
            resources.ApplyResources(this.callerHostDataGridViewTextBoxColumn, "callerHostDataGridViewTextBoxColumn");
            this.callerHostDataGridViewTextBoxColumn.Name = "callerHostDataGridViewTextBoxColumn";
            this.callerHostDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            resources.ApplyResources(this.typeDataGridViewTextBoxColumn, "typeDataGridViewTextBoxColumn");
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            this.typeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // playOnceDataGridViewCheckBoxColumn
            // 
            this.playOnceDataGridViewCheckBoxColumn.DataPropertyName = "PlayOnce";
            resources.ApplyResources(this.playOnceDataGridViewCheckBoxColumn, "playOnceDataGridViewCheckBoxColumn");
            this.playOnceDataGridViewCheckBoxColumn.Name = "playOnceDataGridViewCheckBoxColumn";
            this.playOnceDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // Notes
            // 
            this.Notes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Notes.DataPropertyName = "Notes";
            resources.ApplyResources(this.Notes, "Notes");
            this.Notes.Name = "Notes";
            this.Notes.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAddPersonalizedGreeting);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnAddPersonalizedGreeting
            // 
            this.btnAddPersonalizedGreeting.AntiAliasText = false;
            resources.ApplyResources(this.btnAddPersonalizedGreeting, "btnAddPersonalizedGreeting");
            this.btnAddPersonalizedGreeting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddPersonalizedGreeting.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAddPersonalizedGreeting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddPersonalizedGreeting.LinkImage = global::CallButler.Manager.Properties.Resources.toolbox_16;
            this.btnAddPersonalizedGreeting.Name = "btnAddPersonalizedGreeting";
            this.btnAddPersonalizedGreeting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddPersonalizedGreeting.UnderlineOnHover = true;
            this.btnAddPersonalizedGreeting.Click += new System.EventHandler(this.btnAddPersonalizedGreeting_Click);
            // 
            // PersonalizedGreetingView
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.dgPersonalizedGreetings);
            this.Controls.Add(this.panel1);
            this.HeaderIcon = global::CallButler.Manager.Properties.Resources.toolbox_48_shadow;
            this.Name = "PersonalizedGreetingView";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.dgPersonalizedGreetings, 0);
            ((System.ComponentModel.ISupportInitialize)(this.callButlerDataset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPersonalizedGreetings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPersonalizedGreetings)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WOSI.CallButler.Data.CallButlerDataset callButlerDataset;
        private System.Windows.Forms.BindingSource bsPersonalizedGreetings;
        private CallButler.Manager.Controls.CallButlerEditDataGrid dgPersonalizedGreetings;
        private System.Windows.Forms.Panel panel1;
        private global::Controls.LinkButton btnAddPersonalizedGreeting;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn personalizedGreetingIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callerDisplayNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callerUsernameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callerHostDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn customScriptLocationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn playOnceDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
    }
}
