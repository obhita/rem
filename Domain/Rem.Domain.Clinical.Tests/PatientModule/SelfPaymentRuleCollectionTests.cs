using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.FluentRuleEngine;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.PatientModule.Rule;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.Tests.PatientModule
{
    [TestClass]
    public class SelfPaymentRuleCollectionTests
    {
        [TestMethod]
        public void ExecuteRules_ValidSelfPayment_NoRuleViolations()
        {
            var ruleEngine = new RuleEngine<SelfPayment> (new SelfPaymentRuleCollection ());
            var selfPayment = new SelfPayment (
                new Mock<Patient> ().Object,
                new Mock<Staff> ().Object,
                new Money ( new Currency ( "en-US" ), 20 ),
                new PaymentMethod (),
                DateTime.Now );
            var results = ruleEngine.ExecuteAllRules ( selfPayment );

            Assert.IsFalse ( results.HasRuleViolation );
        }

        [TestMethod]
        public void ExecuteRules_ZeroAmount_HasRuleViolation()
        {
            var ruleEngine = new RuleEngine<SelfPayment>(new SelfPaymentRuleCollection());
            var selfPayment = new SelfPayment(
                new Mock<Patient>().Object,
                new Mock<Staff>().Object,
                new Money(new Currency("en-US"), 0),
                new PaymentMethod(),
                DateTime.Now);
            var results = ruleEngine.ExecuteAllRules(selfPayment);

            Assert.IsTrue(results.HasRuleViolation);
        }

        [TestMethod]
        public void ExecuteRules_NegativeAmount_HasRuleViolation()
        {
            var ruleEngine = new RuleEngine<SelfPayment>(new SelfPaymentRuleCollection());
            var selfPayment = new SelfPayment(
                new Mock<Patient>().Object,
                new Mock<Staff>().Object,
                new Money(new Currency("en-US"), -10),
                new PaymentMethod(),
                DateTime.Now);
            var results = ruleEngine.ExecuteAllRules(selfPayment);

            Assert.IsTrue(results.HasRuleViolation);
        }

        [TestMethod]
        public void ExecuteRules_FutureDate_HasRuleViolation()
        {
            var ruleEngine = new RuleEngine<SelfPayment>(new SelfPaymentRuleCollection());
            var selfPayment = new SelfPayment(
                new Mock<Patient>().Object,
                new Mock<Staff>().Object,
                new Money(new Currency("en-US"), 10),
                new PaymentMethod(),
                DateTime.Today.AddDays ( 1 ));
            var results = ruleEngine.ExecuteAllRules(selfPayment);

            Assert.IsTrue(results.HasRuleViolation);
        }
    }
}
