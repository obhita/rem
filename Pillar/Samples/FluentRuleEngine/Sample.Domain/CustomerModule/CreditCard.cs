namespace Sample.Domain.CustomerModule
{
    public class CreditCard
    {
        #region Constructors and Destructors

        public CreditCard(CreditCardType creditCardType, string number)
        {
            this.CreditCardType = creditCardType;
            this.Number = number;
        }

        #endregion

        #region Public Properties

        public CreditCardType CreditCardType { get; private set; }

        public string Number { get; private set; }

        #endregion
    }
}