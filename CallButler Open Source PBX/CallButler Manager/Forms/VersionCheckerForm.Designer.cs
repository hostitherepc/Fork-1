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
    partial class VersionCheckerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionCheckerForm));
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.pnlDownloadProgress = new System.Windows.Forms.Panel();
            this.lblPercent = new System.Windows.Forms.Label();
            this.pnlNewVersionAvailable = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblNewVersion = new System.Windows.Forms.Label();
            this.lblInstalledVersion = new System.Windows.Forms.Label();
            this.lblUptoDate = new System.Windows.Forms.Label();
            this.pnlDownloadProgress.SuspendLayout();
            this.pnlNewVersionAvailable.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.AccessibleDescription = null;
            this.btnDownload.AccessibleName = null;
            resources.ApplyResources(this.btnDownload, "btnDownload");
            this.btnDownload.BackgroundImage = null;
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackgroundImage = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // progress
            // 
            this.progress.AccessibleDescription = null;
            this.progress.AccessibleName = null;
            resources.ApplyResources(this.progress, "progress");
            this.progress.BackgroundImage = null;
            this.progress.Font = null;
            this.progress.Name = "progress";
            this.progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // pnlDownloadProgress
            // 
            this.pnlDownloadProgress.AccessibleDescription = null;
            this.pnlDownloadProgress.AccessibleName = null;
            resources.ApplyResources(this.pnlDownloadProgress, "pnlDownloadProgress");
            this.pnlDownloadProgress.BackgroundImage = null;
            this.pnlDownloadProgress.Controls.Add(this.lblPercent);
            this.pnlDownloadProgress.Controls.Add(this.progress);
            this.pnlDownloadProgress.Font = null;
            this.pnlDownloadProgress.Name = "pnlDownloadProgress";
            // 
            // lblPercent
            // 
            this.lblPercent.AccessibleDescription = null;
            this.lblPercent.AccessibleName = null;
            resources.ApplyResources(this.lblPercent, "lblPercent");
            this.lblPercent.Font = null;
            this.lblPercent.Name = "lblPercent";
            // 
            // pnlNewVersionAvailable
            // 
            this.pnlNewVersionAvailable.AccessibleDescription = null;
            this.pnlNewVersionAvailable.AccessibleName = null;
            resources.ApplyResources(this.pnlNewVersionAvailable, "pnlNewVersionAvailable");
            this.pnlNewVersionAvailable.BackColor = System.Drawing.Color.Transparent;
            this.pnlNewVersionAvailable.BackgroundImage = null;
            this.pnlNewVersionAvailable.Controls.Add(this.pnlDownloadProgress);
            this.pnlNewVersionAvailable.Controls.Add(this.lblStatus);
            this.pnlNewVersionAvailable.Font = null;
            this.pnlNewVersionAvailable.Name = "pnlNewVersionAvailable";
            // 
            // lblStatus
            // 
            this.lblStatus.AccessibleDescription = null;
            this.lblStatus.AccessibleName = null;
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.Name = "lblStatus";
            // 
            // lblNewVersion
            // 
            this.lblNewVersion.AccessibleDescription = null;
            this.lblNewVersion.AccessibleName = null;
            resources.ApplyResources(this.lblNewVersion, "lblNewVersion");
            this.lblNewVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblNewVersion.Name = "lblNewVersion";
            // 
            // lblInstalledVersion
            // 
            this.lblInstalledVersion.AccessibleDescription = null;
            this.lblInstalledVersion.AccessibleName = null;
            resources.ApplyResources(this.lblInstalledVersion, "lblInstalledVersion");
            this.lblInstalledVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblInstalledVersion.Name = "lblInstalledVersion";
            // 
            // lblUptoDate
            // 
            this.lblUptoDate.AccessibleDescription = null;
            this.lblUptoDate.AccessibleName = null;
            resources.ApplyResources(this.lblUptoDate, "lblUptoDate");
            this.lblUptoDate.BackColor = System.Drawing.Color.Transparent;
            this.lblUptoDate.Name = "lblUptoDate";
            // 
            // VersionCheckerForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = null;
            this.Controls.Add(this.lblNewVersion);
            this.Controls.Add(this.lblInstalledVersion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblUptoDate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.pnlNewVersionAvailable);
            this.HeaderImage = global::CallButler.Manager.Properties.Resources.earth_network_32_shadow;
            this.Icon = null;
            this.Name = "VersionCheckerForm";
            this.Controls.SetChildIndex(this.pnlNewVersionAvailable, 0);
            this.Controls.SetChildIndex(this.btnDownload, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblUptoDate, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lblInstalledVersion, 0);
            this.Controls.SetChildIndex(this.lblNewVersion, 0);
            this.pnlDownloadProgress.ResumeLayout(false);
            this.pnlNewVersionAvailable.ResumeLayout(false);
            this.pnlNewVersionAvailable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Panel pnlDownloadProgress;
        private System.Windows.Forms.Panel pnlNewVersionAvailable;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblNewVersion;
        private System.Windows.Forms.Label lblInstalledVersion;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Label lblUptoDate;
    }
}