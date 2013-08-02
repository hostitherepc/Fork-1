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
using System.Threading;
using System.Collections;
using System.Security.Cryptography;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace NET.Remoting.Channels
{
    /// <summary>
    /// Client channel sink that, in conjunction with SecureServerChannelSink, provides an
    /// asymmetric key exchange and shared key encryption across a remoting channel.
    /// </summary>
    internal class SecureClientChannelSink : BaseChannelSinkWithProperties, IClientChannelSink
    {
        private readonly string _algorithm;
        private readonly int _maxAttempts;
        private readonly IClientChannelSink _next;
        private Guid _transactID = Guid.Empty;
        //private int _customerID;
        //private int _extensionNumber;
        //private string _password;

        private volatile SymmetricAlgorithm _provider = null;
        private volatile RSACryptoServiceProvider _rsaProvider = null;
        private readonly object _transactionLock = null;
        private const string _defaultExceptionText = "The client sink is unable to maintain a secure channel with server.";

        private Hashtable _sinkProperties;

        public SecureClientChannelSink(IClientChannelSink nextSink, string algorithm, int maxAttempts, int customerID, int extensionNumber, string password)
        {
            _algorithm = algorithm;
            _next = nextSink;
            _maxAttempts = maxAttempts;
            _transactionLock = new object();
            _rsaProvider = new RSACryptoServiceProvider();
            

            //set custom props
            _sinkProperties = new Hashtable();

            _sinkProperties["customerid"] = customerID;
            _sinkProperties["extensionnumber"] = extensionNumber;
            _sinkProperties["password"] = password;
        }

        public override ICollection Keys
        {
            get
            {
                return _sinkProperties.Keys;
            }
        }

        public override object this[object key]
        {
            get
            {
                return _sinkProperties[key];
            }
            set
            {
                _sinkProperties[key] = value;
            }
        }

        private void CreateSharedKeyRequest(ITransportHeaders requestHeaders)
        {
            string rsaKey = _rsaProvider.ToXmlString(false);

            requestHeaders[CommonHeaders.Transaction] = ((int)SecureTransaction.SendingPublicKey).ToString();
            requestHeaders[CommonHeaders.ID] = _transactID.ToString();
            requestHeaders[CommonHeaders.PublicKey] = rsaKey;
        }

        private void SendCredentials(ITransportHeaders requestHeaders)
        {
            string key = System.Text.Encoding.ASCII.GetString(_provider.Key);
            RijndaelHelper rh = new RijndaelHelper(key);

            if (_sinkProperties != null)
            {

                requestHeaders[CommonHeaders.CustomerID] = rh.Encrypt(this["customerid"].ToString());
                requestHeaders[CommonHeaders.ExtensionNumber] = rh.Encrypt(this["extensionnumber"].ToString());
                requestHeaders[CommonHeaders.Password] = rh.Encrypt(this["password"].ToString());
            }
        }

        private Stream DecryptResponse(Stream responseStream, ITransportHeaders responseHeaders)
        {
            try
            {
                if (responseHeaders != null && SecureTransaction.SendingEncryptedResult == (SecureTransaction)Convert.ToInt32((string)responseHeaders[CommonHeaders.Transaction]))
                {
                    Stream decryptedStream = CryptoHelper.GetDecryptedStream(responseStream, _provider);
                    responseStream.Close();
                    return decryptedStream;
                }
            }
            catch { }
            return null;
        }

        private SymmetricAlgorithm ProcessSharedKeyResponse(ITransportHeaders responseHeaders)
        {
            if (responseHeaders == null)
            {
                throw new SecureRemotingException("Transport response is null.");
            }
            string encryptedKey = responseHeaders[CommonHeaders.SharedKey].ToString();
            string encryptedIV = responseHeaders[CommonHeaders.SharedIV].ToString();

            if (encryptedKey == null || encryptedKey == string.Empty)
            {
                throw new SecureRemotingException("Expected shared key from server.");
            }

            if (encryptedIV == null || encryptedIV == string.Empty)
            {
                throw new SecureRemotingException("Expected shared IV from server.");
            }

            SymmetricAlgorithm sharedProvider = CryptoHelper.GetNewSymmetricProvider(_algorithm);
            sharedProvider.Key = _rsaProvider.Decrypt(Convert.FromBase64String(encryptedKey), false);

            string s = System.Text.Encoding.ASCII.GetString(sharedProvider.Key);

            sharedProvider.IV = _rsaProvider.Decrypt(Convert.FromBase64String(encryptedIV), false);
            return sharedProvider;
        }

        private SymmetricAlgorithm ObtainSharedKey(IMessage msg)
        {
            TransportHeaders requestHeaders = new TransportHeaders();
            MemoryStream requestStream = new MemoryStream();
            ITransportHeaders responseHeaders;
            Stream responseStream;

            CreateSharedKeyRequest(requestHeaders);

            _next.ProcessMessage(msg, requestHeaders, requestStream, out responseHeaders, out responseStream);

            return ProcessSharedKeyResponse(responseHeaders);
        }

        private void ClearSharedKey()
        {
            _provider = null;
            _transactID = Guid.Empty;
        }

        private Stream SetupEncryptedMessage(ITransportHeaders requestHeaders, Stream requestStream, ITransportHeaders responseHeaders)
        {
            requestStream = CryptoHelper.GetEncryptedStream(requestStream, _provider);
            requestHeaders[CommonHeaders.Transaction] = ((int)SecureTransaction.SendingEncryptedMessage).ToString();
            requestHeaders[CommonHeaders.ID] = _transactID.ToString();
            SendCredentials(requestHeaders);

            return requestStream;
        }

        private bool ProcessEncryptedMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream)
        {
            Guid id;
            responseHeaders = null;
            lock (_transactionLock)
            {
                id = EnsureIDAndProvider(msg, requestHeaders);
                requestStream = SetupEncryptedMessage(requestHeaders, requestStream, responseHeaders);
            }

            _next.ProcessMessage(msg, requestHeaders, requestStream, out responseHeaders, out responseStream);

            lock (_transactionLock)
            {
                responseStream = DecryptResponse(responseStream, responseHeaders);
                if (responseStream == null && id.Equals(_transactID))
                {
                    ClearSharedKey();
                }
            }

            return responseStream != null;
        }

        private Guid EnsureIDAndProvider(IMessage msg, ITransportHeaders requestHeaders)
        {
            if (_provider == null || _transactID.Equals(Guid.Empty))
            {
                _transactID = Guid.NewGuid();
                _provider = ObtainSharedKey(msg);
            }
            return _transactID;
        }

        public void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream)
        {
            try
            {
                responseHeaders = null;
                responseStream = null;

                long initialStreamPos = requestStream.CanSeek ? requestStream.Position : -1;

                for (int i = 0; i < _maxAttempts; i++)
                {
                    if (ProcessEncryptedMessage(msg, requestHeaders, requestStream, out responseHeaders, out responseStream))
                    {
                        return;
                    }

                    if (requestStream.CanSeek)
                    {
                        requestStream.Position = initialStreamPos;
                    }
                    else
                    {
                        break;
                    }
                }

                throw new SecureRemotingException(_defaultExceptionText);
            }
            finally
            {
                requestStream.Close();
            }
        }

        public Stream GetRequestStream(System.Runtime.Remoting.Messaging.IMessage msg, System.Runtime.Remoting.Channels.ITransportHeaders headers)
        {
            return null;
        }

        public IClientChannelSink NextChannelSink
        {
            get
            {
                return _next;
            }
        }

        private class AsyncProcessingState
        {
            private Stream _stream;
            private ITransportHeaders _headers;
            private IMessage _msg;
            private Guid _id;

            public AsyncProcessingState(IMessage msg, ITransportHeaders headers, ref Stream stream, Guid id)
            {
                _msg = msg;
                _headers = headers;
                _stream = DuplicateStream(ref stream);
                _id = id;
            }

            public Stream Stream
            {
                get
                {
                    return _stream;
                }
            }

            public ITransportHeaders Headers
            {
                get
                {
                    return _headers;
                }
            }

            public IMessage Message
            {
                get
                {
                    return _msg;
                }
            }

            public Guid ID
            {
                get
                {
                    return _id;
                }
            }

            private Stream DuplicateStream(ref Stream stream)
            {
                MemoryStream memStream1 = new MemoryStream();
                MemoryStream memStream2 = new MemoryStream();

                byte[] buffer = new byte[1024];
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memStream1.Write(buffer, 0, read);
                    memStream2.Write(buffer, 0, read);
                }
                stream.Close();

                memStream1.Position = 0;
                memStream2.Position = 0;

                stream = memStream1;
                return memStream2;
            }
        }

        public void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream)
        {
            AsyncProcessingState state = null;
            Stream encryptedStream = null;
            Guid id;

            lock (_transactionLock)
            {
                id = EnsureIDAndProvider(msg, headers);

                state = new AsyncProcessingState(msg, headers, ref stream, id);

                encryptedStream = SetupEncryptedMessage(headers, stream, null);
            }

            sinkStack.Push(this, state);
            _next.AsyncProcessRequest(sinkStack, msg, headers, encryptedStream);
        }

        public void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream)
        {
            AsyncProcessingState asyncState = (AsyncProcessingState)state;

            try
            {
                SecureTransaction transactType = (SecureTransaction)Convert.ToInt32((string)headers[CommonHeaders.Transaction]);
                switch (transactType)
                {
                    case SecureTransaction.SendingEncryptedResult:
                        {
                            lock (_transactionLock)
                            {
                                if (asyncState.ID.Equals(_transactID))
                                {
                                    stream = DecryptResponse(stream, headers);
                                }
                                else
                                {
                                    throw new SecureRemotingException("The key has changed since the message was decrypted.");
                                }
                            }
                            break;
                        }
                    case SecureTransaction.UnknownIdentifier:
                        {
                            throw new SecureRemotingException("The server sink was unable to identify the client, most likely due to the connection information timing out.");
                        }
                    default:
                    case SecureTransaction.Uninitialized:
                        {
                            break;
                        }
                }
            }
            catch (SecureRemotingException)
            {
                lock (_transactionLock)
                {
                    if (_provider == null || asyncState.ID.Equals(_transactID))
                    {
                        ClearSharedKey();
                    }
                    ProcessMessage(asyncState.Message, asyncState.Headers, asyncState.Stream, out headers, out stream);
                }
            }
            finally
            {
                asyncState.Stream.Close();
            }

            sinkStack.AsyncProcessResponse(headers, stream);
        }
    }
}