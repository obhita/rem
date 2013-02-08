namespace Pillar.Domain.Tests.Fixture.PersonFixture
{
    public class PhoneEntity : AbstractAggregateNode
    {
        private string _number;
        private string _type;
        private string _displayName;

        public PhoneEntity ( PersonEntity person )
        {
            Person = person;
        }

        public string Number
        {
            get { return _number; }
            set { ApplyPropertyChange ( ref _number, () => Number, value ); }
        }

        public string Type
        {
            get { return _type; }
            set { ApplyPropertyChange ( ref _type, () => Type, value ); }
        }

        public string DisplayName
        {
            get { return _displayName; }
            set { ApplyPropertyChange(ref _displayName, () => DisplayName, value); }
        }

        public PersonEntity Person { get; private set; }

        public override IAggregateRoot AggregateRoot
        {
            get { return Person; }
        }
    }
}