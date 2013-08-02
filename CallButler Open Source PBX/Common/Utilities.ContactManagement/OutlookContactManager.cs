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
using System.Runtime.InteropServices;
using System.Reflection;
using Microsoft.Win32;
using System.Diagnostics;
using Microsoft.Office.Interop.Outlook;

namespace Utilities.ContactManagement
{
    public class OutlookContactManager : ContactManagerBase
    {
        private AddinExpress.Outlook.SecurityManager olSecurityManager;
        private Microsoft.Office.Interop.Outlook.ApplicationClass _outlookApplication;
        private bool errorCreatingOutlookApp = false;

        public OutlookContactManager()
        {
            if (this.IsInstalled)
            {
                olSecurityManager = new AddinExpress.Outlook.SecurityManager();

                try
                {
                    _outlookApplication = new Microsoft.Office.Interop.Outlook.ApplicationClass();
                }
                catch 
                {
                    errorCreatingOutlookApp = true;
                    return;
                }

                try
                {
                    olSecurityManager.ConnectTo(_outlookApplication);
                }
                catch { }
            }
        }

        public override bool IsInstalled
        {
            get
            {
                bool isInstalled = false;
                try
                {
                    RegistryKey k = Registry.ClassesRoot.OpenSubKey(@"\Outlook.Application\CLSID");

                    if (k != null)
                    {
                        string idString = (string)k.GetValue("");

                        k = Registry.ClassesRoot.OpenSubKey(@"\CLSID\" + idString + @"\LocalServer32");

                        if (k != null)
                        {
                            isInstalled = true;
                        }
                    }
                }
                catch
                {
                }
                if (isInstalled)
                {
                    if (IsRunning == false)
                    {
                        isInstalled = false;
                    }
                }

                if (errorCreatingOutlookApp)
                {
                    isInstalled = false;
                }

                return isInstalled;
            }
        }

        public bool IsRunning
        {
            get
            {
                if (Process.GetProcessesByName("OUTLOOK").Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public Microsoft.Office.Interop.Outlook.ApplicationClass OutlookApplication
        {
            get
            {
                return _outlookApplication;
            }
        }

        public override IContactItem SearchContact(string phoneNumber)
        {
            return SearchContact(phoneNumber, "");
        }

        public override List<IContactItem> GetAllContacts()
        {
            try
            {
                ToggleSecurityWarning(true);

                Microsoft.Office.Interop.Outlook.Items contactItems = GetBaseContacts();

                List<IContactItem> lst = new List<IContactItem>();

                foreach (Microsoft.Office.Interop.Outlook.ContactItem item in contactItems)
                {
                    lst.Add(ContactItemFactory.CreateContactItem(this, item));
                }
                return lst;
            }
            catch (System.Exception ex)
            {
                FireContactManagerFailureEvent(ex);
                return null;
            }
            finally
            {
                ToggleSecurityWarning(false);
            }
        }

        private Microsoft.Office.Interop.Outlook.Items GetBaseContacts()
        {
            try
            {
                Microsoft.Office.Interop.Outlook.NameSpace ns = OutlookApplication.GetNamespace("MAPI");
                Microsoft.Office.Interop.Outlook.MAPIFolder contactsFolder = ns.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderContacts);
                Microsoft.Office.Interop.Outlook.Items contactItems = contactsFolder.Items;
                return contactItems;
            }
            catch (System.Exception ex)
            {
                FireContactManagerFailureEvent(ex);
                return null;
            }
        }

        private string _query;

        private string Query
        {
            get
            {
                if (_query == null)
                {
                    _query = BuildQuery();
                }
                return _query;
            }
        }

        private string BuildQuery()
        {
            PropertyInfo[] pis = typeof(Microsoft.Office.Interop.Outlook.ContactItemClass).GetProperties();

            List<string> fields = new List<string>();

            foreach (PropertyInfo pi in pis)
            {
                string name = pi.Name.Replace("get_", "").Replace("set_", "");
                if (name.Contains("TelephoneNumber"))
                {
                    fields.Add(name);
                }
            }

            StringBuilder builder = new StringBuilder();
            string fmt = "[{0}] = '{1}' or ";
            foreach (string s in fields)
            {
                builder.Append(String.Format(fmt, s, "{0}"));
            }

            builder.Append(" [FullName] = '{1}'");

            string ret = builder.ToString();
            return ret;
        }

        public override IContactItem SearchContact(string phoneNumber, string fullName)
        {
            try
            {
                ToggleSecurityWarning(true);

                Microsoft.Office.Interop.Outlook.Items contactItems = GetBaseContacts();

                string formattedPhone = WOSI.Utilities.StringUtils.FormatPhoneNumber(phoneNumber);
                //string qry = "[BusinessTelephoneNumber] = '{0}' or [FullName] = '{1}' or [BusinessTelephoneNumber] = '{2}'";

                string firstPassQry = String.Format(Query, phoneNumber, fullName);
                Microsoft.Office.Interop.Outlook.ContactItem contactItem = contactItems.Find(firstPassQry) as Microsoft.Office.Interop.Outlook.ContactItem;

                if (contactItem == null)
                {
                    string secondPassQry = String.Format(Query, formattedPhone, fullName);
                    contactItem = contactItems.Find(secondPassQry) as Microsoft.Office.Interop.Outlook.ContactItem;
                }

                IContactItem outlookItem = null;
                if (contactItem != null)
                {
                    outlookItem = ContactItemFactory.CreateContactItem(this, contactItem);
                }

                return outlookItem;
            }
            catch (System.Exception ex)
            {
                FireContactManagerFailureEvent(ex);
                return null;
            }
            finally
            {
                ToggleSecurityWarning(false);
            }
        }

        public override void ToggleSecurityWarning(bool disableWarning)
        {
            if (olSecurityManager != null)
            {
                olSecurityManager.DisableOOMWarnings = disableWarning;
            }
        }

        public override void ShowContactForm(IContactItem contact)
        {
            try
            {
                ToggleSecurityWarning(true);
                Microsoft.Office.Interop.Outlook.ContactItem item = contact.BaseContact as Microsoft.Office.Interop.Outlook.ContactItem;

                if (item != null)
                {
                    item.Display(true);
                    contact.SyncValuesFromBase();
                }
            }
            catch (System.Exception ex)
            {
                FireContactManagerFailureEvent(ex);
            }
            finally
            {
                ToggleSecurityWarning(false);
            }
        }
    }
}
