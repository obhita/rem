namespace Sample.Introduction
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Pillar.FluentRuleEngine;

    using Sample.Domain.Tests;

    [TestClass]
    public class SectionDRuleSets : TestBase
    {
        #region Example1 - Rule Sets

        [TestMethod]
        public void Example1Test()
        {
            var subject = new Example1Subject
            {
                Text = "Text"
            };
            var ruleEngine = RuleEngine<Example1Subject>.CreateTypedRuleEngine();
            var result = ruleEngine.ExecuteRuleSet(subject,"RuleSet1");

            Assert.IsFalse(result.HasRuleViolation);
        }

        #region Nested type: Example1Subject

        public class Example1Subject
        {
            public string Text { get; set; }
        }

        #endregion

        #region Nested type: Example1SubjectRules

        public class Example1SubjectRules : AbstractRuleCollection<Example1Subject>
        {
            public Example1SubjectRules()
            {
                NewPropertyRule(() => this.TextCannotBeNull)
                    .WithProperty(s => s.Text)
                    .NotNull();
                NewPropertyRule(() => this.TextMustBeNull)
                    .WithProperty(s => s.Text)
                    .Constrain(t => t == null);

                NewRuleSet(() => RuleSet1, TextCannotBeNull);
            }

            public IPropertyRule TextCannotBeNull { get; private set; }
            public IPropertyRule TextMustBeNull { get; private set; }

            public IRuleSet RuleSet1 { get; private set; }
        }

        #endregion

        #endregion
    }
}