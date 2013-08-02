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
using System.Reflection;

using WOSI.Utilities;

namespace CallButler.ExceptionManagement
{
    public class ExceptionEventArgs : EventArgs
    {
        public Exception Exception;

        public ExceptionEventArgs(Exception exception)
        {
            this.Exception = exception;
        }
    }

    public class ErrorCaptureUtils
    {
        public static event EventHandler<ExceptionEventArgs> OnSendError;

        public static void SendError(Exception ex, string licenseKey, string licenseName, string version)
        {
            
            SendError(ex, licenseKey, licenseName, version, false);
        }

        public static void SendError(Exception ex, string licenseKey, string licenseName, string version, bool showDialog)
        {
            try
            {
                if (OnSendError != null)
                {
                    OnSendError(null, new ExceptionEventArgs(ex));
                }

                /*WOSIService.ErrorHospitalService svc = new WOSIService.ErrorHospitalService();
                svc.Url = Properties.Settings.Default.ErrorHospitalServiceURL;
                svc.Timeout = 3000;
                svc.UseDefaultCredentials = true;*/
                ErrorPacket ePack = GetErrorPacket(ex, licenseKey, licenseName, version );
                

                if (showDialog)
                {
                    Dialogs.ErrorCaptureDialog dlg = new Dialogs.ErrorCaptureDialog(ePack);
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                    {
                        //svc.HandleWOSIException(BuildProxy(ePack));
                    }
                }
                else
                {
                    //svc.HandleWOSIException(BuildProxy(ePack));
                }
            }
            catch
            {
            }
        }

        /*private static WOSIService.WOSIException_Proxy BuildProxy(ErrorPacket ePack)
        {
            WOSIService.WOSIException_Proxy proxy = new WOSIService.WOSIException_Proxy();
            proxy.ApplicationName = ePack.ApplicationName;
            proxy.ClientIpAddress = ePack.ClientIpAddress;
            proxy.ExceptionMessage = ePack.ExceptionMessage;
            proxy.ExceptionType = ePack.ExceptionType;
            proxy.LicenseKey = ePack.LicenseKey;
            proxy.LicenseName = ePack.LicenseName;
            proxy.OSVersion = ePack.OSVersion;
            proxy.StackTrace = ePack.StackTrace;
            proxy.Timestamp = ePack.Timestamp;
            proxy.CurrentCulture = ePack.Culture;
            proxy.Version = ePack.Version;

            return proxy;
        }*/

        private static ErrorPacket GetErrorPacket(Exception ex, string licenseKey, string licenseName, string version)
        {
            ErrorPacket ePack = new ErrorPacket();

            try
            {
                ePack.ApplicationName = System.Windows.Forms.Application.ProductName;
            }
            catch
            {
            }

            ePack.Culture = System.Globalization.CultureInfo.CurrentCulture.EnglishName;
            ePack.ClientIpAddress = NetworkUtils.GetCurrentIpAddress();
            ePack.ExceptionMessage = ex.Message;
            ePack.StackTrace = ex.StackTrace;
            if (ex.InnerException != null)
            {
                ePack.StackTrace += "\r\n*****Inner Exception****\r\n";
                ePack.StackTrace += ex.InnerException.ToString();
            }
            ePack.ExceptionType = ex.GetType().ToString();
            ePack.LicenseKey = licenseKey;
            ePack.LicenseName = licenseName;
            ePack.OSVersion = System.Environment.OSVersion.ToString();
            ePack.Timestamp = System.DateTime.Now;
            ePack.Version = version;
                
            return ePack;
        }
    }

    public struct ErrorPacket
    {
        [ShowOnErrorDialog("Time")]
        public DateTime Timestamp;
        [ShowOnErrorDialog("Application")]
        public string ApplicationName;
        [ShowOnErrorDialog("Error")]
        public string ExceptionMessage;
        [ShowOnErrorDialog("IP Address")]
        public string ClientIpAddress;
        [ShowOnErrorDialog("License Name")]
        public string LicenseName;
        [ShowOnErrorDialog("License Key")]
        public string LicenseKey;
        public string Culture;
        [ShowOnErrorDialog("OS Version")]
        public string OSVersion;
        public string ExceptionType;
        [ShowOnErrorDialog("Stack Trace")]
        public string StackTrace;
        public string Version;
    }

    public class ShowOnErrorDialog : System.Attribute
    {
        private string _friendlyName;
        
        public ShowOnErrorDialog(string friendlyName)
        {
            FriendlyName = friendlyName;
        }

        public string FriendlyName
        {
            get
            {
                return _friendlyName;
            }
            private set
            {
                _friendlyName = value;
            }
        }
    }

 


}
