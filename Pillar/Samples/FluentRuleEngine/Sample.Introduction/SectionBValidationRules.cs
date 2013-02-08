namespace Sample.Introduction
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Pillar.FluentRuleEngine;

    using Sample.Domain.Tests;

    [TestClass]
    public class SectionBValidationRules : TestBase
    {
        #region Example1 - Introduction to validation rules

        [TestMethod]
        public void Example1Test()
        {
            var subject = new Example1Subject
                {
                    StartDate = new DateTime(2000, 1, 1 ),
                    EndDate = new DateTime(1999, 1, 1 )
                };
            var ruleEngine = RuleEngine<Example1Subject>.CreateTypedRuleEngine();
            var result = ruleEngine.ExecuteAllRules(subject);
            
            Assert.IsTrue(result.HasRuleViolation);

            var ruleViolation = result.RuleViolations.Single();
            Assert.AreEqual(Resource.SectionB_StartDateMustComeAfterEndDate, ruleViolation.Message);
        }

        #region Nested type: Example1Subject

        public class Example1Subject
        {
            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }
        }
        #endregion

        #region Nested type: Example1SubjectRules

        public class Example1SubjectRules : AbstractRuleCollection<Example1Subject>
        {
            public Example1SubjectRules()
            {
                NewRule(() => StartDateMustBeBeforeEndDate)
                    .When(example1Subject => example1Subject.StartDate > example1Subject.EndDate )
                    .Then((example1Subject, ruleEngineContext) =>
                        {
                            var ruleViolation = new RuleViolation(
                                StartDateMustBeBeforeEndDate,
                                example1Subject,
                                Resource.SectionB_StartDateMustComeAfterEndDate);
                            ruleEngineContext.RuleViolationReporter.Report(ruleViolation);
                        });
            }

            public IRule StartDateMustBeBeforeEndDate { get; private set; }
        }

        #endregion

        #endregion

        #region Example2 - Validation rules made easier

        [TestMethod]
        public void Example2Test()
        {
            var subject = new Example2Subject
            {
                StartDate = new DateTime(2000, 1, 1),
                EndDate = new DateTime(1999, 1, 1)
            };
            var ruleEngine = RuleEngine<Example2Subject>.CreateTypedRuleEngine();
            var result = ruleEngine.ExecuteAllRules(subject);

            Assert.IsTrue(result.HasRuleViolation);

            var ruleViolation = result.RuleViolations.Single();
            Assert.AreEqual(Resource.SectionB_StartDateMustComeAfterEndDate, ruleViolation.Message);
        }

        #region Nested type: Example2Subject

        public class Example2Subject
        {
            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }
        }
        #endregion

        #region Nested type: Example2SubjectRules

        public class Example2SubjectRules : AbstractRuleCollection<Example2Subject>
        {
            public Example2SubjectRules()
            {
                NewRule(() => StartDateMustBeBeforeEndDate)
                    .When(example2Subject => example2Subject.StartDate > example2Subject.EndDate)
                    .ThenReportRuleViolation(Resource.SectionB_StartDateMustComeAfterEndDate);
            }

            public IRule StartDateMustBeBeforeEndDate { get; private set; }
        }

        #endregion

        #endregion
    }
}