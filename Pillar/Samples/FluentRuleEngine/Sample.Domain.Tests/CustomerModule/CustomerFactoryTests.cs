namespace Sample.Domain.Tests.CustomerModule
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Sample.Domain.CustomerModule;

    [TestClass]
    public class CustomerFactoryTests : TestBase
    {
        [TestMethod]
        public void CreateCustomer_IsPreferredCustomer_GreetingIsSent()
        {
            var greetingIsSent = false;
            var mockFollowUpService = new Mock<IPreferredCustomerService>();
            mockFollowUpService.Setup(pfs => pfs.SendFormalGreeting(It.IsAny<Customer>())).Callback(() => greetingIsSent = true);
            Container.Configure( c => c.For<IPreferredCustomerService>().Use (mockFollowUpService.Object));

            var customerFactory = new CustomerFactory();
            var name = new Name("John", "Doe");
            customerFactory.CreateCustomer(name, Gender.Male, true);

            Assert.IsTrue(greetingIsSent);
        }
    }
}
