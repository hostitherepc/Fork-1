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
    partial class FindMeNumberDiagramShape
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindMeNumberDiagramShape));
            this.roundedCornerPanel1 = new global::Controls.RoundedCornerPanel();
            this.btnDelete = new global::Controls.LinkButton();
            this.btnMoveDown = new global::Controls.LinkButton();
            this.btnMoveUp = new global::Controls.LinkButton();
            this.btnEditSettings = new global::Controls.LinkButton();
            this.lblTimeout = new global::Controls.SmoothLabel();
            this.lblFor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNumber = new global::Controls.SmoothLabel();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.cboExtension = new System.Windows.Forms.ComboBox();
            this.pnlTryFor = new System.Windows.Forms.Panel();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.smoothLabel2 = new global::Controls.SmoothLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.lblHours = new global::Controls.SmoothLabel();
            this.cbExtensionHours = new System.Windows.Forms.CheckBox();
            this.txtContactNumber = new System.Windows.Forms.TextBox();
            this.pnlHours = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.cboTimeZone = new System.Windows.Forms.ComboBox();
            this.btnSelectAll = new global::Controls.LinkButton();
            this.btnSelectNone = new global::Controls.LinkButton();
            this.btnSelectInverse = new global::Controls.LinkButton();
            this.dividerLine1 = new global::Controls.DividerLine();
            this.arrowLine1 = new global::Controls.ArrowLine();
            this.lblNext = new System.Windows.Forms.Label();
            this.smoothLabel1 = new global::Controls.SmoothLabel();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.scheduleControl = new CallButler.Manager.Controls.ScheduleControl();
            this.roundedCornerPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.pnlDetails.SuspendLayout();
            this.pnlTryFor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            this.pnlHours.SuspendLayout();
            this.SuspendLayout();
            // 
            // roundedCornerPanel1
            // 
            this.roundedCornerPanel1.BorderSize = 1F;
            this.roundedCornerPanel1.Controls.Add(this.dividerLine1);
            this.roundedCornerPanel1.Controls.Add(this.btnDelete);
            this.roundedCornerPanel1.Controls.Add(this.btnMoveDown);
            this.roundedCornerPanel1.Controls.Add(this.btnMoveUp);
            this.roundedCornerPanel1.Controls.Add(this.btnEditSettings);
            this.roundedCornerPanel1.Controls.Add(this.lblTimeout);
            this.roundedCornerPanel1.Controls.Add(this.lblFor);
            this.roundedCornerPanel1.Controls.Add(this.label1);
            this.roundedCornerPanel1.Controls.Add(this.lblNumber);
            this.roundedCornerPanel1.Controls.Add(this.pbIcon);
            this.roundedCornerPanel1.Controls.Add(this.pnlDetails);
            this.roundedCornerPanel1.CornerRadius = 10;
            this.roundedCornerPanel1.DisplayShadow = true;
            this.roundedCornerPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundedCornerPanel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundedCornerPanel1.ForeColor = System.Drawing.Color.Gray;
            this.roundedCornerPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundedCornerPanel1.Name = "roundedCornerPanel1";
            this.roundedCornerPanel1.Padding = new System.Windows.Forms.Padding(5, 5, 6, 6);
            this.roundedCornerPanel1.PanelColor = System.Drawing.Color.White;
            this.roundedCornerPanel1.ShadowColor = System.Drawing.Color.LightGray;
            this.roundedCornerPanel1.ShadowOffset = 3;
            this.roundedCornerPanel1.Size = new System.Drawing.Size(459, 350);
            this.roundedCornerPanel1.TabIndex = 2;
            this.roundedCornerPanel1.Text = "roundedCornerPanel1";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.AntiAliasText = false;
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.LinkImage = global::CallButler.Manager.Controls.Properties.Resources.delete_16;
            this.btnDelete.Location = new System.Drawing.Point(393, 318);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(58, 17);
            this.btnDelete.TabIndex = 33;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.UnderlineOnHover = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDown.AntiAliasText = false;
            this.btnMoveDown.BackColor = System.Drawing.Color.Transparent;
            this.btnMoveDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMoveDown.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveDown.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnMoveDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMoveDown.LinkImage = global::CallButler.Manager.Controls.Properties.Resources.arrow_down_blue_16;
            this.btnMoveDown.Location = new System.Drawing.Point(431, 29);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(18, 17);
            this.btnMoveDown.TabIndex = 32;
            this.btnMoveDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMoveDown.UnderlineOnHover = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUp.AntiAliasText = false;
            this.btnMoveUp.BackColor = System.Drawing.Color.Transparent;
            this.btnMoveUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMoveUp.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveUp.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnMoveUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMoveUp.LinkImage = global::CallButler.Manager.Controls.Properties.Resources.arrow_up_blue_16;
            this.btnMoveUp.Location = new System.Drawing.Point(431, 11);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(18, 17);
            this.btnMoveUp.TabIndex = 31;
            this.btnMoveUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMoveUp.UnderlineOnHover = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnEditSettings
            // 
            this.btnEditSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditSettings.AntiAliasText = false;
            this.btnEditSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnEditSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditSettings.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnEditSettings.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnEditSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditSettings.LinkImage = global::CallButler.Manager.Controls.Properties.Resources.navigate_right_16;
            this.btnEditSettings.Location = new System.Drawing.Point(13, 312);
            this.btnEditSettings.Name = "btnEditSettings";
            this.btnEditSettings.Size = new System.Drawing.Size(118, 29);
            this.btnEditSettings.TabIndex = 30;
            this.btnEditSettings.Text = "Change Settings...";
            this.btnEditSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditSettings.UnderlineOnHover = true;
            this.btnEditSettings.Click += new System.EventHandler(this.btnEditSettings_Click);
            // 
            // lblTimeout
            // 
            this.lblTimeout.BackColor = System.Drawing.Color.Transparent;
            this.lblTimeout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTimeout.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeout.ForeColor = System.Drawing.Color.YellowGreen;
            this.lblTimeout.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTimeout.Location = new System.Drawing.Point(79, 48);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(109, 24);
            this.lblTimeout.TabIndex = 29;
            this.lblTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFor
            // 
            this.lblFor.AutoSize = true;
            this.lblFor.BackColor = System.Drawing.Color.Transparent;
            this.lblFor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lblFor.Location = new System.Drawing.Point(55, 53);
            this.lblFor.Name = "lblFor";
            this.lblFor.Size = new System.Drawing.Size(21, 13);
            this.lblFor.TabIndex = 28;
            this.lblFor.Text = "for";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.label1.Location = new System.Drawing.Point(34, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Try calling me at";
            // 
            // lblNumber
            // 
            this.lblNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNumber.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumber.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblNumber.Location = new System.Drawing.Point(64, 29);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(358, 24);
            this.lblNumber.TabIndex = 25;
            this.lblNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbIcon
            // 
            this.pbIcon.BackColor = System.Drawing.Color.Transparent;
            this.pbIcon.Image = global::CallButler.Manager.Controls.Properties.Resources.telephone_24;
            this.pbIcon.Location = new System.Drawing.Point(8, 8);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(24, 24);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbIcon.TabIndex = 1;
            this.pbIcon.TabStop = false;
            // 
            // pnlDetails
            // 
            this.pnlDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDetails.BackColor = System.Drawing.Color.Transparent;
            this.pnlDetails.Controls.Add(this.cboType);
            this.pnlDetails.Controls.Add(this.smoothLabel1);
            this.pnlDetails.Controls.Add(this.cboExtension);
            this.pnlDetails.Controls.Add(this.pnlTryFor);
            this.pnlDetails.Controls.Add(this.lblHours);
            this.pnlDetails.Controls.Add(this.cbExtensionHours);
            this.pnlDetails.Controls.Add(this.txtContactNumber);
            this.pnlDetails.Controls.Add(this.pnlHours);
            this.pnlDetails.Location = new System.Drawing.Point(8, 78);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(441, 228);
            this.pnlDetails.TabIndex = 34;
            this.pnlDetails.Visible = false;
            // 
            // cboExtension
            // 
            this.cboExtension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExtension.FormattingEnabled = true;
            this.cboExtension.Location = new System.Drawing.Point(26, 35);
            this.cboExtension.Name = "cboExtension";
            this.cboExtension.Size = new System.Drawing.Size(173, 21);
            this.cboExtension.TabIndex = 36;
            this.cboExtension.Visible = false;
            this.cboExtension.SelectedIndexChanged += new System.EventHandler(this.cboExtension_SelectedIndexChanged);
            // 
            // pnlTryFor
            // 
            this.pnlTryFor.Controls.Add(this.numTimeout);
            this.pnlTryFor.Controls.Add(this.smoothLabel2);
            this.pnlTryFor.Controls.Add(this.label9);
            this.pnlTryFor.Location = new System.Drawing.Point(205, 23);
            this.pnlTryFor.Name = "pnlTryFor";
            this.pnlTryFor.Size = new System.Drawing.Size(226, 41);
            this.pnlTryFor.TabIndex = 32;
            // 
            // numTimeout
            // 
            this.numTimeout.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.numTimeout.Location = new System.Drawing.Point(104, 13);
            this.numTimeout.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.Size = new System.Drawing.Size(49, 21);
            this.numTimeout.TabIndex = 1;
            this.numTimeout.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTimeout.ValueChanged += new System.EventHandler(this.numTimeout_ValueChanged);
            // 
            // smoothLabel2
            // 
            this.smoothLabel2.AntiAliasText = false;
            this.smoothLabel2.AutoSize = true;
            this.smoothLabel2.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel2.EnableHelp = true;
            this.smoothLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.smoothLabel2.HelpText = resources.GetString("smoothLabel2.HelpText");
            this.smoothLabel2.HelpTitle = "Find Me Number Timeout";
            this.smoothLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.smoothLabel2.Location = new System.Drawing.Point(3, 15);
            this.smoothLabel2.Name = "smoothLabel2";
            this.smoothLabel2.Size = new System.Drawing.Size(112, 13);
            this.smoothLabel2.TabIndex = 29;
            this.smoothLabel2.Text = "Try this number for";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(157, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "second(s)";
            // 
            // lblHours
            // 
            this.lblHours.AntiAliasText = false;
            this.lblHours.AutoSize = true;
            this.lblHours.BackColor = System.Drawing.Color.Transparent;
            this.lblHours.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHours.EnableHelp = true;
            this.lblHours.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lblHours.HelpText = resources.GetString("lblHours.HelpText");
            this.lblHours.HelpTitle = "Hours of Operation";
            this.lblHours.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblHours.Location = new System.Drawing.Point(43, 65);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(202, 13);
            this.lblHours.TabIndex = 30;
            this.lblHours.Text = "Limit to certain hours of the day/week";
            this.lblHours.Click += new System.EventHandler(this.lblHours_Click);
            // 
            // cbExtensionHours
            // 
            this.cbExtensionHours.AutoSize = true;
            this.cbExtensionHours.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cbExtensionHours.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbExtensionHours.Location = new System.Drawing.Point(26, 65);
            this.cbExtensionHours.Name = "cbExtensionHours";
            this.cbExtensionHours.Size = new System.Drawing.Size(15, 14);
            this.cbExtensionHours.TabIndex = 16;
            this.cbExtensionHours.UseVisualStyleBackColor = true;
            this.cbExtensionHours.CheckedChanged += new System.EventHandler(this.cbExtensionHours_CheckedChanged);
            // 
            // txtContactNumber
            // 
            this.txtContactNumber.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtContactNumber.Location = new System.Drawing.Point(26, 35);
            this.txtContactNumber.Name = "txtContactNumber";
            this.txtContactNumber.Size = new System.Drawing.Size(173, 21);
            this.txtContactNumber.TabIndex = 0;
            this.txtContactNumber.TextChanged += new System.EventHandler(this.txtContactNumber_TextChanged);
            // 
            // pnlHours
            // 
            this.pnlHours.Controls.Add(this.label12);
            this.pnlHours.Controls.Add(this.cboTimeZone);
            this.pnlHours.Controls.Add(this.btnSelectAll);
            this.pnlHours.Controls.Add(this.btnSelectNone);
            this.pnlHours.Controls.Add(this.scheduleControl);
            this.pnlHours.Controls.Add(this.btnSelectInverse);
            this.pnlHours.Location = new System.Drawing.Point(0, 83);
            this.pnlHours.Name = "pnlHours";
            this.pnlHours.Size = new System.Drawing.Size(434, 153);
            this.pnlHours.TabIndex = 31;
            this.pnlHours.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(3, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Time Zone";
            // 
            // cboTimeZone
            // 
            this.cboTimeZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeZone.FormattingEnabled = true;
            this.cboTimeZone.Location = new System.Drawing.Point(59, 7);
            this.cboTimeZone.Name = "cboTimeZone";
            this.cboTimeZone.Size = new System.Drawing.Size(300, 21);
            this.cboTimeZone.TabIndex = 25;
            this.cboTimeZone.SelectedIndexChanged += new System.EventHandler(this.cboTimeZone_SelectedIndexChanged);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.AntiAliasText = false;
            this.btnSelectAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectAll.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnSelectAll.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSelectAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectAll.LinkImage = global::CallButler.Manager.Controls.Properties.Resources.select_all_16;
            this.btnSelectAll.Location = new System.Drawing.Point(360, 47);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(54, 17);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.Text = "All";
            this.btnSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectAll.UnderlineOnHover = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.AntiAliasText = false;
            this.btnSelectNone.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectNone.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnSelectNone.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSelectNone.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectNone.LinkImage = global::CallButler.Manager.Controls.Properties.Resources.select_none_16;
            this.btnSelectNone.Location = new System.Drawing.Point(360, 70);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(54, 17);
            this.btnSelectNone.TabIndex = 6;
            this.btnSelectNone.Text = "None";
            this.btnSelectNone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectNone.UnderlineOnHover = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // btnSelectInverse
            // 
            this.btnSelectInverse.AntiAliasText = false;
            this.btnSelectInverse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectInverse.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnSelectInverse.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSelectInverse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectInverse.LinkImage = global::CallButler.Manager.Controls.Properties.Resources.select_inverse_16;
            this.btnSelectInverse.Location = new System.Drawing.Point(360, 93);
            this.btnSelectInverse.Name = "btnSelectInverse";
            this.btnSelectInverse.Size = new System.Drawing.Size(60, 17);
            this.btnSelectInverse.TabIndex = 7;
            this.btnSelectInverse.Text = "Inverse";
            this.btnSelectInverse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectInverse.UnderlineOnHover = true;
            this.btnSelectInverse.Click += new System.EventHandler(this.btnSelectInverse_Click);
            // 
            // dividerLine1
            // 
            this.dividerLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dividerLine1.BackColor = System.Drawing.Color.Transparent;
            this.dividerLine1.ForeColor = System.Drawing.Color.Silver;
            this.dividerLine1.GradientWidth = 30;
            this.dividerLine1.LineWidth = 1;
            this.dividerLine1.Location = new System.Drawing.Point(14, 71);
            this.dividerLine1.Name = "dividerLine1";
            this.dividerLine1.Size = new System.Drawing.Size(430, 8);
            this.dividerLine1.TabIndex = 27;
            this.dividerLine1.Vertical = false;
            // 
            // arrowLine1
            // 
            this.arrowLine1.ArrowAtEnd = true;
            this.arrowLine1.ArrowAtStart = false;
            this.arrowLine1.ArrowLengthPercentage = 5F;
            this.arrowLine1.ArrowWidthPercentage = 20F;
            this.arrowLine1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.arrowLine1.ForeColor = System.Drawing.Color.DarkGray;
            this.arrowLine1.LineOrientation = global::Controls.LineOrientation.Vertical;
            this.arrowLine1.LineWidth = 20F;
            this.arrowLine1.Location = new System.Drawing.Point(0, 350);
            this.arrowLine1.Name = "arrowLine1";
            this.arrowLine1.Size = new System.Drawing.Size(459, 30);
            this.arrowLine1.TabIndex = 3;
            this.arrowLine1.Text = "arrowLine1";
            // 
            // lblNext
            // 
            this.lblNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNext.AutoSize = true;
            this.lblNext.Location = new System.Drawing.Point(9, 358);
            this.lblNext.Name = "lblNext";
            this.lblNext.Size = new System.Drawing.Size(148, 13);
            this.lblNext.TabIndex = 4;
            this.lblNext.Text = "If there is no answer, then...";
            // 
            // smoothLabel1
            // 
            this.smoothLabel1.AntiAliasText = false;
            this.smoothLabel1.AutoSize = true;
            this.smoothLabel1.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.smoothLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.smoothLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.smoothLabel1.Location = new System.Drawing.Point(3, 11);
            this.smoothLabel1.Name = "smoothLabel1";
            this.smoothLabel1.Size = new System.Drawing.Size(24, 13);
            this.smoothLabel1.TabIndex = 37;
            this.smoothLabel1.Text = "Call";
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "a Telephone Number",
            "my IP Phone",
            "another Extension"});
            this.cboType.Location = new System.Drawing.Point(26, 7);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(173, 21);
            this.cboType.TabIndex = 38;
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            // 
            // scheduleControl
            // 
            this.scheduleControl.BackColor = System.Drawing.Color.White;
            this.scheduleControl.BorderColor = System.Drawing.Color.Silver;
            this.scheduleControl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scheduleControl.DrawOutsideBorder = true;
            this.scheduleControl.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.scheduleControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.scheduleControl.Location = new System.Drawing.Point(6, 33);
            this.scheduleControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.scheduleControl.Name = "scheduleControl";
            this.scheduleControl.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(225)))), ((int)(((byte)(244)))));
            this.scheduleControl.Size = new System.Drawing.Size(352, 102);
            this.scheduleControl.TabIndex = 2;
            this.scheduleControl.SelectionChanged += new System.EventHandler(this.scheduleControl_SelectionChanged);
            // 
            // FindMeNumberDiagramShape
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblNext);
            this.Controls.Add(this.roundedCornerPanel1);
            this.Controls.Add(this.arrowLine1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.Name = "FindMeNumberDiagramShape";
            this.Size = new System.Drawing.Size(459, 380);
            this.roundedCornerPanel1.ResumeLayout(false);
            this.roundedCornerPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            this.pnlTryFor.ResumeLayout(false);
            this.pnlTryFor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            this.pnlHours.ResumeLayout(false);
            this.pnlHours.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private global::Controls.RoundedCornerPanel roundedCornerPanel1;
        private System.Windows.Forms.PictureBox pbIcon;
        private global::Controls.SmoothLabel lblNumber;
        private System.Windows.Forms.Label label1;
        private global::Controls.DividerLine dividerLine1;
        private System.Windows.Forms.Label lblFor;
        private global::Controls.SmoothLabel lblTimeout;
        private global::Controls.LinkButton btnEditSettings;
        private global::Controls.LinkButton btnDelete;
        private global::Controls.LinkButton btnMoveDown;
        private global::Controls.LinkButton btnMoveUp;
        private System.Windows.Forms.Panel pnlDetails;
        private global::Controls.SmoothLabel lblHours;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private global::Controls.SmoothLabel smoothLabel2;
        private ScheduleControl scheduleControl;
        private global::Controls.LinkButton btnSelectInverse;
        private global::Controls.LinkButton btnSelectNone;
        private global::Controls.LinkButton btnSelectAll;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboTimeZone;
        private System.Windows.Forms.CheckBox cbExtensionHours;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtContactNumber;
        private System.Windows.Forms.Panel pnlHours;
        private global::Controls.ArrowLine arrowLine1;
        private System.Windows.Forms.Label lblNext;
        private System.Windows.Forms.Panel pnlTryFor;
        private System.Windows.Forms.ComboBox cboExtension;
        private System.Windows.Forms.ComboBox cboType;
        private global::Controls.SmoothLabel smoothLabel1;

    }
}
