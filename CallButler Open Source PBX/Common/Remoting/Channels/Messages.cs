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
using System.Security.Cryptography;

namespace NET.Remoting.Channels
{
    internal class CommonHeaders
    {
        public static string ID
        {
            get
            {
                return "sc_TransactionID";
            }
        }

        public static string Transaction
        {
            get
            {
                return "sc_TransactionType";
            }
        }

        public static string PublicKey
        {
            get
            {
                return "sc_PublicKey";
            }
        }

        public static string SharedKey
        {
            get
            {
                return "sc_SharedKey";
            }
        }

        public static string SharedIV
        {
            get
            {
                return "sc_SharedIV";
            }
        }

        public static string CustomerID
        {
            get
            {
                return "sc_CustomerID";
            }
        }

        public static string ExtensionNumber
        {
            get
            {
                return "sc_ExtendionNumber";
            }
        }

        public static string Password
        {
            get
            {
                return "sc_Password";
            }
        }

        public static string Domain
        {
            get
            {
                return "sc_Domain";
            }
        }
    }

    internal enum SecureTransaction
    {
        Uninitialized = 0,
        SendingPublicKey,
        SendingSharedKey,
        SendingEncryptedMessage,
        SendingEncryptedResult,
        UnknownIdentifier
    }

    internal class ClientConnectionInfo : IDisposable
    {
        private Guid _transactID;
        private SymmetricAlgorithm _provider;
        private DateTime _lastUsed;
        private bool _disposed = false;
        //private bool _authenticated = false;
        
        public ClientConnectionInfo(Guid transactID, SymmetricAlgorithm provider)
        {
            _transactID = transactID;
            _provider = provider;
            _lastUsed = DateTime.UtcNow;
        }

        ~ClientConnectionInfo()
        {
            Dispose(false);
        }



        //public void Authenticate(string encryptedCustomerID, string encryptedExtensionNumber, string encryptedPassword)
        //{
        //    if (Authenticated == false)
        //    {
        //        RijndaelHelper h = new RijndaelHelper(System.Text.Encoding.ASCII.GetString(this.Provider.Key));

        //        int customerID = Convert.ToInt32(h.Decrypt(encryptedCustomerID));
        //        int ExtensionNumber = Convert.ToInt32(h.Decrypt(encryptedExtensionNumber));
        //        string password = h.Decrypt(encryptedPassword);
                
        //        //Authenticate user
                
        //        _authenticated = true;
        //    }
        //}
        
        //public bool Authenticated
        //{
        //    get
        //    {
        //        return _authenticated;
        //    }
        //}

        public void UpdateLastUsed()
        {
            CheckDisposed();
            _lastUsed = DateTime.UtcNow;
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_provider != null)
                {
                    ((IDisposable)_provider).Dispose();
                }

                if (disposing)
                {
                    GC.SuppressFinalize(this);
                }
            }
        }

        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("ClientConnectionInfo");
            }
        }

        public Guid TransactID
        {
            get
            {
                CheckDisposed();
                return _transactID;
            }
        }

        public SymmetricAlgorithm Provider
        {
            get
            {
                CheckDisposed();
                return _provider;
            }
        }

        public DateTime LastUsed
        {
            get
            {
                CheckDisposed();
                return _lastUsed;
            }
        }

      
    }

    public class ManagementAllowedEventArgs : System.EventArgs
    {
        private bool managementAllowed = false;
        private string ipAddress;
        private string extension;

        public ManagementAllowedEventArgs(string ipAddress, string extension)
        {
            this.ipAddress = ipAddress;
            this.extension = extension;
        }

        public string IpAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                ipAddress = value;
            }
        }

        public string Extension
        {
            get
            {
                return extension;
            }
        }

        public bool ManagementAllowed
        {
            get
            {
                return managementAllowed;
            }
            set
            {
                managementAllowed = value;
            }
        }



    }

    public class AuthenticationEventArgs : System.EventArgs
    {
        private int _customerID = 0;
        private int _extensionNumber = 0;
        private string _password;
        private bool _isAuthenticated = false;


        public AuthenticationEventArgs(int customerID, int extensionNumber, string password)
        {
            CustomerID = customerID;
            ExtensionNumber = extensionNumber;
            Password = password;
        }

        public int CustomerID
        {
            get
            {
                return _customerID;
            }
            private set
            {
                _customerID = value;
            }
        }

        public int ExtensionNumber
        {
            get
            {
                return _extensionNumber;
            }
            private set
            {
                _extensionNumber = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            private set
            {
                _password = value;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return _isAuthenticated;
            }

            set
            {
                _isAuthenticated = value;
            }
        }
    }
}
