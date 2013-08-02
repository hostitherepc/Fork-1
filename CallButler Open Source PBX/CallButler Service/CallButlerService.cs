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
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using WOSI.CallButler.Data;
using WOSI.CallButler.Data.DataProviders;
using WOSI.CallButler.Data.DataProviders.Local;
using CallButler.Telecom;
using CallButler.Service.Services;
using WOSI.Utilities;
using NET.Remoting;
using System.IO;
using Microsoft.Win32;

namespace CallButler.Service
{
    internal partial class CallButlerService : ServiceBase
    {
        private CallButlerDataProviderBase dataProvider;
        private ManagementInterfaceService managementInterfaceServer;
        private TcpRemotingServer tcpManagementDataServer;
        private PipeRemotingServer pipeManagementDataServer;
        //private KinesisService emService;
        private TelecomProviderBase telecomProvider;
        private ScriptService scriptService;
        private PBXRegistrarService pbxRegistrar;
        //private ExtensionStateService extStateService;
        private VoicemailMailerService vmMailerService;
        private VoicemailService vmService;

        public static DialPlanManagerService DialPlanManager;

        private Utilities.PluginManagement.PluginManager pluginManager;
        private string currentIP;

        private string lastErrorMessage;

        private delegate void ClearMenuItemsDelagate();
        private ClearMenuItemsDelagate clearMenuItemsHandler;

        public CallButlerService()
        {
            // Load our private label file
            Service.Services.PrivateLabelService.LoadPrivateLabelFile();

            clearMenuItemsHandler = new ClearMenuItemsDelagate(ClearMenuItemsProc);

            System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
            Version appVersion = a.GetName().Version;
            string appVersionString = appVersion.ToString();

            currentIP = WOSI.Utilities.NetworkUtils.GetCurrentIpAddress();

            InitializeComponent();

            LoadStartupOption();
            mnuLoadOnStartup.Click += new EventHandler(mnuLoadOnStartup_Click);

            mnuManageCallButler.Click += new EventHandler(mnuManageCallButler_Click);
            mnuManageCallButler.Image = Properties.Resources.nut_and_bolt_16;

            mnuExit.Click += new EventHandler(mnuExit_Click);
            mnuExit.Image = Properties.Resources.exit_16;

            mnuPlugins.Image = Properties.Resources.gear_connection_16;

            // Set our default language
            if (Properties.Settings.Default.DefaultLanguage == null || Properties.Settings.Default.DefaultLanguage.Length == 0)
            {
                Properties.Settings.Default.DefaultLanguage = WOSI.Utilities.GlobalizationUtils.GetTopParentCulture(System.Threading.Thread.CurrentThread.CurrentCulture).IetfLanguageTag;
                Properties.Settings.Default.Save();
            }

            LoggingService.NewLogEntry += new EventHandler<LogEntryEventArgs>(LoggingService_NewLogEntry);
        }

        private void LoadStartupOption()
        {
            /*RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            if (regKey != null)
            {
                if (regKey.GetValue(Properties.Settings.Default.ProductDescription) == null)
                {
                    mnuLoadOnStartup.CheckState = CheckState.Unchecked;
                }
                else
                {
                    mnuLoadOnStartup.CheckState = CheckState.Checked;
                }
            }*/
        }

        void mnuLoadOnStartup_Click(object sender, EventArgs e)
        {
            /*RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

            if (mnuLoadOnStartup.Checked)
            {
                mnuLoadOnStartup.CheckState = CheckState.Checked;
                regKey.SetValue(Properties.Settings.Default.ProductDescription, Application.ExecutablePath + " -a");
            }
            else
            {
                if (regKey.GetValue(Properties.Settings.Default.ProductDescription) != null)
                {
                    mnuLoadOnStartup.CheckState = CheckState.Unchecked;
                    regKey.DeleteValue(Properties.Settings.Default.ProductDescription);
                }
            }*/
        }

        #region Menu Events
        void mnuManageCallButler_Click(object sender, EventArgs e)
        {
            string callButlerManagerPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), Properties.Settings.Default.CallButlerManagementAppLocation);

            if (File.Exists(callButlerManagerPath))
            {
                System.Diagnostics.Process.Start(callButlerManagerPath);
            }
            else
            {
                MessageBox.Show(Services.PrivateLabelService.ReplaceProductName("The CallButler management application does not appear to be installed on this computer.\r\nPlease run the CallButler setup program again to install it."), Services.PrivateLabelService.ReplaceProductName("CallButler Manager Not Found"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Public Properties
        public bool NotifyIconVisible
        {
            get
            {
                return notifyIcon.Visible;
            }
            set
            {
                notifyIcon.Visible = value;
            }
        }
        #endregion

        #region Public Service Control Methods
        private void CreateManagementChannels()
        {
            // Create our management interface server
            managementInterfaceServer = new ManagementInterfaceService();
            managementInterfaceServer.Initialize(this, null, null, null, null, null, null);

            if (Properties.Settings.Default.EnableRemoteManagement)
            {
                // Create our remoting object for our management server
                tcpManagementDataServer = new TcpRemotingServer("CallButler TCP Management Server (" + Properties.Settings.Default.ProductID + ")", "CallButlerManagementServer", Properties.Settings.Default.ManagementServicePort, (MarshalByRefObject)managementInterfaceServer);
                tcpManagementDataServer.OnAuthentication += new EventHandler<NET.Remoting.Channels.AuthenticationEventArgs>(managementDataServer_OnAuthentication);
                tcpManagementDataServer.OnManagementAllowed += new EventHandler<NET.Remoting.Channels.ManagementAllowedEventArgs>(managementDataServer_OnManagementAllowed);
            }

            

            pipeManagementDataServer = new PipeRemotingServer("CallButler PIPE Management Server (" + Properties.Settings.Default.ProductID + ")", "CallButlerManagementServer", Properties.Settings.Default.ManagementServicePort, (MarshalByRefObject)managementInterfaceServer);
            pipeManagementDataServer.OnAuthentication += new EventHandler<NET.Remoting.Channels.AuthenticationEventArgs>(managementDataServer_OnAuthentication);
            pipeManagementDataServer.OnManagementAllowed += new EventHandler<NET.Remoting.Channels.ManagementAllowedEventArgs>(managementDataServer_OnManagementAllowed);
            pipeManagementDataServer.StartServer();
        }

        private void Initialize()
        {
            if (Properties.Settings.Default.ProductID != null && Properties.Settings.Default.ProductID.Length > 0)
            {
                lastErrorMessage = null;

                notifyIcon.Text = Properties.Settings.Default.ProductDescription;

                // Load our application permissions
                //Licensing.Management.AppPermissions.LoadPermissionData(Properties.Settings.Default.ProductID, Properties.Resources.Permissions);

                // Load our dataprovider
                System.Collections.Specialized.NameValueCollection settings = new System.Collections.Specialized.NameValueCollection();

                switch (Properties.Settings.Default.DataProviderType)
                {
                    case LocalCallButlerDataProviderTypes.XmlDataProvider:
                        // Create our settings
                        settings["RootDataDirectory"] = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.XmlDataRootDirectory);
                        settings["RootGreetingSoundDirectory"] = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingSoundRootDirectory);

                        dataProvider = new CallButlerXmlDataProvider();
                        dataProvider.Connect(settings);
                        break;
                    /*case LocalCallButlerDataProviderTypes.SqlServerDataProvider:

                        settings["RootDataDirectory"] = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.XmlDataRootDirectory);
                        settings["RootGreetingSoundDirectory"] = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingSoundRootDirectory);
                        settings["ConnectionString"] = Properties.Settings.Default.SqlConnectionString;

                        dataProvider = new CallButlerSQLServerDataProvider();
                        dataProvider.Connect(settings);
                        break;*/
                }

                /*if (Properties.Settings.Default.EnableKinesisServer)
                {
                    extStateService = new ExtensionStateService(dataProvider);
                }*/

                // Create our telecom provider
                int lineCount = /*Math.Min(*/ Properties.Settings.Default.LineCount; /*, Licensing.Management.AppPermissions.StatGetPermissionScalar("MaxLineCount"));*/

                switch (Properties.Settings.Default.TelecomProviderType)
                {
                    case TelecomProviders.inTELIPhoneTelecomProvider:
                        {
                            telecomProvider = new inTELIPhoneTelecomProvider(mnuServiceOptions, notifyIcon, lineCount, Properties.Settings.Default.UseStun, Properties.Settings.Default.StunServer, Properties.Settings.Default.SipPort, Properties.Settings.Default.UseInternalAddressForSIPMessages);

                            // Register all of our profiles
                            //if (Licensing.Management.AppPermissions.StatIsPermitted("Providers"))
                            //{
                                WOSI.CallButler.Data.CallButlerDataset.ProvidersDataTable providers = dataProvider.GetProviders(Properties.Settings.Default.CustomerID);

                                foreach (WOSI.CallButler.Data.CallButlerDataset.ProvidersRow provider in providers)
                                {
                                    if (provider.IsEnabled)
                                    {
                                        telecomProvider.Register(provider.ProviderID, provider);
                                    }
                                }
                            //}

                            // Start our PBX registrar engine
                            if (Properties.Settings.Default.EnablePBX /*&& Licensing.Management.AppPermissions.StatIsPermitted("PBX.Registrar")*/)
                            {
                                pbxRegistrar = new PBXRegistrarService(telecomProvider, dataProvider/*, extStateService*/);
                            }

                            break;
                        }
                }

                // Initialize our codecs
                if (Properties.Settings.Default.AudioCodecs != null && Properties.Settings.Default.AudioCodecs.Length > 0)
                    telecomProvider.SetAudioCodecs(Properties.Settings.Default.AudioCodecs);

                // Create our plugin manager object
                pluginManager = new Utilities.PluginManagement.PluginManager();

                // Create our voicemail mailer service
                vmMailerService = new VoicemailMailerService();

                // Initialize our voicemail service
                vmService = new VoicemailService(dataProvider, pbxRegistrar, pluginManager, vmMailerService);

                // Create our script server
                scriptService = new ScriptService(telecomProvider, dataProvider, vmService, vmMailerService, pluginManager, pbxRegistrar/*, extStateService*/);

                // Load our plugins
                CallButler.Service.Plugin.CallButlerServiceContext serviceContext = new CallButler.Service.Plugin.CallButlerServiceContext(scriptService);
                pluginManager.LoadPlugins(WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.PluginDirectory), "*.dll", true, typeof(CallButler.Service.Plugin.CallButlerServicePlugin));
                
                foreach (CallButler.Service.Plugin.CallButlerServicePlugin plugin in pluginManager.Plugins)
                {
                    try
                    {
                        plugin.Load(serviceContext);
                    }
                    catch
                    {
                    }
                }

                try
                {
                    mnuPlugins.Visible = false;
                    mnuSeparator4.Visible = false;
                }
                catch
                {
                }

                // Initialize our management interface
                managementInterfaceServer.Initialize(this, dataProvider, telecomProvider, scriptService, pluginManager, pbxRegistrar, vmService);

                // Create our default greetings if this the first time we've run
                if (Properties.Settings.Default.FirstTimeRun)
                {
                    WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable lgTable = new CallButlerDataset.LocalizedGreetingsDataTable();
                    WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow lgRow;

                    if (dataProvider.GetGreeting(Properties.Settings.Default.CustomerID, WOSI.CallButler.Data.Constants.WelcomeGreetingGuid) == null)
                    {
                        lgRow = lgTable.NewLocalizedGreetingsRow();

                        lgRow.GreetingID = WOSI.CallButler.Data.Constants.WelcomeGreetingGuid;
                        lgRow.LanguageID = "en";
                        lgRow.LocalizedGreetingID = Guid.NewGuid();
                        lgRow.Type = (short)WOSI.CallButler.Data.GreetingType.SoundGreeting;

                        lgTable.AddLocalizedGreetingsRow(lgRow);
                    }

                    if (dataProvider.GetGreeting(Properties.Settings.Default.CustomerID, WOSI.CallButler.Data.Constants.MainMenuGreetingGuid) == null)
                    {
                        lgRow = lgTable.NewLocalizedGreetingsRow();

                        lgRow.GreetingID = WOSI.CallButler.Data.Constants.MainMenuGreetingGuid;
                        lgRow.LanguageID = "en";
                        lgRow.LocalizedGreetingID = Guid.NewGuid();
                        lgRow.Type = (short)WOSI.CallButler.Data.GreetingType.SoundGreeting;

                        lgTable.AddLocalizedGreetingsRow(lgRow);
                    }

                    managementInterfaceServer.PersistLocalizedGreeting(Properties.Settings.Default.CustomerID, lgTable);

                    Properties.Settings.Default.FirstTimeRun = false;
                    Properties.Settings.Default.Save();
                }

                // Setup our default language
                if (Properties.Settings.Default.DefaultLanguage.Length == 0)
                {
                    Properties.Settings.Default.DefaultLanguage = System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag;
                    Properties.Settings.Default.Save();
                }

                telecomProvider.Startup();

                CallButlerService.DialPlanManager = new DialPlanManagerService(pbxRegistrar, dataProvider, telecomProvider);

                if (tcpManagementDataServer != null)
                    tcpManagementDataServer.StartServer();

                // Create our extension management service
                /*if (Properties.Settings.Default.EnableKinesisServer)
                {
                    emService = new KinesisService(dataProvider, extStateService, scriptService, vmService);
                    emService.Start();
                }*/
            }
        }

        private void ClearMenuItemsProc()
        {
            mnuServiceOptions.DropDownItems.Clear();
        }

        private void DeInitialize()
        {
            if (mnuMain.InvokeRequired)
                mnuMain.Invoke(clearMenuItemsHandler);
            else
                mnuServiceOptions.DropDownItems.Clear();

            /*if (extStateService != null)
            {
                extStateService.ClearState();
                extStateService = null;
            }

            if (emService != null)
            {
                emService.Stop();
                emService.Dispose();
                emService = null;
            }*/

            if (pluginManager != null)
            {
                // Unload our plugins
                foreach (CallButler.Service.Plugin.CallButlerServicePlugin plugin in pluginManager.Plugins)
                {
                    plugin.Unload();
                }

                pluginManager.UnloadPlugins();
                pluginManager = null;
            }

            if (scriptService != null)
            {
                scriptService.Shutdown();
                scriptService = null;
            }

            if (vmMailerService != null)
            {
                vmMailerService = null;
            }

            if (vmService != null)
            {
                vmService = null;
            }

            dataProvider = null;

            if (CallButlerService.DialPlanManager != null)
            {
                CallButlerService.DialPlanManager = null;
            }

            if (pbxRegistrar != null)
            {
                pbxRegistrar.Shutdown();
                pbxRegistrar = null;
            }

            if (telecomProvider != null)
            {
                telecomProvider.Shutdown();
                telecomProvider = null;
            }

            if (tcpManagementDataServer != null)
                tcpManagementDataServer.StopServer();

            GC.Collect();
        }

        public void StartService(string[] args)
        {
            if (Properties.Settings.Default.EnablePerformanceCounters)
                PerformanceCounterService.Initialize();

            CreateManagementChannels();
            Initialize();
        }

        public void StopService()
        {
            /*if (emService != null)
            {
                emService.Stop();
                emService.Dispose();
                emService = null;
            }

            if (extStateService != null)
            {
                extStateService.ClearState();
                extStateService = null;
            }*/

            if(managementInterfaceServer != null)
                managementInterfaceServer = null;

            if (tcpManagementDataServer != null)
            {
                tcpManagementDataServer.StopServer();
                tcpManagementDataServer = null;
            }

            if (pipeManagementDataServer != null)
            {
                pipeManagementDataServer.StopServer();
                pipeManagementDataServer = null;
            }

            if (vmMailerService != null)
            {
                vmMailerService = null;
            }

            if (vmService != null)
            {
                vmService = null;
            }

            DeInitialize();

            PerformanceCounterService.ResetCounters();
        }

        public void RestartService(bool async)
        {
            DeInitialize();
            System.Threading.Thread.Sleep(500);
            Initialize();
        }

        void tcpClientManagementDataServer_OnManagementAllowed(object sender, NET.Remoting.Channels.ManagementAllowedEventArgs e)
        {
            try
            {
                int ext = Convert.ToInt32(e.Extension);
                bool allow = false;
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow row = dataProvider.GetExtensionNumber(Properties.Settings.Default.CustomerID, ext);
                if (row != null)
                {
                    allow = row.EnableManagement;
                }
                e.ManagementAllowed = allow;
            }
            catch
            {
                e.ManagementAllowed = false;
            }
        }

        void tcpClientManagementDataServer_OnAuthentication(object sender, NET.Remoting.Channels.AuthenticationEventArgs e)
        {
            e.IsAuthenticated = managementInterfaceServer.Authenticate(e.CustomerID, e.ExtensionNumber, e.Password);
        }

        void managementDataServer_OnManagementAllowed(object sender, NET.Remoting.Channels.ManagementAllowedEventArgs e)
        {
            if (Properties.Settings.Default.EnableRemoteManagement == false)
            {
                if (currentIP.Equals(e.IpAddress) || e.IpAddress.Equals("127.0.0.1") || e.IpAddress.ToLower().Equals("localhost"))
                {
                    e.ManagementAllowed = true;
                }
                else
                {
                    e.ManagementAllowed = false;
                }
            }
            else
            {
                e.ManagementAllowed = true;
            }
        }

        void managementDataServer_OnAuthentication(object sender, NET.Remoting.Channels.AuthenticationEventArgs e)
        {
            e.IsAuthenticated = managementInterfaceServer.Authenticate(e.CustomerID, e.ExtensionNumber, e.Password);
        }
        #endregion

        #region Logging Events
        void LoggingService_NewLogEntry(object sender, LogEntryEventArgs e)
        {
            // Send an Email with our error, but only if it's new.
            if (Properties.Settings.Default.SendLogErrorEmails && e.IsError && e.Message != lastErrorMessage)
            {
                try
                {
                    lastErrorMessage = e.Message;

                    string smtpPassword = "";

                    if (Properties.Settings.Default.SMTPPassword.Length > 0)
                        smtpPassword = WOSI.Utilities.CryptoUtils.Decrypt(Properties.Settings.Default.SMTPPassword, WOSI.CallButler.Data.Constants.EncryptionPassword);

                    WOSI.Utilities.EmailUtils.SendEmail(Properties.Settings.Default.SMTPEmailFrom, Properties.Settings.Default.LogErrorEmailAddress, Services.PrivateLabelService.ReplaceProductName("A CallButler Error Has Been Logged"), e.Message, Properties.Settings.Default.SMTPServer, Properties.Settings.Default.SMTPPort, Properties.Settings.Default.SMTPUseSSL, Properties.Settings.Default.SMTPUsername, smtpPassword);
                }
                catch
                {
                }
            }
        }
        #endregion

        protected override void OnStart(string[] args)
        {
            StartService(null);
        }

        protected override void OnStop()
        {
            StopService();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
        }
    }
}
