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
    partial class ReceptionistView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceptionistView));
            this.lblRecep = new global::Controls.SmoothLabel();
            this.cbEnableRecep = new System.Windows.Forms.CheckBox();
            this.smoothLabel1 = new global::Controls.SmoothLabel();
            this.extensionsView = new CallButler.Manager.ViewControls.ExtensionsView();
            this.SuspendLayout();
            // 
            // lblRecep
            // 
            this.lblRecep.AntiAliasText = false;
            this.lblRecep.AutoSize = true;
            this.lblRecep.BackColor = System.Drawing.Color.Transparent;
            this.lblRecep.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblRecep.EnableHelp = true;
            this.lblRecep.HelpText = "Check this if you want to give a receptionist the opportunity to answer a call be" +
                "fore CallButler automatically picks up.";
            this.lblRecep.HelpTitle = "Allow Receptionist Answer";
            this.lblRecep.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRecep.Location = new System.Drawing.Point(23, 22);
            this.lblRecep.Name = "lblRecep";
            this.lblRecep.Size = new System.Drawing.Size(241, 13);
            this.lblRecep.TabIndex = 24;
            this.lblRecep.Text = "Allow receptionist/operator to answer call first";
            this.lblRecep.Click += new System.EventHandler(this.lblRecep_Click);
            // 
            // cbEnableRecep
            // 
            this.cbEnableRecep.AutoSize = true;
            this.cbEnableRecep.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cbEnableRecep.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbEnableRecep.Location = new System.Drawing.Point(6, 22);
            this.cbEnableRecep.Name = "cbEnableRecep";
            this.cbEnableRecep.Size = new System.Drawing.Size(15, 14);
            this.cbEnableRecep.TabIndex = 23;
            this.cbEnableRecep.UseVisualStyleBackColor = true;
            this.cbEnableRecep.CheckedChanged += new System.EventHandler(this.cbEnableRecep_CheckedChanged);
            // 
            // smoothLabel1
            // 
            this.smoothLabel1.AntiAliasText = false;
            this.smoothLabel1.AutoSize = true;
            this.smoothLabel1.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel1.EnableHelp = true;
            this.smoothLabel1.HelpText = "The extension choosen below will be called before CallButler automatically answer" +
                "s the call. If there is no answer, CallButler will automatically answer the call" +
                " and begin the Call Flow.";
            this.smoothLabel1.HelpTitle = "Receptionist Extension";
            this.smoothLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.smoothLabel1.Location = new System.Drawing.Point(3, 48);
            this.smoothLabel1.Name = "smoothLabel1";
            this.smoothLabel1.Size = new System.Drawing.Size(321, 13);
            this.smoothLabel1.TabIndex = 25;
            this.smoothLabel1.Text = "Choose an extension below to act as the receptionist/operator";
            // 
            // extensionsView
            // 
            this.extensionsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.extensionsView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.extensionsView.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.extensionsView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.extensionsView.HeaderCaption = "Manage your employee extensions";
            this.extensionsView.HeaderIcon = ((System.Drawing.Image)(resources.GetObject("extensionsView.HeaderIcon")));
            this.extensionsView.HeaderTitle = "Extensions";
            this.extensionsView.Location = new System.Drawing.Point(0, 66);
            this.extensionsView.Name = "extensionsView";
            this.extensionsView.ShowHelpPanel = false;
            this.extensionsView.ShowVoicemailColumn = false;
            this.extensionsView.Size = new System.Drawing.Size(627, 247);
            this.extensionsView.TabIndex = 28;
            // 
            // ReceptionistView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.extensionsView);
            this.Controls.Add(this.smoothLabel1);
            this.Controls.Add(this.lblRecep);
            this.Controls.Add(this.cbEnableRecep);
            this.HeaderCaption = "Use CallButler with a receptionist";
            this.HeaderIcon = ((System.Drawing.Image)(resources.GetObject("$this.HeaderIcon")));
            this.HeaderTitle = "Receptionist";
            this.Name = "ReceptionistView";
            this.Controls.SetChildIndex(this.cbEnableRecep, 0);
            this.Controls.SetChildIndex(this.lblRecep, 0);
            this.Controls.SetChildIndex(this.smoothLabel1, 0);
            this.Controls.SetChildIndex(this.extensionsView, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private global::Controls.SmoothLabel lblRecep;
        private System.Windows.Forms.CheckBox cbEnableRecep;
        private global::Controls.SmoothLabel smoothLabel1;
        private ExtensionsView extensionsView;
    }
}
