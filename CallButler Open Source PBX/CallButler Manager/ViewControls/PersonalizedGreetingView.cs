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
    public partial class PersonalizedGreetingView : CallButler.Manager.ViewControls.ViewControlBase
    {
        public PersonalizedGreetingView()
        {
            InitializeComponent();

            this.HelpRTFText = Properties.Resources.CallPersonalizationHelp;
        }

        protected override void OnLoadData()
        {
            callButlerDataset.Merge(ManagementInterfaceClient.ManagementInterface.GetPersonalizedGreetings(ManagementInterfaceClient.AuthInfo));
            callButlerDataset.Merge(ManagementInterfaceClient.ManagementInterface.GetExtensions(ManagementInterfaceClient.AuthInfo));
        }

        internal void AddPersonalizedGreeting()
        {
            // Create a new personalized greeting row and table
            WOSI.CallButler.Data.CallButlerDataset.PersonalizedGreetingsDataTable pgTable = new WOSI.CallButler.Data.CallButlerDataset.PersonalizedGreetingsDataTable();
            WOSI.CallButler.Data.CallButlerDataset.PersonalizedGreetingsRow pgRow = pgTable.NewPersonalizedGreetingsRow();
            pgRow.PersonalizedGreetingID = Guid.NewGuid();
            pgRow.Type = (short)WOSI.CallButler.Data.PersonalizedGreetingType.Continue;
            pgTable.AddPersonalizedGreetingsRow(pgRow);

            // Create a new localized greeting row and table
            WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable lgTable = new WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable();
            WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow lgRow = lgTable.NewLocalizedGreetingsRow();
            lgRow.LocalizedGreetingID = Guid.NewGuid();
            lgRow.GreetingID = pgRow.PersonalizedGreetingID;
            lgRow.LanguageID = ManagementInterfaceClient.ManagementInterface.GetDefaultLanguage( ManagementInterfaceClient.AuthInfo );
            lgRow.Type = (short)WOSI.CallButler.Data.GreetingType.SoundGreeting;
            lgTable.AddLocalizedGreetingsRow(lgRow);

            Forms.PersonalizedGreetingForm pgForm = new CallButler.Manager.Forms.PersonalizedGreetingForm(pgRow, callButlerDataset.Extensions);

            pgForm.GreetingControl.LoadGreeting(lgRow, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache));

            if (pgForm.ShowDialog(this) == DialogResult.OK)
            {
                pgForm.GreetingControl.SaveGreeting(WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache));

                // Add remotely
                ManagementInterfaceClient.ManagementInterface.PersistPersonalizedGreeting(ManagementInterfaceClient.AuthInfo, pgTable);
                ManagementInterfaceClient.ManagementInterface.PersistLocalizedGreeting(ManagementInterfaceClient.AuthInfo, lgTable);

                // Send our localized greeting sound file
                Utils.GreetingUtils.PersistLocalizedGreetingSound(lgRow);

                // Add locally
                callButlerDataset.PersonalizedGreetings.ImportRow(pgRow);

                callButlerDataset.AcceptChanges();
            }
        }

        private void EditPersonalizedGreeting(WOSI.CallButler.Data.CallButlerDataset.PersonalizedGreetingsRow pgRow)
        {
            // Get our localized greeting
            WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable lgTable = ManagementInterfaceClient.ManagementInterface.GetLocalizedGreetingInDefaultLanguage(ManagementInterfaceClient.AuthInfo, pgRow.PersonalizedGreetingID);

            if (lgTable.Count == 0)
            {
                // If no localized greeting exists, add a new one
                WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow lgRow = lgTable.NewLocalizedGreetingsRow();
                lgRow.LocalizedGreetingID = Guid.NewGuid();
                lgRow.GreetingID = pgRow.PersonalizedGreetingID;
                lgRow.LanguageID = ManagementInterfaceClient.ManagementInterface.GetDefaultLanguage(ManagementInterfaceClient.AuthInfo);
                lgRow.Type = (short)WOSI.CallButler.Data.GreetingType.SoundGreeting;
                lgTable.AddLocalizedGreetingsRow(lgRow);
            }

            Forms.PersonalizedGreetingForm pgForm = new CallButler.Manager.Forms.PersonalizedGreetingForm(pgRow, callButlerDataset.Extensions);

            pgForm.GreetingControl.LoadGreeting(lgTable[0], WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache));

            if (pgForm.ShowDialog(this) == DialogResult.OK)
            {
                pgForm.GreetingControl.SaveGreeting(WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache));

                // Persist remotely
                ManagementInterfaceClient.ManagementInterface.PersistPersonalizedGreeting(ManagementInterfaceClient.AuthInfo, Utils.TableUtils<WOSI.CallButler.Data.CallButlerDataset.PersonalizedGreetingsDataTable>.CreateTableFromRow(pgRow));
                ManagementInterfaceClient.ManagementInterface.PersistLocalizedGreeting(ManagementInterfaceClient.AuthInfo, lgTable);

                // Send our localized greeting sound file
                Utils.GreetingUtils.PersistLocalizedGreetingSound(lgTable[0]);

                callButlerDataset.AcceptChanges();
            }
        }

        private void DeletePersonalizedGreeting(WOSI.CallButler.Data.CallButlerDataset.PersonalizedGreetingsRow prRow)
        {
            if (MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.PersonalizedGreetingView_ConfirmDelete), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_ConfirmDelete), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ManagementInterfaceClient.ManagementInterface.DeletePersonalizedGreeting(ManagementInterfaceClient.AuthInfo, prRow.PersonalizedGreetingID);

                // Delete our local row
                prRow.Delete();

                callButlerDataset.AcceptChanges();
            }
        }

        private void btnAddPersonalizedGreeting_Click(object sender, EventArgs e)
        {
            AddPersonalizedGreeting();   
        }

        private void dgPersonalizedGreetings_EditDataRow(object sender, CallButler.Manager.Controls.DataRowEventArgs e)
        {
            EditPersonalizedGreeting((WOSI.CallButler.Data.CallButlerDataset.PersonalizedGreetingsRow)e.DataRow);
        }

        private void dgPersonalizedGreetings_DeleteDataRow(object sender, CallButler.Manager.Controls.DataRowEventArgs e)
        {
            DeletePersonalizedGreeting((WOSI.CallButler.Data.CallButlerDataset.PersonalizedGreetingsRow)e.DataRow);
        }
    }
}

