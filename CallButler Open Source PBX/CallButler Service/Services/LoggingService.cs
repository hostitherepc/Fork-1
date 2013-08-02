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
using System.IO;
using WOSI.CallButler.Data.DataProviders;
using System.Diagnostics;
using System.Xml;
using WOSI.CallButler.ManagementInterface;

namespace CallButler.Service.Services
{
    internal class LogEntryEventArgs : EventArgs
    {
        public LogLevel Level;
        public string Message;
        public bool IsError = false;

        public LogEntryEventArgs(LogLevel level, string message, bool isError)
        {
            this.Level = level;
            this.Message = message;
            this.IsError = isError;
        }
    }

    class LoggingService
    {
        private static TextWriter logWriter = null;
        private static int logStartDay = -1;
        private static EventLog eventLog = null;
        public static event EventHandler<LogEntryEventArgs> NewLogEntry;

        static LoggingService()
        {
            try
            {
                CreateLogStorage(Properties.Settings.Default.LogStorage);

                Properties.Settings.Default.SettingChanging += new System.Configuration.SettingChangingEventHandler(Default_SettingChanging);
            }
            catch { }
        }

        static void Default_SettingChanging(object sender, System.Configuration.SettingChangingEventArgs e)
        {
            try
            {
                if (e.SettingName == "LogStorage")
                {
                    CreateLogStorage((LogStorage)e.NewValue);
                }
            }
            catch { }
        }

        public static void AddLogEntry(LogLevel level, string message, bool isError)
        {
            try
            {
                Trace.WriteLine("Level : " + level + ", " + message);

                // Only write log entries if they are at or below the current level
                if (Properties.Settings.Default.LogStorage != LogStorage.None && level <= Properties.Settings.Default.LoggingLevel)
                {
                    switch (Properties.Settings.Default.LogStorage)
                    {
                        case LogStorage.File:
                            {
                                logWriter.Write("[" + DateTime.Now.ToString() + "] ");

                                if (isError)
                                    logWriter.Write("**ERROR** ");

                                logWriter.WriteLine(message);

                                logWriter.Flush();

                                break;
                            }
                        case LogStorage.SystemEventLog:
                            {
                                if (isError)
                                    eventLog.WriteEntry(message, EventLogEntryType.Error);
                                else
                                    eventLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.Information);

                                break;
                            }
                    }

                }

                // Raise our event
                WOSI.Utilities.EventUtils.FireAsyncEvent(NewLogEntry, null, new LogEntryEventArgs(level, message, isError));
            }
            catch { }
        }

        private static void CreateLogStorage(LogStorage logStorage)
        {
            if (logStorage == LogStorage.File)
            {
                // Create a new log file for every day
                if (logStartDay != DateTime.Now.DayOfYear)
                {
                    // Close the existing log file writer if it's open
                    if (logWriter != null)
                    {
                        logWriter.Close();
                        logWriter = null;
                        logStartDay = -1;
                    }

                    string logDirectory = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.LogDirectory);

                    if (!Directory.Exists(logDirectory))
                    {
                        Directory.CreateDirectory(logDirectory);
                    }

                    string logFilename = logDirectory + "\\" + DateTime.Today.ToString("MM-dd-yyyy") + ".log";

                    // Check to see if the log already exists
                    logWriter = (TextWriter)new StreamWriter(logFilename, true, Encoding.UTF8);

                    logStartDay = DateTime.Now.DayOfYear;
                }
            }
            else if (logStorage == LogStorage.SystemEventLog)
            {
                if (logWriter != null)
                {
                    logWriter.Close();
                    logWriter = null;
                    logStartDay = -1;
                }

                if (eventLog == null)
                {
                    if (!EventLog.SourceExists(Services.PrivateLabelService.ReplaceProductName("CallButler Log")))
                    {
                        EventLog.CreateEventSource(Services.PrivateLabelService.ReplaceProductName("CallButler Log"), Services.PrivateLabelService.ReplaceProductName("CallButler Log"));
                    }

                    // Create an EventLog instance and assign its source.
                    eventLog = new EventLog();
                    eventLog.Source = Services.PrivateLabelService.ReplaceProductName("CallButler Log");
                    eventLog.Log = Services.PrivateLabelService.ReplaceProductName("CallButler Log");
                }
            }
            else
            {
                if (logWriter != null)
                {
                    logWriter.Close();
                    logWriter = null;
                    logStartDay = -1;
                }

                if (eventLog != null)
                {
                    eventLog.Close();
                    eventLog.Dispose();
                    eventLog = null;
                }
            }
        }
    }
}
