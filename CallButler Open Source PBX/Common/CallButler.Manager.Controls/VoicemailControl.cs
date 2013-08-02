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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WOSI.CallButler.Data;

namespace CallButler.Manager.Controls
{
    public partial class VoicemailControl : UserControl
    {
        public event EventHandler<VoicemailEventArgs> VoicemailRead;
        public event EventHandler<VoicemailEventArgs> GetVoicemailSound;
        public event EventHandler<VoicemailEventArgs> VoicemailDeleted;

        private WOSI.Utilities.Sound.SoundPlayer soundPlayer;
        private Guid extensionID;
        private bool allowUpdates = true;
        private WOSI.CallButler.Data.CallButlerDataset.VoicemailsDataTable fastUpdateTable;

        private global::Utilities.ContactManagement.OutlookContactManager contactManager;

        public VoicemailControl()
        {
            InitializeComponent();

            soundPlayer = new WOSI.Utilities.Sound.SoundPlayer();
            soundPlayer.SoundFinishedPlaying += new EventHandler(soundPlayer_SoundFinishedPlaying);
            soundPlayer.SoundPlaying += new EventHandler<WOSI.Utilities.Sound.SoundPlayingEventArgs>(soundPlayer_SoundPlaying);
            try
            {
                trkVolume.Value = (int)soundPlayer.SoundVolume;
            }
            catch 
            {
                trkVolume.Enabled = false;
                //trkVolume.Value = 0;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!WOSI.Utilities.ControlUtils.DesignMode)
            {
                contactManager = new global::Utilities.ContactManagement.OutlookContactManager();
            }
        }

        void soundPlayer_SoundPlaying(object sender, WOSI.Utilities.Sound.SoundPlayingEventArgs e)
        {
            progSound.MaxValue = e.TotalSamples;
            progSound.UpperValue = e.SamplesPlayed;
        }

        void soundPlayer_SoundFinishedPlaying(object sender, EventArgs e)
        {
            StopSound(true);
        }

        public void StopSounds()
        {
            StopSound(false);
        }

        private void UpdateMessageCount()
        {
            if (bsVoicemails.Count > 0)
                lblMessageCount.Text = string.Format(Properties.LocalizedStrings.VoicemailControl_MessageNumber, bsVoicemails.Position + 1, bsVoicemails.Count);
            else
                lblMessageCount.Text = Properties.LocalizedStrings.VoicemailControl_NoMessages;
        }

        private void StopSound(bool markAsRead)
        {
            soundPlayer.StopSound();

            progSound.LowerValue = 0;
            progSound.UpperValue = 0;

            btnStop.Enabled = false;
            btnPlay.Enabled = true;

            // If our current voicemail is new, mark it old
            if (markAsRead)
            {
                WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail = GetSelectedVoicemail();

                if (voicemail != null & voicemail.IsNew)
                {
                    voicemail.IsNew = false;

                    if (VoicemailRead != null)
                        VoicemailRead(this, new VoicemailEventArgs(voicemail));
                }
            }

            if (fastUpdateTable != null)
            {
                RefreshVoicemails(fastUpdateTable);
                fastUpdateTable = null;
            }
        }

        private WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow GetSelectedVoicemail()
        {
            if (dgVoicemails.SelectedRows.Count > 0)
                return (WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow)((DataRowView)dgVoicemails.SelectedRows[0].DataBoundItem).Row;
            else
                return null;
        }

        public Guid ExtensionID
        {
            get
            {
                return extensionID;
            }
            set
            {
                extensionID = value;
                try
                {
                    bsVoicemails.Filter = "ExtensionID = '" + extensionID.ToString() + "'";
                    if (bsVoicemails.Sort == null || bsVoicemails.Sort.Equals("Timestamp DESC") == false)
                    {
                        bsVoicemails.Sort = "Timestamp DESC";
                    }
                }
                catch { }

                UpdateUI();
            }
        }

        public WOSI.CallButler.Data.CallButlerDataset VoicemailDataset
        {
            get
            {
                return voicemailDataset;
            }
            set
            {
                 voicemailDataset = value;
                 bsVoicemails.DataSource = voicemailDataset;
                 UpdateUI();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            StopSound(false);

            if (dgVoicemails.SelectedRows.Count > 0)
            {
                int currentIndex = dgVoicemails.SelectedRows[0].Index;

                dgVoicemails.Rows[currentIndex - 1].Selected = true;
                bsVoicemails.Position = currentIndex - 1;
            }
            UpdateUI();
        }

        public void RefreshVoicemails(WOSI.CallButler.Data.CallButlerDataset.VoicemailsDataTable newVoicemailTable)
        {
                if (soundPlayer.Playing == false)
                {
                    for (int i = voicemailDataset.Voicemails.Count - 1; i >= 0; i--)
                    {
                        CallButlerDataset.VoicemailsRow row = (CallButlerDataset.VoicemailsRow)voicemailDataset.Voicemails[i];
                        if (row.RowState == DataRowState.Deleted)
                        {
                            continue;
                        }
                        if (newVoicemailTable.FindByVoicemailID(row.VoicemailID) == null)
                        {
                            lock (voicemailDataset.Voicemails)
                            {
                                voicemailDataset.Voicemails.RemoveVoicemailsRow(row);
                            }
                        }
                    }

                    voicemailDataset.Voicemails.Merge(newVoicemailTable);
                    voicemailDataset.AcceptChanges();
                    RefreshVoicemails();
                }
                else
                {
                    fastUpdateTable = newVoicemailTable;
                }
        }

        public void RefreshVoicemails()
        {
            try
            {
                bsVoicemails.DataSource = voicemailDataset;
            }
            catch 
            {
                //TODO: Research this more. Possible .net framework bug
            }
        }

        private void UpdateUI()
        {
            UpdateMessageCount();

            if (dgVoicemails.SelectedRows.Count > 0)
            {
                WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail = GetSelectedVoicemail();

                if (voicemail.IsNew)
                    lblNewMessage.Text = Properties.LocalizedStrings.VoicemailControl_New;
                else
                    lblNewMessage.Text = "";

                lblDateTime.Text = voicemail.Timestamp.ToString("MMM d yyyy, h:mm tt");

                if (voicemail.CallerDisplayName.Length > 0)
                    lblCallerID.Text = voicemail.CallerDisplayName;
                else
                    lblCallerID.Text = Properties.LocalizedStrings.VoicemailControl_UnknownCaller;

                lblTelephoneNumber.Text = WOSI.Utilities.StringUtils.FormatPhoneNumber(voicemail.CallerUsername);
                
                btnPlay.Enabled = !soundPlayer.Playing;
                btnSaveMessage.Enabled = true;
                btnDeleteMessage.Enabled = true;

                if (dgVoicemails.SelectedRows[0].Index > 0)
                    btnPrevious.Enabled = true;
                else
                    btnPrevious.Enabled = false;

                if (dgVoicemails.SelectedRows[0].Index < bsVoicemails.Count - 1)
                    btnNext.Enabled = true;
                else
                    btnNext.Enabled = false;

                if (contactManager != null)
                {
                    if (contactManager.IsInstalled)
                        btnImportOutlook.Enabled = true;
                    else
                        btnImportOutlook.Enabled = false;
                }
            }
            else
            {
                lblNewMessage.Text = "";

                lblDateTime.Text = "";
                lblCallerID.Text = "";

                lblTelephoneNumber.Text = "";


                btnPlay.Enabled = false;
                btnStop.Enabled = false;
                btnSaveMessage.Enabled = false;
                btnDeleteMessage.Enabled = false;
                btnNext.Enabled = false;
                btnPrevious.Enabled = false;
                
                btnImportOutlook.Enabled = false;
            }


        }

        public bool AllowUpdates
        {
            get
            {
                return allowUpdates;
            }
            set
            {
                allowUpdates = value;
            }
        }

        public void SelectVoicemailByID(Guid voicemailID)
        {
            for (int index = 0; index < dgVoicemails.Rows.Count; index++)
            {
                WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail = (WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow)((DataRowView)dgVoicemails.Rows[index].DataBoundItem).Row;

                if (voicemail.VoicemailID == voicemailID)
                {
                    dgVoicemails.Rows[index].Selected = true;
                    return;
                }
            }
        }

        private void dgVoicemails_SelectionChanged(object sender, EventArgs e)
        {
            if (allowUpdates)
            {
                StopSound(false);
                UpdateUI();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            StopSound(false);

            if (dgVoicemails.SelectedRows.Count > 0)
            {
                int currentIndex = dgVoicemails.SelectedRows[0].Index;

                dgVoicemails.Rows[currentIndex + 1].Selected = true;
                bsVoicemails.Position = currentIndex + 1;
            }
            UpdateUI();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            StopSound(false);

            // Check to see if our voicemail already exists
            WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail = GetSelectedVoicemail();

            if (voicemail != null)
            {
                if (GetVoicemailSound != null)
                {
                    VoicemailEventArgs vmArgs = new VoicemailEventArgs(voicemail);
                    GetVoicemailSound(this, vmArgs);

                    if (File.Exists(vmArgs.SoundFilename))
                    {
                        btnStop.Enabled = true;
                        btnPlay.Enabled = false;

                        // Finally play our file
                        soundPlayer.PlaySoundAsync(vmArgs.SoundFilename);
                    }
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopSound(true);
        }

        private void btnDeleteMessage_Click(object sender, EventArgs e)
        {
            StopSound(false);

            if (MessageBox.Show(this, Properties.LocalizedStrings.VoicemailControl_ConfirmDelete, Properties.LocalizedStrings.VoicemailControl_ConfirmDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail = GetSelectedVoicemail();

                if (voicemail != null)
                {
                    // Delete remotely
                    if (VoicemailDeleted != null)
                        VoicemailDeleted(this, new VoicemailEventArgs(voicemail));

                    // Delete locally
                    voicemail.Delete();
                }
            }
        }

        private void btnSaveMessage_Click(object sender, EventArgs e)
        {
            WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow vRow = GetSelectedVoicemail();
            if (vRow != null)
            {
                saveFileDialog.FileName = vRow.CallerUsername + ".snd";
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    // Check to see if our voicemail already exists
                    if (GetVoicemailSound != null)
                    {
                        VoicemailEventArgs vmArgs = new VoicemailEventArgs(vRow);
                        GetVoicemailSound(this, vmArgs);

                        if (vmArgs.SoundFilename != String.Empty)
                        {
                            System.IO.File.Copy(vmArgs.SoundFilename, saveFileDialog.FileName, true);
                        }
                    }
                }
            }
        }

        private void trkVolume_ValueChanged(object sender, EventArgs e)
        {
            soundPlayer.SoundVolume = (int)trkVolume.Value;
        }

        private void dgVoicemails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == callerUsernameDataGridViewTextBoxColumn.Index)
                e.Value = WOSI.Utilities.StringUtils.FormatPhoneNumber((string)e.Value);
            else if (e.ColumnIndex == colVoicemailImage.Index)
            {
                WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail = (WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow)((DataRowView)dgVoicemails.Rows[e.RowIndex].DataBoundItem).Row;

                if (voicemail.IsNew)
                    e.Value = Properties.Resources.message_information_16;
            }
        }

        private void btnImportOutlook_Click(object sender, EventArgs e)
        {
            WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail = GetSelectedVoicemail();

            if (voicemail != null)
            {
                global::Utilities.ContactManagement.IContactItem item = contactManager.SearchContact(voicemail.CallerUsername, voicemail.CallerDisplayName);
                if (item == null)
                {
                    item = global::Utilities.ContactManagement.ContactItemFactory.CreateContactItem(contactManager, voicemail.CallerDisplayName, voicemail.CallerUsername);

                }

                contactManager.ShowContactForm(item);
            }
        }
    }

    public class VoicemailEventArgs : EventArgs
    {
        public WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow Voicemail;
        public string SoundFilename = "";

        public VoicemailEventArgs(WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail)
        {
            this.Voicemail = voicemail;
        }

        public VoicemailEventArgs(WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail, string soundFilename)
        {
            this.Voicemail = voicemail;
            this.SoundFilename = soundFilename;
        }
    }
}
