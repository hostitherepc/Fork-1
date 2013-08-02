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
using System.Diagnostics;

namespace CallButler.Service.Services
{
    static class PerformanceCounterService
    {
        private static string categoryName = Services.PrivateLabelService.ReplaceProductName("CallButler Service");
        private static string categoryHelp = Services.PrivateLabelService.ReplaceProductName("Performance counters for the CallButler Service");

        private const string concurrentCallsName = "Concurrent Calls";
        private const string lockedLinesName = "Locked Lines";
        private const string scriptsRunningName = "Scripts Running";
        private const string totalCallsName = "Total Calls";
        private const string phonesRegisteredName = "Total Phones Registered";

        private static PerformanceCounter concurrentCalls;
        private static PerformanceCounter lockedLines;
        private static PerformanceCounter scriptsRunning;
        private static PerformanceCounter totalCalls;
        private static PerformanceCounter phonesRegistered;

        public static void Initialize()
        {
            try
            {
                bool recreateCounters = false;

                CounterCreationDataCollection counters = new CounterCreationDataCollection();

                if (!PerformanceCounterCategory.Exists(categoryName))
                {
                    PerformanceCounterCategory.Create(categoryName, categoryHelp, PerformanceCounterCategoryType.SingleInstance, counters);
                }

                // Create the actual counters
                if (!PerformanceCounterCategory.CounterExists(concurrentCallsName, categoryName))
                    recreateCounters = true;
                CounterCreationData counter = new CounterCreationData();
                counter.CounterName = concurrentCallsName;
                counter.CounterHelp = "The total number of concurrent calls.";
                counter.CounterType = PerformanceCounterType.NumberOfItems32;
                counters.Add(counter);

                if (!PerformanceCounterCategory.CounterExists(lockedLinesName, categoryName))
                    recreateCounters = true;
                counter = new CounterCreationData();
                counter.CounterName = lockedLinesName;
                counter.CounterHelp = "The total number of lines that are locked and cannot be used for incoming calls.";
                counter.CounterType = PerformanceCounterType.NumberOfItems32;
                counters.Add(counter);

                if (!PerformanceCounterCategory.CounterExists(scriptsRunningName, categoryName))
                    recreateCounters = true;
                counter = new CounterCreationData();
                counter.CounterName = scriptsRunningName;
                counter.CounterHelp = "The total number of scripts currently executing.";
                counter.CounterType = PerformanceCounterType.NumberOfItems32;
                counters.Add(counter);

                if (!PerformanceCounterCategory.CounterExists(totalCallsName, categoryName))
                    recreateCounters = true;
                counter = new CounterCreationData();
                counter.CounterName = totalCallsName;
                counter.CounterHelp = "The total number of calls that have been answered.";
                counter.CounterType = PerformanceCounterType.NumberOfItems64;
                counters.Add(counter);

                if (!PerformanceCounterCategory.CounterExists(phonesRegisteredName, categoryName))
                    recreateCounters = true;
                counter = new CounterCreationData();
                counter.CounterName = phonesRegisteredName;
                counter.CounterHelp = "The total number of IP telephones registered.";
                counter.CounterType = PerformanceCounterType.NumberOfItems32;
                counters.Add(counter);

                // Create our performance counter if it doesn't already exist
                if (recreateCounters)
                {
                    if (PerformanceCounterCategory.Exists(categoryName))
                        PerformanceCounterCategory.Delete(categoryName);

                    PerformanceCounterCategory.Create(categoryName, categoryHelp, PerformanceCounterCategoryType.SingleInstance, counters);
                }

                // Create our counter access classes
                concurrentCalls = new PerformanceCounter(categoryName, concurrentCallsName, false);
                lockedLines = new PerformanceCounter(categoryName, lockedLinesName, false);
                scriptsRunning = new PerformanceCounter(categoryName, scriptsRunningName, false);
                totalCalls = new PerformanceCounter(categoryName, totalCallsName, false);
                phonesRegistered = new PerformanceCounter(categoryName, phonesRegisteredName, false);

                ResetCounters();
            }
            catch(Exception e)
            {
                LoggingService.AddLogEntry(WOSI.CallButler.ManagementInterface.LogLevel.ErrorsOnly, "Unable to create performance monitoring WOSI.CallButler.Data." + Utils.ErrorUtils.FormatErrorString(e), true);
            }
        }

        public static void ResetCounters()
        {
            ConcurrentCalls = 0;
            LockedLines = 0;
            ScriptsRunning = 0;
            totalCalls.RawValue = 0;
            PhonesRegistered = 0;
        }

        public static int PhonesRegistered
        {
            set
            {
                if (phonesRegistered != null)
                    phonesRegistered.RawValue = value;
            }
        }

        public static void IncrementTotalCalls()
        {
            totalCalls.Increment();
        }

        public static int ConcurrentCalls
        {
            set
            {
                if(concurrentCalls != null)
                    concurrentCalls.RawValue = value;
            }
        }

        public static int LockedLines
        {
            set
            {
                if (lockedLines != null)
                    lockedLines.RawValue = value;
            }
        }

        public static int ScriptsRunning
        {
            set
            {
                if (scriptsRunning != null)
                    scriptsRunning.RawValue = value;
            }
        }
    }
}
