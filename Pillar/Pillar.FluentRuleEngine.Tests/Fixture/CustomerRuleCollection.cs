namespace Pillar.FluentRuleEngine.Tests.Fixture
{
    public class CustomerRuleCollection : AbstractRuleCollection<Customer>
    {
        public CustomerRuleCollection ()
        {
            NewRule ( () => FirstAndLastNameMustBeDifferent ).When ( c => c.FirstName == c.LastName ).Then (
                ( cust, ctxt ) =>
                ctxt.RuleViolationReporter.Report (
                    new RuleViolation ( FirstAndLastNameMustBeDifferent, cust, "First and last names must be different", "FirstName", "LastName" ) ) );

            NewPropertyRule ( () => FirstNameRequired ).WithProperty ( customer => customer.FirstName ).AutoValidate ().NotNull ();
        }

        public IPropertyRule FirstNameRequired { get; private set; }

        public IRule FirstAndLastNameMustBeDifferent { get; private set; }

        public IPropertyRule MyEmptyPropertyRule { get; private set; }

        public IRule MyEmptyRule { get; private set; }
    }
}
