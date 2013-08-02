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
using System.Globalization;

namespace CallButler.Manager.Forms
{
    public partial class LangageSelectionForm : CallButler.Manager.Controls.CallButlerDialogFormBase
    {
        public LangageSelectionForm(string currentLanguages, string excludedLanguages)
        {
            InitializeComponent();

            // Fill in our current selected languages
            List<string> languagesList = new List<string>(currentLanguages.Split(';'));
            List<string> excludedLanguagesList = new List<string>(excludedLanguages.Split(';'));

            foreach (string language in languagesList)
            {
                try
                {
                    if (language.Length > 0 && !excludedLanguagesList.Contains(language))
                    {
                        lbSelectedLanguages.Items.Add(new LanguageItem(CultureInfo.GetCultureInfoByIetfLanguageTag(language.Trim())));
                    }
                }
                catch
                {
                }
            }

            // Fill in our system languages
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            foreach (CultureInfo culture in cultures)
            {
                // Only add if the language is not in our excluded language list and not in our selected languages
                if(!excludedLanguagesList.Contains(culture.IetfLanguageTag) && !languagesList.Contains(culture.IetfLanguageTag))
                    lbAllLanguages.Items.Add(new LanguageItem(culture));
            }

            Utils.PrivateLabelUtils.ReplaceProductNameControl(this);
        }

        public string SelectedLanguages
        {
            get
            {
                StringBuilder languageString = new StringBuilder();

                foreach (LanguageItem languageItem in lbSelectedLanguages.Items)
                {
                    languageString.AppendFormat("{0};", languageItem.Culture.IetfLanguageTag);
                }

                return languageString.ToString().TrimEnd(';');
            }
        }

        private void lbAllLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbAllLanguages.SelectedItems.Count > 0)
            {
                btnAddSelectedLanguage.Enabled = true;
            }
            else
            {
                btnAddSelectedLanguage.Enabled = false;
            }
        }

        private void lbSelectedLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSelectedLanguages.SelectedItems.Count > 0)
            {
                btnRemoveSelectedLanguage.Enabled = true;
            }
            else
            {
                btnRemoveSelectedLanguage.Enabled = false;
            }
        }

        private void btnAddSelectedLanguage_Click(object sender, EventArgs e)
        {
            while(lbAllLanguages.SelectedItems.Count > 0)
            {
                LanguageItem li = (LanguageItem)lbAllLanguages.SelectedItems[0];

                lbAllLanguages.Items.Remove(li);
                lbSelectedLanguages.Items.Add(li);
            }
        }

        private void btnAddAllLanguages_Click(object sender, EventArgs e)
        {
            while (lbAllLanguages.Items.Count > 0)
            {
                LanguageItem li = (LanguageItem)lbAllLanguages.Items[0];

                lbAllLanguages.Items.Remove(li);
                lbSelectedLanguages.Items.Add(li);
            }
        }

        private void btnRemoveSelectedLanguage_Click(object sender, EventArgs e)
        {
            while (lbSelectedLanguages.SelectedItems.Count > 0)
            {
                LanguageItem li = (LanguageItem)lbSelectedLanguages.SelectedItems[0];

                lbSelectedLanguages.Items.Remove(li);
                lbAllLanguages.Items.Add(li);
            }
        }

        private void btnRemoveAllLanguages_Click(object sender, EventArgs e)
        {
            while (lbSelectedLanguages.Items.Count > 0)
            {
                LanguageItem li = (LanguageItem)lbSelectedLanguages.Items[0];

                lbSelectedLanguages.Items.Remove(li);
                lbAllLanguages.Items.Add(li);
            }
        }
    }

    public class LanguageItem
    {
        public CultureInfo Culture;

        public LanguageItem(CultureInfo culture)
        {
            Culture = culture;
        }

        public override string ToString()
        {
            return String.Format("{0} - {1}", Culture.NativeName, Culture.EnglishName);
        }
    }
}