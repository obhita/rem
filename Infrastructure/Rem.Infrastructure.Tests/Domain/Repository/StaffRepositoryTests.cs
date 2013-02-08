using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Domain.Repository;

namespace Rem.Infrastructure.Tests.Domain.Repository
{
    [TestClass]
    public class StaffRepositoryTests : NHibernateFixtureBase
    {
        [TestMethod]
        public void GetAllStaffByAgencyKey_GivenValidAgencyKey_ReturnsAllStaff ()
        {
            var repository = SetUpSut();
            var staffList = repository.GetAllStaffByAgencyKey ( 100 );
            Assert.AreEqual ( staffList.Count, 0 );
        }

        [TestMethod]
        public void GetAllStaffByLocationKeyAndAgencyTypeWellKnownName_GivenValidAgencyKeyAndSetOfCodes_ReturnsAllStaff()
        {
            var repository = SetUpSut();
            var staffList = repository.GetAllStaffByLocationKeyAndStaffTypeWellKnownName ( 100, StaffType.AllClinicalStaff );
            Assert.AreEqual ( staffList.Count, 0 );
        }

        private StaffRepository SetUpSut()
        {
            var sessionProviderMock = new Mock<ISessionProvider>();
            sessionProviderMock.Setup(x => x.GetSession()).Returns(Session);
            var keywordsSearchService = new Mock<IKeywordsSearchService> ();

            var sut = new StaffRepository(sessionProviderMock.Object, keywordsSearchService.Object);
            return sut;
        }
    }
}