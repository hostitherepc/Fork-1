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
    partial class CallFlowView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CallFlowView));
            this.pnlOuter = new System.Windows.Forms.Panel();
            this.diagramControl = new global::Controls.Diagram.DiagramControl();
            this.pnlInfo = new global::Controls.RoundedCornerPanel();
            this.lblInfoMessage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnSaveImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMultilingual = new System.Windows.Forms.ToolStripButton();
            this.cboLanguage = new System.Windows.Forms.ToolStripComboBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnOrientation = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlOuter.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOuter
            // 
            this.pnlOuter.Controls.Add(this.diagramControl);
            this.pnlOuter.Controls.Add(this.pnlInfo);
            this.pnlOuter.Controls.Add(this.toolStrip);
            resources.ApplyResources(this.pnlOuter, "pnlOuter");
            this.pnlOuter.Name = "pnlOuter";
            // 
            // diagramControl
            // 
            resources.ApplyResources(this.diagramControl, "diagramControl");
            this.diagramControl.ChildNodeMargin = 50;
            this.diagramControl.ConnectorType = global::Controls.Diagram.DiagramConnectorType.Bezier;
            this.diagramControl.DrawArrows = true;
            this.diagramControl.LayoutDirection = global::Controls.Diagram.DiagramLayoutDirection.Horizontal;
            this.diagramControl.Name = "diagramControl";
            this.diagramControl.PeerNodeMargin = 20;
            this.diagramControl.RootShape = null;
            this.diagramControl.ShowExpanders = false;
            // 
            // pnlInfo
            // 
            this.pnlInfo.BackColor = System.Drawing.Color.Transparent;
            this.pnlInfo.BorderSize = 1F;
            this.pnlInfo.Controls.Add(this.lblInfoMessage);
            this.pnlInfo.Controls.Add(this.pictureBox1);
            this.pnlInfo.CornerRadius = 5;
            this.pnlInfo.DisplayShadow = false;
            resources.ApplyResources(this.pnlInfo, "pnlInfo");
            this.pnlInfo.ForeColor = System.Drawing.Color.Silver;
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.PanelColor = System.Drawing.Color.Beige;
            this.pnlInfo.ShadowColor = System.Drawing.Color.Gray;
            this.pnlInfo.ShadowOffset = 5;
            // 
            // lblInfoMessage
            // 
            resources.ApplyResources(this.lblInfoMessage, "lblInfoMessage");
            this.lblInfoMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lblInfoMessage.Name = "lblInfoMessage";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::CallButler.Manager.Properties.Resources.information;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOrientation,
            this.btnSaveImage,
            this.toolStripSeparator1,
            this.btnMultilingual,
            this.cboLanguage});
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Name = "toolStrip";
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Image = global::CallButler.Manager.Properties.Resources.photo_scenery_16;
            resources.ApplyResources(this.btnSaveImage, "btnSaveImage");
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // btnMultilingual
            // 
            this.btnMultilingual.CheckOnClick = true;
            this.btnMultilingual.Image = global::CallButler.Manager.Properties.Resources.earth2_16;
            resources.ApplyResources(this.btnMultilingual, "btnMultilingual");
            this.btnMultilingual.Name = "btnMultilingual";
            this.btnMultilingual.CheckedChanged += new System.EventHandler(this.btnMultilingual_CheckedChanged);
            // 
            // cboLanguage
            // 
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.cboLanguage.Name = "cboLanguage";
            resources.ApplyResources(this.cboLanguage, "cboLanguage");
            this.cboLanguage.SelectedIndexChanged += new System.EventHandler(this.cboLanguage_SelectedIndexChanged);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "GIF";
            this.saveFileDialog.FileName = "CallButler CallFlow";
            resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
            this.saveFileDialog.InitialDirectory = "My Documents";
            // 
            // btnOrientation
            // 
            this.btnOrientation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHorizontal,
            this.mnuVertical});
            this.btnOrientation.Image = global::CallButler.Manager.Properties.Resources.refresh_16;
            resources.ApplyResources(this.btnOrientation, "btnOrientation");
            this.btnOrientation.Name = "btnOrientation";
            this.btnOrientation.ButtonClick += new System.EventHandler(this.btnOrientation_ButtonClick);
            // 
            // mnuHorizontal
            // 
            this.mnuHorizontal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.mnuHorizontal.Name = "mnuHorizontal";
            resources.ApplyResources(this.mnuHorizontal, "mnuHorizontal");
            this.mnuHorizontal.Click += new System.EventHandler(this.mnuHorizontal_Click);
            // 
            // mnuVertical
            // 
            this.mnuVertical.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.mnuVertical.Name = "mnuVertical";
            resources.ApplyResources(this.mnuVertical, "mnuVertical");
            this.mnuVertical.Click += new System.EventHandler(this.mnuVertical_Click);
            // 
            // CallFlowView
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pnlOuter);
            this.HeaderIcon = global::CallButler.Manager.Properties.Resources.branch_48_shadow;
            this.Name = "CallFlowView";
            this.Controls.SetChildIndex(this.pnlOuter, 0);
            this.pnlOuter.ResumeLayout(false);
            this.pnlOuter.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlOuter;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripComboBox cboLanguage;
        private System.Windows.Forms.ToolStripButton btnMultilingual;
        private global::Controls.Diagram.DiagramControl diagramControl;
        private global::Controls.RoundedCornerPanel pnlInfo;
        private System.Windows.Forms.Label lblInfoMessage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripButton btnSaveImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton btnOrientation;
        private System.Windows.Forms.ToolStripMenuItem mnuHorizontal;
        private System.Windows.Forms.ToolStripMenuItem mnuVertical;
    }
}
