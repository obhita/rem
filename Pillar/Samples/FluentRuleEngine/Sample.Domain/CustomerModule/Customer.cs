namespace Sample.Domain.CustomerModule
{
    using Microsoft.Practices.ServiceLocation;

    using Pillar.Domain;
    using Pillar.Domain.Event;
    using Pillar.FluentRuleEngine;

    public class Customer
    {
        #region Constructors and Destructors

        internal Customer(Name name, Gender gender, bool isPreferred = false)
        {
            this.Name = name;
            this.Gender = gender;
            this.IsPreferred = isPreferred;
        }

        #endregion

        #region Public Properties

        public Address ShippingAddresss { get; private set; }

        public Gender Gender { get; private set; }

        public bool IsPreferred { get; private set; }

        public Name Name { get; private set; }

        public CreditCard CreditCard { get; private set; }

        #endregion

        #region Public Methods

        public void RegisterForMaternitySale()
        {
            var ruleEngine = RuleEngine<Customer>.CreateTypedRuleEngine();
            var result = ruleEngine.ExecuteRuleSet(this, "RegisterForMaternitySalesRules");

            if (!result.HasRuleViolation)
            {
                var followUpService = ServiceLocator.Current.GetInstance<IPreferredCustomerService>();
                followUpService.RegisterForMaternitySale(this);
            }
            else
            {
                DomainEvent.Raise(new RuleViolationEvent { RuleViolations = result.RuleViolations });
            }
        }

        public void Rename(Name name)
        {
            DomainRuleEngine.CreateRuleEngine(this, "RenameRules")
                .WithContext(name)
                .Execute(() => { this.Name = name; });
        }

        public void ReviseShippingAddress ( Address address )
        {
            DomainRuleEngine.CreateRuleEngine(this, "ShippingAddressRules")
                .WithContext(address)
                .Execute(() => { this.ShippingAddresss = address; });
        }

        public void ReviseCreditCard ( CreditCard creditCard )
        {
            DomainRuleEngine.CreateRuleEngine(this, "CreditCardRules")
                .WithContext(creditCard)
                .Execute( () => this.CreditCard = creditCard);
        }

        public void Purchase ( Product product )
        {
            DomainRuleEngine.CreateRuleEngine(this, "PurchaseProductRules")
                .WithContext(product)
                .Execute(() =>
                    {
                        var priceToCharge = product.Price;
                        
                        var creditCardService = ServiceLocator.Current.GetInstance<ICreditCardService>();
                        creditCardService.Charge(CreditCard, priceToCharge);

                        var shippingService = ServiceLocator.Current.GetInstance<IShippingService>();
                        shippingService.ShipProduct(product, ShippingAddresss);
                    });
        }

        #endregion
    }
}