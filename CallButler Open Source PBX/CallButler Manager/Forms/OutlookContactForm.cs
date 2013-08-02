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
using Utilities;
using Utilities.ContactManagement;

namespace CallButler.Manager.Forms
{
  public partial class OutlookContactForm : CallButler.Manager.Controls.CallButlerDialogFormBase
  {
    public OutlookContactForm()
    {
      InitializeComponent();

      Cursor.Current = Cursors.WaitCursor;

      try
      {
        ContactManagerBase cm = ContactManagerFactory.CreateContactManager(ContactType.Outlook);

        List<IContactItem> contactItems = cm.GetAllContacts();
        this.bsContacts.DataSource = contactItems;
        //foreach (IContactItem contact in contactItems)
        //{
        //  Data.ContactListDataset.ContactRow contactRow = contactListDataset.Contact.AddContactRow(contact.FirstName, contact.LastName, contact.PrimaryTelephoneNumber, contact.Email1Address);
        // }
      }
      catch (System.Exception e)
      {
      }

      Cursor.Current = Cursors.Default;

      Utils.PrivateLabelUtils.ReplaceProductNameControl(this);
    }

    public bool MultiSelect
    {
      get
      {
        return dgContacts.MultiSelect;
      }
      set
      {
        dgContacts.MultiSelect = value;
      }
    }

    public IContactItem[] SelectedContacts
    {
      get
      {
        List<IContactItem> selectedContacts = new List<IContactItem>();

        foreach( DataGridViewRow selRow in dgContacts.SelectedRows)
        {
          selectedContacts.Add((IContactItem)selRow.DataBoundItem);
        }

        return selectedContacts.ToArray();
      }
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
      if (txtSearch.Text.Length > 0)
      {
        bsContacts.Filter = "FirstName LIKE '" + txtSearch.Text + "*' OR LastName LIKE '" + txtSearch.Text + "*'";
      }
      else
      {
        bsContacts.Filter = "";
      }
    }

    private void dgContacts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.ColumnIndex == ContactImage.Index)
      {
        e.Value = Properties.Resources.id_card_24;
      }
    }

    private void dgContacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
  }
}