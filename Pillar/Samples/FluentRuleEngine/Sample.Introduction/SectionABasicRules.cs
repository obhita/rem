namespace Sample.Introduction
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Pillar.FluentRuleEngine;

    using Sample.Domain.Tests;

    [TestClass]
    public class SectionABasicRules : TestBase
    {
        #region Example1 - When/Then

        [TestMethod]
        public void Example1Test()
        {
            var subject = new Example1Subject { IsTrue = true };
            var ruleEngine = RuleEngine<Example1Subject>.CreateTypedRuleEngine();
            ruleEngine.ExecuteAllRules(subject);

            Assert.AreEqual("hello world", subject.Greeting);
        }

        #region Nested type: Example1Subject

        public class Example1Subject
        {
            public bool IsTrue { get; set; }

            public string Greeting { get; set; }
        }

        #endregion

        #region Nested type: Example1SubjectRules

        public class Example1SubjectRules : AbstractRuleCollection<Example1Subject>
        {
            public Example1SubjectRules()
            {
                NewRule(() => this.WhenIsTrueThenSayHelloWorld)
                    .When(example1Subject => example1Subject.IsTrue)
                    .Then(example1Subject => example1Subject.Greeting = "hello world");
            }

            public IRule WhenIsTrueThenSayHelloWorld { get; private set; }
        }

        #endregion

        #endregion

        #region Example2 - When/Then/ElseThen

        [TestMethod]
        public void Example2Test()
        {
            var subject = new Example2Subject { IsTrue = false };
            var ruleEngine = RuleEngine<Example2Subject>.CreateTypedRuleEngine();
            ruleEngine.ExecuteAllRules(subject);

            Assert.AreEqual("goodbye cruel world", subject.Greeting);
        }

        #region Nested type: Example2Subject

        public class Example2Subject
        {
            public bool IsTrue { get; set; }

            public string Greeting { get; set; }
        }

        #endregion

        #region Nested type: Example2SubjectRules

        public class Example2SubjectRules : AbstractRuleCollection<Example2Subject>
        {
            public Example2SubjectRules()
            {
                NewRule(() => WhenIsPrimaryThenRecordResult)
                    .When(example2Subject => example2Subject.IsTrue)
                    .Then(example2Subject => example2Subject.Greeting = "hello world")
                    .ElseThen(example2Subject => example2Subject.Greeting = "goodbye cruel world");
            }

            public IRule WhenIsPrimaryThenRecordResult { get; private set; }
        }

        #endregion

        #endregion
        
        #region Example3 - Context Information

        [TestMethod]
        public void Example3Test()
        {
            var subject = new Example3Subject { IsTrue = true };
            var ruleEngine = RuleEngine<Example3Subject>.CreateTypedRuleEngine();

            var ruleEngineContext = new RuleEngineContext<Example3Subject>(subject);
            var example3ContextObject = new Example3ContextObject();
            ruleEngineContext.WorkingMemory.AddContextObject(example3ContextObject);

            ruleEngine.ExecuteRules(ruleEngineContext);

            Assert.AreEqual("hello world", example3ContextObject.Greeting);
        }

        #region Nested type: Example3Subject

        public class Example3Subject
        {
            public bool IsTrue { get; set; }
        }

        public class Example3ContextObject
        {
            public string Greeting { get; set; }
        }
        #endregion

        #region Nested type: Example3SubjectRules

        public class Example3SubjectRules : AbstractRuleCollection<Example3Subject>
        {
            public Example3SubjectRules()
            {
                NewRule(() => WhenIsTrueThenSayHelloWorld)
                    .When(example3Subject => example3Subject.IsTrue)
                    .Then( ( example3Subject, ruleEngineContext) =>
                        {
                            var example3ContextObject = ruleEngineContext.WorkingMemory.GetContextObject<Example3ContextObject>();
                            example3ContextObject.Greeting = "hello world";
                        });
            }

            public IRule WhenIsTrueThenSayHelloWorld { get; private set; }
        }

        #endregion

        #endregion
    }
}