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
using WOSI.CallButler.Data;

namespace CallButler.Manager.ViewControls
{
    public partial class SummaryView : CallButler.Manager.ViewControls.ViewControlBase
    {
        public SummaryView()
        {
            InitializeComponent();

            callButlerDataset.Locale = System.Globalization.CultureInfo.InvariantCulture;

            this.EnableHelpIcon = false;

            quickTipsControl1.ChooseRandomPage();
        }

        protected override void OnLoadData()
        {
            // Load call history data
            callButlerDataset.Merge(ManagementInterfaceClient.ManagementInterface.GetCallHistory(ManagementInterfaceClient.AuthInfo));

            // Load up our call history
            UpdateCallHistoryView();
        }

        private void UpdateCallHistoryView()
        {
            CallButlerDataset.CallHistoryDataTable callHistoryTbl = callButlerDataset.CallHistory;
            CallButlerDataset.CallHistoryRow[] callHistory = (CallButlerDataset.CallHistoryRow []) callHistoryTbl.Select("", "Timestamp DESC");
            
            toolTip.RemoveAll();
            pnlCallHistory.Controls.Clear();

            // Only display as many as will fit on the screen
            const int labelHeight = 24;
            int maxItems = pnlCallHistory.Height / labelHeight;
            int totalItems = Math.Min(callHistory.Length, maxItems);

            pnlCallHistory.SuspendLayout();
            for(int index = totalItems-1; index >= 0; index--)
            {
                Label callItemLabel = CreateCallItemLabel();

                callItemLabel.Text = callHistory[index].Timestamp.ToShortDateString() + " " + callHistory[index].Timestamp.ToShortTimeString() + " - ";

                if (!callHistory[index].IsCallerDisplayNameNull() && callHistory[index].CallerDisplayName.Length > 0)
                    callItemLabel.Text += callHistory[index].CallerDisplayName;
                else
                    callItemLabel.Text += CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.SummaryView_UnkownCaller);

                if (!callHistory[index].IsCallerUsernameNull() && callHistory[index].CallerUsername.Length > 0)
                    callItemLabel.Text += " @ " + WOSI.Utilities.StringUtils.FormatPhoneNumber(callHistory[index].CallerUsername);

                toolTip.SetToolTip(callItemLabel, callItemLabel.Text);

                pnlCallHistory.Controls.Add(callItemLabel);
            }
            pnlCallHistory.ResumeLayout(true);

            //callHistoryTbl.Locale = new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.LCID);

            CallButlerDataset.CallHistoryRow[] calls = (CallButlerDataset.CallHistoryRow[])callHistoryTbl.Select("Timestamp >= #" + DateTime.Now.ToString("d", System.Globalization.CultureInfo.InvariantCulture) + " 00:00#");
            lblCallsToday.Text = calls.Length.ToString();

            DateTime startDate = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
            calls = (CallButlerDataset.CallHistoryRow[])callHistoryTbl.Select("Timestamp >= #" + DateTime.Now.ToString("d", System.Globalization.CultureInfo.InvariantCulture) + "#");
            lblCallsWeek.Text = calls.Length.ToString();

            DateTime now = DateTime.Now;
            startDate = new DateTime(now.Year, now.Month, 1);
            calls = (CallButlerDataset.CallHistoryRow[])callHistoryTbl.Select("Timestamp >= #" + DateTime.Now.ToString("d", System.Globalization.CultureInfo.InvariantCulture) + "#");
            lblCallsMonth.Text = calls.Length.ToString();

            lblCallsTotal.Text = callHistoryTbl.Count.ToString();
        }

        private Label CreateCallItemLabel()
        {
            Label label = new Label();

            label.AutoSize = false;
            label.Dock = DockStyle.Top;
            label.AutoEllipsis = true;
            label.Height = 24;

            return label;
        }

        private void pnlCallHistory_Resize(object sender, EventArgs e)
        {
            UpdateCallHistoryView();
        }

        private void btnViewVoicemail_Click(object sender, EventArgs e)
        {
            ((MainForm)this.ParentForm).LoadViewControl(new ExtensionsView());
        }

        private void btnViewCallHistory_Click(object sender, EventArgs e)
        {
            ((MainForm)this.ParentForm).LoadViewControl(new CallHistoryView());
        }
    }
}

