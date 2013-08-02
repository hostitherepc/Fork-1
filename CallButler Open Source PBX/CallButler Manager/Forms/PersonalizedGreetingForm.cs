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
    public partial class PersonalizedGreetingForm : CallButler.Manager.Forms.EditorWizardFormBase
    {
        private bool extensionSelected = false;

        WOSI.CallButler.Data.CallButlerDataset.PersonalizedGreetingsRow personalizedGreeting;

        public PersonalizedGreetingForm(WOSI.CallButler.Data.CallButlerDataset.PersonalizedGreetingsRow personalizedGreeting, WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensions)
        {
            InitializeComponent();

            this.personalizedGreeting = personalizedGreeting;

            LoadData();

            wizard.PageIndex = 0;

            txtCallerID.Select();

            rbScript.Visible = true;
            lblScript.Visible = true;
            btnImportOutlook.Enabled = Utilities.ContactManagement.ContactManagerFactory.CreateContactManager(Utilities.ContactManagement.ContactType.Outlook).IsInstalled;

            Utils.PrivateLabelUtils.ReplaceProductNameControl(this);
        }

        public Manager.Controls.GreetingControl GreetingControl
        {
            get
            {
                return greetingControl;
            }
        }

        private void LoadData()
        {
            txtCallerID.Text = personalizedGreeting.CallerDisplayName;
            txtTelephoneNumber.Text = personalizedGreeting.CallerUsername;
            cbPlayOnce.Checked = personalizedGreeting.PlayOnce;
            txtNotes.Text = personalizedGreeting.Notes;
            cbRegex.Checked = personalizedGreeting.UseRegex;
            txtDialedNumber.Text = personalizedGreeting.DialedUsername;

            addOnModuleChooserControl.Load();
            extensionsView.LoadData();

            switch (personalizedGreeting.Type)
            {
                case (short)WOSI.CallButler.Data.PersonalizedGreetingType.Continue:
                    rbContinue.Checked = true;
                    break;
                case (short)WOSI.CallButler.Data.PersonalizedGreetingType.Hangup:
                    rbHangup.Checked = true;
                    break;
                case (short)WOSI.CallButler.Data.PersonalizedGreetingType.SendToExtension:
                    rbExtension.Checked = true;
                    break;
                case (short)WOSI.CallButler.Data.PersonalizedGreetingType.CustomScript:
                    rbScript.Checked = true;
                    txtScriptFile.Text = personalizedGreeting.Data;
                    break;
                case (short)WOSI.CallButler.Data.PersonalizedGreetingType.Module:
                    rbModule.Checked = true;

                    try
                    {
                        if (personalizedGreeting.Data != null && personalizedGreeting.Data.Length > 0)
                            addOnModuleChooserControl.SelectedAddOnModule = new Guid(personalizedGreeting.Data);
                    }
                    catch
                    {
                    }

                    break;
            }
        }

        private void UpdateData()
        {
            personalizedGreeting.CustomerID = Properties.Settings.Default.CustomerID;
            personalizedGreeting.CallerDisplayName = txtCallerID.Text;
            personalizedGreeting.CallerUsername = txtTelephoneNumber.Text;
            personalizedGreeting.DialedUsername = txtDialedNumber.Text;

            personalizedGreeting.PlayOnce = cbPlayOnce.Checked;
            if (!personalizedGreeting.PlayOnce)
                personalizedGreeting.HasPlayed = false;

            personalizedGreeting.Notes = txtNotes.Text;
            personalizedGreeting.Data = "";
            personalizedGreeting.UseRegex = cbRegex.Checked;

            if (rbContinue.Checked)
                personalizedGreeting.Type = (short)WOSI.CallButler.Data.PersonalizedGreetingType.Continue;
            else if (rbHangup.Checked)
                personalizedGreeting.Type = (short)WOSI.CallButler.Data.PersonalizedGreetingType.Hangup;
            else if (rbExtension.Checked)
            {
                personalizedGreeting.Type = (short)WOSI.CallButler.Data.PersonalizedGreetingType.SendToExtension;
                personalizedGreeting.Data = "";

                if (extensionsView.SelectedExensionID != Guid.Empty)
                {
                    personalizedGreeting.Data = extensionsView.SelectedExensionID.ToString();
                }
                else
                {
                    personalizedGreeting.Data = "";
                }
            }
            else if (rbScript.Checked)
            {
                personalizedGreeting.Type = (short)WOSI.CallButler.Data.PersonalizedGreetingType.CustomScript;
                personalizedGreeting.Data = txtScriptFile.Text;
            }
            else if (rbModule.Checked)
            {
                personalizedGreeting.Type = (short)WOSI.CallButler.Data.PersonalizedGreetingType.Module;

                Guid moduleID = addOnModuleChooserControl.SelectedAddOnModule;

                if (moduleID == Guid.Empty)
                    personalizedGreeting.Data = "";
                else
                    personalizedGreeting.Data = moduleID.ToString();
            }
        }

        private void wizard_WizardFinished(object sender, global::Controls.Wizard.PageChangedEventArgs e)
        {
            UpdateData();
        }

        private void btnImportOutlook_Click(object sender, EventArgs e)
        {
            OutlookContactForm ocForm = new OutlookContactForm();

            ocForm.MultiSelect = false;

            if (ocForm.ShowDialog(this) == DialogResult.OK)
            {
                if (ocForm.SelectedContacts.Length > 0)
                {
                    txtTelephoneNumber.Text = ocForm.SelectedContacts[0].BusinessTelephoneNumber;
                }
            }
        }

        private void btnScriptBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                txtScriptFile.Text = openFileDialog.FileName;
        }

        private void Type_CheckedChanged(object sender, EventArgs e)
        {
            if (rbExtension.Checked)
            {
                wizType.PageIndex = 1;
                wizType.Visible = true;
            }
            else if (rbScript.Checked)
            {
                wizType.PageIndex = 0;
                wizType.Visible = true;
            }
            else if (rbModule.Checked)
            {
                try
                {
                    if (personalizedGreeting.Data != null && personalizedGreeting.Data.Length > 0)
                        addOnModuleChooserControl.SelectedAddOnModule = new Guid(personalizedGreeting.Data);
                }
                catch
                {
                }

                wizType.PageIndex = 2;
                wizType.Visible = true;
            }
            else
            {
                wizType.Visible = false;
            }
        }

        private void wizard_PageChanged(object sender, EventArgs e)
        {
            if (wizard.PageIndex == 2 && rbExtension.Checked)
            {
                if (!extensionSelected)
                {
                    extensionSelected = true;

                    // Select our extension row
                    try
                    {
                        Guid extID = new Guid(personalizedGreeting.Data);

                        extensionsView.SelectedExensionID = extID;
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void PersonalizedGreetingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            greetingControl.StopSounds();
        }

        private void lblRegex_Click(object sender, EventArgs e)
        {
            cbRegex.Checked = !cbRegex.Checked;
        }

        private void lblOneTime_Click(object sender, EventArgs e)
        {
            cbPlayOnce.Checked = !cbPlayOnce.Checked;
        }

        private void lblContinueCall_Click(object sender, EventArgs e)
        {
            rbContinue.Checked = true;
        }

        private void lblTransfer_Click(object sender, EventArgs e)
        {
            rbExtension.Checked = true;
        }

        private void lblHangUp_Click(object sender, EventArgs e)
        {
            rbHangup.Checked = true;
        }

        private void lblScript_Click(object sender, EventArgs e)
        {
            rbScript.Checked = true;
        }

        private void lblModule_Click(object sender, EventArgs e)
        {
            rbModule.Checked = true;
        }
    }
}

