namespace Sample.Introduction
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Pillar.FluentRuleEngine;

    using Sample.Domain.Tests;

    [TestClass]
    public class SectionCPropertyRules : TestBase
    {
        #region Example1 - Introduction to property validation rules

        [TestMethod]
        public void Example1Test()
        {
            var subject = new Example1Subject
            {
                Text = null
            };
            var ruleEngine = RuleEngine<Example1Subject>.CreateTypedRuleEngine();
            var result = ruleEngine.ExecuteAllRules(subject);

            Assert.IsTrue(result.HasRuleViolation);

            // The rule engine will let you know what object violated the rule
            var ruleViolation = result.RuleViolations.Single();
            Assert.AreSame(subject, ruleViolation.OffendingObject);

            // It will also let you know what properties are involved in the rule violation
            Assert.AreEqual("Text", ruleViolation.PropertyNames.Single());
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
            }

            public IPropertyRule TextCannotBeNull { get; private set; }
        }

        #endregion

        #endregion

        #region Example2 - Multiple constraints

        [TestMethod]
        public void Example2Test()
        {
            var subject = new Example2Subject
            {
                EmailAddress = "InvalidEmailAddress"
            };
            var ruleEngine = RuleEngine<Example2Subject>.CreateTypedRuleEngine();
            var result = ruleEngine.ExecuteAllRules(subject);

            // Since FavoriteFruit is not null but it isn't a valid email address
            Assert.IsTrue(result.HasRuleViolation);

            // The rule engine will let you know rule was violated
            var ruleViolation = result.RuleViolations.Single();
            Assert.AreEqual("MustBeValidEmailAddress", ruleViolation.Rule.Name);
        }

        #region Nested type: Example2Subject

        public class Example2Subject
        {
            public string EmailAddress { get; set; }
        }
        #endregion

        #region Nested type: Example2SubjectRules

        public class Example2SubjectRules : AbstractRuleCollection<Example2Subject>
        {
            private const string EmailRegex = @"\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b";

            public Example2SubjectRules()
            {
                NewPropertyRule(() => this.MustBeValidEmailAddress)
                    .WithProperty(s => s.EmailAddress)
                    .NotNull()
                    .MatchesRegex(EmailRegex);
            }

            public IPropertyRule MustBeValidEmailAddress { get; private set; }
        }

        #endregion

        #endregion

        #region Example3 - Inline constraint

        [TestMethod]
        public void Example3Test()
        {
            var subject = new Example3Subject
            {
                FavoriteFruit = "Apple"
            };
            var ruleEngine = RuleEngine<Example3Subject>.CreateTypedRuleEngine();
            var result = ruleEngine.ExecuteAllRules(subject);

            // Since FavoriteFruit is in the list of approved fruits there is no rule violation
            Assert.IsFalse(result.HasRuleViolation);
        }

        #region Nested type: Example3Subject

        public class Example3Subject
        {
            public string FavoriteFruit { get; set; }
        }
        #endregion

        #region Nested type: Example3SubjectRules

        public class Example3SubjectRules : AbstractRuleCollection<Example3Subject>
        {
            private readonly string[] _approvedFruits = new[]{ "Apple", "Orange", "Pear"};

            public Example3SubjectRules()
            {
                NewPropertyRule(() => this.FavoriteFruitMustBeInList)
                    .WithProperty(s => s.FavoriteFruit)
                    .NotNull()
                    .Constrain(ff => _approvedFruits.Contains(ff));
            }

            public IPropertyRule FavoriteFruitMustBeInList { get; private set; }
        }

        #endregion

        #endregion

        #region Example4 - Collection Properties

        [TestMethod]
        public void Example4Test()
        {
            var subject = new Example4Subject();
            var example4Address1 = new Example4Address
                {
                    Street1 = "1 Elm Street",
                    Street2 = "Apt C",
                    City = "Orlando",
                    State = "FL",
                    Zipcode = "32801"
                };
                
            var example4Address2 = new Example4Address
                {
                    Street1 = "1 Elm Street",
                    Street2 = "Apt C",
                    City = "Orlando",
                    State = "FL",
                    Zipcode = "32801"
                };

            var example4Address3 = new Example4Address();

            subject.Addresses.Add(example4Address1);
            subject.Addresses.Add(example4Address2);
            subject.Addresses.Add(example4Address3);

            var ruleEngine = RuleEngine<Example4Subject>.CreateTypedRuleEngine();
            var result = ruleEngine.ExecuteAllRules(subject);

            Assert.IsTrue(result.HasRuleViolation);

            var ruleViolationCount = result.RuleViolations
                .Where(rv => object.ReferenceEquals(rv.OffendingObject, example4Address3))
                .Count();
            Assert.AreEqual(4, ruleViolationCount);
        }

        #region Nested type: Example4Subject
        public class Example4Address
        {
            public string Street1 { get; set; }
            public string Street2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zipcode { get; set; }
        }

        public class Example4Subject
        {
            public Example4Subject()
            {
                Addresses = new List<Example4Address>();
            }

            public IList<Example4Address> Addresses { get; set; }
        }
        #endregion

        #region Nested type: Example4SubjectRules

        public class Example4AddressRules : AbstractRuleCollection<Example4Address>
        {
            public Example4AddressRules()
            {
                NewPropertyRule(() => this.Street1CannotBeNull)
                    .WithProperty(a => a.Street1)
                    .NotNull();
                NewPropertyRule(() => this.CityCannotBeNull)
                    .WithProperty(a => a.City)
                    .NotNull();
                NewPropertyRule(() => this.StateCannotBeNull)
                    .WithProperty(a => a.State)
                    .NotNull();
                NewPropertyRule(() => this.ZipcodeCannotBeNull)
                    .WithProperty(a => a.Zipcode)
                    .NotNull();
            }

            public IPropertyRule Street1CannotBeNull { get; private set; }
            public IPropertyRule CityCannotBeNull { get; private set; }
            public IPropertyRule StateCannotBeNull { get; private set; }
            public IPropertyRule ZipcodeCannotBeNull { get; private set; }
        }

        public class Example4SubjectRules : AbstractRuleCollection<Example4Subject>
        {
            public Example4SubjectRules()
            {
                NewCollectionPropertyRule(() => this.AddressesMustMatchAddressRules)
                    .WithProperty(s => s.Addresses)
                    .WithRuleCollection(new Example4AddressRules());
            }

            public ICollectionPropertyRule AddressesMustMatchAddressRules { get; private set; }
        }
        #endregion

        #endregion

        #region Example5 - Complex Properties

        [TestMethod]
        public void Example5Test()
        {
            var subject = new Example5Subject
                {
                    Address = new Example5Address
                        {

                            Street1 = "1 Elm Street",
                            Street2 = "Apt C",
                            City = "Orlando",
                            State = "FL",
                            Zipcode = "32801"
                        }
                };
            
            var ruleEngine = RuleEngine<Example5Subject>.CreateTypedRuleEngine();
            var result = ruleEngine.ExecuteAllRules(subject);

            Assert.IsFalse(result.HasRuleViolation);
        }

        #region Nested type: Example5Subject
        public class Example5Address
        {
            public string Street1 { get; set; }
            public string Street2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zipcode { get; set; }
        }

        public class Example5Subject
        {
            public Example5Address Address { get; set; }
        }
        #endregion

        #region Nested type: Example5SubjectRules

        public class Example5SubjectRules : AbstractRuleCollection<Example5Subject>
        {
            public Example5SubjectRules()
            {
                NewPropertyRule(() => this.AddressStreet1CannotBeNull)
                    .WithProperty(s => s.Address.Street1)
                    .NotNull();
                NewPropertyRule(() => this.AddressCityCannotBeNull)
                    .WithProperty(a => a.Address.City)
                    .NotNull();
                NewPropertyRule(() => this.AddressStateCannotBeNull)
                    .WithProperty(a => a.Address.State)
                    .NotNull();
                NewPropertyRule(() => this.AddressZipcodeCannotBeNull)
                    .WithProperty(a => a.Address.Zipcode)
                    .NotNull();
            }

            public IPropertyRule AddressStreet1CannotBeNull { get; private set; }
            public IPropertyRule AddressCityCannotBeNull { get; private set; }
            public IPropertyRule AddressStateCannotBeNull { get; private set; }
            public IPropertyRule AddressZipcodeCannotBeNull { get; private set; }
        }
        #endregion

        #endregion
    }
}