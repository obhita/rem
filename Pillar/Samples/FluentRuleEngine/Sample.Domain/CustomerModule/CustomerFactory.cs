namespace Sample.Domain.CustomerModule
{
    using Pillar.FluentRuleEngine;

    public class CustomerFactory : ICustomerFactory
    {
        #region Public Methods

        public Customer CreateCustomer(Name name, Gender gender, bool isPreferred = false)
        {
            var customer = new Customer(name, gender, isPreferred);

            var ruleEngine = RuleEngine<Customer>.CreateTypedRuleEngine();
            ruleEngine.ExecuteRuleSet(customer, "CustomerCreationRules");

            return customer;
        }

        #endregion
    }
}