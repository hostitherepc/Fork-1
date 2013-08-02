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
    public partial class ExtensionsView : CallButler.Manager.ViewControls.ViewControlBase
    {
        private bool showVoicemailColumn = true;

        public ExtensionsView()
        {
            InitializeComponent();

            this.HelpRTFText = Properties.Resources.ExtensionsHelp;
        }

        private void btnAddNewExtension_Click(object sender, EventArgs e)
        {
            AddNewExtension();
        }

        internal void AddNewExtension()
        {
            // Check to make sure we can add a new extension
            int maxExtensions = 100;

            if (maxExtensions > 0 && maxExtensions <= callButlerDataset.Extensions.Count)
            {
                MessageBox.Show(this, String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ExtensionsView_ExtLimit), maxExtensions), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_PermissionDenied), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create a new Extension row
            WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = callButlerDataset.Extensions.NewExtensionsRow();

            extension.CustomerID = Properties.Settings.Default.CustomerID;
            extension.ExtensionID = Guid.NewGuid();
            short extensionNum = 0;

            // Find us an unused extension number
            for (short index = Properties.Settings.Default.MinExtensionNumber; index < short.MaxValue; index++)
            {
                if (callButlerDataset.Extensions.Select("ExtensionNumber = " + index).Length == 0)
                {
                    extensionNum = index;
                    extension.ExtensionNumber = index;
                    break;
                }
            }

            // Create a new voicemail greeting
            WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable voicemailGreetingTable = new WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable();
            WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow voicemailGreeting = voicemailGreetingTable.NewLocalizedGreetingsRow();
            voicemailGreeting.GreetingID = extension.ExtensionID;
            voicemailGreeting.LocalizedGreetingID = Guid.NewGuid();
            voicemailGreeting.LanguageID = ManagementInterfaceClient.ManagementInterface.GetDefaultLanguage(ManagementInterfaceClient.AuthInfo);
            voicemailGreeting.Type = (short)WOSI.CallButler.Data.GreetingType.SoundGreeting;
            voicemailGreetingTable.AddLocalizedGreetingsRow(voicemailGreeting);

            // Create a new extension contact numbers table
            WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable extensionContactNumbers = new WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable();

            // Show our extension editor form
            Forms.ExtensionEditorForm extensionForm = new CallButler.Manager.Forms.ExtensionEditorForm(callButlerDataset.Extensions, extension, extensionContactNumbers, voicemailGreeting);

            WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable origExt = (WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable) callButlerDataset.Extensions.Copy();

            if (extensionForm.ShowDialog(this) == DialogResult.OK)
            {
                // Check to make sure the extension isn't already taken
                if (origExt.Select("ExtensionNumber = " + extension.ExtensionNumber).Length > 0)
                {
                    MessageBox.Show(this, String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ExtensionView_ExtTaken), extension.ExtensionNumber, extensionNum), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ExtensionView_ExtConflict), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    extension.ExtensionNumber = extensionNum;
                }

                // Add our local extension
                callButlerDataset.Extensions.AddExtensionsRow(extension);

                // Add our remote extension
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensionDataTable = Utils.TableUtils<WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable>.CreateTableFromRow(extension);
                ManagementInterfaceClient.ManagementInterface.PersistExtension(ManagementInterfaceClient.AuthInfo, extensionDataTable);
                
                // Add our voicemail greeting
                ManagementInterfaceClient.ManagementInterface.PersistLocalizedGreeting(ManagementInterfaceClient.AuthInfo, voicemailGreetingTable);

                // Send our voicemail greeting sound file
                Utils.GreetingUtils.PersistLocalizedGreetingSound(voicemailGreeting);
                
                // Persist our contact numbers
                ManagementInterfaceClient.ManagementInterface.PersistExtensionContactNumbers(ManagementInterfaceClient.AuthInfo, extensionContactNumbers);
            }
        }

        private void EditExtension(WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension)
        {
            short currentExt = extension.ExtensionNumber;

            // Get our extension contact numbers
            WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable contactNumbersTable = ManagementInterfaceClient.ManagementInterface.GetExtensionContactNumbers(ManagementInterfaceClient.AuthInfo, extension.ExtensionID);

            // Get our extension greeting
            WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable voicemailGreetings = ManagementInterfaceClient.ManagementInterface.GetLocalizedGreeting(ManagementInterfaceClient.AuthInfo, extension.ExtensionID, ManagementInterfaceClient.ManagementInterface.GetDefaultLanguage(ManagementInterfaceClient.AuthInfo));
            WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow voicemailGreeting = null;

            if (voicemailGreetings.Count > 0)
            {
                voicemailGreeting = voicemailGreetings[0];

                // Download our greeting sound if it exists
                Utils.GreetingUtils.GetLocalizedGreetingSound(voicemailGreeting);
            }
            else
            {
                // If the greeting doesn't exist, create a new one
                voicemailGreeting = voicemailGreetings.NewLocalizedGreetingsRow();
                voicemailGreeting.GreetingID = extension.ExtensionID;
                voicemailGreeting.LocalizedGreetingID = Guid.NewGuid();
                voicemailGreeting.LanguageID = ManagementInterfaceClient.ManagementInterface.GetDefaultLanguage(ManagementInterfaceClient.AuthInfo);
                voicemailGreeting.Type = (short)WOSI.CallButler.Data.GreetingType.SoundGreeting;
                voicemailGreetings.AddLocalizedGreetingsRow(voicemailGreeting);
            }

            WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable origExt = (WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable) callButlerDataset.Extensions.Copy();


            // Show our extension editor form
            Forms.ExtensionEditorForm extensionForm = new CallButler.Manager.Forms.ExtensionEditorForm(callButlerDataset.Extensions, extension, contactNumbersTable, voicemailGreeting);

            if (extensionForm.ShowDialog(this) == DialogResult.OK)
            {
                // Check to make sure the extension isn't already taken
                if (currentExt != extension.ExtensionNumber && origExt.Select("ExtensionNumber = " + extension.ExtensionNumber).Length > 0)
                {
                    MessageBox.Show(this, String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ExtensionView_ExtTaken), extension.ExtensionNumber, currentExt), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ExtensionView_ExtConflict), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    extension.ExtensionNumber = currentExt;
                }

                // Edit our remote extension
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensionDataTable = Utils.TableUtils<WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable>.CreateTableFromRow(extension);
                ManagementInterfaceClient.ManagementInterface.PersistExtension(ManagementInterfaceClient.AuthInfo, extensionDataTable);

                // Edit our remote greeting
                ManagementInterfaceClient.ManagementInterface.PersistLocalizedGreeting(ManagementInterfaceClient.AuthInfo, voicemailGreetings);

                // Send our voicemail greeting sound file
                Utils.GreetingUtils.PersistLocalizedGreetingSound(voicemailGreeting);

                ManagementInterfaceClient.ManagementInterface.PersistExtensionContactNumbers(ManagementInterfaceClient.AuthInfo, contactNumbersTable);
            }
        }

        private void DeleteExtension(WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension)
        {
            if (MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ExtensionView_ConfirmDelete), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_ConfirmDelete), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Delete the remote extension
                ManagementInterfaceClient.ManagementInterface.DeleteExtension(ManagementInterfaceClient.AuthInfo, extension.ExtensionID);

                // Delete the local copy
                extension.Table.Rows.Remove(extension);
                //extension.Delete();
            }
        }

        protected override void OnLoadData()
        {
            // Load our data
            callButlerDataset.Extensions.Merge(ManagementInterfaceClient.ManagementInterface.GetExtensions(ManagementInterfaceClient.AuthInfo));
            callButlerDataset.Voicemails.Merge(ManagementInterfaceClient.ManagementInterface.GetVoicemails(ManagementInterfaceClient.AuthInfo));
            callButlerDataset.AcceptChanges();
        }

        private void dgExtensions_EditDataRow(object sender, CallButler.Manager.Controls.DataRowEventArgs e)
        {
            EditExtension((WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow)e.DataRow);
        }

        private void dgExtensions_DeleteDataRow(object sender, CallButler.Manager.Controls.DataRowEventArgs e)
        {
            DeleteExtension((WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow)e.DataRow);
        }

        private void dgExtensions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == NewMessagesColumn.Index)
            {
                // Get our new messages count
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = (WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow)((DataRowView)dgExtensions.Rows[e.RowIndex].DataBoundItem).Row;

                e.Value = callButlerDataset.Voicemails.Compute("COUNT(IsNew)", "ExtensionID = '" + extension.ExtensionID.ToString() + "' AND IsNew = True").ToString() + " new " + callButlerDataset.Voicemails.Compute("COUNT(IsNew)", "ExtensionID = '" + extension.ExtensionID.ToString() + "'").ToString() + " total";
            }
        }

        private void dgExtensions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == PlayMessagesColumn.Index)
            {
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = (WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow)((DataRowView)dgExtensions.Rows[e.RowIndex].DataBoundItem).Row;

                Forms.AnsweringMachineForm amForm = new CallButler.Manager.Forms.AnsweringMachineForm(callButlerDataset, extension.ExtensionID);

                amForm.ShowDialog(this);
            }
        }

        public bool ShowVoicemailColumn
        {
            get
            {
                return showVoicemailColumn;
            }
            set
            {
                showVoicemailColumn = value;
                NewMessagesColumn.Visible = value;
                PlayMessagesColumn.Visible = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataGridView DataGridView
        {
            get
            {
                return dgExtensions;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Guid SelectedExensionID
        {
            get
            {
                if (dgExtensions.SelectedRows.Count == 0)
                    return Guid.Empty;

                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = (WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow)((DataRowView)dgExtensions.SelectedRows[0].DataBoundItem).Row;

                return extension.ExtensionID;
            }
            set
            {
                // Select our extension
                for (int index = 0; index < dgExtensions.Rows.Count; index++)
                {
                    Guid extensionID = ((WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow)((DataRowView)dgExtensions.Rows[index].DataBoundItem).Row).ExtensionID;

                    if (extensionID == value)
                    {
                        dgExtensions.Rows[index].Selected = true;
                        dgExtensions.FirstDisplayedScrollingRowIndex = index;
                    }
                }
            }
        }
    }
}

