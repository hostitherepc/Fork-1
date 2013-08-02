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
using WOSI.NET.inTELIPhone;
using WOSI.NET.SIP;
using WOSI.NET.SIP.Messages;
//using T2.Kinesis.Gidgets;

namespace CallButler.Service.Services
{
    public enum PBXPresenceStatus
    {
        Offline,
        Online
    }

    public class PBXPresenceInfo
    {
        public int ExtensionNumber;
        public string Name;
        public string AddressOfRecord;
        public string RemoteAddress;
        public int RemotePort;
        public int ExpiresInSeconds;
        public DateTime LastRegistration;
        public PBXPresenceStatus Status = PBXPresenceStatus.Offline;
        public string SessionID;
        public int CSeq;
    }

    internal class PBXRegistrarService
    {
        private CallButler.Telecom.TelecomProviderBase telecomProvider;
        private WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider;
        //private ExtensionStateService extStateService;

        private System.Threading.Timer presenceTimeout;

        private WOSI.NET.inTELIPhone.inTELIPhoneClient ipClient;

        private List<PBXPresenceInfo> presenceData;
        private readonly object presenceDataLock = new object();

        public PBXRegistrarService(CallButler.Telecom.TelecomProviderBase telecomProvider, WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider/*, ExtensionStateService extStateService*/)
        {
            this.telecomProvider = telecomProvider;
            this.dataProvider = dataProvider;
            //this.extStateService = extStateService;

            presenceData = new List<PBXPresenceInfo>();

            if (telecomProvider is CallButler.Telecom.inTELIPhoneTelecomProvider)
            {
                ipClient = ((CallButler.Telecom.inTELIPhoneTelecomProvider)telecomProvider).BaseProviderObject;

                ipClient.PreprocessSipRequest += new EventHandler<WOSI.NET.SIP.PreprocessSipRequestEventArgs>(ipClient_PreprocessSipRequest);
            }

            presenceTimeout = new System.Threading.Timer(new System.Threading.TimerCallback(PresenceTimeoutTimerProc), this, 0, 30000);
        }

        private static void PresenceTimeoutTimerProc(object state)
        {
            PBXRegistrarService regService = (PBXRegistrarService)state;

            lock (regService.presenceDataLock)
            {
                List<PBXPresenceInfo> removals = new List<PBXPresenceInfo>();

                foreach (PBXPresenceInfo presenceInfo in regService.presenceData)
                {
                    TimeSpan delta = (TimeSpan)(DateTime.Now - presenceInfo.LastRegistration);

                    if (presenceInfo.ExpiresInSeconds < delta.TotalSeconds)
                    {
                        presenceInfo.Status = PBXPresenceStatus.Offline;
                        removals.Add(presenceInfo);
                    }
                }

                foreach (PBXPresenceInfo presenceInfo in removals)
                {
                    presenceInfo.Status = PBXPresenceStatus.Offline;
                    regService.presenceData.Remove(presenceInfo);
                    regService.UpdateExtensionState(presenceInfo);
                }

                PerformanceCounterService.PhonesRegistered = regService.presenceData.Count;
            }
        }

        void ipClient_PreprocessSipRequest(object sender, WOSI.NET.SIP.PreprocessSipRequestEventArgs e)
        {
            switch (e.Request.SIPMethodType)
            {
                case SIPMethodType.REGISTER:
                    {
                        ProcessRegister(e);
                        break;
                    }
                case SIPMethodType.INVITE:
                    {
                        ProcessExtensionAuthentication(e);
                        break;
                    }
                case SIPMethodType.REFER:
                    {
                        ProcessExtensionAuthentication(e);
                        break;
                    }
                default:
                    {
                        e.Handled = false;
                        break;
                    }
            }
        }

        public void ProcessExtensionAuthentication(PreprocessSipRequestEventArgs e)
        {
            try
            {
                SIPURI fromURI = new SIPURI(e.Request.HeaderFields["From", "f"].FieldValue);

                // Is this coming from an internal extension
                WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = dataProvider.GetExtensionNumber(Properties.Settings.Default.CustomerID, Convert.ToInt32(fromURI.User));

                if (extension != null)
                {
                    if (ProcessRequestAuthorization(e, fromURI, false))
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
            }
            catch
            {
            }
        }

        public bool PresenceInfoExists(int extensionNumber, string remoteIpAddress, int remotePort, bool matchPort)
        {
            if (FindPresenceInfo(extensionNumber, remoteIpAddress, remotePort, matchPort) == null)
                return false;
            else
                return true;
        }

        public PBXPresenceInfo[] PresenceInfoData
        {
            get
            {
                return presenceData.ToArray();
            }
        }

        public PBXPresenceInfo FindPresenceInfo(int extensionNumber, string remoteIpAddress, int remotePort, bool matchPort)
        {
            lock (presenceDataLock)
            {
                foreach (PBXPresenceInfo presenceInfo in presenceData)
                {
                    if (presenceInfo.ExtensionNumber == extensionNumber && presenceInfo.RemoteAddress == remoteIpAddress && (!matchPort || (matchPort && presenceInfo.RemotePort == remotePort)))
                        return presenceInfo;
                }
            }

            return null;
        }

        private void UpdateExtensionState(PBXPresenceInfo presenceInfo)
        {
            /*if (extStateService != null)
            {
                extStateService.UpdateExtensionState(true, presenceInfo.ExtensionNumber, StateEventType.PhoneStateUpdate,
                    new ExtensionStateParameter(StateParameterType.PhoneStatus, (int)presenceInfo.Status),
                    new ExtensionStateParameter(StateParameterType.PhonePort, presenceInfo.RemotePort),
                    new ExtensionStateParameter(StateParameterType.PhoneIPAddress, presenceInfo.RemoteAddress),
                    new ExtensionStateParameter(StateParameterType.PhoneDisplayName, presenceInfo.Name));
            }*/
        }

        private void RemovePresenceInfo(PBXPresenceInfo presenceInfo)
        {
            lock (presenceDataLock)
            {
                presenceInfo.Status = PBXPresenceStatus.Offline;
                presenceData.Remove(presenceInfo);
                PerformanceCounterService.PhonesRegistered = presenceData.Count;
            }

            UpdateExtensionState(presenceInfo);
        }

        void ProcessRegister(PreprocessSipRequestEventArgs e)
        {
            e.Handled = true;
            bool isNewRegistration = false;

            // Remove the Record-Route field as per RFC-3261 10.3
            if (e.Request.HeaderFields.Contains("Record-Route", null))
                e.Request.HeaderFields.Remove("Record-Route");

            try
            {
                SIPURI addressOfRecord = new SIPURI(e.Request.HeaderFields["To", "t"].FieldValue);

                // Make sure the request is authorized
                if (ProcessRequestAuthorization(e, addressOfRecord, false))
                {
                    int extensionNumber = 0;

                    try
                    {
                        extensionNumber = Convert.ToInt32(addressOfRecord.User);
                    }
                    catch
                    {
                        ipClient.SendSipResponse(e.Request.CreateResponse(404, "Not Found"));
                        return;
                    }

                    PBXPresenceInfo presenceInfo = FindPresenceInfo(extensionNumber, e.Request.SentFrom, e.Request.SentFromPort, true);

                    if (e.Request.HeaderFields.Contains("Contact", "m"))
                    {
                        HeaderField[] contactFields = e.Request.HeaderFields.GetMultiHeaderFields("Contact", "m");
                        
                        bool containsStar = false;

                        foreach (HeaderField contactField in contactFields)
                        {
                            if (contactField.FieldValue == "*")
                            {
                                containsStar = true;
                            }
                        }

                        if (containsStar)
                        {
                            if (contactFields.Length > 1 || !e.Request.HeaderFields.Contains("Expires", null) || e.Request.HeaderFields["Expires", null].FieldValue.Trim() != "0")
                            {
                                ipClient.SendSipResponse(e.Request.CreateResponse(400, "Invalid Request"));
                                return;
                            }
                        }

                        int requestedExpiration = 0;

                        if (contactFields[0].ContainsParameter("Expires"))
                            requestedExpiration = Convert.ToInt32(contactFields[0].Parameters["Expires"]);
                        else if (e.Request.HeaderFields.Contains("Expires", null))
                            requestedExpiration = Convert.ToInt32(e.Request.HeaderFields["Expires", null].FieldValue);
                        else
                            requestedExpiration = Properties.Settings.Default.PresenceTimeout;

                        if (presenceInfo != null)
                        {
                            // Check the Call-ID
                            string callID = e.Request.HeaderFields["Call-ID", "i"].FieldValue;

                            if (callID == presenceInfo.SessionID)
                            {
                                // Parse our CSeq
                                int cSeq = Convert.ToInt32(e.Request.HeaderFields["CSeq", null].FieldValue.Split(' ')[0]);

                                if (cSeq < presenceInfo.CSeq)
                                {
                                    RemovePresenceInfo(presenceInfo);

                                    ipClient.SendSipResponse(e.Request.CreateResponse(400, "Invalid Request"));

                                    return;
                                }
                            }

                            if (requestedExpiration == 0)
                            {
                                RemovePresenceInfo(presenceInfo);
                                ipClient.SendSipResponse(e.Request.CreateResponse(200, "OK"));
                                return;
                            }
                        }
                        else
                        {
                            presenceInfo = new PBXPresenceInfo();
                            isNewRegistration = true;
                        }

                        presenceInfo.AddressOfRecord = new SIPURI(e.Request.HeaderFields["To", "t"].FieldValue).BasicURIStringWithoutParameters;
                        presenceInfo.CSeq = Convert.ToInt32(e.Request.HeaderFields["CSeq", null].FieldValue.Split(' ')[0]);
                        presenceInfo.ExpiresInSeconds = requestedExpiration;
                        presenceInfo.ExtensionNumber = extensionNumber;
                        presenceInfo.LastRegistration = DateTime.Now;
                        presenceInfo.Name = addressOfRecord.DisplayName;
                        presenceInfo.RemoteAddress = e.Request.SentFrom;
                        presenceInfo.RemotePort = e.Request.SentFromPort;
                        presenceInfo.SessionID = e.Request.HeaderFields["Call-ID", "i"].FieldValue;
                        presenceInfo.Status = PBXPresenceStatus.Online;

                        if (isNewRegistration)
                        {
                            lock (presenceDataLock)
                            {
                                presenceData.Add(presenceInfo);
                                LoggingService.AddLogEntry(WOSI.CallButler.ManagementInterface.LogLevel.Extended, "Extension " + presenceInfo.ExtensionNumber + " registered for " + presenceInfo.RemoteAddress, false);

                                UpdateExtensionState(presenceInfo);
                            }
                        }
                    }

                    if (presenceInfo != null)
                    {
                        SipResponse response = e.Request.CreateResponse(200, "OK");

                        response.HeaderFields.InsertAfter("CSeq", e.Request.HeaderFields["Contact", "m"]);
                        response.HeaderFields["Contact", "m"].Parameters["expires"] = presenceInfo.ExpiresInSeconds.ToString();
                        response.HeaderFields.InsertAfter("Contact", "Date", DateTime.Now.ToUniversalTime().ToString("ddd, d MMM yyyy HH:mm:ss G\\MT"));

                        ipClient.SendSipResponse(response);

                        // Send our message waiting notification
                        if(isNewRegistration)
                            SendMessageWaitingNotification(extensionNumber);

                        PerformanceCounterService.PhonesRegistered = presenceData.Count;
                    }
                    else
                    {
                        ipClient.SendSipResponse(e.Request.CreateResponse(404, "Not Found"));
                    }

                    return;
                }
            }
            catch (Exception ex)
            {
                ipClient.SendSipResponse(e.Request.CreateResponse(500, "Server Error"));
                LoggingService.AddLogEntry(WOSI.CallButler.ManagementInterface.LogLevel.ErrorsOnly, Utils.ErrorUtils.FormatErrorString(ex), true);
            }
        }

        private bool ProcessRequestAuthorization(PreprocessSipRequestEventArgs e, SIPURI fromURI, bool requireRegistration)
        {
            try
            {
                switch (e.Request.SIPMethodType)
                {
                    case SIPMethodType.INVITE:
                    case SIPMethodType.REGISTER:

                        //if (Properties.Settings.Default.SIPRegistrarDomain == null || Properties.Settings.Default.SIPRegistrarDomain.Length == 0 || fromURI.Host == ipClient.LocalIPAddress /*|| fromURI.Host == ipClient.Sip*/ || string.Compare(fromURI.Host, Properties.Settings.Default.SIPRegistrarDomain, true) == 0)
                        //{
                            int extensionNumber = Convert.ToInt32(fromURI.User);

                            // Check to see if this is a valid extension
                            WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = dataProvider.GetExtensionNumber(Properties.Settings.Default.CustomerID, extensionNumber);

                            if (extension == null)
                            {
                                ipClient.SendSipResponse(e.Request.CreateResponse(404, "Not Found"));
                                return false;
                            }

                            // Get our PBX password from our database
                            string pbxPassword = "";

                            if (extension.PBXPassword.Length > 0)
                                pbxPassword = WOSI.Utilities.CryptoUtils.Decrypt(extension.PBXPassword, WOSI.CallButler.Data.Constants.EncryptionPassword);

                            if (requireRegistration && !PresenceInfoExists(extensionNumber, e.Request.SentFrom, e.Request.SentFromPort, true))
                            {
                                // If we require registration and the SIP client isn't registered, return a 404 not found
                                ipClient.SendSipResponse(e.Request.CreateResponse(404, "Not Found"));

                                return false;
                            }
                            else if (IsRequestAuthorized(e.Request, extensionNumber.ToString(), pbxPassword))
                            {
                                return true;
                            }
                            else
                            {
                                string realmDomain = Properties.Settings.Default.SIPRegistrarDomain;

                                if (realmDomain == null || realmDomain.Length == 0)
                                    realmDomain = ipClient.LocalIPAddress;

                                // Tell the client that we need authorization
                                string authString = string.Format("Digest realm=\"{0}\", domain=\"sip:{1}\", nonce=\"{2}\", algorithm=MD5", realmDomain, realmDomain, Guid.NewGuid().ToString());

                                SipResponse response = e.Request.CreateResponse(401, "Unauthorized");
                                response.HeaderFields.InsertAfter("CSeq", "WWW-Authenticate", authString);
                                ipClient.SendSipResponse(response);

                                return false;
                            }
                        /*}
                        else
                        {
                            ipClient.SendSipResponse(e.Request.CreateResponse(404, "Not Found"));
                            return false;
                        }*/

                        break;

                    default:
                        return true;
                }
            }
            catch
            {
            }

            return false;
        }

        public static bool IsRequestAuthorized(WOSI.NET.SIP.Messages.SipRequest request, string username, string password)
        {
            if (request.HeaderFields.Contains("Authorization", null))
            {
                string fieldValue = request.HeaderFields["Authorization", null].FieldValue.Trim();

                if (fieldValue.StartsWith("digest ", StringComparison.InvariantCultureIgnoreCase))
                    fieldValue = fieldValue.Remove(0, 7);

                // Split out our auth params
                string[] authParams = WOSI.Utilities.StringUtils.SplitQuotedString(fieldValue, ',');

                string realm = "";
                string uri = "";
                string nonce = "";
                string response = "";
                string parsedUsername = "";

                foreach (string authParam in authParams)
                {
                    string[] paramValues = WOSI.Utilities.StringUtils.SplitQuotedString(authParam, '=');

                    if (paramValues.Length > 0)
                    {
                        paramValues[0] = paramValues[0].Trim();
                        paramValues[1] = paramValues[1].Trim();

                        if (string.Compare(paramValues[0], "realm", true) == 0)
                        {
                            realm = paramValues[1].Trim('"');
                        }
                        else if (string.Compare(paramValues[0], "uri", true) == 0)
                        {
                            uri = paramValues[1].Trim('"');
                        }
                        else if (string.Compare(paramValues[0], "nonce", true) == 0)
                        {
                            nonce = paramValues[1].Trim('"');
                        }
                        else if (string.Compare(paramValues[0], "response", true) == 0)
                        {
                            response = paramValues[1].Trim('"');
                        }
                        else if (paramValues[0].ToLower().Contains("username"))
                        {
                            parsedUsername = paramValues[1].Trim('"');
                        }
                    }
                }

                // Make sure our usernames match
                if (string.Compare(parsedUsername, username) != 0)
                    return false;

                // Create our hashed digest
                string a1 = WOSI.Utilities.CryptoUtils.CreateASCIIMD5Hash(username + ":" + realm + ":" + password);
                string a2 = WOSI.Utilities.CryptoUtils.CreateASCIIMD5Hash(request.Method + ":" + uri);

                string hashStr = WOSI.Utilities.CryptoUtils.CreateASCIIMD5Hash(a1 + ":" + nonce + ":" + a2);

                // Check to see if our hashstr is equal to our response
                if (hashStr == response)
                    return true;
            }

            return false;
        }

        public WOSI.CallButler.Data.CallButlerPhoneStatusDataset GetPhoneStatus()
        {
            WOSI.CallButler.Data.CallButlerPhoneStatusDataset psData = new WOSI.CallButler.Data.CallButlerPhoneStatusDataset();

            WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extensions = dataProvider.GetExtensions(Properties.Settings.Default.CustomerID);

            // Loop through each extension and get a status for it
            foreach (WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension in extensions)
            {
                WOSI.CallButler.Data.CallButlerPhoneStatusDataset.PhoneStatusRow statusRow = psData.PhoneStatus.NewPhoneStatusRow();
                PBXPresenceInfo[] presenceInfo = GetPresenceInfoForExtension(extension.ExtensionNumber);

                statusRow.ExtensionID = extension.ExtensionID;
                statusRow.ExtensionNumber = extension.ExtensionNumber;
                statusRow.FirstName = extension.FirstName;
                statusRow.LastName = extension.LastName;
                statusRow.StatusCode = (int)WOSI.CallButler.Data.PhoneExtensionStatus.Offline;

                if (presenceInfo != null && presenceInfo.Length > 0)
                {
                    foreach (PBXPresenceInfo tmpPInfo in presenceInfo)
                    {
                        if (tmpPInfo.Status != PBXPresenceStatus.Offline)
                        {
                            statusRow.StatusCode = (int)WOSI.CallButler.Data.PhoneExtensionStatus.Online;
                            statusRow.RemoteAddress += tmpPInfo.RemoteAddress + " ";

                            break;
                        }
                    }

                    statusRow.RemoteAddress = statusRow.RemoteAddress.Trim();
                }

                psData.PhoneStatus.AddPhoneStatusRow(statusRow);
            }

            return psData;
        }

        public PBXPresenceInfo[] GetPresenceInfoForExtension(int extensionNumber)
        {
            List<PBXPresenceInfo> foundPresenceInfo = new List<PBXPresenceInfo>();
            lock (presenceDataLock)
            {
                foreach (PBXPresenceInfo presenceInfo in presenceData)
                {
                    if (presenceInfo.ExtensionNumber == extensionNumber)
                    {
                        foundPresenceInfo.Add(presenceInfo);
                    }
                }
            }

            return foundPresenceInfo.ToArray();
        }

        public void SendMessageWaitingNotification(Guid extensionID)
        {
            WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = dataProvider.GetExtension(Properties.Settings.Default.CustomerID, extensionID);

            if (extension != null)
                SendMessageWaitingNotification(extension);
        }

        public void SendMessageWaitingNotification(int extensionNumber)
        {
            WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension = dataProvider.GetExtensionNumber(Properties.Settings.Default.CustomerID, extensionNumber);

            if (extension != null)
                SendMessageWaitingNotification(extension);
        }

        private void SendMessageWaitingNotification(WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extension)
        {
            PBXPresenceInfo[] presenceInfos = GetPresenceInfoForExtension(extension.ExtensionNumber);

            if (presenceInfos != null)
            {
                foreach (PBXPresenceInfo presenceInfo in presenceInfos)
                {
                    if (presenceInfo.Status == PBXPresenceStatus.Online)
                    {
                        // Create our notify message
                        SipRequest request = new SipRequest(SIPMethodType.NOTIFY);

                        SIPURI requestURI = new SIPURI(presenceInfo.AddressOfRecord);

                        request.BranchID = "z9hG4bK" + Guid.NewGuid().ToString();
                        request.RequestURI = new SIPURI(presenceInfo.AddressOfRecord);
                        request.HeaderFields["To", "t"].FieldValue = requestURI.ExtendedURIStringWithParameters;
                        request.HeaderFields["From", "f"].FieldValue = requestURI.ExtendedURIStringWithParameters;
                        request.HeaderFields.Add("Event", "message-summary");
                        request.HeaderFields.Add("Content-Type", "application/simple-message-summary");

                        StringBuilder sb = new StringBuilder();

                        int newVoicemailCount = dataProvider.GetNewVoicemailCount(extension.ExtensionID);
                        int totalVoicemailCount = dataProvider.GetVoicemails(extension.ExtensionID).Count;

                        string vmStatus = "no";

                        if (newVoicemailCount > 0)
                            vmStatus = "yes";

                        sb.AppendFormat("Messages-Waiting: {0}\r\n", vmStatus);
                        sb.AppendFormat("Voice-Message: {0}/{1}", newVoicemailCount, totalVoicemailCount);

                        request.MessageBody = sb.ToString();

                        ipClient.SendSipRequest(request, presenceInfo.RemoteAddress, presenceInfo.RemotePort);
                    }
                }
            }
        }

        public void Shutdown()
        {
            presenceTimeout.Dispose();
            presenceTimeout = null;

            presenceData.Clear();

            if (ipClient != null)
            {
                ipClient.PreprocessSipRequest -= ipClient_PreprocessSipRequest;
            }
        }
    }
}
