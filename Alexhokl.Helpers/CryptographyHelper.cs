using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using Alexhokl.Helpers.Cryptography;


namespace Alexhokl.Helpers
{
    public static class CryptographyHelper
    {
        #region public methods
        #region hash methods
        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static byte[] GetHash(byte[] data, HashMethod method)
        {
            using (var alogrithm = GetHashAlogrithm(method))
            {
                return alogrithm.ComputeHash(data);
            }
        }

        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static byte[] GetHash(Stream data, HashMethod method)
        {
            using (var alogirthm = GetHashAlogrithm(method))
            {
                return alogirthm.ComputeHash(data);
            }
        }

        /// <summary>
        /// Gets the hash in hexadecimal string.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static string GetHashInHexadecimalString(byte[] data, HashMethod method)
        {
            return GetHash(data, method).ToHexadecimalString();
        }

        /// <summary>
        /// Gets the hash in hexadecimal string.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static string GetHashInHexadecimalString(Stream data, HashMethod method)
        {
            return GetHash(data, method).ToHexadecimalString();
        }

        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="method">The hash method.</param>
        /// <param name="encoding">The encoding of parameter <c>data</c>.</param>
        /// <returns></returns>
        public static byte[] GetHash(string data, HashMethod method, Encoding encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            using (var alogrithm = GetHashAlogrithm(method))
            {
            return alogrithm.ComputeHash(encoding.GetBytes(data));
            }
        }

        /// <summary>
        /// Gets the hash in hexadecimal string.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="method">The method.</param>
        /// <param name="encoding">The encoding of parameter <c>data</c>.</param>
        /// <returns></returns>
        public static string GetHashInHexadecimalString(string data, HashMethod method, Encoding encoding)
        {
            return GetHash(data, method, encoding).ToHexadecimalString();
        }

        /// <summary>
        /// Gets the hash in base64 string.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static string GetHashInBase64String(byte[] data, HashMethod method)
        {
            return GetHash(data, method).ToBase64String();
        }

        /// <summary>
        /// Gets the hash in base64 string.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static string GetHashInBase64String(Stream data, HashMethod method)
        {
            return GetHash(data, method).ToBase64String();
        }

        /// <summary>
        /// Gets the hash in base64 string.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="method">The method.</param>
        /// <param name="encoding">The encoding of parameter <c>data</c>.</param>
        /// <returns></returns>
        public static string GetHashInBase64String(string data, HashMethod method, Encoding encoding)
        {
            return GetHash(data, method, encoding).ToBase64String();
        }
        #endregion

        #region encryption methods
        /// <summary>
        /// Encrypts the specified data using a symmetric key algorithm.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="initializationVector">The initialization vector.</param>
        /// <param name="method">The encryption method.</param>
        /// <returns></returns>
        public static byte[] Encrypt(
            byte[] data, byte[] key, byte[] initializationVector, SymmetricEncryptionMethod method)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            using (SymmetricAlgorithm provider = GetSymmetricEncryptionAlgorithm(method))
            {
                // assigned the specified key and IV as
                // a pair is created upon creation of the provider
                provider.Key = key;
                provider.IV = initializationVector;
                ICryptoTransform encryptor = provider.CreateEncryptor();

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                        return ms.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts the specified encrypted data using a symmetric key algorithm.
        /// </summary>
        /// <param name="encryptedData">The encrypted data.</param>
        /// <param name="key">The key.</param>
        /// <param name="initializationVector">The initialization vector.</param>
        /// <param name="method">The encryption method.</param>
        /// <returns></returns>
        public static byte[] Decrypt(
            byte[] encryptedData, byte[] key, byte[] initializationVector, SymmetricEncryptionMethod method)
        {
            using (SymmetricAlgorithm provider = GetSymmetricEncryptionAlgorithm(method))
            {
                // assigned the specified key and IV as
                // a pair is created upon creation of the provider
                provider.Key = key;
                provider.IV = initializationVector;
                ICryptoTransform decryptor = provider.CreateDecryptor();

                using (MemoryStream ms = new MemoryStream(encryptedData))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (MemoryStream os = new MemoryStream())
                        {
                            cs.CopyTo(os);
                            return os.ToArray();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Encrypts the specified data using a symmetric key algorithm.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="initializationVector">The initialization vector.</param>
        /// <param name="method">The method.</param>
        /// <param name="encoding">The encoding of parameter <c>data</c>.</param>
        /// <returns></returns>
        public static byte[] Encrypt(
            string data, byte[] key, byte[] initializationVector, SymmetricEncryptionMethod method, Encoding encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            return Encrypt(encoding.GetBytes(data), key, initializationVector, method);
        }

        /// <summary>
        /// Decrypts he specified data to string using a symmetric key algorithm.
        /// </summary>
        /// <param name="encryptedData">The encrypted data.</param>
        /// <param name="key">The key.</param>
        /// <param name="initializationVector">The initialization vector.</param>
        /// <param name="method">The method.</param>
        /// <param name="encoding">The encoding of the string to be encoded.</param>
        /// <returns></returns>
        public static string DecryptToString(
            byte[] encryptedData, byte[] key, byte[] initializationVector, SymmetricEncryptionMethod method, Encoding encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            byte[] decryptedBytes = Decrypt(encryptedData, key, initializationVector, method);
            return encoding.GetString(decryptedBytes);
        }
        #endregion
        #endregion

        #region helper methods
        /// <summary>
        /// Gets the alogrithm.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="hashName">Name of the hash algorithm.</param>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException">
        /// </exception>
        private static HashAlgorithm GetHashAlogrithm(HashMethod method, string hashName = null)
        {
            if (hashName == null)
            {
                switch (method)
                {
                    case HashMethod.MD5:
                        return MD5CryptoServiceProvider.Create();
                    case HashMethod.SHA1:
                        return SHA1CryptoServiceProvider.Create();
                    case HashMethod.SHA256:
                        return SHA256CryptoServiceProvider.Create();
                    case HashMethod.SHA384:
                        return SHA384CryptoServiceProvider.Create();
                    case HashMethod.SHA512:
                        return SHA512CryptoServiceProvider.Create();
                    case HashMethod.CRC32:
                        return new CRC32();
                    default:
                        throw new NotSupportedException(
                            string.Format("Method [{0}] is not supported.", method.ToString()));
                }
            }
            else
            {
                switch (method)
                {
                    case HashMethod.MD5:
                        return MD5CryptoServiceProvider.Create(hashName);
                    case HashMethod.SHA1:
                        return SHA1CryptoServiceProvider.Create(hashName);
                    case HashMethod.SHA256:
                        return SHA256CryptoServiceProvider.Create(hashName);
                    case HashMethod.SHA384:
                        return SHA384CryptoServiceProvider.Create(hashName);
                    case HashMethod.SHA512:
                        return SHA512CryptoServiceProvider.Create(hashName);
                    default:
                        throw new NotSupportedException(
                            string.Format("Method [{0}] is not supported.", method.ToString()));
                }
            }
        }

        private static SymmetricAlgorithm GetSymmetricEncryptionAlgorithm(SymmetricEncryptionMethod method)
        {
            switch (method)
            {
                case SymmetricEncryptionMethod.AES:
                    return new AesCryptoServiceProvider();
                case SymmetricEncryptionMethod.DES:
                    return new DESCryptoServiceProvider();
                case SymmetricEncryptionMethod.TripleDES:
                    return new TripleDESCryptoServiceProvider();
                case SymmetricEncryptionMethod.Rijndael:
                    return new RijndaelManaged();
                default:
                    throw new NotSupportedException(
                        string.Format("Method [{0}] is not supported.", method.ToString()));
            }
        }

        /// <summary>
        /// Return an hexadecimal string representation of the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private static string ToHexadecimalString(this byte[] data)
        {
            return BitConverter.ToString(data);
        }

        /// <summary>
        /// To the base64 string.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private static string ToBase64String(this byte[] data)
        {
            return Convert.ToBase64String(data);
        }
        #endregion
    }

    public enum HashMethod
    {
        /// <summary>
        /// Message-Digest
        /// </summary>
        MD5,

        /// <summary>
        /// SHA-1
        /// </summary>
        SHA1,

        /// <summary>
        /// SHA-2 using 256 bits digest
        /// </summary>
        SHA256,

        /// <summary>
        /// SHA-2 using 384 bits digest
        /// </summary>
        SHA384,

        /// <summary>
        /// SHA-2 using 512 bits digest
        /// </summary>
        SHA512,

        /// <summary>
        /// Cyclic Redundancy Check (CRC32)
        /// </summary>
        CRC32
    }

    public enum SymmetricEncryptionMethod
    {
        AES,
        DES,
        TripleDES,
        Rijndael
    }
}
