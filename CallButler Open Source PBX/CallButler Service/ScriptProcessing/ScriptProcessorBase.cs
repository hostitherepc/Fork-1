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
using CallButler.Service.ScriptProcessing.ScriptCompilers;
using CallButler.Service.Services;
using WOSI.CallButler.ManagementInterface;
using CallButler.Telecom;

namespace CallButler.Service.ScriptProcessing
{
    internal class ScriptProcessorBase
    {
        private ScriptProcessorBase linkedScriptProcessor;

        public ScriptProcessorBase()
        {
        }

        private string GetSoundFileForLanguage(string languageID, string filename)
        {
            try
            {
                while (true)
                {
                    string soundFilename = string.Format("{0}\\{1}\\{2}", WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemSoundRootDirectory), languageID, filename);

                    if (File.Exists(soundFilename))
                        return soundFilename;
                    else
                    {
                        System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.GetCultureInfo(languageID);

                        if (culture.Parent == System.Globalization.CultureInfo.InvariantCulture)
                            return null;
                        else
                        {
                            languageID = culture.Parent.IetfLanguageTag;
                        }
                    }
                }
            }
            catch
            {
            }

            return null;
        }

        public void LinkScriptProcessor(ScriptProcessorBase scriptProcessor)
        {
            linkedScriptProcessor = scriptProcessor;
        }

        private void UnlinkScriptProcessor()
        {
            linkedScriptProcessor = null;
        }

        public void ProcessExternalCommand(string command, string commandData, string eventToken, TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider, Utilities.PluginManagement.PluginManager pluginManager, PBXRegistrarService pbxRegistrar)
        {
            // Parse out our external event action
            if (Enum.IsDefined(typeof(BaseExternalCommands), command))
            {
                BaseExternalCommands externalCommand = WOSI.Utilities.EnumUtils<BaseExternalCommands>.Parse(command);

                string languageID = "en";

                switch (externalCommand)
                {
                    case BaseExternalCommands.CALLBUTLERINTERNAL_StartAddonModule:

                        CallButler.Service.Plugin.CallButlerAddonModulePlugin[] addonModules = pluginManager.GetAllPluginsOfType<CallButler.Service.Plugin.CallButlerAddonModulePlugin>();

                        foreach (CallButler.Service.Plugin.CallButlerAddonModulePlugin addonModule in addonModules)
                        {
                            if (addonModule.PluginID.ToString() == commandData)
                            {
                                try
                                {
                                    // Make sure the module is licensed
                                    if (!addonModule.IsLicensed)
                                        break;

                                    // We found our module and we should load the script it uses
                                    tsInterface.ScriptProcessor = new AddonModuleScriptProcessor(addonModule);
                                    tsInterface.ScriptProcessor.StartProcessing(tsInterface, telecomProvider, dataProvider);
                                    return;

                                }
                                catch (Exception e)
                                {
                                    LoggingService.AddLogEntry(WOSI.CallButler.ManagementInterface.LogLevel.ErrorsOnly, "Failed to load Addon-Module '" + addonModule.PluginName + "'\r\n\r\n" + e.Message + "\r\n\r\n" + e.StackTrace, true);
                                }
                            }
                        }

                        break;

                    case BaseExternalCommands.CALLBUTLERINTERNAL_ReturnToCallFlowMainMenu:
                        
                        // Return to the Call flow main menu.
                        tsInterface.ScriptProcessor = new StandardScriptProcessor(pluginManager, pbxRegistrar);
                        ((StandardScriptProcessor)tsInterface.ScriptProcessor).StartFromMainMenu(tsInterface);

                        break;

                    case BaseExternalCommands.CALLBUTLERINTERNAL_PlayLicenseIntroGreeting:

                        // If the line isn't in use, don't do anything
                        if (!telecomProvider.IsLineInUse(tsInterface.LineNumber))
                        {
                            tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
                            break;
                        }

                        // Read our intro sound bytes
                        byte[] introSoundBytes = null;

                        if (telecomProvider.AudioInputRate == 8000)
                        {
                            introSoundBytes = new byte[Properties.Resources.powered_by_8khz.Length];
                            Properties.Resources.powered_by_8khz.Read(introSoundBytes, 0, introSoundBytes.Length);
                        }
                        else if (telecomProvider.AudioInputRate == 16000)
                        {
                            introSoundBytes = new byte[Properties.Resources.powered_by_16khz.Length];
                            Properties.Resources.powered_by_16khz.Read(introSoundBytes, 0, introSoundBytes.Length);
                        }

                        // Play our license intro sound
                        if (introSoundBytes != null)
                        {
                            telecomProvider.PlaySound(tsInterface.LineNumber, introSoundBytes);
                        }

                        break;

                    case BaseExternalCommands.CALLBUTLERINTERNAL_PlaySystemSound:

                        // If the line isn't in use, don't do anything
                        if (!telecomProvider.IsLineInUse(tsInterface.LineNumber))
                        {
                            tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
                            break;
                        }

                        // Get the sound with the current language
                        languageID = tsInterface.IMLInterpreter.GetLocalVariable("LanguageID");

                        string soundFilename = GetSoundFileForLanguage(languageID, commandData);

                        if (soundFilename == null)
                        {
                            // If we don't get a sound with the current language, try the default language
                            soundFilename = GetSoundFileForLanguage(Properties.Settings.Default.DefaultLanguage, commandData);

                            if (soundFilename == null)
                            {
                                // If we don't get a sound file with the default language, try english
                                soundFilename = GetSoundFileForLanguage("en", commandData);

                                if (soundFilename == null)
                                {
                                    if (!File.Exists(soundFilename))
                                    {
                                        // If the sound still doesn't exist, tell the IML interpreter to move on
                                        tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
                                        break;
                                    }
                                }
                            }
                        }

                        // If we get here, our system sound should exist and we should play it.
                        if(string.Compare(commandData, "ring.snd", true) == 0)
                            telecomProvider.PlaySound(tsInterface.LineNumber, soundFilename, true);
                        else
                            telecomProvider.PlaySound(tsInterface.LineNumber, soundFilename, false);

                        LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + tsInterface.LineNumber + ") Playing sound at " + soundFilename, false);

                        break;

                    case BaseExternalCommands.CALLBUTLERINTERNAL_PlayGreeting:

                        // If the line isn't in use, don't do anything
                        if (!telecomProvider.IsLineInUse(tsInterface.LineNumber))
                        {
                            tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
                            break;
                        }

                        // Get our current language
                        languageID = tsInterface.IMLInterpreter.GetLocalVariable("LanguageID");

                        // Create our greetingID
                        Guid greetingID = new Guid(commandData);

                        // Get the greeting in our selected language
                        WOSI.CallButler.Data.CallButlerDataset.GreetingsRow greeting = dataProvider.GetGreeting(Properties.Settings.Default.CustomerID, greetingID);

                        if (greeting != null)
                        {
                            // Get the greeting for our specified language
                            WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow localizedGreeting = dataProvider.GetLocalizedGreeting(Properties.Settings.Default.CustomerID, greetingID, languageID);

                            if (localizedGreeting == null)
                            {
                                // If the greeting doesn't exist in the current language, try using the default language
                                localizedGreeting = dataProvider.GetLocalizedGreeting(Properties.Settings.Default.CustomerID, greetingID, Properties.Settings.Default.DefaultLanguage);

                                if (localizedGreeting == null)
                                {
                                    // If the greeting doesn't exist in the default language, heck just return the first one that exists
                                    WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow[] localizedGreetings = greeting.GetLocalizedGreetingsRows();

                                    if (localizedGreetings.Length > 0)
                                        localizedGreeting = localizedGreetings[0];
                                }
                            }

                            if (localizedGreeting != null)
                            {
                                // Determine how we should play this greeting
                                WOSI.CallButler.Data.GreetingType greetingType = (WOSI.CallButler.Data.GreetingType)localizedGreeting.Type;

                                switch (greetingType)
                                {
                                    case WOSI.CallButler.Data.GreetingType.SoundGreeting:
                                        // Create our sound file path
                                        string soundFilePath = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingSoundRootDirectory) + "\\" + localizedGreeting.LanguageID + "\\" + greetingID.ToString() + ".snd";

                                        if (File.Exists(soundFilePath))
                                        {
                                            telecomProvider.PlaySound(tsInterface.LineNumber, soundFilePath, false);
                                            LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + tsInterface.LineNumber + ") Playing sound at " + soundFilePath, false);
                                        }
                                        else
                                            tsInterface.IMLInterpreter.SignalEventCallback(eventToken);

                                        break;

                                    case WOSI.CallButler.Data.GreetingType.TextGreeting:

                                        // Speak our text
                                        string textToSpeak = tsInterface.IMLInterpreter.ParseVariableTokens(localizedGreeting.Data);

                                        // Take out any XML
                                        if (!WOSI.Utilities.StringUtils.IsWellFormedXml(textToSpeak))
                                            textToSpeak = WOSI.Utilities.StringUtils.XmlEncodeString(textToSpeak);

                                        if (textToSpeak.Length > 0)
                                        {
                                            if (!localizedGreeting.IsVoiceNull() && localizedGreeting.Voice.Length > 0)
                                                textToSpeak = "<voice required=\"Name=" + localizedGreeting.Voice + "\">" + textToSpeak + "</voice>";
                                            else if(Properties.Settings.Default.DefaultTTSVoice != null && Properties.Settings.Default.DefaultTTSVoice.Length > 0)
                                                textToSpeak = "<voice required=\"Name=" + Properties.Settings.Default.DefaultTTSVoice + "\">" + textToSpeak + "</voice>";

                                            telecomProvider.SpeakText(tsInterface.LineNumber, textToSpeak);
                                            LoggingService.AddLogEntry(LogLevel.Extended, "(Line " + tsInterface.LineNumber + ") Speaking '" + textToSpeak + "'", false);
                                        }
                                        else
                                        {
                                            tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
                                        }

                                        break;
                                }
                            }
                            else
                            {
                                // If no greeting is found in the right language, tell the interpreter to move on
                                tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
                            }
                        }
                        // If the greeting isn't found, tell the interpreter to go on
                        else
                        {
                            tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
                        }

                        break;
                }
            }
            else
            {
                OnExternalCommand(command, commandData, eventToken, tsInterface, telecomProvider, dataProvider);

                if (linkedScriptProcessor != null)
                    linkedScriptProcessor.OnLinkedExternalCommand(command, commandData, eventToken, tsInterface, telecomProvider, dataProvider);
            }
        }

        protected virtual void OnExternalCommand(string command, string commandData, string eventToken, TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
        }

        protected virtual void OnLinkedExternalCommand(string command, string commandData, string eventToken, TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
        }

        public void StartProcessing(TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            OnStartProcessing(tsInterface, telecomProvider, dataProvider);
        }

        protected virtual void OnStartProcessing(TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
        }

        public virtual void OnDTMFDigit(CallButler.Telecom.TelecomProviderBase telecomProvider, CallInputEventArgs e)
        {
        }

        public virtual void OnCallFailed(CallButler.Telecom.TelecomProviderBase telecomProvider, LineEventArgs e)
        {
        }

        public virtual void OnCallConnected(CallButler.Telecom.TelecomProviderBase telecomProvider, LineEventArgs e)
        {
        }

        public virtual void OnIncomingTransfer(CallButler.Telecom.TelecomProviderBase telcomProvider, CallButler.Telecom.TransferEventArgs e)
        {
        }

        public virtual void OnCallTemporarilyMoved(CallButler.Telecom.TelecomProviderBase telcomProvider, CallEventArgs e)
        {
        }

        public virtual void OnAnswerDetectHuman(CallButler.Telecom.TelecomProviderBase telecomProvider, LineEventArgs e)
        {
        }

        public virtual void OnAnswerDetectMachine(CallButler.Telecom.TelecomProviderBase telecomProvider, LineEventArgs e)
        {
        }

        public virtual void OnAnswerDetectMachineGreetingFinished(CallButler.Telecom.TelecomProviderBase telecomProvider, LineEventArgs e)
        {
        }
    }
}
