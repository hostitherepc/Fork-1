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

namespace CallButler.Service.ScriptProcessing
{
    internal class StandardScriptProcessor : ScriptProcessorBase
    {
        private enum StandardExternalCommands
        {
            CALLBUTLERINTERNAL_ChooseLanguage,
            CALLBUTLERINTERNAL_SetLanguageID,
            CALLBUTLERINTERNAL_ProcessMainMenuOption,
            CALLBUTLERINTERNAL_DialByNameSearch,
            CALLBUTLERINTERNAL_VoicemailManagement,
            CALLBUTLERINTERNAL_StartAddonModule
        }

        private enum StandardExternalEvents
        {
            CALLBUTLERINTERNAL_NotMultilingual,
            CALLBUTLERINTERNAL_InvalidLanguage,
            CALLBUTLERINTERNAL_ExtensionNotFound,
            CALLBUTLERINTERNAL_InvalidMenuOption,
            CALLBUTLERINTERNAL_GreetingMenuOption,
            CALLBUTLERINTERNAL_ExtensionMenuOption,
            CALLBUTLERINTERNAL_ScriptMenuOption,
            CALLBUTLERINTERNAL_NumberTransferMenuOption,
            CALLBUTLERINTERNAL_AddonModuleMenuOption,
            CALLBUTLERINTERNAL_AddonModuleFailed
        }

        private Utilities.PluginManagement.PluginManager pluginManager;
        private PBXRegistrarService pbxRegistrar;

        public StandardScriptProcessor(Utilities.PluginManagement.PluginManager pluginManager, PBXRegistrarService pbxRegistrar)
        {
            this.pluginManager = pluginManager;
            this.pbxRegistrar = pbxRegistrar;
        }

        protected override void OnStartProcessing(TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            string callFlowScriptLocation = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory) + "\\Call Flow.xml";

            IMLScript imlScript = new IMLScript();

            ScriptPage introPage = new ScriptPage();
            introPage.ID = Guid.NewGuid().ToString();

            if (Properties.Settings.Default.WelcomeGreetingDelay > 0)
            {
                Delay welcomeDelay = new Delay();
                welcomeDelay.DelayTime = Properties.Settings.Default.WelcomeGreetingDelay.ToString();
                introPage.Actions.Add(welcomeDelay);
            }

            /*if (LicenseService.IsTrialLicense())
            {
                introPage.Actions.Add(ScriptCompilers.ScriptUtils.CreateExternalAction(ScriptCompilers.BaseExternalCommands.CALLBUTLERINTERNAL_PlayLicenseIntroGreeting.ToString(), ""));
            }*/

            ScriptCompilers.ScriptUtils.ProcessPersonalizedGreeting(dataProvider, ref introPage, Properties.Settings.Default.CustomerID, tsInterface.IMLInterpreter.CallerDisplayName, tsInterface.IMLInterpreter.CallerUsername, tsInterface.IMLInterpreter.CallerHost, tsInterface.IMLInterpreter.DialedUsername);

            if (File.Exists(callFlowScriptLocation))
            {
                GotoPage gotoPage = new GotoPage();
                gotoPage.Location = callFlowScriptLocation;
                introPage.Actions.Add(gotoPage);

                imlScript.Pages.Add(introPage);
            }

            tsInterface.IMLInterpreter.StartScript(imlScript, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory));
        }

        public void StartFromMainMenu(TelecomScriptInterface tsInterface)
        {
            string callFlowScriptLocation = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory) + "\\Call Flow.xml";

            if (File.Exists(callFlowScriptLocation))
            {
                IMLScript imlScript = IMLScript.OpenScript(callFlowScriptLocation);
                tsInterface.IMLInterpreter.StartScript(imlScript, "MainMenuPage", WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.SystemScriptsRootDirectory));
            }
        }

        protected override void OnExternalCommand(string command, string commandData, string eventToken, TelecomScriptInterface tsInterface, CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
        {
            // Parse out our external event action
            if (Enum.IsDefined(typeof(StandardExternalCommands), command))
            {
                StandardExternalCommands externalCommand = WOSI.Utilities.EnumUtils<StandardExternalCommands>.Parse(command);

                switch (externalCommand)
                {
                    case StandardExternalCommands.CALLBUTLERINTERNAL_ChooseLanguage:
                        {
                            if (!Properties.Settings.Default.Multilingual)
                            {
                                tsInterface.IMLInterpreter.SignalExternalEvent(StandardExternalEvents.CALLBUTLERINTERNAL_NotMultilingual.ToString());
                            }
                            else
                            {
                                tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
                            }
                            break;
                        }
                    case StandardExternalCommands.CALLBUTLERINTERNAL_SetLanguageID:
                        {
                            List<string> languages = new List<string>();
                            languages.Add(Properties.Settings.Default.DefaultLanguage);
                            languages.AddRange(Properties.Settings.Default.Languages.Split(';'));

                            int languageNumber = Convert.ToInt32(commandData);

                            if (languageNumber > 0 && languageNumber <= languages.Count)
                            {
                                // Set our language ID variable
                                tsInterface.IMLInterpreter.SetLocalVariable("LanguageID", languages[languageNumber - 1]);
                                tsInterface.IMLInterpreter.SignalEventCallback(eventToken);
                            }
                            else
                            {
                                tsInterface.IMLInterpreter.SignalExternalEvent(StandardExternalEvents.CALLBUTLERINTERNAL_InvalidLanguage.ToString());
                            }

                            break;
                        }
                    case StandardExternalCommands.CALLBUTLERINTERNAL_ProcessMainMenuOption:
                        {
                            WOSI.CallButler.Data.CallButlerDataset.DepartmentsDataTable departments = dataProvider.GetDepartments(Properties.Settings.Default.CustomerID);

                            WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow[] choosenDepartments = (WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow[])departments.Select("OptionNumber = " + commandData);

                            if (choosenDepartments.Length > 0)
                            {
                                WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow choosenDepartment = choosenDepartments[0];

                                switch (choosenDepartment.Type)
                                {
                                    case (short)WOSI.CallButler.Data.DepartmentTypes.Greeting:
                                        tsInterface.IMLInterpreter.SetLocalVariable("MainMenuOptionGreetingID", choosenDepartment.DepartmentID.ToString());
                                        tsInterface.IMLInterpreter.SignalExternalEvent(StandardExternalEvents.CALLBUTLERINTERNAL_GreetingMenuOption.ToString());
                                        break;

                                    case (short)WOSI.CallButler.Data.DepartmentTypes.Extension:

                                        // Find our extension number
                                        try
                                        {
                                            WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = dataProvider.GetExtension(Properties.Settings.Default.CustomerID, new Guid(choosenDepartment.Data1));

                                            if (extension != null)
                                            {
                                                tsInterface.IMLInterpreter.SetLocalVariable("Extension", extension.ExtensionNumber.ToString());
                                            }
                                        }
                                        catch
                                        {
                                        }
                                                                                
                                        tsInterface.IMLInterpreter.SignalExternalEvent(StandardExternalEvents.CALLBUTLERINTERNAL_ExtensionMenuOption.ToString());
                                        break;

                                    case (short)WOSI.CallButler.Data.DepartmentTypes.Script:
                                        tsInterface.IMLInterpreter.SetLocalVariable("CustomScriptPath", choosenDepartment.Data1);
                                        tsInterface.IMLInterpreter.SignalExternalEvent(StandardExternalEvents.CALLBUTLERINTERNAL_ScriptMenuOption.ToString());

                                        break;

                                    case (short)WOSI.CallButler.Data.DepartmentTypes.Number:
                                        tsInterface.IMLInterpreter.SetLocalVariable("TransferToNumber", choosenDepartment.Data1);
                                        tsInterface.IMLInterpreter.SignalExternalEvent(StandardExternalEvents.CALLBUTLERINTERNAL_NumberTransferMenuOption.ToString());

                                        break;

                                    case (short)WOSI.CallButler.Data.DepartmentTypes.Module:
                                        tsInterface.IMLInterpreter.SetLocalVariable("AddonModuleID", choosenDepartment.Data1);
                                        tsInterface.IMLInterpreter.SignalExternalEvent(StandardExternalEvents.CALLBUTLERINTERNAL_AddonModuleMenuOption.ToString());

                                        break;
                                }
                            }
                            else
                            {
                                tsInterface.IMLInterpreter.SignalExternalEvent(StandardExternalEvents.CALLBUTLERINTERNAL_InvalidMenuOption.ToString());
                            }

                            break;
                        }
                    case StandardExternalCommands.CALLBUTLERINTERNAL_DialByNameSearch:

                        // Find our extensions for this search string
                        WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensions = dataProvider.GetExtensions(Properties.Settings.Default.CustomerID);

                        WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow[] matchingExtensions = (WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow[])extensions.Select("SearchNumber LIKE '" + commandData + "*' AND EnableSearch = True");

                        // Get our extension search index
                        int searchIndex = Convert.ToInt32(tsInterface.IMLInterpreter.GetLocalVariable("ExtensionSearchIndex"));
                        searchIndex++;

                        if (matchingExtensions.Length > 0 && searchIndex < matchingExtensions.Length)
                        {
                            tsInterface.IMLInterpreter.SetLocalVariable("Extension", matchingExtensions[searchIndex].ExtensionNumber.ToString());
                            tsInterface.IMLInterpreter.SetLocalVariable("ExtensionName", matchingExtensions[searchIndex].FirstName + " " + matchingExtensions[searchIndex].LastName);
                            tsInterface.IMLInterpreter.SetLocalVariable("ExtensionSearchIndex", searchIndex.ToString());
                        }
                        else
                        {
                            tsInterface.IMLInterpreter.SignalExternalEvent(StandardExternalEvents.CALLBUTLERINTERNAL_ExtensionNotFound.ToString());
                        }

                        tsInterface.IMLInterpreter.SignalEventCallback(eventToken);

                        break;

                    case StandardExternalCommands.CALLBUTLERINTERNAL_VoicemailManagement:
                        {
                            try
                            {
                                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = dataProvider.GetExtensionNumber(Properties.Settings.Default.CustomerID, Convert.ToInt32(tsInterface.IMLInterpreter.GetLocalVariable("Extension")));

                                if (extension != null)
                                {
                                    tsInterface.ScriptProcessor = new VoicemailManagementScriptProcessor(extension, pbxRegistrar);
                                    tsInterface.ScriptProcessor.StartProcessing(tsInterface, telecomProvider, dataProvider);

                                    break;
                                }
                            }
                            catch
                            {
                            }

                            tsInterface.IMLInterpreter.SignalTransferFailure();

                            break;
                        }
                    /*case StandardExternalCommands.CALLBUTLERINTERNAL_StartAddonModule:
                        {
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

                            tsInterface.ScriptProcessor = this;
                            tsInterface.IMLInterpreter.SignalExternalEvent(StandardExternalEvents.CALLBUTLERINTERNAL_AddonModuleFailed.ToString());

                            break;
                        }*/
                }
            }
        }
    }
}
