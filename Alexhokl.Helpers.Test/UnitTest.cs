using System;
using System.Security.Cryptography;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Alexhokl.Helpers.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest
    {
        public UnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestToDelimitedString()
        {
            Assert.AreEqual<string>("test", StringHelper.ToDelimitedString("Test", "-"));
            Assert.AreEqual<string>("test-case", StringHelper.ToDelimitedString("TestCase", "-"));
            Assert.AreEqual<string>("my-test", StringHelper.ToDelimitedString("MyTest", "-"));
        }

        [TestMethod]
        public void TestGetNetworkAddress()
        {
            IPAddress address = IPAddress.Parse("192.168.1.100");
            IPAddress mask = IPAddress.Parse("255.255.255.0");

            Assert.AreEqual<string>(
                "192.168.1.0", 
                IPAddressHelper.GetNetworkAddress(address, mask).ToString());
        }

        [TestMethod]
        public void TestDes()
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

                Assert.AreEqual<string>(secret, decryptedSecret);

                encryptedData =
                    CryptographyHelper.Encrypt(
                        secret, key, initializationVector, SymmetricEncryptionMethod.DES, Encoding.UTF8);
                decryptedSecret =
                    CryptographyHelper.DecryptToString(
                        encryptedData, key, initializationVector, SymmetricEncryptionMethod.DES, Encoding.UTF8);

                Assert.AreEqual<string>(secret, decryptedSecret);
            }
        }

        [TestMethod]
        public void TestTripleDes()
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

                Assert.AreEqual<string>(secret, decryptedSecret);

                encryptedData =
                    CryptographyHelper.Encrypt(
                        secret, key, initializationVector, SymmetricEncryptionMethod.TripleDES, Encoding.UTF8);
                decryptedSecret =
                    CryptographyHelper.DecryptToString(
                        encryptedData, key, initializationVector, SymmetricEncryptionMethod.TripleDES, Encoding.UTF8);

                Assert.AreEqual<string>(secret, decryptedSecret);
            }
        }

        [TestMethod]
        public void TestAes()
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

                Assert.AreEqual<string>(secret, decryptedSecret);

                encryptedData =
                    CryptographyHelper.Encrypt(
                        secret, key, initializationVector, SymmetricEncryptionMethod.AES, Encoding.UTF8);
                decryptedSecret =
                    CryptographyHelper.DecryptToString(
                        encryptedData, key, initializationVector, SymmetricEncryptionMethod.AES, Encoding.UTF8);

                Assert.AreEqual<string>(secret, decryptedSecret);
            }
        }

        [TestMethod]
        public void TestCrc()
        {
            string secret = "This is the secret.";
            Assert.AreEqual<string>(
                "+rX77g==",
                Convert.ToBase64String(
                    CryptographyHelper.GetHash(
                        Encoding.UTF8.GetBytes(secret), 
                        HashMethod.CRC32)));
        }

        [TestMethod]
        public void TestRijndael()
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

                Assert.AreEqual<string>(secret, decryptedSecret);

                encryptedData =
                    CryptographyHelper.Encrypt(
                        secret, key, initializationVector, SymmetricEncryptionMethod.Rijndael, Encoding.UTF8);
                decryptedSecret =
                    CryptographyHelper.DecryptToString(
                        encryptedData, key, initializationVector, SymmetricEncryptionMethod.Rijndael, Encoding.UTF8);

                Assert.AreEqual<string>(secret, decryptedSecret);
            }
        }
    }
}
