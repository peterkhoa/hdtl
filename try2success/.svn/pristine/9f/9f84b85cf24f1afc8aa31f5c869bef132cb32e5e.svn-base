using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.IO;
using System.Security.Cryptography;

namespace Library
{
    public class Encryptor
    {
        private static string passPhrase = "Pas5pr@se";        // can be any string
        private static string saltValue = "s@1tValue";        // can be any string
        private static string hashAlgorithm = "SHA1";             // can be "MD5"
        private static int passwordIterations = 2;                  // can be any number
        private static string initVector = "@1B2c3A5e5F6g7H8"; // must be 16 bytes
        private static int keySize = 256;                // can be 192 or 128


        /// <summary>
        /// Enrypts the specified input string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>An encrypted string</returns>
        public static string Encrypt(string input)
        {
            if (input == "")
            {
                return "";
            }
            // Generate the key for encryption
            // Convert strings into byte arrays.
            byte[] initVectorBytes;
            initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            byte[] saltValueBytes;
            saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our plaintext into a byte array.
            byte[] plainTextBytes;
            plainTextBytes = Encoding.Unicode.GetBytes(input);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            PasswordDeriveBytes password;
            password = new PasswordDeriveBytes(passPhrase,
                                                             saltValueBytes,
                                                             hashAlgorithm,
                                                             passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes;
            keyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey;
            symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor;
            encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream;
            memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            CryptoStream cryptoStream;
            cryptoStream = new CryptoStream(memoryStream,
                                                         encryptor,
                                                         CryptoStreamMode.Write);
            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes;
            cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            //' Convert encrypted data into a base64-encoded string.
            string cipherText;
            cipherText = Convert.ToBase64String(cipherTextBytes);

            // Return encrypted string.
            return cipherText;
        }

        /// <summary>
        /// Decrypts the specified input string.
        /// </summary>
        /// <param name="key">The key string.</param>
        /// <param name="input">The input string.</param>
        /// <returns>A decrypted string</returns>
        public static string Decrypt(string input)
        {
            if (input == "")
            {
                return "";
            }
            byte[] initVectorBytes;
            initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            byte[] saltValueBytes;
            saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our ciphertext into a byte array.
            byte[] cipherTextBytes;
            cipherTextBytes = Convert.FromBase64String(input);

            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            PasswordDeriveBytes password;
            password = new PasswordDeriveBytes(passPhrase,
                                                            saltValueBytes,
                                                            hashAlgorithm,
                                                            passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes;
            keyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey;
            symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor;
            decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream;
            memoryStream = new MemoryStream(cipherTextBytes);

            // Define memory stream which will be used to hold encrypted data.
            CryptoStream cryptoStream;
            cryptoStream = new CryptoStream(memoryStream,
                                                        decryptor,
                                                        CryptoStreamMode.Read);

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            // Start decrypting.
            int decryptedByteCount;
            decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                                  0,
                                                                  plainTextBytes.Length);

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText;
            plainText = Encoding.Unicode.GetString(plainTextBytes,
                                                                  0,
                                                                  decryptedByteCount);

            // Return decrypted string.
            return plainText;
        }

    }
}
