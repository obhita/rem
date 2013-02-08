using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Rem.Domain.Clinical.PatientModule;

namespace Rem.Domain.ClinicalBilling.ContextMap.Tests
{
    [TestClass]
    public class PayorCoverageTranslatorTests
    {
        [TestMethod]
        public void Translate_PayorCoverageIsNull_ReturnsNullPayorCoverage()
        {
            // Setup
            var fixture = new Fixture ().Customize ( new AutoMoqCustomization () );

            var sut = fixture.CreateAnonymous<PayorCoverageTranslator> ();

            PayorCoverageCache payorCoverageCache = null;

            // Exercise
            var billingPayorCoverage = sut.Translate ( payorCoverageCache );

            // Verify
            Assert.IsNull ( billingPayorCoverage );
        }

        [TestMethod]
        public void Translate_PatientPhoneListIsNull_ReturnsNullPatientAccountPhoneList()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var sut = fixture.CreateAnonymous<PayorCoverageTranslator>();

            IList<PayorCoverageCache> payorCoverageCacheList = null;

            // Exercise
            var billingPayorCoverageList = sut.Translate(payorCoverageCacheList);

            // Verify
            Assert.IsNull(billingPayorCoverageList);
        }
    }
}
