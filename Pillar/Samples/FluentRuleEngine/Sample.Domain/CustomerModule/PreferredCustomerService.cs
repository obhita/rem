namespace Sample.Domain.CustomerModule
{
    public class PreferredCustomerService : IPreferredCustomerService
    {
        public void SendFormalGreeting(Customer customer)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Welcome {0}.", customer.Name));
        }

        public void RegisterForMaternitySale(Customer customer)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Registering {0} for maternity sale.", customer.Name));
        }
    }
}