using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.Infrastructure.Tests.Fixture
{
    [DataContract]
    public class TestPersonDto : TestDtoBase
    {
        private DateTime _birthDate;
        private ObservableCollection<LookupValueDto> _cars;
        private string _firstName;
        private string _lastName;
        private DateTime? _nullableBirthDate;
        private ObservableCollection<TestPhoneDto> _phones;

        public TestPersonDto ()
        {
            Phones = new ObservableCollection<TestPhoneDto> ();
            Cars = new ObservableCollection<LookupValueDto> ();
        }


        [DataMember]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged ( () => FirstName );
            }
        }


        [DataMember]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged ( () => LastName );
            }
        }


        [DataMember]
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                RaisePropertyChanged ( () => BirthDate );
            }
        }


        [DataMember]
        public DateTime? NullableBirthDate
        {
            get { return _nullableBirthDate; }
            set
            {
                _nullableBirthDate = value;
                RaisePropertyChanged ( () => NullableBirthDate );
            }
        }


        [DataMember]
        public ObservableCollection<TestPhoneDto> Phones
        {
            get { return _phones; }
            set
            {
                _phones = value;
                RaisePropertyChanged ( () => Phones );
            }
        }


        [DataMember]
        public ObservableCollection<LookupValueDto> Cars
        {
            get { return _cars; }
            set
            {
                _cars = value;
                RaisePropertyChanged ( () => Cars );
            }
        }
    }
}