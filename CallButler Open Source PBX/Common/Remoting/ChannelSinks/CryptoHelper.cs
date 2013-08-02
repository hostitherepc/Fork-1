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
// CryptoHelper.cs

using System;
using System.IO;
using System.Security.Cryptography;

namespace NET.Remoting.ChannelSinks
{
	/// <summary>Helper functions for working with encryption and streams.</summary>
	public class CryptoHelper
	{
		#region Member Variables
		/// <summary>Size to use for byte buffers when performing IO.</summary>
		private const int _bufferSize = 2048;
		#endregion

		#region Construction
		/// <summary>Prevent external instantiation.  Class only has static helpers.</summary>
		private CryptoHelper() {}
		#endregion

		#region Creating Providers
		/// <summary>Factory for symmetric algorithm providers.  Creates a new provider by name.</summary>
		/// <param name="algorithm">The name of the algorithm to use (e.g. "DES")</param>
		/// <returns>A SymmetricAlgorithm provider to be used for communication
		/// between client and server.</returns>
		/// <remarks>Currently supports "DES", "3DES", "RIJNDAEL", and "RC2".</remarks>
		public static SymmetricAlgorithm GetNewSymmetricProvider(string algorithm)
		{
			// Return a provider based on the chosen algorithm
			switch(algorithm.Trim().ToLower())
			{
				case "3des": return new TripleDESCryptoServiceProvider();
				case "rijndael": return new RijndaelManaged();
				case "rc2": return new RC2CryptoServiceProvider();
				case "des": return new DESCryptoServiceProvider();
				default: throw new ArgumentException("Provider must be '3DES', 'DES', 'RIJNDAEL', or 'RC2'.", "algorithm");
			}
		}
		#endregion

		#region Encryption and Decryption Helpers
		/// <summary>
		/// Encrypts a stream with the specified symmetric provider.  The returned stream
		/// is at position zero and ready to be read.
		/// </summary>
		/// <param name="inStream">The stream to encrypt.</param>
		/// <param name="provider">The cryptographic provider to use for encryption.</param>
		/// <returns>Encrypted stream ready to be read.</returns>
		public static Stream GetEncryptedStream(Stream inStream, SymmetricAlgorithm provider) 
		{
			// Make sure we got valid input
			if (inStream == null) throw new ArgumentNullException("Invalid stream.", "inStream");
			if (provider == null) throw new ArgumentNullException("Invalid provider.", "provider");

			// Create the output stream
			MemoryStream outStream = new MemoryStream();
			CryptoStream encryptStream = new CryptoStream(outStream, provider.CreateEncryptor(), CryptoStreamMode.Write);

			// Encrypt the stream by reading all bytes from the input stream and
			// writing them to the output encryption stream.  Note that we're depending
			// on the fact that ~CryptoStream does not close the underlying stream.
			int numBytes;
			byte [] inputBytes = new byte[_bufferSize];
			while((numBytes = inStream.Read(inputBytes, 0, inputBytes.Length)) != 0) 
			{
				encryptStream.Write(inputBytes, 0, numBytes);
			}
			encryptStream.FlushFinalBlock();

			// Go back to the beginning of the newly encrypted stream and return it
			outStream.Position = 0;
			return outStream;
		}

		
		/// <summary>
		/// Decrypts a stream with the specified symmetric provider.
		/// </summary>
		/// <param name="inStream">The stream to decrypt.</param>
		/// <param name="provider">The cryptographic provider to use for encrypting.</param>
		/// <returns>Plaintext stream ready to be read.</returns>
		public static Stream GetDecryptedStream(Stream inStream, SymmetricAlgorithm provider) 
		{
			// Make sure we got valid input
			if (inStream == null) throw new ArgumentNullException("Invalid stream.", "inStream");
			if (provider == null) throw new ArgumentNullException("Invalid provider.", "provider");

			// Create the input and output streams
			CryptoStream decryptStream = new CryptoStream(inStream, provider.CreateDecryptor(), CryptoStreamMode.Read);
			MemoryStream outStream = new MemoryStream();
			
			// Read the stream and write it to the output. Note that we're depending
			// on the fact that ~CryptoStream does not close the underlying stream.
			int numBytes;
			byte [] inputBytes = new byte[_bufferSize];
			while((numBytes = decryptStream.Read(inputBytes, 0, inputBytes.Length)) != 0) 
			{
				outStream.Write(inputBytes, 0, numBytes);
			}

			// Go to the beginning of the decrypted stream and return it
			outStream.Position = 0;
			return outStream;
		}
		#endregion
	}
}
