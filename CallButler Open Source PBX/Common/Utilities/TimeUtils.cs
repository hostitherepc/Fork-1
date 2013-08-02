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
using Microsoft.Win32;

namespace WOSI.Utilities
{
    public class TimeZoneInfo
    {
        private string displayName = "";
        private string daylightName = "";
        private string standardName = "";
        private TimeSpan gmtStandardOffset;
        private TimeSpan gmtDaylightOffset;

        public TimeZoneInfo(string displayName, string daylightName, string standardName, TimeSpan gmtStandardOffset, TimeSpan gmtDaylightOffset)
        {
            this.displayName = displayName;
            this.standardName = standardName;
            this.daylightName = daylightName;
            this.gmtStandardOffset = gmtStandardOffset;
            this.gmtDaylightOffset = gmtDaylightOffset;
        }

        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public string DaylightName
        {
            get
            {
                return daylightName;
            }
        }

        public string StandardName
        {
            get
            {
                return standardName;
            }
        }

        public TimeSpan GMTStandardOffset
        {
            get
            {
                return gmtStandardOffset;
            }
        }

        public TimeSpan GMTDaylightOffset
        {
            get
            {
                return gmtDaylightOffset;
            }
        }

        public override string ToString()
        {
            return displayName;
        }
    }

    public class TimeUtils
    {
        private TimeZoneInfo[] timeZones;

        public TimeUtils()
        {
            timeZones = GetSystemTimeZones();
        }

        public TimeZoneInfo[] TimeZones
        {
            get
            {
                return timeZones;
            }
        }

        public int CurrentTimeZoneIndex
        {
            get
            {
                int index = 0;

                //bool isDaylightTime = System.TimeZone.CurrentTimeZone.IsDaylightSavingTime(DateTime.Now);

                foreach (TimeZoneInfo timeZone in timeZones)
                {
                    /*if (isDaylightTime)
                    {
                        if (timeZone.DaylightName == System.TimeZone.CurrentTimeZone.DaylightName)
                            return index;
                    }
                    else
                    {*/
                        if (timeZone.StandardName == System.TimeZone.CurrentTimeZone.StandardName)
                            return index;
                    //}

                    index++;
                }

                return -1;
            }
        }

        public int GetTimeZoneIndexFromStandardOffset(TimeSpan offset)
        {
            int index = 0;

            foreach (TimeZoneInfo timeZone in timeZones)
            {
                if (timeZone.GMTStandardOffset == offset)
                    return index;

                index++;
            }

            return -1;
        }

        public static TimeZoneInfo[] GetSystemTimeZones()
        {
            List<TimeZoneInfo> timeZones = new List<TimeZoneInfo>();

            RegistryKey regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones");

            if (regKey != null)
            {
                string[] timezoneKeyNames = regKey.GetSubKeyNames();

                foreach (string timezoneKeyName in timezoneKeyNames)
                {
                    try
                    {
                        RegistryKey timezoneKey = regKey.OpenSubKey(timezoneKeyName);

                        if (timezoneKey != null)
                        {
                            string displayName = "";
                            string daylightName = "";
                            string standardName = "";

                            if (timezoneKey.GetValue("Display") != null)
                                displayName = (string)timezoneKey.GetValue("Display");

                            if (timezoneKey.GetValue("Dlt") != null)
                                daylightName = (string)timezoneKey.GetValue("Dlt");

                            if (timezoneKey.GetValue("Std") != null)
                                standardName = (string)timezoneKey.GetValue("Std");

                            // Calculate our offsets
                            if (timezoneKey.GetValue("TZI") != null)
                            {
                                byte[] tzi = (byte[])timezoneKey.GetValue("TZI");

                                int bias = -BitConverter.ToInt32(tzi, 0);
                                int stdBias = -BitConverter.ToInt32(tzi, 4);
                                int dltBias = -BitConverter.ToInt32(tzi, 8);

                                TimeSpan standardOffset = new TimeSpan(0, bias + stdBias, 0);
                                TimeSpan daylightOffset = new TimeSpan(0, bias + dltBias, 0);

                                timeZones.Add(new TimeZoneInfo(displayName, daylightName, standardName, standardOffset, daylightOffset));
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }

            return timeZones.ToArray();
        }
    }

}
