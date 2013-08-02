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
using System.IO;
using CallButler.Manager.Controls;
namespace CallButler.Manager.Forms
{
    public partial class PrebuiltConfigForm : System.Windows.Forms.Form
    {
        private int pageIndex = 0;
        private WOSI.CallButler.Data.CallButlerDataset cbDataset;

        private Dictionary<string, WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow> greetingValues;

        public PrebuiltConfigForm()
        {
            greetingValues = new Dictionary<string, WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow>();

            InitializeComponent();

            cbDataset = new WOSI.CallButler.Data.CallButlerDataset();

            wizMain.PageIndex = 0;

            prebuiltConfigControl.LoadData();

            Utils.PrivateLabelUtils.ReplaceProductNameControl(this);
        }

        public WOSI.CallButler.Data.CallButlerDataset ConfigurationData
        {
            get
            {
                return cbDataset;
            }
        }

        private void prebuiltConfigControl_SelectedConfigurationChanged(object sender, EventArgs e)
        {
            if (prebuiltConfigControl.SelectedConfiguration != null)
            {
                btnNext.Enabled = true;
            }
            else
            {
                btnNext.Enabled = false;
            }
        }

        private void UpdateCurrentValue()
        {
            // Get our configuration inputs
            Data.PrebuiltConfigData.InputRow[] inputRows = prebuiltConfigControl.SelectedConfiguration.GetInputRows();

            int inputIndex = pageIndex - 1;

            if (inputIndex >= 0 && inputIndex < inputRows.Length)
            {
                Data.PrebuiltConfigData.InputRow inputRow = inputRows[inputIndex];

                if (inputRow.Type == "Text")
                {
                    inputRow.Value = txtTextValue.Text;
                }
                else if (inputRow.Type == "Greeting")
                {
                    if(greetingValues.ContainsKey(inputRow.Name))
                        greetingControl.SaveGreeting(WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache), greetingValues[inputRow.Name]);
                }
            }
        }

        private void UpdatePage(bool moveForward)
        {
            greetingControl.StopSounds();

            UpdateCurrentValue();

            if(moveForward)
                pageIndex++;
            else
                pageIndex--;

            if (pageIndex == 0)
                btnBack.Enabled = false;
            else
                btnBack.Enabled = true;

            btnNext.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.PrebuiltConfigForm_Next);

            if (pageIndex == 0)
                wizMain.PageIndex = 0;
            else
            {
                // Get our configuration inputs
                Data.PrebuiltConfigData.InputRow[] inputRows = prebuiltConfigControl.SelectedConfiguration.GetInputRows();

                int inputIndex = pageIndex - 1;

                if (inputIndex >= 0 && inputIndex < inputRows.Length)
                {
                    Data.PrebuiltConfigData.InputRow inputRow = inputRows[inputIndex];

                    if (inputRow.Display)
                    {
                        if (inputRow.Type == "Text")
                        {
                            wizMain.PageIndex = 1;

                            lblTextTitle.Text = inputRow.Title;
                            lblTextDescription.Text = inputRow.Description;
                            lblTextDisplayName.Text = inputRow.DisplayName;
                            txtTextValue.Text = inputRow.Value;
                            txtTextValue.Select();
                        }
                        else if (inputRow.Type == "Greeting")
                        {
                            lblGreetingTitle.Text = inputRow.Title;
                            lblGreetingDescription.Text = inputRow.Description;
                            greetingControl.SuggestedText = inputRow.DisplayName;

                            if (!greetingValues.ContainsKey(inputRow.Name))
                            {
                                greetingValues[inputRow.Name] = cbDataset.LocalizedGreetings.NewLocalizedGreetingsRow();
                                greetingValues[inputRow.Name].GreetingID = new Guid(inputRow.Value);
                                greetingValues[inputRow.Name].LocalizedGreetingID = Guid.NewGuid();
                                greetingValues[inputRow.Name].LanguageID = ManagementInterfaceClient.ManagementInterface.GetDefaultLanguage(ManagementInterfaceClient.AuthInfo);
                                greetingValues[inputRow.Name].Type = (short)WOSI.CallButler.Data.GreetingType.SoundGreeting;
                            }

                            greetingControl.LoadGreeting(greetingValues[inputRow.Name], WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache));

                            wizMain.PageIndex = 2;
                        }
                    }
                    else
                    {
                        UpdatePage(moveForward);
                    }
                }
                else if (pageIndex != 0)
                {
                    btnNext.Text = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.PrebuiltConfigForm_Finish);

                    wizMain.PageIndex = 3;
                }
            }
        }

        private void Finish()
        {
            // Check to see if our config data file exists
            string dataFilePath = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.PrebuiltConfigDataPath) + prebuiltConfigControl.SelectedConfiguration.Filename;

            if (File.Exists(dataFilePath))
            {
                // Load our configuration data
                TextReader configReader = (TextReader)new StreamReader(dataFilePath);

                string configData = configReader.ReadToEnd();

                configReader.Close();

                // Fill in our input data
                Data.PrebuiltConfigData.InputRow[] inputRows = prebuiltConfigControl.SelectedConfiguration.GetInputRows();

                foreach (Data.PrebuiltConfigData.InputRow inputRow in inputRows)
                {
                    if (inputRow.Type == "Text")
                    {
                        configData = configData.Replace("#" + inputRow.Name + "#", inputRow.Value);
                    }
                }

                StringReader sr = new StringReader(configData);

                try
                {
                    cbDataset.EnforceConstraints = false;
                    cbDataset.ReadXml(sr);

                    System.Collections.IEnumerator dictEnum = greetingValues.GetEnumerator();

                    while(dictEnum.MoveNext())
                    {
                        cbDataset.LocalizedGreetings.AddLocalizedGreetingsRow(((KeyValuePair<string, WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow>)dictEnum.Current).Value);
                    }
                }
                catch
                {
                }

                sr.Close();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (wizMain.Page == pgFinish)
            {
                Finish();
            }
            else
                UpdatePage(true);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            UpdatePage(false);
        }
    }
}