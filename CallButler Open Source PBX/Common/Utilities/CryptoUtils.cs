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
using System.Text;
using System.Globalization;
using System.IO;

namespace WOSI.Utilities
{
	/// <summary>
	/// Summary description for CryptoUtils.
	/// </summary>
	public class CryptoUtils
	{
		private CryptoUtils()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        public static System.Security.SecureString CreateSecureString(string inputString)
        {
            System.Security.SecureString secureString = new System.Security.SecureString();

            foreach (Char character in inputString)
            {
                secureString.AppendChar(character);
            }

            return secureString;
        }

        public static string GetFileChecksum(string filename)
        {
            if (File.Exists(filename))
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

                byte[] fileBytes = FileUtils.GetFileBytes(filename);
                return GetByteChecksum(fileBytes); 
            }

            return "";
        }

        public static string GetByteChecksum(byte[] bytes)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] hashValue = md5.ComputeHash(bytes);

            return Convert.ToBase64String(hashValue); 
        }

		public static string CreateMD5Hash(string stringToHash)
		{
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] hashValue = md5.ComputeHash(System.Text.Encoding.Unicode.GetBytes(stringToHash));

			StringBuilder hexString = new StringBuilder(32);

			for(int index = 0; index < hashValue.Length; index++)
			{
				hexString.AppendFormat(CultureInfo.InvariantCulture, "{0:x2}", hashValue[index]);
			}

			return hexString.ToString();
		}

        public static string CreateASCIIMD5Hash(string stringToHash)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] hashValue = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(stringToHash));

            StringBuilder hexString = new StringBuilder(32);

            for (int index = 0; index < hashValue.Length; index++)
            {
                hexString.AppendFormat(CultureInfo.InvariantCulture, "{0:x2}", hashValue[index]);
            }

            return hexString.ToString();
        }

		public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV) 
		{ 
			// Create a MemoryStream to accept the encrypted bytes 
			MemoryStream ms = new MemoryStream(); 

			// Create a symmetric algorithm. 
			// We are going to use Rijndael because it is strong and
			// available on all platforms. 
			// You can use other algorithms, to do so substitute the
			// next line with something like 
			//      TripleDES alg = TripleDES.Create(); 
			Rijndael alg = Rijndael.Create(); 

			// Now set the key and the IV. 
			// We need the IV (Initialization Vector) because
			// the algorithm is operating in its default 
			// mode called CBC (Cipher Block Chaining).
			// The IV is XORed with the first block (8 byte) 
			// of the data before it is encrypted, and then each
			// encrypted block is XORed with the 
			// following block of plaintext.
			// This is done to make encryption more secure. 

			// There is also a mode called ECB which does not need an IV,
			// but it is much less secure. 
			alg.Key = Key; 
			alg.IV = IV; 

			// Create a CryptoStream through which we are going to be
			// pumping our data. 
			// CryptoStreamMode.Write means that we are going to be
			// writing data to the stream and the output will be written
			// in the MemoryStream we have provided. 
			CryptoStream cs = new CryptoStream(ms, 
				alg.CreateEncryptor(), CryptoStreamMode.Write); 

			// Write the data and make it do the encryption 
			cs.Write(clearData, 0, clearData.Length); 

			// Close the crypto stream (or do FlushFinalBlock). 
			// This will tell it that we have done our encryption and
			// there is no more data coming in, 
			// and it is now a good time to apply the padding and
			// finalize the encryption process. 
			cs.Close(); 

			// Now get the encrypted data from the MemoryStream.
			// Some people make a mistake of using GetBuffer() here,
			// which is not the right way. 
			byte[] encryptedData = ms.ToArray();

			return encryptedData; 
		} 

		// Encrypt a string into a string using a password 
		//    Uses Encrypt(byte[], byte[], byte[]) 

		public static string Encrypt(string clearText, string Password) 
		{ 
			// First we need to turn the input string into a byte array. 
			byte[] clearBytes = 
				System.Text.Encoding.Unicode.GetBytes(clearText); 

			// Then, we need to turn the password into Key and IV 
			// We are using salt to make it harder to guess our key
			// using a dictionary attack - 
			// trying to guess a password by enumerating all possible words. 
			PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, 
				new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
							   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76}); 

			// Now get the key/IV and do the encryption using the
			// function that accepts byte arrays. 
			// Using PasswordDeriveBytes object we are first getting
			// 32 bytes for the Key 
			// (the default Rijndael key length is 256bit = 32bytes)
			// and then 16 bytes for the IV. 
			// IV should always be the block size, which is by default
			// 16 bytes (128 bit) for Rijndael. 
			// If you are using DES/TripleDES/RC2 the block size is
			// 8 bytes and so should be the IV size. 
			// You can also read KeySize/BlockSize properties off
			// the algorithm to find out the sizes. 
			byte[] encryptedData = Encrypt(clearBytes, 
				pdb.GetBytes(32), pdb.GetBytes(16)); 

			// Now we need to turn the resulting byte array into a string. 
			// A common mistake would be to use an Encoding class for that.
			//It does not work because not all byte values can be
			// represented by characters. 
			// We are going to be using Base64 encoding that is designed
			//exactly for what we are trying to do. 
			return Convert.ToBase64String(encryptedData); 

		}
    
		// Encrypt bytes into bytes using a password 
		//    Uses Encrypt(byte[], byte[], byte[]) 

		public static byte[] Encrypt(byte[] clearData, string Password) 
		{ 
			// We need to turn the password into Key and IV. 
			// We are using salt to make it harder to guess our key
			// using a dictionary attack - 
			// trying to guess a password by enumerating all possible words. 
			PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, 
				new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
							   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76}); 

			// Now get the key/IV and do the encryption using the function
			// that accepts byte arrays. 
			// Using PasswordDeriveBytes object we are first getting
			// 32 bytes for the Key 
			// (the default Rijndael key length is 256bit = 32bytes)
			// and then 16 bytes for the IV. 
			// IV should always be the block size, which is by default
			// 16 bytes (128 bit) for Rijndael. 
			// If you are using DES/TripleDES/RC2 the block size is 8
			// bytes and so should be the IV size. 
			// You can also read KeySize/BlockSize properties off the
			// algorithm to find out the sizes. 
			return Encrypt(clearData, pdb.GetBytes(32), pdb.GetBytes(16)); 

		}

		// Encrypt a file into another file using a password 
		public static void Encrypt(string fileIn, 
			string fileOut, string Password) 
		{ 

			// First we are going to open the file streams 
			FileStream fsIn = new FileStream(fileIn, 
				FileMode.Open, FileAccess.Read); 
			FileStream fsOut = new FileStream(fileOut, 
				FileMode.OpenOrCreate, FileAccess.Write); 

			// Then we are going to derive a Key and an IV from the
			// Password and create an algorithm 
			PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, 
				new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
							   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76}); 

			Rijndael alg = Rijndael.Create(); 
			alg.Key = pdb.GetBytes(32); 
			alg.IV = pdb.GetBytes(16); 

			// Now create a crypto stream through which we are going
			// to be pumping data. 
			// Our fileOut is going to be receiving the encrypted bytes. 
			CryptoStream cs = new CryptoStream(fsOut, 
				alg.CreateEncryptor(), CryptoStreamMode.Write); 

			// Now will will initialize a buffer and will be processing
			// the input file in chunks. 
			// This is done to avoid reading the whole file (which can
			// be huge) into memory. 
			int bufferLen = 4096; 
			byte[] buffer = new byte[bufferLen]; 
			int bytesRead; 

			do 
			{ 
				// read a chunk of data from the input file 
				bytesRead = fsIn.Read(buffer, 0, bufferLen); 

				// encrypt it 
				cs.Write(buffer, 0, bytesRead); 
			} while(bytesRead != 0); 

			// close everything 

			// this will also close the unrelying fsOut stream
			cs.Close(); 
			fsIn.Close();     
		} 

		// Decrypt a byte array into a byte array using a key and an IV 
		public static byte[] Decrypt(byte[] cipherData, 
			byte[] Key, byte[] IV) 
		{
            try
            {
                // Create a MemoryStream that is going to accept the
                // decrypted bytes 
                MemoryStream ms = new MemoryStream();

                // Create a symmetric algorithm. 
                // We are going to use Rijndael because it is strong and
                // available on all platforms. 
                // You can use other algorithms, to do so substitute the next
                // line with something like 
                //     TripleDES alg = TripleDES.Create(); 
                Rijndael alg = Rijndael.Create();

                // Now set the key and the IV. 
                // We need the IV (Initialization Vector) because the algorithm
                // is operating in its default 
                // mode called CBC (Cipher Block Chaining). The IV is XORed with
                // the first block (8 byte) 
                // of the data after it is decrypted, and then each decrypted
                // block is XORed with the previous 
                // cipher block. This is done to make encryption more secure. 
                // There is also a mode called ECB which does not need an IV,
                // but it is much less secure. 
                alg.Key = Key;
                alg.IV = IV;

                // Create a CryptoStream through which we are going to be
                // pumping our data. 
                // CryptoStreamMode.Write means that we are going to be
                // writing data to the stream 
                // and the output will be written in the MemoryStream
                // we have provided. 
                CryptoStream cs = new CryptoStream(ms,
                    alg.CreateDecryptor(), CryptoStreamMode.Write);

                // Write the data and make it do the decryption 
                cs.Write(cipherData, 0, cipherData.Length);

                // Close the crypto stream (or do FlushFinalBlock). 
                // This will tell it that we have done our decryption
                // and there is no more data coming in, 
                // and it is now a good time to remove the padding
                // and finalize the decryption process. 
                cs.Close();

                // Now get the decrypted data from the MemoryStream. 
                // Some people make a mistake of using GetBuffer() here,
                // which is not the right way. 
                byte[] decryptedData = ms.ToArray();

                return decryptedData;
            }
            catch
            {
                return null;
            }
		}

		// Decrypt a string into a string using a password 
		//    Uses Decrypt(byte[], byte[], byte[]) 

		public static string Decrypt(string cipherText, string Password) 
		{
            try
            {
                // First we need to turn the input string into a byte array. 
                // We presume that Base64 encoding was used 
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                // Then, we need to turn the password into Key and IV 
                // We are using salt to make it harder to guess our key
                // using a dictionary attack - 
                // trying to guess a password by enumerating all possible words. 
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                    new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 
							   0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

                // Now get the key/IV and do the decryption using
                // the function that accepts byte arrays. 
                // Using PasswordDeriveBytes object we are first
                // getting 32 bytes for the Key 
                // (the default Rijndael key length is 256bit = 32bytes)
                // and then 16 bytes for the IV. 
                // IV should always be the block size, which is by
                // default 16 bytes (128 bit) for Rijndael. 
                // If you are using DES/TripleDES/RC2 the block size is
                // 8 bytes and so should be the IV size. 
                // You can also read KeySize/BlockSize properties off
                // the algorithm to find out the sizes. 
                byte[] decryptedData = Decrypt(cipherBytes,
                    pdb.GetBytes(32), pdb.GetBytes(16));

                // Now we need to turn the resulting byte array into a string. 
                // A common mistake would be to use an Encoding class for that.
                // It does not work 
                // because not all byte values can be represented by characters. 
                // We are going to be using Base64 encoding that is 
                // designed exactly for what we are trying to do. 
                return System.Text.Encoding.Unicode.GetString(decryptedData);
            }
            catch
            {
                return null;
            }
		}

		// Decrypt bytes into bytes using a password 
		//    Uses Decrypt(byte[], byte[], byte[]) 

		public static byte[] Decrypt(byte[] cipherData, string Password) 
		{ 
			// We need to turn the password into Key and IV. 
			// We are using salt to make it harder to guess our key
			// using a dictionary attack - 
			// trying to guess a password by enumerating all possible words. 
			PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, 
				new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
							   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76}); 

			// Now get the key/IV and do the Decryption using the 
			//function that accepts byte arrays. 
			// Using PasswordDeriveBytes object we are first getting
			// 32 bytes for the Key 
			// (the default Rijndael key length is 256bit = 32bytes)
			// and then 16 bytes for the IV. 
			// IV should always be the block size, which is by default
			// 16 bytes (128 bit) for Rijndael. 
			// If you are using DES/TripleDES/RC2 the block size is
			// 8 bytes and so should be the IV size. 

			// You can also read KeySize/BlockSize properties off the
			// algorithm to find out the sizes. 
			return Decrypt(cipherData, pdb.GetBytes(32), pdb.GetBytes(16)); 
		}

		// Decrypt a file into another file using a password 
		public static void Decrypt(string fileIn, 
			string fileOut, string Password) 
		{
            try
            {
                // First we are going to open the file streams 
                FileStream fsIn = new FileStream(fileIn,
                    FileMode.Open, FileAccess.Read);
                FileStream fsOut = new FileStream(fileOut,
                    FileMode.OpenOrCreate, FileAccess.Write);

                // Then we are going to derive a Key and an IV from
                // the Password and create an algorithm 
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                    new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
							   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
                Rijndael alg = Rijndael.Create();

                alg.Key = pdb.GetBytes(32);
                alg.IV = pdb.GetBytes(16);

                // Now create a crypto stream through which we are going
                // to be pumping data. 
                // Our fileOut is going to be receiving the Decrypted bytes. 
                CryptoStream cs = new CryptoStream(fsOut,
                    alg.CreateDecryptor(), CryptoStreamMode.Write);

                // Now will will initialize a buffer and will be 
                // processing the input file in chunks. 
                // This is done to avoid reading the whole file (which can be
                // huge) into memory. 
                int bufferLen = 4096;
                byte[] buffer = new byte[bufferLen];
                int bytesRead;

                do
                {
                    // read a chunk of data from the input file 
                    bytesRead = fsIn.Read(buffer, 0, bufferLen);

                    // Decrypt it 
                    cs.Write(buffer, 0, bytesRead);

                } while (bytesRead != 0);

                // close everything 
                cs.Close(); // this will also close the unrelying fsOut stream 
                fsIn.Close();
            }
            catch
            {
            }
		}
	}
}
