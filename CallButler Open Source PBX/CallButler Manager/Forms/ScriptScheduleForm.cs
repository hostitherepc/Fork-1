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
    public partial class ScriptScheduleForm : CallButler.Manager.Forms.EditorWizardFormBase
    {
        WOSI.CallButler.Data.CallButlerDataset.ScriptSchedulesRow scriptSchedule;
        WOSI.Utilities.TimeUtils timeUtils;

        public ScriptScheduleForm(WOSI.CallButler.Data.CallButlerDataset.ScriptSchedulesRow scriptSchedule)
        {
            InitializeComponent();

            this.scriptSchedule = scriptSchedule;

            timeUtils = new WOSI.Utilities.TimeUtils();

            cboTimeZone.DataSource = timeUtils.TimeZones;
            cboTimeZone.SelectedIndex = timeUtils.CurrentTimeZoneIndex;

            // Load our data
            txtName.Text = scriptSchedule.Name;
            txtScriptFile.Text = scriptSchedule.ScriptLocation;
            cbEnabled.Checked = scriptSchedule.Enabled;
            cbHasHours.Checked = scriptSchedule.HasHoursOfOperation;
            scheduleControl.DeserializeSelection(scriptSchedule.HoursOfOperation);

            if (!scriptSchedule.IsHoursOfOperationUTCOffsetNull())
            {
                int tzIndex = timeUtils.GetTimeZoneIndexFromStandardOffset(scriptSchedule.HoursOfOperationUTCOffset);

                if (tzIndex >= 0)
                {
                    cboTimeZone.SelectedIndex = tzIndex;
                }
            }

            wizard.PageIndex = 0;

            txtName.Select();

            Utils.PrivateLabelUtils.ReplaceProductNameControl(this);
        }

        private void UpdateData()
        {
            scriptSchedule.Name = txtName.Text;
            scriptSchedule.ScriptLocation = txtScriptFile.Text;
            scriptSchedule.Enabled = cbEnabled.Checked;
            scriptSchedule.HasHoursOfOperation = cbHasHours.Checked;
            scriptSchedule.HoursOfOperation = scheduleControl.SerializeSelection();
            scriptSchedule.HoursOfOperationUTCOffset = ((WOSI.Utilities.TimeZoneInfo)cboTimeZone.Items[cboTimeZone.SelectedIndex]).GMTStandardOffset;
        }

        private void cbHasHours_CheckedChanged(object sender, EventArgs e)
        {
            pnlSchedule.Enabled = cbHasHours.Checked;
        }

        private void wizard_WizardFinished(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void btnScriptBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                txtScriptFile.Text = openFileDialog.FileName;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            scheduleControl.SelectAll();
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            scheduleControl.SelectNone();
        }

        private void btnSelectInverse_Click(object sender, EventArgs e)
        {
            scheduleControl.SelectInverse();
        }

        private void lblHours_Click(object sender, EventArgs e)
        {
            cbHasHours.Checked = !cbHasHours.Checked;
        }
    }
}

