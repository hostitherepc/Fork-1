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
using global::WOSI.CallButler.Data;

namespace WOSI.CallButler.ManagementInterface
{
    [Serializable]
    public class AudioCodecInformation
    {
        public string Name = "";
        public bool Enabled = false;

        public override string ToString()
        {
            return Name;
        }
    }

    public interface ICallButlerManagementInterface
    {
        bool IsConnected { get;}

        bool ReportErrors{get;set;}

        #region Settings Functions
        bool GetMultilingual(CallButlerAuthInfo authInfo);
        void SetMultilingual(CallButlerAuthInfo authInfo, bool isMultilingual);

        string GetDefaultLanguage(CallButlerAuthInfo authInfo);
        void SetDefaultLanguage(CallButlerAuthInfo authInfo, string defLanguage);

        string GetLanguages(CallButlerAuthInfo authInfo);
        void SetLanguages(CallButlerAuthInfo authInfo, string languages);

        string GetApplicationPermissions(CallButlerAuthInfo authInfo);

        int LineCount { get;set;}

        string SMTPServer { get;set;}
        int SMTPPort { get;set;}
        bool SMTPUseSSL { get;set;}
        string SMTPUsername { get;set;}
        string SMTPPassword { get;set;}
        string SMTPFromEmail { get;set;}

        string[] GetTTSVoices();

        byte[] PrivateLabelData { get;}
        string ProductID { get;}
        string SplashInfo { get;}
        System.Collections.Specialized.NameValueCollection AvailableEditions { get;}
        void SetProductID(CallButlerAuthInfo authInfo, string productID);
        
        string ProductDescription { get;}

        string TelephoneNumberDescription { get;}

        string AdditionalCopyrightNotice { get;}
        
        string HoldMusicLocation { get; set;}

        int SIPPort { get;set;}
        bool EnableSTUN { get;set;}
        bool UseInternalAddressForSIP { get;set;}
        string STUNServer { get;set;}

        string LicenseName { get;set;}
        string LicenseKey { get; set;}
        DateTime LicenseExpiration { get;}
        bool IsLicensed { get;}
        bool IsFreeVersion { get;}
        DateTime TrialExpiration { get;}
        bool IsDownloadRegistered { get;set;}

        Version ServerVersion { get;}

        bool ExpertModeEnabled { get; set; }

        LogLevel LogLevel { get; set;}
        LogStorage LogStorage { get;set;}

        bool AllowRemoteManagement { get;set;}
       
        AudioCodecInformation[] GetAudioCodecs(CallButlerAuthInfo authInfo);
        void SetAudioCodecs(CallButlerAuthInfo authInfo, AudioCodecInformation[] codecs);

        void SetManagementPassword(CallButlerAuthInfo authInfo, string password);
        string GetManagementPassword(CallButlerAuthInfo authInfo);

        void SetAnswerTimeout(CallButlerAuthInfo authInfo, int answerTimeout);
        int GetAnswerTimeout(CallButlerAuthInfo authInfo);

        void SetWelcomeGreetingDelay(CallButlerAuthInfo authInfo, int delay);
        int GetWelcomeGreetingDelay(CallButlerAuthInfo authInfo);

        string GetLogErrorEmail(CallButlerAuthInfo authInfo);
        void SetLogErrorEmail(CallButlerAuthInfo authInfo, string emailAddress);

        bool GetSendLogErrorEmail(CallButlerAuthInfo authInfo);
        void SetSendLogErrorEmail(CallButlerAuthInfo authInfo, bool sendEmail);

        bool GetReceptionistEnabled(CallButlerAuthInfo authInfo);
        void SetReceptionistEnabled(CallButlerAuthInfo authInfo, bool enabled);

        Guid GetReceptionistExtension(CallButlerAuthInfo authInfo);
        void SetReceptionistExtension(CallButlerAuthInfo authInfo, Guid extensionID);

        string GetBusyRedirectServer(CallButlerAuthInfo authInfo);
        void SetBusyRedirectServer(CallButlerAuthInfo authInfo, string server);

        #region PBX Settings Functions
        int GetPBXRegistrationTimeout(CallButlerAuthInfo authInfo);
        void SetPBXRegistrationTimeout(CallButlerAuthInfo authInfo, int timeout);

        string GetPBXDialPrefix(CallButlerAuthInfo authInfo);
        void SetPBXDialPrefix(CallButlerAuthInfo authInfo, string dialPrefix);

        string GetPBXRegistrarDomain(CallButlerAuthInfo authInfo);
        void SetPBXRegistrarDomain(CallButlerAuthInfo authInfo, string domain);

        #endregion
        #endregion

        #region Outbound Call Functions
        void PlaceGreetingRecordCall(CallButlerAuthInfo authInfo, string numberToCall, Guid greetingID, string languageID);
        #endregion

        #region Service Functions
        void RestartService(CallButlerAuthInfo authInfo);
        #endregion

        #region Status Functions
        global::WOSI.CallButler.Data.CallButlerPhoneStatusDataset.PhoneStatusDataTable GetPhoneExtensionStatus(CallButlerAuthInfo authInfo);
        #endregion

        #region Plugin Functions
        Guid[] GetInstalledServicePlugins();
        Guid[] GetInstalledAddonModules();
        object ExchangePluginData(CallButlerAuthInfo authInfo, Guid pluginID, string method, object data);
        #endregion

        #region Scheduling Functions
        void PlaceScheduleReminderCall(CallButlerAuthInfo authInfo, Guid extensionID, OutlookReminder [] reminders);
        //void PlaceScheduleReminderCall(CallButlerAuthInfo authInfo, Guid [] extensionID, DateTime [] scheduleTime, string [] subject, string [] location);
        #endregion

        #region Call History Functions
        void ClearCallHistory(CallButlerAuthInfo authInfo);
        CallButlerDataset.CallHistoryDataTable GetCallHistory(CallButlerAuthInfo authInfo);
        CallButlerDataset.CallHistoryDataTable GetRecentCalls(CallButlerAuthInfo authInfo, int count);
        #endregion

        #region Extension Functions
        CallButlerDataset.ExtensionsDataTable GetExtensions(CallButlerAuthInfo authInfo);
        CallButlerDataset.ExtensionsDataTable GetExtensionNumber(CallButlerAuthInfo authInfo, int extensionNumber);
        CallButlerDataset.LocalizedGreetingsDataTable GetExtensionVoicemailGreeting(CallButlerAuthInfo authInfo, Guid extensionID);

        void PersistExtension(CallButlerAuthInfo authInfo, global::WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extension);
        void DeleteExtension(CallButlerAuthInfo authInfo, Guid extensionID);

        CallButlerDataset.ExtensionContactNumbersDataTable GetExtensionContactNumbers(CallButlerAuthInfo authInfo, Guid extensionID);
        void PersistExtensionContactNumbers(CallButlerAuthInfo authInfo, global::WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable changes);
        void DeleteExtensionContactNumber(CallButlerAuthInfo authInfo, Guid extensionId, Guid extensionContactNumberID);
        #endregion

        #region Voicemail Functions
        byte[] GetVoicemailSound(CallButlerAuthInfo authInfo,Guid extensionID, Guid voicemailId);
        CallButlerDataset.VoicemailsDataTable GetVoicemails(CallButlerAuthInfo authInfo, Guid extensionID);
        CallButlerDataset.VoicemailsDataTable GetVoicemails(CallButlerAuthInfo authInfo);
        void PersistVoicemail(CallButlerAuthInfo authInfo, CallButlerDataset.VoicemailsDataTable voicemail);
        void DeleteVoicemail(CallButlerAuthInfo authInfo, Guid extensionId, Guid voicemailID);
        #endregion

        #region Department Functions
        CallButlerDataset.DepartmentsDataTable GetDepartments(CallButlerAuthInfo authInfo);
        void PersistDepartment(CallButlerAuthInfo authInfo, CallButlerDataset.DepartmentsDataTable department);
        void DeleteDepartment(CallButlerAuthInfo authInfo, Guid departmentID);
        #endregion

        #region GreetingsFunctions
        void DeleteGreeting(CallButlerAuthInfo authInfo, Guid greetingID);

        CallButlerDataset.LocalizedGreetingsDataTable GetLocalizedGreeting(CallButlerAuthInfo authInfo, Guid greetingID, string languageID);
        CallButlerDataset.LocalizedGreetingsDataTable GetLocalizedGreeting(CallButlerAuthInfo authInfo, Guid greetingID, Guid localizedGreetingID);
        CallButlerDataset.LocalizedGreetingsDataTable GetLocalizedGreetingInDefaultLanguage(CallButlerAuthInfo authInfo, Guid greetingID);

        void PersistLocalizedGreeting(CallButlerAuthInfo authInfo, global::WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable localizedGreeting);

        byte[] GetLocalizedGreetingSound(CallButlerAuthInfo authInfo, Guid greetingID, Guid localizedGreetingID);
        void PersistLocalizedGreetingSound(CallButlerAuthInfo authInfo, Guid greetingID, Guid localizedGreetingID, byte[] soundBytes);
        #endregion

        #region Client Functions
        CallButlerDataset.ExtensionsDataTable GetEmployeeExtension(CallButlerAuthInfo authInfo, int extension, string password);
        CallButlerDataset.VoicemailsDataTable GetNewEmployeeVoicemails(CallButlerAuthInfo authInfo, Guid extensionId);
        bool DoesEmployeeHaveNewVoicemails(CallButlerAuthInfo authInfo, Guid extensionId);
        #endregion

        #region Personalized Greeting Functions
        CallButlerDataset.PersonalizedGreetingsDataTable GetPersonalizedGreetings(CallButlerAuthInfo authInfo);
        void PersistPersonalizedGreeting(CallButlerAuthInfo authInfo, CallButlerDataset.PersonalizedGreetingsDataTable personalizedGreeting);
        void DeletePersonalizedGreeting(CallButlerAuthInfo authInfo, Guid personalizedGreetingID);
        #endregion

        #region Provider Functions
        CallButlerDataset.ProvidersDataTable GetProviders(CallButlerAuthInfo authInfo);
        void PersistProviders(CallButlerAuthInfo authInfo, CallButlerDataset.ProvidersDataTable providers);
        void DeleteProvider(CallButlerAuthInfo authInfo, Guid providerID);
        #endregion

        #region Script Schedule Functions
        CallButlerDataset.ScriptSchedulesDataTable GetScriptSchedules(CallButlerAuthInfo authInfo);
        void PersistScriptSchedule(CallButlerAuthInfo authInfo, CallButlerDataset.ScriptSchedulesDataTable scriptSchedules);
        void DeleteScriptSchedule(CallButlerAuthInfo authInfo, Guid scriptScheduleID);
        #endregion

        void PersistVoicemailSound(CallButlerAuthInfo authInfo, Guid extensionID, CallButlerDataset.ExtensionsDataTable extensions, byte[] soundBytes);
        int GetCustomerLogin(string login, string managementPassword);

        string GetHostedTestAddress(CallButlerAuthInfo authInfo);

        bool GetFirstTimeRun(CallButlerAuthInfo authInfo);

        void SetFirstTimeRun(CallButlerAuthInfo authInfo, bool val);

        byte GetRecordVolume(CallButlerAuthInfo authInfo);

        void SetRecordVolume(CallButlerAuthInfo authInfo, byte recordVolume);

        byte GetSoundVolume(CallButlerAuthInfo authInfo);

        void SetSoundVolume(CallButlerAuthInfo authInfo, byte soundVolume);

        byte GetSpeechVolume(CallButlerAuthInfo authInfo);

        void SetSpeechVolume(CallButlerAuthInfo authInfo, byte speechVolume);

        string GetDefaultVoice(CallButlerAuthInfo authInfo);

        void SetDefaultVoice(CallButlerAuthInfo authInfo, string defaultVoice);

        void SendEmail( CallButlerAuthInfo authInfo, string sendTo, string subject, string message);

        void SendTestEmail(CallButlerAuthInfo authInfo, string sendTo, string subject, string message, string smtpServer, int smtpPort, bool useSSL, string smtpUsername, string smtpPassword);

        string [] GetHoldMusic(CallButlerAuthInfo authInfo);
        void DeleteHoldMusic(CallButlerAuthInfo authInfo, string fileName);
        void PersistHoldMusic(CallButlerAuthInfo authInfo, string fileName, byte[] soundBytes);
        

    }

}
