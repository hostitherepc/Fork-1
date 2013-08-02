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
    public partial class ReceptionistView : CallButler.Manager.ViewControls.ViewControlBase
    {
        private DataGridViewCheckBoxColumn cbColumn;

        public ReceptionistView()
        {
            InitializeComponent();

            //extensionsView.DataGridView.ReadOnly = false;

            /*foreach (DataGridViewColumn column in extensionsView.DataGridView.Columns)
            {
                column.ReadOnly = true;
            }*/

            // Add a checkbox to the view.
            cbColumn = new DataGridViewCheckBoxColumn();
            cbColumn.HeaderText = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Receptionist_IsReceptionist);
            cbColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            extensionsView.DataGridView.Columns.Insert(0, cbColumn);
            cbColumn.DisplayIndex = 0;

            extensionsView.DataGridView.CellContentClick += new DataGridViewCellEventHandler(DataGridView_CellContentClick);

            extensionsView.DataGridView.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(DataGridView_DataBindingComplete);
        }

        void DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Guid extensionID = ManagementInterfaceClient.ManagementInterface.GetReceptionistExtension(ManagementInterfaceClient.AuthInfo);

            // Select the right receptionist in the view
            foreach (DataGridViewRow row in extensionsView.DataGridView.Rows)
            {
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = (WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow)((DataRowView)row.DataBoundItem).Row;

                if (extension.ExtensionID == extensionID)
                {
                    row.SetValues(true);
                }
                else
                {
                    row.SetValues(false);
                }
            }
        }

        void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == cbColumn.Index)
            {
                // Set all the other checkboxes to false
                foreach (DataGridViewRow row in extensionsView.DataGridView.Rows)
                {
                    if (e.RowIndex != row.Index)
                        row.SetValues(false);
                    else
                    {
                        bool newValue = !(bool)extensionsView.DataGridView[e.ColumnIndex, e.RowIndex].Value;
                        row.SetValues(newValue);
                    }
                }

                if ((bool)extensionsView.DataGridView[e.ColumnIndex, e.RowIndex].Value == true)
                {
                    WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = (WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow)((DataRowView)extensionsView.DataGridView.Rows[e.RowIndex].DataBoundItem).Row;

                    ManagementInterfaceClient.ManagementInterface.SetReceptionistExtension(ManagementInterfaceClient.AuthInfo, extension.ExtensionID);
                }
                else
                {
                    ManagementInterfaceClient.ManagementInterface.SetReceptionistExtension(ManagementInterfaceClient.AuthInfo, Guid.Empty);
                }
            }
        }

        protected override void OnLoadData()
        {
            cbEnableRecep.Checked = ManagementInterfaceClient.ManagementInterface.GetReceptionistEnabled(ManagementInterfaceClient.AuthInfo);
            extensionsView.LoadData();
        }

        private void lblRecep_Click(object sender, EventArgs e)
        {
            cbEnableRecep.Checked = !cbEnableRecep.Checked;
        }

        private void cbEnableRecep_CheckedChanged(object sender, EventArgs e)
        {
            ManagementInterfaceClient.ManagementInterface.SetReceptionistEnabled(ManagementInterfaceClient.AuthInfo, cbEnableRecep.Checked);
        }
    }
}

