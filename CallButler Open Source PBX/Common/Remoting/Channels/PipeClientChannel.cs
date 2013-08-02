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

/*=====================================================================

  File:        PipeClientChannel.cs

=====================================================================*/

using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Channels;
using System.Threading;

namespace NET.Remoting
{
    public class PipeClientChannel: IChannelSender
    {
        // pipe:// prefix
        private const String  ChannelScheme = "pipe";

        private const int     DefaultChannelPriority=1;
        private const String  DefaultChannelName = "Pipe";    

        private int m_ChannelPriority;
        private String m_ChannelName;
        private String m_pipeName = null;

        private IClientChannelSinkProvider _clientSinkProvider; // client sink chain provider

        public PipeClientChannel()
        {
            InitDefaults();
            InitProperties(null);
            InitProviders(null);
        }

        public PipeClientChannel(
                                  IDictionary properties, 
                                  IClientChannelSinkProvider clientProviderChain
                                 )
        {
            InitDefaults();
            InitProperties(properties);
            InitProviders(clientProviderChain);
        }   

        internal void InitDefaults()
        {
         m_ChannelPriority = DefaultChannelPriority;
         m_ChannelName = DefaultChannelName;
        }

        internal void InitProperties(IDictionary properties)
        {
            
            if(properties != null)
            {
             foreach (DictionaryEntry entry in properties)
             {
               switch ((String) entry.Key)
               {
                 case "name": m_ChannelName = (String) entry.Value;
                   break;
                 case "priority": m_ChannelPriority = Convert.ToInt32(entry.Value);
                   break;
                 case "pipe": m_pipeName = (String) entry.Value;
                   break;
               }
             }
            }
        }
        
        void InitProviders(IClientChannelSinkProvider clientProviderChain)
        {
            _clientSinkProvider = clientProviderChain;
            
            if(_clientSinkProvider == null)
            {
              _clientSinkProvider = new BinaryClientFormatterSinkProvider();
            }
            
            IClientChannelSinkProvider tempSinkProvider = _clientSinkProvider;
            
            // Move to the end of provider list
            while (tempSinkProvider.Next != null) 
             tempSinkProvider = tempSinkProvider.Next;
            
            // Append transport sink provider to end
            tempSinkProvider.Next = new PipeClientTransportSinkProvider();
        }

        // IChannel
        public String ChannelName { get { return(m_ChannelName); } }
        public int    ChannelPriority { get { return(m_ChannelPriority); } }

        public String Parse(String url, out string uri)
        {
            return(PipeConnection.Parse(url, out uri));
        }

        // IChannelSender
        public IMessageSink CreateMessageSink(String url, Object data, out String objuri)
        {
            DBG.Info(null, "CreateMessageSink: url = " + url);
            // Set the out parameters
            objuri = null;
            String chanuri = null;

            if (url != null) // Is this a well known object?
            {
                /*
                String urlCompare = String.ToLower(url);

                // Starts with pipe:// ?
                if (urlCompare.StartsWith(ChannelScheme) == false)
                {
                    return null;
                }
                */

                // Parse returns null if this is not one of the pipe channel url's
                chanuri = Parse(url, out objuri);
            }
            else if(data != null)
            {
                IChannelDataStore cds = data as IChannelDataStore;
                if(cds != null)
                {
                    DBG.Info(null, "ChannelUris[0] = " + cds.ChannelUris[0]);
                    //Console.WriteLine("Channel Uri {0}", cds.ChannelUris[0]);

                    chanuri = Parse(cds.ChannelUris[0], out objuri);
                    DBG.Info(null, "CreateMessageSink: chanuri = " + chanuri + ", objuri = " + objuri);
                    if(chanuri != null)
                        url = cds.ChannelUris[0];
                }
            }

            if (null != chanuri)
            {
                if (url == null)
                    url = chanuri;

                DBG.Info(null, "CreateMessageSink: delegating w/ url = " + url);
                //Console.WriteLine("CreateMessageSink: delegating w/ url =  {0}", url);

                return (IMessageSink)_clientSinkProvider.CreateSink(this, url, data);
            }

            DBG.Info(null, "CreateMessageSink: ignoring request...");
            return null;
        } // CreateMessageSink

        public void Dispose()
        {
            // Nothing to do
        }
    }

    internal class PipeClientTransportSinkProvider : IClientChannelSinkProvider
    {
        internal PipeClientTransportSinkProvider()
        {
        }

        public IClientChannelSink CreateSink(IChannelSender channel,
                                             String url,
                                             Object data)
        {
            return new PipeClientTransportSink(url);
        }

        public IClientChannelSinkProvider Next
        {
            get { return null; }
            set { throw new NotSupportedException(); }
        }
    }

    internal class PipeClientTransportSink : IClientChannelSink
    {
        private String _pipeName;
        private PipeConnectionPool _pipeConnectionPool=null;
        private int _defaultRetryCount = 2;

        private WaitCallback callback;

        internal PipeClientTransportSink(String url)
        {
            String objuri = null;
            String chanuri = PipeConnection.Parse(url, out objuri);

            DBG.Info(null, "PipeClientTransportSink: creating pipe on uri: " + chanuri);
            //Console.WriteLine("PipeClientTransportSink {0}", chanuri);

            _pipeName = chanuri;
            _pipeConnectionPool = PipeConnectionPoolManager.LookupPool(_pipeName);

            callback = new WaitCallback(this.ReceiveCallback);
        }

        public IDictionary Properties
        {
            get { return(null); } 
        }

        public void AsyncProcessRequest(IClientChannelSinkStack stack, 
                                        IMessage msg,
                                        ITransportHeaders headers, 
                                        Stream stream)
        {
            DBG.Info(null, "Async: Send the message across the pipe");

            PipeConnection _pipe = SendWithRetry(msg, headers, stream);

            IMethodCallMessage mcm = (IMethodCallMessage)msg;
            MethodBase methodBase = mcm.MethodBase;
            bool oneway = RemotingServices.IsOneWay(methodBase);

            if (oneway)
            {
                if (_pipeConnectionPool != null)
                {
                    _pipeConnectionPool.ReturnToPool(_pipe);
                }
                _pipe = null;
            }
            else
            {
                PipeConnectionCookie cookie = new PipeConnectionCookie();
                
                cookie.pipe = _pipe;
                cookie.sinkStack = stack;

                //TODO Switch to use Completion port
                ThreadPool.QueueUserWorkItem(callback, cookie);
            }
        }

        private void ReceiveCallback(Object state)
        {   
            //Console.WriteLine("ReceiveCallback TID {0}", Thread.CurrentThread.GetHashCode());

            PipeConnectionCookie cookie = (PipeConnectionCookie)state;

            PipeConnection _pipe = cookie.pipe;
            IClientChannelSinkStack sinkStack = cookie.sinkStack;

            try            
            {
    
                // Read response
                //
                _pipe.BeginReadMessage();
               
                ITransportHeaders responseHeaders = _pipe.ReadHeaders();
                responseHeaders["__CustomErrorsEnabled"] = false;

                Stream responseStream  = _pipe.ReadStream();
                _pipe.EndReadMessage();
    
                if (_pipeConnectionPool != null)
                {
                    _pipeConnectionPool.ReturnToPool(_pipe);
                }
    
                _pipe = null;
                
                sinkStack.AsyncProcessResponse(responseHeaders, responseStream);
            }
            catch (Exception e)
            {
                try
                {
                    if (sinkStack != null)
                        sinkStack.DispatchException(e);
                }
                catch(Exception )
                {
                    // Fatal Error.. ignore
                }
            }
        } // ReceiveCallback

        public void AsyncProcessResponse(IClientResponseChannelSinkStack stack, 
                                         Object obj,
                                         ITransportHeaders headers, 
                                         Stream stream)
        {
            throw new NotSupportedException();
        }

        public Stream GetRequestStream(IMessage msg, ITransportHeaders headers)
        {
            // we don't do any serialization here.
            return(null); 
        }

        public void ProcessMessage(IMessage msg, 
                                   ITransportHeaders reqHead,
                                   Stream reqStm, 
                                   out ITransportHeaders respHead,
                                   out Stream respStm)
        {
            DBG.Info(null, "Being asked to process the serialized message!");
            //Console.WriteLine("ProcessMessage TID {0}", Thread.CurrentThread.GetHashCode());

            // Send the message across the pipe.
            PipeConnection _pipe = SendWithRetry(msg, reqHead, reqStm);
            respHead = null;
            respStm = null;
            // Read response
            if (_pipe != null)
            {
                _pipe.BeginReadMessage();
                respHead = _pipe.ReadHeaders();
                respHead["__CustomErrorsEnabled"] = false;
                respStm = _pipe.ReadStream();
                _pipe.EndReadMessage();

                if (_pipeConnectionPool != null)
                {
                    _pipeConnectionPool.ReturnToPool(_pipe);
                }

                _pipe = null;
            }
        }




        IClientChannelSink IClientChannelSink.NextChannelSink
        {
            get { return(null); }
        }

        public PipeConnection SendWithRetry(IMessage msg, 
                                   ITransportHeaders reqHead,
                                   Stream reqStm)
        {
            IMethodCallMessage mcm = (IMethodCallMessage)msg;

            String uri = mcm.Uri;
            PipeConnection _pipe = null;

            int tryCount = _defaultRetryCount;
            long reqStmPosition = -1;

            if (reqStm.CanSeek == false)
                tryCount=1;
            else
                reqStmPosition = reqStm.Position;

            while ( tryCount>0)
            {
                try
                {
                    if (_pipeConnectionPool != null)
                    {
                        DBG.Info(null, "Look in pipe connection in pool");
                        _pipe = (PipeConnection)_pipeConnectionPool.Obtain();
                    }

                    // otherwise create a new connection
                    if (_pipe == null)
                    {
                        _pipe = new PipeConnection(_pipeName, false, IntPtr.Zero);
                    }

                    //Send with Retry
                    _pipe.BeginWriteMessage();
                    _pipe.WriteHeaders(uri, reqHead);
                    _pipe.Write(reqStm);
                    _pipe.EndWriteMessage();

                    tryCount=0;
                }
                catch(PipeIOException pe)
                {
                    pe=pe;
                    if (_pipe != null)
                    {
                        _pipe.Dispose();
                        _pipe = null;
                    }
                   

                   tryCount--;
                   reqStm.Position = reqStmPosition;
                }
            }

            return _pipe;
        }
    }
}












