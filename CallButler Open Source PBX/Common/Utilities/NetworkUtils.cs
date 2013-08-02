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
using System.Net;

namespace WOSI.Utilities
{
    public class NetworkUtils
    {
        public static string GetCurrentIpAddress()
        {
            string hostname = Dns.GetHostName();

            IPAddress[] ips = Dns.GetHostAddresses(hostname);

            string ip = String.Empty;

            if (ips.Length > 0)
            {
                ip = ips[0].ToString();
            }

            return ip;
        }

        public static IPAddress GetHostIPAddress(string hostName, System.Net.Sockets.AddressFamily addressFamily)
        {
            try
            {
                // If there's a port, remove it
                if (hostName.Contains(":"))
                    hostName = hostName.Split(':')[0];

                IPAddress parsedAddress;

                if (IPAddress.TryParse(hostName, out parsedAddress))
                    return parsedAddress;

                IPHostEntry ipEntry = Dns.GetHostEntry(hostName);

                if (ipEntry.AddressList.Length > 0)
                {
                    foreach (IPAddress address in ipEntry.AddressList)
                    {
                        if (address.AddressFamily == addressFamily && address.GetAddressBytes()[0] > 0)
                            return address;
                    }
                }
            }
            catch
            {
            }

            return new IPAddress(0);
        }

        public static void ParseHostString(string hostString, ref string hostName, ref int port)
        {
            hostName = hostString;

            if (hostString.Contains(":"))
            {
                string[] hostParts = hostString.Split(':');

                if (hostParts.Length == 2)
                {
                    hostName = hostParts[0];

                    int.TryParse(hostParts[1], out port);
                }
            }
        }
    }
}
