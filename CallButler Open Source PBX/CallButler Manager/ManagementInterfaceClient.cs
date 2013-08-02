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
using System.Collections.Specialized;

namespace CallButler.Manager
{
    static class ManagementInterfaceClient
    {
        private static NameValueCollection clientSettings;

        private static WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceClientBase managementInterfaceClient;

        static WOSI.CallButler.ManagementInterface.CallButlerAuthInfo authInfo;


        public static WOSI.CallButler.ManagementInterface.CallButlerAuthInfo AuthInfo
        {
            get
            {
                return authInfo;
            }
        }

        static ManagementInterfaceClient()
        {
            clientSettings = new NameValueCollection();

            // Load up our data provider
            switch (Properties.Settings.Default.ManagementInterfaceType)
            {
                case WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceType.Local:

                    clientSettings["ChannelName"] = "CallButler Management Client";
                    clientSettings["Host"] = Properties.Settings.Default.CallButlerServer;
                    clientSettings["ServiceURI"] = "CallButlerManagementServer";
                    clientSettings["Port"] = Properties.Settings.Default.TcpManagementPort.ToString();
                    clientSettings["CustomerID"] = Properties.Settings.Default.CustomerID.ToString();
                    clientSettings["ExtensionNumber"] = "";
                    clientSettings["Password"] = "";

                    //managementInterfaceClient = new WOSI.CallButler.ManagementInterface.TcpRemotingCallButlerManagementInterfaceClient();

                    break;
                case WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceType.Hosted:
                    //managementInterfaceClient = new WOSI.CallButler.ManagementInterface.HostedCallButlerManagementInterfaceClient();
                    
                    break;
            }
        }

        public static void Connect(string server, int port, string password)
        {
            clientSettings["ExtensionNumber"] = "-1";
            clientSettings["Password"] = password;
            clientSettings["Host"] = server;
            clientSettings["Port"] = port.ToString();

            ProcessConnection();

            managementInterfaceClient.Connect(clientSettings);
            authInfo = new WOSI.CallButler.ManagementInterface.CallButlerAuthInfo(Properties.Settings.Default.CustomerID, password);
        }

        public static void Connect(int customerID, string password)
        {
            authInfo = new WOSI.CallButler.ManagementInterface.CallButlerAuthInfo(customerID, password);
            ProcessConnection();
            managementInterfaceClient.Connect(null);
        }

        public static void Disconnect()
        {
            managementInterfaceClient.Disconnect();
        }

        private static void ProcessConnection()
        {
            if (managementInterfaceClient != null)
            {
                managementInterfaceClient.Disconnect();
                managementInterfaceClient = null;
            }

            if (Properties.Settings.Default.ManagementInterfaceType != WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceType.Hosted)
            {
                try
                {
                    if (System.Net.Dns.GetHostEntry(clientSettings["Host"]).HostName == System.Net.Dns.GetHostEntry("127.0.0.1").HostName)
                    {
                        // If we're connecting to the local computer, use IPC, otherwise use TCP
                        //managementInterfaceClient = new WOSI.CallButler.ManagementInterface.IpcRemotingCallButlerManagementInterfaceClient();
                        managementInterfaceClient = new WOSI.CallButler.ManagementInterface.PipeRemotingCallButlerManagementInterfaceClient();
                        return;
                    }
                }
                catch
                {
                }

                managementInterfaceClient = new WOSI.CallButler.ManagementInterface.TcpRemotingCallButlerManagementInterfaceClient();
            }
            else
            {
                //managementInterfaceClient = new WOSI.CallButler.ManagementInterface.HostedCallButlerManagementInterfaceClient();
            }
        }

        public static string CurrentServer
        {
            get
            {
                return clientSettings["Host"].ToString();
            }
        }

        public static WOSI.CallButler.ManagementInterface.CallButlerManagementInterfaceClientBase ManagementClient
        {
            get
            {
                return managementInterfaceClient;
            }
        }

        public static WOSI.CallButler.ManagementInterface.ICallButlerManagementInterface ManagementInterface
        {
            get
            {
                return managementInterfaceClient.ManagementInterface;
            }
        }
    }
}
