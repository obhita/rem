using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Extension;
using Pillar.Common.Tests;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.Tests.AgencyModule
{
    [TestClass]
    public class StaffFactoryTests
    {
        [TestMethod]
        public void CreateStaff_GivenValidArguments_CreatesAStaff()
        {
            var staffRepository = new Mock<IStaffRepository>();
            var lookupValueRepository = CreateMockLookupRepository();

            var staffFactory = new StaffFactory(staffRepository.Object, lookupValueRepository);

            var agency = new Mock<Agency>();

            StaffProfile staffProfile =
                new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirst("Fred").WithLast("Smith"));

            Staff staff = staffFactory.CreateStaff(agency.Object, staffProfile);

            Assert.IsNotNull(staff);
        }

        [TestMethod]
        public void CreateStaff_GivenValidArguments_StaffIsMadePersistent()
        {
            bool isPersistent = false;

            var staffRepository = new Mock<IStaffRepository>();
            var lookupValueRepository = CreateMockLookupRepository();
            staffRepository.Setup(s => s.MakePersistent(It.IsAny<Staff>())).Callback(() => isPersistent = true);

            var staffFactory = new StaffFactory(staffRepository.Object, lookupValueRepository);

            var agency = new Mock<Agency>();

            StaffProfile staffProfile =
                new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirst("Fred").WithLast("Smith"));

            staffFactory.CreateStaff(agency.Object, staffProfile);

            Assert.IsTrue(isPersistent);
        }

        [TestMethod]
        public void CreateStaff_GivenValidArguments_StaffIsEditable()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                var staffRepository = new Mock<IStaffRepository>();
                var lookupValueRepository = CreateMockLookupRepository();
                var staffFactory = new StaffFactory(staffRepository.Object, lookupValueRepository);

                var agency = new Mock<Agency>();

                StaffProfile staffProfileWithoutMiddleName =
                    new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirst("Fred").WithLast("Smith"));

                var staff = staffFactory.CreateStaff(agency.Object, staffProfileWithoutMiddleName);

                StaffProfile staffProfileWithMiddleName =
                    new StaffProfileBuilder().WithStaffName(
                        new PersonNameBuilder().WithFirst("Fred").WithLast("Smith").WithMiddle("Middle Name"));

                staff.ReviseStaffProfile(staffProfileWithMiddleName);
            }
        }

        [TestMethod]
        public void CreateStaff_GivenValidArguments_StaffChecklistIsCreated()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                var staffRepository = new Mock<IStaffRepository>();
                var lookupValueRepository = new LookupValueRepositoryFixture();

                var staffFactory = new StaffFactory(staffRepository.Object, lookupValueRepository);

                var agency = new Mock<Agency>();

                StaffProfile staffProfile =
                    new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirst("Fred").WithLast("Smith"));

                var staff = staffFactory.CreateStaff(agency.Object, staffProfile);

                AssertStaffChecklistCreation(staff, lookupValueRepository);
            }
        }

        private void AssertStaffChecklistCreation(Staff staff, LookupValueRepositoryFixture lookupValueRepository)
        {
            Assert.IsTrue(staff.StaffChecklist.Count() == 2);

            staff.StaffChecklist.ForEach(
                sc =>
                    {
                        Assert.IsTrue(
                            lookupValueRepository.GetAllStaffChecklistItemTypes().Any(allSc => allSc.Name == sc.StaffChecklistItemType.Name));
                        Assert.IsTrue(!sc.CheckedIndicator);
                    });
        }

        [TestMethod]
        public void CreateStaff_GivenValidArguments_StaffEventIsCreated()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                var staffRepository = new Mock<IStaffRepository>();
                var lookupValueRepository = new LookupValueRepositoryFixture();

                var staffFactory = new StaffFactory(staffRepository.Object, lookupValueRepository);

                var agency = new Mock<Agency>();

                StaffProfile staffProfile =
                    new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirst("Fred").WithLast("Smith"));

                Staff staff = staffFactory.CreateStaff(agency.Object, staffProfile);

                AssertStaffEventCreation(staff, lookupValueRepository);
            }
        }

        private void AssertStaffEventCreation(Staff staff, LookupValueRepositoryFixture lookupValueRepository)
        {
            Assert.IsTrue(staff.StaffEvents.Count() == 2);

            staff.StaffEvents.ForEach(
                se =>
                Assert.IsTrue(lookupValueRepository.GetAllStaffEventTypes().Any(allStaffEvents => allStaffEvents.Name == se.StaffEventType.Name)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateStaff_NullAgency_ThrowsArgumentException()
        {
            var staffRepository = new Mock<IStaffRepository>();
            var lookupValueRepository = new Mock<ILookupValueRepository>();
            var staffFactory = new StaffFactory(staffRepository.Object, lookupValueRepository.Object);

            StaffProfile staffProfile =
                new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirst("Fred").WithLast("Smith"));

            staffFactory.CreateStaff(null, staffProfile);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateStaff_NullFirstName_ThrowsArgumentException()
        {
            var staffRepository = new Mock<IStaffRepository>();
            var lookupValueRepository = new Mock<ILookupValueRepository>();
            var staffFactory = new StaffFactory(staffRepository.Object, lookupValueRepository.Object);
            var agency = new Mock<Agency>();

            StaffProfile staffProfileWithoutFirstName =
                new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithLast("Smith"));

            staffFactory.CreateStaff(agency.Object, staffProfileWithoutFirstName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateStaff_NullLastName_ThrowsArgumentException()
        {
            var staffRepository = new Mock<IStaffRepository>();
            var lookupValueRepository = new Mock<ILookupValueRepository>();
            var staffFactory = new StaffFactory(staffRepository.Object, lookupValueRepository.Object);
            var agency = new Mock<Agency>();

            StaffProfile staffProfileWithoutLastName =
                new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirst("Fred"));

            staffFactory.CreateStaff(agency.Object, staffProfileWithoutLastName);
        }

        [TestMethod]
        public void DestroyStaff_GivenAStaff_StaffIsMadeTransient()
        {
            bool isTransient = false;

            var staffRepository = new Mock<IStaffRepository>();
            var lookupValueRepository = new Mock<ILookupValueRepository>();
            staffRepository.Setup(s => s.MakeTransient(It.IsAny<Staff>())).Callback(() => isTransient = true);

            var staffFactory = new StaffFactory(staffRepository.Object, lookupValueRepository.Object);

            var staff = new Mock<Staff>();

            staffFactory.DestroyStaff(staff.Object);

            Assert.IsTrue(isTransient);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DestroyStaff_GivenANullStaff_ThrowsArgumentException()
        {
            var staffRepository = new Mock<IStaffRepository>();
            var lookupValueRepository = new Mock<ILookupValueRepository>();
            var staffFactory = new StaffFactory(staffRepository.Object, lookupValueRepository.Object);

            staffFactory.DestroyStaff(null);
        }

        private ILookupValueRepository CreateMockLookupRepository()
        {
            var lookupValueRepository = new Mock<ILookupValueRepository>();
            lookupValueRepository.Setup(l => l.GetAll(typeof(StaffChecklistItemType))).Returns(() => new List<LookupBase>());
            lookupValueRepository.Setup(l => l.GetAll(typeof(StaffEventType))).Returns(() => new List<LookupBase>());
            return lookupValueRepository.Object;
        }
    }
}