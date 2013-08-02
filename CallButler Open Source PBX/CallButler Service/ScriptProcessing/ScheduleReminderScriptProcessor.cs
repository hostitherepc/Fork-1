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
using WOSI.IVR.IML.Classes;
using WOSI.IVR.IML.Classes.ScriptActions;
using WOSI.IVR.IML.Classes.ScriptEvents;
using System.Globalization;
using System.Text.RegularExpressions;
using CallButler.Service.ScriptProcessing.ScriptCompilers;
using CallButler.Service.Services;
using WOSI.CallButler.ManagementInterface;

namespace CallButler.Service.ScriptProcessing
{
    class ScheduleReminderScriptProcessor : ScriptProcessorBase
    {
        private enum ScheduleReminderExternalCommands
        {
            CALLBUTLERINTERNAL_GetNextNumber,
            CALLBUTLERINTERNAL_FetchNextReminder
        }

        private enum ScheduleReminderExternalEvents
        {
            CALLBUTLERINTERNAL_EndOfReminders,
            CALLBUTLERINTERNAL_LastReminder
        }

        private WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension;
        private OutlookReminder[] reminders;
        private int extensionNumberIndex = -1;
        private int reminderIndex = -1;

        public ScheduleReminderScriptProcessor(WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension, OutlookReminder [] reminders)
        {
            this.extension = extension;
            this.reminders = reminders;
        }

        protected override void OnStartProcessing(TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            string scheduleReminderScriptLocation = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory) + "\\Schedule Reminder.xml";

            if (File.Exists(scheduleReminderScriptLocation))
            {
                IMLScript imlScript = IMLScript.OpenScript(scheduleReminderScriptLocation);

                extensionNumberIndex = -1;

                tsInterface.IMLInterpreter.StartScript(imlScript, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory));
            } 
        }

        protected override void OnExternalCommand(string command, string commandData, string eventToken, TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            // Parse out our external event action
            if (Enum.IsDefined(typeof(ScheduleReminderExternalCommands), command))
            {
                ScheduleReminderExternalCommands externalCommand = WOSI.Utilities.EnumUtils<ScheduleReminderExternalCommands>.Parse(command);

                switch (externalCommand)
                {
                    case ScheduleReminderExternalCommands.CALLBUTLERINTERNAL_GetNextNumber:
                        {
                            // If we have a previous call, end it
                            if (telecomProvider.IsLineInUse(tsInterface.LineNumber))
                                telecomProvider.EndCall(tsInterface.LineNumber);

                            extensionNumberIndex++;

                            // Get our extension contact numbers
                            WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow[] contactNumbers = (WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow[])dataProvider.GetExtensionContactNumbers(extension.ExtensionID).Select("", "Priority ASC");

                            while (extensionNumberIndex < contactNumbers.Length)
                            {
                                WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersRow contactNumber = contactNumbers[extensionNumberIndex];

                                // Is the number online?
                                if (contactNumber.Online)
                                {
                                    // Does the number have hours?
                                    if (!contactNumber.HasHoursOfOperation || (contactNumber.HasHoursOfOperation && ScriptUtils.IsInHoursOfOperation(contactNumber.HoursOfOperation, contactNumber.HoursOfOperationUTCOffset)))
                                    {
                                        // If we get here, try the number
                                        tsInterface.IMLInterpreter.SetLocalVariable("NumberToCall", contactNumber.ContactNumber);

                                        tsInterface.IMLInterpreter.SetLocalVariable("ExtensionTimeout", contactNumber.Timeout.ToString());

                                        string introDetails = "You have " + reminders.Length + " upcoming appointment";

                                        if (reminders.Length > 1)
                                        {
                                            introDetails += "s";
                                        }

                                        introDetails += ".";

                                        tsInterface.IMLInterpreter.SetLocalVariable("IntroDetails", introDetails);

                                        tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
                                        
                                        return;
                                    }
                                }

                                extensionNumberIndex++;
                            }

                            if(telecomProvider.IsLineInUse(tsInterface.LineNumber))
                                telecomProvider.EndCall(tsInterface.LineNumber);

                            tsInterface.IMLInterpreter.StopScript();
                            break;
                        }
                    case ScheduleReminderExternalCommands.CALLBUTLERINTERNAL_FetchNextReminder:
                        {
                            reminderIndex++;

                            if (reminderIndex < reminders.Length)
                            {
                                OutlookReminder reminder = reminders[reminderIndex];

                                tsInterface.IMLInterpreter.SetLocalVariable("ScheduleDetails", GetReminderDetails(reminder));

                                if (reminderIndex == reminders.Length - 1)
                                {
                                    tsInterface.IMLInterpreter.SignalExternalEvent(ScheduleReminderExternalEvents.CALLBUTLERINTERNAL_LastReminder.ToString());
                                }
                                else
                                {
                                    tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
                                }
                                
                            }
                            else
                            {
                                tsInterface.IMLInterpreter.SignalExternalEvent(ScheduleReminderExternalEvents.CALLBUTLERINTERNAL_EndOfReminders.ToString());
                            }
                            

                            break;
                        }
                }
            }
        }

        private string GetReminderDetails(OutlookReminder reminder)
        {
             string scheduleDetails = "You have an appointment at " + reminder.ScheduledDateTime.ToShortTimeString() + ".";
             
             if (reminder.Subject.Length > 0)
             {
                 scheduleDetails += "The subject of this appointment is " + reminder.Subject + ". ";
             }

             if (reminder.Location.Length > 0)
             {
                 scheduleDetails += "The location of this appointment is " + reminder.Location + ". ";
             }

             return scheduleDetails;

        }
    }
}

