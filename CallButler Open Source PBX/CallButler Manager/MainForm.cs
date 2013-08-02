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
using System.IO;
using System.Net;
using CallButler.Manager.ViewControls;
using CallButler.Manager.Utils;

namespace CallButler.Manager
{
    public partial class MainForm : Form
    {
        private PluginManager pluginManager;

        public MainForm()
        {
            InitializeComponent();

             System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
            Version appVersion = a.GetName().Version;
            string appVersionString = appVersion.ToString();

            if (Properties.Settings.Default.ApplicationVersion != appVersion.ToString())
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.ApplicationVersion = appVersionString;
                Properties.Settings.Default.Save();
            }

            if (Properties.Settings.Default.ManagementInterfaceType == WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceType.Hosted)
            {
                Forms.LoginForm loginForm = new CallButler.Manager.Forms.LoginForm();
                if (loginForm.ShowDialog(this) != DialogResult.OK)
                {
                    Environment.Exit(0);
                }
            }

            RemotingExceptionManager.RemoteManagementError += new EventHandler(RemotingExceptionManager_RemoteManagementError);
            RemotingExceptionManager.ConnectionError += new EventHandler(RemotingExceptionManager_ConnectionError);
            RemotingExceptionManager.AuthenticationError += new EventHandler(RemotingExceptionManager_AuthenticationError);

            WOSI.Utilities.TimeZoneInfo[] tzInfo = WOSI.Utilities.TimeUtils.GetSystemTimeZones();

            // Load our plugins
            PluginManager.LoadPlugins(WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.PluginsPath));
          
            global::Controls.LoadingDialog.HideDialog();

            InitializeUI();

            ProcessLogin(Properties.Settings.Default.CallButlerServer, false);
            this.BringToFront();
        }

        void RemotingExceptionManager_RemoteManagementError(object sender, EventArgs e)
        {
            MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_RemoteManagementError), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_RemoteManagementErrorCaption), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save the data from our current view control
            if (currentViewControl != null)
                currentViewControl.SaveData();

            // Close our connection
            try
            {
                ManagementInterfaceClient.Disconnect();
            }
            catch
            {
            }

            // Clear out our greeting cache
            string greetingsCacheFolder = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache);

            if (WOSI.Utilities.FileUtils.GetDirectorySize(greetingsCacheFolder) >= 100000000)
                Directory.Delete(greetingsCacheFolder, true);

            // Clear out our voicemail cache
            string voicemailCacheFolder = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.VoicemailSoundCache);

            if (WOSI.Utilities.FileUtils.GetDirectorySize(voicemailCacheFolder) >= 100000000)
                Directory.Delete(voicemailCacheFolder, true);

            // Unload our pluings
            PluginManager.UnloadPlugins();

            if (Properties.Settings.Default.DisplayShutdownDialog)
            {
                if (connectionError == false)
                {
                    Forms.ShutownForm shutdownForm = new CallButler.Manager.Forms.ShutownForm();

                    shutdownForm.ShowDialog();
                }
            }
        }

        private void btnExtensions_Click(object sender, EventArgs e)
        {
            LoadViewControl(new ExtensionsView());
        }

        private void btnBackToSummary_Click(object sender, EventArgs e)
        {
            LoadViewControl(new SummaryView());
        }

        private void btnCallFlow_Click(object sender, EventArgs e)
        {
            LoadViewControl(new CallFlowView());
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuConnectOtherComputer_Click(object sender, EventArgs e)
        {
            ProcessLogin(null, true);
            //PromptComputerConnection();
        }

        private void mnuConnectThisComputer_Click(object sender, EventArgs e)
        {
            ProcessLogin("localhost", false);
            //ConnectToCallButlerServer("127.0.0.1", Properties.Settings.Default.TcpManagementPort, Properties.Settings.Default.ManagementPassword);
        }

        private void mnuSettings_Click(object sender, EventArgs e)
        {
            LoadViewControl(new SettingsView());
        }

        private void btnPersonalGreetings_Click(object sender, EventArgs e)
        {
            LoadViewControl(new PersonalizedGreetingView());
        }

        private void btnTestDrive_Click(object sender, EventArgs e)
        {
            LoadViewControl(new TestDriveView());
        }

        private void btnCloseCommonTasks_Click(object sender, EventArgs e)
        {
            ShowCommonTasks(false, true);
        }

        private void mnuShowCommonTasks_Click(object sender, EventArgs e)
        {
            ShowCommonTasks(mnuShowCommonTasks.Checked, true);
        }

        private void btnNewExtension_Click(object sender, EventArgs e)
        {
            ExtensionsView extensionView = new ExtensionsView();

            LoadViewControl(extensionView);

            extensionView.AddNewExtension();
        }

        private void btnAddNewDepartment_Click(object sender, EventArgs e)
        {
            CallFlowView callFlowView = new CallFlowView();

            LoadViewControl(callFlowView);

            callFlowView.AddNewDepartment();
        }

        private void btnAddPersonalExtension_Click(object sender, EventArgs e)
        {
            PersonalizedGreetingView pgView = new PersonalizedGreetingView();

            LoadViewControl(pgView);

            pgView.AddPersonalizedGreeting();
        }

        private void btnProviders_Click(object sender, EventArgs e)
        {
            ProvidersView pView = new ProvidersView();
            LoadViewControl(pView);
        }

        private void mnuRegister_Click(object sender, EventArgs e)
        {
            LicenseRegistration();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            Forms.AboutForm aboutForm = new CallButler.Manager.Forms.AboutForm();

            aboutForm.ShowDialog(this);
        }

        private void mnuExpertMode_Click(object sender, EventArgs e)
        {
            ManagementInterfaceClient.ManagementInterface.ExpertModeEnabled = mnuExpertMode.Checked;
            ChangeUserMode(mnuExpertMode.Checked);
        }

        private void btnScriptSchedule_Click(object sender, EventArgs e)
        {
            LoadViewControl(new ScriptScheduleView());
        }

        private void mnuCallHistory_Click(object sender, EventArgs e)
        {
            LoadViewControl(new CallHistoryView());
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CallButler.Manager.Forms.VersionCheckerForm dlg = new CallButler.Manager.Forms.VersionCheckerForm();
            dlg.ShowDialog(this);
        }

        private void btnDownloadNewVersion_Click(object sender, EventArgs e)
        {
            CallButler.Manager.Forms.VersionCheckerForm dlg = new CallButler.Manager.Forms.VersionCheckerForm();
            dlg.ShowDialog(this);
        }

        private void btnHideUpdateNotification_Click(object sender, EventArgs e)
        {
            pnlNewVersion.Hide();
        }

        private void mnuSupport_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.callbutler.com/support");
        }

        private void picCallButlerLogo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.callbutler.com");
        }

        private void btnPlugins_Click(object sender, EventArgs e)
        {
            LoadViewControl(new PluginView());
            ShowCommonTasks(false, false);
        }

        private void mnuPurchase_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.BuyLicenseURL + ManagementInterfaceClient.ManagementInterface.ProductID);
        }

        private void mnuLogin_Click(object sender, EventArgs e)
        {
            /*if (Properties.Settings.Default.ManagementInterfaceType == WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceType.Hosted)
            {
                Forms.LoginForm loginForm = new CallButler.Manager.Forms.LoginForm();
                if (loginForm.ShowDialog(this) != DialogResult.Cancel)
                {
                    ConnectToCallButlerServer("CallButler Server", Properties.Settings.Default.TcpManagementPort, Properties.Settings.Default.ManagementPassword);
                }
            }*/

            ProcessLogin(null, true);
        }

        private void lblPoweredBy_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.callbutler.com");
        }

        private void mnuExpertMode_CheckedChanged(object sender, EventArgs e)
        {
            if(ManagementInterfaceClient.ManagementInterface.ExpertModeEnabled == false && mnuExpertMode.Checked)
            {
                if (MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_ChangeToExpertMode), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_ChangeToExpertModeCaption), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    mnuExpertMode.Checked = false;
                }
            }
        }

        private void btnGetPhoneNumber_Click(object sender, EventArgs e)
        {
            CallButler.Manager.Plugin.CallButlerManagementProviderPlugin plugin = (CallButler.Manager.Plugin.CallButlerManagementProviderPlugin)PluginManager.GetPluginFromID(new Guid(Properties.Settings.Default.PreferredVoIPProviderPluginID));
            if (plugin != null)
            {
               new ProvidersView().AddNewProvider();
            }
        }

        private void mnuTour_Click(object sender, EventArgs e)
        {
            ShowGettingStartedView(true);
        }

        private void mnuQuickStart_Click(object sender, EventArgs e)
        {
            ShowQuickStartWizard(false, false);
        }

        private void mnuChangeEdition_Click(object sender, EventArgs e)
        {
            if (ChooseEdition(false) == DialogResult.OK)
                ConfigureUI();
        }

        private void mnuImport_Click(object sender, EventArgs e)
        {
            if (openSettingsFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                ImportSettings(openSettingsFileDialog.FileName);
            }
        }

        private void btnPBX_Click(object sender, EventArgs e)
        {
            LoadViewControl(new PBXView());
        }

        private void btnReceptionist_Click(object sender, EventArgs e)
        {
            LoadViewControl(new ReceptionistView());
        }

        private void mnuUserManual_Click(object sender, EventArgs e)
        {
            string userManualPath = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.UserManualPath);

            if (File.Exists(userManualPath))
            {
                System.Windows.Forms.Help.ShowHelp(this, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.UserManualPath));
            }
            else
            {
                MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_UnableToFindUserManual), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void mnuLanguage_Click(object sender, EventArgs e)
        {
            ChangeLanguage(((ToolStripMenuItem)sender).Tag.ToString(), true);
        }

        private void btnAddonModules_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(CallButler.Manager.Properties.Settings.Default.ModulesDownloadURL);
        }
    }
}