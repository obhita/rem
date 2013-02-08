using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Domain.Primitives;
using Pillar.FluentRuleEngine;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.PatientModule.Rule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.Tests.PatientModule
{
    [TestClass]
    public class PayorCoverageRuleCollectionTests
    {
        [TestMethod]
        public void ExecuteRules_Valid_NoRuleViolations()
        {
            var repositoryMock = new Mock<IPayorCoverageCacheRepository> ();
            repositoryMock.Setup (
                rep => rep.AnyPayorCoverageTypeInEffectiveDateRange(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<PayorCoverageCacheType>(), It.IsAny<DateRange>())).Returns(
                    false );
            var ruleCollection = new PayorCoverageCacheRuleCollection ( repositoryMock.Object );
            var ruleEngine = new RuleEngine<PayorCoverageCache> ( ruleCollection );

            var payorCoverageTypeMock = new Mock<PayorCoverageCacheType> ();
            payorCoverageTypeMock.SetupGet ( pct => pct.WellKnownName ).Returns ( "Test" );

            var payorCoverageCache = new PayorCoverageCache (
                new Mock<Patient> ().Object,
                new Mock<PayorCache> ().Object,
                new DateRange ( DateTime.Now, null ),
                "12345",
                new PayorSubscriberCache (
                    new Address (
                        "Test",
                        null,
                        "City",
                        new Mock<CountyArea> ().Object,
                        new Mock<StateProvince> ().Object,
                        new Mock<Country> ().Object,
                        new PostalCode ( "12345" ) ),
                    null,
                    new Mock<AdministrativeGender> ().Object,
                    new PersonName ( string.Empty, "Fred", null, "Savage", null ),
                    new Mock<PayorSubscriberRelationshipCacheType> ().Object ),
                payorCoverageTypeMock.Object );

            var ruleContext = new RuleEngineContext<PayorCoverageCache> ( payorCoverageCache );
            ruleContext.WorkingMemory.AddContextObject ( payorCoverageCache.PayorCoverageCacheType );
            ruleContext.WorkingMemory.AddContextObject ( payorCoverageCache.EffectiveDateRange );

            var results = ruleEngine.ExecuteRules ( ruleContext );

            Assert.IsFalse ( results.HasRuleViolation );
        }

        [TestMethod]
        public void ExecuteRules_Duplicate_HasRuleViolations()
        {

            var payorCoverageTypeMock = new Mock<PayorCoverageCacheType>();
            payorCoverageTypeMock.SetupGet(pct => pct.WellKnownName).Returns("Test");

            var payorCoverageCache = new PayorCoverageCache(
                new Mock<Patient>().Object,
                new Mock<PayorCache>().Object,
                new DateRange(DateTime.Now, null),
                "12345",
                new PayorSubscriberCache(
                    new Address(
                        "Test",
                        null,
                        "City",
                        new Mock<CountyArea>().Object,
                        new Mock<StateProvince>().Object,
                        new Mock<Country>().Object,
                        new PostalCode("12345")),
                    null,
                    new Mock<AdministrativeGender>().Object,
                    new PersonName(string.Empty, "Fred", null, "Savage", null),
                    new Mock<PayorSubscriberRelationshipCacheType>().Object),
                payorCoverageTypeMock.Object);

            var repositoryMock = new Mock<IPayorCoverageCacheRepository>();
            repositoryMock.Setup(
                rep => rep.AnyPayorCoverageTypeInEffectiveDateRange(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<PayorCoverageCacheType>(), It.IsAny<DateRange>())).Returns(
                    true);
            var ruleCollection = new PayorCoverageCacheRuleCollection(repositoryMock.Object);
            var ruleEngine = new RuleEngine<PayorCoverageCache>(ruleCollection);

            var ruleContext = new RuleEngineContext<PayorCoverageCache>(payorCoverageCache);
            ruleContext.WorkingMemory.AddContextObject(payorCoverageCache.PayorCoverageCacheType);
            ruleContext.WorkingMemory.AddContextObject(payorCoverageCache.EffectiveDateRange);

            var results = ruleEngine.ExecuteRules(ruleContext);

            Assert.IsTrue(results.HasRuleViolation);
        }
    }
}
