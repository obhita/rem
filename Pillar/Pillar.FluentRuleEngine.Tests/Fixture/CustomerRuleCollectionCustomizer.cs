namespace Pillar.FluentRuleEngine.Tests.Fixture
{
    public class CustomerRuleCollectionCustomizer : AbstractRuleCollection<Customer>, IRuleCollectionCustomizer<CustomerRuleCollection, Customer>
    {
        public CustomerRuleCollectionCustomizer ()
        {
            NewPropertyRule ( () => LastNameCannotBeNull ).WithProperty ( c => c.LastName ).AutoValidate ().NotNull ();
        }

        public IPropertyRule LastNameCannotBeNull { get; private set; }

        #region IRuleCollectionCustomizer<CustomerRuleCollection,Customer> Members

        public int Priority
        {
            get { return 0; }
        }

        public void Customize ( CustomerRuleCollection ruleCollection )
        {
            ruleCollection.FirstNameRequired.Disable ();

            ruleCollection.AddRules ( this );
        }

        #endregion
    }
}
