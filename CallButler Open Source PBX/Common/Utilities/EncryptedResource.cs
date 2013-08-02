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
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WOSI.Utilities
{
    public class EncryptedResource
    {
        private System.Resources.ResourceReader resourceReader;

        public void LoadResourceFile(string filename)
        {
            // Open the skin file
            LoadResourceBytes(File.ReadAllBytes(filename));

            // Unzip the bytes
            /*System.IO.Compression.GZipStream zipStream = new System.IO.Compression.GZipStream(memStream, System.IO.Compression.CompressionMode.Decompress);
            
            MemoryStream unzippedStream = new MemoryStream(FileUtils.GetStreamBytes(zipStream));

            zipStream.Close();
            zipStream.Dispose();
            memStream.Close();
            memStream.Dispose();*/

            //memStream.Close();
            //memStream.Dispose();

            //unzippedStream.Close();
            //unzippedStream.Dispose();
        }

        public void LoadResourceBytes(byte[] resourceData)
        {
            // Decrypt the bytes
            byte[] decryptedBytes = Utilities.CryptoUtils.Decrypt(resourceData, "asdlfa9sd879*Lasldflkajsdf243o8729");

            MemoryStream memStream = new MemoryStream(decryptedBytes);

            resourceReader = new System.Resources.ResourceReader(memStream);
        }

        public void CloseResources()
        {
            if (resourceReader != null)
            {
                resourceReader.Close();
                resourceReader = null;
            }
        }

        public static void CreateResourceFile(string filename, Dictionary<string, object> data)
        {
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();

            System.Resources.ResourceWriter rw = new System.Resources.ResourceWriter(memStream);

            System.Collections.IDictionaryEnumerator dictEnum = data.GetEnumerator();

            while (dictEnum.MoveNext())
            {
                if(dictEnum.Value != null)
                    rw.AddResource(dictEnum.Key.ToString(), dictEnum.Value);
            }

            rw.Generate();

            byte[] resourceBytes = FileUtils.GetStreamBytes(memStream);

            resourceBytes = Utilities.CryptoUtils.Encrypt(resourceBytes, "asdlfa9sd879*Lasldflkajsdf243o8729");

            // Zip the bytes
            /*MemoryStream zippedMemStream = new MemoryStream();
            System.IO.Compression.GZipStream zipStream = new System.IO.Compression.GZipStream(zippedMemStream, System.IO.Compression.CompressionMode.Compress);

            byte[] resourceBytes = FileUtils.GetStreamBytes(memStream);
            zipStream.Write(resourceBytes, 0, resourceBytes.Length);

            // Now Encrypt the resource file
            zippedMemStream.Position = 0;
            resourceBytes = Utilities.CryptoUtils.Encrypt(FileUtils.GetStreamBytes(zippedMemStream), "asdlfa9sd879*Lasldflkajsdf243o8729");

            zippedMemStream.Close();
            zippedMemStream.Dispose();*/

            memStream.Close();
            memStream.Dispose();

            rw.Close();
            rw.Dispose();

            File.WriteAllBytes(filename, resourceBytes);
        }

        public T GetResource<T>(string resourceName)
        {
            System.Collections.IDictionaryEnumerator resEnum = resourceReader.GetEnumerator();

            while (resEnum.MoveNext())
            {
                if (resourceName == (string)resEnum.Key && resEnum.Value is T)
                    return (T)resEnum.Value;
            }

            return default(T);
        }
    }
}
