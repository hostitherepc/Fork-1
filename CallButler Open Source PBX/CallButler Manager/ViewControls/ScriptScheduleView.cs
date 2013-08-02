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
    public partial class ScriptScheduleView : CallButler.Manager.ViewControls.ViewControlBase
    {
        public ScriptScheduleView()
        {
            InitializeComponent();

            this.HelpRTFText = Properties.Resources.ScriptScheduleHelp;
        }

        protected override void OnLoadData()
        {
            callButlerDataset.ScriptSchedules.Merge(ManagementInterfaceClient.ManagementInterface.GetScriptSchedules(ManagementInterfaceClient.AuthInfo));
            callButlerDataset.AcceptChanges();

            bsScriptSchedules.Sort = "Priority ASC";
        }

        public void AddNewScriptSchedule()
        {
            // Create a new row and table
            WOSI.CallButler.Data.CallButlerDataset.ScriptSchedulesRow scriptSchedule = callButlerDataset.ScriptSchedules.NewScriptSchedulesRow();

            scriptSchedule.CustomerID = Properties.Settings.Default.CustomerID;
            scriptSchedule.ScriptScheduleID = Guid.NewGuid();
            scriptSchedule.Name = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ScriptScheduleView_NewSchedule);

            Forms.ScriptScheduleForm ssForm = new CallButler.Manager.Forms.ScriptScheduleForm(scriptSchedule);

            if (ssForm.ShowDialog(this) == DialogResult.OK)
            {
                callButlerDataset.ScriptSchedules.AddScriptSchedulesRow(scriptSchedule);

                // Add remotely
                ManagementInterfaceClient.ManagementInterface.PersistScriptSchedule(ManagementInterfaceClient.AuthInfo, Utils.TableUtils<WOSI.CallButler.Data.CallButlerDataset.ScriptSchedulesDataTable>.CreateTableFromRow(scriptSchedule));
            }
        }

        public void EditScriptSchedule(WOSI.CallButler.Data.CallButlerDataset.ScriptSchedulesRow scriptSchedule)
        {
            Forms.ScriptScheduleForm ssForm = new CallButler.Manager.Forms.ScriptScheduleForm(scriptSchedule);

            if (ssForm.ShowDialog(this) == DialogResult.OK)
            {
                // Edit remotely
                ManagementInterfaceClient.ManagementInterface.PersistScriptSchedule(ManagementInterfaceClient.AuthInfo, Utils.TableUtils<WOSI.CallButler.Data.CallButlerDataset.ScriptSchedulesDataTable>.CreateTableFromRow(scriptSchedule));
            }
        }

        public void DeleteScriptSchedule(WOSI.CallButler.Data.CallButlerDataset.ScriptSchedulesRow schedule)
        {
            if (MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ScriptScheduleView_ConfirmDelete), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_ConfirmDelete), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Delete the remote extension
                ManagementInterfaceClient.ManagementInterface.DeleteScriptSchedule(ManagementInterfaceClient.AuthInfo, schedule.ScriptScheduleID);

                // Delete the local copy
                schedule.Delete();
            }
        }

        private void btnAddNewScriptSchedule_Click(object sender, EventArgs e)
        {
            AddNewScriptSchedule();
        }

        private void dgScriptSchedule_DeleteDataRow(object sender, CallButler.Manager.Controls.DataRowEventArgs e)
        {
            DeleteScriptSchedule((WOSI.CallButler.Data.CallButlerDataset.ScriptSchedulesRow)e.DataRow);
        }

        private void dgScriptSchedule_EditDataRow(object sender, CallButler.Manager.Controls.DataRowEventArgs e)
        {
            EditScriptSchedule((WOSI.CallButler.Data.CallButlerDataset.ScriptSchedulesRow)e.DataRow);
        }

        private void dgScriptSchedule_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == enabledDataGridViewCheckBoxColumn.Index)
            {
                if ((bool)e.Value)
                    e.Value = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_Yes);
                else
                    e.Value = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_No);
            }
        }
    }
}

