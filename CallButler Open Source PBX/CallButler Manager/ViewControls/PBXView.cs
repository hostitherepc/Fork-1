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

namespace CallButler.Manager.ViewControls
{
    public partial class PBXView : CallButler.Manager.ViewControls.ViewControlBase
    {
        public PBXView()
        {
            InitializeComponent();

            wizard1.PageIndex = 0;
        }

        protected override void OnLoadData()
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            LoadPhoneStatus();

            numRegister.Value = ManagementInterfaceClient.ManagementInterface.GetPBXRegistrationTimeout(ManagementInterfaceClient.AuthInfo);

            txtDialPrefix.Text = ManagementInterfaceClient.ManagementInterface.GetPBXDialPrefix(ManagementInterfaceClient.AuthInfo);

            txtRegDomain.Text = ManagementInterfaceClient.ManagementInterface.GetPBXRegistrarDomain(ManagementInterfaceClient.AuthInfo);

            btnApply.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void SaveSettings()
        {
            ManagementInterfaceClient.ManagementInterface.SetPBXRegistrationTimeout(ManagementInterfaceClient.AuthInfo, (int)numRegister.Value);

            ManagementInterfaceClient.ManagementInterface.SetPBXDialPrefix(ManagementInterfaceClient.AuthInfo, txtDialPrefix.Text);

            ManagementInterfaceClient.ManagementInterface.SetPBXRegistrarDomain(ManagementInterfaceClient.AuthInfo, txtRegDomain.Text);

            btnApply.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void LoadPhoneStatus()
        {
            dgPhones.Rows.Clear();

            // Get our phone status information
            WOSI.CallButler.Data.CallButlerPhoneStatusDataset.PhoneStatusDataTable phoneStatus = ManagementInterfaceClient.ManagementInterface.GetPhoneExtensionStatus(ManagementInterfaceClient.AuthInfo);

            foreach (WOSI.CallButler.Data.CallButlerPhoneStatusDataset.PhoneStatusRow statusRow in phoneStatus)
            {
                DataGridViewRow row = new DataGridViewRow();
                string statusText = "";
                string configureText = "";

                switch ((WOSI.CallButler.Data.PhoneExtensionStatus)statusRow.StatusCode)
                {
                    case WOSI.CallButler.Data.PhoneExtensionStatus.Online:
                        statusText = "Online";
                        row.DefaultCellStyle.ForeColor = Color.Green;
                        row.DefaultCellStyle.SelectionForeColor = Color.Green;
                        configureText = "Configure...";
                        break;

                    case WOSI.CallButler.Data.PhoneExtensionStatus.Offline:
                        statusText = "Offline";
                        row.DefaultCellStyle.ForeColor = Color.Firebrick;
                        row.DefaultCellStyle.SelectionForeColor = Color.Firebrick;
                        break;
                }

                row.Tag = statusRow;
                row.CreateCells(dgPhones, Properties.Resources.telephone_24, statusRow.ExtensionNumber, string.Format("{0} {1}", statusRow.FirstName, statusRow.LastName), statusRow.RemoteAddress, statusText, configureText);
                //row.CreateCells(dgPhones, "456");

                dgPhones.Rows.Add(row);
                //dgPhones.Rows.Add(
                /*ListViewItem lvItem = new ListViewItem();

                lvItem.Text = string.Format("{0} - {1} {2}", statusRow.ExtensionNumber, statusRow.FirstName, statusRow.LastName);

                string statusText = "";

                switch ((WOSI.CallButler.Data.PhoneExtensionStatus)statusRow.StatusCode)
                {
                    case WOSI.CallButler.Data.PhoneExtensionStatus.Online:
                        statusText = "Online";
                        lvItem.ForeColor = Color.Green;
                        break;

                    case WOSI.CallButler.Data.PhoneExtensionStatus.Offline:
                        statusText = "Offline";
                        lvItem.ForeColor = Color.Firebrick;
                        break;
                }

                lvItem.SubItems.Add(statusText);

                lvPhones.Items.Add(lvItem);*/
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void SettingChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void txtDialPrefix_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '#' && e.KeyChar != '*' && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void ManagePhone(string addressString)
        {
            if (addressString != null && addressString.Length > 0)
            {
                string[] addresses = addressString.Split(' ');

                foreach (string address in addresses)
                {
                    System.Diagnostics.Process.Start(string.Format("http://" + address));
                }
            }
        }

        private void dgPhones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            WOSI.CallButler.Data.CallButlerPhoneStatusDataset.PhoneStatusRow statusRow = (WOSI.CallButler.Data.CallButlerPhoneStatusDataset.PhoneStatusRow)dgPhones.Rows[e.RowIndex].Tag;

            ManagePhone(statusRow.RemoteAddress);
        }

        private void dgPhones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == colConfigure.Index)
            {
                WOSI.CallButler.Data.CallButlerPhoneStatusDataset.PhoneStatusRow statusRow = (WOSI.CallButler.Data.CallButlerPhoneStatusDataset.PhoneStatusRow)dgPhones.Rows[e.RowIndex].Tag;

                ManagePhone(statusRow.RemoteAddress);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPhoneStatus();
        }
    }
}

