namespace Sample.Domain.Tests.CustomerModule
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Pillar.Domain.Event;

    using Sample.Domain.CustomerModule;

    [TestClass]
    public class CustomerRules : TestBase
    {
        #region Public Methods

        [TestMethod]
        public void RegisterForMaternitySales_IsFemale_ShouldRegisterCustomer()
        {
            var isRegistered = false;
            var preferredCustomerService = new Mock<IPreferredCustomerService>();
            this.Container.Configure(c => c.For<IPreferredCustomerService>().Use(preferredCustomerService.Object));
            preferredCustomerService.Setup(s => s.RegisterForMaternitySale(It.IsAny<Customer>())).Callback(() => isRegistered = true);

            var customer = new Customer(new Name("Jane", "Doe"), Gender.Female);
            customer.RegisterForMaternitySale();

            Assert.IsTrue(isRegistered);
        }

        [TestMethod]
        public void RegisterForMaternitySales_IsMale_ShouldFireRuleViolationEvent()
        {
            var rulesViolated = false;
            DomainEvent.Register<RuleViolationEvent>(e => rulesViolated = true);
            var customer = new Customer(new Name("John", "Doe"), Gender.Male);
            customer.RegisterForMaternitySale();

            Assert.IsTrue(rulesViolated);
        }

        [TestMethod]
        public void Rename_GivenSameName_ShouldFireRuleViolation()
        {
            var rulesViolated = false;
            DomainEvent.Register<RuleViolationEvent>(e => rulesViolated = true);

            var customer = new Customer(new Name("John", "Doe"), Gender.Male);
            customer.Rename(new Name("John", "Doe"));

            Assert.IsTrue(rulesViolated);
        }

        #endregion
    }
}