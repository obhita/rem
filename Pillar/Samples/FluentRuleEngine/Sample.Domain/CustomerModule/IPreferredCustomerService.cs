namespace Sample.Domain.CustomerModule
{
    public interface IPreferredCustomerService
    {
        void SendFormalGreeting(Customer customer);

        void RegisterForMaternitySale(Customer customer);
    }
}
