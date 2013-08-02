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
using System.Windows.Forms;

namespace CallButler.Manager.Utils
{
    class EmailUtils
    {

        public static void SendTestEmail(string smtpServer, int smtpPort, bool useSSL, string smtpUsername, string smtpPassword)
        {
            SendTestEmail(null, smtpServer, smtpPort, useSSL, smtpUsername, smtpPassword);
        }

        public static void SendTestEmail(string emailAddress)
        {
            // Send a test Email
            string emailMessage = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.EmailUtils_TestEmail);
            string sendTo = emailAddress;

            if (sendTo == null)
            {
                global::Controls.InputBoxResult result = global::Controls.InputBox.ShowDialog(null, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_EnterValidEmail), "", null);

                if (result.DialogResult == System.Windows.Forms.DialogResult.OK)
                    sendTo = result.Value;
                else
                    return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ManagementInterfaceClient.ManagementInterface.SendEmail(ManagementInterfaceClient.AuthInfo, emailAddress, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.EmailUtils_TestEmailSubject), emailMessage);
                Cursor.Current = Cursors.Default;

                MessageBox.Show(String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.EmailUtils_EmailSuccess), sendTo), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_EmailSent), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_EmailProblem), sendTo, exception.Message), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_EmailError), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void SendTestEmail(string emailAddress, string smtpServer, int smtpPort, bool useSSL, string smtpUsername, string smtpPassword)
        {
            // Send a test Email
            string emailMessage = CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.EmailUtils_TestEmail);
            string sendTo = emailAddress;

            if (sendTo == null)
            {
                global::Controls.InputBoxResult result = global::Controls.InputBox.ShowDialog(null, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_EnterValidEmail), "", null);

                if (result.DialogResult == System.Windows.Forms.DialogResult.OK)
                    sendTo = result.Value;
                else
                    return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ManagementInterfaceClient.ManagementInterface.SendTestEmail(ManagementInterfaceClient.AuthInfo, sendTo, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.EmailUtils_TestEmailSubject), emailMessage, smtpServer, smtpPort, useSSL, smtpUsername, smtpPassword);
                Cursor.Current = Cursors.Default;

                MessageBox.Show(String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.EmailUtils_EmailSuccess), sendTo), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_EmailSent), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(String.Format(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_EmailProblem), sendTo, exception.Message), CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Common_EmailError), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
