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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WOSI.Utilities;
using WOSI.CallButler.Data;
using Controls;

namespace CallButler.Manager.Controls
{
    public partial class ExtensionContactControl : UserControl
    {
        private string numberType;

        private CallButlerDataset.ExtensionContactNumbersDataTable _extContactTable;
        private CallButlerDataset.ExtensionsRow extension;
        private WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensions;
        private bool enableCallBlast;
        private bool enableCallIPPhone = true;

        /*public event EventHandler<ExtensionContactEventArgs> OnContactNumberChanged;
        public event EventHandler<ExtensionContactEventArgs> OnPriorityChanged;
        public event EventHandler<ExtensionContactEventArgs> OnTimezoneChanged;
        public event EventHandler<ExtensionContactEventArgs> OnTimeoutChanged;
        public event EventHandler<ExtensionContactEventArgs> OnScheduleChanged;
        public event EventHandler<ExtensionContactEventArgs> OnAddContactItem;
        public event EventHandler<ExtensionContactEventArgs> OnExtensionHoursCheckChanged;
        public event EventHandler<ExtensionContactEventArgs> OnDeleteContactItem;*/

        public ExtensionContactControl()
        {
            InitializeComponent();
        }

        public void LoadData(WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensions, CallButlerDataset.ExtensionsRow extension, CallButlerDataset.ExtensionContactNumbersDataTable extensionContactTable)
        {
            this.extensions = extensions;
            this.extension = extension;
            ExtensionContactTable = extensionContactTable;

            pnlFlow.Controls.Clear();

            // Create our voicemail shape
            VoicemailDiagramShape vds = new VoicemailDiagramShape();
            vds.Dock = DockStyle.Top;
            pnlFlow.Controls.Add(vds);

            WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow[] contactRows = (WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow[])ExtensionContactTable.Select("ExtensionID = '" + extension.ExtensionID + "'", "Priority ASC");

            foreach (WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow contactRow in contactRows)
            {
                AddContactControl(extensions, contactRow, false);
            }

            cbCallBlast.Checked = extension.UseCallBlast;

            UpdateCallBlast();
        }

        public string NumberTypeName
        {
            get
            {
                return numberType;
            }
            set
            {
                numberType = value;
            }
        }

        public bool EnableCallIPPhone
        {
            get
            {
                return enableCallIPPhone;
            }
            set
            {
                enableCallIPPhone = value;
            }
        }
        

        public bool EnableCallBlast
        {
            get
            {
                return enableCallBlast;
            }
            set
            {
                enableCallBlast = value;

                lblCallBlast.Visible = enableCallBlast;
                cbCallBlast.Visible = enableCallBlast;
            }
        }

        public WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable ExtensionContactTable
        {
            get
            {
                return _extContactTable;
            }
            private set
            {
                _extContactTable = value;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddContactItem();
        }

        private void AddContactControl(WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensions, WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow contactRow, bool scrollIntoView)
        {
            // Create our contact number shape
            FindMeNumberDiagramShape fmd = new FindMeNumberDiagramShape(extensions, contactRow);
            fmd.Dock = DockStyle.Top;
            fmd.NumberType = numberType;
            fmd.UsingCallBlast = cbCallBlast.Checked;
            fmd.EnableCallIPPhone = enableCallIPPhone;
            fmd.Visible = false;

            fmd.DeletePressed += new EventHandler(fmd_DeletePressed);
            fmd.MoveDownPressed += new EventHandler(fmd_MoveDownPressed);
            fmd.MoveUpPressed += new EventHandler(fmd_MoveUpPressed);
            fmd.SizeChanged += new EventHandler(fmd_SizeChanged);

            pnlFlow.Controls.Add(fmd);
            pnlFlow.Controls.SetChildIndex(fmd, 1);

            fmd.Visible = true;

            if (scrollIntoView)
            {
                fmd.Expanded = true;
                //pnlFlow.ScrollControlIntoView(fmd);
                //pnlFlow.AutoScrollPosition = new Point(pnlFlow.AutoScrollPosition.X, fmd.Top);
            }
        }

        void fmd_SizeChanged(object sender, EventArgs e)
        {
            pnlFlow.ScrollControlIntoView((FindMeNumberDiagramShape)sender);
        }

        void fmd_MoveUpPressed(object sender, EventArgs e)
        {
            ChangePriority((FindMeNumberDiagramShape)sender, -1);
        }

        void fmd_MoveDownPressed(object sender, EventArgs e)
        {
            ChangePriority((FindMeNumberDiagramShape)sender, 1);
        }

        void fmd_DeletePressed(object sender, EventArgs e)
        {
            DeleteContactItem((FindMeNumberDiagramShape)sender);
        }

        private void ChangePriority(FindMeNumberDiagramShape fmd, short prChange)
        {
            WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow contactRow = fmd.ContactRow;

            short oldPriority = contactRow.Priority;

            // Increase the priority of the current contactRow. Zero is highest priority.
            contactRow.Priority += prChange;

            // Get all or our contacts in order of priority
            WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow[] contactRows = (WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow[])ExtensionContactTable.Select("ExtensionID = '" + extension.ExtensionID + "'", "Priority ASC");

            foreach (WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow contact in contactRows)
            {
                if (contact != contactRow && contact.Priority >= contactRow.Priority)
                {
                    contact.Priority = oldPriority;
                    break;
                }
            }

            UpdatePriorityNumbers();

            int childIndex = pnlFlow.Controls.GetChildIndex(fmd);

            if ((prChange > 0 && childIndex > 1) || (prChange < 0 && childIndex < pnlFlow.Controls.Count))
            {
                childIndex -= prChange;
                pnlFlow.Controls.SetChildIndex(fmd, childIndex);
            }
        }

        private void AddContactItem()
        {
            // Update our current contact numbers priority and return the highest priority number
            short priorityNumber = UpdatePriorityNumbers();

            WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow contactRow = ExtensionContactTable.NewExtensionContactNumbersRow();

            contactRow.ExtensionContactNumberID = Guid.NewGuid();
            contactRow.ExtensionID = extension.ExtensionID;
            contactRow.Priority = priorityNumber;
            contactRow.Timeout = 20;

            ExtensionContactTable.AddExtensionContactNumbersRow(contactRow);

            AddContactControl(extensions, contactRow, true);
        }

        private void DeleteContactItem(FindMeNumberDiagramShape fmd)
        {
            WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow contactRow = fmd.ContactRow;

            Guid extensionId = contactRow.ExtensionID;
            Guid extContactId = contactRow.ExtensionContactNumberID;
            contactRow.Delete();

            pnlFlow.Controls.Remove(fmd);
        }

        private short UpdatePriorityNumbers()
        {
            // Get all or our contacts in order of priority
            WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow[] contactRows = (WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow[])ExtensionContactTable.Select("ExtensionID = '" + extension.ExtensionID + "'", "Priority ASC");

            short priorityIndex = 0;

            foreach (WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow contactRow in contactRows)
            {
                contactRow.Priority = priorityIndex;
                priorityIndex++;
            }

            return priorityIndex;
        }

        private void lblCallBlast_Click(object sender, EventArgs e)
        {
            cbCallBlast.Checked = !cbCallBlast.Checked;
        }

        private void cbCallBlast_CheckedChanged(object sender, EventArgs e)
        {
            extension.UseCallBlast = cbCallBlast.Checked;
            UpdateCallBlast();
        }

        private void UpdateCallBlast()
        {
            foreach (Control control in pnlFlow.Controls)
            {
                if (control is FindMeNumberDiagramShape)
                {
                    ((FindMeNumberDiagramShape)control).UsingCallBlast = cbCallBlast.Checked;
                }
            }
        }
    }

    /*public class ExtensionContactEventArgs : System.EventArgs
    {
        public CallButlerDataset.ExtensionContactNumbersRow ContactRow;

        public ExtensionContactEventArgs(CallButlerDataset.ExtensionContactNumbersRow contactRow)
        {
            this.ContactRow = contactRow;
        }
    }*/
}
