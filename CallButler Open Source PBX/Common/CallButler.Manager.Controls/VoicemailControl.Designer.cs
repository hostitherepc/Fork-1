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


using Controls;

namespace CallButler.Manager.Controls
{
    partial class VoicemailControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoicemailControl));
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDeleteMessage = new System.Windows.Forms.Button();
            this.btnSaveMessage = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.progSound = new global::Controls.ProgressBarEx();
            this.lblNewMessage = new global::Controls.SmoothLabel();
            this.lblTelephoneNumber = new global::Controls.SmoothLabel();
            this.lblCallerID = new global::Controls.SmoothLabel();
            this.lblDateTime = new global::Controls.SmoothLabel();
            this.lblMessageCount = new global::Controls.SmoothLabel();
            this.trkVolume = new global::Controls.TrackBarEx();
            this.voicemailDataset = new WOSI.CallButler.Data.CallButlerDataset();
            this.bsVoicemails = new System.Windows.Forms.BindingSource(this.components);
            this.dgVoicemails = new CallButler.Manager.Controls.CallButlerDataGrid();
            this.colVoicemailImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.voicemailIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extensionIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timestampDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callerDisplayNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callerUsernameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callerHostDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isNewDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlList = new System.Windows.Forms.Panel();
            this.gradientPanel1 = new global::Controls.GradientPanel();
            this.pnlPlayer = new System.Windows.Forms.Panel();
            this.btnImportOutlook = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.voicemailDataset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVoicemails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgVoicemails)).BeginInit();
            this.pnlList.SuspendLayout();
            this.pnlPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnPrevious, "btnPrevious");
            this.btnPrevious.Image = global::CallButler.Manager.Controls.Properties.Resources.media_step_back_16;
            this.btnPrevious.Name = "btnPrevious";
            this.toolTip.SetToolTip(this.btnPrevious, resources.GetString("btnPrevious.ToolTip"));
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnPlay, "btnPlay");
            this.btnPlay.Image = global::CallButler.Manager.Controls.Properties.Resources.media_play_16;
            this.btnPlay.Name = "btnPlay";
            this.toolTip.SetToolTip(this.btnPlay, resources.GetString("btnPlay.ToolTip"));
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Gainsboro;
            this.btnStop.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnStop, "btnStop");
            this.btnStop.Image = global::CallButler.Manager.Controls.Properties.Resources.media_stop_red_16;
            this.btnStop.Name = "btnStop";
            this.toolTip.SetToolTip(this.btnStop, resources.GetString("btnStop.ToolTip"));
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Gainsboro;
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Image = global::CallButler.Manager.Controls.Properties.Resources.media_step_forward_16;
            this.btnNext.Name = "btnNext";
            this.toolTip.SetToolTip(this.btnNext, resources.GetString("btnNext.ToolTip"));
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // btnDeleteMessage
            // 
            this.btnDeleteMessage.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDeleteMessage.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnDeleteMessage, "btnDeleteMessage");
            this.btnDeleteMessage.Image = global::CallButler.Manager.Controls.Properties.Resources.error_16;
            this.btnDeleteMessage.Name = "btnDeleteMessage";
            this.toolTip.SetToolTip(this.btnDeleteMessage, resources.GetString("btnDeleteMessage.ToolTip"));
            this.btnDeleteMessage.UseVisualStyleBackColor = false;
            this.btnDeleteMessage.Click += new System.EventHandler(this.btnDeleteMessage_Click);
            // 
            // btnSaveMessage
            // 
            this.btnSaveMessage.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSaveMessage.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnSaveMessage, "btnSaveMessage");
            this.btnSaveMessage.Image = global::CallButler.Manager.Controls.Properties.Resources.disk_blue_16;
            this.btnSaveMessage.Name = "btnSaveMessage";
            this.toolTip.SetToolTip(this.btnSaveMessage, resources.GetString("btnSaveMessage.ToolTip"));
            this.btnSaveMessage.UseVisualStyleBackColor = false;
            this.btnSaveMessage.Click += new System.EventHandler(this.btnSaveMessage_Click);
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::CallButler.Manager.Controls.Properties.Resources.loudspeaker_16;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // progSound
            // 
            this.progSound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.progSound.DrawTicks = false;
            this.progSound.ForeColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.progSound, "progSound");
            this.progSound.LowerValue = 0;
            this.progSound.MaxColor = System.Drawing.Color.DimGray;
            this.progSound.MaxValue = 100;
            this.progSound.MinColor = System.Drawing.Color.DimGray;
            this.progSound.MinValue = 0;
            this.progSound.Name = "progSound";
            this.progSound.UpperValue = 0;
            // 
            // lblNewMessage
            // 
            this.lblNewMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.lblNewMessage, "lblNewMessage");
            this.lblNewMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblNewMessage.Name = "lblNewMessage";
            // 
            // lblTelephoneNumber
            // 
            this.lblTelephoneNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.lblTelephoneNumber, "lblTelephoneNumber");
            this.lblTelephoneNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTelephoneNumber.Name = "lblTelephoneNumber";
            // 
            // lblCallerID
            // 
            this.lblCallerID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.lblCallerID, "lblCallerID");
            this.lblCallerID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCallerID.Name = "lblCallerID";
            // 
            // lblDateTime
            // 
            this.lblDateTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.lblDateTime, "lblDateTime");
            this.lblDateTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDateTime.Name = "lblDateTime";
            // 
            // lblMessageCount
            // 
            this.lblMessageCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.lblMessageCount, "lblMessageCount");
            this.lblMessageCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMessageCount.Name = "lblMessageCount";
            // 
            // trkVolume
            // 
            resources.ApplyResources(this.trkVolume, "trkVolume");
            this.trkVolume.BackColor = System.Drawing.Color.Transparent;
            this.trkVolume.MaxValue = 65535;
            this.trkVolume.MinValue = 0;
            this.trkVolume.Name = "trkVolume";
            this.trkVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trkVolume.TrackerCursor = System.Windows.Forms.Cursors.Hand;
            this.trkVolume.TrackerImage = ((System.Drawing.Image)(resources.GetObject("trkVolume.TrackerImage")));
            this.trkVolume.Value = 0;
            this.trkVolume.ValueChanged += new System.EventHandler(this.trkVolume_ValueChanged);
            // 
            // voicemailDataset
            // 
            this.voicemailDataset.DataSetName = "CallButlerDataset";
            this.voicemailDataset.RemotingFormat = System.Data.SerializationFormat.Binary;
            this.voicemailDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.ExcludeSchema;
            // 
            // bsVoicemails
            // 
            this.bsVoicemails.DataMember = "Voicemails";
            this.bsVoicemails.DataSource = this.voicemailDataset;
            // 
            // dgVoicemails
            // 
            this.dgVoicemails.AllowUserToAddRows = false;
            this.dgVoicemails.AllowUserToDeleteRows = false;
            this.dgVoicemails.AllowUserToResizeRows = false;
            this.dgVoicemails.AutoGenerateColumns = false;
            this.dgVoicemails.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgVoicemails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgVoicemails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgVoicemails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgVoicemails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVoicemails.ColumnHeadersVisible = false;
            this.dgVoicemails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colVoicemailImage,
            this.voicemailIDDataGridViewTextBoxColumn,
            this.extensionIDDataGridViewTextBoxColumn,
            this.timestampDataGridViewTextBoxColumn,
            this.callerDisplayNameDataGridViewTextBoxColumn,
            this.callerUsernameDataGridViewTextBoxColumn,
            this.callerHostDataGridViewTextBoxColumn,
            this.isNewDataGridViewCheckBoxColumn});
            this.dgVoicemails.DataSource = this.bsVoicemails;
            resources.ApplyResources(this.dgVoicemails, "dgVoicemails");
            this.dgVoicemails.MultiSelect = false;
            this.dgVoicemails.Name = "dgVoicemails";
            this.dgVoicemails.ReadOnly = true;
            this.dgVoicemails.RowHeadersVisible = false;
            this.dgVoicemails.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgVoicemails.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgVoicemails.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(225)))), ((int)(((byte)(244)))));
            this.dgVoicemails.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.dgVoicemails.RowTemplate.Height = 32;
            this.dgVoicemails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgVoicemails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgVoicemails_CellFormatting);
            this.dgVoicemails.SelectionChanged += new System.EventHandler(this.dgVoicemails_SelectionChanged);
            // 
            // colVoicemailImage
            // 
            this.colVoicemailImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(this.colVoicemailImage, "colVoicemailImage");
            this.colVoicemailImage.Image = global::CallButler.Manager.Controls.Properties.Resources.message_16;
            this.colVoicemailImage.Name = "colVoicemailImage";
            this.colVoicemailImage.ReadOnly = true;
            // 
            // voicemailIDDataGridViewTextBoxColumn
            // 
            this.voicemailIDDataGridViewTextBoxColumn.DataPropertyName = "VoicemailID";
            resources.ApplyResources(this.voicemailIDDataGridViewTextBoxColumn, "voicemailIDDataGridViewTextBoxColumn");
            this.voicemailIDDataGridViewTextBoxColumn.Name = "voicemailIDDataGridViewTextBoxColumn";
            this.voicemailIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // extensionIDDataGridViewTextBoxColumn
            // 
            this.extensionIDDataGridViewTextBoxColumn.DataPropertyName = "ExtensionID";
            resources.ApplyResources(this.extensionIDDataGridViewTextBoxColumn, "extensionIDDataGridViewTextBoxColumn");
            this.extensionIDDataGridViewTextBoxColumn.Name = "extensionIDDataGridViewTextBoxColumn";
            this.extensionIDDataGridViewTextBoxColumn.ReadOnly = true;
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
            this.callerDisplayNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
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
            // isNewDataGridViewCheckBoxColumn
            // 
            this.isNewDataGridViewCheckBoxColumn.DataPropertyName = "IsNew";
            resources.ApplyResources(this.isNewDataGridViewCheckBoxColumn, "isNewDataGridViewCheckBoxColumn");
            this.isNewDataGridViewCheckBoxColumn.Name = "isNewDataGridViewCheckBoxColumn";
            this.isNewDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // pnlList
            // 
            this.pnlList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlList.Controls.Add(this.dgVoicemails);
            resources.ApplyResources(this.pnlList, "pnlList");
            this.pnlList.Name = "pnlList";
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BackColor = System.Drawing.Color.DimGray;
            this.gradientPanel1.BorderColor = System.Drawing.Color.Black;
            this.gradientPanel1.BorderWidth = 1F;
            resources.ApplyResources(this.gradientPanel1, "gradientPanel1");
            this.gradientPanel1.DrawBorder = false;
            this.gradientPanel1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.gradientPanel1.GradientAngle = 90F;
            this.gradientPanel1.Name = "gradientPanel1";
            // 
            // pnlPlayer
            // 
            resources.ApplyResources(this.pnlPlayer, "pnlPlayer");
            this.pnlPlayer.Controls.Add(this.btnImportOutlook);
            this.pnlPlayer.Controls.Add(this.pictureBox2);
            this.pnlPlayer.Controls.Add(this.btnSaveMessage);
            this.pnlPlayer.Controls.Add(this.progSound);
            this.pnlPlayer.Controls.Add(this.btnDeleteMessage);
            this.pnlPlayer.Controls.Add(this.lblNewMessage);
            this.pnlPlayer.Controls.Add(this.btnPrevious);
            this.pnlPlayer.Controls.Add(this.lblTelephoneNumber);
            this.pnlPlayer.Controls.Add(this.btnPlay);
            this.pnlPlayer.Controls.Add(this.lblCallerID);
            this.pnlPlayer.Controls.Add(this.btnStop);
            this.pnlPlayer.Controls.Add(this.lblDateTime);
            this.pnlPlayer.Controls.Add(this.btnNext);
            this.pnlPlayer.Controls.Add(this.lblMessageCount);
            this.pnlPlayer.Controls.Add(this.trkVolume);
            this.pnlPlayer.Controls.Add(this.pictureBox1);
            this.pnlPlayer.Name = "pnlPlayer";
            // 
            // btnImportOutlook
            // 
            this.btnImportOutlook.BackColor = System.Drawing.Color.Gainsboro;
            this.btnImportOutlook.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnImportOutlook, "btnImportOutlook");
            this.btnImportOutlook.Image = global::CallButler.Manager.Controls.Properties.Resources.outlook_16;
            this.btnImportOutlook.Name = "btnImportOutlook";
            this.toolTip.SetToolTip(this.btnImportOutlook, resources.GetString("btnImportOutlook.ToolTip"));
            this.btnImportOutlook.UseVisualStyleBackColor = false;
            this.btnImportOutlook.Click += new System.EventHandler(this.btnImportOutlook_Click);
            // 
            // saveFileDialog
            // 
            resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
            // 
            // VoicemailControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlList);
            this.Controls.Add(this.gradientPanel1);
            this.Controls.Add(this.pnlPlayer);
            this.Name = "VoicemailControl";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.voicemailDataset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVoicemails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgVoicemails)).EndInit();
            this.pnlList.ResumeLayout(false);
            this.pnlPlayer.ResumeLayout(false);
            this.pnlPlayer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDeleteMessage;
        private System.Windows.Forms.Button btnSaveMessage;
        private TrackBarEx trkVolume;
        private System.Windows.Forms.PictureBox pictureBox2;
        private SmoothLabel lblMessageCount;
        private SmoothLabel lblDateTime;
        private SmoothLabel lblCallerID;
        private SmoothLabel lblTelephoneNumber;
        private SmoothLabel lblNewMessage;
        private ProgressBarEx progSound;
        private WOSI.CallButler.Data.CallButlerDataset voicemailDataset;
        private System.Windows.Forms.BindingSource bsVoicemails;
        private CallButler.Manager.Controls.CallButlerDataGrid dgVoicemails;
        private System.Windows.Forms.Panel pnlList;
        private GradientPanel gradientPanel1;
        private System.Windows.Forms.Panel pnlPlayer;
        private System.Windows.Forms.DataGridViewImageColumn colVoicemailImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn voicemailIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn extensionIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callerDisplayNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callerUsernameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callerHostDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isNewDataGridViewCheckBoxColumn;
        private System.Windows.Forms.Button btnImportOutlook;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}
