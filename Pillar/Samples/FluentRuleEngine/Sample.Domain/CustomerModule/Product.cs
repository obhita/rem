namespace Sample.Domain.CustomerModule
{
    public class Product
    {
        #region Constructors and Destructors

        public Product(string description, double price)
        {
            this.Description = description;
            this.Price = price;
        }

        #endregion

        #region Public Properties

        public string Description { get; private set; }

        public double Price { get; private set; }

        #endregion
    }
}