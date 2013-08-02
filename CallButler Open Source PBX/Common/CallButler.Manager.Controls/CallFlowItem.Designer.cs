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
  partial class CallFlowItem
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
        this.roundedCornerPanel1 = new global::Controls.RoundedCornerPanel();
      this.pnlActions = new System.Windows.Forms.FlowLayoutPanel();
      this.dividerLine1 = new global::Controls.DividerLine();
      this.lblCaption = new System.Windows.Forms.Label();
      this.pnlHeader = new System.Windows.Forms.Panel();
      this.lblTitle = new System.Windows.Forms.Label();
      this.pbIcon = new System.Windows.Forms.PictureBox();
      this.roundedCornerPanel1.SuspendLayout();
      this.pnlHeader.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
      this.SuspendLayout();
      // 
      // roundedCornerPanel1
      // 
      this.roundedCornerPanel1.BorderSize = 1F;
      this.roundedCornerPanel1.Controls.Add(this.pnlActions);
      this.roundedCornerPanel1.Controls.Add(this.dividerLine1);
      this.roundedCornerPanel1.Controls.Add(this.lblCaption);
      this.roundedCornerPanel1.Controls.Add(this.pnlHeader);
      this.roundedCornerPanel1.CornerRadius = 10;
      this.roundedCornerPanel1.DisplayShadow = true;
      this.roundedCornerPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.roundedCornerPanel1.ForeColor = System.Drawing.Color.Gray;
      this.roundedCornerPanel1.Location = new System.Drawing.Point(0, 0);
      this.roundedCornerPanel1.Name = "roundedCornerPanel1";
      this.roundedCornerPanel1.Padding = new System.Windows.Forms.Padding(5, 5, 6, 6);
      this.roundedCornerPanel1.PanelColor = System.Drawing.Color.White;
      this.roundedCornerPanel1.ShadowColor = System.Drawing.Color.LightGray;
      this.roundedCornerPanel1.ShadowOffset = 3;
      this.roundedCornerPanel1.Size = new System.Drawing.Size(275, 85);
      this.roundedCornerPanel1.TabIndex = 1;
      this.roundedCornerPanel1.Text = "roundedCornerPanel1";
      // 
      // pnlActions
      // 
      this.pnlActions.BackColor = System.Drawing.Color.Transparent;
      this.pnlActions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
      this.pnlActions.Location = new System.Drawing.Point(5, 52);
      this.pnlActions.Name = "pnlActions";
      this.pnlActions.Size = new System.Drawing.Size(264, 27);
      this.pnlActions.TabIndex = 5;
      // 
      // dividerLine1
      // 
      this.dividerLine1.BackColor = System.Drawing.Color.Transparent;
      this.dividerLine1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dividerLine1.ForeColor = System.Drawing.Color.Silver;
      this.dividerLine1.GradientWidth = 30;
      this.dividerLine1.LineWidth = 1;
      this.dividerLine1.Location = new System.Drawing.Point(5, 44);
      this.dividerLine1.Name = "dividerLine1";
      this.dividerLine1.Size = new System.Drawing.Size(264, 8);
      this.dividerLine1.TabIndex = 3;
      this.dividerLine1.Vertical = false;
      // 
      // lblCaption
      // 
      this.lblCaption.BackColor = System.Drawing.Color.Transparent;
      this.lblCaption.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblCaption.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCaption.ForeColor = System.Drawing.Color.Gray;
      this.lblCaption.Location = new System.Drawing.Point(5, 29);
      this.lblCaption.Name = "lblCaption";
      this.lblCaption.Size = new System.Drawing.Size(264, 15);
      this.lblCaption.TabIndex = 3;
      this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlHeader
      // 
      this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
      this.pnlHeader.Controls.Add(this.lblTitle);
      this.pnlHeader.Controls.Add(this.pbIcon);
      this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlHeader.Location = new System.Drawing.Point(5, 5);
      this.pnlHeader.Name = "pnlHeader";
      this.pnlHeader.Size = new System.Drawing.Size(264, 24);
      this.pnlHeader.TabIndex = 1;
      // 
      // lblTitle
      // 
      this.lblTitle.BackColor = System.Drawing.Color.Transparent;
      this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
      this.lblTitle.Location = new System.Drawing.Point(24, 0);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new System.Drawing.Size(240, 24);
      this.lblTitle.TabIndex = 1;
      this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pbIcon
      // 
      this.pbIcon.BackColor = System.Drawing.Color.Transparent;
      this.pbIcon.Dock = System.Windows.Forms.DockStyle.Left;
      this.pbIcon.Location = new System.Drawing.Point(0, 0);
      this.pbIcon.Name = "pbIcon";
      this.pbIcon.Size = new System.Drawing.Size(24, 24);
      this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pbIcon.TabIndex = 0;
      this.pbIcon.TabStop = false;
      // 
      // CallFlowItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.roundedCornerPanel1);
      this.Name = "CallFlowItem";
      this.Size = new System.Drawing.Size(275, 85);
      this.roundedCornerPanel1.ResumeLayout(false);
      this.pnlHeader.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

      private global::Controls.RoundedCornerPanel roundedCornerPanel1;
    private System.Windows.Forms.Panel pnlHeader;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.PictureBox pbIcon;
    private System.Windows.Forms.Label lblCaption;
      private global::Controls.DividerLine dividerLine1;
    private System.Windows.Forms.FlowLayoutPanel pnlActions;
  }
}
