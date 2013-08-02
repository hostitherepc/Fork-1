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
using System.Net;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;

namespace NET.Remoting.Channels
{
    /// <summary>Used to create a secure server channel sink.</summary>
    public class SecureServerChannelSinkProvider : IServerChannelSinkProvider
    {
        private IServerChannelSinkProvider _next = null;
        private string _algorithm = "3DES";
        private bool _requireSecurity = false;
        private double _connectionAgeLimit = 60.0;
        private double _sweepFrequency = 15.0;

        public event EventHandler<AuthenticationEventArgs> OnAuthentication;
        public event EventHandler<ManagementAllowedEventArgs> OnManagementAllowed;
        
        public SecureServerChannelSinkProvider()
        {
        }

        public SecureServerChannelSinkProvider(IDictionary properties, ICollection providerData)
        {
            foreach (DictionaryEntry entry in properties)
            {
                switch (entry.Key.ToString().ToLower())
                {
                    case "algorithm":
                        _algorithm = (string)entry.Value;
                        break;
                    case "connectionagelimit":
                        _connectionAgeLimit = double.Parse((string)entry.Value);
                        if (_connectionAgeLimit < 0)
                        {
                            throw new ArgumentException("Connection age limit must be greater than 0.", "_connectionAgeLimit");
                        }
                        break;
                    case "sweepfrequency":
                        _sweepFrequency = double.Parse((string)entry.Value);
                        if (_sweepFrequency < 0)
                        {
                            throw new ArgumentException("Sweep frequency must be greater than 0.", "_sweepFrequency");
                        }
                        break;

                    case "requiresecurity":
                        _requireSecurity = bool.Parse((string)entry.Value);
                        break;
                    default:
                        throw new ArgumentException("Invalid configuration entry: " + (String)entry.Key);
                }
            }
        }

        public IServerChannelSink CreateSink(IChannelReceiver channel)
        {
            IServerChannelSink nextSink = null;
            if (_next != null)
            {
                if ((nextSink = _next.CreateSink(channel)) == null) return null;
            }

            SecureServerChannelSink sSink = new SecureServerChannelSink(nextSink, _algorithm, _connectionAgeLimit, _sweepFrequency, _requireSecurity);
            sSink.OnAuthentication += new EventHandler<AuthenticationEventArgs>(sSink_OnAuthentication);
            sSink.OnManagementAllowed += new EventHandler<ManagementAllowedEventArgs>(sSink_OnManagementAllowed);
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

        public void GetChannelData(System.Runtime.Remoting.Channels.IChannelDataStore channelData){}

        public System.Runtime.Remoting.Channels.IServerChannelSinkProvider Next
        {
            get
            {
                return _next;
            }
            set
            {
                _next = value;
            }
        }
    }
}
