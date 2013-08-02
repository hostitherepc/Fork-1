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

namespace CallButler.Manager.Forms
{
    public enum QuickStartResult
    {
        Normal,
        ProductTour
    }

    public partial class QuickStartForm : Form
    {
        private QuickStartResult result = QuickStartResult.Normal;

        MainForm mainForm;

        public QuickStartForm(MainForm mainForm, bool firstTime)
        {
            this.mainForm = mainForm;

            InitializeComponent();

            pgTrialNumber.Enabled = false;
            testDriveView.LoadData();

            if (firstTime)
            {
                Wizard.PageIndex = 0;
            }
            else
            {
                Wizard.PageIndex = 2;
            }

            Utils.PrivateLabelUtils.ReplaceProductNameControl(this);
        }

        public QuickStartResult Result
        {
            get
            {
                return result;
            }
        }

        private void Wizard_PageChanged(object sender, EventArgs e)
        {
            btnFinish.Enabled = true;
            btnBack.Enabled = true;
            btnNext.Enabled = true;

            switch (Wizard.PageIndex)
            {
                case 0:

                    btnBack.Enabled = false;
                    btnFinish.Enabled = false;

                    break;

                case 1:

                    btnBack.Enabled = false;

                    break;

                case 2:

                    btnBack.Enabled = false;

                    break;

                case 3:

                    btnNext.Enabled = false;

                    break;
            }
        }

        private void Wizard_BeforePageChanged(object sender, global::Controls.Wizard.PageChangedEventArgs e)
        {
            switch (Wizard.PageIndex)
            {
                case 0:
                    
                    // Validate our Email address
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                    {
                        MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_EnterValidEmail), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_InvalidEmail), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEmail.Clear();
                        txtEmail.Select();
                        e.Cancel = true;
                        return;
                    }

                    // Validate the user name
                    if (txtName.Text.Length == 0)
                    {
                        MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_NameRequired), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtName.Select();
                        e.Cancel = true;
                        return;
                    }

                    // Validate the user name
                    if (txtPhone.Text.Length == 0)
                    {
                        MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_PhoneNumberRequired), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPhone.Select();
                        e.Cancel = true;
                        return;
                    }

                    // Validate the country
                    if (cboCountry.SelectedIndex == 0)
                    {
                        MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_SelectCountry), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        return;
                    }

                    /*if (txtEmail.Text != "test@test.com")
                    {
                        // Send the registration to our webservice
                        RegistrationServices.RegistrationService regService = new CallButler.Manager.RegistrationServices.RegistrationService();

                        //Controls.LoadingDialog.ShowDialog(this, Properties.LocalizedStrings.Common_SendingData, Properties.Resources.loading, false, 2000);

                        try
                        {
                            regService.Register2(txtName.Text, txtEmail.Text, txtCompanyName.Text, ManagementInterfaceClient.ManagementInterface.ProductID, true, cboCountry.Text, txtPhone.Text);
                        }
                        catch(Exception exp)
                        {
                        }

                        //Controls.LoadingDialog.HideDialog();
                    }*/

                    ManagementInterfaceClient.ManagementInterface.IsDownloadRegistered = true;

                    // Create a new default extension if one doesn't exist
                    WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensionDataTable = ManagementInterfaceClient.ManagementInterface.GetExtensionNumber(ManagementInterfaceClient.AuthInfo, 100);

                    if (extensionDataTable == null || extensionDataTable.Count == 0)
                    {
                        // Create a new Extension row
                        extensionDataTable = new WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable();
                        WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = extensionDataTable.NewExtensionsRow();

                        extension.CustomerID = Properties.Settings.Default.CustomerID;
                        extension.ExtensionID = Guid.NewGuid();
                        extension.EmailAddress = txtEmail.Text;
                        extension.EmailAttachment = true;
                        extension.EmailNotification = true;
                        extension.EnableSearch = true;
                        extension.ExtensionNumber = 100;

                        string[] name = txtName.Text.Split(' ');
                        extension.FirstName = name[0];
                        if(name.Length > 1)
                        {
                            for (int index = 1; index < name.Length; index++)
                                extension.LastName += name[index] + " ";
                        }

                        extensionDataTable.AddExtensionsRow(extension);

                        // Persist our extension
                        ManagementInterfaceClient.ManagementInterface.PersistExtension(ManagementInterfaceClient.AuthInfo, extensionDataTable);

                        // Create a new extension contact numbers table
                        WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable extensionContactNumbers = new WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable();

                        WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow contactNumber = extensionContactNumbers.NewExtensionContactNumbersRow();
                        contactNumber.ContactNumber = txtPhone.Text;
                        contactNumber.ExtensionContactNumberID = Guid.NewGuid();
                        contactNumber.ExtensionID = extension.ExtensionID;
                        contactNumber.Timeout = 20;

                        extensionContactNumbers.AddExtensionContactNumbersRow(contactNumber);

                        // Persist our contact numbers
                        ManagementInterfaceClient.ManagementInterface.PersistExtensionContactNumbers(ManagementInterfaceClient.AuthInfo, extensionContactNumbers);
                    }

                    break;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(Wizard.PageIndex < Wizard.Pages.Count - 1)
                Wizard.Next();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Wizard.Back();
        }

        private void QuickStartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            testDriveView.SaveData();
        }

        private void btnPrivacy_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.callbutler.com/privacy.aspx");
        }

        private void btnStartUsing_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHeadStart_Click(object sender, EventArgs e)
        {
            Forms.PrebuiltConfigForm pbForm = new CallButler.Manager.Forms.PrebuiltConfigForm();

            if (pbForm.ShowDialog(this) == DialogResult.OK)
            {
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensions = new WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable();
                extensions.Merge(pbForm.ConfigurationData.Extensions);
                ManagementInterfaceClient.ManagementInterface.PersistExtension(ManagementInterfaceClient.AuthInfo, extensions);

                WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable contactNumbers = new WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable();
                contactNumbers.Merge(pbForm.ConfigurationData.ExtensionContactNumbers);
                ManagementInterfaceClient.ManagementInterface.PersistExtensionContactNumbers(ManagementInterfaceClient.AuthInfo, contactNumbers);

                ManagementInterfaceClient.ManagementInterface.PersistDepartment(ManagementInterfaceClient.AuthInfo, pbForm.ConfigurationData.Departments);

                WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable lgTable = new WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable();

                // Persist our greeting sounds
                foreach (WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow lgRow in pbForm.ConfigurationData.LocalizedGreetings)
                {
                    lgTable.Clear();
                    lgTable.ImportRow(lgRow);
                    ManagementInterfaceClient.ManagementInterface.PersistLocalizedGreeting(ManagementInterfaceClient.AuthInfo, lgTable);
                    Utils.GreetingUtils.PersistLocalizedGreetingSound(lgRow);
                }
                
                ManagementInterfaceClient.ManagementInterface.PersistPersonalizedGreeting(ManagementInterfaceClient.AuthInfo, pbForm.ConfigurationData.PersonalizedGreetings);
                ManagementInterfaceClient.ManagementInterface.PersistProviders(ManagementInterfaceClient.AuthInfo, pbForm.ConfigurationData.Providers);
                ManagementInterfaceClient.ManagementInterface.PersistScriptSchedule(ManagementInterfaceClient.AuthInfo, pbForm.ConfigurationData.ScriptSchedules);

                // Send us back to the try it now page
                Wizard.NextTo(pgTest);
            }
        }

        private void btnTour_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.result = QuickStartResult.ProductTour;
            this.Close();
        }
    }
}
