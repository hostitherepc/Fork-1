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

namespace CallButler.Manager.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            lblDescription.Text = ManagementInterfaceClient.ManagementInterface.ProductDescription;

            lblCopyright.Text = String.Format("©2005-{0} {1}", DateTime.Now.Year, Properties.LocalizedStrings.AboutForm_CopyrightPostfix);
            lblClientVersion.Text = String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.AboutForm_ManagerVersion), this.GetType().Assembly.GetName().Version.ToString());
            lblServerVersion.Text = String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.AboutForm_ServerVersion), ManagementInterfaceClient.ManagementInterface.ServerVersion.ToString());
            lblAdditionalCopyright.Text = ManagementInterfaceClient.ManagementInterface.AdditionalCopyrightNotice;

            if (ManagementInterfaceClient.ManagementInterface.IsFreeVersion)
            {
                lblRegInfo.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.AboutForm_FreeLicense);
            }
            else if (ManagementInterfaceClient.ManagementInterface.IsLicensed)
            {
                lblRegInfo.Text = String.Format(Properties.LocalizedStrings.AboutForm_LicensedTo, ManagementInterfaceClient.ManagementInterface.LicenseName);

                DateTime expDate = ManagementInterfaceClient.ManagementInterface.LicenseExpiration;

                if (expDate > DateTime.MinValue)
                    lblRegInfo.Text += String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.AboutForm_ExpiresOn), expDate.ToLongDateString());
            }
            else
            {
                DateTime trialExp = ManagementInterfaceClient.ManagementInterface.TrialExpiration;

                if (trialExp > DateTime.Now)
                {
                    lblRegInfo.Text = String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.AboutForm_ExpiresOn), trialExp.ToLongDateString());
                }
                else
                {
                    lblRegInfo.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.AboutForm_Expired);
                }
            }

            this.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(this.Text);
        }

        private void pbLogo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.telephony2.com");
        }
    }
}