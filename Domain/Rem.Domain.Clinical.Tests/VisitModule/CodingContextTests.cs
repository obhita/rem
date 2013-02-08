using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.Clinical.Tests.VisitModule
{
    [TestClass]
    public class CodingContextTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReportError_GivenNull_ThrowsArgumentException()
        {
            var codingContext = new CodingContext ();
            codingContext.ReportError ( null );
        }

        [TestMethod]
        public void ReportError_GivenAMessageString_StoresMessageAndSetsHasErrorStatus()
        {
            const string errorMessage = "some error";
            var codingContext = new CodingContext();
            codingContext.ReportError(errorMessage);

            Assert.AreEqual ( codingContext.ErrorNote, errorMessage );
            Assert.AreEqual ( codingContext.CodingStatus, CodingStatus.HasError );
        }
    }
}
