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
using System.Reflection;
using Microsoft.Office.Interop.Outlook;

namespace Utilities.ContactManagement
{
    public class OutlookContactItem : IContactItem
    {
        public OutlookContactItem(Microsoft.Office.Interop.Outlook.ContactItem baseItem)
        {
            _baseContact = baseItem;
            SyncValuesFromBase();
        }

        private Microsoft.Office.Interop.Outlook.ContactItem _baseContact;

        [OutlookSync]
        public string FirstName
        {
            get
            {
                return _baseContact.FirstName;
            }
            set
            {
                _baseContact.FirstName = value;
            }
        }

        [OutlookSync]
        public string LastName
        {
            get
            {
                return _baseContact.LastName;// _lastName;
            }
            set
            {
                _baseContact.LastName = value;
            }
        }

        [OutlookSync]
        public string PrimaryTelephoneNumber
        {
            get
            {
                return _baseContact.PrimaryTelephoneNumber;
            }
            set
            {
                _baseContact.PrimaryTelephoneNumber = value;
            }
        }

        [OutlookSync]
        public string Email1Address
        {
            get
            {
                return _baseContact.Email1Address;
            }
            set
            {
                _baseContact.Email1Address = value;
            }
        }

        [OutlookSync]
        public string FullName
        {
            get
            {
                return _baseContact.FullName;
            }
            set
            {
                _baseContact.FullName = value;
            }
        }

        [OutlookSync]
        public string BusinessTelephoneNumber
        {
            get
            {
                return _baseContact.BusinessTelephoneNumber;
            }
            set
            {
                _baseContact.BusinessTelephoneNumber = value;
            }
        }

        public object BaseContact
        {
            get
            {
                return _baseContact;
            }
            //set
            //{
            //  _baseContact = (Outlook.ContactItem) value;
            //  if (_baseContact != null)
            //  {
            //     SyncValuesFromBase();
            //  }
            //}
        }

        public void SyncValuesFromContact()
        {
            Microsoft.Office.Interop.Outlook.ContactItem item = BaseContact as Microsoft.Office.Interop.Outlook.ContactItem;
            PropertyInfo[] props = this.GetType().GetProperties();

            foreach (PropertyInfo pi in props)
            {
                string name = pi.Name.Replace("set_", "");
                object[] attribs = pi.GetCustomAttributes(typeof(OutlookSync), false);

                if (attribs.Length > 0)
                {
                    SyncValue(pi.Name, pi.GetValue(this, null));
                }
            }
        }

        public void SyncValuesFromBase()
        {
            Microsoft.Office.Interop.Outlook.ContactItem item = BaseContact as Microsoft.Office.Interop.Outlook.ContactItem;
            PropertyInfo[] props = this.GetType().GetProperties();

            foreach (PropertyInfo pi in props)
            {
                string name = pi.Name.Replace("set_", "");
                object[] attribs = pi.GetCustomAttributes(typeof(OutlookSync), false);

                if (attribs.Length > 0)
                {
                    Microsoft.Office.Interop.Outlook.ItemProperty oProp = item.ItemProperties[name];
                    if (oProp != null)
                    {
                        pi.SetValue(this, oProp.Value, null);
                    }
                }
            }
        }

        private void SyncValue(string methodName, object value)
        {
            methodName = methodName.Replace("set_", "");
            Microsoft.Office.Interop.Outlook.ContactItem item = BaseContact as Microsoft.Office.Interop.Outlook.ContactItem;

            if (item != null)
            {
                Microsoft.Office.Interop.Outlook.ItemProperty prop = item.ItemProperties[methodName];

                if (prop != null)
                {
                    prop.Value = value;
                }
            }
        }
    }

    public class OutlookSync : Attribute
    {

    }
}
