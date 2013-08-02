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

// Stephen Toub
// stoub@microsoft.com
// SecureServerChannelSinkProvider.cs

using System;
using System.Net;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;

namespace NET.Remoting.ChannelSinks
{
	/// <summary>Used to create a secure server channel sink.</summary>
	public class SecureServerChannelSinkProvider : IServerChannelSinkProvider
	{
		#region Member Variables
		/// <summary>The next sink provider in the sink provider chain.</summary>
		private IServerChannelSinkProvider _next = null;
		/// <summary>The name of the symmetric algorithm to use.</summary>
		private string _algorithm = "DES";
		/// <summary>Whether OAEP padding should be used.</summary>
		private bool _oaep = false;
		/// <summary>Whether the server requires the client to use the secure client sink.</summary>
		private bool _requireSecurity = false;
		/// <summary>The minimum amount of time (s) information about a client connection should be retained.</summary>
		private double _connectionAgeLimit = 60.0;
		/// <summary>How often (s) the connection sweeper should run.</summary>
		private double _sweepFrequency = 15.0;
		/// <summary>List of client IP's that should have the opposite security requirement.</summary>
		private IPAddress [] _securityExemptionList = null;
		#endregion

		#region Construction
		/// <summary>Initializes the sink provider.</summary>
		public SecureServerChannelSinkProvider()
		{
		}

		/// <summary>Initializes the sink provider.</summary>
		/// <param name="properties">Parameters specified in the config file.</param>
		/// <param name="providerData">Sink provider data.</param>
		public SecureServerChannelSinkProvider(IDictionary properties, ICollection providerData)
		{
			// Read in web.config parameters
			foreach (DictionaryEntry entry in properties)
			{
				switch ((String)entry.Key)
				{
					case "algorithm":
						_algorithm = (string)entry.Value;
						break;

					case "oaep":
						_oaep = bool.Parse((string)entry.Value);
						break;

					case "connectionAgeLimit":
						_connectionAgeLimit = double.Parse((string)entry.Value); 
						if (_connectionAgeLimit < 0) throw new ArgumentException("Connection age limit must be greater than 0.", "_connectionAgeLimit");
						break;

					case "sweepFrequency":
						_sweepFrequency = double.Parse((string)entry.Value); 
						if (_sweepFrequency < 0) throw new ArgumentException("Sweep frequency must be greater than 0.", "_sweepFrequency");
						break;

					case "requireSecurity":
						_requireSecurity = bool.Parse((string)entry.Value);
						break;

					case "securityExemptionList":
						// Get the semicolon separated list of IP addresses and parse them
						// all into an array of IPAddress objects
						string ipList = (string)entry.Value;
						if (ipList != null && ipList != string.Empty) 
						{
							string [] values = ipList.Split(';');
							_securityExemptionList = new IPAddress[values.Length];
							for(int i=0; i<values.Length; i++) _securityExemptionList[i] = IPAddress.Parse(values[i].Trim());
						}
						break;

					default:
						throw new ArgumentException("Invalid configuration entry: " + (String)entry.Key);
				}
			}
		}
		#endregion

		#region Sink Creation
		/// <summary>Creates the channel sink.</summary>
		/// <param name="channel">The channel for which to create the channel sink chain.</param>
		/// <returns>The new channel sink.</returns>
		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			IServerChannelSink nextSink = null;
			if (_next != null) 
			{
				// Call CreateSink on the next sink provider in the chain.  This will return
				// to us the actual next sink object.  If the next sink is null, uh oh!
				if ((nextSink = _next.CreateSink(channel)) == null) return null;
			}

			// Create this sink, passing to it the previous sink in the chain so that it knows
			// to whom messages should be passed.
			return new SecureServerChannelSink(
				nextSink, _algorithm, _oaep, 
				_connectionAgeLimit, _sweepFrequency, 
				_requireSecurity, _securityExemptionList);
		}

		/// <summary>Returns the channel data for the channel that the current sink is associated with.</summary>
		/// <param name="channelData">An IChannelDataStore object in which the channel data is to be returned.</param>
		public void GetChannelData(System.Runtime.Remoting.Channels.IChannelDataStore channelData)
		{
			// Do nothing.  No channel specific data.
		}

		/// <summary>Gets or sets the next sink provider in the channel sink provider chain.</summary>
		public System.Runtime.Remoting.Channels.IServerChannelSinkProvider Next
		{
			get { return _next; }
			set { _next = value; }
		}
		#endregion
	}
}
