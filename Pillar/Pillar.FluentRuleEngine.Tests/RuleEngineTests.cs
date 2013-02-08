using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.FluentRuleEngine.RuleSelectors;
using Pillar.FluentRuleEngine.Rules;
using Pillar.FluentRuleEngine.Tests.Fixture;

namespace Pillar.FluentRuleEngine.Tests
{
    [TestClass]
    public class RuleEngineTests
    {
        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void Constructor_GivenNullRuleCollection_ThrowsArgumentNullException ()
        {
            new RuleEngine<Patient> ( null );
        }

        [TestMethod]
        public void ExecuteRules_GivenPatientRuleCollection_AllRulesRun ()
        {
            var patient = new Patient { Name = new Name () };

            var patientRuleCollection = new PatientRuleCollection ();

            var runRuleCount = 0;
            foreach ( var patientRule in patientRuleCollection )
            {
                var ruleBuilder = new RuleBuilder<RuleEngineContext<Patient>, Patient> ( patientRule as Rule );
                ruleBuilder.Then ( p => runRuleCount++ ).ElseThen ( p => runRuleCount++ );
            }

            var ruleEngine = new RuleEngine<Patient> ( patientRuleCollection );

            var ruleEngineContext = new RuleEngineContext<Patient> ( patient );
            ruleEngineContext.WorkingMemory.AddContextObject ( new Name () );

            ruleEngine.ExecuteRules ( ruleEngineContext );

            Assert.AreEqual ( patientRuleCollection.Count (), runRuleCount );
        }

        [TestMethod]
        public void ExecuteRuleSetRules_GivenRuleSet_OnlyRulesInRuleSetAreRun ()
        {
            var patient = new Patient { Name = new Name () };

            var patientRuleCollection = new PatientRuleCollection ();

            var runRuleCount = 0;
            foreach ( var patientRule in patientRuleCollection )
            {
                var ruleBuilder = new RuleBuilder<RuleEngineContext<Patient>, Patient> ( patientRule as Rule );
                ruleBuilder.Then ( p => runRuleCount++ ).ElseThen ( p => runRuleCount++ );
            }

            var ruleEngine = new RuleEngine<Patient> ( patientRuleCollection );

            var ruleEngineContext = new RuleEngineContext<Patient> (
                patient, new SelectAllRulesInRuleSetSelector ( patientRuleCollection.PatientNameRuleSet ) );
            ruleEngineContext.WorkingMemory.AddContextObject ( new Name () );

            ruleEngine.ExecuteRules ( ruleEngineContext );

            Assert.AreEqual ( patientRuleCollection.PatientNameRuleSet.Count (), runRuleCount );
        }

        [TestMethod]
        public void ExecuteRules_WithDisabledRule_DisabledRuleIsNotRun ()
        {
            var patient = new Patient { Name = new Name () };

            var patientRuleCollection = new PatientRuleCollection ();

            patientRuleCollection.FirstNameCannotBeNull.Disable ();

            var ruleRun = false;
            var ruleBuilder = new RuleBuilder<RuleEngineContext<Patient>, Patient> ( patientRuleCollection.FirstNameCannotBeNull as Rule );
            ruleBuilder.Then ( p => ruleRun = true ).ElseThen ( p => ruleRun = true );

            var ruleEngine = new RuleEngine<Patient> ( patientRuleCollection );

            var ruleEngineContext = new RuleEngineContext<Patient> ( patient );
            ruleEngineContext.WorkingMemory.AddContextObject ( new Name () );

            ruleEngine.ExecuteRules ( ruleEngineContext );

            Assert.IsFalse ( ruleRun );
        }
    }
}
