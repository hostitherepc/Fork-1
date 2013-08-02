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
    partial class PluginView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginView));
            this.lbPlugins = new global::Controls.ListBoxEx();
            this.roundedCornerPanel1 = new global::Controls.RoundedCornerPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlAddonView = new System.Windows.Forms.Panel();
            this.roundedCornerPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbPlugins
            // 
            this.lbPlugins.AntiAliasText = false;
            this.lbPlugins.BorderColor = System.Drawing.Color.Gray;
            this.lbPlugins.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbPlugins.CaptionColor = System.Drawing.Color.Gray;
            this.lbPlugins.CaptionFont = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPlugins.DisplayMember = "Name";
            resources.ApplyResources(this.lbPlugins, "lbPlugins");
            this.lbPlugins.DrawBorder = false;
            this.lbPlugins.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbPlugins.FormattingEnabled = true;
            this.lbPlugins.ItemMargin = 5;
            this.lbPlugins.Name = "lbPlugins";
            this.lbPlugins.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(225)))), ((int)(((byte)(244)))));
            this.lbPlugins.ValueMember = "CategoryID";
            this.lbPlugins.SelectedIndexChanged += new System.EventHandler(this.lbPlugins_SelectedIndexChanged);
            // 
            // roundedCornerPanel1
            // 
            this.roundedCornerPanel1.BorderSize = 1F;
            this.roundedCornerPanel1.Controls.Add(this.lbPlugins);
            this.roundedCornerPanel1.Controls.Add(this.label1);
            this.roundedCornerPanel1.CornerRadius = 10;
            this.roundedCornerPanel1.DisplayShadow = false;
            resources.ApplyResources(this.roundedCornerPanel1, "roundedCornerPanel1");
            this.roundedCornerPanel1.ForeColor = System.Drawing.Color.DarkGray;
            this.roundedCornerPanel1.Name = "roundedCornerPanel1";
            this.roundedCornerPanel1.PanelColor = System.Drawing.Color.White;
            this.roundedCornerPanel1.ShadowColor = System.Drawing.Color.Gray;
            this.roundedCornerPanel1.ShadowOffset = 5;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.label1.Name = "label1";
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // pnlAddonView
            // 
            resources.ApplyResources(this.pnlAddonView, "pnlAddonView");
            this.pnlAddonView.Name = "pnlAddonView";
            // 
            // PluginView
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pnlAddonView);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.roundedCornerPanel1);
            this.HeaderCaption = "Manage extended functionality for CallButler";
            this.HeaderIcon = global::CallButler.Manager.Properties.Resources.gear_connection_48;
            this.HeaderTitle = "Add-On Modules";
            this.Name = "PluginView";
            this.Controls.SetChildIndex(this.roundedCornerPanel1, 0);
            this.Controls.SetChildIndex(this.splitter1, 0);
            this.Controls.SetChildIndex(this.pnlAddonView, 0);
            this.roundedCornerPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private global::Controls.ListBoxEx lbPlugins;
        private global::Controls.RoundedCornerPanel roundedCornerPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlAddonView;
    }
}
