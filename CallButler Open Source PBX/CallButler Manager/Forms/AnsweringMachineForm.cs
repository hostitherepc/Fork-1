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

namespace CallButler.Manager.Forms
{
    public partial class AnsweringMachineForm : Form
    {
        public AnsweringMachineForm(WOSI.CallButler.Data.CallButlerDataset dataset, Guid extensionID)
        {
            InitializeComponent();

            voicemailControl.VoicemailDataset = dataset;
            voicemailControl.ExtensionID = extensionID;

            Utils.PrivateLabelUtils.ReplaceProductNameControl(this);
        }

        private void AnsweringMachineForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            voicemailControl.StopSounds();
        }

        private string GetVoicemailFilePath(WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail)
        {
            return WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.VoicemailSoundCache) + "\\" + voicemail.ExtensionID.ToString() + "\\" + voicemail.VoicemailID.ToString() + ".snd";
        }

        private void voicemailControl_GetVoicemailSound(object sender, CallButler.Manager.Controls.VoicemailEventArgs e)
        {
            string voicemailFile = GetVoicemailFilePath(e.Voicemail);

            if (!File.Exists(voicemailFile))
            {
                // Download our voicemail file if we don't have it locally
                byte[] soundBytes = ManagementInterfaceClient.ManagementInterface.GetVoicemailSound(ManagementInterfaceClient.AuthInfo, e.Voicemail.ExtensionID, e.Voicemail.VoicemailID);

                if (soundBytes != null)
                {
                    WOSI.Utilities.FileUtils.SaveBytesToFile(voicemailFile, soundBytes);
                }
            }

            e.SoundFilename = voicemailFile;
        }

        private void voicemailControl_VoicemailDeleted(object sender, CallButler.Manager.Controls.VoicemailEventArgs e)
        {
            // Delete remotely
            ManagementInterfaceClient.ManagementInterface.DeleteVoicemail(ManagementInterfaceClient.AuthInfo,e.Voicemail.ExtensionID, e.Voicemail.VoicemailID);
        }

        private void voicemailControl_VoicemailRead(object sender, CallButler.Manager.Controls.VoicemailEventArgs e)
        {
            ManagementInterfaceClient.ManagementInterface.PersistVoicemail(ManagementInterfaceClient.AuthInfo, Utils.TableUtils<WOSI.CallButler.Data.CallButlerDataset.VoicemailsDataTable>.CreateTableFromRow(e.Voicemail));
        }
    }
}