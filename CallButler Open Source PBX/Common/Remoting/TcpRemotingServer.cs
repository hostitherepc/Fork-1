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
using System.Collections.Specialized;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using NET.Remoting.Channels;

namespace NET.Remoting
{
    public class TcpRemotingServer
    {
        private object remotingObject;
        private TcpServerChannel tcpChannel;
        public event EventHandler<AuthenticationEventArgs> OnAuthentication;
        public event EventHandler<ManagementAllowedEventArgs> OnManagementAllowed;


        public TcpRemotingServer(string channelName, string serviceUri, int port, object remotingObject)
        {
            this.remotingObject = remotingObject;
            SecureServerChannelSinkProvider sSink = GetSecureServerSink();

            tcpChannel = new TcpServerChannel(channelName, port, sSink);

            RemotingConfiguration.CustomErrorsMode = CustomErrorsModes.Off;

            RemotingConfiguration.RegisterWellKnownServiceType(remotingObject.GetType(), serviceUri, WellKnownObjectMode.Singleton);
        }

        public TcpRemotingServer(string channelName, string serviceUri, int port, MarshalByRefObject remotingObject)
        {
            
            this.remotingObject = remotingObject;

            SecureServerChannelSinkProvider sSink = GetSecureServerSink();
            tcpChannel = new TcpServerChannel(channelName, port, sSink);

            if (RemotingConfiguration.CustomErrorsMode != CustomErrorsModes.Off)
            {
                RemotingConfiguration.CustomErrorsMode = CustomErrorsModes.Off;
            }

            RemotingServices.Marshal(remotingObject, serviceUri);
        }

        private SecureServerChannelSinkProvider GetSecureServerSink()
        {
            IDictionary sProps = new Hashtable();
            sProps["algorithm"] = "3DES";
            sProps["requireSecurity"] = "true";
            sProps["connectionAgeLimit"] = "120";
            sProps["sweepFrequency"] = "60";
            

            SecureServerChannelSinkProvider sSink = new SecureServerChannelSinkProvider(sProps, null);
            sSink.OnAuthentication += new EventHandler<AuthenticationEventArgs>(sSink_OnAuthentication);
            sSink.OnManagementAllowed += new EventHandler<ManagementAllowedEventArgs>(sSink_OnManagementAllowed);

            BinaryServerFormatterSinkProvider binaryFormatter = new BinaryServerFormatterSinkProvider();
            binaryFormatter.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            sSink.Next = binaryFormatter;

            return sSink;
        }

        void sSink_OnManagementAllowed(object sender, ManagementAllowedEventArgs e)
        {
            if (OnManagementAllowed != null)
            {
                OnManagementAllowed(sender, e);
            }
        }

        void sSink_OnAuthentication(object sender, AuthenticationEventArgs e)
        {
            if (OnAuthentication != null)
            {
                OnAuthentication(sender, e);
            }
        }

        public void StartServer()
        {
            ChannelServices.RegisterChannel(tcpChannel, false);
        }

        public void StopServer()
        {
            try
            {
                ChannelServices.UnregisterChannel(tcpChannel);
            }
            catch
            {
            }
        }
    }
}
