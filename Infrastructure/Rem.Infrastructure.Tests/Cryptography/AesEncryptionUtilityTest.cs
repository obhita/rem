using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Rem.Infrastructure.Tests
{
    
    
    /// <summary>
    ///This is a test class for AesEncryptionUtilityTest and is intended
    ///to contain all AesEncryptionUtilityTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AesEncryptionUtilityTest
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        ///// <summary>
        /////A test for AesEncryptionUtility Constructor
        /////</summary>
        //[TestMethod()]
        //public void AesEncryptionUtilityConstructorTest()
        //{
        //   // "XeIg+rtPwbnMPDoVC7l2KNMgA8vDEMq21+w/Clxwp34=YkOHqd01DxGzh8KnOkp/ZQ==9paSpuP9ZwqiTrawW9muXQ=="
        //    string payload = "Claudio";
        //    X509CertificateUtility certificateUtility = new X509CertificateUtility (); // TODO: Initialize to an appropriate value
        //    AesEncryptionUtility target = new AesEncryptionUtility(certificateUtility);
        //    var result = target.Encrypt ( payload );

        //    System.Diagnostics.Debug.WriteLine ( result );

        //    var clearText = target.Decrypt ( result );


        //    Assert.AreEqual(clearText, result);



        //}

      
    }
}
