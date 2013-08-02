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
using System.Runtime.Serialization;
namespace WOSI.CallButler.ManagementInterface
{
    [Serializable]
    public class OutlookReminder
    {
        Guid extensionID;
        DateTime scheduleDateTime;
        string subject = String.Empty;
        string location = String.Empty;
        DateTime qTime;
        string entryID;
        
        public OutlookReminder(Guid extensionID, DateTime scheduledDateTime, string subject, string location, DateTime qTime, string entryID)
        {
            ExtensionID = extensionID;
            ScheduledDateTime = scheduleDateTime;
            Subject = subject;
            Location = location;
            QTime = qTime;
            EntryID = entryID;
        }

        public string EntryID
        {
            get
            {
                return entryID;
            }
            private set
            {
                entryID = value;
            }
        }

        public DateTime QTime
        {
            get
            {
                return qTime;
            }
            private set
            {
                qTime = value;
            }
        }

        public bool SendCall
        {
            get
            {
                if (QTime.AddMinutes(2) <= System.DateTime.Now)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Guid ExtensionID
        {
            get
            {
                return extensionID;
            }
            private set
            {
                extensionID = value;
            }
        }

        public DateTime ScheduledDateTime
        {
            get
            {
                return scheduleDateTime;
            }
            private set
            {
                scheduleDateTime = value;
            }
        }

        public string Subject
        {
            get
            {
                return subject;
            }
            private set
            {
                subject = value;
            }
        }

        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }
    }
}
