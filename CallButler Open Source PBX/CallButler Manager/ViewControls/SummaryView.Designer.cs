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
    partial class SummaryView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SummaryView));
            this.grpCallHistory = new global::Controls.GroupBoxEx();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlCallHistory = new System.Windows.Forms.Panel();
            this.btnViewCallHistory = new global::Controls.LinkButton();
            this.groupBoxEx1 = new global::Controls.GroupBoxEx();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCallsToday = new System.Windows.Forms.Label();
            this.lblCallsTotal = new System.Windows.Forms.Label();
            this.lblCallsMonth = new System.Windows.Forms.Label();
            this.lblCallsWeek = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.bsVoicemail = new System.Windows.Forms.BindingSource(this.components);
            this.callButlerDataset = new WOSI.CallButler.Data.CallButlerDataset();
            this.roundedCornerPanel1 = new global::Controls.RoundedCornerPanel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.quickTipsControl1 = new CallButler.Manager.Controls.QuickTipsControl();
            this.lblDescription = new global::Controls.SmoothLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpCallHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsVoicemail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.callButlerDataset)).BeginInit();
            this.roundedCornerPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCallHistory
            // 
            this.grpCallHistory.AntiAliasText = false;
            this.grpCallHistory.Controls.Add(this.pictureBox1);
            this.grpCallHistory.Controls.Add(this.pnlCallHistory);
            this.grpCallHistory.Controls.Add(this.btnViewCallHistory);
            this.grpCallHistory.CornerRadius = 10;
            this.grpCallHistory.DividerAbove = false;
            resources.ApplyResources(this.grpCallHistory, "grpCallHistory");
            this.grpCallHistory.DrawLeftDivider = false;
            this.grpCallHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.grpCallHistory.HeaderColor = System.Drawing.Color.Silver;
            this.grpCallHistory.Name = "grpCallHistory";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::CallButler.Manager.Properties.Resources.telephone_24;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pnlCallHistory
            // 
            resources.ApplyResources(this.pnlCallHistory, "pnlCallHistory");
            this.pnlCallHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.pnlCallHistory.Name = "pnlCallHistory";
            this.pnlCallHistory.Resize += new System.EventHandler(this.pnlCallHistory_Resize);
            // 
            // btnViewCallHistory
            // 
            this.btnViewCallHistory.AntiAliasText = false;
            resources.ApplyResources(this.btnViewCallHistory, "btnViewCallHistory");
            this.btnViewCallHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewCallHistory.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnViewCallHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewCallHistory.LinkImage = global::CallButler.Manager.Properties.Resources.view_16;
            this.btnViewCallHistory.Name = "btnViewCallHistory";
            this.btnViewCallHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewCallHistory.UnderlineOnHover = true;
            this.btnViewCallHistory.Click += new System.EventHandler(this.btnViewCallHistory_Click);
            // 
            // groupBoxEx1
            // 
            this.groupBoxEx1.AntiAliasText = false;
            this.groupBoxEx1.Controls.Add(this.pictureBox2);
            this.groupBoxEx1.Controls.Add(this.panel2);
            this.groupBoxEx1.CornerRadius = 10;
            this.groupBoxEx1.DividerAbove = false;
            resources.ApplyResources(this.groupBoxEx1, "groupBoxEx1");
            this.groupBoxEx1.DrawLeftDivider = false;
            this.groupBoxEx1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.groupBoxEx1.HeaderColor = System.Drawing.Color.Silver;
            this.groupBoxEx1.Name = "groupBoxEx1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::CallButler.Manager.Properties.Resources.column_chart_24;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblCallsToday);
            this.panel2.Controls.Add(this.lblCallsTotal);
            this.panel2.Controls.Add(this.lblCallsMonth);
            this.panel2.Controls.Add(this.lblCallsWeek);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.panel2.Name = "panel2";
            // 
            // lblCallsToday
            // 
            resources.ApplyResources(this.lblCallsToday, "lblCallsToday");
            this.lblCallsToday.Name = "lblCallsToday";
            // 
            // lblCallsTotal
            // 
            resources.ApplyResources(this.lblCallsTotal, "lblCallsTotal");
            this.lblCallsTotal.Name = "lblCallsTotal";
            // 
            // lblCallsMonth
            // 
            resources.ApplyResources(this.lblCallsMonth, "lblCallsMonth");
            this.lblCallsMonth.Name = "lblCallsMonth";
            // 
            // lblCallsWeek
            // 
            resources.ApplyResources(this.lblCallsWeek, "lblCallsWeek");
            this.lblCallsWeek.Name = "lblCallsWeek";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // bsVoicemail
            // 
            this.bsVoicemail.AllowNew = false;
            this.bsVoicemail.DataMember = "Voicemails";
            this.bsVoicemail.DataSource = this.callButlerDataset;
            this.bsVoicemail.Filter = "IsNew = True";
            this.bsVoicemail.Sort = "";
            // 
            // callButlerDataset
            // 
            this.callButlerDataset.DataSetName = "CallButlerDataset";
            this.callButlerDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // roundedCornerPanel1
            // 
            this.roundedCornerPanel1.BorderSize = 1F;
            this.roundedCornerPanel1.Controls.Add(this.pictureBox3);
            this.roundedCornerPanel1.Controls.Add(this.quickTipsControl1);
            this.roundedCornerPanel1.Controls.Add(this.lblDescription);
            this.roundedCornerPanel1.CornerRadius = 5;
            this.roundedCornerPanel1.DisplayShadow = true;
            resources.ApplyResources(this.roundedCornerPanel1, "roundedCornerPanel1");
            this.roundedCornerPanel1.ForeColor = System.Drawing.Color.Gainsboro;
            this.roundedCornerPanel1.Name = "roundedCornerPanel1";
            this.roundedCornerPanel1.PanelColor = System.Drawing.Color.GhostWhite;
            this.tableLayoutPanel1.SetRowSpan(this.roundedCornerPanel1, 2);
            this.roundedCornerPanel1.ShadowColor = System.Drawing.Color.Gainsboro;
            this.roundedCornerPanel1.ShadowOffset = 2;
            // 
            // pictureBox3
            // 
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = global::CallButler.Manager.Properties.Resources.about_32;
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // quickTipsControl1
            // 
            this.quickTipsControl1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.quickTipsControl1, "quickTipsControl1");
            this.quickTipsControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.quickTipsControl1.Name = "quickTipsControl1";
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblDescription.Name = "lblDescription";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.roundedCornerPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpCallHistory, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxEx1, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // SummaryView
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tableLayoutPanel1);
            this.HeaderIcon = global::CallButler.Manager.Properties.Resources.gear_find_48_shadow;
            this.Name = "SummaryView";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.grpCallHistory.ResumeLayout(false);
            this.grpCallHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxEx1.ResumeLayout(false);
            this.groupBoxEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsVoicemail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.callButlerDataset)).EndInit();
            this.roundedCornerPanel1.ResumeLayout(false);
            this.roundedCornerPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private global::Controls.GroupBoxEx grpCallHistory;
        private System.Windows.Forms.Panel pnlCallHistory;
        private global::Controls.LinkButton btnViewCallHistory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private global::Controls.GroupBoxEx groupBoxEx1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lblCallsToday;
        private System.Windows.Forms.Label lblCallsTotal;
        private System.Windows.Forms.Label lblCallsMonth;
        private System.Windows.Forms.Label lblCallsWeek;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private WOSI.CallButler.Data.CallButlerDataset callButlerDataset;
        private System.Windows.Forms.BindingSource bsVoicemail;
        private System.Windows.Forms.DataGridViewTextBoxColumn mailboxIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private global::Controls.SmoothLabel lblDescription;
        private global::Controls.RoundedCornerPanel roundedCornerPanel1;
        private CallButler.Manager.Controls.QuickTipsControl quickTipsControl1;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}
