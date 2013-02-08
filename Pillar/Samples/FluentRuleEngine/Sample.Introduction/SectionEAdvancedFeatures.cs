namespace Sample.Introduction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Pillar.Common.Specification;
    using Pillar.FluentRuleEngine;

    using Sample.Domain.Tests;

    [TestClass]
    public class SectionEAdvancedFeatures : TestBase
    {
        #region Example1 - Specification Rules

        [TestMethod]
        public void Example1Test()
        {
            var subject = new Example1Subject
                {
                    DueDate = DateTime.Today.AddDays(-1)
                };
            var ruleEngine = RuleEngine<Example1Subject>.CreateTypedRuleEngine();
            var results = ruleEngine.ExecuteAllRules(subject);

            Assert.IsFalse(results.HasRuleViolation);
        }

        #region Nested type: Example1Subject

        public class Example1Subject
        {
            public DateTime DueDate { get; set; }
        }

        public class OverDueSpecification : ISpecification<Example1Subject>
        {
            public bool IsSatisfiedBy(Example1Subject example1Subject)
            {
                return example1Subject.DueDate.Date > DateTime.Today;
            }
        }

        #endregion

        #region Nested type: Example1SubjectRules

        public class Example1SubjectRules : AbstractRuleCollection<Example1Subject>
        {
            public Example1SubjectRules()
            {
                NewSpecificationRule(() => MustBeOverdue)
                    .WithSpecification(new OverDueSpecification());
            }

            public ISpecificationRule MustBeOverdue { get; private set; }
        }

        #endregion

        #endregion

        #region Example2 - Rule Priority

        [TestMethod]
        public void Example2Test()
        {
            var milk = new Example2LineItem
            {
                Product = "Milk",
                Price = 4.5
            };
            var bread = new Example2LineItem
            {
                Product = "Bread",
                Price = 3.75
            };
            var eggs = new Example2LineItem
            {
                Product = "Eggs",
                Price = 2.55
            };

            var purchaseOrder = new Example2PurchaseOrder();
            purchaseOrder.LineItems.Add(milk);
            purchaseOrder.LineItems.Add(bread);
            purchaseOrder.LineItems.Add(eggs);

            var account = new Example2Account { Balance = 10 };

            var ruleEngine = RuleEngine<Example2PurchaseOrder>.CreateTypedRuleEngine();
            var ruleEngineContext = new RuleEngineContext<Example2PurchaseOrder>(purchaseOrder);
            ruleEngineContext.WorkingMemory.AddContextObject(account);

            var result = ruleEngine.ExecuteRules(ruleEngineContext);

            Assert.IsTrue(result.HasRuleViolation);
        }

        #region Nested type: Example2Subject

        public class Example2LineItem
        {
            public string Product { get; set; }

            public double Price { get; set; }
        }

        public class Example2PurchaseOrder
        {
            public Example2PurchaseOrder()
            {
                LineItems = new List<Example2LineItem>();
            }

            public IList<Example2LineItem> LineItems { get; set; }

            public double Sum { get; set; }
        }

        public class Example2Account
        {
            public double Balance { get; set; }
        }

        #endregion

        #region Nested type: Example2SubjectRules

        public class Example2SubjectRules : AbstractRuleCollection<Example2PurchaseOrder>
        {
            public Example2SubjectRules()
            {
                NewRule(() => this.CalculateSum)
                    .When(p => true)
                    .Then(p => p.Sum = (from li in p.LineItems select li.Price).Sum())
                    .WithPriority(100);

                NewRule(() => SumCannotExceedBalance)
                    .When((purchaseOrder, ruleEngineContext) =>
                        {
                            var account = ruleEngineContext.WorkingMemory.GetContextObject<Example2Account>();
                            return account.Balance < purchaseOrder.Sum;
                        })
                    .ThenReportRuleViolation(Resource.SectionE_PurchaseOrderSumCannotExceedBalance)
                    .WithPriority(200);
            }

            public IRule CalculateSum { get; private set; }
            public IRule SumCannotExceedBalance { get; private set; }
        }

        #endregion

        #endregion
        
        #region Example3 - Conditionally Run Groups of Rules

        [TestMethod]
        public void Example3Test()
        {
            var newCustomer = new Example3Customer { IsNewCustomer = true };
            var ruleEngine = RuleEngine<Example3Customer>.CreateTypedRuleEngine();
            ruleEngine.ExecuteAllRules(newCustomer);

            Assert.IsTrue(newCustomer.GreetingHasBeenSent);
            Assert.IsNotNull(newCustomer.Account);

            var oldCustomer = new Example3Customer { IsNewCustomer = false };
            ruleEngine.ExecuteAllRules(oldCustomer);

            Assert.IsFalse(oldCustomer.GreetingHasBeenSent);
        }

        #region Nested type: Example3Subject
        public class Example3Account {}

        public class Example3Customer
        {
            public bool IsNewCustomer { get; set; }

            public bool GreetingHasBeenSent { get; set; }

            public Example3Account Account { get; set; }

            public void SendGreeting()
            {
                GreetingHasBeenSent = true;
            }
        }

        #endregion

        #region Nested type: Example3SubjectRules

        public class Example3SubjectRules : AbstractRuleCollection<Example3Customer>
        {
            public Example3SubjectRules()
            {
                ShouldRunWhen( c => c.IsNewCustomer, () =>
                    {
                        NewRule(() => SendANewCustomerGreeting)
                            .When(c => !c.GreetingHasBeenSent)
                            .Then(c => c.SendGreeting());
                        NewRule(() => SetUpNewCustomerAccount)
                            .When(c => c.Account == null)
                            .Then(c => c.Account = new Example3Account());
                    });
            }

            public IRule SendANewCustomerGreeting { get; private set; }
            public IRule SetUpNewCustomerAccount { get; private set; }
        }

        #endregion

        #endregion
    }
}