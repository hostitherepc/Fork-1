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
using System.Collections.Specialized;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Data;

namespace WOSI.CallButler.Data.DataProviders
{
    public class NetworkCredentialEventArgs : EventArgs
    {
        public NetworkCredential Credentials;
        public bool Cancel = false;

        public NetworkCredentialEventArgs(NetworkCredential credentials, bool cancel)
        {
            this.Credentials = credentials;
            this.Cancel = cancel;
        }
    }

    public class ConnectionFailureEventArgs : EventArgs
    {
        public string FailureReason = "";

        public ConnectionFailureEventArgs(string failureReason)
        {
            this.FailureReason = failureReason;
        }
    }

    public abstract class CallButlerDataProviderBase
    {
        public event EventHandler<NetworkCredentialEventArgs> CredentialsRequested;
        public event EventHandler<ConnectionFailureEventArgs> ConnectionFailure;
        public event EventHandler ChangesMade;

        protected bool isConnected = false;

        public CallButlerDataProviderBase()
        {
        }

        public virtual bool Connect(NameValueCollection settings)
        {
            isConnected = true;
            return true;
        }

        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
        }

        public static DataTable GetChildTable(DataTable parentTable, string childTableName)
        {
            foreach (DataRelation relation in parentTable.ChildRelations)
            {
                if (string.Compare(relation.ChildTable.TableName, childTableName, true) == 0)
                    return relation.ChildTable;
            }

            return null;
        }

        protected void RaiseCredentialsRequestedEvent(NetworkCredentialEventArgs e)
        {
            if (CredentialsRequested != null)
                CredentialsRequested(this, e);
        }

        protected void RaiseConnectionFailureEvent(ConnectionFailureEventArgs e)
        {
            if (ConnectionFailure != null)
                ConnectionFailure(this, e);
        }

        protected void RaiseChangesMadeEvent()
        {
            if (ChangesMade != null)
                ChangesMade(this, EventArgs.Empty);
        }

        #region Script Schedule Functions
        public abstract CallButler.Data.CallButlerDataset.ScriptSchedulesDataTable GetScriptSchedules(int customerID);
        public abstract bool PersistScriptSchedule(int customerID, CallButler.Data.CallButlerDataset.ScriptSchedulesRow scriptSchedule);
        public abstract void DeleteScriptSchedule(int customerID, Guid scriptScheduleID);
        #endregion

        #region Call History Functions
        public abstract CallButler.Data.CallButlerDataset.CallHistoryDataTable GetCallHistory(int customerID);
        public abstract void ClearCallHistory(int customerID);
        public abstract bool PersistCallHistory(CallButler.Data.CallButlerDataset.CallHistoryRow callHistory);
        #endregion

        #region Department Functions
        public abstract CallButler.Data.CallButlerDataset.DepartmentsDataTable GetDepartments(int customerID);
        public abstract bool PersistDepartment(CallButler.Data.CallButlerDataset.DepartmentsRow department);
        public abstract void DeleteDepartment(int customerID, Guid departmentID);
        #endregion

        #region Extension Functions
        public abstract CallButler.Data.CallButlerDataset.ExtensionsRow GetExtension(int customerID, Guid extensionID);
        public abstract CallButler.Data.CallButlerDataset.ExtensionsRow GetExtensionNumber(int customerID, int extensionNumber);
        public abstract CallButler.Data.CallButlerDataset.ExtensionsDataTable GetExtensions(int customerID);

        public abstract CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable GetExtensionContactNumbers(Guid extensionID);
        public abstract void UpdateExtensionContactNumbers(CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable changes);

        public abstract bool PersistExtension(CallButler.Data.CallButlerDataset.ExtensionsRow extension);
        public abstract void DeleteExtension(int customerID, Guid extensionID);
        public abstract void DeleteExtensionContactNumber(Guid extensionId, Guid extensionContactNumberID);

        #endregion

        #region Voicemail Functions
        public abstract CallButlerDataset.VoicemailsRow GetVoicemail(Guid extensionID, Guid voicemailID);
        public abstract CallButlerDataset.VoicemailsDataTable GetVoicemails(Guid extensionID);
        public abstract CallButlerDataset.VoicemailsDataTable GetVoicemails(int customerID);
        public abstract bool PersistVoicemail(CallButler.Data.CallButlerDataset.VoicemailsRow voicemail);
        public abstract void DeleteVoicemail(Guid extensionID, Guid voicemailID);

        public abstract int GetNewVoicemailCount(Guid extensionID);
        public abstract int GetTotalVoicemailCount(Guid extensionID);
        public abstract void MarkVoicemailRead(Guid extensionID, Guid voicemailID);
        #endregion

        #region Greeting Functions
        public abstract CallButler.Data.CallButlerDataset.GreetingsRow GetGreeting(int customerID, Guid greetingID);
        public abstract CallButler.Data.CallButlerDataset.LocalizedGreetingsRow GetLocalizedGreeting(int customerID, Guid greetingID, string languageID);
        public abstract CallButler.Data.CallButlerDataset.LocalizedGreetingsRow GetLocalizedGreeting(int customerID, Guid greetingID, Guid localizedGreetingID);

        public abstract bool PersistGreeting(CallButler.Data.CallButlerDataset.GreetingsRow greeting);
        public abstract void DeleteGreeting(int customerID, Guid greetingID);

        public abstract bool PersistLocalizedGreeting(int customerID, CallButler.Data.CallButlerDataset.LocalizedGreetingsRow localizedGreeting);
        public abstract void DeleteLocalizedGreeting(int customerID, Guid localizedGreetingID);
        #endregion

        #region Personalized Greeting Functions
        public abstract CallButler.Data.CallButlerDataset.PersonalizedGreetingsDataTable GetPersonalizedGreetings(int customerID);
        public abstract bool PersistPersonalizedGreeting(CallButlerDataset.PersonalizedGreetingsRow personalizedGreeting);
        public abstract void DeletePersonalizedGreeting(int customerID, Guid personalizedGreetingID);
        #endregion

        #region Provider Functions
        public abstract CallButlerDataset.ProvidersDataTable GetProviders(int customerID);
        public abstract bool PersistProvider(CallButlerDataset.ProvidersRow provider);
        public abstract void DeleteProvider(int customerID, Guid providerID);
        #endregion

        #region Validation
        public abstract bool IsExtensionChildDataValid(int customerID, Guid extensionID);
        public abstract bool IsGreetingChildDataValid(int customerID, Guid greetingID);
        #endregion

        public abstract int GetCustomerID(string telephoneNumber);

        public abstract string GetDefaultLanguage(int customerID);

        public abstract void PersistDefaultLanguage(int customerID, string defaultLanguage);

        public abstract bool GetMultilingual(int customerID);

        public abstract void PersistMultilingual(int customerID, bool multilingual);

        public abstract void PersistLanguages(int customerID, string languages);

        public abstract string GetLanguages(int customerID);

        //public abstract bool IsCustomerValid(int customerID, string managementPassword);

        public abstract int GetCustomerLogin(string login, string managementPassword);


        public abstract void PersistManagementPassword(int customerID, string password);

        public abstract string GetManagementPassword(int customerID);

        public abstract string GetPermissionSet(int customerID);

        public abstract string GetHostedTestAddress(int customerID);

        public abstract bool GetFirstRun(int customerID);

        public abstract void SetFirstRun(int customerID, bool val);

        public abstract int GetRecordVolume(int customerID);

        public abstract void PersistRecordVolume(int customerID, int recordVolume);

        public abstract int GetSpeechVolume(int customerID);

        public abstract void PersistSpeechVolume(int customerID, int speechVolume);

        public abstract int GetSoundVolume(int customerID);

        public abstract void PersistSoundVolume(int customerID, int soundVolume);

        public abstract string GetDefaultVoice(int customerID);

        public abstract void PersistDefaultVoice(int customerID, string defaultVoice);
        
    }
}
