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



namespace CallButler.Manager.Forms
{
    partial class SMTPServerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMTPServerForm));
            this.label5 = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.numSMTPPort = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSendTestEmail = new global::Controls.LinkButton();
            this.cbSSL = new System.Windows.Forms.CheckBox();
            this.smoothLabel3 = new global::Controls.SmoothLabel();
            this.smoothLabel1 = new global::Controls.SmoothLabel();
            this.lblSSL = new global::Controls.SmoothLabel();
            this.smoothLabel2 = new global::Controls.SmoothLabel();
            this.smoothLabel4 = new global::Controls.SmoothLabel();
            ((System.ComponentModel.ISupportInitialize)(this.numSMTPPort)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AccessibleDescription = null;
            this.label5.AccessibleName = null;
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = null;
            this.label5.Name = "label5";
            // 
            // txtServerName
            // 
            this.txtServerName.AccessibleDescription = null;
            this.txtServerName.AccessibleName = null;
            resources.ApplyResources(this.txtServerName, "txtServerName");
            this.txtServerName.BackgroundImage = null;
            this.txtServerName.Font = null;
            this.txtServerName.Name = "txtServerName";
            // 
            // txtUsername
            // 
            this.txtUsername.AccessibleDescription = null;
            this.txtUsername.AccessibleName = null;
            resources.ApplyResources(this.txtUsername, "txtUsername");
            this.txtUsername.BackgroundImage = null;
            this.txtUsername.Font = null;
            this.txtUsername.Name = "txtUsername";
            // 
            // txtPassword
            // 
            this.txtPassword.AccessibleDescription = null;
            this.txtPassword.AccessibleName = null;
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.BackgroundImage = null;
            this.txtPassword.Font = null;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // numSMTPPort
            // 
            this.numSMTPPort.AccessibleDescription = null;
            this.numSMTPPort.AccessibleName = null;
            resources.ApplyResources(this.numSMTPPort, "numSMTPPort");
            this.numSMTPPort.Font = null;
            this.numSMTPPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numSMTPPort.Name = "numSMTPPort";
            this.numSMTPPort.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            this.btnOK.AccessibleDescription = null;
            this.btnOK.AccessibleName = null;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackgroundImage = null;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = null;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackgroundImage = null;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSendTestEmail
            // 
            this.btnSendTestEmail.AccessibleDescription = null;
            this.btnSendTestEmail.AccessibleName = null;
            resources.ApplyResources(this.btnSendTestEmail, "btnSendTestEmail");
            this.btnSendTestEmail.AntiAliasText = false;
            this.btnSendTestEmail.BackColor = System.Drawing.Color.Transparent;
            this.btnSendTestEmail.BackgroundImage = null;
            this.btnSendTestEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendTestEmail.Font = null;
            this.btnSendTestEmail.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnSendTestEmail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendTestEmail.LinkImage = global::CallButler.Manager.Properties.Resources.mail_earth_16;
            this.btnSendTestEmail.Name = "btnSendTestEmail";
            this.btnSendTestEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSendTestEmail.UnderlineOnHover = true;
            this.btnSendTestEmail.Click += new System.EventHandler(this.btnSendTestEmail_Click);
            // 
            // cbSSL
            // 
            this.cbSSL.AccessibleDescription = null;
            this.cbSSL.AccessibleName = null;
            resources.ApplyResources(this.cbSSL, "cbSSL");
            this.cbSSL.BackColor = System.Drawing.Color.Transparent;
            this.cbSSL.BackgroundImage = null;
            this.cbSSL.Font = null;
            this.cbSSL.Name = "cbSSL";
            this.cbSSL.UseVisualStyleBackColor = false;
            // 
            // smoothLabel3
            // 
            this.smoothLabel3.AccessibleDescription = null;
            this.smoothLabel3.AccessibleName = null;
            resources.ApplyResources(this.smoothLabel3, "smoothLabel3");
            this.smoothLabel3.AntiAliasText = false;
            this.smoothLabel3.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel3.EnableHelp = true;
            this.smoothLabel3.Font = null;
            this.smoothLabel3.Name = "smoothLabel3";
            // 
            // smoothLabel1
            // 
            this.smoothLabel1.AccessibleDescription = null;
            this.smoothLabel1.AccessibleName = null;
            resources.ApplyResources(this.smoothLabel1, "smoothLabel1");
            this.smoothLabel1.AntiAliasText = false;
            this.smoothLabel1.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel1.EnableHelp = true;
            this.smoothLabel1.Font = null;
            this.smoothLabel1.Name = "smoothLabel1";
            // 
            // lblSSL
            // 
            this.lblSSL.AccessibleDescription = null;
            this.lblSSL.AccessibleName = null;
            resources.ApplyResources(this.lblSSL, "lblSSL");
            this.lblSSL.AntiAliasText = false;
            this.lblSSL.BackColor = System.Drawing.Color.Transparent;
            this.lblSSL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSSL.EnableHelp = true;
            this.lblSSL.Font = null;
            this.lblSSL.Name = "lblSSL";
            this.lblSSL.Click += new System.EventHandler(this.lblSSL_Click);
            // 
            // smoothLabel2
            // 
            this.smoothLabel2.AccessibleDescription = null;
            this.smoothLabel2.AccessibleName = null;
            resources.ApplyResources(this.smoothLabel2, "smoothLabel2");
            this.smoothLabel2.AntiAliasText = false;
            this.smoothLabel2.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel2.EnableHelp = true;
            this.smoothLabel2.Font = null;
            this.smoothLabel2.Name = "smoothLabel2";
            // 
            // smoothLabel4
            // 
            this.smoothLabel4.AccessibleDescription = null;
            this.smoothLabel4.AccessibleName = null;
            resources.ApplyResources(this.smoothLabel4, "smoothLabel4");
            this.smoothLabel4.AntiAliasText = false;
            this.smoothLabel4.BackColor = System.Drawing.Color.Transparent;
            this.smoothLabel4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothLabel4.EnableHelp = true;
            this.smoothLabel4.Font = null;
            this.smoothLabel4.Name = "smoothLabel4";
            // 
            // SMTPServerForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.Controls.Add(this.smoothLabel4);
            this.Controls.Add(this.smoothLabel2);
            this.Controls.Add(this.lblSSL);
            this.Controls.Add(this.smoothLabel1);
            this.Controls.Add(this.smoothLabel3);
            this.Controls.Add(this.cbSSL);
            this.Controls.Add(this.btnSendTestEmail);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.numSMTPPort);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.label5);
            this.HeaderImage = global::CallButler.Manager.Properties.Resources.mail_earth_32_shadow;
            this.Icon = null;
            this.Name = "SMTPServerForm";
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.txtServerName, 0);
            this.Controls.SetChildIndex(this.txtUsername, 0);
            this.Controls.SetChildIndex(this.txtPassword, 0);
            this.Controls.SetChildIndex(this.numSMTPPort, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.btnSendTestEmail, 0);
            this.Controls.SetChildIndex(this.cbSSL, 0);
            this.Controls.SetChildIndex(this.smoothLabel3, 0);
            this.Controls.SetChildIndex(this.smoothLabel1, 0);
            this.Controls.SetChildIndex(this.lblSSL, 0);
            this.Controls.SetChildIndex(this.smoothLabel2, 0);
            this.Controls.SetChildIndex(this.smoothLabel4, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numSMTPPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.NumericUpDown numSMTPPort;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private global::Controls.LinkButton btnSendTestEmail;
        private System.Windows.Forms.CheckBox cbSSL;
        private global::Controls.SmoothLabel smoothLabel3;
        private global::Controls.SmoothLabel smoothLabel1;
        private global::Controls.SmoothLabel lblSSL;
        private global::Controls.SmoothLabel smoothLabel2;
        private global::Controls.SmoothLabel smoothLabel4;
    }
}
