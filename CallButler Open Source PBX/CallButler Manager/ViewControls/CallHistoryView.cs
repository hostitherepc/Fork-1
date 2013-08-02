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
using System.Drawing.Printing;
using CallButler.Manager.Utils;
using CallButler.Manager.Forms;
using Utilities;
using WOSI.Utilities;
using Utilities.ContactManagement;

namespace CallButler.Manager.ViewControls
{
    public partial class CallHistoryView : CallButler.Manager.ViewControls.ViewControlBase
    {
        bool _firstTime = true;

        private string _currentSort;
        private ContactManagerBase _contactManager;

        public CallHistoryView()
        {
            InitializeComponent();

            cboFilterBy.SelectedIndex = 0;
            CurrentSort = this.bsCallHistory.Sort;

            ContactManager = ContactManagerFactory.CreateContactManager(ContactType.Outlook);
            ContactManager.OnContactManagerFailureEvent += new ContactManagerBase.ContactManagerFailureEventHandler(ContactManager_OnContactManagerFailureEvent);
            SetupOutlook();

            this.EnableHelpIcon = false;
        }

        void ContactManager_OnContactManagerFailureEvent(object source, Exception ex)
        {
            ToggleOutlookOptions(false);
        }

        private ContactManagerBase ContactManager
        {
            get
            {
                return _contactManager;
            }
            set
            {
                _contactManager = value;
            }
        }

        private void ToggleOutlookOptions(bool visible)
        {
            btnImportOutlook.Enabled = visible;
            dgCalls.Columns["ViewInOutlook"].Visible = visible;
        }

        private void SetupOutlook()
        {
            if (ContactManager.IsInstalled)
            {
                ToggleOutlookOptions(true);
            }
            else
            {
                ToggleOutlookOptions(false);
            }
        }

        private bool FirstTime
        {
            get
            {
                return _firstTime;
            }
            set
            {
                _firstTime = value;
            }
        }

        protected override void OnLoadData()
        {
            callHistoryDataset.Merge(ManagementInterfaceClient.ManagementInterface.GetCallHistory(ManagementInterfaceClient.AuthInfo));

            //LoadMissingContacts();

        }

        private void LoadMissingContacts()
        {
            if (ContactManager.IsInstalled)
            {
                foreach (WOSI.CallButler.Data.CallButlerDataset.CallHistoryRow row in callHistoryDataset.CallHistory.Rows)
                {
                    if (row.CallerDisplayName.Length == 0)
                    {
                        IContactItem item = ContactManager.SearchContact(row.CallerUsername);
                        if (ContactManager.IsInstalled == false)
                        {
                            break;
                        }
                        if (item != null)
                        {
                            row.CallerDisplayName = item.FullName;
                        }
                    }
                }
            }
        }

        private void dgCalls_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == callerUsernameDataGridViewTextBoxColumn.Index)
            {
                e.Value = WOSI.Utilities.StringUtils.FormatPhoneNumber((string)e.Value);
            }
            else if (e.ColumnIndex == ToUsername.Index && e.Value != DBNull.Value)
            {
                e.Value = WOSI.Utilities.StringUtils.FormatPhoneNumber((string)e.Value);
            }
            else if (e.ColumnIndex == callDurationDataGridViewTextBoxColumn.Index && e.Value != null)
            {

                if (e.Value is TimeSpan)
                {
                    TimeSpan t = (TimeSpan)e.Value;
                    e.Value = string.Format("{0:d2}:{1:d2}", t.Minutes, t.Seconds);
                }
            }
        }

        private void btnSaveCallHistory_Click(object sender, EventArgs e)
        {
            SaveCallHistory();
        }

        private void SaveCallHistory()
        {
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                callHistoryDataset.CallHistory.WriteXml(saveFileDialog.FileName);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        private void cboFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        private void UpdateFilter()
        {
            if (FirstTime || txtSearch.Text.Length == 0)
            {
                bsCallHistory.Filter = "";
            }
            else
            {
                string searchString = "CallerDisplayName LIKE '";

                if (cboFilterBy.SelectedIndex == 1)
                {
                    searchString = "CallerUsername LIKE '";
                }

                searchString += txtSearch.Text + "*'";

                bsCallHistory.Filter = searchString;
            }
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            if (FirstTime)
            {
                FirstTime = false;
                txtSearch.Text = String.Empty;
                txtSearch.ForeColor = this.ForeColor;
            }
        }

        private void btnClearCriteria_Click(object sender, EventArgs e)
        {
            txtSearch.Text = String.Empty;
        }

        private void btnPrintHistory_Click(object sender, EventArgs e)
        {
            DataRow[] rows = SelectProperGridRows(this.callHistoryDataset.CallHistory);

            ReportPrinter p = new ReportPrinter("CallButlerDataset_CallHistory", rows, "CallButler.Manager.Reports.CallHistoryReport.rdlc", this);
            p.OnPrintPageSelectionEvent += new ReportPrinter.PrintPageSelectionEventHandler(p_OnPrintPageSelectionEvent);
            p.Print();
        }

        private void p_OnPrintPageSelectionEvent(object source, PrintPageEventArgs e)
        {
            ReportPrinter p = source as ReportPrinter;

            if (p != null)
            {
                DataRow[] rows = GetSelectedGridRows();
                p.DataSource = rows;
                p.FinalizePrint(e);
            }
        }

        private DataRow[] SelectProperGridRows(WOSI.CallButler.Data.CallButlerDataset.CallHistoryDataTable historyTable)
        {
            return historyTable.Select(this.bsCallHistory.Filter, CurrentSort);
        }

        private DataRow[] GetSelectedGridRows()
        {
            DataGridViewSelectedRowCollection selectedRows = this.dgCalls.SelectedRows;

            WOSI.CallButler.Data.CallButlerDataset ds = new WOSI.CallButler.Data.CallButlerDataset();
            ds.CallHistory.Columns.Add("OriginalIndex", typeof(int));

            foreach (DataGridViewRow row in selectedRows)
            {
                DataRowView rv = row.DataBoundItem as DataRowView;
                DataRow newRow = ds.CallHistory.LoadDataRow(rv.Row.ItemArray, true);
                newRow["OriginalIndex"] = row.Index;
            }

            DataRow[] rows = SelectProperGridRows(ds.CallHistory);

            return rows;
        }

        private string CurrentSort
        {
            get
            {
                return _currentSort;
            }
            set
            {
                _currentSort = value;
            }
        }

        private void dgCalls_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string text = dgCalls.Columns[e.ColumnIndex].DataPropertyName.ToString();
            text += StringUtils.GetProperSortText(dgCalls.SortOrder);
            CurrentSort = text;
        }

        private void btnImportOutlook_Click(object sender, EventArgs e)
        {
            LoadSelectedContacts();
        }

        private void LoadSelectedContacts()
        {
            if (ContactManager.IsInstalled)
            {
                WOSI.CallButler.Data.CallButlerDataset.CallHistoryRow[] rows = (WOSI.CallButler.Data.CallButlerDataset.CallHistoryRow[])GetSelectedGridRows();

                foreach (WOSI.CallButler.Data.CallButlerDataset.CallHistoryRow row in rows)
                {
                    IContactItem item = ContactManager.SearchContact(row.CallerUsername, row.CallerDisplayName);
                    if (item == null)
                    {
                        item = ContactItemFactory.CreateContactItem(ContactManager, row.CallerDisplayName, row.CallerUsername);

                    }
                    ContactManager.ShowContactForm(item);

                    if (row.Table.Columns["OriginalIndex"] != null)
                    {
                        WOSI.CallButler.Data.CallButlerDataset.CallHistoryRow cRow = GetUnderlyingRow(dgCalls.Rows[Convert.ToInt32(row["OriginalIndex"])]);
                        if (item != null)
                        {
                            cRow.CallerDisplayName = item.FullName;
                        }
                    }
                }
            }
        }

        private WOSI.CallButler.Data.CallButlerDataset.CallHistoryRow GetUnderlyingRow(DataGridViewRow row)
        {
            DataRowView rv = (DataRowView)row.DataBoundItem;
            WOSI.CallButler.Data.CallButlerDataset.CallHistoryRow callRow = (WOSI.CallButler.Data.CallButlerDataset.CallHistoryRow)rv.Row;
            return callRow;
        }

        private void ViewInContactManager(DataGridViewRow row)
        {
            WOSI.CallButler.Data.CallButlerDataset.CallHistoryRow callRow = GetUnderlyingRow(row);

            IContactItem item = ContactManager.SearchContact(callRow.CallerUsername, callRow.CallerDisplayName);

            if (item == null || (callRow.IsCallerUsernameNull() || callRow.CallerUsername.Length == 0) && (callRow.IsCallerDisplayNameNull() || callRow.CallerDisplayName.Length == 0))
            {
                if (MessageBox.Show(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallHistoryView_ContactNotFound), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallHistoryView_NoContactFound), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    item = ContactItemFactory.CreateContactItem(ContactManager);
                    item.FullName = callRow.CallerDisplayName;
                    item.BusinessTelephoneNumber = StringUtils.FormatPhoneNumber(callRow.CallerUsername);
                    ContactManager.ShowContactForm(item);
                }
            }
            else
            {
                ContactManager.ShowContactForm(item);
            }

            /*if (item != null)
            {
                callRow.CallerDisplayName = item.FullName;
            }*/
        }

        private void dgCalls_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ContactManager.IsInstalled)
            {
                if (dgCalls.Columns[e.ColumnIndex].Name.Equals("ViewInOutlook"))
                {
                    ViewInContactManager(dgCalls.Rows[e.RowIndex]);
                }
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallHistoryView_ConfirmClear), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_ConfirmDelete), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ManagementInterfaceClient.ManagementInterface.ClearCallHistory(ManagementInterfaceClient.AuthInfo);
                callHistoryDataset.CallHistory.Clear();
            }
        }
    }
}

