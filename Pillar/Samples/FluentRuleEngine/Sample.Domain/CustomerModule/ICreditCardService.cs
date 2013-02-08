namespace Sample.Domain.CustomerModule
{
    public interface ICreditCardService
    {
        void Charge(CreditCard creditCard, double priceToCharge);
    }
}
