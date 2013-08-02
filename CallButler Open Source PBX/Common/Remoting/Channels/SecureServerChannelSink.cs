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
using System.IO;
using System.Net;
using System.Timers;
using System.Threading;
using System.Collections;
using System.Security.Cryptography;
using System.DirectoryServices;
using System.Security.Principal;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using Microsoft.Win32;


namespace NET.Remoting.Channels
{
    /// <summary>
    /// Server channel sink that, in conjunction with SecureClientChannelSink, provides an 
    /// asymmetric key exchange and shared key encryption across a remoting channel.
    /// </summary>
    internal class SecureServerChannelSink : BaseChannelSinkWithProperties, IServerChannelSink
    {
        private readonly string _algorithm = "";
        private readonly double _connectionAgeLimit;
        private readonly double _sweepFrequency;
        private readonly bool _requireSecurity;
        private readonly Hashtable _connections = null;
        private readonly IServerChannelSink _next = null;
        private System.Timers.Timer _sweepTimer = null;
        private Hashtable _authUsers;
        public event EventHandler<AuthenticationEventArgs> OnAuthentication;
        public event EventHandler<ManagementAllowedEventArgs> OnManagementAllowed;

        public SecureServerChannelSink(IServerChannelSink nextSink, string algorithm, double connectionAgeLimit, double sweeperFrequency, bool requireSecurity)
        {
            _algorithm = algorithm;
            _connectionAgeLimit = connectionAgeLimit;
            _sweepFrequency = sweeperFrequency;
            _requireSecurity = requireSecurity;

            _next = nextSink;

            _connections = new Hashtable(103, 0.5F);
            StartConnectionSweeper();
        }

        private ServerProcessing MakeSharedKey(Guid transactID, ITransportHeaders requestHeaders, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
        {
            SymmetricAlgorithm symmetricProvider = CryptoHelper.GetNewSymmetricProvider(_algorithm);

            ClientConnectionInfo cci = new ClientConnectionInfo(transactID, symmetricProvider);

            lock (_connections.SyncRoot)
            {
                _connections[transactID.ToString()] = cci;
            }

            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();

            string publicKey = requestHeaders[CommonHeaders.PublicKey].ToString();

            if (publicKey == null || publicKey == string.Empty)
            {
                throw new SecureRemotingException("No public key found with which to encrypt the shared key.");
            }

            rsaProvider.FromXmlString(publicKey); // load the public key

            byte[] encryptedKey = rsaProvider.Encrypt(symmetricProvider.Key, false);
            byte[] encryptedIV = rsaProvider.Encrypt(symmetricProvider.IV, false);

            responseHeaders = new TransportHeaders();
            responseHeaders[CommonHeaders.Transaction] = ((int)SecureTransaction.SendingSharedKey).ToString();
            responseHeaders[CommonHeaders.SharedKey] = Convert.ToBase64String(encryptedKey);
            responseHeaders[CommonHeaders.SharedIV] = Convert.ToBase64String(encryptedIV);

            IPAddress clientAddress = requestHeaders[CommonTransportKeys.IPAddress] as IPAddress;

            responseMsg = null;
            responseStream = new MemoryStream();

            return ServerProcessing.Complete;
        }

        public ServerProcessing ProcessEncryptedMessage(Guid transactID, IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
        {
            ClientConnectionInfo cci;
            lock (_connections.SyncRoot)
            {
                cci = (ClientConnectionInfo)_connections[transactID.ToString()];
            }

            if (cci == null)
            {
                throw new SecureRemotingException("No connection information about client.");
            }

            cci.UpdateLastUsed();

            Stream decryptedStream = CryptoHelper.GetDecryptedStream(requestStream, cci.Provider);
            requestStream.Close();

            ServerProcessing processingResult = _next.ProcessMessage(sinkStack, requestMsg, requestHeaders, decryptedStream, out responseMsg, out responseHeaders, out responseStream);


            responseHeaders[CommonHeaders.Transaction] = ((int)SecureTransaction.SendingEncryptedResult).ToString();
            Stream encryptedStream = CryptoHelper.GetEncryptedStream(responseStream, cci.Provider);
            responseStream.Close(); // close the plaintext stream now that we're done with it
            responseStream = encryptedStream;

            return processingResult;
        }

        private bool PreviousTransactionWithClient(Guid transactID)
        {
            lock (_connections.SyncRoot)
            {
                return (!transactID.Equals(Guid.Empty) && _connections[transactID.ToString()] != null);
            }
        }

        private ServerProcessing SendEmptyToClient(SecureTransaction transactType, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
        {
            responseMsg = null;
            responseStream = new MemoryStream();
            responseHeaders = new TransportHeaders();
            responseHeaders[CommonHeaders.Transaction] = ((int)transactType).ToString();
            return ServerProcessing.Complete;
        }

        private Hashtable AuthUsers
        {
            get
            {
                if (_authUsers == null)
                {
                    _authUsers = new Hashtable();
                    _authUsers = Hashtable.Synchronized(_authUsers);
                }
                return _authUsers;
            }
        }

        private bool IsAuthenticated(string userKey)
        {
            if (AuthUsers[userKey] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CheckForManagementAllowed(ClientConnectionInfo cci, string clientIpAddress, string extension)
        {
            RijndaelHelper h = new RijndaelHelper(System.Text.Encoding.ASCII.GetString(cci.Provider.Key));

            string extensionNumber = h.Decrypt(extension);
            if (OnManagementAllowed != null)
            {
                ManagementAllowedEventArgs args = new ManagementAllowedEventArgs( clientIpAddress, extensionNumber );

                OnManagementAllowed(this, args);

                if (args.ManagementAllowed == false)
                {
                    throw new Exception("Remote Management not allowed for : " + clientIpAddress);
                }
            }
            else
            {
                throw new Exception("Remote Management not allowed");
            }
        }

        public void Authenticate(ClientConnectionInfo cci, string encryptedCustomerID, string encryptedExtensionNumber, string encryptedPassword)
        {
            string userKey = encryptedCustomerID + " " + encryptedExtensionNumber + " " + encryptedPassword;

            if (IsAuthenticated(userKey) == false)
            {
                RijndaelHelper h = new RijndaelHelper(System.Text.Encoding.ASCII.GetString(cci.Provider.Key));

                int customerID = Convert.ToInt32(h.Decrypt(encryptedCustomerID));
                int extensionNumber = Convert.ToInt32(h.Decrypt(encryptedExtensionNumber));
                string password = h.Decrypt(encryptedPassword);

                if (OnAuthentication != null)
                {
                    AuthenticationEventArgs args = new AuthenticationEventArgs(customerID, extensionNumber, password);
                    OnAuthentication(this, args);

                    if (args.IsAuthenticated == false)
                    {
                        throw new Exception("Failed to authenticate");
                    }
                    else
                    {
                        lock (AuthUsers.SyncRoot)
                        {
                            AuthUsers.Add(userKey, DateTime.Now);
                        }
                    }
                }
                else
                {
                    throw new Exception("Failed to authenticate");
                }
            }
        }

        public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
        {
            responseHeaders = null;

            string strTransactID = (string)requestHeaders[CommonHeaders.ID];
            Guid transactID = (strTransactID == null ? Guid.Empty : new Guid(strTransactID));
            SecureTransaction transactType = (SecureTransaction)Convert.ToInt32((string)requestHeaders[CommonHeaders.Transaction]);

            IPAddress clientAddress = requestHeaders[CommonTransportKeys.IPAddress] as IPAddress;
            
            sinkStack.Push(this, null);

            ServerProcessing processingResult;

            switch (transactType)
            {
                case SecureTransaction.SendingPublicKey:
                    {
                        processingResult = MakeSharedKey(transactID, requestHeaders, out responseMsg, out responseHeaders, out responseStream);
                        System.Diagnostics.Debug.WriteLine("Connection added: " + transactID);
                        break;
                    }
                case SecureTransaction.SendingEncryptedMessage:
                    {
                        ClientConnectionInfo cci = (ClientConnectionInfo)_connections[transactID.ToString()];
                        string customerID = requestHeaders[CommonHeaders.CustomerID].ToString();
                        string password = requestHeaders[CommonHeaders.Password].ToString();
                        string extensionNumber = requestHeaders[CommonHeaders.ExtensionNumber].ToString();

                        
                        if (PreviousTransactionWithClient(transactID))
                        {
                            if (RequireSecurity == true)
                            {
                                Authenticate(cci,customerID, extensionNumber, password);
                            }
                        
                            processingResult = ProcessEncryptedMessage(transactID, sinkStack, requestMsg, requestHeaders, requestStream, out responseMsg, out responseHeaders, out responseStream);

                            if (clientAddress != null && cci != null)
                            {
                                CheckForManagementAllowed(cci, clientAddress.ToString(), extensionNumber);
                            }                
                            
                        }
                        else
                        {
                            processingResult = SendEmptyToClient(SecureTransaction.UnknownIdentifier, out responseMsg, out responseHeaders, out responseStream);
                        }

                 
                        break;
                    }
                case SecureTransaction.Uninitialized:
                    {
                        if (!RequireSecurity)
                        {
                            processingResult = _next.ProcessMessage(sinkStack, requestMsg, requestHeaders, requestStream, out responseMsg, out responseHeaders, out responseStream);
                        }
                        else
                        {
                            throw new SecureRemotingException("Server requires a secure connection for this client");
                        }
                        break;
                    }
                default:
                    {
                        // Houston, we have a problem!
                        throw new SecureRemotingException("Invalid request from client: " + transactType + ".");
                    }
            }

            sinkStack.Pop(this);
            return processingResult;
        }

        private bool RequireSecurity
        {
            get
            {
                return _requireSecurity;
            }
        }

        public IServerChannelSink NextChannelSink
        {
            get
            {
                return _next;
            }
        }

        public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
        {
            return null;
        }

        public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream stream)
        {
            throw new NotSupportedException();
        }

        private void StartConnectionSweeper()
        {
            if (_sweepTimer == null)
            {
                _sweepTimer = new System.Timers.Timer(_sweepFrequency * 1000);
                _sweepTimer.Elapsed += new ElapsedEventHandler(SweepConnections);
                _sweepTimer.Elapsed += new ElapsedEventHandler(SweepAuthentication);
                _sweepTimer.Start();
            }
        }

        private void SweepConnections(object sender, ElapsedEventArgs e)
        {
            lock (_connections.SyncRoot)
            {
                ArrayList toDelete = new ArrayList(_connections.Count);

                foreach (DictionaryEntry entry in _connections)
                {
                    ClientConnectionInfo cci = (ClientConnectionInfo)entry.Value;
                    if (cci.LastUsed.AddSeconds(_connectionAgeLimit).CompareTo(DateTime.UtcNow) < 0)
                    {
                        toDelete.Add(entry.Key);
                        ((IDisposable)cci).Dispose();
                    }
                }

                foreach (Object obj in toDelete)
                {
                    _connections.Remove(obj);
                }
                toDelete = null;
            }
        }

        private void SweepAuthentication(object sender, ElapsedEventArgs e)
        {
            lock (_connections.SyncRoot)
            {
                if (_authUsers != null)
                {
                    ArrayList toDelete = new ArrayList(_authUsers.Count);

                    foreach (DictionaryEntry entry in _authUsers)
                    {
                        DateTime authTime = Convert.ToDateTime(entry.Value);

                        if (authTime.AddSeconds(_connectionAgeLimit).CompareTo(DateTime.UtcNow) < 0)
                        {
                            toDelete.Add(entry.Key);
                        }
                    }

                    foreach (Object obj in toDelete)
                    {
                        _authUsers.Remove(obj);
                    }
                    toDelete = null;
                }
            }
        }
    }
}
