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
using System.Text;
using System.Data;
using System.IO;
using WOSI.CallButler.Data;
using WOSI.CallButler.Data.DataProviders;
using WOSI.CallButler.ManagementInterface;
using SpeechLib;

namespace CallButler.Service.Services
{
    class ManagementInterfaceService : MarshalByRefObject, WOSI.CallButler.ManagementInterface.ICallButlerManagementInterface
    {
        private CallButlerDataProviderBase dataProvider;
        private Telecom.TelecomProviderBase telecomProvider;
        private ScriptService scriptService;
        private Utilities.PluginManagement.PluginManager pluginManager;
        private SpeechLib.SpVoiceClass tts;
        private CallButlerService cbService;
        private PBXRegistrarService registrarService;
        private VoicemailService vmService;

        public ManagementInterfaceService()
        {
            
        }

        internal void Initialize(CallButlerService cbService, CallButlerDataProviderBase dataProvider, Telecom.TelecomProviderBase telecomProvider, ScriptService scriptService, Utilities.PluginManagement.PluginManager pluginManager, PBXRegistrarService registrarService, VoicemailService vmService)
        {
            this.cbService = cbService;
            this.dataProvider = dataProvider;
            this.telecomProvider = telecomProvider;
            this.scriptService = scriptService;
            this.pluginManager = pluginManager;
            this.registrarService = registrarService;
            this.tts = new SpVoiceClass();
            this.vmService = vmService;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public bool IsConnected
        {
            get
            {
                return true;
            }
        }

        #region Settings Functions

        public int LineCount
        {
            get
            {
                int lineCount = /*Math.Min(*/Properties.Settings.Default.LineCount; //, Licensing.Management.AppPermissions.StatGetPermissionScalar("MaxLineCount"));

                return lineCount;
            }
            set
            {
                int lineCount = value; //Math.Min(value, Licensing.Management.AppPermissions.StatGetPermissionScalar("MaxLineCount"));

                Properties.Settings.Default.LineCount = lineCount;
                Properties.Settings.Default.Save();
            }
        }

        public string[] GetTTSVoices()
        {
            System.Collections.Generic.List<string> voices = new System.Collections.Generic.List<string>();

            try
            {
                ISpeechObjectTokens tokens = tts.GetVoices("", "");

                if (tokens != null)
                {

                    for (int index = 0; index < tokens.Count; index++)
                    {
                        try
                        {
                            ISpeechObjectToken token = tokens.Item(index);

                            voices.Add(token.GetAttribute("Name"));
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }

            return voices.ToArray();
        }

        //public string DefaultTTSVoice
        //{
        //    get
        //    {
        //        return Properties.Settings.Default.DefaultTTSVoice;
        //    }
        //    set
        //    {
        //        Properties.Settings.Default.DefaultTTSVoice = value;
        //        Properties.Settings.Default.Save();
        //    }
        //}

        public string GetDefaultVoice(CallButlerAuthInfo authInfo)
        {
            string defaultVoice = "";
            if (Authenticate(authInfo))
            {
                defaultVoice = Properties.Settings.Default.DefaultTTSVoice;
            }

            return defaultVoice;
        }

        public void SetDefaultVoice(CallButlerAuthInfo authInfo, string defaultVoice)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.DefaultTTSVoice = defaultVoice;
                Properties.Settings.Default.Save();
            }
        }

        public byte[] PrivateLabelData
        {
            get
            {
                if (File.Exists(Services.PrivateLabelService.GetPrivateLabelFilePath()))
                {
                    return File.ReadAllBytes(Services.PrivateLabelService.GetPrivateLabelFilePath());
                }

                return null;
            }
        }

        public string ProductID
        {
            get
            {
                return Properties.Settings.Default.ProductID;
            }
        }

        public string SplashInfo
        {
            get
            {
                return Properties.Settings.Default.SplashInfo;
            }
        }

        public System.Collections.Specialized.NameValueCollection AvailableEditions
        {
            get
            {
                System.Collections.Specialized.NameValueCollection ae = new System.Collections.Specialized.NameValueCollection();

                for (int index = 0; index < Properties.Settings.Default.AvailableProductIDs.Length; index++)
                {
                    ae.Add(Properties.Settings.Default.AvailableProductIDs[index], Properties.Settings.Default.AvailableProductDescriptions[index]);
                }

                return ae;
            }
        }

        public void SetProductID(CallButlerAuthInfo authInfo, string productID)
        {
            if (Authenticate(authInfo))
            {
                foreach (string availableProductID in Properties.Settings.Default.AvailableProductIDs)
                {
                    if (productID == availableProductID)
                    {
                        Properties.Settings.Default.ProductID = productID;
                        Properties.Settings.Default.Save();
                        break;
                    }
                }
            }
        }

        public string ProductDescription
        {
            get
            {
                return Properties.Settings.Default.ProductDescription;
            }
        }

        public string TelephoneNumberDescription
        {
            get
            {
                return Properties.Settings.Default.TelephoneNumberDescription;
            }
        }

        public string AdditionalCopyrightNotice
        {
            get
            {
                return Properties.Settings.Default.AdditionalCopyrightNotice;
            }
        }

        public bool GetMultilingual(CallButlerAuthInfo authInfo)
        {
            return Authenticate(authInfo) ? Properties.Settings.Default.Multilingual : false;
        }

        public void SetMultilingual(CallButlerAuthInfo authInfo, bool isMultilingual)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.Multilingual = isMultilingual;
                Properties.Settings.Default.Save();
            }
        }

        public string GetDefaultLanguage(CallButlerAuthInfo authInfo)
        {
            return Authenticate(authInfo) ? Properties.Settings.Default.DefaultLanguage : null;
        }

        public void SetDefaultLanguage(CallButlerAuthInfo authInfo, string defaultLanguage)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.DefaultLanguage = defaultLanguage;
                Properties.Settings.Default.Save();
            }
        }

        public string GetLanguages(CallButlerAuthInfo authInfo)
        {
            return Authenticate(authInfo) ? Properties.Settings.Default.Languages : null;
        }

        public void SetLanguages(CallButlerAuthInfo authInfo, string languages)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.Languages = languages;
                Properties.Settings.Default.Save();
            }
        }

        public string GetApplicationPermissions(CallButlerAuthInfo authInfo)
        {
            return Authenticate(authInfo) ? Properties.Resources.Permissions : null;
        }

        public string SMTPServer
        {
            get
            {
                return Properties.Settings.Default.SMTPServer;
            }
            set
            {
                Properties.Settings.Default.SMTPServer = value;
                Properties.Settings.Default.Save();
            }
        }

        public int SMTPPort
        {
            get
            {
                return Properties.Settings.Default.SMTPPort;
            }
            set
            {
                Properties.Settings.Default.SMTPPort = value;
                Properties.Settings.Default.Save();
            }
        }

        public bool SMTPUseSSL
        {
            get
            {
                return Properties.Settings.Default.SMTPUseSSL;
            }
            set
            {
                Properties.Settings.Default.SMTPUseSSL = value;
                Properties.Settings.Default.Save();
            }
        }

        public string SMTPUsername
        {
            get
            {
                return Properties.Settings.Default.SMTPUsername;
            }
            set
            {
                Properties.Settings.Default.SMTPUsername = value;
                Properties.Settings.Default.Save();
            }
        }

        public string SMTPPassword
        {
            get
            {
                return Properties.Settings.Default.SMTPPassword;
            }
            set
            {
                Properties.Settings.Default.SMTPPassword = value;
                Properties.Settings.Default.Save();
            }
        }

        public string SMTPFromEmail
        {
            get
            {
                return Properties.Settings.Default.SMTPEmailFrom;
            }

            set
            {
                Properties.Settings.Default.SMTPEmailFrom = value;
                Properties.Settings.Default.Save();
            }
        }

        //public byte SoundVolume
        //{
        //    get
        //    {
        //        return Properties.Settings.Default.SoundVolume;
        //    }
        //    set
        //    {
        //        Properties.Settings.Default.SoundVolume = value;
        //        Properties.Settings.Default.Save();
        //    }
        //}

        public byte GetSoundVolume(CallButlerAuthInfo authInfo)
        {
            byte soundVolume = 0;

            if (Authenticate(authInfo))
            {
                soundVolume = Properties.Settings.Default.SoundVolume;
            }
            return soundVolume;
        }

        public void SetSoundVolume(CallButlerAuthInfo authInfo, byte soundVolume)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.SoundVolume = soundVolume;
                Properties.Settings.Default.Save();
            }
        }

        //public byte RecordVolume
        //{
        //    get
        //    {
        //        return Properties.Settings.Default.RecordVolume;
        //    }
        //    set
        //    {
        //        Properties.Settings.Default.RecordVolume = value;
        //        Properties.Settings.Default.Save();
        //    }
        //}

        public byte GetRecordVolume(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
            {
                return Properties.Settings.Default.RecordVolume;
            }
            return 0;
        }

        public void SetRecordVolume(CallButlerAuthInfo authInfo, byte recordVolume)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.RecordVolume = recordVolume;
                Properties.Settings.Default.Save();
            }
        }


        //public byte SpeechVolume
        //{
        //    get
        //    {
        //        return Properties.Settings.Default.SpeechVolume;
        //    }
        //    set
        //    {
        //        Properties.Settings.Default.SpeechVolume = value;
        //        Properties.Settings.Default.Save();
        //    }
        //}

        public byte GetSpeechVolume(CallButlerAuthInfo authInfo)
        {
            byte speechVolume = 0;
            if(Authenticate(authInfo))
            {
                speechVolume = Properties.Settings.Default.SpeechVolume;
            }

            return speechVolume;
        }


        public void SetSpeechVolume(CallButlerAuthInfo authInfo, byte speechVolume)
        {
            Properties.Settings.Default.SpeechVolume = speechVolume;
            Properties.Settings.Default.Save();
        }



        public string HoldMusicLocation
        {
            get
            {
                return Properties.Settings.Default.HoldMusicRootDirectory;
            }
            set
            {
                Properties.Settings.Default.HoldMusicRootDirectory = value;
                Properties.Settings.Default.Save();
            }
        }

        public int SIPPort
        {
            get
            {
                return Properties.Settings.Default.SipPort;
            }
            set
            {
                Properties.Settings.Default.SipPort = value;
                Properties.Settings.Default.Save();
            }
        }

        public bool UseInternalAddressForSIP
        {
            get
            {
                return Properties.Settings.Default.UseInternalAddressForSIPMessages;
            }
            set
            {
                Properties.Settings.Default.UseInternalAddressForSIPMessages = value;
                Properties.Settings.Default.Save();
            }
        }

        public bool EnableSTUN
        {
            get
            {
                return Properties.Settings.Default.UseStun;
            }
            set
            {
                Properties.Settings.Default.UseStun = value;
                Properties.Settings.Default.Save();
            }
        }

        public string STUNServer
        {
            get
            {
                return Properties.Settings.Default.StunServer;
            }
            set
            {
                Properties.Settings.Default.StunServer = value;
                Properties.Settings.Default.Save();
            }
        }

        public string LicenseName
        {
            get
            {
                return Properties.Settings.Default.LicenseName;
            }
            set
            {
                Properties.Settings.Default.LicenseName = value;
                Properties.Settings.Default.Save();

                //LicenseService.RefreshLicenseInfo();
            }
        }

        public string LicenseKey
        {
            get
            {
                return Properties.Settings.Default.LicenseKey;
            }
            set
            {
                Properties.Settings.Default.LicenseKey = value;
                Properties.Settings.Default.Save();

                //LicenseService.RefreshLicenseInfo();
            }
        }

        public bool IsLicensed
        {
            get
            {
                return true; //LicenseService.IsLicensed();
            }
        }

        public bool IsFreeVersion
        {
            get
            {
                return Properties.Settings.Default.IsFreeVersion;
            }
        }

        public DateTime LicenseExpiration
        {
            get
            {
                return DateTime.MinValue; //LicenseService.GetLicenseExpiration();
            }
        }

        public DateTime TrialExpiration
        {
            get
            {
                return DateTime.MinValue; //LicenseService.GetTrialExpiration();
            }
        }

        public bool IsDownloadRegistered
        {
            get
            {
                return Properties.Settings.Default.DownloadRegistered;
            }
            set
            {
                Properties.Settings.Default.DownloadRegistered = value;
                Properties.Settings.Default.Save();
            }
        }

        //public string ManagementPassword
        //{
        //    get
        //    {
        //        return Properties.Settings.Default.ManagementPassword;
        //    }
        //    set
        //    {
        //        Properties.Settings.Default.ManagementPassword = value;
        //        Properties.Settings.Default.Save();
        //    }
        //}

        public bool AllowRemoteManagement
        {
            get
            {
                return Properties.Settings.Default.EnableRemoteManagement;
            }
            set
            {
                Properties.Settings.Default.EnableRemoteManagement = value;
            }
        }

        public void SetManagementPassword(CallButlerAuthInfo authInfo, string password)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.ManagementPassword = password;
                Properties.Settings.Default.Save();
            }
        }

        public string GetManagementPassword(CallButlerAuthInfo authInfo)
        {
            return Properties.Settings.Default.ManagementPassword;
        }

        public Version ServerVersion
        {
            get
            {
                return this.GetType().Assembly.GetName().Version;
            }
        }

        public bool ExpertModeEnabled
        {
            get
            {
                return Properties.Settings.Default.ExpertMode;
            }
            set
            {
                Properties.Settings.Default.ExpertMode = value;
                Properties.Settings.Default.Save();
            }
        }

        public LogLevel LogLevel
        {
            get
            {
                return Properties.Settings.Default.LoggingLevel;
            }
            set
            {
                Properties.Settings.Default.LoggingLevel = value;
                Properties.Settings.Default.Save();
            }
        }

        public LogStorage LogStorage
        {
            get
            {
                return Properties.Settings.Default.LogStorage;
            }
            set
            {
                Properties.Settings.Default.LogStorage = value;
                Properties.Settings.Default.Save();
            }
        }

        public bool GetFirstTimeRun(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
            {
                return Properties.Settings.Default.FirstTimeRun;
            }

            return true;
        }


        public void SetFirstTimeRun(CallButlerAuthInfo authInfo, bool val)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.FirstTimeRun = val;
                Properties.Settings.Default.Save();
            }
        }

        //public bool FirstTimeRun
        //{
        //    get
        //    {
        //        return Properties.Settings.Default.FirstTimeRun;
        //    }
        //    set
        //    {
        //        Properties.Settings.Default.FirstTimeRun = value;
        //        Properties.Settings.Default.Save();
        //    }
        //}

        public AudioCodecInformation[] GetAudioCodecs(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
            {
                List<AudioCodecInformation> codecs = new List<AudioCodecInformation>();
                
                foreach (CallButler.Telecom.AudioCodecInformation acInfo in telecomProvider.GetAudioCodecs())
                {
                    WOSI.CallButler.ManagementInterface.AudioCodecInformation newAcInfo = new AudioCodecInformation();

                    newAcInfo.Enabled = acInfo.Enabled;
                    newAcInfo.Name = acInfo.Name;

                    codecs.Add(newAcInfo);
                }

                return codecs.ToArray();
            }
            else
                return null;
        }

        internal void SetAudioCodecsInternal(AudioCodecInformation[] codecs)
        {
            List<CallButler.Telecom.AudioCodecInformation> newCodecs = new List<CallButler.Telecom.AudioCodecInformation>();

            foreach (WOSI.CallButler.ManagementInterface.AudioCodecInformation acInfo in codecs)
            {
                CallButler.Telecom.AudioCodecInformation newAcInfo = new CallButler.Telecom.AudioCodecInformation();

                newAcInfo.Enabled = acInfo.Enabled;
                newAcInfo.Name = acInfo.Name;

                newCodecs.Add(newAcInfo);
            }

            telecomProvider.SetAudioCodecs(newCodecs.ToArray());
            Properties.Settings.Default.AudioCodecs = newCodecs.ToArray();
            Properties.Settings.Default.Save();
        }

        public void SetAudioCodecs(CallButlerAuthInfo authInfo, AudioCodecInformation[] codecs)
        {
            if (Authenticate(authInfo))
            {
                SetAudioCodecsInternal(codecs);
            }
        }

        public void SetAnswerTimeout(CallButlerAuthInfo authInfo, int answerTimeout)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.AnswerTimeout = answerTimeout;
                Properties.Settings.Default.Save();
            }
        }

        public int GetAnswerTimeout(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
                return Properties.Settings.Default.AnswerTimeout;
            else
                return 0;
        }

        public void SetWelcomeGreetingDelay(CallButlerAuthInfo authInfo, int delay)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.WelcomeGreetingDelay = delay;
                Properties.Settings.Default.Save();
            }
        }

        public int GetWelcomeGreetingDelay(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
                return Properties.Settings.Default.WelcomeGreetingDelay;
            else
                return 0;
        }

        public string GetLogErrorEmail(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
                return Properties.Settings.Default.LogErrorEmailAddress;
            else
                return null;
        }

        public void SetLogErrorEmail(CallButlerAuthInfo authInfo, string emailAddress)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.LogErrorEmailAddress = emailAddress;
                Properties.Settings.Default.Save();
            }
        }

        public bool GetSendLogErrorEmail(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
                return Properties.Settings.Default.SendLogErrorEmails;
            else
                return false;
        }

        public void SetSendLogErrorEmail(CallButlerAuthInfo authInfo, bool sendEmail)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.SendLogErrorEmails = sendEmail;
                Properties.Settings.Default.Save();
            }
        }

        public bool GetReceptionistEnabled(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
            {
                return Properties.Settings.Default.TryReceptionistFirst;
            }

            return false;
        }

        public void SetReceptionistEnabled(CallButlerAuthInfo authInfo, bool enabled)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.TryReceptionistFirst = enabled;
                Properties.Settings.Default.Save();
            }
        }

        public Guid GetReceptionistExtension(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
            {
                return Properties.Settings.Default.ReceptionistExtensionID;
            }

            return Guid.Empty;
        }

        public void SetReceptionistExtension(CallButlerAuthInfo authInfo, Guid extensionID)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.ReceptionistExtensionID = extensionID;
                Properties.Settings.Default.Save();
            }
        }

        public string GetBusyRedirectServer(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
                return Properties.Settings.Default.BusyRedirectServer;
            else
                return null;
        }

        public void SetBusyRedirectServer(CallButlerAuthInfo authInfo, string server)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.BusyRedirectServer = server;
                Properties.Settings.Default.Save();
            }
        }

        #region PBX Settings Functions
        public int GetPBXRegistrationTimeout(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
            {
                return Properties.Settings.Default.PresenceTimeout;
            }
            else
            {
                return 0;
            }
        }

        public void SetPBXRegistrationTimeout(CallButlerAuthInfo authInfo, int timeout)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.PresenceTimeout = timeout;
                Properties.Settings.Default.Save();
            }
        }

        public string GetPBXDialPrefix(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
            {
                return Properties.Settings.Default.OutboundDialingPrefix;
            }
            else
            {
                return "";
            }
        }

        public void SetPBXDialPrefix(CallButlerAuthInfo authInfo, string dialPrefix)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.OutboundDialingPrefix = dialPrefix;
                Properties.Settings.Default.Save();
            }
        }

        public string GetPBXRegistrarDomain(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
            {
                return Properties.Settings.Default.SIPRegistrarDomain;
            }
            else
            {
                return "";
            }
        }

        public void SetPBXRegistrarDomain(CallButlerAuthInfo authInfo, string domain)
        {
            if (Authenticate(authInfo))
            {
                Properties.Settings.Default.SIPRegistrarDomain = domain;
                Properties.Settings.Default.Save();
            }
        }
        #endregion
        #endregion

        #region Status Functions
        public WOSI.CallButler.Data.CallButlerPhoneStatusDataset.PhoneStatusDataTable GetPhoneExtensionStatus(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
            {
                return registrarService.GetPhoneStatus().PhoneStatus;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Service Functions
        public void RestartService(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
            {
                cbService.RestartService(true);
            }
        }
        #endregion

        #region Plugin Functions
        public Guid[] GetInstalledServicePlugins()
        {
            List<Guid> pluginList = new List<Guid>();

            foreach (CallButler.Service.Plugin.CallButlerServicePlugin plugin in pluginManager.Plugins)
            {
                pluginList.Add(plugin.PluginID);
            }

            return pluginList.ToArray();
        }

        public object ExchangePluginData(CallButlerAuthInfo authInfo, Guid pluginID, string method, object data)
        {
            if (Authenticate(authInfo))
            {
                foreach (CallButler.Service.Plugin.CallButlerServicePlugin plugin in pluginManager.Plugins)
                {
                    if (plugin.PluginID == pluginID)
                        return plugin.ExchangePluginData(method, data);
                }
            }

            return null;
        }

        public Guid[] GetInstalledAddonModules()
        {
            List<Guid> moduleList = new List<Guid>();

            CallButler.Service.Plugin.CallButlerAddonModulePlugin[] addonModules = pluginManager.GetAllPluginsOfType<CallButler.Service.Plugin.CallButlerAddonModulePlugin>();

            foreach (CallButler.Service.Plugin.CallButlerAddonModulePlugin addon in addonModules)
            {
                moduleList.Add(addon.PluginID);
            }

            return moduleList.ToArray();
        }
        #endregion

        #region Scheduling Functions
        public void PlaceScheduleReminderCall(CallButlerAuthInfo authInfo, Guid extensionID, OutlookReminder[] reminders)
        {
            if (Authenticate(authInfo))
            {
                scriptService.PlaceScheduleReminderCall(extensionID, reminders);
            }
        }
        #endregion

        #region Call History Functions

        public void ClearCallHistory(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
            {
                dataProvider.ClearCallHistory(authInfo.CustomerID);
            }
        }

        public WOSI.CallButler.Data.CallButlerDataset.CallHistoryDataTable GetCallHistory(CallButlerAuthInfo authInfo)
        {
            return Authenticate(authInfo) ? dataProvider.GetCallHistory(authInfo.CustomerID) : null;
        }

        public WOSI.CallButler.Data.CallButlerDataset.CallHistoryDataTable GetRecentCalls(CallButlerAuthInfo authInfo, int count)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        #region Extension Functions
        public CallButlerDataset.ExtensionsDataTable GetExtension(CallButlerAuthInfo authInfo, Guid extensionID)
        {
            if (Authenticate(authInfo))
            {
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensionTable = new CallButlerDataset.ExtensionsDataTable();
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = dataProvider.GetExtension(authInfo.CustomerID, extensionID);

                if (extension != null)
                    extensionTable.ImportRow(extension);

                return extensionTable;
            }
            else
            {
                return null;
            }
        }

        public CallButlerDataset.ExtensionsDataTable GetExtensionNumber(CallButlerAuthInfo authInfo, int extensionNumber)
        {
            if (Authenticate(authInfo))
            {
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensionTable = new CallButlerDataset.ExtensionsDataTable();
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = dataProvider.GetExtensionNumber(authInfo.CustomerID, extensionNumber);

                if (extension != null)
                    extensionTable.ImportRow(extension);

                return extensionTable;
            }
            else
            {
                return null;
            }
        }

        public CallButlerDataset.ExtensionsDataTable GetExtensions(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
            {
                int maxExtensions = 100; // Licensing.Management.AppPermissions.StatGetPermissionScalar("Extensions");

                if (maxExtensions != 0)
                    return (CallButlerDataset.ExtensionsDataTable)TrimRows(dataProvider.GetExtensions(authInfo.CustomerID), maxExtensions);
                else
                    return dataProvider.GetExtensions(authInfo.CustomerID);
            }
            else
                return null;
        }

        public CallButlerDataset.LocalizedGreetingsDataTable GetExtensionVoicemailGreeting(CallButlerAuthInfo authInfo, Guid extensionID)
        {
            return Authenticate(authInfo) ? GetLocalizedGreeting(authInfo, extensionID, GetDefaultLanguage(authInfo)) : null;
        }

        public CallButlerDataset.ExtensionContactNumbersDataTable GetExtensionContactNumbers(CallButlerAuthInfo authInfo, Guid extensionID)
        {
            return Authenticate(authInfo) ? dataProvider.GetExtensionContactNumbers(extensionID) : null;
        }

        public void PersistExtensionContactNumbers(CallButlerAuthInfo authInfo, WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable changes)
        {
            if (Authenticate(authInfo) && IsDataValid(authInfo, changes))
            {
                dataProvider.UpdateExtensionContactNumbers(changes);
            }
        }

        public void PersistExtension(CallButlerAuthInfo authInfo, WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extension)
        {
            if (Authenticate(authInfo) && IsDataValid(authInfo, extension))
            {
                foreach (WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow eRow in extension)
                {
                    if (eRow.RowState != DataRowState.Deleted)
                    {
                        // Update our search number
                        string searchString = eRow.LastName;

                        if (searchString.Length == 0)
                            searchString = eRow.FirstName;

                        eRow.SearchNumber = WOSI.Utilities.StringUtils.ConvertStringToPhoneNumberString(searchString);
                        dataProvider.PersistExtension(eRow);
                    }
                }
            }
        }

        public void DeleteExtension(CallButlerAuthInfo authInfo, Guid extensionID)
        {
            if (Authenticate(authInfo))
            {
                dataProvider.DeleteExtension(authInfo.CustomerID, extensionID);

                // Delete the greeting for this extension if it exists
                DeleteGreeting(authInfo, extensionID);
            }
        }

        public void DeleteExtensionContactNumber(CallButlerAuthInfo authInfo, Guid extensionId, Guid extensionContactNumberId)
        {
            if (Authenticate(authInfo))
            {
                dataProvider.DeleteExtensionContactNumber(extensionId, extensionContactNumberId);
            }
        }

        #endregion

        #region Voicemail Functions
        public CallButlerDataset.VoicemailsDataTable GetVoicemails(CallButlerAuthInfo authInfo)
        {
            return Authenticate(authInfo) ? dataProvider.GetVoicemails(authInfo.CustomerID) : null;
        }

        public CallButlerDataset.VoicemailsDataTable GetVoicemails(CallButlerAuthInfo authInfo, Guid extensionID)
        {
            return Authenticate(authInfo) ? dataProvider.GetVoicemails(extensionID) : null;
        }

        public void PersistVoicemail(CallButlerAuthInfo authInfo, CallButlerDataset.VoicemailsDataTable voicemail)
        {
            if (Authenticate(authInfo) && IsDataValid(authInfo, voicemail))
            {
                List<Guid> extensions = new List<Guid>();

                foreach (CallButlerDataset.VoicemailsRow vRow in voicemail)
                {
                    if (vRow.RowState != DataRowState.Deleted)
                    {
                        dataProvider.PersistVoicemail(vRow);
                        extensions.Add(vRow.ExtensionID);
                    }
                }

                // Send a message waiting notification to our PBX phone
                if (registrarService != null)
                {
                    foreach (Guid extensionID in extensions)
                    {
                        registrarService.SendMessageWaitingNotification(extensionID);
                    }
                }
            }
        }

        public void PersistVoicemailSound(CallButlerAuthInfo authInfo, Guid extensionID, CallButlerDataset.ExtensionsDataTable extensions, byte[] soundBytes)
        {
            if (Authenticate(authInfo))
            {
                CallButlerDataset.ExtensionsDataTable extensionTable = GetExtension(authInfo, extensionID);


                if (extensionTable.Rows.Count > 0)
                {
                    CallButlerDataset.ExtensionsRow extensionRow = (CallButlerDataset.ExtensionsRow)extensionTable.Rows[0];
                    string username = extensionRow.FirstName + " " + extensionRow.LastName;
                    string vmDirectory = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.VoicemailRootDirectory);

                    if (!Directory.Exists(vmDirectory))
                        Directory.CreateDirectory(vmDirectory);

                    string fmt = "{0}\\{1}\\{2}.snd";

                    CallButlerDataset.VoicemailsDataTable voicemailTable = new CallButlerDataset.VoicemailsDataTable();

                    foreach (CallButlerDataset.ExtensionsRow extRow in extensions.Rows)
                    {
                        Guid voicemailID = Guid.NewGuid();
                        string vmFileName = String.Format(fmt, vmDirectory, extRow.ExtensionID, voicemailID);
                        WOSI.Utilities.FileUtils.SaveBytesToFile(vmFileName, soundBytes);

                        CallButlerDataset.VoicemailsRow row = voicemailTable.NewVoicemailsRow();

                        row.CallerDisplayName = username;
                        row.CallerHost = Services.PrivateLabelService.ReplaceProductName("CallButler");
                        row.CallerUsername = extensionRow.ExtensionNumber.ToString();
                        row.ExtensionID = extRow.ExtensionID;
                        row.IsNew = true;
                        row.Timestamp = System.DateTime.Now;
                        row.VoicemailID = voicemailID;

                        voicemailTable.Rows.Add(row);
                    }


                    PersistVoicemail(authInfo, voicemailTable);
                }
            }
        }
        
        public void DeleteVoicemail(CallButlerAuthInfo authInfo, Guid extensionID, Guid voicemailID)
        {
            if (Authenticate(authInfo))
            {
                vmService.DeleteVoicemail(extensionID, voicemailID);
            }
        }

        public byte[] GetVoicemailSound(CallButlerAuthInfo authInfo, Guid extensionID, Guid voicemailId)
        {
            if (Authenticate(authInfo))
            {
                return vmService.GetVoicemailBytes(extensionID, voicemailId);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Department Functions
        public CallButlerDataset.DepartmentsDataTable GetDepartments(CallButlerAuthInfo authInfo)
        {
            return Authenticate(authInfo) ? dataProvider.GetDepartments(authInfo.CustomerID) : null;
        }

        public void PersistDepartment(CallButlerAuthInfo authInfo, CallButlerDataset.DepartmentsDataTable department)
        {
            if (Authenticate(authInfo) && IsDataValid(authInfo, department))
            {
                foreach (WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow departmentRow in department)
                {
                    if (departmentRow.RowState != DataRowState.Deleted)
                    {
                        bool added = dataProvider.PersistDepartment(departmentRow);

                        if (!added)
                        {
                            // If this department is not a greeting department, make sure we delete any greetings that may have been associated with it
                            if (departmentRow.Type != (short)WOSI.CallButler.Data.DepartmentTypes.Greeting)
                                DeleteGreeting(authInfo, departmentRow.DepartmentID);
                        }
                    }
                }
            }
        }

        public void DeleteDepartment(CallButlerAuthInfo authInfo, Guid departmentID)
        {
            if (Authenticate(authInfo))
            {
                dataProvider.DeleteDepartment(authInfo.CustomerID, departmentID);

                // Delete any greetings with this department ID
                DeleteGreeting(authInfo, departmentID);
            }
        }
        #endregion

        #region Client Functions

        public CallButlerDataset.ExtensionsDataTable GetEmployeeExtension(CallButlerAuthInfo authInfo, int extension, string password)
        {
            if (Authenticate(authInfo))
            {
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensionsTable = GetExtensionNumber(authInfo, extension);

                if (extensionsTable.Count > 0)
                {
                    // If the passwords don't match, send an empty data table
                    if (extensionsTable[0].Password != password)
                    {
                        return new CallButlerDataset.ExtensionsDataTable();
                    }
                }

                return extensionsTable;
            }
            else
            {
                return null;
            }
        }

        public bool DoesEmployeeHaveNewVoicemails(CallButlerAuthInfo authInfo, Guid extensionId)
        {
            if (Authenticate(authInfo))
            {
                CallButlerDataset.VoicemailsDataTable voicemailTable = dataProvider.GetVoicemails(extensionId);

                if (voicemailTable.Select("IsNew = True").Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public CallButlerDataset.VoicemailsDataTable GetNewEmployeeVoicemails(CallButlerAuthInfo authInfo, Guid extensionId)
        {
            if (Authenticate(authInfo))
            {
                CallButlerDataset.VoicemailsDataTable vmTable = GetVoicemails(authInfo, extensionId);
                CallButlerDataset.VoicemailsDataTable newVmTable = new CallButlerDataset.VoicemailsDataTable();
                DataRow[] rows = vmTable.Select("IsNew=true");
                return MergeVoicemailDataRows(rows);
            }
            else
            {
                return null;
            }
        }

        private CallButlerDataset.VoicemailsDataTable MergeVoicemailDataRows(DataRow[] rows)
        {
            CallButlerDataset.VoicemailsDataTable vmTable = new CallButlerDataset.VoicemailsDataTable();
            foreach (DataRow row in rows)
            {
                vmTable.LoadDataRow(row.ItemArray, true);
            }
            return vmTable;
        }

        #endregion

        #region Greetings Functions
        public CallButlerDataset.LocalizedGreetingsDataTable GetLocalizedGreeting(CallButlerAuthInfo authInfo, Guid greetingID, string languageID)
        {
            if (Authenticate(authInfo))
            {
                CallButlerDataset.LocalizedGreetingsDataTable lgTable = new CallButlerDataset.LocalizedGreetingsDataTable();
                CallButlerDataset.LocalizedGreetingsRow localizedGreeting = dataProvider.GetLocalizedGreeting(authInfo.CustomerID, greetingID, languageID);

                if (localizedGreeting != null)
                    lgTable.ImportRow(localizedGreeting);

                return lgTable;
            }
            else
            {
                return null;
            }
        }

        public CallButlerDataset.LocalizedGreetingsDataTable GetLocalizedGreeting(CallButlerAuthInfo authInfo, Guid greetingID, Guid localizedGreetingID)
        {
            if (Authenticate(authInfo))
            {
                CallButlerDataset.LocalizedGreetingsDataTable lgTable = new CallButlerDataset.LocalizedGreetingsDataTable();
                CallButlerDataset.LocalizedGreetingsRow localizedGreeting = dataProvider.GetLocalizedGreeting(authInfo.CustomerID, greetingID, localizedGreetingID);

                if (localizedGreeting != null)
                    lgTable.ImportRow(localizedGreeting);

                return lgTable;
            }
            else
            {
                return null;
            }
        }

        public CallButlerDataset.LocalizedGreetingsDataTable GetLocalizedGreetingInDefaultLanguage(CallButlerAuthInfo authInfo, Guid greetingID)
        {
            return Authenticate(authInfo) ? GetLocalizedGreeting(authInfo, greetingID, GetDefaultLanguage(authInfo)) : null;
        }

        internal void PersistLocalizedGreeting(int customerID, WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable localizedGreeting)
        {
            string greetingDir = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingSoundRootDirectory);

            foreach (CallButlerDataset.LocalizedGreetingsRow lRow in localizedGreeting.Rows)
            {
                if (lRow.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                // Check to see if a greeting exists
                WOSI.CallButler.Data.CallButlerDataset.GreetingsRow greeting = dataProvider.GetGreeting(customerID, lRow.GreetingID);

                // If not, create one
                if (greeting == null)
                {
                    CallButlerDataset.GreetingsDataTable greetingTable = new CallButlerDataset.GreetingsDataTable();
                    greeting = greetingTable.NewGreetingsRow();
                    greeting.CustomerID = customerID;
                    greeting.GreetingID = lRow.GreetingID;

                    greetingTable.AddGreetingsRow(greeting);

                    dataProvider.PersistGreeting(greeting);
                }
                else
                {
                    // If a localized greeting already exists for this language, delete it
                    CallButlerDataset.LocalizedGreetingsRow exLg = dataProvider.GetLocalizedGreeting(customerID, lRow.GreetingID, lRow.LanguageID);

                    if (exLg != null)
                        dataProvider.DeleteLocalizedGreeting(customerID, exLg.LocalizedGreetingID);

                    if (greeting.CustomerID != customerID)
                        throw new Exception("Invalid Data found.");
                }

                // Now add our localized greeting
                bool added = dataProvider.PersistLocalizedGreeting(customerID, lRow);

                if (!added)
                {
                    // Delete any local greeting sounds if the greeting is a text greeting
                    if (lRow.Type == (short)GreetingType.TextGreeting)
                    {
                        string greetingFilename = greetingDir + "\\" + lRow.LanguageID + "\\" + lRow.GreetingID.ToString() + ".snd";

                        if (File.Exists(greetingFilename))
                            File.Delete(greetingFilename);
                    }
                }
            }
        }

        public void PersistLocalizedGreeting(CallButlerAuthInfo authInfo, WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable localizedGreeting)
        {
            if (Authenticate(authInfo))
            {
                PersistLocalizedGreeting(authInfo.CustomerID, localizedGreeting);
            }
        }

        public void DeleteGreeting(CallButlerAuthInfo authInfo, Guid greetingID)
        {
            if (Authenticate(authInfo))
            {
                dataProvider.DeleteGreeting(authInfo.CustomerID, greetingID);

                string greetingDirectory = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingSoundRootDirectory);

                if (Directory.Exists(greetingDirectory))
                {
                    // Delete any greeting files
                    string[] greetingFiles = Directory.GetFiles(greetingDirectory, greetingID + ".snd", SearchOption.AllDirectories);

                    foreach (string greetingFile in greetingFiles)
                    {
                        File.Delete(greetingFile);
                    }
                }
            }
        }

        public byte[] GetLocalizedGreetingSound(CallButlerAuthInfo authInfo, Guid greetingID, Guid localizedGreetingID)
        {
            if (Authenticate(authInfo))
            {
                // First, find our greeting
                WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow localizedGreeting = dataProvider.GetLocalizedGreeting(authInfo.CustomerID, greetingID, localizedGreetingID);

                if (localizedGreeting != null)
                {
                    string greetingFilename = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingSoundRootDirectory) + "\\" + localizedGreeting.LanguageID + "\\" + localizedGreeting.GreetingID.ToString() + ".snd";

                    if (File.Exists(greetingFilename))
                        return WOSI.Utilities.FileUtils.GetFileBytes(greetingFilename);
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public void PersistLocalizedGreetingSound(CallButlerAuthInfo authInfo, Guid greetingID, Guid localizedGreetingID, byte[] soundBytes)
        {
            if (Authenticate(authInfo))
            {
                // First, find our greeting
                WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow localizedGreeting = dataProvider.GetLocalizedGreeting(authInfo.CustomerID, greetingID, localizedGreetingID);

                if (localizedGreeting != null)
                {
                    string greetingDirectory = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingSoundRootDirectory) + "\\" + localizedGreeting.LanguageID;
                    string greetingFilename = greetingDirectory + "\\" + localizedGreeting.GreetingID.ToString() + ".snd";

                    if (!Directory.Exists(greetingDirectory))
                        Directory.CreateDirectory(greetingDirectory);

                    if (localizedGreeting.Data == "CallRecording")
                    {
                        string tmpGreetingFilename = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingRecordingTempPath) + "\\" + localizedGreeting.LanguageID + "\\" + greetingID.ToString() + ".snd";

                        if (File.Exists(tmpGreetingFilename))
                        {
                            File.Copy(tmpGreetingFilename, greetingFilename, true);
                            File.Delete(tmpGreetingFilename);
                        }
                    }
                    else
                    {
                        ConvertAndSaveSoundFile(greetingFilename, soundBytes);
                    }

                    // TODO: Add some code to check if the file is a valid sound file and not some sort of virus
                    // Create our new checksum
                    localizedGreeting.Data = WOSI.Utilities.CryptoUtils.GetFileChecksum(greetingFilename);

                    // Update our checksum
                    dataProvider.PersistLocalizedGreeting(authInfo.CustomerID, localizedGreeting);
                }
            }
        }
        #endregion

        #region Personalized Greeting Functions
        public CallButlerDataset.PersonalizedGreetingsDataTable GetPersonalizedGreetings(CallButlerAuthInfo authInfo)
        {
            return Authenticate(authInfo) ? dataProvider.GetPersonalizedGreetings(authInfo.CustomerID) : null;
        }

        public void PersistPersonalizedGreeting(CallButlerAuthInfo authInfo, CallButlerDataset.PersonalizedGreetingsDataTable personalizedGreeting)
        {
            if (Authenticate(authInfo) && IsDataValid(authInfo, personalizedGreeting))
            {
                foreach (CallButlerDataset.PersonalizedGreetingsRow row in personalizedGreeting)
                {
                    if (row.RowState != DataRowState.Deleted)
                    {
                        dataProvider.PersistPersonalizedGreeting(row);
                    }
                }
            }
        }

        public void DeletePersonalizedGreeting(CallButlerAuthInfo authInfo, Guid personalizedGreetingID)
        {
            if (Authenticate(authInfo))
            {
                dataProvider.DeletePersonalizedGreeting(authInfo.CustomerID, personalizedGreetingID);

                // Delete any greeting associated with this
                DeleteGreeting(authInfo, personalizedGreetingID);
            }
        }
        #endregion

        #region Provider Functions
        public CallButlerDataset.ProvidersDataTable GetProviders(CallButlerAuthInfo authInfo)
        {
            if (Authenticate(authInfo))
            {
                WOSI.CallButler.Data.CallButlerDataset.ProvidersDataTable providers = dataProvider.GetProviders(authInfo.CustomerID);

                // Fill in the status for each provider
                foreach (WOSI.CallButler.Data.CallButlerDataset.ProvidersRow provider in providers)
                {
                    provider.Status = telecomProvider.GetRegistrationState(provider.ProviderID);
                }

                return providers;
            }
            else
            {
                return null;
            }
        }

        public void PersistProviders(CallButlerAuthInfo authInfo, CallButlerDataset.ProvidersDataTable providers)
        {
            if (Authenticate(authInfo) && IsDataValid(authInfo, providers))
            {
                foreach (CallButlerDataset.ProvidersRow row in providers)
                {
                    if (row.RowState != DataRowState.Deleted)
                    {
                        dataProvider.PersistProvider(row);

                        // Register our provider
                        telecomProvider.Register(row.ProviderID, row);
                    }
                }
            }
        }

        public void DeleteProvider(CallButlerAuthInfo authInfo, Guid providerID)
        {
            if (Authenticate(authInfo))
            {
                // Unregister our provider
                telecomProvider.Unregister(providerID);

                dataProvider.DeleteProvider(authInfo.CustomerID, providerID);
            }
        }

        #endregion

        #region Script Schedule Functions
        public CallButlerDataset.ScriptSchedulesDataTable GetScriptSchedules(CallButlerAuthInfo authInfo)
        {
            return Authenticate(authInfo) ? dataProvider.GetScriptSchedules(authInfo.CustomerID) : null;
        }

        public void PersistScriptSchedule(CallButlerAuthInfo authInfo, CallButlerDataset.ScriptSchedulesDataTable scriptSchedules)
        {
            if (Authenticate(authInfo) && IsDataValid(authInfo, scriptSchedules))
            {
                foreach (WOSI.CallButler.Data.CallButlerDataset.ScriptSchedulesRow sRow in scriptSchedules)
                {
                    if (sRow.RowState != DataRowState.Deleted)
                    {
                        dataProvider.PersistScriptSchedule(authInfo.CustomerID, sRow);
                    }
                }
            }
        }

        public void DeleteScriptSchedule(CallButlerAuthInfo authInfo, Guid scriptScheduleID)
        {
            if (Authenticate(authInfo))
            {
                dataProvider.DeleteScriptSchedule(authInfo.CustomerID, scriptScheduleID);
            }
        }
        #endregion

        #region Outbound Call Functions
        public void PlaceGreetingRecordCall(CallButlerAuthInfo authInfo, string numberToCall, Guid greetingID, string languageID)
        {
            if (Authenticate(authInfo))
            {// First, find our greeting
                //WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow localizedGreeting = dataProvider.GetLocalizedGreeting(authInfo.CustomerID, greetingID, localizedGreetingID);
                scriptService.PlaceGreetingRecordCall(numberToCall, greetingID, languageID);
            }
        }
        #endregion

        private DataTable TrimRows(DataTable inputTable, int rowCount)
        {
            if (inputTable.Rows.Count > rowCount)
            {
                for (int index = rowCount - 1; index < inputTable.Rows.Count; index++)
                {
                    inputTable.Rows.RemoveAt(index);
                }
            }

            return inputTable;
        }

        private bool IsDataValid(CallButlerAuthInfo authInfo, DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0)
            {
                return true;
            }
            bool isValid = false;
            if (dataTable.Columns.Contains("CustomerID"))
            {
                DataRow[] rows = dataTable.Select("CustomerID <> " + authInfo.CustomerID);
                if (rows.Length == 0)
                {
                    isValid = true;
                }

            }
            else if (dataTable.Columns.Contains("ExtensionID"))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    Guid extensionID = Guid.Empty;

                    if (row.RowState == DataRowState.Deleted)
                    {
                        extensionID = (Guid)row["ExtensionID", DataRowVersion.Original];
                    }
                    else
                    {
                        extensionID = (Guid)row["ExtensionID"];
                    }
                    if (dataProvider.IsExtensionChildDataValid(authInfo.CustomerID, extensionID) == false)
                    {
                        isValid = false;
                        break;
                    }
                    else
                    {
                        isValid = true;
                    }
                }

            }
            else if (dataTable.Columns.Contains("GreetingID"))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    Guid greetingID = Guid.Empty;
                    if (row.RowState == DataRowState.Deleted)
                    {
                        greetingID = (Guid)row["GreetingID", DataRowVersion.Original];
                    }
                    else
                    {
                        greetingID = (Guid)row["GreetingID"];
                    }
                    if (dataProvider.IsGreetingChildDataValid(authInfo.CustomerID, greetingID) == false)
                    {
                        isValid = false;
                        break;
                    }
                    else
                    {
                        isValid = true;
                    }
                }
            }

            if (isValid == false)
            {
                //TODO: Change to more specific exception
                throw new Exception("Invalid Data found.");
            }
            else
            {
                return true;
            }
        }

        public void SendEmail(CallButlerAuthInfo authInfo, string sendTo, string subject, string message)
        {
            if (Authenticate(authInfo))
            {
                // Decrypt our SMTP password
                string smtpPassword = Properties.Settings.Default.SMTPPassword;

                if (smtpPassword.Length > 0)
                    smtpPassword = WOSI.Utilities.CryptoUtils.Decrypt(smtpPassword, WOSI.CallButler.Data.Constants.EncryptionPassword);

                WOSI.Utilities.EmailUtils.SendEmail(Properties.Settings.Default.SMTPEmailFrom, sendTo, subject, message, Properties.Settings.Default.SMTPServer, Properties.Settings.Default.SMTPPort, Properties.Settings.Default.SMTPUseSSL, Properties.Settings.Default.SMTPUsername, smtpPassword);
            }
        }

        public void SendTestEmail(CallButlerAuthInfo authInfo, string sendTo, string subject, string message, string smtpServer, int smtpPort, bool useSSL, string smtpUsername, string smtpPassword)
        {
            if (Authenticate(authInfo))
            {
                WOSI.Utilities.EmailUtils.SendEmail(Properties.Settings.Default.SMTPEmailFrom, sendTo, subject, message, smtpServer, smtpPort, useSSL, smtpUsername, smtpPassword);
            }
        }

        private bool Authenticate(CallButlerAuthInfo authInfo)
        {
            if (authInfo != null && Authenticate(authInfo.CustomerID, authInfo.ExtensionNumber, authInfo.Password))
            {
                return true;
            }
            else
            {
                throw new Exception("Failed to authenticate");
            }
        }

        public bool ReportErrors
        {
            get
            {
                return Properties.Settings.Default.ReportErrors;
            }
            set
            {
                Properties.Settings.Default.ReportErrors = value;
                Properties.Settings.Default.Save();
            }
        }
        public string GetHostedTestAddress(CallButlerAuthInfo authInfo)
        {
            return String.Empty;
        }

        public int GetCustomerLogin(string login, string managementPassword)
        {
            return -1;
        }

        public string [] GetHoldMusic(CallButlerAuthInfo authInfo)
        {
            string[] fileList = new string[0];
            
            if (Authenticate(authInfo))
            {
                string holdMusicLocation = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.HoldMusicRootDirectory);

                if (Directory.Exists(holdMusicLocation))
                {
                    fileList = Directory.GetFiles(holdMusicLocation);
                }
            }

            return fileList;
        }

        public void DeleteHoldMusic(CallButlerAuthInfo authInfo, string fileName)
        {
            if (Authenticate(authInfo))
            {
                string fileLocation = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.HoldMusicRootDirectory);
                fileLocation = Path.Combine(fileLocation, fileName);

                if (File.Exists(fileLocation))
                {
                    File.SetAttributes(fileLocation, FileAttributes.Normal);
                    File.Delete(fileLocation);
                }
            }

        }

        public void PersistHoldMusic(CallButlerAuthInfo authInfo, string fileName, byte[] soundBytes)
        {
            if (Authenticate(authInfo))
            {
                string outputPath = Path.Combine(WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.HoldMusicRootDirectory), Path.GetFileNameWithoutExtension(fileName) + ".wav");

                if (File.Exists(outputPath))
                {
                    File.SetAttributes(outputPath, FileAttributes.Normal);
                }

                ConvertAndSaveSoundFile(outputPath, soundBytes);
            }
        }

        private void ConvertAndSaveSoundFile(string filename, byte[] soundBytes)
        {
            // Save a temp file
            string tmpFilePath = Path.Combine(Path.GetTempPath(), "tmpsound.snd");
            WOSI.Utilities.FileUtils.SaveBytesToFile(tmpFilePath, soundBytes);

            // Convert the file to WAV
            WOSI.Utilities.SoundConversion.SoundConverter.ConvertToWAV(tmpFilePath, filename, telecomProvider.AudioInputRate, 16, false);

            // Delete our temp file
            File.Delete(tmpFilePath);
        }
        
        public bool Authenticate(int customerID, int extensionNumber, string password)
        {
            //For local version.
            bool isAuthenticated = false;

            if (customerID == Properties.Settings.Default.CustomerID)
            {
                if (extensionNumber == -1)
                {
                    if (PasswordIsValid(Properties.Settings.Default.ManagementPassword, password))
                    {
                        isAuthenticated = true;
                    }
                }
                else
                {
                    CallButlerDataset.ExtensionsRow eRow = dataProvider.GetExtensionNumber(customerID, extensionNumber);

                    if (eRow != null && PasswordIsValid(eRow.PBXPassword, password))
                    {
                        isAuthenticated = true;
                    }
                }
            }
            else
            {
                isAuthenticated = false;
            }

            return isAuthenticated;
        }

        private bool PasswordIsValid(string storedPassword, string suppliedPassword)
        {
            if (storedPassword.Equals(suppliedPassword))
                return true;
            else if (suppliedPassword.Length > 0)
            {
                if (suppliedPassword.Equals(WOSI.Utilities.CryptoUtils.Decrypt(storedPassword, WOSI.CallButler.Data.Constants.EncryptionPassword)))
                    return true;
            }

            return false;
        }
    }
}
