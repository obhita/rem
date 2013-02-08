using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Tests.Domain;

namespace Rem.Infrastructure.Tests.Service
{
    [TestClass]
    public class NHibernateInformationServiceTests : NHibernateFixtureBase
    {
        [TestInitialize]
        public void SetupTest()
        {
            base.OnSetup();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            base.OnTeardown();
        }

        [TestMethod]
        public void GetPropertyInformation_GivenValideInfo_ReturnsData()
        {
            var nHibernateInformationService = new NHibernateInformationService ( SessionFactory );
            var dbInfo = nHibernateInformationService.GetPropertyDatabaseInformation ( typeof(Patient), "Name.First" );
            Assert.IsFalse(string.IsNullOrWhiteSpace(dbInfo.Table));
            Assert.IsFalse(string.IsNullOrWhiteSpace(dbInfo.Column));
            Assert.IsFalse(string.IsNullOrWhiteSpace(dbInfo.DataType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPropertyInformation_GivenNullEntity_ThrowsArgumentException()
        {
            var nHibernateInformationService = new NHibernateInformationService(SessionFactory);
            nHibernateInformationService.GetPropertyDatabaseInformation(null, "Name");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPropertyInformation_GivenNullPropertyName_ThrowsArgumentException()
        {
            var nHibernateInformationService = new NHibernateInformationService(SessionFactory);
            nHibernateInformationService.GetPropertyDatabaseInformation(typeof(Patient), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPropertyInformation_GivenEmptyPropertyName_ThrowsArgumentException()
        {
            var nHibernateInformationService = new NHibernateInformationService(SessionFactory);
            nHibernateInformationService.GetPropertyDatabaseInformation(typeof(Patient), string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPropertyInformation_GivenWhitespacePropertyName_ThrowsArgumentException()
        {
            var nHibernateInformationService = new NHibernateInformationService(SessionFactory);
            nHibernateInformationService.GetPropertyDatabaseInformation(typeof(Patient), "   ");
        }
    }
}
