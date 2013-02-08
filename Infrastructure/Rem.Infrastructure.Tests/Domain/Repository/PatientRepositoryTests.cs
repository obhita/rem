using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Domain.Repository;

namespace Rem.Infrastructure.Tests.Domain.Repository
{
    [TestClass]
    public class PatientRepositoryTests
    {
        private NHibernateFixture _nhibernateFixture;
        private PatientRepository _sut;

        [TestInitialize]
        public void Setup()
        {
            _nhibernateFixture = new NHibernateFixture();

            var sessionProviderMock = new Mock<ISessionProvider>();
            sessionProviderMock.Setup(x => x.GetSession()).Returns(_nhibernateFixture.Session);

            _sut = new PatientRepository(sessionProviderMock.Object);
        }

        [TestCleanup]
        public void Teardown()
        {
            _nhibernateFixture.Dispose();
        }

        [TestMethod]
        public void FindPatientsByAdvancedSearch_GivenAllNonNullEmptyParameters_RunsQueryWithoutException ()
        {
            _sut.FindPatientsByAdvancedSearch("first", "middle", "last", "gender", DateTime.Now, "mmn", "identifier type", "identifier", "address", "city", "state", "suffix",
                                               "zip code", "uniqueIdentifier", 1, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FindPatientsByAdvancedSearch_GivenAllNullEmptyParameters_ThrowsException()
        {
            _sut.FindPatientsByAdvancedSearch ( null, null, null, null, null, null, null, null, null, null, null, null,
                                               null, null, 1, 2 );
        }
    }
}
