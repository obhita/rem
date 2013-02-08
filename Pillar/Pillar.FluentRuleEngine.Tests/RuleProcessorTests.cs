using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.FluentRuleEngine.Rules;
using Pillar.FluentRuleEngine.Tests.Fixture;

namespace Pillar.FluentRuleEngine.Tests
{
    [TestClass]
    public class RuleProcessorTests
    {
        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void Process_GivenNullRule_ThrowsArgumentException ()
        {
            var customer = new Customer ();
            var ruleEngineContext = new RuleEngineContext<Customer> ( customer );
            var ruleProcessor = new RuleProcessor ();
            ruleProcessor.Process ( ruleEngineContext, null );
        }

        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void Process_GivenNullRuleEngineContext_ThrowsArgumentException ()
        {
            var mockRuleCollection = new MockRuleCollection ();
            var ruleProcessor = new RuleProcessor ();
            ruleProcessor.Process ( null, mockRuleCollection.MyMockRule );
        }

        [TestMethod]
        public void Process_GivenARule_CallsWhenClause ()
        {
            var customer = new Customer ();
            var ruleEngineContext = new RuleEngineContext<Customer> ( customer );
            var mockRuleCollection = new MockRuleCollection ();

            var ruleProcessor = new RuleProcessor ();
            ruleProcessor.Process ( ruleEngineContext, mockRuleCollection.MyMockRule );

            Assert.IsTrue ( mockRuleCollection.WhenClauseCalled );
        }

        [TestMethod]
        public void Process_WhenWhenClauseReturnsTrue_CallsThenClause ()
        {
            var customer = new Customer ();
            var ruleEngineContext = new RuleEngineContext<Customer> ( customer );
            var mockRuleCollection = new MockRuleCollection { WhenClauseReturns = true };

            var ruleProcessor = new RuleProcessor ();
            ruleProcessor.Process ( ruleEngineContext, mockRuleCollection.MyMockRule );

            Assert.IsTrue ( mockRuleCollection.ThenClauseCalled );
        }

        [TestMethod]
        public void Process_WhenWhenClauseReturnsTrue_DoesNotCallElseThenClause ()
        {
            var customer = new Customer ();
            var ruleEngineContext = new RuleEngineContext<Customer> ( customer );
            var mockRuleCollection = new MockRuleCollection { WhenClauseReturns = true };

            var ruleProcessor = new RuleProcessor ();
            ruleProcessor.Process ( ruleEngineContext, mockRuleCollection.MyMockRule );

            Assert.IsFalse ( mockRuleCollection.ElseThenClauseCalled );
        }

        [TestMethod]
        public void Process_WhenWhenClauseReturnsFalse_CallsElseThenClause ()
        {
            var customer = new Customer ();
            var ruleEngineContext = new RuleEngineContext<Customer> ( customer );
            var mockRuleCollection = new MockRuleCollection { WhenClauseReturns = false };

            var ruleProcessor = new RuleProcessor ();
            ruleProcessor.Process ( ruleEngineContext, mockRuleCollection.MyMockRule );

            Assert.IsTrue ( mockRuleCollection.ElseThenClauseCalled );
        }

        [TestMethod]
        public void Process_WhenWhenClauseReturnsFalse_DoesNotCallThenClause ()
        {
            var customer = new Customer ();
            var ruleEngineContext = new RuleEngineContext<Customer> ( customer );
            var mockRuleCollection = new MockRuleCollection { WhenClauseReturns = false };

            var ruleProcessor = new RuleProcessor ();
            ruleProcessor.Process ( ruleEngineContext, mockRuleCollection.MyMockRule );

            Assert.IsFalse ( mockRuleCollection.ThenClauseCalled );
        }

        [TestMethod]
        public void Process_WhenRuleIsSkipAndViolationError_RuleIsSkiped ()
        {
            var customer = new Customer ();
            var ruleEngineContext = new RuleEngineContext<Customer> ( customer );
            var mockRuleCollection = new MockRuleCollection { WhenClauseReturns = false };

            ruleEngineContext.RuleViolationReporter.Report ( new RuleViolation ( mockRuleCollection.MyMockRule, null, null ) );
            ( mockRuleCollection.MyMockRule as Rule ).AddShouldRunClause ( ctx => ctx.RuleViolationReporter.Count () == 0 );

            var ruleProcessor = new RuleProcessor ();
            ruleProcessor.Process ( ruleEngineContext, mockRuleCollection.MyMockRule );

            Assert.IsFalse ( mockRuleCollection.WhenClauseCalled );
        }

        #region Nested type: MockRuleCollection

        private class MockRuleCollection : AbstractRuleCollection<Customer>
        {
            public MockRuleCollection ()
            {
                NewRule ( () => MyMockRule ).When (
                    c =>
                        {
                            WhenClauseCalled = true;
                            return WhenClauseReturns;
                        } ).Then ( c => ThenClauseCalled = true ).ElseThen ( c => ElseThenClauseCalled = true );
            }

            public bool WhenClauseReturns { get; set; }

            public bool WhenClauseCalled { get; set; }

            public bool ThenClauseCalled { get; set; }

            public bool ElseThenClauseCalled { get; set; }

            public IRule MyMockRule { get; private set; }
        }

        #endregion
    }
}
