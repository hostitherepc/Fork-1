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

  File:        PipeChannel.cs

=====================================================================*/

using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Channels;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections;
using System.Threading;

namespace NET.Remoting
{
    public class PipeChannel : IChannelReceiver, 
                               IChannelSender
    {
        private PipeClientChannel _clientChannel = null;
        private PipeServerChannel _serverChannel = null;

        public PipeChannel()
        {
            _clientChannel = new PipeClientChannel();
        }

        public PipeChannel(String name) : this()
        {
            _serverChannel = new PipeServerChannel(name);
        }

        public PipeChannel(
                    IDictionary properties, 
                    IClientChannelSinkProvider clientProviderChain,
                    IServerChannelSinkProvider serverProviderChain
                )
        {
          _clientChannel = new PipeClientChannel(properties, clientProviderChain);
          _serverChannel = new PipeServerChannel(properties, serverProviderChain);
        }

        // IChannel
        public String ChannelName     { get { return(_clientChannel.ChannelName); } }
        public int    ChannelPriority { get { return(_clientChannel.ChannelPriority); } }
        
        public String Parse(String url, out string uri)
        {
            return(PipeConnection.Parse(url, out uri));
        }
        
        // IChannelSender
        public IMessageSink CreateMessageSink(String url, Object data, out String uri)
        {
            return _clientChannel.CreateMessageSink(url, data, out uri);
        }

        // IChannelReciever
        public Object ChannelData
        {
            get { return (_serverChannel == null) ? null : _serverChannel.ChannelData; }
        }

        public String[] GetUrlsForUri(String objectURI)
        {
            if (_serverChannel != null)
            {
                return _serverChannel.GetUrlsForUri(objectURI);            
            }
            else
            {
                return null;
            }
        }
       
        public void StartListening(Object data)
        {
            if (_serverChannel != null)
                _serverChannel.StartListening(data);
        }

        public void StopListening(Object data)
        {
            if (_serverChannel != null)
                _serverChannel.StopListening(data);
        }

        public void Dispose()
        {
            if (_serverChannel != null)
                _serverChannel.Dispose();

            if (_clientChannel != null)
                _clientChannel.Dispose();
        }
    }
}
