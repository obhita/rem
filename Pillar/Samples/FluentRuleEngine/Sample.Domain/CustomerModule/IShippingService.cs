namespace Sample.Domain.CustomerModule
{
    public interface IShippingService
    {
        void ShipProduct(Product product, Address shippingAddresss);
    }
}
