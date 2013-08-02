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
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace NET.Remoting.TwoWay
{
    public abstract class TwoWayRemotingServerBase : MarshalByRefObject, ITwoWayRemotingServer, IDisposable
    {
        private delegate void AsyncMethodInvoker(TwoWayRemotingServerBase server, ITwoWayRemotingClient clientObject, string clientUri, string methodName, bool disconnectOnException, params object[] methodArgs);

        public event EventHandler<TwoWayRemotingConnectionEventArgs> ClientRequestingConnection;
        public event EventHandler<TwoWayRemotingConnectionEventArgs> ClientConnected;
        public event EventHandler<TwoWayRemotingConnectionEventArgs> ClientDisconnected;

        private Dictionary<string, TwoWayRemotingClientConnectionInfo> connectedClients;
        private int keepAliveInterval;
        private object serverObject;

        private bool started = false;

        private System.Threading.Timer keepAliveTimer;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public TwoWayRemotingServerBase(int keepAliveIntervalSeconds)
        {
            connectedClients = new Dictionary<string, TwoWayRemotingClientConnectionInfo>();
            keepAliveInterval = keepAliveIntervalSeconds * 1000;
        }

        public TwoWayRemotingClientConnectionInfo[] ConnectedClients
        {
            get
            {
                List<TwoWayRemotingClientConnectionInfo> cc = new List<TwoWayRemotingClientConnectionInfo>();

                lock (connectedClients)
                {
                    foreach (string key in connectedClients.Keys)
                    {
                        cc.Add(connectedClients[key]);
                    }
                }

                return cc.ToArray();
            }
        }

        public void Start(object serverObject)
        {
            this.serverObject = serverObject;

            OnStart();

            // Start our keep alive timer
            if(keepAliveInterval > 0)
                keepAliveTimer = new System.Threading.Timer(new System.Threading.TimerCallback(KeepAliveTimerProc), this, keepAliveInterval, keepAliveInterval);

            started = true;
        }

        public void Stop()
        {
            started = false;

            // Kill our keep alive timer
            if(keepAliveTimer != null)
                keepAliveTimer.Dispose();

            OnStop();
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnStop()
        {
        }

        public bool IsStarted
        {
            get
            {
                return started;
            }
        }

        private static void KeepAliveTimerProc(object state)
        {
            TwoWayRemotingServerBase server = (TwoWayRemotingServerBase)state;

            // Loop through the connected clients and ping them
            lock (server.connectedClients)
            {
                foreach (string key in server.connectedClients.Keys)
                {
                    server.InternalCallClientMethodAsync(server.connectedClients[key].ClientObject, server.connectedClients[key].RemoteUri, "Ping", true);
                }
            }
        }

        public void AcceptConnection(TwoWayRemotingConnectionEventArgs connectionArgs)
        {
            if (connectionArgs != null && connectionArgs.ClientObject != null)
            {
                ConnectionRequestResultEventArgs cr = new ConnectionRequestResultEventArgs(true, null);

                InternalCallClientMethodAsync(connectionArgs.ClientObject, connectionArgs.ClientUri, "OnConnectionAccepted", false, cr);

                TwoWayRemotingClientConnectionInfo ci = new TwoWayRemotingClientConnectionInfo(connectionArgs.ClientObject, connectionArgs.SessionKey);
                lock (connectedClients)
                {
                    connectedClients.Add(ci.RemoteUri, ci);
                }

                if (ClientConnected != null)
                    ClientConnected(this, connectionArgs);
            }
        }

        public void DeclineConnection(TwoWayRemotingConnectionEventArgs connectionArgs, string reason)
        {
            if (connectionArgs != null && connectionArgs.ClientObject != null)
            {
                ConnectionRequestResultEventArgs cr = new ConnectionRequestResultEventArgs(false, reason);

                InternalCallClientMethodAsync(connectionArgs.ClientObject, connectionArgs.ClientUri, "OnConnectionDenied", false, cr);
            }
        }

        public void DisconnectClient(ITwoWayRemotingClient clientObject)
        {
            DisconnectClient(clientObject, null);
        }

        public void DisconnectClient(ITwoWayRemotingClient clientObject, string reason)
        {
            DisconnectClient(clientObject, reason, true);
        }

        public void DisconnectClient(ITwoWayRemotingClient clientObject, string reason, bool notifyClient)
        {
            if (IsClientConnected(clientObject))
            {
                RemoveClient(clientObject);

                if (notifyClient)
                    InternalCallClientMethodAsync(clientObject, GetClientID(clientObject), "OnDisconnected", false, reason);
            }

            if (ClientDisconnected != null)
                ClientDisconnected(this, new TwoWayRemotingConnectionEventArgs(clientObject, null));
        }

        public bool IsClientConnected(ITwoWayRemotingClient clientObject)
        {
            if (connectedClients.ContainsKey(GetClientID(clientObject)))
                return true;
            else
                return false;
        }

        private void RemoveClient(ITwoWayRemotingClient clientObject)
        {
            string clientID = GetClientID(clientObject);

            if (connectedClients.ContainsKey(clientID))
            {
                lock (connectedClients)
                {
                    connectedClients.Remove(clientID);
                }
            }
        }

        public string GetClientID(ITwoWayRemotingClient clientObject)
        {
            return RemotingServices.GetObjectUri((MarshalByRefObject)clientObject);
        }

        private TwoWayRemotingClientConnectionInfo GetConnectionInfo(ITwoWayRemotingClient clientObject)
        {
            string remoteUri = GetClientID(clientObject);

            foreach (TwoWayRemotingClientConnectionInfo ci in this.ConnectedClients)
            {
                if (ci.RemoteUri == remoteUri)
                    return ci;
            }

            return null;
        }

        #region Client Method Functions
        private void InternalCallClientMethodAsync(ITwoWayRemotingClient clientObject, string clientUri, string methodName, bool disconnectOnException, params object[] methodArgs)
        {
            AsyncMethodInvoker invoker = new AsyncMethodInvoker(InvokeClientMethod);
            AsyncCallback cleanUp = new AsyncCallback(AsyncDelegateCleanup);

            IAsyncResult ar = invoker.BeginInvoke(this, clientObject, clientUri, methodName, disconnectOnException, methodArgs, cleanUp, null);
        }

        public void CallClientMethodAsync(ITwoWayRemotingClient clientObject, string methodName, bool disconnectOnException, params object[] methodArgs)
        {
            // Get the session ID for the client object
            TwoWayRemotingClientConnectionInfo ci = GetConnectionInfo(clientObject);

            if (ci != null)
            {
                InternalCallClientMethodAsync(clientObject, ci.RemoteUri, "CallClientMethod", disconnectOnException, ci.SessionKey, methodName, methodArgs);
            }
        }

        public void BroadcastClientMethodAsync(string methodName, bool disconnectOnException, params object[] methodArgs)
        {
            foreach (TwoWayRemotingClientConnectionInfo connectionInfo in this.ConnectedClients)
            {
                CallClientMethodAsync(connectionInfo.ClientObject, methodName, disconnectOnException, methodArgs);
            }
        }

        private static void AsyncDelegateCleanup(IAsyncResult asyncResult)
        {
            asyncResult.AsyncWaitHandle.Close();
        }

        private static void InvokeClientMethod(TwoWayRemotingServerBase server, ITwoWayRemotingClient clientObject, string clientUri, string methodName, bool disconnectOnException, params object[] methodArgs)
        {
            // Create a list of our parameter types
            List<Type> paramTypes = new List<Type>(methodArgs.Length);
            foreach (object param in methodArgs)
            {
                paramTypes.Add(param.GetType());
            }

            System.Reflection.MethodInfo method = clientObject.GetType().GetMethod(methodName, paramTypes.ToArray());

            try
            {
                System.Net.IPAddress address = (System.Net.IPAddress)System.Runtime.Remoting.Messaging.CallContext.GetData("ClientIP");
                System.Diagnostics.Trace.WriteLine("****** Yo: " + server.GetClientID(clientObject));
                method.Invoke(clientObject, methodArgs);
                System.Diagnostics.Trace.WriteLine("*** Done");
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine("*** Remoting Error: " + server.GetClientID(clientObject) + "\r\n\r\n" + e.ToString());
                if (e.InnerException != null)
                {
                    if (disconnectOnException)
                        server.DisconnectClient(clientObject, null, false);
                    else
                        server.OnClientMethodError(clientObject, e.InnerException);
                }
                else
                {
                    if (disconnectOnException)
                        server.DisconnectClient(clientObject, null, false);
                    else
                        server.OnClientMethodError(clientObject, e);
                }
            }
        }

        protected virtual void OnClientMethodError(ITwoWayRemotingClient clientObject, Exception e)
        {
            System.Diagnostics.Trace.WriteLine("*** Remoting Error: " + GetClientID(clientObject) + "\r\n\r\n" + e.ToString());
        }
        #endregion

        #region IRemotingServer Members

        public void Connect(ITwoWayRemotingClient remotingClient, string responseChannelName, string sessionKey)
        {
            Connect(remotingClient, responseChannelName, sessionKey, null, null);
        }

        public void Connect(ITwoWayRemotingClient remotingClient, string responseChannelName, string sessionKey, string username, string password)
        {
            System.Diagnostics.Trace.WriteLine("************** Connecting!!!");

            ITwoWayRemotingClient client = (ITwoWayRemotingClient)Activator.GetObject(typeof(ITwoWayRemotingClient), string.Format("{0}", RemotingServices.GetObjectUri((MarshalByRefObject)remotingClient), responseChannelName));

            System.Diagnostics.Trace.WriteLine("************** Connecting2");
            
            if (ClientRequestingConnection != null)
            {
                TwoWayRemotingConnectionEventArgs e = new TwoWayRemotingConnectionEventArgs(client, sessionKey, username, password);

                ClientRequestingConnection(this, e);
            }
        }

        public void Disconnect(ITwoWayRemotingClient remotingClient, string sessionKey)
        {
            DisconnectClient(remotingClient, null, false);
        }

        public bool Ping()
        {
            return true;
        }

        public object CallServerMethod(ITwoWayRemotingClient remotingClient, string methodName, params object[] methodArgs)
        {
            // Check to see if the client is connected. If it's not, throw a network error.
            if (!IsClientConnected(remotingClient))
                throw new System.Net.Sockets.SocketException(10061);

            if (serverObject != null)
            {
                List<object> methodParams = new List<object>(methodArgs);

                // Create a type array for our params
                List<Type> paramTypes = new List<Type>(methodArgs.Length);
                foreach (object param in methodArgs)
                {
                    paramTypes.Add(param.GetType());
                }

                // See if our method exists
                System.Reflection.MethodInfo method = serverObject.GetType().GetMethod(methodName, paramTypes.ToArray());

                // If it doesn't, see if we should pass in our clientObject as our first param
                if (method == null)
                {
                    paramTypes.Insert(0, typeof(ITwoWayRemotingClient));
                    methodParams.Insert(0, remotingClient);
                    method = serverObject.GetType().GetMethod(methodName, paramTypes.ToArray());

                    // If it still doesn't exist, try adding our client uri as our first param
                    /*if (method == null)
                    {
                        paramTypes[0] = typeof(string);
                        
                        try
                        {
                            method = serverObject.GetType().GetMethod(methodName, paramTypes.ToArray());
                        }
                        catch
                        {
                        }
                    }*/
                }
                
                if (method == null)
                    throw new NotSupportedException("The specified method does not exist on this object");

                return method.Invoke(serverObject, methodParams.ToArray());
            }
            else
                return null;
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Stop();
        }

        #endregion
    }
}
