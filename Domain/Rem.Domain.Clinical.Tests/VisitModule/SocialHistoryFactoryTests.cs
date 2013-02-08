using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Pillar.Domain.Event;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.Tests.VisitModule
{
    [TestClass]
    public class SocialHistoryFactoryTests
    {
        private SocialHistoryFactory _factory;
        private Mock<ILookupValueRepository> _lookupValueRepository;
        private Mock<ISocialHistoryRepository> _repository;
        private Mock<Visit> _visit;

        [TestInitialize]
        public void Setup()
        {
            var activityType = new Mock<ActivityType> ();
            _lookupValueRepository = new Mock<ILookupValueRepository> ();
            _lookupValueRepository
                .Setup ( l => l.GetLookupByWellKnownName<ActivityType> ( It.IsAny<string> () ) )
                .Returns ( activityType.Object );

            _repository = new Mock<ISocialHistoryRepository> ();

            _factory = new SocialHistoryFactory (
                _repository.Object,
                _lookupValueRepository.Object );

            _visit = new Mock<Visit> ();
            var clinicalCase = new Mock<ClinicalCase> ();
            var patient = new Mock<Patient> ();

            _visit.Setup ( v => v.ClinicalCase ).Returns ( clinicalCase.Object );
            clinicalCase.Setup ( c => c.Patient ).Returns ( patient.Object );
        }

        [TestMethod]
        public void CreateSocialHistory_GivenValidArguments_CreatesSocialHistory ()
        {
            var entity = _factory.CreateSocialHistory ( _visit.Object );

            Assert.IsNotNull ( entity );
        }

        [TestMethod]
        public void CreateSocialHistory_GivenValidArguments_SocialHistoryIsEditable ()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);
                var entity = _factory.CreateSocialHistory(_visit.Object);

                var smokingStatus =
                    _lookupValueRepository.Object.GetLookupByWellKnownName<SmokingStatus>(It.IsAny<string>());
                var socialHistorySmoking = new SocialHistorySmoking(smokingStatus);
                entity.ReviseSocialHistorySmoking(socialHistorySmoking);
            }
        }

        [TestMethod]
        public void CreateSocialHistory_GivenValidArguments_SocialHistoryIsMadePersistent ()
        {
            var isPersistent = false;

            _repository
                .Setup ( v => v.MakePersistent ( It.IsAny<SocialHistory> () ) )
                .Callback ( () => isPersistent = true );

            _factory.CreateSocialHistory ( _visit.Object );

            Assert.IsTrue ( isPersistent );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateSocialHistory_NullVisit_ThrowsArgumentException ()
        {
            _factory.CreateSocialHistory ( null );
        }

        [TestMethod]
        public void DestorySocialHistory_GivenSocialHistory_SocialHistoryIsTransient ()
        {
            var isTransient = false;

            _repository
                .Setup ( v => v.MakeTransient ( It.IsAny<SocialHistory> () ) )
                .Callback ( () => isTransient = true );

            var socialHistory = new Mock<SocialHistory> ();

            socialHistory.Setup ( v => v.Visit ).Returns ( _visit.Object );

            _factory.DestroySocialHistory ( socialHistory.Object );

            Assert.IsTrue ( isTransient );
        }

        private static void SetupServiceLocatorFixture(ServiceLocatorFixture serviceLocatorFixture)
        {
            serviceLocatorFixture.StructureMapContainer.Configure (
                c => c.For<IDomainEventService> ().HybridHttpOrThreadLocalScoped ().Use<DomainEventService> () );
        }
    }
}