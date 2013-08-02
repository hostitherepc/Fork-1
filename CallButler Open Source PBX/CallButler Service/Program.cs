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
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using CallButler.ExceptionManagement;

namespace CallButler.Service
{
    static class Program
    {
        public static bool runAsApplication = false;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool resetSettings = false;

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            try
            {
                // Lock our license so that only one instance of this server can run on this machine
                /*if (!Licensing.Management.LicenseManager.LockLicense(Properties.Settings.Default.ProductID, Properties.Settings.Default.LicenseKey, true))
                {
                    return;
                }
                else
                {*/
                    foreach (string arg in args)
                    {
                        string argVal = arg.ToUpper();

                        if (argVal == "/A" || argVal == "-A")
                            runAsApplication = true;
                        else if (argVal == "/R" || argVal == "-R")
                        {
                            Properties.Settings.Default.Reset();
                            Properties.Settings.Default.Save();
                            resetSettings = true;
                        }
                        else if (argVal == "/RP" || argVal == "-RP")
                        {
                            Properties.Settings.Default.ManagementPassword = "";
                            Properties.Settings.Default.Save();
                            resetSettings = true;
                        }
                    }

                    if (runAsApplication)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.DoEvents();

                        CallButlerService service = new CallButlerService();

                        service.NotifyIconVisible = true;

                        service.StartService(args);

                        Application.Run();

                        service.NotifyIconVisible = false;

                        service.StopService();
                    }
                    else if(!resetSettings)
                    {
                        ServiceBase[] ServicesToRun;

                        CallButlerService service = new CallButlerService();

                        service.NotifyIconVisible = false;

                        ServicesToRun = new ServiceBase[] { service };

                        ServiceBase.Run(ServicesToRun);
                    }
                //}

                //Licensing.Management.LicenseManager.UnlockLicense();
            }
            catch (Exception ex)
            {
                string licenseKey = "";
                string licenseName = "";
                try
                {
                    licenseKey = Properties.Settings.Default.LicenseKey;
                    licenseName = Properties.Settings.Default.LicenseName;
                }
                catch { }
                Service.Services.LoggingService.AddLogEntry(WOSI.CallButler.ManagementInterface.LogLevel.ErrorsOnly, Utils.ErrorUtils.FormatErrorString(ex), true);
                if (Properties.Settings.Default.ReportErrors)
                {
                    ErrorCaptureUtils.SendError(ex, licenseKey, licenseName,Application.ProductVersion, runAsApplication);
                }
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is System.Threading.ThreadAbortException)
            {
            }
            else
            {
                if (e.ExceptionObject is Exception)
                {
                    Application_ThreadException(sender, new System.Threading.ThreadExceptionEventArgs((Exception)e.ExceptionObject));
                }
                else
                {
                    Application_ThreadException(sender, new System.Threading.ThreadExceptionEventArgs(new Exception(e.ExceptionObject.ToString())));
                }
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string licenseKey = "";
            string licenseName = "";

            try
            {
                licenseKey = Properties.Settings.Default.LicenseKey;
                licenseName = Properties.Settings.Default.LicenseName;
            }
            catch { }
            Service.Services.LoggingService.AddLogEntry(WOSI.CallButler.ManagementInterface.LogLevel.ErrorsOnly, Utils.ErrorUtils.FormatErrorString(e.Exception), true);
            if (Properties.Settings.Default.ReportErrors)
            {
                ErrorCaptureUtils.SendError(e.Exception, licenseKey, licenseName, Application.ProductVersion, runAsApplication);
            }
        }
    }
}