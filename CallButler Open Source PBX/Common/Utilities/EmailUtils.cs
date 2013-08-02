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
using System.Net.Mail;
using System.IO;

namespace WOSI.Utilities
{
    public class EmailSendCompleteEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {
        public MailMessage Message;

        public EmailSendCompleteEventArgs(System.ComponentModel.AsyncCompletedEventArgs e, MailMessage message, object cookie) : base (e.Error, e.Cancelled, cookie)
        {
            this.Message = message;
        }
    }

    public class EmailUtils
    {
        private class EmailCookieData
        {
            public MailMessage Message;
            public object Cookie;
        }

        public event EventHandler<EmailSendCompleteEventArgs> SendCompleted;

        public static void SendEmail(string from, string to, string subject, string message, string smtpHost, int smtpPort, bool enableSSL, string username, string password)
        {
            SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort);
                          
            if (username.Length > 0 || password.Length > 0)
            {
                smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
            }

            smtpClient.EnableSsl = enableSSL;

            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

            mailMessage.To.Add(to);
            mailMessage.From = new System.Net.Mail.MailAddress(from);
            mailMessage.Subject = subject;
            mailMessage.Body = message;

            smtpClient.Send(mailMessage);
        }

        public void SendEmailAsync(string from, string to, string subject, string message, string smtpHost, int smtpPort, bool enableSSL, string username, string password, object cookie)
        {
            SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort);

            smtpClient.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);

            if (username.Length > 0 || password.Length > 0)
            {
                smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
            }

            smtpClient.EnableSsl = enableSSL;

            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

            mailMessage.To.Add(to);
            mailMessage.From = new System.Net.Mail.MailAddress(from);
            mailMessage.Subject = subject;
            mailMessage.Body = message;

            EmailCookieData cookieData = new EmailCookieData();
            cookieData.Cookie = cookie;
            cookieData.Message = mailMessage;

            smtpClient.SendAsync(mailMessage, cookieData);
        }

        public void SendEmailAsync(string from, string to, string subject, string message, string smtpHost, int smtpPort, bool enableSSL, string username, string password, string attachmentFile, string attachmentName, object cookie)
        {
            SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort);

            smtpClient.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);

            if (username.Length > 0 || password.Length > 0)
            {
                smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
            }

            smtpClient.EnableSsl = enableSSL;

            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

            mailMessage.To.Add(to);
            mailMessage.From = new System.Net.Mail.MailAddress(from);
            mailMessage.Subject = subject;
            mailMessage.Body = message;

            if (attachmentFile != null && attachmentFile.Length > 0 && File.Exists(attachmentFile))
            {
                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachmentFile);

                if (attachmentName != null && attachmentName.Length > 0)
                    attachment.Name = attachmentName;
                else
                    attachment.Name = Path.GetFileName(attachmentFile);

                mailMessage.Attachments.Add(attachment);
            }

            EmailCookieData cookieData = new EmailCookieData();
            cookieData.Cookie = cookie;
            cookieData.Message = mailMessage;

            smtpClient.SendAsync(mailMessage, cookieData);
        }

        void smtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            EmailCookieData cookieData = e.UserState as EmailCookieData;

            if (cookieData != null && SendCompleted != null)
            {
                SendCompleted(this, new EmailSendCompleteEventArgs(e, cookieData.Message, cookieData.Cookie));
            }
        }
    }
}
