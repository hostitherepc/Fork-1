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
using System.Text;
using System.Security.Cryptography;


namespace NET.Remoting.Channels
{
  public class RijndaelHelper
  {

    private string _key = null;

    public RijndaelHelper(string key)
    {
      _key = key;
    }

    public string Key
    {
      get
      {
        return _key;
      }
    }

    private string HashAlgorithm
    {
      get
      {
        return "MD5";
      }
    }

    private int KeySize
    {
      get
      {
        return 256;
      }
    }

    private string InitVector
    {
      get
      {
        return "@1B2c3D4e5F6h7H8";
      }
    }

    private string SaltValue
    {
      get
      {
        return "SS";
      }
    }

    private int PasswordIterations
    {
      get
      {
        return 1;
      }
    }

    public string Encrypt(string text)
    {
      CryptoStream cryptoStream = null;
      MemoryStream memoryStream = null;
      try
      {
        byte [] initVectorBytes = Encoding.ASCII.GetBytes(InitVector);
        byte [] saltValueBytes  = Encoding.ASCII.GetBytes(SaltValue);
        byte [] plainTextBytes  = Encoding.UTF8.GetBytes(text);

        PasswordDeriveBytes password = new PasswordDeriveBytes(Key, saltValueBytes, HashAlgorithm, PasswordIterations);
        
        byte [] keyBytes = password.GetBytes(KeySize / 8);
        
        RijndaelManaged symmetricKey = new RijndaelManaged();

        symmetricKey.Mode = CipherMode.CBC;
        
        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
        
        memoryStream = new MemoryStream();
                
        cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                
        cryptoStream.FlushFinalBlock();

        byte [] cipherTextBytes = memoryStream.ToArray();
                
        string cipherText = Convert.ToBase64String(cipherTextBytes);
        return cipherText;
      }
      finally
      {
        if( memoryStream != null )
        {
          memoryStream.Close();
        }
        if( cryptoStream != null )
        {
          cryptoStream.Close();
        }
      }
    }
    
    public string Decrypt(string text)
    {
      MemoryStream memoryStream = null;
      CryptoStream cryptoStream = null;
      try
      {
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(InitVector);
        byte[] saltValueBytes  = Encoding.ASCII.GetBytes(SaltValue);
        byte[] cipherTextBytes = Convert.FromBase64String(text);

        PasswordDeriveBytes password = new PasswordDeriveBytes(Key, saltValueBytes, HashAlgorithm, PasswordIterations);
        
        byte[] keyBytes = password.GetBytes(KeySize / 8);
        
        RijndaelManaged symmetricKey = new RijndaelManaged();
        
        symmetricKey.Mode = CipherMode.CBC;
        
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        
        memoryStream = new MemoryStream(cipherTextBytes);
        cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

        byte [] plainTextBytes = new byte[cipherTextBytes.Length];
        
        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                
        string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        return plainText;
      }
      finally
      {
        if( memoryStream != null )
        {
          memoryStream.Close();
        }
        if( cryptoStream != null )
        {
          cryptoStream.Close();
        }
      }
    }
  }
}
