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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CallButler.Manager.Controls;
using System.Globalization;
using Controls;

namespace CallButler.Manager.ViewControls
{
    public partial class CallFlowView : CallButler.Manager.ViewControls.ViewControlBase
    {
        private WOSI.CallButler.Data.CallButlerDataset vbData;

        private CallFlowItem welcomeGreetingItem;
        private CallFlowItem languageSelectionItem;
        private CallFlowItem mainMenuGreetingItem;
        private CallFlowItem mainMenuItem;

        private bool languageNotification = false;
        private bool mainMenuNotification = false;

        private string defaultLanguage = "en";
        private string supportedLanguages = "";

        public CallFlowView()
        {
            InitializeComponent();

            this.HelpRTFText = Properties.Resources.CallFlowHelp;

            // Add our root shape
            CallFlowItem answerCallItem = new CallFlowItem();
            answerCallItem.Title = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_PickUpCall);
            answerCallItem.Caption = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_AnswerCall);
            answerCallItem.Width = 220;
            answerCallItem.Icon = Properties.Resources.phone_pick_up_24;
            LinkButton btnAnswerSettings = CreateLinkButton(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_CallSettings));
            btnAnswerSettings.Click += new EventHandler(btnAnswerSettings_Click);
            answerCallItem.AddActionControl(btnAnswerSettings);

            diagramControl.RootShape = answerCallItem;

            vbData = new WOSI.CallButler.Data.CallButlerDataset();

            UpdateOrientation(Properties.Settings.Default.CallFlowOrientation, false);

            diagramControl.BringToFront();
        }

        protected override void OnLoadData()
        {
            diagramControl.RootShape.ClearChildShapes();

            vbData.Merge(ManagementInterfaceClient.ManagementInterface.GetDepartments(ManagementInterfaceClient.AuthInfo));
            vbData.Merge(ManagementInterfaceClient.ManagementInterface.GetExtensions(ManagementInterfaceClient.AuthInfo));
            vbData.AcceptChanges();

            // Add our welcome greeting control
            welcomeGreetingItem = new CallFlowItem();
            welcomeGreetingItem.Title = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_WelcomeGreeting);
            welcomeGreetingItem.Caption = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_WelcomeCaller);
            welcomeGreetingItem.Icon = Properties.Resources.call_greeting_24;
            welcomeGreetingItem.Width = 220;

            LinkButton btnChangeWelcomeGreeting = CreateLinkButton(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_Change));
            btnChangeWelcomeGreeting.Click += new EventHandler(btnChangeWelcomeGreeting_Click);
            welcomeGreetingItem.AddActionControl(btnChangeWelcomeGreeting);

            diagramControl.RootShape.AddChildShape(welcomeGreetingItem);

            // Add our language selection item
            languageSelectionItem = new CallFlowItem();
            languageSelectionItem.Title = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_ChooseLanguage);
            languageSelectionItem.Caption = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_AskChooseLanguage);
            languageSelectionItem.Icon = Properties.Resources.call_greeting_24;
            languageSelectionItem.Width = 220;

            LinkButton btnChangeLanguageGreeting = CreateLinkButton(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_Change));
            btnChangeLanguageGreeting.Click += new EventHandler(btnChangeLanguageGreeting_Click);
            languageSelectionItem.AddActionControl(btnChangeLanguageGreeting);

            // Add our main menu greeting item
            mainMenuGreetingItem = new CallFlowItem();
            mainMenuGreetingItem.Title = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_MainMenuGreeting);
            mainMenuGreetingItem.Caption = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_ExplainOptions);
            mainMenuGreetingItem.Icon = Properties.Resources.call_greeting_24;
            mainMenuGreetingItem.Width = 220;

            LinkButton btnChangeMainMenuGreeting = CreateLinkButton(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_Change));
            btnChangeMainMenuGreeting.Click += new EventHandler(btnChangeMainMenuGreeting_Click);
            mainMenuGreetingItem.AddActionControl(btnChangeMainMenuGreeting);

            // Add our main menu item
            mainMenuItem = new CallFlowItem();
            mainMenuItem.Title = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_DialAnOption);
            mainMenuItem.Caption = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_CallerDials);
            mainMenuItem.Icon = Properties.Resources.phone_button_24;
            mainMenuItem.Width = 220;

            LinkButton btnNewOption = CreateLinkButton(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_AddNewOption));
            btnNewOption.Click += new EventHandler(btnNewOption_Click);
            mainMenuItem.AddActionControl(btnNewOption);

            mainMenuGreetingItem.AddChildShape(mainMenuItem);

            // Add our departments
            WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow[] departments = (WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow[])vbData.Departments.Select("", "OptionNumber ASC");

            foreach (WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow department in departments)
                AddCallFlowDepartmentItem(department);

            // Update our languages
            defaultLanguage = ManagementInterfaceClient.ManagementInterface.GetDefaultLanguage(ManagementInterfaceClient.AuthInfo);
            supportedLanguages = ManagementInterfaceClient.ManagementInterface.GetLanguages(ManagementInterfaceClient.AuthInfo);
            btnMultilingual.Checked = ManagementInterfaceClient.ManagementInterface.GetMultilingual(ManagementInterfaceClient.AuthInfo);
            UpdateLanguages();

            UpdateGreetings();

            diagramControl.ExpandAll();
            diagramControl.RefreshDiagram();
        }

        void btnAnswerSettings_Click(object sender, EventArgs e)
        {
            Forms.CallAnswerSettingsForm answerSettingsForm = new CallButler.Manager.Forms.CallAnswerSettingsForm();

            answerSettingsForm.AnswerTimeout = ManagementInterfaceClient.ManagementInterface.GetAnswerTimeout(ManagementInterfaceClient.AuthInfo) / 1000;
            answerSettingsForm.WelcomeGreetingDelay = ManagementInterfaceClient.ManagementInterface.GetWelcomeGreetingDelay(ManagementInterfaceClient.AuthInfo) / 1000;

            if (answerSettingsForm.ShowDialog(this) == DialogResult.OK)
            {
                ManagementInterfaceClient.ManagementInterface.SetAnswerTimeout(ManagementInterfaceClient.AuthInfo, answerSettingsForm.AnswerTimeout * 1000);
                ManagementInterfaceClient.ManagementInterface.SetWelcomeGreetingDelay(ManagementInterfaceClient.AuthInfo, answerSettingsForm.WelcomeGreetingDelay * 1000);
            }
        }

        void btnChangeMainMenuGreeting_Click(object sender, EventArgs e)
        {
            // Create our options greeting suggested text
            StringBuilder optionsText = new StringBuilder(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_MainMenu));

            optionsText.Append(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_KnowParty));

            foreach (WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow department in vbData.Departments)
            {
                optionsText.AppendFormat(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_ForDepartment), department.Name, department.OptionNumber);
            }

            optionsText.Append(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_ForDirectory));

            ChangeGreeting(WOSI.CallButler.Data.Constants.MainMenuGreetingGuid, optionsText.ToString());
            ClearNotifyUpdateMenuGreeting();
        }

        void btnChangeLanguageGreeting_Click(object sender, EventArgs e)
        {
            // Create out language greeting suggested text
            int optionNumber = 1;
            StringBuilder languageText = new StringBuilder();

            // Add our default language
            languageText.AppendFormat(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_ForDepartment), CultureInfo.GetCultureInfoByIetfLanguageTag(defaultLanguage).DisplayName, optionNumber);
            optionNumber++;

            // Add our other languages
            string[] languages = supportedLanguages.Split(';');
            foreach (string language in languages)
            {
                if (language.Length > 0)
                {
                    languageText.AppendFormat(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_ForDepartment), CultureInfo.GetCultureInfoByIetfLanguageTag(language).DisplayName, optionNumber);
                    optionNumber++;
                }
            }

            ChangeGreeting(WOSI.CallButler.Data.Constants.LanguageGreetingGuid, languageText.ToString());
            ClearNoftifyUpdateLanguageGreeting();
        }

        void btnChangeWelcomeGreeting_Click(object sender, EventArgs e)
        {
            ChangeGreeting(WOSI.CallButler.Data.Constants.WelcomeGreetingGuid, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_WelcomeTo));
        }

        private void ChangeGreeting(Guid greetingID, string suggestedText)
        {
            string languageID = defaultLanguage;

            if (btnMultilingual.Checked)
            {
                languageID = ((Forms.LanguageItem)cboLanguage.SelectedItem).Culture.IetfLanguageTag;
            }

            Utils.GreetingUtils.EditLocalizedGreeting(vbData, greetingID, languageID, suggestedText);
        }

        private int GetNewOptionNumber()
        {
            int optionNumber = 1;

            while (vbData.Departments.Select("OptionNumber = " + optionNumber).Length > 0 && optionNumber <= 99)
            {
                optionNumber++;
            }

            return optionNumber;
        }

        internal void AddNewDepartment()
        {
            // Create a new department row
            WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow newDepartment = vbData.Departments.NewDepartmentsRow();
            newDepartment.CustomerID = Properties.Settings.Default.CustomerID;
            newDepartment.DepartmentID = Guid.NewGuid();
            newDepartment.Name = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_NewDepartment);
            newDepartment.Type = (int)WOSI.CallButler.Data.DepartmentTypes.Greeting;
            newDepartment.Enabled = true;
            newDepartment.OptionNumber = GetNewOptionNumber();

            Forms.DepartmentForm dpForm = new CallButler.Manager.Forms.DepartmentForm(newDepartment, vbData);

            if (dpForm.ShowDialog(this) == DialogResult.OK)
            {
                // Add our department locally
                vbData.Departments.AddDepartmentsRow(newDepartment);

                // Add our department remotely
                ManagementInterfaceClient.ManagementInterface.PersistDepartment(ManagementInterfaceClient.AuthInfo, Utils.TableUtils<WOSI.CallButler.Data.CallButlerDataset.DepartmentsDataTable>.CreateTableFromRow(newDepartment));

                PersistDepartmentGreeting(newDepartment, null, dpForm);

                CallFlowItem cfItem = AddCallFlowDepartmentItem(newDepartment);

                diagramControl.ScrollControlIntoView(cfItem);

                NotifyUpdateMenuGreeting();

                vbData.AcceptChanges();
            }
        }

        private void PersistDepartmentGreeting(WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow department, WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow localizedGreeting, Forms.DepartmentForm dpForm)
        {
            // If the department is a greeting, add a new localized greeting
            if (department.Type == (short)WOSI.CallButler.Data.DepartmentTypes.Greeting)
            {
                WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable localizedGreetingTable = new WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable();

                bool newRowCreated = false;
                if (localizedGreeting == null)
                {
                    localizedGreeting = localizedGreetingTable.NewLocalizedGreetingsRow();
                    localizedGreeting.LocalizedGreetingID = Guid.NewGuid();
                    localizedGreeting.GreetingID = department.DepartmentID;
                    localizedGreeting.LanguageID = GetCurrentLanguage();

                    localizedGreetingTable.AddLocalizedGreetingsRow(localizedGreeting);
                    newRowCreated = true;
                }
                //else
                  

                dpForm.GreetingControl.SaveGreeting(WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache), localizedGreeting);
                if (newRowCreated == false)
                {
                    localizedGreetingTable.ImportRow(localizedGreeting);
                }

                ManagementInterfaceClient.ManagementInterface.PersistLocalizedGreeting(ManagementInterfaceClient.AuthInfo, localizedGreetingTable);

                // Send our greeting sound file
                Utils.GreetingUtils.PersistLocalizedGreetingSound(localizedGreeting);
            }
        }

        void DeleteDepartment(WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow department)
        {
            if (MessageBox.Show(this, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_ConfirmDeleteDepartment), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_ConfirmDelete), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Delete our deparment remotely
                ManagementInterfaceClient.ManagementInterface.DeleteDepartment(ManagementInterfaceClient.AuthInfo, department.DepartmentID);

                // Delete our department locally
                department.Delete();
                vbData.AcceptChanges();

                // Delete our department item
                foreach (global::Controls.Diagram.DiagramShapeControlBase diagramShape in mainMenuItem.ChildShapes)
                {
                    WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow departmentRow = (WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow)diagramShape.Tag;

                    if (departmentRow == department)
                        mainMenuItem.RemoveChildShape(diagramShape);
                }

                UpdateDepartments();

                NotifyUpdateMenuGreeting();
            }
        }

        CallFlowItem AddCallFlowDepartmentItem(WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow department)
        {
            // Create our call flow item
            CallFlowItem cfItem = new CallFlowItem();
            cfItem.Icon = Properties.Resources.office_24;
            cfItem.Width = 220;
            cfItem.Tag = department;

            LinkButton btnDeleteDepartment = CreateLinkButton(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_Delete));
            btnDeleteDepartment.Tag = cfItem;
            btnDeleteDepartment.Click += new EventHandler(btnDeleteDepartment_Click);
            cfItem.AddActionControl(btnDeleteDepartment);

            LinkButton btnEditDepartment = CreateLinkButton(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_Edit));
            btnEditDepartment.Tag = cfItem;
            btnEditDepartment.Click += new EventHandler(btnEditDepartment_Click);
            cfItem.AddActionControl(btnEditDepartment);

            mainMenuItem.AddChildShape(cfItem);

            // Create our sub call flow item
            CallFlowItem subItem = new CallFlowItem();
            subItem.Width = 220;
            cfItem.AddChildShape(subItem);

            UpdateDepartmentItem(cfItem);

            mainMenuItem.Expanded = true;
            cfItem.Expanded = true;

            return cfItem;
        }

        /*void RenumberDepartmentOptions()
        {
            WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow[] departments = (WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow[])vbData.Departments.Select("", "OptionNumber ASC");

            int optionNumber = 1;

            foreach (WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow department in departments)
            {
                if (department.RowState != DataRowState.Deleted)
                {
                    department.OptionNumber = optionNumber;
                    optionNumber++;
                }
            }

            ManagementInterfaceClient.ManagementInterface.PersistDepartment(ManagementInterfaceClient.AuthInfo, vbData.Departments);
            vbData.Departments.AcceptChanges();
            UpdateDepartments();
        }*/

        void EditDepartment(WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow department, CallFlowItem cfItem)
        {
            Forms.DepartmentForm dpForm = new CallButler.Manager.Forms.DepartmentForm(department, vbData);
            WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow localizedGreeting = null;

            // Get and load our localized greeting if this is a greeting department
            if (department.Type == (short)WOSI.CallButler.Data.DepartmentTypes.Greeting)
            {
                WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable localizedGreetings = ManagementInterfaceClient.ManagementInterface.GetLocalizedGreeting(ManagementInterfaceClient.AuthInfo, department.DepartmentID, GetCurrentLanguage());

                if (localizedGreetings.Count > 0)
                {
                    localizedGreeting = localizedGreetings[0];
                    Utils.GreetingUtils.GetLocalizedGreetingSound(localizedGreeting);
                    dpForm.GreetingControl.LoadGreeting(localizedGreeting, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache));
                }
            }

            if (dpForm.ShowDialog(this) == DialogResult.OK)
            {
                vbData.AcceptChanges();

                // Update our department item remotely

                ManagementInterfaceClient.ManagementInterface.PersistDepartment(ManagementInterfaceClient.AuthInfo, Utils.TableUtils<WOSI.CallButler.Data.CallButlerDataset.DepartmentsDataTable>.CreateTableFromRow(department));

                PersistDepartmentGreeting(department, localizedGreeting, dpForm);


                //if ((WOSI.CallButler.Data.DepartmentTypes)department.Type == WOSI.CallButler.Data.DepartmentTypes.Greeting)
                //    dpForm.GreetingControl.SaveGreeting(WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache));


                UpdateDepartmentItem(cfItem);
                NotifyUpdateMenuGreeting();
            }
        }

        void btnEditDepartment_Click(object sender, EventArgs e)
        {
            CallFlowItem cfItem = (CallFlowItem)((Control)sender).Tag;
            WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow department = (WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow)cfItem.Tag;

            EditDepartment(department, cfItem);
            diagramControl.ScrollControlIntoView(cfItem);
        }

        void btnDeleteDepartment_Click(object sender, EventArgs e)
        {
            CallFlowItem cfItem = (CallFlowItem)((Control)sender).Tag;
            WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow department = (WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow)cfItem.Tag;

            DeleteDepartment(department);
        }

        void btnNewOption_Click(object sender, EventArgs e)
        {
            AddNewDepartment();
        }

        private void UpdateDepartments()
        {
            foreach (global::Controls.Diagram.DiagramShapeControlBase diagramShape in mainMenuItem.ChildShapes)
            {
                UpdateDepartmentItem((CallFlowItem)diagramShape);
            }
        }

        private void UpdateDepartmentItem(CallFlowItem departmentItem)
        {
            WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow departmentRow = (WOSI.CallButler.Data.CallButlerDataset.DepartmentsRow)departmentItem.Tag;
            departmentItem.Title = String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_Dial), departmentRow.OptionNumber, departmentRow.Name);

            CallFlowItem subItem = (CallFlowItem)departmentItem.ChildShapes[0];
            subItem.Title = "";
            subItem.Caption = "";

            switch ((WOSI.CallButler.Data.DepartmentTypes)departmentRow.Type)
            {
                case WOSI.CallButler.Data.DepartmentTypes.Extension:

                    // Get our extenstion
                    try
                    {
                        WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = vbData.Extensions.FindByExtensionID(new Guid(departmentRow.Data1));

                        if (extension != null)
                        {
                            subItem.Icon = Properties.Resources.telephone_24;
                            subItem.Title = String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_TransferExt), extension.ExtensionNumber);
                            subItem.Caption = extension.FirstName + " " + extension.LastName;
                        }

                        break;
                    }
                    catch
                    {
                    }

                    subItem.Icon = Properties.Resources.telephone_24;
                    subItem.Title = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_UnknownExt);
                    subItem.Caption = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_ExtDeleted);

                    break;

                case WOSI.CallButler.Data.DepartmentTypes.Greeting:
                    subItem.Title = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_PlayGreeting);
                    subItem.Icon = Properties.Resources.call_greeting_24;

                    break;

                case WOSI.CallButler.Data.DepartmentTypes.Number:

                    subItem.Icon = Properties.Resources.telephone_24;
                    subItem.Title = String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_TransferNum), WOSI.Utilities.StringUtils.FormatPhoneNumber(departmentRow.Data1));

                    break;

                case WOSI.CallButler.Data.DepartmentTypes.Script:

                    subItem.Icon = Properties.Resources.scroll_24;
                    subItem.Title = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_CustomScript);
                    break;

                case WOSI.CallButler.Data.DepartmentTypes.Module:

                    subItem.Icon = Properties.Resources.gear_connection_24;
                    subItem.Title = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_AddonModule);

                    break;
            }
        }

        private LinkButton CreateLinkButton(string text)
        {
            LinkButton lb = new LinkButton();
            lb.Text = text;
            lb.AntiAliasText = false;
            lb.ForeColor = Color.RoyalBlue;
            lb.UnderlineOnHover = true;
            lb.AutoSize = true;
            lb.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            lb.Cursor = Cursors.Hand;
            lb.TextAlign = ContentAlignment.MiddleCenter;

            return lb;
        }

        private void NotifyUpdateMenuGreeting()
        {
            mainMenuNotification = true;
            ShowNotification(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_MenuOptionChanges));
        }

        private void ClearNotifyUpdateMenuGreeting()
        {
            if (mainMenuNotification)
            {
                mainMenuNotification = false;
                ClearNotification();
            }
        }

        private void NotifyUpdateLanguageGreeting()
        {
            ShowNotification(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_LanguageChanges));
            languageNotification = true;
            diagramControl.ScrollControlIntoView(languageSelectionItem);
        }

        private void ClearNoftifyUpdateLanguageGreeting()
        {
            if (languageNotification)
            {
                languageNotification = false;
                ClearNotification();
            }
        }

        private void ShowNotification(string notificationString)
        {
            lblInfoMessage.Text = notificationString;
            pnlInfo.Visible = true;
        }

        private void ClearNotification()
        {
            pnlInfo.Visible = false;
        }

        private void ManageLanguages()
        {
            Forms.LangageSelectionForm langForm = new CallButler.Manager.Forms.LangageSelectionForm(supportedLanguages, defaultLanguage);

            if (langForm.ShowDialog(this) == DialogResult.OK)
            {
                supportedLanguages = langForm.SelectedLanguages;
                ManagementInterfaceClient.ManagementInterface.SetLanguages(ManagementInterfaceClient.AuthInfo, supportedLanguages);

                UpdateLanguages();
                NotifyUpdateLanguageGreeting();
            }
            else
                cboLanguage.SelectedIndex = 1;
        }

        private void UpdateLanguages()
        {
            cboLanguage.Items.Clear();

            // Add our manage languages item
            cboLanguage.Items.Add(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.CallFlowView_AddRemoveLanguages));

            Forms.LanguageItem li = new Forms.LanguageItem(CultureInfo.GetCultureInfoByIetfLanguageTag(defaultLanguage));
            cboLanguage.Items.Add(li);

            cboLanguage.SelectedIndex = 1;

            // Populate our other languages
            if (supportedLanguages.Length > 0)
            {
                string[] languages = supportedLanguages.Split(';');

                foreach (string language in languages)
                {
                    try
                    {
                        if (language.Length > 0)
                            cboLanguage.Items.Add(new Forms.LanguageItem(CultureInfo.GetCultureInfoByIetfLanguageTag(language)));
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLanguage.SelectedIndex == 0)
                ManageLanguages();
        }

        private void UpdateGreetings()
        {
            if (btnMultilingual.Checked)
            {
                welcomeGreetingItem.RemoveChildShape(mainMenuGreetingItem);
                welcomeGreetingItem.AddChildShape(languageSelectionItem);
                languageSelectionItem.AddChildShape(mainMenuGreetingItem);

                diagramControl.ExpandAll();
            }
            else
            {
                ClearNoftifyUpdateLanguageGreeting();

                welcomeGreetingItem.RemoveChildShape(languageSelectionItem);
                welcomeGreetingItem.AddChildShape(mainMenuGreetingItem);

                diagramControl.ExpandAll();
            }
        }

        private void btnMultilingual_CheckedChanged(object sender, EventArgs e)
        {
            cboLanguage.Visible = btnMultilingual.Checked;

            ManagementInterfaceClient.ManagementInterface.SetMultilingual(ManagementInterfaceClient.AuthInfo, btnMultilingual.Checked);

            UpdateGreetings();
        }

        private string GetCurrentLanguage()
        {
            if (btnMultilingual.Checked)
                return ((Forms.LanguageItem)cboLanguage.SelectedItem).Culture.IetfLanguageTag;
            else
                return defaultLanguage;
        }

        private System.Drawing.Imaging.ImageFormat GetSelectedImageFormat()
        {
            System.Drawing.Imaging.ImageFormat fmt = System.Drawing.Imaging.ImageFormat.Gif;

            switch (saveFileDialog.FilterIndex)
            {
                case (2):
                    fmt = System.Drawing.Imaging.ImageFormat.Jpeg;
                    break;
                case (3):
                    fmt = System.Drawing.Imaging.ImageFormat.Bmp;
                    break;
            }

            return fmt;
        }

        private void mnuHorizontal_Click(object sender, EventArgs e)
        {
            UpdateOrientation(global::Controls.Diagram.DiagramLayoutDirection.Horizontal, true);
        }

        private void mnuVertical_Click(object sender, EventArgs e)
        {
            UpdateOrientation(global::Controls.Diagram.DiagramLayoutDirection.Vertical, true);
        }

        private void UpdateOrientation(global::Controls.Diagram.DiagramLayoutDirection newOrientation, bool saveSettings)
        {
            if (newOrientation == global::Controls.Diagram.DiagramLayoutDirection.Vertical)
            {
                mnuVertical.Checked = true;
                mnuHorizontal.Checked = false;
            }
            else if (newOrientation == global::Controls.Diagram.DiagramLayoutDirection.Horizontal)
            {
                mnuVertical.Checked = false;
                mnuHorizontal.Checked = true;
            }

            diagramControl.LayoutDirection = newOrientation;

            if (saveSettings)
            {
                Properties.Settings.Default.CallFlowOrientation = newOrientation;
                Properties.Settings.Default.Save();
            }
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                int width = diagramControl.HorizontalScroll.Maximum;
                int height = diagramControl.VerticalScroll.Maximum;

                System.Drawing.Bitmap b = new System.Drawing.Bitmap(width, height);

                Rectangle r = new Rectangle(0, 0, width, height);

                this.pnlOuter.Controls.Remove(diagramControl);
                Size origSize = diagramControl.Size;

                diagramControl.Width = width;
                diagramControl.Height = height;

                diagramControl.AutoScroll = false;
                diagramControl.DrawToBitmap(b, r);

                System.Drawing.Imaging.ImageFormat fmt = GetSelectedImageFormat();

                b.Save(saveFileDialog.FileName, fmt);

                diagramControl.AutoScroll = true;
                diagramControl.Size = origSize;

                this.pnlOuter.Controls.Add(diagramControl);
                diagramControl.AutoScrollPosition = new Point(0, 0);
                diagramControl.RefreshDiagram();
                diagramControl.BringToFront();
            }
        }

        private void btnOrientation_ButtonClick(object sender, EventArgs e)
        {
            if (diagramControl.LayoutDirection == global::Controls.Diagram.DiagramLayoutDirection.Horizontal)
                UpdateOrientation(global::Controls.Diagram.DiagramLayoutDirection.Vertical, true);
            else
                UpdateOrientation(global::Controls.Diagram.DiagramLayoutDirection.Horizontal, true);
        }
    }
}

