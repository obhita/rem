using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Infrastructure.Tests.Service.DataTransferObject
{
    [TestClass]
    public class DataErrorInfoTests
    {
        [TestMethod]
        public void DataErrorInfo_ObjectLevelError_Succeeds ()
        {
            var dataErrorInfo = new DataErrorInfo ( "ErrorMessage", ErrorLevel.Error );
        }

        [TestMethod]
        public void DataErrorInfo_PropertyLevelError_Succeeds ()
        {
            var dataErrorInfo = new DataErrorInfo ( "ErrorMessage", ErrorLevel.Error, "PropertyName" );
        }

        [TestMethod]
        public void DataErrorInfo_CrossPropertyLevelError_Succeeds ()
        {
            var dataErrorInfo = new DataErrorInfo ( "ErrorMessage", ErrorLevel.Error, "PropertyName1", "PropertyName2" );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void DataErrorInfo_PropertyLevelWithEmptyPropertyNameError_ThrowsArgumentException ()
        {
            var dataErrorInfo = new DataErrorInfo ( "ErrorMessage", ErrorLevel.Error, "" );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void DataErrorInfo_PropertyLevelWithNullPropertyNameError_ThrowsArgumentException ()
        {
            string[] s = new string[] { null };
            var dataErrorInfo = new DataErrorInfo ( "ErrorMessage", ErrorLevel.Error, s );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void DataErrorInfo_CrossPropertyLevelWithNullPropertyNameError_ThrowsArgumentException ()
        {
            var dataErrorInfo = new DataErrorInfo ( "ErrorMessage", ErrorLevel.Error, "FirstName", "LastName", null );
        }

        [TestMethod]
        public void DataErrorInfo_ObjectLevelError_DataErrorInfoTypeIsObjectLevel()
        {
            var dataErrorInfo = new DataErrorInfo("ErrorMessage", ErrorLevel.Error);
            Assert.AreEqual ( dataErrorInfo.DataErrorInfoType, DataErrorInfoType.ObjectLevel );
        }

        [TestMethod]
        public void DataErrorInfo_PropertyLevelError_DataErrorInfoTypeIsPropertyLevel()
        {
            var dataErrorInfo = new DataErrorInfo("ErrorMessage", ErrorLevel.Error, "PropertyName");
            Assert.AreEqual(dataErrorInfo.DataErrorInfoType, DataErrorInfoType.PropertyLevel);
        }

        [TestMethod]
        public void DataErrorInfo_CrossPropertyLevelError_DataErrorInfoTypeIsCrossPropertyLevel()
        {
            var dataErrorInfo = new DataErrorInfo("ErrorMessage", ErrorLevel.Error, "PropertyName1", "PropertyName2");
            Assert.AreEqual(dataErrorInfo.DataErrorInfoType, DataErrorInfoType.CrossPropertyLevel);
        }

    }
}