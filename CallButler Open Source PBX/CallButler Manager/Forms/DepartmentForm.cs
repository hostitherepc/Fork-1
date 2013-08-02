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
    public partial class DepartmentForm : CallButler.Manager.Forms.EditorWizardFormBase
    {
        private WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow departmentRow;
        bool dgLoaded = false;

        public DepartmentForm(WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow departmentRow, WOSI.CallButler.Data.CallButlerDataset data)
        {
            this.departmentRow = departmentRow;

            InitializeComponent();

            PopulateAddonModuleList();

            this.callButlerDataset = data;
            bsMailboxes.DataSource = this.callButlerDataset;

            wizard.PageIndex = 0;
            wzdDepartmentDetails.PageIndex = 0;
            numOptionNumber.Value = departmentRow.OptionNumber;
            txtDepartmentName.Select();
            txtDepartmentName.Text = departmentRow.Name;

            switch ((WOSI.CallButler.Data.DepartmentTypes)departmentRow.Type)
            {
                case WOSI.CallButler.Data.DepartmentTypes.Greeting:
                    rbPlayMessage.Checked = true;
                    
                    break;
                case WOSI.CallButler.Data.DepartmentTypes.Extension:
                    rbTransferExtension.Checked = true;
                    break;
                case WOSI.CallButler.Data.DepartmentTypes.Number:
                    rbTransferNumber.Checked = true;

                    txtTelephoneNumber.Text = departmentRow.Data1;

                    break;
                case WOSI.CallButler.Data.DepartmentTypes.Script:
                    rbScript.Checked = true;

                    txtScriptFile.Text = departmentRow.Data1;

                    break;

                case WOSI.CallButler.Data.DepartmentTypes.Module:
                    rbAddon.Checked = true;

                    SelectAddonModule(departmentRow.Data1);

                    break;
            }

            UpdateDepartmentTypeView();

            rbTransferNumber.Visible = true;
            rbScript.Visible = true;
            lblScript.Visible = true;
            btnImportOutlook.Enabled = Utilities.ContactManagement.ContactManagerFactory.CreateContactManager(Utilities.ContactManagement.ContactType.Outlook).IsInstalled;

            lblNumberDescription.Text = ManagementInterfaceClient.ManagementInterface.TelephoneNumberDescription;
            lblNumber.Text += lblNumberDescription.Text;

            Utils.PrivateLabelUtils.ReplaceProductNameControl(this);
        }

        private void PopulateAddonModuleList()
        {
            addOnModuleChooserControl.Load();
        }

        private void SelectAddonModule(string data)
        {
            try
            {
                if (data != null && data.Length > 0)
                    addOnModuleChooserControl.SelectedAddOnModule = new Guid(data);
            }
            catch
            {
            }
        }

        public Manager.Controls.GreetingControl GreetingControl
        {
            get
            {
                return greetingControl;
            }
        }

        private void UpdateDepartmentTypeView()
        {
            if (rbPlayMessage.Checked)
            {
                pgSpecificSettings.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.DepartmentForm_MessageSettings);
                wzdDepartmentDetails.PageIndex = 0;
            }
            else if (rbTransferExtension.Checked)
            {
                pgSpecificSettings.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.DepartmentForm_ExtensionSettings);
                wzdDepartmentDetails.PageIndex = 1;
            }
            else if (rbTransferNumber.Checked)
            {
                pgSpecificSettings.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.DepartmentForm_NumberSettings);
                wzdDepartmentDetails.PageIndex = 2;
            }
            else if (rbScript.Checked)
            {
                pgSpecificSettings.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.DepartmentForm_ScriptSettings);
                wzdDepartmentDetails.PageIndex = 3;
            }
            else if (rbAddon.Checked)
            {
                pgSpecificSettings.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.DepartmentForm_AddonModuleSettings);
                wzdDepartmentDetails.PageIndex = 4;
            }
        }
        
        private void rbDepartmentType_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDepartmentTypeView();
        }

        private void wizard_WizardFinished(object sender, EventArgs e)
        {
            // Save our data
            departmentRow.Name = txtDepartmentName.Text;
            departmentRow.OptionNumber = (int)numOptionNumber.Value;

            if (rbPlayMessage.Checked)
            {
                departmentRow.Type = (int)WOSI.CallButler.Data.DepartmentTypes.Greeting;
                departmentRow.SetData1Null();
                departmentRow.SetData2Null();
            }
            else if (rbTransferExtension.Checked)
            {
                departmentRow.Type = (int)WOSI.CallButler.Data.DepartmentTypes.Extension;

                if (extensionsView.SelectedExensionID != Guid.Empty)
                    departmentRow.Data1 = extensionsView.SelectedExensionID.ToString();
                else
                    departmentRow.Data1 = "";
            }
            else if (rbTransferNumber.Checked)
            {
                departmentRow.Type = (int)WOSI.CallButler.Data.DepartmentTypes.Number;

                departmentRow.Data1 = txtTelephoneNumber.Text;
            }
            else if (rbScript.Checked)
            {
                departmentRow.Type = (int)WOSI.CallButler.Data.DepartmentTypes.Script;

                departmentRow.Data1 = txtScriptFile.Text;
            }
            else if (rbAddon.Checked)
            {
                departmentRow.Type = (int)WOSI.CallButler.Data.DepartmentTypes.Module;

                Guid moduleID = addOnModuleChooserControl.SelectedAddOnModule;

                if (moduleID != Guid.Empty)
                {
                    departmentRow.Data1 = moduleID.ToString();
                }
                else
                {
                    departmentRow.Data1 = "";
                }
            }

        }

        private void wizardPage2_ShowFromNext(object sender, EventArgs e)
        {
            dgLoaded = true;
        }

        private void btnImportOutlook_Click(object sender, EventArgs e)
        {
            Forms.OutlookContactForm ocForm = new OutlookContactForm();

            if (ocForm.ShowDialog(this) == DialogResult.OK)
            {
                if(ocForm.SelectedContacts.Length > 0)
                {
                    if (ocForm.SelectedContacts[0].BusinessTelephoneNumber != null)
                    {
                        txtTelephoneNumber.Text = ocForm.SelectedContacts[0].BusinessTelephoneNumber;
                    }
                    else
                    {
                        txtTelephoneNumber.Text = ocForm.SelectedContacts[0].PrimaryTelephoneNumber;
                    }
                }
            }
        }

        private void btnScriptBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                txtScriptFile.Text = openFileDialog.FileName;
        }

        private void lblMessage_Click(object sender, EventArgs e)
        {
            rbPlayMessage.Checked = true;
        }

        private void lblExtension_Click(object sender, EventArgs e)
        {
            rbTransferExtension.Checked = true;
        }

        private void lblNumber_Click(object sender, EventArgs e)
        {
            rbTransferNumber.Checked = true;
        }

        private void lblScript_Click(object sender, EventArgs e)
        {
            rbScript.Checked = true;
        }

        private void wzdDepartmentDetails_PageChanged(object sender, EventArgs e)
        {
            if (wzdDepartmentDetails.Page.Name.Equals("pgExtensionSelector"))
            {
                extensionsView.LoadData();
            }
        }

        private void extensionsView_VisibleChanged(object sender, EventArgs e)
        {
            if (!dgLoaded && extensionsView.Visible)
            {
                if (rbTransferExtension.Checked)
                {
                    try
                    {
                        extensionsView.SelectedExensionID = new Guid(departmentRow.Data1);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void DepartmentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            greetingControl.StopSounds();
        }

        private void lblAddon_Click(object sender, EventArgs e)
        {
            rbAddon.Checked = true;
        }
    }
}

