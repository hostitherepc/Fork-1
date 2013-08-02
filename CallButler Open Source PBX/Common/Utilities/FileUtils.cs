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
using System.Windows.Forms;

namespace WOSI.Utilities
{
    public class FileUtils
    {
        public static string GetApplicationRelativePath(string relativePathFromAssembly)
        {
            return Path.GetFullPath(Path.GetDirectoryName(Application.ExecutablePath) + relativePathFromAssembly);
        }

        public static byte[] GetFileBytes(string filename)
        {
           if (File.Exists(filename))
            {
                FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Read);

                byte[] fileBytes = new byte[fs.Length];

                fs.Read(fileBytes, 0, (int)fs.Length);

                fs.Close();

                return fileBytes;
            }

            return null;
        }

        public static byte[] GetStreamBytes(Stream stream)
        {
            try
            {
                stream.Position = 0;
            }
            catch
            {
            }

            byte[] readBuffer = new byte[1024];
            List<byte> outputBytes = new List<byte>();

            int offset = 0;

            while (true)
            {
                int bytesRead = stream.Read(readBuffer, 0, readBuffer.Length);

                if (bytesRead == 0)
                {
                    break;
                }
                else if (bytesRead == readBuffer.Length)
                {
                    outputBytes.AddRange(readBuffer);
                }
                else
                {
                    byte[] tempBuf = new byte[bytesRead];

                    Array.Copy(readBuffer, tempBuf, bytesRead);

                    outputBytes.AddRange(tempBuf);

                    break;
                }

                offset += bytesRead;
            }

            return outputBytes.ToArray();
        }

        public static long GetDirectorySize(string directoryPath)
        {
            long size = 0;

            if (Directory.Exists(directoryPath))
            {
                DirectoryInfo dInfo = new DirectoryInfo(directoryPath);

                FileInfo[] files = dInfo.GetFiles();

                foreach (FileInfo file in files)
                {
                    size += file.Length;
                }
            }

            return size;
        }

        public static void SaveBytesToFile(string filename, byte[] bytesToWrite)
        {
            if (filename != null && filename.Length > 0 && bytesToWrite != null)
            {
                if (!Directory.Exists(Path.GetDirectoryName(filename)))
                    Directory.CreateDirectory(Path.GetDirectoryName(filename));

                FileStream file = File.Create(filename);

                file.Write(bytesToWrite, 0, bytesToWrite.Length);

                file.Close();
            }
        }
    }
}
