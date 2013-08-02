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
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace NET.Remoting.Channels
{
    public class CryptoHelper
    {
        private CryptoHelper() { }

        public static SymmetricAlgorithm GetNewSymmetricProvider(string algorithm)
        {
            switch (algorithm.Trim().ToLower())
            {
                case "3des":
                    {
                        return new TripleDESCryptoServiceProvider();
                    }
                case "rijndael":
                    {
                        return new RijndaelManaged();
                    }
                case "rc2":
                    {
                        return new RC2CryptoServiceProvider();
                    }
                case "des":
                    {
                        return new DESCryptoServiceProvider();
                    }
                default:
                    {
                        throw new ArgumentException("Provider must be '3DES', 'DES', 'RIJNDAEL', or 'RC2'.", "algorithm");
                    }
            }
        }

        public static Stream GetEncryptedStream(Stream inStream, SymmetricAlgorithm provider)
        {
            if (inStream == null)
            {
                throw new ArgumentNullException("Invalid stream.", "inStream");
            }
            if (provider == null)
            {
                throw new ArgumentNullException("Invalid provider.", "provider");
            }

            Stream compStream = CompressStream(inStream);

            MemoryStream outStream = new MemoryStream();

            CryptoStream encryptStream = new CryptoStream(outStream, provider.CreateEncryptor(), CryptoStreamMode.Write);

            // Read the in stream in 1024 byte chunks
            byte[] buffer = new byte[1024];
            int bytesRead = 0;

            do
            {
                bytesRead = compStream.Read(buffer, 0, 1024);

                if (bytesRead > 0)
                    encryptStream.Write(buffer, 0, bytesRead);
            }
            while (bytesRead > 0);

            encryptStream.FlushFinalBlock();

            outStream.Position = 0;

            return outStream;
        }

        public static Stream GetDecryptedStream(Stream inStream, SymmetricAlgorithm provider)
        {
            if (inStream == null)
            {
                throw new ArgumentNullException("Invalid stream.", "inStream");
            }
            if (provider == null)
            {
                throw new ArgumentNullException("Invalid provider.", "provider");
            }

            CryptoStream decryptStream = new CryptoStream(inStream, provider.CreateDecryptor(), CryptoStreamMode.Read);

            MemoryStream outStream = new MemoryStream();

            // Read the in stream in 1024 byte chunks
            byte[] buffer = new byte[1024];
            int bytesRead = 0;

            do
            {
                bytesRead = decryptStream.Read(buffer, 0, 1024);

                if (bytesRead > 0)
                    outStream.Write(buffer, 0, bytesRead);
            }
            while (bytesRead > 0);

            outStream.Flush();

            Stream unCompStream = DecompressStream(outStream);

            unCompStream.Position = 0;
            return unCompStream;
        }

        public static Stream CompressStream(Stream inStream)
        {
            MemoryStream outStream = new System.IO.MemoryStream();

            inStream.Position = 0;

            using (GZipStream compressedStream = new GZipStream(outStream, CompressionMode.Compress, true))
            {

                // Read the in stream in 1024 byte chunks
                byte[] buffer = new byte[1024];
                int bytesRead = 0;

                do
                {
                    bytesRead = inStream.Read(buffer, 0, 1024);

                    if (bytesRead > 0)
                        compressedStream.Write(buffer, 0, bytesRead);
                }
                while (bytesRead > 0);
            }

            outStream.Seek(0, SeekOrigin.Begin);

            return outStream;
        }

        public static Stream DecompressStream(Stream inStream)
        {
            Stream outStream = new System.IO.MemoryStream();

            inStream.Position = 0;

            using (GZipStream decompressedStream = new GZipStream(inStream, CompressionMode.Decompress))
            {
                // Read the in stream in 1024 byte chunks
                byte[] buffer = new byte[1024];
                int bytesRead = 0;

                do
                {
                    bytesRead = decompressedStream.Read(buffer, 0, 1024);

                    if (bytesRead > 0)
                        outStream.Write(buffer, 0, bytesRead);
                }
                while (bytesRead > 0);
            }

            outStream.Flush();
            outStream.Position = 0;

            return outStream;
        }
    }
}
