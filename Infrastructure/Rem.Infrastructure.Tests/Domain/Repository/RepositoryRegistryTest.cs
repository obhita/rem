using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Domain.Repository;
using StructureMap;

namespace Rem.Infrastructure.Tests.Domain.Repository
{
    [TestClass]
    public class RepositoryRegistryTest
    {
        [TestMethod]
        public void RepositoryRegistry_AgencyRepositoryGetsRegisteredByConvention ()
        {
            IContainer container = ConfigureDIContainerWithRepositoryRegistry();

            var instance = container.GetInstance<IAgencyRepository> ();

            Assert.IsInstanceOfType (instance, typeof ( AgencyRepository ) );
        }


        [TestMethod]
        public void RepositoryRegistry_GivenDetailedEthnicityName_ReturnDetailedEthnicityByRaceFetcher()
        {
            IContainer container = ConfigureDIContainerWithRepositoryRegistry();

            var instance = container.GetInstance ( typeof ( ILookupValueByRelatedKeysFetcher ), "DetailedEthnicity" );

            Assert.IsInstanceOfType(instance, typeof(DetailedEthnicityByRaceFetcher));
        }


        #region Test Fixtures

        private IContainer ConfigureDIContainerWithRepositoryRegistry()
        {
            var mockSession = new Mock<ISession>();
            var mockSessionProvider = new Mock<ISessionProvider>();
            var mockKeywordsSearchService = new Mock<IKeywordsSearchService>();

            RepositoryRegistry target = new RepositoryRegistry();

            IContainer container = new Container(x =>
            {
                x.For<ISession>().Use(mockSession.Object);
                x.For<ISessionProvider>().Use(mockSessionProvider.Object);
                x.For<IKeywordsSearchService>().Use(mockKeywordsSearchService.Object);
                x.AddRegistry(target);
            });

            return container;
        }

        #endregion
    }
}
