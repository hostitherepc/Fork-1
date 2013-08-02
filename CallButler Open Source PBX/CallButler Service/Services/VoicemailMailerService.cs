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
using System.Threading;
using System.IO;

namespace CallButler.Service.Services
{
    public class VoicemailMailerService
    {
        private class VoicemailData
        {
            public WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow Extension;
            public WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow Voicemail;

            public string Subject = null;
            public string Message = null;
            public string AttachmentPath = null;
        }

        private Queue<VoicemailData> vmMailQueue;
        private Timer queueTimer;
        private WOSI.Utilities.EmailUtils emailer;
        
        public VoicemailMailerService()
        {
            emailer = new WOSI.Utilities.EmailUtils();
            emailer.SendCompleted += new EventHandler<WOSI.Utilities.EmailSendCompleteEventArgs>(emailer_SendCompleted);
            vmMailQueue = new Queue<VoicemailData>();

            TimerCallback timerDelegate = new TimerCallback(VMQueueTimerProc);
            
            // Create our queue timer
            queueTimer = new Timer(timerDelegate, null, 1000, 30000);
        }

        void emailer_SendCompleted(object sender, WOSI.Utilities.EmailSendCompleteEventArgs e)
        {
            string tmpVoicemailName = e.UserState as string;

            // Delete our temporary voicemail file
            if (tmpVoicemailName != null)
            {
                // Dispose of our message first
                e.Message.Dispose();
                e.Message = null;

                if (File.Exists(tmpVoicemailName))
                    File.Delete(tmpVoicemailName);
            }
        }

        private void VMQueueTimerProc(object state)
        {
            while (vmMailQueue.Count > 0)
            {
                try
                {
                    VoicemailData vmData = vmMailQueue.Dequeue();

                    if (vmData.Extension != null && vmData.Extension.EmailNotification)
                    {
                        // Create our message stuff
                        string subject = vmData.Subject;

                        if (subject == null)
                        {
                            subject = "You have a voicemail message from ";

                            if (vmData.Voicemail.CallerDisplayName.Length > 0)
                                subject += vmData.Voicemail.CallerDisplayName;
                            else if (vmData.Voicemail.CallerUsername.Length > 0)
                                subject += WOSI.Utilities.StringUtils.FormatPhoneNumber(vmData.Voicemail.CallerUsername);
                            else
                                subject += "an unknown caller";
                        }

                        string message = vmData.Message;

                        if (message == null)
                        {
                            message = "Date:\t\t" + vmData.Voicemail.Timestamp.ToLongDateString() + "\r\n";
                            message += "Time:\t\t" + vmData.Voicemail.Timestamp.ToShortTimeString() + "\r\n";

                            if (vmData.Voicemail.CallerDisplayName.Length > 0)
                                message += "From:\t\t" + vmData.Voicemail.CallerDisplayName;
                            else
                                message += "From:\t\tUnknown Caller";

                            message += " - " + WOSI.Utilities.StringUtils.FormatPhoneNumber(vmData.Voicemail.CallerUsername) + "\r\n";
                            message += "To:\t\t" + vmData.Extension.FirstName + " " + vmData.Extension.LastName;
                        }

                        try
                        {
                            string smtpPassword = "";

                            if (Properties.Settings.Default.SMTPPassword.Length > 0)
                                smtpPassword = WOSI.Utilities.CryptoUtils.Decrypt(Properties.Settings.Default.SMTPPassword, WOSI.CallButler.Data.Constants.EncryptionPassword);

                            string voicemailFilename = vmData.AttachmentPath;

                            if (voicemailFilename == null)
                                voicemailFilename = WOSI.Utilities.FileUtils.GetApplicationRelativePath(Properties.Settings.Default.VoicemailRootDirectory) + "\\" + vmData.Voicemail.ExtensionID.ToString() + "\\" + vmData.Voicemail.VoicemailID + ".snd";

                            if ((vmData.AttachmentPath != null || vmData.Extension.EmailAttachment) && File.Exists(voicemailFilename))
                            {
                                // Make a copy of our voicemail file
                                string tmpFilename = Path.GetTempPath() + "\\";
                                string attachmentName = "voicemail.wav";

                                if (vmData.AttachmentPath != null)
                                {
                                    tmpFilename += Path.GetFileName(vmData.AttachmentPath);
                                    attachmentName = Path.GetFileName(vmData.AttachmentPath);
                                }
                                else
                                {
                                    tmpFilename += vmData.Voicemail.VoicemailID + ".snd";
                                }

                                File.Copy(voicemailFilename, tmpFilename, true);

                                emailer.SendEmailAsync(Properties.Settings.Default.SMTPEmailFrom, vmData.Extension.EmailAddress, subject, message, Properties.Settings.Default.SMTPServer, Properties.Settings.Default.SMTPPort, Properties.Settings.Default.SMTPUseSSL, Properties.Settings.Default.SMTPUsername, smtpPassword, tmpFilename, attachmentName, tmpFilename);
                            }
                            else
                            {
                                emailer.SendEmailAsync(Properties.Settings.Default.SMTPEmailFrom, vmData.Extension.EmailAddress, subject, message, Properties.Settings.Default.SMTPServer, Properties.Settings.Default.SMTPPort, Properties.Settings.Default.SMTPUseSSL, Properties.Settings.Default.SMTPUsername, smtpPassword, null);
                            }

                            LoggingService.AddLogEntry(WOSI.CallButler.ManagementInterface.LogLevel.Extended, string.Format("Voicemail Email sent to: {0}", vmData.Extension.EmailAddress), false);
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                }
                catch(Exception e)
                {
                    LoggingService.AddLogEntry(WOSI.CallButler.ManagementInterface.LogLevel.ErrorsOnly, Utils.ErrorUtils.FormatErrorString(e), true);
                }
            }
        }

        public void QueueVoicemailEmail(WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension, string subject, string message, string attachmentPath)
        {
            VoicemailData vmData = new VoicemailData();
            vmData.Extension = extension;
            vmData.AttachmentPath = attachmentPath;
            vmData.Message = message;
            vmData.Subject = subject;

            vmMailQueue.Enqueue(vmData);
        }

        public void QueueVoicemailEmail(WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension, WOSI.CallButler.Data.CallButlerDataset.VoicemailsRow voicemail)
        {
            VoicemailData vmData = new VoicemailData();
            vmData.Extension = extension;
            vmData.Voicemail = voicemail;

            vmMailQueue.Enqueue(vmData);
        }
    }
}
