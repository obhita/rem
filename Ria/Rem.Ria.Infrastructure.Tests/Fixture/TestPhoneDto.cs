using System.Runtime.Serialization;

namespace Rem.Ria.Infrastructure.Tests.Fixture
{
    [DataContract ( Name = "TestPhoneDto", Namespace = "Rem.Ria.Infrastructure.Tests" )]
    public class TestPhoneDto : TestDtoBase
    {
        private string _phoneNumber;

        [DataMember]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                RaisePropertyChanged ( () => PhoneNumber );
            }
        }
    }
}