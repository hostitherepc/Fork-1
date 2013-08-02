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
using System.Runtime.Remoting.Channels.Ipc;
using NET.Remoting.Channels;

namespace NET.Remoting
{
    public class IpcRemotingClient<ObjectType>
    {
        private IpcClientChannel ipcClient = null;
        private IDictionary tcpClientProps = null;
        private ObjectType remotingObject = default(ObjectType);
        private string serviceUri = "";
        private int port = 0;

        IClientChannelSinkProvider sink;
        BinaryClientFormatterSinkProvider formatter;

        IDictionary sProps;

        public IpcRemotingClient(string channelName, string serviceUri, int port, int customerID, int extensionNumber, string password)
        {
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
            //tcpClientProps["name"] = channelName;
            tcpClientProps["secure"] = false;
            tcpClientProps["exclusiveAddressUse"] = false;
            ipcClient = new IpcClientChannel(tcpClientProps, formatter);
        }

        public void Connect()
        {
            if (ChannelServices.GetChannel(ipcClient.ChannelName) == null)
            {
                ChannelServices.RegisterChannel(ipcClient, false);
            }

            ActivateObjects();
        }

        public void Disconnect()
        {
            DeactivateObjects();

            if (ipcClient != null)
            {
                ChannelServices.UnregisterChannel(ipcClient);
                ipcClient = null;
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
            remotingObject = (ObjectType)Activator.GetObject(typeof(ObjectType), "ipc://" + port + "/" + serviceUri);
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