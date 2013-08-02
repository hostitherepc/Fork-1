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
using System.Data;
using System.IO;

namespace WOSI.CallButler.Data.DataProviders.Local
{
    public class CallButlerXmlDataProvider : CallButlerDataProviderBase
    {
        private string rootDataDirectory = "";
        private string rootGreetingSoundDirectory = "";
        private CallButlerDataset data;

        //private readonly object fileLock = new object();

        public override bool Connect(NameValueCollection settings)
        {
            rootDataDirectory = settings["RootDataDirectory"];
            rootGreetingSoundDirectory = settings["RootGreetingSoundDirectory"];

            data = new CallButlerDataset();

            // Load all of our data
            LoadDataset();

            isConnected = true;

            return true;
        }

        private void LoadDataset()
        {
            //lock (fileLock)
            //{
                FillFromDataFile("CallHistory.xml");
                FillFromDataFile("Departments.xml");
                FillFromDataFile("Extensions.xml");
                FillFromDataFile("Greetings.xml");
                FillFromDataFile("PersonalizedGreetings.xml");
                FillFromDataFile("Providers.xml");
                FillFromDataFile("ScriptSchedules.xml");

                data.AcceptChanges();
            //}
        }

        private void SaveDataTable(DataTable table, string filename)
        {
            //lock (fileLock)
            //{
                if (!Directory.Exists(rootDataDirectory))
                    Directory.CreateDirectory(rootDataDirectory);

                table.WriteXml(rootDataDirectory + "\\" + filename);
            //}
        }

        private void FillFromDataFile(string filename)
        {
            //lock (fileLock)
            //{
                try
                {
                    if (File.Exists(rootDataDirectory + "\\" + filename))
                        data.ReadXml(rootDataDirectory + "\\" + filename);
                }
                catch
                {

                }
            //}
        }

        private void SaveChanges()
        {
            // Now save our files
            if (data.CallHistory.GetChanges() != null)
            {
                SaveDataTable(data.CallHistory, "CallHistory.xml");
            }

            if (data.Departments.GetChanges() != null)
            {
                SaveDataTable(data.Departments, "Departments.xml");
            }

            if (data.Extensions.GetChanges() != null || data.ExtensionContactNumbers.GetChanges() != null || data.Voicemails.GetChanges() != null)
            {
                SaveDataTable(data.Extensions, "Extensions.xml");
            }

            if (data.Greetings.GetChanges() != null || data.LocalizedGreetings.GetChanges() != null)
            {
                SaveDataTable(data.Greetings, "Greetings.xml");
            }

            if (data.PersonalizedGreetings.GetChanges() != null)
            {
                SaveDataTable(data.PersonalizedGreetings, "PersonalizedGreetings.xml");
            }

            if (data.Providers.GetChanges() != null)
            {
                SaveDataTable(data.Providers, "Providers.xml");
            }

            if (data.ScriptSchedules.GetChanges() != null)
            {
                SaveDataTable(data.ScriptSchedules, "ScriptSchedules.xml");
            }

            if (data.HasChanges())
                RaiseChangesMadeEvent();

            data.AcceptChanges();
        }

        private void UpdateRowChanges(DataRow existingRow, DataRow updatedRow)
        {
            for (int index = 0; index < existingRow.ItemArray.Length; index++)
            {
                existingRow[index] = updatedRow[index];
            }
        }

        #region Script Schedule Functions
        public override CallButlerDataset.ScriptSchedulesDataTable GetScriptSchedules(int customerID)
        {
            CallButler.Data.CallButlerDataset.ScriptSchedulesDataTable scriptScheduleTable = new CallButlerDataset.ScriptSchedulesDataTable();

            scriptScheduleTable.Merge(data.ScriptSchedules);
            scriptScheduleTable.AcceptChanges();

            return scriptScheduleTable;
        }

        public override bool PersistScriptSchedule(int customerID, CallButlerDataset.ScriptSchedulesRow scriptSchedule)
        {
            bool added = false;
            CallButler.Data.CallButlerDataset.ScriptSchedulesRow existingRow = data.ScriptSchedules.FindByScriptScheduleID(scriptSchedule.ScriptScheduleID);

            // If the row doesn't exist, add it. Otherwise edit it
            if (existingRow == null)
            {
                // Add a new row
                data.ScriptSchedules.ImportRow(scriptSchedule);
                added = true;
            }
            else
            {
                // Update the row
                UpdateRowChanges(existingRow, scriptSchedule);
            }

            SaveChanges();

            return added;
        }

        public override void DeleteScriptSchedule(int customerID, Guid scriptScheduleID)
        {
            CallButler.Data.CallButlerDataset.ScriptSchedulesRow scriptSchedule = data.ScriptSchedules.FindByScriptScheduleID(scriptScheduleID);

            if (scriptSchedule != null)
            {
                scriptSchedule.Delete();
                SaveChanges();
            }
        }
        #endregion

        #region Call History Functions
        public override CallButler.Data.CallButlerDataset.CallHistoryDataTable GetCallHistory(int customerID)
        {
            CallButler.Data.CallButlerDataset.CallHistoryDataTable callHistoryTable = new CallButlerDataset.CallHistoryDataTable();

            callHistoryTable.Merge(data.CallHistory);
            callHistoryTable.AcceptChanges();

            return callHistoryTable;
        }

        public override void ClearCallHistory(int customerID)
        {
            data.CallHistory.Clear();
            SaveDataTable(data.CallHistory, "CallHistory.xml");
        }

        public override bool PersistCallHistory(CallButler.Data.CallButlerDataset.CallHistoryRow callHistory)
        {
            bool added = false;
            CallButler.Data.CallButlerDataset.CallHistoryRow existingRow = data.CallHistory.FindByCallID(callHistory.CallID);

            // If the row doesn't exist, add it. Otherwise edit it
            if (existingRow == null)
            {
                // Add a new row
                data.CallHistory.ImportRow(callHistory);
                added = true;
            }
            else
            {
                // Update the row
                UpdateRowChanges(existingRow, callHistory);
            }

            SaveChanges();

            return added;
        }
        #endregion

        #region Department Functions
        public override CallButler.Data.CallButlerDataset.DepartmentsDataTable GetDepartments(int customerID)
        {
            CallButler.Data.CallButlerDataset.DepartmentsDataTable departmentsTable = new CallButlerDataset.DepartmentsDataTable();

            departmentsTable.Merge(data.Departments);
            departmentsTable.AcceptChanges();

            return departmentsTable;
        }

        public override bool PersistDepartment(CallButler.Data.CallButlerDataset.DepartmentsRow department)
        {
            bool added = false;
            CallButler.Data.CallButlerDataset.DepartmentsRow existingRow = data.Departments.FindByDepartmentID(department.DepartmentID);

            // If the row doesn't exist, add it. Otherwise edit it
            if (existingRow == null)
            {
                // Add a new row
                data.Departments.ImportRow(department);
                added = true;
            }
            else
            {
                // Update the row
                UpdateRowChanges(existingRow, department);
            }

            SaveChanges();

            return added;
        }

        public override void DeleteDepartment(int customerID, Guid departmentID)
        {
            // Get our existing row
            CallButlerDataset.DepartmentsRow department = data.Departments.FindByDepartmentID(departmentID);

            if (department!= null)
            {
                department.Delete();
                SaveChanges();
            }
        }
        #endregion

        #region Extension Functions
        public override CallButler.Data.CallButlerDataset.ExtensionsRow GetExtension(int customerID, Guid extensionID)
        {
            return data.Extensions.FindByExtensionID(extensionID);
        }

        public override CallButler.Data.CallButlerDataset.ExtensionsRow GetExtensionNumber(int customerID, int extensionNumber)
        {
            CallButler.Data.CallButlerDataset.ExtensionsDataTable extensionsTable = new CallButlerDataset.ExtensionsDataTable();

            CallButler.Data.CallButlerDataset.ExtensionsRow[] extensions = (CallButler.Data.CallButlerDataset.ExtensionsRow[])data.Extensions.Select("ExtensionNumber = " + extensionNumber.ToString());

            if (extensions.Length > 0)
                return extensions[0];
            else
                return null;
        }

        public override CallButler.Data.CallButlerDataset.ExtensionsDataTable GetExtensions(int customerID)
        {
            CallButler.Data.CallButlerDataset.ExtensionsDataTable extensionsTable = new CallButlerDataset.ExtensionsDataTable();

            extensionsTable.Merge(data.Extensions);
            extensionsTable.AcceptChanges();

            return extensionsTable;
        }

        public override CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable GetExtensionContactNumbers(Guid extensionID)
        {
            CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable contactNumbersTable = new CallButlerDataset.ExtensionContactNumbersDataTable();

            CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow[] contactNumbers = (CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow[])data.ExtensionContactNumbers.Select("ExtensionID = '" + extensionID.ToString() + "'");

            foreach (CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow contactNumber in contactNumbers)
            {
                contactNumbersTable.ImportRow(contactNumber);
            }

            return contactNumbersTable;
        }

        public override void UpdateExtensionContactNumbers(CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable changes)
        {
            if (changes != null)
            {
                data.ExtensionContactNumbers.Merge(changes);

                SaveChanges();
            }
        }

        public override bool PersistExtension(CallButler.Data.CallButlerDataset.ExtensionsRow extension)
        {
            bool added = false;
            CallButler.Data.CallButlerDataset.ExtensionsRow existingRow = data.Extensions.FindByExtensionID(extension.ExtensionID);

            // If the row doesn't exist, add it. Otherwise edit it
            if (existingRow == null)
            {
                // Add a new row
                data.Extensions.ImportRow(extension);
                added = true;
            }
            else
            {
                // Update the row
                UpdateRowChanges(existingRow, extension);
            }

            SaveChanges();

            return added;
        }

        public override void DeleteExtension(int customerID, Guid extensionID)
        {
            CallButlerDataset.ExtensionsRow extension = data.Extensions.FindByExtensionID(extensionID);

            if (extension != null)
            {
                extension.Delete();
                SaveChanges();
            }
        }

        public override void DeleteExtensionContactNumber(Guid extensionId, Guid extensionContactNumberID)
        {
            CallButlerDataset.ExtensionContactNumbersRow row = data.ExtensionContactNumbers.FindByExtensionContactNumberID(extensionContactNumberID);
            if (row != null)
            {
                row.Delete();
                SaveChanges();
            }
            
        }
        #endregion

        #region Voicemail Functions
        public override CallButlerDataset.VoicemailsRow GetVoicemail(Guid extensionID, Guid voicemailID)
        {
            return data.Voicemails.FindByVoicemailID(voicemailID);
        }

        public override CallButlerDataset.VoicemailsDataTable GetVoicemails(Guid extensionID)
        {
            CallButler.Data.CallButlerDataset.VoicemailsDataTable voicemailTable = new CallButlerDataset.VoicemailsDataTable();

            CallButler.Data.CallButlerDataset.VoicemailsRow[] voicemails = (CallButler.Data.CallButlerDataset.VoicemailsRow[])data.Voicemails.Select("ExtensionID = '" + extensionID + "'");

            foreach (CallButler.Data.CallButlerDataset.VoicemailsRow voicemail in voicemails)
            {
                voicemailTable.ImportRow(voicemail);
            }

            return voicemailTable;
        }

        public override CallButlerDataset.VoicemailsDataTable GetVoicemails(int customerID)
        {
            CallButler.Data.CallButlerDataset.VoicemailsDataTable voicemailTable = new CallButlerDataset.VoicemailsDataTable();

            voicemailTable.Merge(data.Voicemails);
            voicemailTable.AcceptChanges();

            return voicemailTable;
        }

        public override bool PersistVoicemail(CallButler.Data.CallButlerDataset.VoicemailsRow voicemail)
        {
            bool added = false;
            CallButler.Data.CallButlerDataset.VoicemailsRow existingRow = data.Voicemails.FindByVoicemailID(voicemail.VoicemailID);

            // If the row doesn't exist, add it. Otherwise edit it
            if (existingRow == null)
            {
                // Add a new row
                data.Voicemails.ImportRow(voicemail);
                added = true;
            }
            else
            {
                // Update the row
                UpdateRowChanges(existingRow, voicemail);
            }

            SaveChanges();

            return added;
        }

        public override void DeleteVoicemail(Guid extensionID, Guid voicemailID)
        {
            CallButler.Data.CallButlerDataset.VoicemailsRow voicemail = data.Voicemails.FindByVoicemailID(voicemailID);

            if (voicemail != null)
            {
                voicemail.Delete();

                SaveChanges();
            }
        }

        public override int GetNewVoicemailCount(Guid extensionID)
        {
            return (int)data.Voicemails.Compute("COUNT(IsNew)", "ExtensionID = '" + extensionID.ToString() + "' AND IsNew = True");
        }

        public override int GetTotalVoicemailCount(Guid extensionID)
        {
            return (int)data.Voicemails.Compute("COUNT(ExtensionID)", "ExtensionID = '" + extensionID.ToString());
        }

        public override void MarkVoicemailRead(Guid extensionID, Guid voicemailID)
        {
            CallButler.Data.CallButlerDataset.VoicemailsRow voicemail = data.Voicemails.FindByVoicemailID(voicemailID);

            if (voicemail != null)
            {
                voicemail.IsNew = false;
                SaveChanges();
            }
        }
        #endregion

        #region Greeting Functions
        public override CallButler.Data.CallButlerDataset.GreetingsRow GetGreeting(int customerID, Guid greetingID)
        {
            return data.Greetings.FindByGreetingID(greetingID);
        }

        public override CallButler.Data.CallButlerDataset.LocalizedGreetingsRow GetLocalizedGreeting(int customerID, Guid greetingID, string languageID)
        {
            CallButler.Data.CallButlerDataset.LocalizedGreetingsRow[] localizedGreetings = (CallButler.Data.CallButlerDataset.LocalizedGreetingsRow[])data.LocalizedGreetings.Select("GreetingID = '" + greetingID.ToString() + "' AND LanguageID = '" + languageID + "'");

            if (localizedGreetings.Length > 0)
                return localizedGreetings[0];
            else
                return null;
        }

        public override CallButler.Data.CallButlerDataset.LocalizedGreetingsRow GetLocalizedGreeting(int customerID,Guid greetingID, Guid localizedGreetingID)
        {
            return data.LocalizedGreetings.FindByLocalizedGreetingID(localizedGreetingID);
        }

        public override bool PersistGreeting(CallButler.Data.CallButlerDataset.GreetingsRow greeting)
        {
            bool added = false;
            CallButler.Data.CallButlerDataset.GreetingsRow existingRow = data.Greetings.FindByGreetingID(greeting.GreetingID);

            // If the row doesn't exist, add it. Otherwise edit it
            if (existingRow == null)
            {
                // Add a new row
                data.Greetings.ImportRow(greeting);
                added = true;
            }
            else
            {
                // Update the row
                UpdateRowChanges(existingRow, greeting);
            }

            SaveChanges();

            return added;
        }

        public override void DeleteGreeting(int customerID, Guid greetingID)
        {
            CallButlerDataset.GreetingsRow greeting = data.Greetings.FindByGreetingID(greetingID);

            if (greeting != null)
            {
                greeting.Delete();
                SaveChanges();
            }
        }

        public override bool PersistLocalizedGreeting(int customerID, CallButler.Data.CallButlerDataset.LocalizedGreetingsRow localizedGreeting)
        {
            bool added = false;
            CallButler.Data.CallButlerDataset.LocalizedGreetingsRow existingRow = data.LocalizedGreetings.FindByLocalizedGreetingID(localizedGreeting.LocalizedGreetingID);

            // If the row doesn't exist, add it. Otherwise edit it
            if (existingRow == null)
            {
                // Add a new row
                data.LocalizedGreetings.ImportRow(localizedGreeting);
                added = true;
            }
            else
            {
                // Update the row
                UpdateRowChanges(existingRow, localizedGreeting);
            }

            SaveChanges();

            return added;
        }

        public override void DeleteLocalizedGreeting(int customerID, Guid localizedGreetingID)
        {
            CallButlerDataset.LocalizedGreetingsRow lGreeting = data.LocalizedGreetings.FindByLocalizedGreetingID(localizedGreetingID);

            if (lGreeting != null)
            {
                lGreeting.Delete();
                SaveChanges();
            }
        }
        #endregion

        #region Personalized Greeting Functions
        public override CallButler.Data.CallButlerDataset.PersonalizedGreetingsDataTable GetPersonalizedGreetings(int customerID)
        {
            CallButler.Data.CallButlerDataset.PersonalizedGreetingsDataTable pgTable = new CallButlerDataset.PersonalizedGreetingsDataTable();

            pgTable.Merge(data.PersonalizedGreetings);
            pgTable.AcceptChanges();

            return pgTable;
        }

        public override bool PersistPersonalizedGreeting(CallButlerDataset.PersonalizedGreetingsRow personalizedGreeting)
        {
            bool added = false;
            CallButler.Data.CallButlerDataset.PersonalizedGreetingsRow existingRow = data.PersonalizedGreetings.FindByPersonalizedGreetingID(personalizedGreeting.PersonalizedGreetingID);

            // If the row doesn't exist, add it. Otherwise edit it
            if (existingRow == null)
            {
                // Add a new row
                data.PersonalizedGreetings.ImportRow(personalizedGreeting);
                added = true;
            }
            else
            {
                // Update the row
                UpdateRowChanges(existingRow, personalizedGreeting);
            }

            SaveChanges();

            return added;
        }

        public override void DeletePersonalizedGreeting(int customerID, Guid personalizedGreetingID)
        {
            CallButler.Data.CallButlerDataset.PersonalizedGreetingsRow pgRow = data.PersonalizedGreetings.FindByPersonalizedGreetingID(personalizedGreetingID);

            if (pgRow != null)
            {
                pgRow.Delete();
                SaveChanges();
            }
        }
        #endregion

        #region Provider Functions

        public override CallButlerDataset.ProvidersDataTable GetProviders(int customerID)
        {
            CallButler.Data.CallButlerDataset.ProvidersDataTable providersTable = new CallButlerDataset.ProvidersDataTable();
            providersTable.Merge(data.Providers);
            providersTable.AcceptChanges();
            return providersTable;
        }

        public override bool PersistProvider(CallButlerDataset.ProvidersRow provider)
        {
             bool added = false;
            CallButler.Data.CallButlerDataset.ProvidersRow existingRow = data.Providers.FindByProviderID(provider.ProviderID);

            // If the row doesn't exist, add it. Otherwise edit it
            if (existingRow == null)
            {
                // Add a new row
                data.Providers.ImportRow(provider);
                added = true;
            }
            else
            {
                // Update the row
                UpdateRowChanges(existingRow, provider);
            }

            SaveChanges();

            return added;
        }

        public override void DeleteProvider(int customerID, Guid providerID)
        {
            CallButler.Data.CallButlerDataset.ProvidersRow pRow = data.Providers.FindByProviderID(providerID);

            if (pRow != null)
            {
                pRow.Delete();
                SaveChanges();
            }
        }

        #endregion

        #region Validation 

        private DataRow [] GetExtensionRows(int customerID)
        {
            return data.Extensions.Select("CustomerID = " + customerID);
        }

        private DataRow[] GetGreetingRows(int customerID)
        {
            return data.Greetings.Select("CustomerID = " + customerID);
        }

        public override bool IsExtensionChildDataValid(int customerID, Guid extensionID)
        {
            DataRow[] foundExtRows = GetExtensionRows(customerID);

            if (foundExtRows.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
            /*string qry = "ExtensionID = '{0}' and ExtensionContactNumberID = '{1}'";

            foreach (CallButlerDataset.ExtensionsRow row in foundExtRows)
            {

                DataRow[] fRows = data.ExtensionContactNumbers.Select(String.Format(qry, row.ExtensionID, extensionContactNumberID));

                if( fRows.Length > 0)
                {
                    return true;
                }
            }

            return false;
             * */

        }

        public override bool IsGreetingChildDataValid(int customerID, Guid greetingID)
        {
            DataRow[] foundExtRows = GetGreetingRows(customerID);

            if (foundExtRows.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            /*
            string qry = "ExtensionID = '{0}' and LocalizedGreetingID = '{1}'";

            foreach (CallButlerDataset.ExtensionsRow row in foundExtRows)
            {

                DataRow[] fRows = data.Voicemails.Select(String.Format(qry, row.ExtensionID, localizedGreetingID));

                if (fRows.Length > 0)
                {
                    return true;
                }
            }

            return false;
             */
        }

        #endregion

        #region Hosted Methods Not used for local
        public override int GetCustomerID(string telephoneNumber)
        {
            return 1;
        }

        public override string GetDefaultLanguage(int customerID)
        {
            return "";
        }

        public override void PersistDefaultLanguage(int customerID, string defaultLanguage)
        {
            
        }

        public override bool GetMultilingual(int customerID)
        {
            return false;    
        }

        public override void PersistMultilingual(int customerID, bool multilingual)
        {
            
        }

        public override void PersistLanguages(int customerID, string languages)
        {
        }

        public override string GetLanguages(int customerID)
        {
            return "";
        }

        public override int GetCustomerLogin(string login, string managementPassword)
        {
            return -1;
        }

        public override void PersistManagementPassword(int customerID, string password)
        {
        }

        public override string GetManagementPassword(int customerID)
        {
            return "";
        }

        public override string GetPermissionSet(int customerID)
        {
            return "";
        }

        public override string GetHostedTestAddress(int customerID)
        {
            return "";
        }


        public override bool GetFirstRun(int customerID)
        {
            return false;
        }

        public override void SetFirstRun(int customerID, bool val)
        {

        }

        public override int GetRecordVolume(int customerID)
        {
            return -1;
        }

        public override void PersistRecordVolume(int customerID, int recordVolume)
        {

        }

        public override int GetSpeechVolume(int customerID)
        {
            return -1;
        }

        public override void PersistSpeechVolume(int customerID, int speechVolume)
        {
            
        }

        public override int GetSoundVolume(int customerID)
        {
            return -1;
        }

        public override void PersistSoundVolume(int customerID, int speechVolume)
        {
            
        }

        public override string GetDefaultVoice(int customerID)
        {
            return "";
        }

        public override void PersistDefaultVoice(int customerID, string defaultVoice)
        {
            
        }

        #endregion

    }
}
