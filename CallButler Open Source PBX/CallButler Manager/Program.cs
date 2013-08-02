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
using System.Windows.Forms;

namespace CallButler.Manager
{
    static class Program
    {
        public static bool DemoMode = false;
        public static bool AdminMode = false;
        public static bool RemoteMode = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                foreach (string arg in args)
                {
                    if (arg.StartsWith("-d", true, System.Globalization.CultureInfo.InvariantCulture) || arg.StartsWith("/d", true, System.Globalization.CultureInfo.InvariantCulture))
                        DemoMode = true;

                    if (arg.StartsWith("-admin", true, System.Globalization.CultureInfo.InvariantCulture) || arg.StartsWith("/admin", true, System.Globalization.CultureInfo.InvariantCulture))
                        AdminMode = true;

                    if (arg.StartsWith("-r", true, System.Globalization.CultureInfo.InvariantCulture) || arg.StartsWith("/r", true, System.Globalization.CultureInfo.InvariantCulture))
                        RemoteMode = true;
                }

                if(Properties.Settings.Default.UICulture == null || Properties.Settings.Default.UICulture.Length == 0)
                    System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
                else
                    System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Properties.Settings.Default.UICulture);

                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.DoEvents();

                global::Controls.LoadingDialog.ShowDialog(null, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.Program_Initializing), Properties.Resources.loading, false, 0);

                MainForm mainForm = new MainForm();

                mainForm.ProcessCommandArgs(args);

                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                if (ex is System.Threading.ThreadAbortException)
                    System.Threading.Thread.ResetAbort();
                else
                    RemotingExceptionManager.ProcessException(ex);
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is System.Threading.ThreadAbortException)
                System.Threading.Thread.ResetAbort();
            else
                RemotingExceptionManager.ProcessException((Exception) e.ExceptionObject);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (e.Exception is System.Threading.ThreadAbortException)
                System.Threading.Thread.ResetAbort();
            else
                RemotingExceptionManager.ProcessException(e.Exception);
        }
    }
}