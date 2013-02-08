namespace Pillar.Domain.Tests.Fixture.PersonFixture
{
    public class PersonEntity : AbstractAggregateRoot, IEventActionDummyInterface1
    {
        private int _age;
        private string _firstName;
        private string _lastName;
        private string _displayName;

        public string FirstName
        {
            get { return _firstName; }
            set { ApplyPropertyChange ( ref _firstName, () => FirstName, value ); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { ApplyPropertyChange ( ref _lastName, () => LastName, value ); }
        }

        public int Age
        {
            get { return _age; }
            set { ApplyPropertyChange ( ref _age, () => Age, value ); }
        }

        public string DisplayName
        {
            get { return _displayName; }
            set { ApplyPropertyChange ( ref _displayName, () => DisplayName, value  );}
        }

        public int OnAggregateChangedCount { get; set; }
    }
}