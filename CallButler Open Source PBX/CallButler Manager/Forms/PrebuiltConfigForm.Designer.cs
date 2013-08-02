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
    partial class PrebuiltConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrebuiltConfigForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.smoothLabel5 = new global::Controls.SmoothLabel();
            this.wizMain = new global::Controls.Wizard.Wizard();
            this.pgFinish = new global::Controls.Wizard.WizardPage();
            this.label2 = new System.Windows.Forms.Label();
            this.pgGreeting = new global::Controls.Wizard.WizardPage();
            this.lblGreetingTitle = new System.Windows.Forms.Label();
            this.lblGreetingDescription = new System.Windows.Forms.Label();
            this.greetingControl = new CallButler.Manager.Controls.GreetingControl(ManagementInterfaceClient.ManagementClient, ManagementInterfaceClient.AuthInfo);
            this.pgDetails = new global::Controls.Wizard.WizardPage();
            this.lblTextTitle = new System.Windows.Forms.Label();
            this.lblTextDescription = new System.Windows.Forms.Label();
            this.lblTextDisplayName = new System.Windows.Forms.Label();
            this.txtTextValue = new System.Windows.Forms.TextBox();
            this.pgChooseConfig = new global::Controls.Wizard.WizardPage();
            this.prebuiltConfigControl = new CallButler.Manager.ViewControls.PrebuiltConfigControl();
            this.label1 = new System.Windows.Forms.Label();
            this.reflectionPicture1 = new global::Controls.ReflectionPicture();
            this.wizMain.SuspendLayout();
            this.pgFinish.SuspendLayout();
            this.pgGreeting.SuspendLayout();
            this.pgDetails.SuspendLayout();
            this.pgChooseConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNext.Name = "btnNext";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            resources.ApplyResources(this.btnBack, "btnBack");
            this.btnBack.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBack.Name = "btnBack";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // smoothLabel5
            // 
            resources.ApplyResources(this.smoothLabel5, "smoothLabel5");
            this.smoothLabel5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.smoothLabel5.Name = "smoothLabel5";
            // 
            // wizMain
            // 
            this.wizMain.AlwaysShowFinishButton = false;
            this.wizMain.BackColor = System.Drawing.Color.Transparent;
            this.wizMain.CloseOnCancel = false;
            this.wizMain.CloseOnFinish = false;
            this.wizMain.Controls.Add(this.pgFinish);
            this.wizMain.Controls.Add(this.pgGreeting);
            this.wizMain.Controls.Add(this.pgDetails);
            this.wizMain.Controls.Add(this.pgChooseConfig);
            this.wizMain.DisplayButtons = false;
            resources.ApplyResources(this.wizMain, "wizMain");
            this.wizMain.Name = "wizMain";
            this.wizMain.PageIndex = 3;
            this.wizMain.Pages.AddRange(new global::Controls.Wizard.WizardPage[] {
            this.pgChooseConfig,
            this.pgDetails,
            this.pgGreeting,
            this.pgFinish});
            this.wizMain.ShowTabs = false;
            this.wizMain.TabBackColor = System.Drawing.Color.WhiteSmoke;
            this.wizMain.TabBackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.wizMain.TabDividerLineType = global::Controls.Wizard.WizardTabDividerLineType.SingleLine;
            // 
            // pgFinish
            // 
            this.pgFinish.Controls.Add(this.label2);
            resources.ApplyResources(this.pgFinish, "pgFinish");
            this.pgFinish.IsFinishPage = false;
            this.pgFinish.Name = "pgFinish";
            this.pgFinish.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // pgGreeting
            // 
            this.pgGreeting.Controls.Add(this.lblGreetingTitle);
            this.pgGreeting.Controls.Add(this.lblGreetingDescription);
            this.pgGreeting.Controls.Add(this.greetingControl);
            resources.ApplyResources(this.pgGreeting, "pgGreeting");
            this.pgGreeting.IsFinishPage = false;
            this.pgGreeting.Name = "pgGreeting";
            this.pgGreeting.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // lblGreetingTitle
            // 
            resources.ApplyResources(this.lblGreetingTitle, "lblGreetingTitle");
            this.lblGreetingTitle.Name = "lblGreetingTitle";
            // 
            // lblGreetingDescription
            // 
            resources.ApplyResources(this.lblGreetingDescription, "lblGreetingDescription");
            this.lblGreetingDescription.Name = "lblGreetingDescription";
            // 
            // greetingControl
            // 
            this.greetingControl.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.greetingControl, "greetingControl");
            this.greetingControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.greetingControl.Name = "greetingControl";
            // 
            // pgDetails
            // 
            this.pgDetails.Controls.Add(this.lblTextTitle);
            this.pgDetails.Controls.Add(this.lblTextDescription);
            this.pgDetails.Controls.Add(this.lblTextDisplayName);
            this.pgDetails.Controls.Add(this.txtTextValue);
            resources.ApplyResources(this.pgDetails, "pgDetails");
            this.pgDetails.IsFinishPage = false;
            this.pgDetails.Name = "pgDetails";
            this.pgDetails.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // lblTextTitle
            // 
            resources.ApplyResources(this.lblTextTitle, "lblTextTitle");
            this.lblTextTitle.Name = "lblTextTitle";
            // 
            // lblTextDescription
            // 
            resources.ApplyResources(this.lblTextDescription, "lblTextDescription");
            this.lblTextDescription.Name = "lblTextDescription";
            // 
            // lblTextDisplayName
            // 
            resources.ApplyResources(this.lblTextDisplayName, "lblTextDisplayName");
            this.lblTextDisplayName.Name = "lblTextDisplayName";
            // 
            // txtTextValue
            // 
            resources.ApplyResources(this.txtTextValue, "txtTextValue");
            this.txtTextValue.Name = "txtTextValue";
            // 
            // pgChooseConfig
            // 
            this.pgChooseConfig.Controls.Add(this.prebuiltConfigControl);
            this.pgChooseConfig.Controls.Add(this.label1);
            resources.ApplyResources(this.pgChooseConfig, "pgChooseConfig");
            this.pgChooseConfig.IsFinishPage = false;
            this.pgChooseConfig.Name = "pgChooseConfig";
            this.pgChooseConfig.TabLinkColor = System.Drawing.SystemColors.ControlText;
            // 
            // prebuiltConfigControl
            // 
            resources.ApplyResources(this.prebuiltConfigControl, "prebuiltConfigControl");
            this.prebuiltConfigControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.prebuiltConfigControl.Name = "prebuiltConfigControl";
            this.prebuiltConfigControl.SelectedConfigurationChanged += new System.EventHandler(this.prebuiltConfigControl_SelectedConfigurationChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // reflectionPicture1
            // 
            resources.ApplyResources(this.reflectionPicture1, "reflectionPicture1");
            this.reflectionPicture1.Image = global::CallButler.Manager.Properties.Resources.office_building_128_shadow;
            this.reflectionPicture1.Name = "reflectionPicture1";
            // 
            // PrebuiltConfigForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::CallButler.Manager.Properties.Resources.cb_header;
            this.Controls.Add(this.reflectionPicture1);
            this.Controls.Add(this.smoothLabel5);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.wizMain);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrebuiltConfigForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.wizMain.ResumeLayout(false);
            this.pgFinish.ResumeLayout(false);
            this.pgGreeting.ResumeLayout(false);
            this.pgDetails.ResumeLayout(false);
            this.pgDetails.PerformLayout();
            this.pgChooseConfig.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private global::Controls.Wizard.Wizard wizMain;
        private global::Controls.Wizard.WizardPage pgChooseConfig;
        private CallButler.Manager.ViewControls.PrebuiltConfigControl prebuiltConfigControl;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private global::Controls.Wizard.WizardPage pgDetails;
        private System.Windows.Forms.TextBox txtTextValue;
        private System.Windows.Forms.Label lblTextDisplayName;
        private System.Windows.Forms.Label lblTextDescription;
        private global::Controls.Wizard.WizardPage pgFinish;
        private System.Windows.Forms.Label label2;
        private global::Controls.Wizard.WizardPage pgGreeting;
        private System.Windows.Forms.Label lblGreetingDescription;
        private CallButler.Manager.Controls.GreetingControl greetingControl;
        private System.Windows.Forms.Label lblGreetingTitle;
        private System.Windows.Forms.Label lblTextTitle;
        private global::Controls.SmoothLabel smoothLabel5;
        private global::Controls.ReflectionPicture reflectionPicture1;
    }
}