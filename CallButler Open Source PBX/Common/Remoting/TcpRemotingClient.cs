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
    public class TcpRemotingClient<ObjectType>
    {
        private TcpClientChannel tcpClient = null;
        private IDictionary tcpClientProps = null;
        private ObjectType remotingObject = default(ObjectType);
        private string server = "";
        private string serviceUri = "";
        private int port = 0;

        IClientChannelSinkProvider sink;
        BinaryClientFormatterSinkProvider formatter;

        IDictionary sProps;

        public TcpRemotingClient(string channelName, string server, string serviceUri, int port, int customerID, int extensionNumber, string password)
        {
            this.server = server;
            this.serviceUri = serviceUri;
            this.port = port;

            sProps = new Hashtable();
            sProps["customerid"] = customerID;
            sProps["extensionnumber"] = extensionNumber;
            sProps["password"] = password;

            sink = new SecureClientChannelSinkProvider(sProps, null);

            formatter = new BinaryClientFormatterSinkProvider();

            formatter.Next = sink;

            tcpClientProps = new Hashtable();
            tcpClientProps["name"] = channelName;
            tcpClientProps["secure"] = false;

            tcpClient = new TcpClientChannel(tcpClientProps, formatter);
        }

        public void Connect()
        {
            if (ChannelServices.GetChannel(tcpClient.ChannelName) == null)
            {
                ChannelServices.RegisterChannel(tcpClient, false);
            }

            ActivateObjects();
        }

        public void Disconnect()
        {
            DeactivateObjects();

            if (tcpClient != null)
            {
                ChannelServices.UnregisterChannel(tcpClient);
                tcpClient = null;
            }

            formatter = null;
            sink = null;

            GC.Collect();
        }

        public ObjectType RemotingObject
        {
            get
            {
                return remotingObject;
            }
        }

        private void ActivateObjects()
        {
            DeactivateObjects();
            remotingObject = (ObjectType)Activator.GetObject(typeof(ObjectType), "tcp://" + server + ":" + port + "/" + serviceUri);
            IDictionary di = ChannelServices.GetChannelSinkProperties(remotingObject);

            di["customerid"] = sProps["customerid"];
            di["extensionnumber"] = sProps["extensionnumber"];
            di["password"] = sProps["password"];

        }

        private void DeactivateObjects()
        {
            remotingObject = default(ObjectType);

            GC.Collect();
        }
    }
}
