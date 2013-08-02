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
    partial class PBXView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PBXView));
            this.wizard1 = new global::Controls.Wizard.Wizard();
            this.pgSummry = new global::Controls.Wizard.WizardPage();
            this.dgPhones = new System.Windows.Forms.DataGridView();
            this.colPhoneImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colExtensionNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConfigure = new System.Windows.Forms.DataGridViewLinkColumn();
            this.header6 = new global::Controls.Wizard.Header();
            this.pgSettings = new global::Controls.Wizard.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.numRegister = new System.Windows.Forms.NumericUpDown();
            this.smoothLabel15 = new global::Controls.SmoothLabel();
            this.header1 = new global::Controls.Wizard.Header();
            this.wizardPage1 = new global::Controls.Wizard.WizardPage();
            this.txtDialPrefix = new System.Windows.Forms.TextBox();
            this.smoothLabel1 = new global::Controls.SmoothLabel();
            this.header2 = new global::Controls.Wizard.Header();
            this.pgSecurity = new global::Controls.Wizard.WizardPage();
            this.txtRegDomain = new System.Windows.Forms.TextBox();
            this.smoothLabel2 = new global::Controls.SmoothLabel();
            this.header3 = new global::Controls.Wizard.Header();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnRefresh = new global::Controls.LinkButton();
            this.wizard1.SuspendLayout();
            this.pgSummry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPhones)).BeginInit();
            this.pgSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRegister)).BeginInit();
            this.wizardPage1.SuspendLayout();
            this.pgSecurity.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard1
            // 
            this.wizard1.AlwaysShowFinishButton = false;
            this.wizard1.CloseOnCancel = true;
            this.wizard1.CloseOnFinish = true;
            this.wizard1.Controls.Add(this.pgSummry);
            this.wizard1.Controls.Add(this.pgSettings);
            this.wizard1.Controls.Add(this.wizardPage1);
            this.wizard1.Controls.Add(this.pgSecurity);
            this.wizard1.DisplayButtons = false;
            this.wizard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizard1.Location = new System.Drawing.Point(0, 22);
            this.wizard1.Name = "wizard1";
            this.wizard1.PageIndex = 0;
            this.wizard1.Pages.AddRange(new global::Controls.Wizard.WizardPage[] {
            this.pgSummry,
            this.pgSettings,
            this.wizardPage1,
            this.pgSecurity});
            this.wizard1.ShowTabs = true;
            this.wizard1.Size = new System.Drawing.Size(627, 265);
            this.wizard1.TabBackColor = System.Drawing.Color.WhiteSmoke;
            this.wizard1.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.wizard1.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.SingleLine;
            this.wizard1.TabIndex = 2;
            this.wizard1.TablPanelTopMargin = 0;
            this.wizard1.TabPanelWidth = 120;
            this.wizard1.TabWidth = 120;
            // 
            // pgSummry
            // 
            this.pgSummry.Controls.Add(this.dgPhones);
            this.pgSummry.Controls.Add(this.btnRefresh);
            this.pgSummry.Controls.Add(this.header6);
            this.pgSummry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgSummry.Icon = global::CallButler.Manager.Properties.Resources.about_16;
            this.pgSummry.IsFinishPage = false;
            this.pgSummry.Location = new System.Drawing.Point(120, 0);
            this.pgSummry.Name = "pgSummry";
            this.pgSummry.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.pgSummry.Size = new System.Drawing.Size(507, 217);
            this.pgSummry.TabIndex = 2;
            this.pgSummry.TabLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.pgSummry.Text = "Status";
            // 
            // dgPhones
            // 
            this.dgPhones.AllowUserToAddRows = false;
            this.dgPhones.AllowUserToDeleteRows = false;
            this.dgPhones.AllowUserToResizeRows = false;
            this.dgPhones.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgPhones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgPhones.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgPhones.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPhones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPhones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPhones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPhoneImage,
            this.colExtensionNumber,
            this.colName,
            this.colAddress,
            this.colStatus,
            this.colConfigure});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(225)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPhones.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgPhones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPhones.Location = new System.Drawing.Point(10, 47);
            this.dgPhones.MultiSelect = false;
            this.dgPhones.Name = "dgPhones";
            this.dgPhones.ReadOnly = true;
            this.dgPhones.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgPhones.RowHeadersVisible = false;
            this.dgPhones.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgPhones.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(225)))), ((int)(((byte)(244)))));
            this.dgPhones.RowTemplate.Height = 32;
            this.dgPhones.RowTemplate.ReadOnly = true;
            this.dgPhones.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPhones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPhones.ShowEditingIcon = false;
            this.dgPhones.Size = new System.Drawing.Size(497, 148);
            this.dgPhones.TabIndex = 5;
            this.dgPhones.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPhones_CellDoubleClick);
            this.dgPhones.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPhones_CellContentClick);
            // 
            // colPhoneImage
            // 
            this.colPhoneImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colPhoneImage.HeaderText = "";
            this.colPhoneImage.Image = global::CallButler.Manager.Properties.Resources.telephone_24;
            this.colPhoneImage.Name = "colPhoneImage";
            this.colPhoneImage.ReadOnly = true;
            this.colPhoneImage.Width = 5;
            // 
            // colExtensionNumber
            // 
            this.colExtensionNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colExtensionNumber.HeaderText = "Extension";
            this.colExtensionNumber.Name = "colExtensionNumber";
            this.colExtensionNumber.ReadOnly = true;
            this.colExtensionNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colExtensionNumber.Width = 79;
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colAddress
            // 
            this.colAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colAddress.HeaderText = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.ReadOnly = true;
            this.colAddress.Width = 71;
            // 
            // colStatus
            // 
            this.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 63;
            // 
            // colConfigure
            // 
            this.colConfigure.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.colConfigure.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colConfigure.HeaderText = "";
            this.colConfigure.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.colConfigure.LinkColor = System.Drawing.Color.RoyalBlue;
            this.colConfigure.Name = "colConfigure";
            this.colConfigure.ReadOnly = true;
            this.colConfigure.Text = "";
            this.colConfigure.ToolTipText = "Configure the selected phone";
            this.colConfigure.TrackVisitedState = false;
            this.colConfigure.VisitedLinkColor = System.Drawing.Color.RoyalBlue;
            this.colConfigure.Width = 5;
            // 
            // header6
            // 
            this.header6.CausesValidation = false;
            this.header6.Description = "View the status of IP phones connected to the PBX.";
            this.header6.Dock = System.Windows.Forms.DockStyle.Top;
            this.header6.Image = global::CallButler.Manager.Properties.Resources.about_32;
            this.header6.Location = new System.Drawing.Point(10, 0);
            this.header6.Name = "header6";
            this.header6.Size = new System.Drawing.Size(497, 47);
            this.header6.TabIndex = 4;
            this.header6.Title = "Extension IP Phone Status";
            // 
            // pgSettings
            // 
            this.pgSettings.Controls.Add(this.label1);
            this.pgSettings.Controls.Add(this.numRegister);
            this.pgSettings.Controls.Add(this.smoothLabel15);
            this.pgSettings.Controls.Add(this.header1);
            this.pgSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgSettings.Icon = global::CallButler.Manager.Properties.Resources.nut_and_bolt_16;
            this.pgSettings.IsFinishPage = false;
            this.pgSettings.Location = new System.Drawing.Point(120, 0);
            this.pgSettings.Name = "pgSettings";
            this.pgSettings.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.pgSettings.Size = new System.Drawing.Size(507, 217);
            this.pgSettings.TabIndex = 3;
            this.pgSettings.TabLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.pgSettings.Text = "General Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(256, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "seconds.";
            // 
            // numRegister
            // 
            this.numRegister.Location = new System.Drawing.Point(197, 68);
            this.numRegister.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numRegister.Name = "numRegister";
            this.numRegister.Size = new System.Drawing.Size(57, 21);
            this.numRegister.TabIndex = 37;
            this.numRegister.ValueChanged += new System.EventHandler(this.SettingChanged);
            // 
            // smoothLabel15
            // 
            this.smoothLabel15.AntiAliasText = false;
            this.smoothLabel15.AutoSize = true;
            this.smoothLabel15.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel15.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel15.EnableHelp = true;
            this.smoothLabel15.HelpText = "This setting determines how long CallButler will wait to hear from an IP phone be" +
                "fore it determines that it is offline.";
            this.smoothLabel15.HelpTitle = "IP Phone Registration";
            this.smoothLabel15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.smoothLabel15.Location = new System.Drawing.Point(18, 70);
            this.smoothLabel15.Name = "smoothLabel15";
            this.smoothLabel15.Size = new System.Drawing.Size(192, 13);
            this.smoothLabel15.TabIndex = 36;
            this.smoothLabel15.Text = "Require IP phones to register every";
            // 
            // header1
            // 
            this.header1.CausesValidation = false;
            this.header1.Description = "General PBX settings.";
            this.header1.Dock = System.Windows.Forms.DockStyle.Top;
            this.header1.Image = global::CallButler.Manager.Properties.Resources.nut_and_bolt_48_shadow;
            this.header1.Location = new System.Drawing.Point(10, 0);
            this.header1.Name = "header1";
            this.header1.Size = new System.Drawing.Size(497, 47);
            this.header1.TabIndex = 5;
            this.header1.Title = "General PBX Settings";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.txtDialPrefix);
            this.wizardPage1.Controls.Add(this.smoothLabel1);
            this.wizardPage1.Controls.Add(this.header2);
            this.wizardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage1.Icon = global::CallButler.Manager.Properties.Resources.telephone_16;
            this.wizardPage1.IsFinishPage = false;
            this.wizardPage1.Location = new System.Drawing.Point(120, 0);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.wizardPage1.Size = new System.Drawing.Size(507, 217);
            this.wizardPage1.TabIndex = 4;
            this.wizardPage1.TabLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.wizardPage1.Text = "Dial Settings";
            // 
            // txtDialPrefix
            // 
            this.txtDialPrefix.Location = new System.Drawing.Point(239, 66);
            this.txtDialPrefix.Name = "txtDialPrefix";
            this.txtDialPrefix.Size = new System.Drawing.Size(57, 21);
            this.txtDialPrefix.TabIndex = 38;
            this.txtDialPrefix.TextChanged += new System.EventHandler(this.SettingChanged);
            this.txtDialPrefix.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialPrefix_KeyPress);
            // 
            // smoothLabel1
            // 
            this.smoothLabel1.AntiAliasText = false;
            this.smoothLabel1.AutoSize = true;
            this.smoothLabel1.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel1.EnableHelp = true;
            this.smoothLabel1.HelpText = resources.GetString("smoothLabel1.HelpText");
            this.smoothLabel1.HelpTitle = "Outbound Call Prefix";
            this.smoothLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.smoothLabel1.Location = new System.Drawing.Point(21, 69);
            this.smoothLabel1.Name = "smoothLabel1";
            this.smoothLabel1.Size = new System.Drawing.Size(231, 13);
            this.smoothLabel1.TabIndex = 37;
            this.smoothLabel1.Text = "All outbound numbers must be prefixed with";
            // 
            // header2
            // 
            this.header2.CausesValidation = false;
            this.header2.Description = "Settings for phone dialing.";
            this.header2.Dock = System.Windows.Forms.DockStyle.Top;
            this.header2.Image = global::CallButler.Manager.Properties.Resources.telephone_48_shadow;
            this.header2.Location = new System.Drawing.Point(10, 0);
            this.header2.Name = "header2";
            this.header2.Size = new System.Drawing.Size(497, 47);
            this.header2.TabIndex = 6;
            this.header2.Title = "Dial Settings";
            // 
            // pgSecurity
            // 
            this.pgSecurity.Controls.Add(this.txtRegDomain);
            this.pgSecurity.Controls.Add(this.smoothLabel2);
            this.pgSecurity.Controls.Add(this.header3);
            this.pgSecurity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgSecurity.Enabled = false;
            this.pgSecurity.Icon = global::CallButler.Manager.Properties.Resources.lock_16;
            this.pgSecurity.IsFinishPage = false;
            this.pgSecurity.Location = new System.Drawing.Point(120, 0);
            this.pgSecurity.Name = "pgSecurity";
            this.pgSecurity.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.pgSecurity.Size = new System.Drawing.Size(507, 217);
            this.pgSecurity.TabIndex = 5;
            this.pgSecurity.TabLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.pgSecurity.Text = "Security Settings";
            // 
            // txtRegDomain
            // 
            this.txtRegDomain.Location = new System.Drawing.Point(21, 82);
            this.txtRegDomain.Name = "txtRegDomain";
            this.txtRegDomain.Size = new System.Drawing.Size(295, 21);
            this.txtRegDomain.TabIndex = 39;
            this.txtRegDomain.TextChanged += new System.EventHandler(this.SettingChanged);
            // 
            // smoothLabel2
            // 
            this.smoothLabel2.AntiAliasText = false;
            this.smoothLabel2.AutoSize = true;
            this.smoothLabel2.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel2.EnableHelp = true;
            this.smoothLabel2.HelpText = "This setting requires that IP telephones registering with this PBX use the value " +
                "specified here when they authenticate their registration.";
            this.smoothLabel2.HelpTitle = "SIP Registration Domain";
            this.smoothLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.smoothLabel2.Location = new System.Drawing.Point(18, 66);
            this.smoothLabel2.Name = "smoothLabel2";
            this.smoothLabel2.Size = new System.Drawing.Size(131, 13);
            this.smoothLabel2.TabIndex = 38;
            this.smoothLabel2.Text = "SIP registration domain";
            // 
            // header3
            // 
            this.header3.CausesValidation = false;
            this.header3.Description = "PBX security settings.";
            this.header3.Dock = System.Windows.Forms.DockStyle.Top;
            this.header3.Image = global::CallButler.Manager.Properties.Resources.lock_32_shadow;
            this.header3.Location = new System.Drawing.Point(10, 0);
            this.header3.Name = "header3";
            this.header3.Size = new System.Drawing.Size(497, 47);
            this.header3.TabIndex = 7;
            this.header3.Title = "Security Settings";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 287);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(627, 26);
            this.panel1.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(549, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Enabled = false;
            this.btnApply.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnApply.Location = new System.Drawing.Point(468, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.AntiAliasText = false;
            this.btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnRefresh.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.LinkImage = global::CallButler.Manager.Properties.Resources.refresh_16;
            this.btnRefresh.Location = new System.Drawing.Point(10, 195);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(497, 22);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.UnderlineOnHover = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // PBXView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.wizard1);
            this.Controls.Add(this.panel1);
            this.HeaderCaption = "Configure CallButler for use in an office environment";
            this.HeaderIcon = global::CallButler.Manager.Properties.Resources.node_48_shadow;
            this.HeaderTitle = "PBX";
            this.Name = "PBXView";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.wizard1, 0);
            this.wizard1.ResumeLayout(false);
            this.pgSummry.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPhones)).EndInit();
            this.pgSettings.ResumeLayout(false);
            this.pgSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRegister)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.pgSecurity.ResumeLayout(false);
            this.pgSecurity.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private global::Controls.Wizard.Wizard wizard1;
        private global::Controls.Wizard.WizardPage pgSummry;
        private global::Controls.Wizard.WizardPage pgSettings;
        private global::Controls.Wizard.Header header6;
        private global::Controls.Wizard.Header header1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numRegister;
        private global::Controls.SmoothLabel smoothLabel15;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private global::Controls.Wizard.WizardPage wizardPage1;
        private global::Controls.Wizard.Header header2;
        private global::Controls.SmoothLabel smoothLabel1;
        private System.Windows.Forms.TextBox txtDialPrefix;
        private global::Controls.Wizard.WizardPage pgSecurity;
        private global::Controls.Wizard.Header header3;
        private global::Controls.SmoothLabel smoothLabel2;
        private System.Windows.Forms.TextBox txtRegDomain;
        private System.Windows.Forms.DataGridView dgPhones;
        private System.Windows.Forms.DataGridViewImageColumn colPhoneImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExtensionNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewLinkColumn colConfigure;
        private global::Controls.LinkButton btnRefresh;
    }
}
