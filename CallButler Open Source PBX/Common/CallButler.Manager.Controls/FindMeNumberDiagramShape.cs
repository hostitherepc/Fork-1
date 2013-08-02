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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CallButler.Manager.Controls
{
    public partial class FindMeNumberDiagramShape : UserControl
    {
        public event EventHandler DeletePressed;
        public event EventHandler MoveUpPressed;
        public event EventHandler MoveDownPressed;

        private bool loading = true;

        private bool expanded = false;
        private bool usingCallBlast = false;
        private bool enableCallIPPhone = true;

        private string numberType = "";

        WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow contactRow;
        WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensions;

        public FindMeNumberDiagramShape(WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensions, WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow contactRow)
        {
            this.contactRow = contactRow;
            this.extensions = extensions;

            InitializeComponent();

            Expanded = false;

            WOSI.Utilities.TimeUtils timeUtils = new WOSI.Utilities.TimeUtils();

            cboTimeZone.DataSource = timeUtils.TimeZones;
            cboTimeZone.SelectedIndex = timeUtils.CurrentTimeZoneIndex;

            numTimeout.Value = contactRow.Timeout;
            cbExtensionHours.Checked = contactRow.HasHoursOfOperation;
            scheduleControl.DeserializeSelection(contactRow.HoursOfOperation);

            // For compatibility with old data model
            if (contactRow.CallPBXPhone)
            {
                contactRow.CallPBXPhone = false;
                SelectIPPhone();
            }
            else
            {
                switch ((WOSI.CallButler.Data.ExtensionContactNumberType)contactRow.Type)
                {
                    case WOSI.CallButler.Data.ExtensionContactNumberType.TelephoneNumber:
                        SelectTelephone();
                        txtContactNumber.Text = contactRow.ContactNumber;
                        break;

                    case WOSI.CallButler.Data.ExtensionContactNumberType.Extension:
                        SelectExtension();

                        // Select the proper extension
                        try
                        {
                            Guid extensionID = new Guid(contactRow.ContactNumber);

                            foreach (global::Controls.ListBoxExItem item in cboExtension.Items)
                            {
                                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = (WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow)item.Tag;

                                if (extension.ExtensionID == extensionID)
                                {
                                    cboExtension.SelectedItem = item;
                                }
                            }
                        }
                        catch
                        {
                        }

                        break;

                    case WOSI.CallButler.Data.ExtensionContactNumberType.IPPhone:
                        SelectIPPhone();
                        txtContactNumber.Text = "";
                        break;
                }
            }

            // Select the proper time zone
            if (!contactRow.IsHoursOfOperationUTCOffsetNull())
            {
                int tzIndex = timeUtils.GetTimeZoneIndexFromStandardOffset(contactRow.HoursOfOperationUTCOffset);

                if (tzIndex >= 0)
                {
                    cboTimeZone.SelectedIndex = tzIndex;
                }
            }

            loading = false;
        }

        public bool Expanded
        {
            get
            {
                return expanded;
            }
            set
            {
                expanded = value;

                if (expanded)
                {
                    pnlDetails.Visible = true;
                    btnEditSettings.Text = Properties.LocalizedStrings.ExtensionContactControl_Done;
                    btnEditSettings.LinkImage = Properties.Resources.navigate_left_16;
                    UpdateSizing();
                    
                    txtContactNumber.Focus();
                }
                else
                {
                    pnlDetails.Visible = false;
                    btnEditSettings.Text = Properties.LocalizedStrings.ExtensionContactControl_ChangeSettings;
                    btnEditSettings.LinkImage = Properties.Resources.navigate_right_16;
                    UpdateSizing();
                }
            }
        }

        public WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow ContactRow
        {
            get
            {
                return contactRow;
            }
        }

        public int Timeout
        {
            get
            {
                return (int)numTimeout.Value;
            }
            set
            {
                numTimeout.Value = value;
            }
        }

        public string ContactNumber
        {
            get
            {
                return txtContactNumber.Text;
            }
            set
            {
                txtContactNumber.Text = value;
            }
        }

        public bool EnableCallIPPhone
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        public string NumberType
        {
            get
            {
                return numberType;
            }
            set
            {
                numberType = value;

                if ((WOSI.CallButler.Data.ExtensionContactNumberType)contactRow.Type == WOSI.CallButler.Data.ExtensionContactNumberType.TelephoneNumber)
                {
                    cboType.Items[0] = value;
                }
            }
        }

        public bool UseHours
        {
            get
            {
                return cbExtensionHours.Checked;
            }
            set
            {
                cbExtensionHours.Checked = value;
            }
        }

        public bool UsingCallBlast
        {
            get
            {
                return usingCallBlast;
            }
            set
            {
                usingCallBlast = value;

                if ((WOSI.CallButler.Data.ExtensionContactNumberType)contactRow.Type != WOSI.CallButler.Data.ExtensionContactNumberType.Extension)
                {
                    pnlTryFor.Visible = !usingCallBlast;
                    lblFor.Visible = !usingCallBlast;
                    lblTimeout.Visible = !usingCallBlast;
                }

                if (usingCallBlast)
                    lblNext.Text = Properties.LocalizedStrings.ExtensionContactControl_And;
                else
                    lblNext.Text = Properties.LocalizedStrings.ExtensionContactControl_NoAnswer2;
            }
        }

        private void btnEditSettings_Click(object sender, EventArgs e)
        {
            this.Expanded = !this.Expanded;
        }

        private void numTimeout_ValueChanged(object sender, EventArgs e)
        {
            lblTimeout.Text = string.Format(Properties.LocalizedStrings.ExtensionContactControl_Seconds, (int)numTimeout.Value);
            contactRow.Timeout = (short)numTimeout.Value;
        }

        private void txtContactNumber_TextChanged(object sender, EventArgs e)
        {
            if ((WOSI.CallButler.Data.ExtensionContactNumberType)contactRow.Type == WOSI.CallButler.Data.ExtensionContactNumberType.TelephoneNumber)
            {
                lblNumber.Text = WOSI.Utilities.StringUtils.FormatPhoneNumber(txtContactNumber.Text);
                contactRow.ContactNumber = txtContactNumber.Text;
            }
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
            cbExtensionHours.Checked = !cbExtensionHours.Checked;
        }

        private void cbExtensionHours_CheckedChanged(object sender, EventArgs e)
        {
            pnlHours.Visible = cbExtensionHours.Checked;
            contactRow.HasHoursOfOperation = cbExtensionHours.Checked;

            UpdateSizing();
        }

        private void scheduleControl_SelectionChanged(object sender, EventArgs e)
        {
            contactRow.HoursOfOperation = scheduleControl.SerializeSelection();
        }

        private void cboTimeZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!loading)
                contactRow.HoursOfOperationUTCOffset = ((WOSI.Utilities.TimeZoneInfo)cboTimeZone.Items[cboTimeZone.SelectedIndex]).GMTStandardOffset;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DeletePressed != null)
                DeletePressed(this, EventArgs.Empty);
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (MoveUpPressed != null)
                MoveUpPressed(this, EventArgs.Empty);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (MoveDownPressed != null)
                MoveDownPressed(this, EventArgs.Empty);
        }

        private void UpdateSizing()
        {
            if (!Expanded)
            {
                this.Height = 147;
            }
            else
            {
                if (cbExtensionHours.Checked)
                {
                    this.Height = 368;
                }
                else
                {
                    this.Height = 232;
                }
            }
        }

        private void SelectTelephone()
        {
            contactRow.Type = (short)WOSI.CallButler.Data.ExtensionContactNumberType.TelephoneNumber;
            cboExtension.Visible = false;
            txtContactNumber.Text = "";
            txtContactNumber.Visible = true;

            lblNumber.Text = "";

            if (!usingCallBlast)
            {
                pnlTryFor.Visible = true;
                lblFor.Visible = true;
                lblTimeout.Visible = true;
            }

            cboType.SelectedIndex = 0;
        }

        private void SelectIPPhone()
        {
            contactRow.Type = (short)WOSI.CallButler.Data.ExtensionContactNumberType.IPPhone;
            contactRow.ContactNumber = "";
            cboExtension.Visible = false;
            txtContactNumber.Visible = false;

            lblNumber.Text = "My IP Phone";

            if (!usingCallBlast)
            {
                pnlTryFor.Visible = true;
                lblFor.Visible = true;
                lblTimeout.Visible = true;
            }

            cboType.SelectedIndex = 1;
        }

        private void SelectExtension()
        {
            // Fill in our extension list
            if (cboExtension.Items.Count == 0)
            {
                foreach (WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension in extensions)
                {
                    if (extension.ExtensionID != contactRow.ExtensionID)
                    {
                        global::Controls.ListBoxExItem item = new global::Controls.ListBoxExItem();

                        item.Text = string.Format("{0} - {1} {2}", extension.ExtensionNumber, extension.FirstName, extension.LastName);
                        item.Tag = extension;

                        cboExtension.Items.Add(item);
                    }
                }
            }

            contactRow.Type = (short)WOSI.CallButler.Data.ExtensionContactNumberType.Extension;
            pnlTryFor.Visible = false;
            cboExtension.Visible = true;
            txtContactNumber.Visible = false;

            lblNumber.Text = "";

            lblFor.Visible = false;
            lblTimeout.Visible = false;

            cboType.SelectedIndex = 2;
        }

        private void cboExtension_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((WOSI.CallButler.Data.ExtensionContactNumberType)contactRow.Type == WOSI.CallButler.Data.ExtensionContactNumberType.Extension && cboExtension.SelectedItem != null)
            {
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = (WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow)((global::Controls.ListBoxExItem)cboExtension.SelectedItem).Tag;

                contactRow.ContactNumber = extension.ExtensionID.ToString();

                lblNumber.Text = cboExtension.SelectedItem.ToString();
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboType.SelectedIndex)
            {
                case 0:
                    SelectTelephone();
                    break;

                case 1:
                    SelectIPPhone();
                    break;

                case 2:
                    SelectExtension();
                    break;
            }
        }
    }
}
