namespace Pillar.FluentRuleEngine.Tests.Fixture
{
    public class Address
    {
        public Address ( string street1, string street2, string city, string state, string zipcode )
        {
            Street1 = street1;
            Street2 = street2;
            City = city;
            State = state;
            Zipcode = zipcode;
        }

        public string Street1 { get; private set; }
        public string Street2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zipcode { get; private set; }
    }
}
