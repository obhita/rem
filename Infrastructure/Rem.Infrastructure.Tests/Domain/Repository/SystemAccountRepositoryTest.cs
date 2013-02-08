using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Domain.Repository;

namespace Rem.Infrastructure.Tests.Domain.Repository
{
    [TestClass]
    public class SystemAccountRepositoryTest : NHibernateFixtureBase
    {
        [TestMethod]
        public void FindSystemAccountsByKeywords_GivenNonNullEmptySearchCriteria_RunsQueryWithoutException()
        {
            var sut = SetUpSut ();
            var searchCriteria = "Josh Smith";
            sut.FindSystemAccountsByKeyword(searchCriteria, 1, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindSystemAccountsByKeywords_GivenNonNullSearchCriteria_ThrowsException()
        {
            var sut = SetUpSut ();
            string searchCriteria = null;
            sut.FindSystemAccountsByKeyword(searchCriteria, 1, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindSystemAccountsByKeywords_GivenEmptySearchCriteria_ThrowsException()
        {
            var sut = SetUpSut ();
            var searchCriteria = "  ";
            sut.FindSystemAccountsByKeyword(searchCriteria, 1, 2);
        }

        [TestMethod]
        public void FindSystemAccountsByAdvancedSearch_GivenAllNonNullEmptyParameters_RunsQueryWithoutException()
        {
            var sut = SetUpSut ();
            sut.FindSystemAccountsByAdvancedSearch("first", "middle", "last", "user", 1, true, 1, 2 );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindSystemAccountsByAdvancedSearch_GivenAllNullEmptyParameters_ThrowsException()
        {
            var sut = SetUpSut ();
            sut.FindSystemAccountsByAdvancedSearch(null, null, null, null, null, null, 0, 0);
        }

        private SystemAccountRepository SetUpSut()
        {
            var sessionProviderMock = new Mock<ISessionProvider>();
            sessionProviderMock.Setup(x => x.GetSession()).Returns(Session);

            var sut = new SystemAccountRepository(sessionProviderMock.Object);
            return sut;
        }
    }
}
