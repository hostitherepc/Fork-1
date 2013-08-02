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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using CallButler.Manager.Utils;

namespace CallButler.Manager.Forms
{
    public partial class VersionCheckerForm : CallButler.Manager.Controls.CallButlerDialogFormBase
    {
        private WebClient webClient;
        private DataSet versionInfo;
        private string downloadFilename;

        public VersionCheckerForm()
        {
            InitializeComponent();
            webClient = new WebClient();

            /*versionInfo = NotificationUtils.GetNewVersionDetails(ManagementInterfaceClient.ManagementInterface.ProductID);

            if (versionInfo.Tables.Count > 0)
            {
                if (versionInfo.Tables[0].Rows.Count > 0)
                {
                    lblInstalledVersion.Text = Application.ProductVersion;
                    lblNewVersion.Text = versionInfo.Tables[0].Rows[0]["Version"].ToString();
                }
            }

            if (NotificationUtils.IsNewVersionAvailable(ManagementInterfaceClient.ManagementInterface.ProductID, Application.ProductVersion))
            {
                pnlNewVersionAvailable.Visible = true;
                lblUptoDate.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.VersionCheckerForm_NotOnLatest);
                btnDownload.Visible = true;
            }
            else
            {*/
                lblUptoDate.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.VersionCheckerForm_RunningLatest);
                btnCancel.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_Close);
                btnDownload.Visible = false;
            //}

            Utils.PrivateLabelUtils.ReplaceProductNameControl(this);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            /*SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "CallButler Install|*.exe";
            DataSet ds = NotificationUtils.GetNewVersionDetails(ManagementInterfaceClient.ManagementInterface.ProductID);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    string fileDownloadUrl = row["FileDownloadURL"].ToString().Replace("~", "");
                    dlg.FileName = fileDownloadUrl.Substring(fileDownloadUrl.LastIndexOf(@"//")).Replace(@"//","");

                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        downloadFilename = dlg.FileName;

                        webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);

                        string url = Properties.Settings.Default.SupportUrl + fileDownloadUrl;

                        Uri uri = new Uri(url);
                        webClient.DownloadFileAsync(uri, dlg.FileName);
                        lblStatus.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.VersionCheckerForm_Downloading);
                        btnDownload.Enabled = false;
                    }
                }
            }*/
        }

        void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.VersionCheckerForm_ProblemDownloading), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.VersionCheckerForm_DownloadError), MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDownload.Enabled = true;
                throw e.Error;
            }
            else if (e.Cancelled == false)
            {
                btnCancel.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_Close);
                lblStatus.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.VersionCheckerForm_DownloadComplete);

                if (MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.VersionCheckeForm_FinishedDownload), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.VersionCheckerForm_DownloadComplete), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes && System.IO.File.Exists(downloadFilename))
                {
                    System.Diagnostics.Process.Start(downloadFilename);

                    Application.Exit();
                    return;
                }
            }
        }

        void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progress.Maximum = 100;
            progress.Value = e.ProgressPercentage;
            lblPercent.Text = e.ProgressPercentage + "%";
            progress.Refresh();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            webClient.CancelAsync();
            this.DialogResult = DialogResult.Cancel;
        }

    }
}