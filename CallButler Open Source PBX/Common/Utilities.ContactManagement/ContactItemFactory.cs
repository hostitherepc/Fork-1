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
using Microsoft.Office.Interop.Outlook;

namespace Utilities.ContactManagement
{
    public class ContactItemFactory
    {
        public static IContactItem CreateContactItem(ContactManagerBase mgr)
        {
            return CreateContactItem(mgr, null);
        }

        public static IContactItem CreateContactItem(ContactManagerBase mgr, string fullName, string businessPhone)
        {
            try
            {
                mgr.ToggleSecurityWarning(true);
                IContactItem item = CreateContactItem(mgr);
                item.FullName = fullName;
                item.BusinessTelephoneNumber = businessPhone;
                return item;
            }
            finally
            {
                mgr.ToggleSecurityWarning(false);
            }
        }

        public static IContactItem CreateContactItem(ContactManagerBase mgr, object baseContact)
        {
            try
            {
                mgr.ToggleSecurityWarning(true);
                IContactItem newItem = null;
                if (mgr is OutlookContactManager)
                {
                    OutlookContactManager cmgr = (OutlookContactManager)mgr;

                    if (baseContact == null)
                    {
                        baseContact = cmgr.OutlookApplication.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olContactItem) as Microsoft.Office.Interop.Outlook.ContactItem;
                    }
                    newItem = new OutlookContactItem((Microsoft.Office.Interop.Outlook.ContactItem)baseContact);
                    //newItem.BaseContact = baseContact;
                }
                return newItem;
            }
            finally
            {
                mgr.ToggleSecurityWarning(false);
            }
        }
    }
}
