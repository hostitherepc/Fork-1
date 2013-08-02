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
    public partial class GettingStartedView : CallButler.Manager.ViewControls.ViewControlBase
    {
        public GettingStartedView()
        {
            InitializeComponent();

            wizard.PageIndex = 0;

            // Disable any pages
            pgAboutExtensions.Enabled = true;
            pgExtensions.Enabled = true;
            pgAboutCallFlow.Enabled = true;
            pgCallFlow.Enabled = true;
            pgTestDrive.Enabled = true;
            pgProviders.Enabled = true;
            btnEmailSetup.Visible = true;
        }

        protected override void OnLoadData()
        {
            if (pgExtensions.Enabled)
                extensionsView.LoadData();

            if(pgCallFlow.Enabled)
                callFlowView.LoadData();

            if(pgTestDrive.Enabled)
                testDriveView.LoadData();
        }

        protected override void OnSaveData()
        {
            if(pgTestDrive.Enabled)
                testDriveView.SaveData();

            ManagementInterfaceClient.ManagementInterface.SetFirstTimeRun(ManagementInterfaceClient.AuthInfo, false);
        }

        private void PersistData()
        {
            if(pgExtensions.Enabled)
                extensionsView.SaveData();

            if (pgCallFlow.Enabled)
                callFlowView.SaveData();
        }

        private void wizard_CloseFromCancel(object sender, CancelEventArgs e)
        {
            if (ParentForm is MainForm)
                ((MainForm)ParentForm).ShowGettingStartedView(false);
        }

        private void wizard_WizardFinished(object sender, EventArgs e)
        {
            PersistData();

            if (ParentForm is MainForm)
                ((MainForm)ParentForm).ShowGettingStartedView(false);
        }

        private void btnOtherVoIP_Click(object sender, EventArgs e)
        {
            if (new ProvidersView().AddNewProvider(CallButler.Manager.Forms.ProviderType.Other, false) == DialogResult.OK)
            {
                wizard.PageIndex++;
            }
        }

        private void btnEmailSetup_Click(object sender, EventArgs e)
        {
            Forms.SMTPServerForm smtpForm = new Forms.SMTPServerForm();

            smtpForm.ShowDialog(this);
        }

        private void btnSetupPassword_Click(object sender, EventArgs e)
        {
            Forms.AssignPasswordForm apForm = new CallButler.Manager.Forms.AssignPasswordForm();

            if (apForm.ShowDialog(this) == DialogResult.OK)
            {
                string hashedPassword = "";
                
                if(apForm.Password.Length > 0)
                    hashedPassword = WOSI.Utilities.CryptoUtils.CreateMD5Hash(apForm.Password);
                
                ManagementInterfaceClient.ManagementInterface.SetManagementPassword(ManagementInterfaceClient.AuthInfo, hashedPassword);
                ManagementInterfaceClient.AuthInfo.Password = hashedPassword;

                if (apForm.SavePassword)
                {
                    Properties.Settings.Default.ManagementPassword = hashedPassword;
                    Properties.Settings.Default.Save();
                    ManagementInterfaceClient.Connect(ManagementInterfaceClient.CurrentServer, Properties.Settings.Default.TcpManagementPort, hashedPassword);
                }
            }
        }

        private void btnPreBuilt_Click(object sender, EventArgs e)
        {
            Forms.PrebuiltConfigForm pbForm = new CallButler.Manager.Forms.PrebuiltConfigForm();

            if (pbForm.ShowDialog(this) == DialogResult.OK)
            {
                ManagementInterfaceClient.ManagementInterface.PersistExtension(ManagementInterfaceClient.AuthInfo, pbForm.ConfigurationData.Extensions);
                ManagementInterfaceClient.ManagementInterface.PersistDepartment(ManagementInterfaceClient.AuthInfo, pbForm.ConfigurationData.Departments);
                ManagementInterfaceClient.ManagementInterface.PersistExtensionContactNumbers(ManagementInterfaceClient.AuthInfo, pbForm.ConfigurationData.ExtensionContactNumbers);

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

                LoadData();
            }
        }

        private void btnCallButlerVoIP_Click(object sender, EventArgs e)
        {
            if (new ProvidersView().AddNewProvider(CallButler.Manager.Forms.ProviderType.CallButler, false) == DialogResult.OK)
            {
                wizard.PageIndex++;
            }
        }
    }
}

