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
using CallButler.Manager.ViewControls;
using System.IO;

namespace CallButler.Manager
{
    public partial class MainForm : Form
    {
        private enum ConnectionResponse
        {
            Connected,
            UnableToConnect,
            FailedToAuthenticate,
            RemoteManagementNotAllowed
        }

        private class LoginInfo
        {
            public string Server;
            public int Port = 0;
            public string Password = "";
            public bool SavePassword = false;
        }

        private ViewControlBase currentViewControl = null;
        private bool connectionError = false;

        private List<global::Controls.LinkButton> pluginToolbarItems;

        public void ProcessCommandArgs(string[] args)
        {
            if (args.Length > 0)
            {
                ImportSettings(args[0]);
            }
        }

        private void ChangeLanguage(string languageID, bool reloadForm)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(languageID);

            Properties.Settings.Default.UICulture = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            Properties.Settings.Default.Save();

            if (reloadForm)
            {
                this.SuspendLayout();

                foreach (Control control in this.Controls)
                {
                    control.Dispose();
                }

                this.Controls.Clear();

                InitializeComponent();

                InitializeUI();
                ConfigureUI();
            }

            foreach (ToolStripMenuItem menu in mnuLanguage.DropDownItems)
            {
                if (menu.Tag is string && (string)menu.Tag == languageID)
                    menu.Checked = true;
                else
                    menu.Checked = false;
            }
        }

        public void ImportSettings(string filename)
        {
            if (File.Exists(filename))
            {
                if (Path.GetExtension(filename).ToLower() == ".cbps")
                {
                    LoadProviderConfiguration(filename);
                }
            }
        }

        #region Configuration File Loaders
        private void LoadProviderConfiguration(string filename)
        {
            WOSI.CallButler.Data.CallButlerDataset cbds = new WOSI.CallButler.Data.CallButlerDataset();

            try
            {
                cbds.ReadXml(filename);
            }
            catch
            {
                MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Configruation_UnableToLoad), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            ManagementInterfaceClient.ManagementInterface.PersistProviders(ManagementInterfaceClient.AuthInfo, cbds.Providers);

            LoadViewControl(new ProvidersView());

            MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Configruation_ProviderSettingsLoaded), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        public void LoadViewControl(ViewControlBase viewControl)
        {
            connectionError = false;

            ShowCommonTasks(Properties.Settings.Default.DisplayCommonTasks, false);

            // Save the data from our current view control
            if (currentViewControl != null)
            {
                lblLoadStatus.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_SavingChanges);
                lblLoadStatus.Visible = true;
                if (!currentViewControl.SaveData())
                    return;

                lblLoadStatus.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_ChangesSaved);

                currentViewControl.Dispose();
                currentViewControl = null;
            }

            lblLoadStatus.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_LoadingData);
            if (!viewControl.LoadData())
                return;

            currentViewControl = viewControl;

            // Remove the view from the screen
            pnlContent.Controls.Clear();

            Utils.PrivateLabelUtils.ReplaceProductNameControl(viewControl);

            // Load the new control
            lblViewTitle.Text = viewControl.HeaderTitle;
            lblHeaderCaption.Text = viewControl.HeaderCaption;
            picHeaderIcon.Image = viewControl.HeaderIcon;          

            lblLoadStatus.Visible = false;

            viewControl.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(viewControl);
            viewControl.Show();

            if (viewControl is SummaryView)
                btnBackToSummary.Visible = false;
            else
                btnBackToSummary.Visible = true;
        }

        private void LoadSkin()
        {
            try
            {
                // Get our private label resource file
                byte[] privateLabelData = ManagementInterfaceClient.ManagementInterface.PrivateLabelData;

                if (privateLabelData != null)
                {
                    WOSI.Utilities.EncryptedResource er = new WOSI.Utilities.EncryptedResource();

                    try
                    {
                        er.LoadResourceBytes(privateLabelData);

                        Image mainLogo = er.GetResource<Image>("Main Logo");

                        if (mainLogo != null)
                        {
                            picCallButlerLogo.Image = mainLogo;
                            picCallButlerLogo.Left += er.GetResource<int>("Main Logo Offset X");
                            picCallButlerLogo.Top += er.GetResource<int>("Main Logo Offset Y");
                        }

                        string productName = er.GetResource<string>("Product Name");

                        if (productName != null && productName.Length > 0)
                        {
                            Utils.PrivateLabelUtils.ProductName = productName;
                        }

                        Utils.PrivateLabelUtils.ReplaceProductNameControl(this);
                        Utils.PrivateLabelUtils.ReplaceProductNameControl(mnuMain);
                    }
                    catch(Exception e)
                    {
                    }
                }
                // Check to see if the skin resource file exists
                /*string skinFile = WOSI.Utilities.FileUtils.GetApplicationRelativePath("\\plskin.dat");

                if (File.Exists(skinFile))
                {
                    lblPoweredBy.Visible = true;

                    // Open the skin file
                    byte[] encryptedBytes = File.ReadAllBytes(skinFile);

                    // Decrypt the bytes
                    byte[] decryptedBytes = WOSI.Utilities.CryptoUtils.Decrypt(encryptedBytes, "asdlfa9sd879*Lasldflkajsdf243o8729");

                    MemoryStream memStream = new MemoryStream(decryptedBytes);

                    System.Resources.ResourceReader rr = new System.Resources.ResourceReader(memStream);

                    System.Collections.IDictionaryEnumerator resEnum = rr.GetEnumerator();

                    while (resEnum.MoveNext())
                    {
                        string resourceID = (string)resEnum.Key;

                        if(resourceID == "Main Logo")
                        {
                            picCallButlerLogo.Image = (Image)resEnum.Value;
                        }
                        else if (resourceID == "Main Logo Offset X")
                        {
                            picCallButlerLogo.Left += (int)resEnum.Value;
                        }
                        else if (resourceID == "Main Logo Offset Y")
                        {
                            picCallButlerLogo.Top += (int)resEnum.Value;
                        }
                    }

                    memStream.Close();
                    memStream.Dispose();
                    rr.Close();
                }*/
            }
            catch
            {
            }
        }

        public void ShowCommonTasks(bool show, bool saveState)
        {
            //if (pnlLeftOuter.Visible != show)
            //{
                pnlLeftOuter.Visible = show;
                mnuShowCommonTasks.Checked = show;
            //}

            if (saveState)
            {
                Properties.Settings.Default.DisplayCommonTasks = show;
                Properties.Settings.Default.Save();
            }
        }

        public void ShowNoConnectionView()
        {
            connectionError = true;

            NoConnectionView ncView = new NoConnectionView();

            currentViewControl = null;
            
            pnlContent.Controls.Clear();

            lblViewTitle.Text = ncView.HeaderTitle;
            lblHeaderCaption.Text = ncView.HeaderCaption;
            picHeaderIcon.Image = ncView.HeaderIcon;

            ncView.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(ncView);
            ncView.Show();

            pnlExpired.Visible = false;
            pnlNewVersion.Visible = false;

            lblConnectionStatus.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_NoConnection);
        }

        public void ShowGettingStartedView(bool show)
        {
            if (show)
            {
                pnlHeaderButtonContainer.Visible = false;
                LoadViewControl(new GettingStartedView());
                ShowCommonTasks(false, false);
                btnBackToSummary.Visible = false;
                mnuEdit.Enabled = false;
                mnuView.Enabled = false;
            }
            else
            {
                pnlHeaderButtonContainer.Visible = true;
                LoadViewControl(new SummaryView());
                ShowCommonTasks(Properties.Settings.Default.DisplayCommonTasks, false);
                mnuEdit.Enabled = true;
                mnuView.Enabled = true;
            }
        }

        private void LoadApplicationPermissions()
        {
            // Load our application permissions from the server
            //Licensing.Management.AppPermissions.LoadPermissionData(ManagementInterfaceClient.ManagementInterface.ProductID, ManagementInterfaceClient.ManagementInterface.GetApplicationPermissions(ManagementInterfaceClient.AuthInfo), Program.AdminMode);

            // Update our UI
            mnuExpertMode.Visible = true;


            if (Properties.Settings.Default.ManagementInterfaceType == WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceType.Hosted)
            {
                mnuConnect.Visible = false;
                mnuLogin.Visible = true;
            }
            else
            {
                mnuConnect.Visible = true;
                mnuLogin.Visible = false;
            }

            mnuConnectThisComputer.Visible = true;


            if (mnuSettings.Visible && mnuExpertMode.Visible)
                mnuExpertModeDiv.Visible = true;
            else
                mnuExpertModeDiv.Visible = false;

            mnuQuickStart.Visible = true;

            bool visible = false;

            visible = true;
            mnuSettings.Visible = visible;
            btnManageGeneralSettings.Visible = visible;

            visible = true;
            mnuExtensions.Visible = visible;
            divExtensions.Visible = visible;
            btnExtensions.Visible = visible;
            btnNewExtension.Visible = visible;

            visible = true;
            mnuCallFlow.Visible = visible;
            divCallFlow.Visible = visible;
            btnCallFlow.Visible = visible;
            btnAddNewDepartment.Visible = true;

            visible = true;
            mnuCallPersonalization.Visible = visible;
            divCallPersonalization.Visible = visible;
            btnCallPersonalization.Visible = visible;
            btnAddCallPersonalization.Visible = visible;

            visible = true;
            mnuTestDrive.Visible = visible;
            divTestDrive.Visible = visible;
            btnTestDrive.Visible = visible;

            visible = true;
            mnuVoIPProviders.Visible = visible;
            divProviders.Visible = visible;
            btnProviders.Visible = visible;

            visible = true;
            mnuRegister.Visible = visible;

            visible = true;
            btnGetPhoneNumber.Visible = visible;

            visible = true;
            mnuChangeEdition.Visible = visible;
        }

        /*private void ConnectToCallButlerServer(string server, int port, string password)
        {
            Application.DoEvents();

            global::Controls.LoadingDialog.ShowDialog(null, String.Format(Properties.LocalizedStrings.MainForm_ConnectingTo, server), Properties.Resources.loading, false, 0);

            try
            {
                pnlNewVersion.Visible = false;

                currentServer = server;
                currentPort = port;

                // Try Connecting
                if (!TryConnection(server, port, password, 0))
                {
                    // If we can't connect, see if the service is installed
                    string cServer = server;
                    if (server.Trim().ToLower().Equals("localhost"))
                    {
                        cServer = "127.0.0.1";
                    }

                    System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController("CallButler Service", cServer);

                    try
                    {
                        if (sc.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
                        {
                            if (MessageBox.Show(Properties.LocalizedStrings.MainForm_ServiceNotRunning, Properties.LocalizedStrings.MainForm_CallButlerService, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.No)
                            {
                                ShowNoConnectionView();
                                global::Controls.LoadingDialog.HideDialog();
                                return;
                            }

                            global::Controls.LoadingDialog.ShowDialog(null, Properties.LocalizedStrings.MainForm_StartingService, Properties.Resources.loading, false, 0);

                            // Start the service
                            sc.Start();
                            sc.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                        }
                    }
                    catch
                    {
                    }

                    // Try connecting again
                    if (!TryConnection(server, port, password, 5))
                    {
                        // If it still doesnt work, try launching the app on the desktop
                        string cbServicePath = WOSI.Utilities.FileUtils.GetApplicationRelativePath("") + "\\..\\Service\\CallButler Service.exe";

                        if (System.IO.File.Exists(cbServicePath) && MessageBox.Show(Properties.LocalizedStrings.MainForm_ServiceNotRunning, Properties.LocalizedStrings.MainForm_CallButlerService, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
                        {
                            global::Controls.LoadingDialog.ShowDialog(null, String.Format(Properties.LocalizedStrings.MainForm_WaitingForService, server), Properties.Resources.loading, false, 0);
                            System.Diagnostics.Process.Start(cbServicePath, "-a");

                            // Try again for 20 seconds
                            if (!TryConnection(server, port, password, 20))
                            {
                                // If we get here, then all has failed.
                                ShowNoConnectionView();
                                global::Controls.LoadingDialog.HideDialog();
                                return;
                            }
                        }
                        else
                        {
                            ShowNoConnectionView();
                            global::Controls.LoadingDialog.HideDialog();
                            return;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                global::Controls.LoadingDialog.HideDialog();
                RemotingExceptionManager.ProcessException(e);
                return;
            }


            ConfigureUI();

            lblConnectionStatus.Text = String.Format(Properties.LocalizedStrings.MainForm_ConnectedTo, server);

            Properties.Settings.Default.CallButlerServer = server;
            Properties.Settings.Default.TcpManagementPort = port;
            Properties.Settings.Default.Save();

            global::Controls.LoadingDialog.HideDialog();
        }*/

        private void InitializeUI()
        {
            ShowCommonTasks(Properties.Settings.Default.DisplayCommonTasks, false);

            // Check to see if we have any plugins that require a separate view
            btnPlugins.Visible = false;
            divPlugins.Visible = false;
            mnuPlugins.Visible = false;

            if (pluginToolbarItems == null)
                pluginToolbarItems = new List<global::Controls.LinkButton>();
            else
            {
                foreach (global::Controls.LinkButton buttonItem in pluginToolbarItems)
                {
                    buttonItem.Parent.Controls.Remove(buttonItem);
                    buttonItem.Dispose();
                }
            }

            foreach (CallButler.Manager.Plugin.CallButlerManagementPlugin plugin in PluginManager.Plugins)
            {
                if (plugin is CallButler.Manager.Plugin.CallButlerManagementAddonModulePlugin)
                {
                    /*if (((CallButler.Manager.Plugin.CallButlerManagementAddonModulePlugin)plugin).ShowInPluginView)
                    {
                        btnPlugins.Visible = true;
                        divPlugins.Visible = true;
                        mnuPlugins.Visible = true;

                        break;
                    }*/

                    if (plugin.ShowInToolbar)
                    {
                        global::Controls.LinkButton buttonItem = new global::Controls.LinkButton();

                        buttonItem.Cursor = btnExtensions.Cursor;
                        buttonItem.Text = plugin.PluginName;
                        buttonItem.ForeColor = btnExtensions.ForeColor;
                        buttonItem.Dock = DockStyle.Right;
                        buttonItem.AutoSize = true;
                        buttonItem.AntiAliasText = false;
                        buttonItem.UnderlineOnHover = true;
                        buttonItem.Tag = plugin;
                        buttonItem.Click += new EventHandler(pluginButtonItem_Click);

                        pnlHeaderButtonContainer.Controls.Add(buttonItem);

                        //pnlHeaderButtonContainer.Controls.SetChildIndex(buttonItem, 0);

                    }
                }
            }
        }

        void pluginButtonItem_Click(object sender, EventArgs e)
        {
            if (((global::Controls.LinkButton)sender).Tag is CallButler.Manager.Plugin.CallButlerManagementPlugin)
            {
                CallButler.Manager.Plugin.CallButlerManagementPluginViewControl pluginViewControl = ((CallButler.Manager.Plugin.CallButlerManagementPlugin)((global::Controls.LinkButton)sender).Tag).GetNewViewControl();

                if (pluginViewControl != null)
                {
                    ViewControlBase viewControl = new ViewControlBase();
                    pluginViewControl.Dock = DockStyle.Fill;
                    viewControl.Controls.Add(pluginViewControl);
                    pluginViewControl.BringToFront();
                    
                    LoadViewControl(viewControl);
                }
            }
        }

        private void ConfigureUI()
        {
            pnlNewVersion.Visible = false;

            try
            {
                LoadSkin();

                // Check to see if we've already choosen a version of CB to run
                if (ManagementInterfaceClient.ManagementInterface.ProductID == null || ManagementInterfaceClient.ManagementInterface.ProductID.Length == 0)
                {
                    ChooseEdition(true);
                }

                /*if (Properties.Settings.Default.UpdateNotification && Utils.NotificationUtils.IsNewVersionAvailable(ManagementInterfaceClient.ManagementInterface.ProductID, Application.ProductVersion))
                {
                    pnlNewVersion.Visible = true;
                }*/

                LoadApplicationPermissions();

                this.Text = ManagementInterfaceClient.ManagementInterface.ProductDescription;
                mnuExpertMode.Checked = ManagementInterfaceClient.ManagementInterface.ExpertModeEnabled;
                ChangeUserMode(mnuExpertMode.Checked);

                mnuPurchase.Visible = true;
                if (Properties.Settings.Default.ManagementInterfaceType != WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceType.Hosted)
                {
                    // Check our license for this server
                    if (!ManagementInterfaceClient.ManagementInterface.IsFreeVersion)
                    {
                        DateTime expDate;

                        if (ManagementInterfaceClient.ManagementInterface.IsLicensed)
                        {
                            expDate = ManagementInterfaceClient.ManagementInterface.LicenseExpiration;
                        }
                        else
                        {
                            expDate = ManagementInterfaceClient.ManagementInterface.TrialExpiration;
                        }

                        global::Controls.LoadingDialog.HideDialog();

                        Forms.NagForm nagForm = new CallButler.Manager.Forms.NagForm();

                        if (expDate > DateTime.MinValue && expDate >= DateTime.Now)
                        {
                            int daysLeft = (int)((TimeSpan)(expDate - DateTime.Now)).TotalDays;

                            nagForm.StatusText = String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_Expires), daysLeft);
                            nagForm.ShowDialog(this);

                        }
                        else if (expDate > DateTime.MinValue && expDate < DateTime.Now)
                        {
                            nagForm.StatusText = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_Expired);
                            nagForm.StatusTextColor = Color.Firebrick;
                            pnlExpired.Visible = true;
                            nagForm.ShowDialog(this);
                        }
                        else
                        {
                            mnuPurchase.Visible = false;
                        }

                        if (nagForm.EnterLicense)
                            LicenseRegistration();
                    }
                }

                bool firstTime = !ManagementInterfaceClient.ManagementInterface.IsDownloadRegistered;

                if (firstTime || ManagementInterfaceClient.ManagementInterface.GetFirstTimeRun(ManagementInterfaceClient.AuthInfo))
                {
                    global::Controls.LoadingDialog.HideDialog();

                    ShowQuickStartWizard(firstTime, true);
                }
                else
                {
                    LoadViewControl(new SummaryView());
                }

                if (!this.Visible)
                    this.Show();

                // Show our info form
                string splashInfo = ManagementInterfaceClient.ManagementInterface.SplashInfo;

                if (splashInfo != null && splashInfo.Length > 0)
                {
                    Forms.StartInfoForm sif = new CallButler.Manager.Forms.StartInfoForm(splashInfo);

                    global::Controls.LoadingDialog.HideDialog();

                    sif.ShowDialog(this);
                }
            }
            catch (Exception e)
            {
                RemotingExceptionManager.ProcessException(e);
                return;
            }
        }

        private void ShowQuickStartWizard(bool firstTime, bool quitOnCancel)
        {
            if(!firstTime)
            {
                this.Hide();
                LoadViewControl(new SummaryView());
            }

            Forms.QuickStartForm qsForm = new CallButler.Manager.Forms.QuickStartForm(this, firstTime);

            if (qsForm.ShowDialog() != DialogResult.OK)
            {
                if(quitOnCancel)
                    Environment.Exit(1);
            }
            else
            {
                ManagementInterfaceClient.ManagementInterface.SetFirstTimeRun(ManagementInterfaceClient.AuthInfo, false);

                switch (qsForm.Result)
                {
                    case CallButler.Manager.Forms.QuickStartResult.ProductTour:
                        ShowGettingStartedView(true);
                        break;
                    case CallButler.Manager.Forms.QuickStartResult.Normal:
                        LoadViewControl(new SummaryView());
                        break;
                }
            }

            if (!firstTime)
                this.Show();
        }

        private DialogResult ChooseEdition(bool exitOnCancel)
        {
            Forms.EditionChooserForm ecf = new CallButler.Manager.Forms.EditionChooserForm();

            ecf.SetEditions(ManagementInterfaceClient.ManagementInterface.AvailableEditions);

            this.BringToFront();
            ecf.TopMost = true;

            if(ecf.ShowDialog(this) == DialogResult.OK)
            {
                global::Controls.LoadingDialog.ShowDialog(null, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_ChangingEdition), Properties.Resources.loading, false, 0);

                // Set our new product ID
                ManagementInterfaceClient.ManagementInterface.SetProductID(ManagementInterfaceClient.AuthInfo, ecf.GetSelectedEditionProductID());
                ManagementInterfaceClient.ManagementInterface.RestartService(ManagementInterfaceClient.AuthInfo);
                System.Threading.Thread.Sleep(2000);

                global::Controls.LoadingDialog.HideDialog();

                return DialogResult.OK;
            }
            else if (exitOnCancel && (ManagementInterfaceClient.ManagementInterface.ProductID == null || ManagementInterfaceClient.ManagementInterface.ProductID.Length == 0))
            {
                Environment.Exit(1);
            }

            return DialogResult.Cancel;
        }

        #region Login Functions
        public void ProcessLogin(string server, bool promptNew)
        {
            if (Properties.Settings.Default.RecentServers == null)
            {
                Properties.Settings.Default.RecentServers = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.Save();
            }

            global::Controls.LoadingDialog.HideDialog();

            string password = "";
            bool savePassword = false;
            int port = Properties.Settings.Default.TcpManagementPort;

            if (promptNew)
            {
                LoginInfo loginInfo = PromptUserForLogin(server, password);

                if (loginInfo == null)
                {
                    ShowNoConnectionView();
                    return;
                }

                server = loginInfo.Server;
                port = loginInfo.Port;
                password = loginInfo.Password;
                savePassword = loginInfo.SavePassword;
            }
            else
            {
                if (Properties.Settings.Default.ManagementPassword != null && Properties.Settings.Default.ManagementPassword.Length > 0)
                {
                    password = WOSI.Utilities.CryptoUtils.Decrypt(Properties.Settings.Default.ManagementPassword, WOSI.CallButler.Data.Constants.EncryptionPassword);

                    if (password == null)
                        password = "";

                    savePassword = true;
                }
            }

            if (server != null && server.Length > 0)
            {
                // Split out our port from our server
                string[] serverParams = server.Split(':');

                if (serverParams.Length > 1)
                {
                    int.TryParse(serverParams[1], out port);
                }

                server = serverParams[0];
            }

            bool firstAttempt = true;

            while (true)
            {
                // Try connecting without any password first
                if (firstAttempt && !Program.RemoteMode)
                {
                    if (server.Length == 0)
                        server = "localhost";

                    ConnectionResponse response = TryConnection(server, port, "", 0);

                    if (response == ConnectionResponse.Connected)
                    {
                        break;
                    }
                    else if (response == ConnectionResponse.UnableToConnect)
                    {
                        if (TryStartService())
                        {
                            response = TryConnection(server, port, "", 0);

                            if (response == ConnectionResponse.Connected)
                            {
                                break;
                            }
                        }
                    }
                }

                LoginInfo loginInfo = PromptUserForLogin(server, password);

                firstAttempt = false;

                if (loginInfo != null)
                {
                    server = loginInfo.Server;

                    ConnectionResponse response = TryConnection(loginInfo.Server, loginInfo.Port, loginInfo.Password, 0);

                    if (response == ConnectionResponse.Connected)
                    {
                        savePassword = loginInfo.SavePassword;
                        password = loginInfo.Password;
                        break;
                    }

                    switch (response)
                    {
                        case ConnectionResponse.FailedToAuthenticate:

                            password = "";

                            MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_BadPassword), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            break;

                        case ConnectionResponse.UnableToConnect:

                            if (string.Compare(server, "localhost", true) == 0 || server == "127.0.0.1" || string.Compare(server, Environment.MachineName, true) == 0)
                            {
                                if (TryStartService())
                                {
                                    firstAttempt = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_UnableToConnect), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            break;

                        case ConnectionResponse.RemoteManagementNotAllowed:

                            MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_RemoteManagementError), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            break;
                    }
                    
                    password = "";
                }
                else
                {
                    ShowNoConnectionView();
                    return;
                }
            }
            

            ConfigureUI();
            lblConnectionStatus.Text = String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_ConnectedTo), server);

            Properties.Settings.Default.CallButlerServer = server;

            if (!Properties.Settings.Default.RecentServers.Contains(server) && string.Compare(server, "localhost", true) != 0)
                Properties.Settings.Default.RecentServers.Insert(0, server);

            // Encrypt and save our password
            if (savePassword)
            {
                Properties.Settings.Default.ManagementPassword = WOSI.Utilities.CryptoUtils.Encrypt(password, WOSI.CallButler.Data.Constants.EncryptionPassword);
            }
            else
            {
                Properties.Settings.Default.ManagementPassword = "";
            }

            Properties.Settings.Default.Save();
        }

        private bool TryStartService()
        {
            System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController("CallButler Service", Environment.MachineName);

            try
            {
                if (sc.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
                {
                    if (MessageBox.Show(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_ServiceNotRunning), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_CallButlerService), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.No)
                    {
                        return false;
                    }

                    global::Controls.LoadingDialog.ShowDialog(null, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_StartingService), Properties.Resources.loading, false, 0);

                    // Start the service
                    sc.Start();
                    sc.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running, TimeSpan.FromSeconds(20));
                    System.Threading.Thread.Sleep(5);
                    return true;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_ServiceNotFound), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private LoginInfo PromptUserForLogin(string serverName, string password)
        {
            global::Controls.LoadingDialog.HideDialog();

            Forms.LoginForm loginForm = new CallButler.Manager.Forms.LoginForm();

            // Populate our recent servers list
            //if (Properties.Settings.Default.RecentServers != null)
            //{
                string[] recentServers = new string[Properties.Settings.Default.RecentServers.Count];
                Properties.Settings.Default.RecentServers.CopyTo(recentServers, 0);
                loginForm.RecentServers = recentServers;
            //}

            loginForm.ServerName = serverName;

            if (password != null && password.Length > 0)
            {
                loginForm.Password = password;
                loginForm.SavePassword = true;
            }

            if (loginForm.ShowDialog(this) == DialogResult.OK)
            {
                LoginInfo loginInfo = new LoginInfo();
                loginInfo.Port = Properties.Settings.Default.TcpManagementPort;

                if (loginForm.ServerName != null)
                {
                    string[] serverParams = loginForm.ServerName.Split(':');

                    if (serverParams.Length > 1)
                    {
                        int.TryParse(serverParams[1], out loginInfo.Port);
                    }

                    loginInfo.Server = serverParams[0];
                }

                loginInfo.Password = loginForm.Password;
                loginInfo.SavePassword = loginForm.SavePassword;

                return loginInfo;
            }

            return null;
        }

        private ConnectionResponse TryConnection(string server, int port, string password, int timeoutInSeconds)
        {
            ConnectionResponse response = ConnectionResponse.UnableToConnect;

            global::Controls.LoadingDialog.ShowDialog(null, String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_ConnectingTo), server), Properties.Resources.loading, false, 0);

            // Try connecting
            DateTime endTime = DateTime.Now.AddSeconds(timeoutInSeconds);

            while (true)
            {
                try
                {
                    ManagementInterfaceClient.Connect(server, port, password);
                    response = ConnectionResponse.Connected;
                    break;
                }
                catch (Exception ex)
                {
                    if ((ex is System.Runtime.Remoting.RemotingException || ex is Exception) && ex.Message.Contains("Failed to authenticate"))
                        response = ConnectionResponse.FailedToAuthenticate;
                    else if ((ex is System.Runtime.Remoting.RemotingException || ex is Exception) && ex.Message.Contains("Remote Management not allowed"))
                        response = ConnectionResponse.RemoteManagementNotAllowed;
                }

                if (timeoutInSeconds == 0)
                    break;

                System.Threading.Thread.Sleep(1000);
            }

            return response;
        }
        #endregion

        /*public void PromptComputerConnection()
        {
            if (Properties.Settings.Default.ManagementInterfaceType != WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceType.Hosted)
            {
                Forms.ComputerConnectionForm ccForm = new CallButler.Manager.Forms.ComputerConnectionForm();

                if (ccForm.ShowDialog(this) == DialogResult.OK)
                {
                    ConnectToCallButlerServer(ccForm.Server, ccForm.Port, "");
                }
            }
            else
            {
                mnuLogin_Click(null, null);
            }
        }*/

        public void LicenseRegistration()
        {
            /*Licensing.Management.LicenseKeyEntryForm regForm = new Licensing.Management.LicenseKeyEntryForm();

            regForm.ProductID = ManagementInterfaceClient.ManagementInterface.ProductID;

            while (true)
            {
                if (regForm.ShowDialog(this) == DialogResult.OK)
                {
                    ManagementInterfaceClient.ManagementInterface.LicenseName = regForm.LicenseToName;
                    ManagementInterfaceClient.ManagementInterface.LicenseKey = regForm.LicenseKey;

                    if (ManagementInterfaceClient.ManagementInterface.IsLicensed)
                    {
                        DateTime expDate = ManagementInterfaceClient.ManagementInterface.LicenseExpiration;

                        if (expDate > DateTime.MinValue && expDate >= DateTime.Now)
                        {
                            pnlExpired.Visible = false;
                            MessageBox.Show(this, String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_ExpireOnDate), expDate.ToLongDateString()), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_TrialLicense), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        else if (expDate > DateTime.MinValue && expDate < DateTime.Now)
                        {
                            if (MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_EnteredExpiredLicense), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_InvalidLicenseKey), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                                break;
                        }
                        else
                        {
                            pnlExpired.Visible = false;
                            MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_ThankYouForReg), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_LicenseValidated), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        } 
                    }
                    else
                    {
                        if (MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_EnteredBadLicense), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_InvalidLicenseKey), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                            break;
                    }
                }
                else
                    break;
            }*/
        }

        private void ChangeUserMode(bool enableExpert)
        {
            if (enableExpert)
            {
                mnuCallFlow.Visible = false;
                mnuScriptSchedule.Visible = true;
                btnAddNewDepartment.Visible = false;
                btnCallFlow.Visible = false;
                btnScriptSchedule.Visible = true;
            }
            else
            {
                mnuCallFlow.Visible = true;
                mnuScriptSchedule.Visible = false;
                btnAddNewDepartment.Visible = true;
                btnCallFlow.Visible = true;
                btnScriptSchedule.Visible = false;
            }

            LoadViewControl(new SummaryView());
        }

        void RemotingExceptionManager_ConnectionError(object sender, EventArgs e)
        {
            ShowNoConnectionView();
        }

        void RemotingExceptionManager_AuthenticationError(object sender, EventArgs e)
        {
            // Try a blank password first
            if (Properties.Settings.Default.ManagementPassword != null && Properties.Settings.Default.ManagementPassword.Length > 0)
            {
                // Reset our password
                Properties.Settings.Default.ManagementPassword = "";
                Properties.Settings.Default.Save();

                //ProcessLogin();
            }

            ProcessLogin(null, true);
            /*else
            {
                /*Forms.PasswordForm pwForm = new CallButler.Manager.Forms.PasswordForm();

                // Show our password form
                if (pwForm.ShowDialog(this) == DialogResult.OK)
                {
                    // Hash our password
                    string password = "";

                    if (pwForm.Password.Length > 0)
                        password = WOSI.Utilities.CryptoUtils.CreateMD5Hash(pwForm.Password);

                    if (pwForm.SavePassword)
                    {
                        Properties.Settings.Default.ManagementPassword = password;
                        Properties.Settings.Default.Save();
                    }

                    ConnectToCallButlerServer(currentServer, currentPort, password);
                }
                else
                {
                    // Reset our password
                    Properties.Settings.Default.ManagementPassword = "";
                    Properties.Settings.Default.Save();

                    ShowNoConnectionView();
                }
            }*/
        }
    }
}
