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
using System.Configuration.Install;
using System.Management;

namespace CallButler.Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        public override void Install(System.Collections.IDictionary savedState)
        {
            try
            {
                base.Install(savedState);

                // Turn on "Allow service to interact with desktop".
                // (Note: A published technique to do this by setting a bit in the service's Type registry value (0x100) does not turn this on, so do as follows.)
                // This will let the NotifyIcon appear in the sys tray (with balloon at start-up), but not its context menu.
                ConnectionOptions options = new ConnectionOptions();
                options.Impersonation = ImpersonationLevel.Impersonate;
                ManagementScope scope = new ManagementScope(@"root\CIMv2", options); // Common Information Model, version 2.
                ManagementObject wmiService = new ManagementObject("Win32_Service.Name='" + this.serviceInstaller.ServiceName + "'");
                ManagementBaseObject inParameters = wmiService.GetMethodParameters("Change");
                inParameters["DesktopInteract"] = true;
                ManagementBaseObject outParameters = wmiService.InvokeMethod("Change", inParameters, null);
            }
            catch (Exception e)
            {
                string licenseKey = "";
                string licenseName = "";
                try
                {
                    licenseKey = Properties.Settings.Default.LicenseKey;
                    licenseName = Properties.Settings.Default.LicenseName;
                }
                catch { }

                Service.Services.LoggingService.AddLogEntry(WOSI.CallButler.ManagementInterface.LogLevel.ErrorsOnly, Utils.ErrorUtils.FormatErrorString(e), true);
                CallButler.ExceptionManagement.ErrorCaptureUtils.SendError(e, licenseKey, licenseName, "");
            }
        }
    }
}