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
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;

namespace NET.Remoting.TwoWay
{
    public abstract class TwoWayRemotingClientBase : MarshalByRefObject, ITwoWayRemotingClient, IDisposable
    {
        public event EventHandler<ConnectionRequestResultEventArgs> ConnectionAccepted;
        public event EventHandler<ConnectionRequestResultEventArgs> ConnectionDenied;
        public event EventHandler<ConnectionRequestResultEventArgs> Disconnected;

        private object clientObject;
        private bool connected = false;
        private string sessionKey;
        private string username;
        private string password;
        private string responseChannelName;

        protected ITwoWayRemotingServer serverInterface;

        private System.Threading.Timer keepAliveTimer;
        private int keepAliveInterval;

        public TwoWayRemotingClientBase(int keepAliveIntervalSeconds)
        {
            keepAliveInterval = keepAliveIntervalSeconds * 1000;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public bool Connected
        {
            get
            {
                return connected;
            }
        }

        public bool Ping()
        {
            return true;
        }

        protected void InitializeConnect(object clientObject, string responseChannelName, bool async)
        {
            InitializeConnect(clientObject, responseChannelName, null, null, async);
        }

        protected void InitializeConnect(object clientObject, string responseChannelName, string username, string password, bool async)
        {
            // Create a new session key
            sessionKey = Guid.NewGuid().ToString();

            this.clientObject = clientObject;
            this.username = username;
            this.password = password;
            this.responseChannelName = responseChannelName;

            if (async)
            {
                System.Threading.Thread connectThread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(InitializeConnectProc));
                connectThread.IsBackground = true;
                connectThread.Start(this);
            }
            else
            {
                serverInterface.Connect(this, responseChannelName, sessionKey, username, password);
            }

        }

        private static void KeepAliveTimerProc(object state)
        {
            TwoWayRemotingClientBase client = (TwoWayRemotingClientBase)state;

            // Ping our server
            client.serverInterface.Ping();
        }

        private static void InitializeConnectProc(object state)
        {
            TwoWayRemotingClientBase rClient = (TwoWayRemotingClientBase)state;

            try
            {
                rClient.serverInterface.Connect(rClient, rClient.responseChannelName, rClient.sessionKey, rClient.username, rClient.password);
            }
            catch(Exception e)
            {
                WOSI.Utilities.EventUtils.FireAsyncEvent(rClient.ConnectionDenied, rClient, new ConnectionRequestResultEventArgs(false, "The server is currently unavailable or does not exist."));
            }
        }

        public object CallServerMethod(string methodName, params object[] methodArgs)
        {
            //if (!Connected)
            //    throw new Exception("Client is not connected to a server.");

            if (serverInterface != null)
            {
                try
                {
                    return serverInterface.CallServerMethod(this, methodName, methodArgs);
                }
                catch(Exception e)
                {
                    if (e.InnerException != null)
                        OnServerMethodError(e.InnerException);
                    else
                        OnServerMethodError(e);
                }
            }
           
            return null;
        }

        public void Disconnect()
        {
            if (Connected)
            {
                // Kill our keep alive timer
                if (keepAliveTimer != null)
                    keepAliveTimer.Dispose();

                try
                {
                    serverInterface.Disconnect(this, sessionKey);
                }
                catch
                {
                }

                OnDisconnected(null);
            }
        }

        public object CallClientMethod(string sessionKey, string methodName, params object[] methodArgs)
        {
            // Check to make sure our session key is okay
            if(this.sessionKey != sessionKey)
                throw new System.Net.Sockets.SocketException(10061);

            if (clientObject != null)
            {
                System.Reflection.MethodInfo method = clientObject.GetType().GetMethod(methodName);

                if (method == null)
                    throw new NotSupportedException("The specified method does not exist on this object");
                
                return method.Invoke(clientObject, methodArgs);
            }
            
            return null;
        }

        protected virtual void OnServerMethodError(Exception e)
        {
        }

        public void OnConnectionAccepted(ConnectionRequestResultEventArgs e)
        {
            connected = true;

            // Start our keep alive timer
            if (keepAliveInterval > 0)
                keepAliveTimer = new System.Threading.Timer(new System.Threading.TimerCallback(KeepAliveTimerProc), this, keepAliveInterval, keepAliveInterval);

            WOSI.Utilities.EventUtils.FireAsyncEvent(ConnectionAccepted, this, e);
        }

        public void OnConnectionDenied(ConnectionRequestResultEventArgs e)
        {
            connected = false;

            WOSI.Utilities.EventUtils.FireAsyncEvent(ConnectionDenied, this, e);
        }

        public void OnDisconnected(string reason)
        {
            sessionKey = null;
            connected = false;

            InternalOnDisconnected();

            WOSI.Utilities.EventUtils.FireAsyncEvent(Disconnected, this, new ConnectionRequestResultEventArgs(false, reason));
        }

        protected virtual void InternalOnDisconnected()
        {
        }

        #region IDisposable Members

        public void Dispose()
        {
            connected = false;
            InternalOnDisconnected();
        }

        #endregion
    }
}
