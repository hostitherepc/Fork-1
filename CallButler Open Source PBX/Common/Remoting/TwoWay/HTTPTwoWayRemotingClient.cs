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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using NET.Remoting.ChannelSinks;

namespace NET.Remoting.TwoWay
{
    public class HTTPTwoWayRemotingClient : TwoWayRemotingClientBase
    {
        private ObjRef objRef;
        //private System.Runtime.Remoting.Channels.Http.HttpChannel channel;
        private System.Runtime.Remoting.Channels.Tcp.TcpChannel channel;
        //private Belikov.GenuineChannels.GenuineTcp.GenuineTcpChannel channel;

        public HTTPTwoWayRemotingClient(int listenPort, int keepAliveIntervalSeconds) : base(keepAliveIntervalSeconds)
        {
            /*IDictionary sProps = new Hashtable();
            sProps["algorithm"] = "3DES";
            sProps["requireSecurity"] = "true";
            sProps["connectionAgeLimit"] = "120";
            sProps["sweepFrequency"] = "60";*/

            BinaryServerFormatterSinkProvider binaryServerFormatSinkProvider = new BinaryServerFormatterSinkProvider();
            binaryServerFormatSinkProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

            //SecureServerChannelSinkProvider secureServerSinkProvider = new SecureServerChannelSinkProvider(sProps, null);
            //secureServerSinkProvider.Next = binaryServerFormatSinkProvider;

            BinaryClientFormatterSinkProvider binaryClientFormatSinkProvider = new BinaryClientFormatterSinkProvider();
            //binaryClientFormatSinkProvider.Next = new SecureClientChannelSinkProvider(sProps, null);

            IDictionary props = new Hashtable();
            props["port"] = listenPort;
            //props["useIpAddress"] = false;
            //props["clientConnectionLimit"] = 1;
            //props["rejectRemoteRequests"] = false;
            //props["ConnectTimeout"] = 15000;
            //props["InvocationTimeout"] = 15000;
            //props["Priority"] = "100";

            //Belikov.GenuineChannels.Security.SecuritySessionServices.SetCurrentSecurityContext(new Belikov.GenuineChannels.Security.SecuritySessionParameters(Belikov.GenuineChannels.Security.SecuritySessionServices.DefaultContext.Name, Belikov.GenuineChannels.Security.SecuritySessionAttributes.ForceSync, TimeSpan.FromSeconds(15), Belikov.GenuineChannels.Connection.GenuineConnectionType.Persistent, null, TimeSpan.FromMinutes(5)));


            //channel = new System.Runtime.Remoting.Channels.Tcp.TcpChannel(props, binaryClientFormatSinkProvider, secureServerSinkProvider);
            channel = new System.Runtime.Remoting.Channels.Tcp.TcpChannel(props, binaryClientFormatSinkProvider, binaryServerFormatSinkProvider);
            //channel = new System.Runtime.Remoting.Channels.Http.HttpChannel(props, binaryClientFormatSinkProvider, secureServerSinkProvider);
            //channel = new Belikov.GenuineChannels.GenuineTcp.GenuineTcpChannel(props, binaryClientFormatSinkProvider, binaryServerFormatSinkProvider);
            System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(channel);
        }

        public void Connect(object clientObject, string host, string channelName, int port, bool async)
        {
            Connect(clientObject, host, channelName, port, null, null, async);
        }

        public void Connect(object clientObject, string host, string channelName, int port, string username, string password, bool async)
        {
            if (!Connected)
            {
                // Register this client to receive messages
                objRef = System.Runtime.Remoting.RemotingServices.Marshal(this, string.Format("{0}.rem", Guid.NewGuid().ToString()), typeof(ITwoWayRemotingClient));

                // Create a connection to our server
                //serverInterface = (ITwoWayRemotingServer)Activator.GetObject(typeof(ITwoWayRemotingServer), string.Format("http://{0}:{1}/{2}", host, port, channelName));
                serverInterface = (ITwoWayRemotingServer)Activator.GetObject(typeof(ITwoWayRemotingServer), string.Format("tcp://{0}:{1}/{2}", host, port, channelName));
                //serverInterface = (ITwoWayRemotingServer)Activator.GetObject(typeof(ITwoWayRemotingServer), string.Format("gtcp://{0}:{1}/{2}", host, port, channelName));

                this.InitializeConnect(clientObject, channelName, username, password, async);
            }
        }

        protected override void OnServerMethodError(Exception e)
        {
            if (e is System.Net.WebException || e is System.Net.Sockets.SocketException)
            {
                OnDisconnected(e.Message);
            }
            else
                throw e;
        }

        protected override void  InternalOnDisconnected()
        {
            serverInterface = null;

            try
            {
                System.Runtime.Remoting.RemotingServices.Unmarshal(objRef);
                System.Runtime.Remoting.Channels.ChannelServices.UnregisterChannel(channel);
            }
            catch
            {
            }

            GC.Collect();
        }
    }
}
