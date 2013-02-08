namespace Sample.Domain.CustomerModule
{
    public class AddressBuilder
    {
        #region Constants and Fields

        private string _city;

        private string _state;

        private string _street1;

        private string _street2;

        private string _zipcode;

        #endregion

        #region Public Methods

        public Address Build()
        {
            return new Address(this._street1, this._street2, this._city, this._state, this._zipcode);
        }

        public AddressBuilder WithCity(string city)
        {
            this._city = city;
            return this;
        }

        public AddressBuilder WithState(string state)
        {
            this._state = state;
            return this;
        }

        public AddressBuilder WithStreet1(string street1)
        {
            this._street1 = street1;
            return this;
        }

        public AddressBuilder WithStreet2(string street2)
        {
            this._street2 = street2;
            return this;
        }

        public AddressBuilder WithZipcode(string zipcode)
        {
            this._zipcode = zipcode;
            return this;
        }

        #endregion
    }
}