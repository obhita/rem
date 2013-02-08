namespace Sample.Domain.CustomerModule
{
    public interface ICustomerFactory
    {
        Customer CreateCustomer(Name name, Gender gender, bool isPreferred = false);
    }
}
