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
using System.Text.RegularExpressions;
using WOSI.IVR.IML.Classes;
using WOSI.IVR.IML.Classes.ScriptActions;
using WOSI.IVR.IML.Classes.ScriptEvents;

namespace CallButler.Service.ScriptProcessing.ScriptCompilers
{
    public enum BaseExternalCommands
    {
        CALLBUTLERINTERNAL_PlayLicenseIntroGreeting,
        CALLBUTLERINTERNAL_PlayGreeting,
        CALLBUTLERINTERNAL_PlaySystemSound,
        CALLBUTLERINTERNAL_ReturnToCallFlowMainMenu,
        CALLBUTLERINTERNAL_StartAddonModule
    }    

    public class ScriptUtils
    {
        public static ExternalAction CreateGreetingExternalAction(Guid greetingID)
        {
            return CreateGreetingExternalAction(greetingID.ToString());
        }

        public static ExternalAction CreateGreetingExternalAction(string greetingID)
        {
            return CreateExternalAction(BaseExternalCommands.CALLBUTLERINTERNAL_PlayGreeting.ToString(), greetingID);
        }

        public static ExternalAction CreateSystemSoundExternalAction(string soundName)
        {
            return CreateExternalAction(BaseExternalCommands.CALLBUTLERINTERNAL_PlaySystemSound.ToString(), soundName);
        }

        public static ExternalAction CreateExternalAction(string command, string commandData)
        {
            ExternalAction externalAction = new ExternalAction();

            externalAction.Action = command;
            externalAction.ParameterData = commandData;
            externalAction.Async = false;

            return externalAction;
        }

        public static ExternalEvent CreateExternalEvent(string eventValue)
        {
            ExternalEvent externalEvent = new ExternalEvent();

            externalEvent.Event = eventValue;

            return externalEvent;
        }

        public static GotoPage CreateGotoPageAction(string pageID)
        {
            GotoPage gotoPage = new GotoPage();

            gotoPage.PageID = pageID;

            return gotoPage;
        }

       public static bool IsInHoursOfOperation(string inputString, TimeSpan utcOffset)
        {
            // The hour of operation string is a string of characters of either a 1 or 0. Each character position indicates the hour of the week.

            // First, get the hour of the week
            int hourOfWeek = ((int)DateTime.Now.ToUniversalTime().DayOfWeek * 24) + DateTime.Now.ToUniversalTime().TimeOfDay.Hours;

            // Next, offset this hour by our UTC offset
            hourOfWeek += (int)utcOffset.TotalHours;

            if (System.TimeZone.CurrentTimeZone.IsDaylightSavingTime(DateTime.Now))
            {
                hourOfWeek++;
            }

            // Next see if the current time is within the hours of operation
            if (inputString.Length < hourOfWeek)
                return false;

            if (inputString[hourOfWeek] == '0')
                return false;
            else
                return true;
        }

        public static bool ProcessPersonalizedGreeting(WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider, ref WOSI.IVR.IML.Classes.ScriptPage scriptPage, int customerID, string callerDisplayName, string callerPhoneNumber, string callerHost, string dialedPhoneNumber)
        {
            // Loop through our personalized greetings and see if we can find one that matches our caller
            WOSI.CallButler.Data.CallButlerDataset.PersonalizedGreetingsDataTable personalGreetings = dataProvider.GetPersonalizedGreetings(customerID);
            WOSI.CallButler.Data.CallButlerDataset.PersonalizedGreetingsRow personalizedGreeting = null;

            foreach (WOSI.CallButler.Data.CallButlerDataset.PersonalizedGreetingsRow pgRow in personalGreetings)
            {
                string matchExpression = "";

                // Try our dialed number first
                if (pgRow.DialedUsername.Length > 0)
                {
                    if (!pgRow.UseRegex)
                        matchExpression = pgRow.DialedUsername.Replace("*", ".*");
                    else
                        matchExpression = pgRow.DialedUsername;

                    if (Regex.IsMatch(dialedPhoneNumber, matchExpression, RegexOptions.IgnoreCase))
                    {
                        personalizedGreeting = pgRow;
                        break;
                    }
                }

                // Next try our caller username/phone number
                if (pgRow.CallerUsername.Length > 0)
                {
                    if (!pgRow.UseRegex)
                        matchExpression = pgRow.CallerUsername.Replace("*", ".*");
                    else
                        matchExpression = pgRow.CallerUsername;

                    // First find by the caller number
                    if (Regex.IsMatch(callerPhoneNumber, matchExpression, RegexOptions.IgnoreCase))
                    {
                        personalizedGreeting = pgRow;
                        break;
                    }
                }

                // Next try the host
                if (pgRow.CallerHost.Length > 0)
                {
                    if (!pgRow.UseRegex)
                        matchExpression = pgRow.CallerHost.Replace("*", ".*");
                    else
                        matchExpression = pgRow.CallerHost;

                    if (Regex.IsMatch(callerHost, matchExpression, RegexOptions.IgnoreCase))
                    {
                        personalizedGreeting = pgRow;
                        break;
                    }
                }

                // Next try the caller id/name
                if (pgRow.CallerDisplayName.Length > 0)
                {
                    if (!pgRow.UseRegex)
                        matchExpression = pgRow.CallerDisplayName.Replace("*", ".*");
                    else
                        matchExpression = pgRow.CallerDisplayName;

                    if (Regex.IsMatch(callerDisplayName, matchExpression, RegexOptions.IgnoreCase))
                    {
                        personalizedGreeting = pgRow;
                        break;
                    }
                }
            }

            // If we have a personalized greeting, tell the interpreter to play it
            if (personalizedGreeting != null && (!personalizedGreeting.PlayOnce || (personalizedGreeting.PlayOnce && !personalizedGreeting.HasPlayed)))
            {
                scriptPage.Actions.Add(ScriptUtils.CreateGreetingExternalAction(personalizedGreeting.PersonalizedGreetingID));

                switch (personalizedGreeting.Type)
                {
                    case (short)WOSI.CallButler.Data.PersonalizedGreetingType.CustomScript:
                        GotoPage gotoCustomScript = new GotoPage();
                        gotoCustomScript.Location = personalizedGreeting.Data;
                        scriptPage.Actions.Add(gotoCustomScript);
                        break;

                    case (short)WOSI.CallButler.Data.PersonalizedGreetingType.Hangup:
                        scriptPage.Actions.Add(new HangupCall());
                        break;

                    case (short)WOSI.CallButler.Data.PersonalizedGreetingType.SendToExtension:
                        // Get the proper extension
                        WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = dataProvider.GetExtension(customerID, new Guid(personalizedGreeting.Data));

                        if (extension != null)
                        {
                            TransferCall transferCall = new TransferCall();
                            transferCall.TransferTo = extension.ExtensionNumber.ToString()  ;
                            transferCall.IsExtension = true;

                            scriptPage.Actions.Add(transferCall);
                        }

                        break;

                    case (short)WOSI.CallButler.Data.PersonalizedGreetingType.Module:

                        ExternalAction moduleAction = new ExternalAction();

                        moduleAction.Async = false;
                        moduleAction.Action = BaseExternalCommands.CALLBUTLERINTERNAL_StartAddonModule.ToString();
                        moduleAction.ParameterData = personalizedGreeting.Data;

                        scriptPage.Actions.Add(moduleAction);

                        break;
                }

                if (personalizedGreeting.PlayOnce)
                {
                    personalizedGreeting.HasPlayed = true;
                    dataProvider.PersistPersonalizedGreeting(personalizedGreeting);
                }

                return true;
            }
            else
                return false;
        }
    }
}
