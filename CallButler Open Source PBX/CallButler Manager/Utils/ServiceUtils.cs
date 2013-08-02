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
using System.ServiceProcess;
using System.Diagnostics;

namespace CallButler.Manager.Utils
{
    public class ServiceUtils
    {
        public static void RestartCallButlerService(string server)
        {
            if (server.ToLower().Equals("localhost"))
            {
                server = "127.0.0.1";
            }
            global::Controls.LoadingDialog.ShowDialog(null, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.MainForm_StartingService), Properties.Resources.loading, false, 0);
            try
            {
                ServiceController sc = new ServiceController("CallButler Service", server);
                
                if (sc.Status != ServiceControllerStatus.Stopped)
                {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                }
                
                sc.Start();
                sc.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                global::Controls.LoadingDialog.HideDialog();
            }
            catch
            {
                try
                {
                    Process [] processes = Process.GetProcessesByName("CallButler Service");
                    
                    if (processes.Length > 0)
                    {
                        processes[0].Kill();
                    }
                    
                    string cbServicePath = WOSI.Utilities.FileUtils.GetApplicationRelativePath("") + "\\..\\Service\\CallButler Service.exe";

                    if (System.IO.File.Exists(cbServicePath))
                    {
                        System.Diagnostics.Process.Start(cbServicePath, "-a");

                        // Wait a few seconds for the app to start up
                        System.Threading.Thread.Sleep(5000);
                        global::Controls.LoadingDialog.HideDialog();

                    }
                    else
                    {
                        global::Controls.LoadingDialog.HideDialog();
                        System.Windows.Forms.MessageBox.Show(null, "Unable to restart CallButler Service", "Service Restart Failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    }
                }
                catch(Exception ex)
                {
                    global::Controls.LoadingDialog.HideDialog();
                    System.Windows.Forms.MessageBox.Show(null, "Unable to restart CallButler Service", "Service Restart Failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
        }
    }
}
