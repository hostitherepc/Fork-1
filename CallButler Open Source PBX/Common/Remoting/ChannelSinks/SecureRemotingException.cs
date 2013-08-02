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
// SecureRemotingException.cs

using System;
using System.Security.Permissions;
using System.Runtime.Remoting;
using System.Runtime.Serialization;

namespace NET.Remoting.ChannelSinks
{
	/// <summary>The exception that is thrown when something goes wrong in the secure remoting channel.</summary>
	[Serializable]
	public class SecureRemotingException : RemotingException, ISerializable
	{
		#region Construction and Serialization
		/// <summary>Initializes a new instance of the SecureRemotingException class with default properties.</summary>
		public SecureRemotingException()
		{
		}
		
		/// <summary>Initializes a new instance of the SecureRemotingException class with the given message.</summary>
		/// <param name="message">The error message that explains why the exception occurred.</param>
		public SecureRemotingException(string message) : 
			base(message)
		{
		}

		/// <summary>Initializes a new instance of the SecureRemotingException class with the specified properties.</summary>
		/// <param name="message">The error message that explains why the exception occurred.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		public SecureRemotingException(string message, System.Exception innerException) : 
			base(message, innerException)
		{
		}

		/// <summary>Initializes the exception with serialized information.</summary>
		/// <param name="info">Serialization information.</param>
		/// <param name="context">Streaming context.</param>
		protected SecureRemotingException(SerializationInfo info, StreamingContext context) :
			base(info, context)
		{
		}

		/// <summary>Provides serialization functionality.</summary>
		/// <param name="info">Serialization information.</param>
		/// <param name="context">Streaming context.</param>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
		#endregion
	}
}
