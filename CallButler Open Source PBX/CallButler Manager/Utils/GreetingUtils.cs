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
using System.Windows.Forms;

namespace CallButler.Manager.Utils
{
    class GreetingUtils
    {
        public static string GetLocalizedGreetingSound(WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow localizedGreeting)
        {
            global::Controls.LoadingDialog.ShowDialog(null, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.GreetingUtils_GettingSounds), Properties.Resources.loading, false, 1000);

            // First check to see if the greeting is a sound greeting
            if ((WOSI.CallButler.Data.GreetingType)localizedGreeting.Type == WOSI.CallButler.Data.GreetingType.SoundGreeting)
            {
                string greetingDirectory = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache) + "\\" + localizedGreeting.LanguageID;
                string greetingFilename = greetingDirectory + "\\" + localizedGreeting.GreetingID.ToString() + ".snd";

                // Check to see if we already have the latest greeting
                if (File.Exists(greetingFilename))
                {
                    string localFileChecksum = WOSI.Utilities.CryptoUtils.GetFileChecksum(greetingFilename);

                    if (localFileChecksum == localizedGreeting.Data)
                    {
                        global::Controls.LoadingDialog.HideDialog();
                        return greetingFilename;
                    }
                }

                // If we don't have the greeting, download it
                byte[] soundBytes = ManagementInterfaceClient.ManagementInterface.GetLocalizedGreetingSound(ManagementInterfaceClient.AuthInfo, localizedGreeting.GreetingID, localizedGreeting.LocalizedGreetingID);

                if (!Directory.Exists(greetingDirectory))
                    Directory.CreateDirectory(greetingDirectory);

                WOSI.Utilities.FileUtils.SaveBytesToFile(greetingFilename, soundBytes);

                global::Controls.LoadingDialog.HideDialog();
                return greetingFilename;
            }
            else
            {
                global::Controls.LoadingDialog.HideDialog();
                return null;
            }
        }

        public static void PersistLocalizedGreetingSound(WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow localizedGreeting)
        {
            global::Controls.LoadingDialog.ShowDialog(null, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.GreetingUtils_SavingSounds), Properties.Resources.loading, false, 1000);

            if (localizedGreeting.Type == (short)WOSI.CallButler.Data.GreetingType.SoundGreeting)
            {
                if (localizedGreeting.Data == "CallRecording")
                {
                    ManagementInterfaceClient.ManagementInterface.PersistLocalizedGreetingSound(ManagementInterfaceClient.AuthInfo, localizedGreeting.GreetingID, localizedGreeting.LocalizedGreetingID, null);
                }
                else
                {
                    string greetingFilename = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache) + "\\" + localizedGreeting.LanguageID + "\\" + localizedGreeting.GreetingID.ToString() + ".snd";

                    if (File.Exists(greetingFilename))
                    {
                        byte[] soundBytes = WOSI.Utilities.FileUtils.GetFileBytes(greetingFilename);

                        ManagementInterfaceClient.ManagementInterface.PersistLocalizedGreetingSound(ManagementInterfaceClient.AuthInfo, localizedGreeting.GreetingID, localizedGreeting.LocalizedGreetingID, soundBytes);
                    }
                }
            }

            global::Controls.LoadingDialog.HideDialog();
        }

        public static void EditLocalizedGreeting(WOSI.CallButler.Data.CallButlerDataset greetingsData, Guid greetingID, string languageID, string suggestedText)
        {
            global::Controls.LoadingDialog.ShowDialog(null, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.GreetingUtils_SavingSounds), Properties.Resources.loading, false, 1000);

            WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable localizedGreetings = ManagementInterfaceClient.ManagementInterface.GetLocalizedGreeting(ManagementInterfaceClient.AuthInfo, greetingID, languageID);
            WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsRow localizedGreeting;

            // If the localized greeting doesn't exist, create one
            if (localizedGreetings.Count > 0)
            {
                localizedGreeting = localizedGreetings[0];
            }
            else
            {
                localizedGreeting = localizedGreetings.NewLocalizedGreetingsRow();
                localizedGreeting.GreetingID = greetingID;
                localizedGreeting.LocalizedGreetingID = Guid.NewGuid();
                localizedGreeting.LanguageID = languageID;
                localizedGreeting.Type = (short)WOSI.CallButler.Data.GreetingType.TextGreeting;

                localizedGreetings.AddLocalizedGreetingsRow(localizedGreeting);
            }

            // Download our localized greeting sound
            GetLocalizedGreetingSound(localizedGreeting);

            Forms.GreetingForm greetingForm = new CallButler.Manager.Forms.GreetingForm();
            greetingForm.SuggestedText = suggestedText;
            greetingForm.GreetingControl.LoadGreeting(localizedGreeting, WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache));

            if (greetingForm.ShowDialog() == DialogResult.OK)
            {
                greetingForm.GreetingControl.SaveGreeting(WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.GreetingsSoundCache));

                // Save our greeting remotely
                ManagementInterfaceClient.ManagementInterface.PersistLocalizedGreeting(ManagementInterfaceClient.AuthInfo, localizedGreetings);

                // Upload our greeting file
                PersistLocalizedGreetingSound(localizedGreeting);
            }

            global::Controls.LoadingDialog.HideDialog();
        }
    }
}
