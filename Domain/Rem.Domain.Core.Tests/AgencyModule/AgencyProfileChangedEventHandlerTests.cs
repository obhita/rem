using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.AgencyModule.Event;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.Tests.AgencyModule
{
    [TestClass]
    public class AgencyProfileChangedEventHandlerTests
    {
        [TestMethod]
        public void Handle_SameLegalName_NoAliasAdded()
        {
            AgencyName agencyName = new AgencyName("TestAgencyName", "DisplayName", "BusinessName");

            AgencyProfile agencyProfile = new AgencyProfile (
                new Mock<AgencyType> ().Object,
                agencyName,
                new DateRange ( DateTime.Now.AddDays ( -1 ), DateTime.Now ),
                "url",
                new Mock<GeographicalRegion> ().Object,
                "note" );

            var aliasAdded = false;

            var agencyMock = new Mock<Agency>();
            agencyMock.SetupGet(p => p.AgencyProfile).Returns(agencyProfile);
            agencyMock.Setup(a => a.AddAgencyAlias(It.IsAny<AgencyAlias>())).Callback(() => aliasAdded = true);
           
            var agencyProfileChangedEvent = new Mock<AgencyProfileChangedEvent>();
            agencyProfileChangedEvent.SetupGet(p => p.Agency).Returns(agencyMock.Object);
            agencyProfileChangedEvent.SetupGet(p => p.OldAgencyProfile).Returns(agencyProfile);

            new AgencyProfileChangedEventHandler().Handle(agencyProfileChangedEvent.Object);

            Assert.IsFalse(aliasAdded);
        }

        [TestMethod]
        public void Handle_DifferentLegalName_AliasAdded()
        {
            AgencyName oldAgencyName = new AgencyName("TestAgencyName", "DisplayName", "BusinessName");

            AgencyProfile oldAgencyProfile = new AgencyProfile(
                new Mock<AgencyType>().Object,
                oldAgencyName,
                new DateRange(DateTime.Now.AddDays(-1), DateTime.Now),
                "url",
                new Mock<GeographicalRegion>().Object,
                "note");

            AgencyName newAgencyName = new AgencyName("NewAgencyName", "DisplayName", "BusinessName");

            AgencyProfile newAgencyProfile = new AgencyProfile(
                new Mock<AgencyType>().Object,
                newAgencyName,
                new DateRange(DateTime.Now.AddDays(-1), DateTime.Now),
                "url",
                new Mock<GeographicalRegion>().Object,
                "note");

            var aliasAdded = false;

            var agencyMock = new Mock<Agency>();
            agencyMock.SetupGet(p => p.AgencyProfile).Returns(newAgencyProfile);
            agencyMock.Setup(a => a.AddAgencyAlias(It.IsAny<AgencyAlias>())).Callback(() => aliasAdded = true);

            var agencyProfileChangedEvent = new Mock<AgencyProfileChangedEvent>();
            agencyProfileChangedEvent.SetupGet(p => p.Agency).Returns(agencyMock.Object);
            agencyProfileChangedEvent.SetupGet(p => p.OldAgencyProfile).Returns(oldAgencyProfile);

            new AgencyProfileChangedEventHandler().Handle(agencyProfileChangedEvent.Object);

            Assert.IsTrue(aliasAdded);
        }
    }
}
