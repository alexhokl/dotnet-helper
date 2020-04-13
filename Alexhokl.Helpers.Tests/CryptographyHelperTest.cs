using System;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace Alexhokl.Helpers.Tests
{
    public class CryptographyHelperTest
    {
        [Fact]
        public void Des()
        {
            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {
                // retrieves the new pair of key and IV created
                // upon creation on the provider
                byte[] key = provider.Key;
                byte[] initializationVector = provider.IV;

                string secret = "This is the secret.";
                byte[] dataBytes = Encoding.UTF8.GetBytes(secret);

                byte[] encryptedData =
                    CryptographyHelper.Encrypt(
                        dataBytes, key, initializationVector, SymmetricEncryptionMethod.DES);
                byte[] decrptedData =
                    CryptographyHelper.Decrypt(
                        encryptedData, key, initializationVector, SymmetricEncryptionMethod.DES);

                string decryptedSecret = Encoding.UTF8.GetString(decrptedData);

                Assert.Equal(secret, decryptedSecret);

                encryptedData =
                    CryptographyHelper.Encrypt(
                        secret, key, initializationVector, SymmetricEncryptionMethod.DES, Encoding.UTF8);
                decryptedSecret =
                    CryptographyHelper.DecryptToString(
                        encryptedData, key, initializationVector, SymmetricEncryptionMethod.DES, Encoding.UTF8);

                Assert.Equal(secret, decryptedSecret);
            }
        }

        [Fact]
        public void TripleDes()
        {
            using (TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider())
            {
                // retrieves the new pair of key and IV created
                // upon creation on the provider
                byte[] key = provider.Key;
                byte[] initializationVector = provider.IV;

                string secret = "This is the secret.";
                byte[] dataBytes = Encoding.UTF8.GetBytes(secret);

                byte[] encryptedData =
                    CryptographyHelper.Encrypt(
                        dataBytes, key, initializationVector, SymmetricEncryptionMethod.TripleDES);
                byte[] decrptedData =
                    CryptographyHelper.Decrypt(
                        encryptedData, key, initializationVector, SymmetricEncryptionMethod.TripleDES);

                string decryptedSecret = Encoding.UTF8.GetString(decrptedData);

                Assert.Equal(secret, decryptedSecret);

                encryptedData =
                    CryptographyHelper.Encrypt(
                        secret, key, initializationVector, SymmetricEncryptionMethod.TripleDES, Encoding.UTF8);
                decryptedSecret =
                    CryptographyHelper.DecryptToString(
                        encryptedData, key, initializationVector, SymmetricEncryptionMethod.TripleDES, Encoding.UTF8);

                Assert.Equal(secret, decryptedSecret);
            }
        }

        [Fact]
        public void Aes()
        {
            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                // retrieves the new pair of key and IV created
                // upon creation on the provider
                byte[] key = provider.Key;
                byte[] initializationVector = provider.IV;

                string secret = "This is the secret.";
                byte[] dataBytes = Encoding.UTF8.GetBytes(secret);

                byte[] encryptedData =
                    CryptographyHelper.Encrypt(
                        dataBytes, key, initializationVector, SymmetricEncryptionMethod.AES);
                byte[] decrptedData =
                    CryptographyHelper.Decrypt(
                        encryptedData, key, initializationVector, SymmetricEncryptionMethod.AES);

                string decryptedSecret = Encoding.UTF8.GetString(decrptedData);

                Assert.Equal(secret, decryptedSecret);

                encryptedData =
                    CryptographyHelper.Encrypt(
                        secret, key, initializationVector, SymmetricEncryptionMethod.AES, Encoding.UTF8);
                decryptedSecret =
                    CryptographyHelper.DecryptToString(
                        encryptedData, key, initializationVector, SymmetricEncryptionMethod.AES, Encoding.UTF8);

                Assert.Equal(secret, decryptedSecret);
            }
        }

        [Fact]
        public void Crc()
        {
            string secret = "This is the secret.";
            Assert.Equal(
                "+rX77g==",
                Convert.ToBase64String(
                    CryptographyHelper.GetHash(
                        Encoding.UTF8.GetBytes(secret),
                        HashMethod.CRC32)));
        }

        [Fact]
        public void Rijndael()
        {
            using (RijndaelManaged provider = new RijndaelManaged())
            {
                // retrieves the new pair of key and IV created
                // upon creation on the provider
                byte[] key = provider.Key;
                byte[] initializationVector = provider.IV;

                string secret = "This is the secret.";
                byte[] dataBytes = Encoding.UTF8.GetBytes(secret);

                byte[] encryptedData =
                    CryptographyHelper.Encrypt(
                        dataBytes, key, initializationVector, SymmetricEncryptionMethod.Rijndael);
                byte[] decrptedData =
                    CryptographyHelper.Decrypt(
                        encryptedData, key, initializationVector, SymmetricEncryptionMethod.Rijndael);

                string decryptedSecret = Encoding.UTF8.GetString(decrptedData);

                Assert.Equal(secret, decryptedSecret);

                encryptedData =
                    CryptographyHelper.Encrypt(
                        secret, key, initializationVector, SymmetricEncryptionMethod.Rijndael, Encoding.UTF8);
                decryptedSecret =
                    CryptographyHelper.DecryptToString(
                        encryptedData, key, initializationVector, SymmetricEncryptionMethod.Rijndael, Encoding.UTF8);

                Assert.Equal(secret, decryptedSecret);
            }
        }
    }
}



