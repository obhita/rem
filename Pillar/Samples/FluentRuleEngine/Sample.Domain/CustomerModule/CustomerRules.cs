namespace Sample.Domain.CustomerModule
{
    using Microsoft.Practices.ServiceLocation;

    using Pillar.FluentRuleEngine;

    public class CustomerRules : AbstractRuleCollection<Customer>
    {
        #region Constructors and Destructors

        public CustomerRules()
        {
            this.NewRule(() => this.PreferredCustomersShouldBeSentAFormalGreeting)
                .When(c => c.IsPreferred)
                .Then(
                    c =>
                        {
                            var followUpService = ServiceLocator.Current.GetInstance<IPreferredCustomerService>();
                            followUpService.SendFormalGreeting(c);
                        });

            this.NewRule(() => this.OnlyFemaleCustomersCanRegisterForMaternitySale)
                .When(c => c.Gender != Gender.Female)
                .ThenReportRuleViolation(Resource.OnlyFemaleCustomersCanRegisterForMaternitySaleMessage);

            this.NewRule(() => this.CannotRenameCustomerToSameName)
                .When((c, ctxt) =>
                    {
                        var name = ctxt.WorkingMemory.GetContextObject<Name>();
                        return c.Name == name;
                    })
                .ThenReportRuleViolation(Resource.CannotRenameCustomerToSameName);

            this.NewPropertyRule(() => this.ShippingAddressIsRequired)
                .WithProperty(c => c.ShippingAddresss)
                .NotNull();

            //this.NewRule(() => this.ShippingAddressStreet1IsRequired)
            //    .OnContextObject<Address>()
            //    .WithProperty(a => a.Street1)
            //    .NotNull();

            //this.NewRule(() => this.ShippingAddressCityIsRequired)
            //    .OnContextObject<Address>()
            //    .WithProperty(a => a.City)
            //    .NotNull();

            //this.NewRule(() => this.ShippingAddressStateIsRequired)
            //    .OnContextObject<Address>()
            //    .WithProperty(a => a.State)
            //    .NotNull();

            //this.NewRule(() => this.ShippingAddressZipcodeIsRequiredAndOnlySupports5Characters)
            //    .OnContextObject<Address>()
            //    .WithProperty(a => a.Zipcode)
            //    .NotNull()
            //    .MinLength(5)
            //    .MaxLength(5);

            this.NewRuleSet(() => this.CustomerCreationRules, this.PreferredCustomersShouldBeSentAFormalGreeting);

            this.NewRuleSet(() => this.RegisterForMaternitySalesRules, this.OnlyFemaleCustomersCanRegisterForMaternitySale);

            this.NewRuleSet(() => this.RenameRules, this.CannotRenameCustomerToSameName);

            this.NewRuleSet(() => this.PurchaseProductRules, this.ShippingAddressIsRequired);
        }

        #endregion

        //public IRule ShippingAddressCityIsRequired { get; private set; }

        //public IRule ShippingAddressStateIsRequired { get; private set; }

        //public IRule ShippingAddressStreet1IsRequired { get; private set; }

        //public IRule ShippingAddressZipcodeIsRequiredAndOnlySupports5Characters { get; private set; }

        #region Public Properties

        public IPropertyRule ShippingAddressIsRequired { get; private set; }

        public IRule CannotRenameCustomerToSameName { get; private set; }

        public IRuleSet CustomerCreationRules { get; private set; }

        public IRule OnlyFemaleCustomersCanRegisterForMaternitySale { get; private set; }

        public IRule PreferredCustomersShouldBeSentAFormalGreeting { get; private set; }

        public IRuleSet PurchaseProductRules { get; private set; }

        public IRuleSet RegisterForMaternitySalesRules { get; private set; }

        public IRuleSet RenameRules { get; private set; }

        #endregion
    }
}