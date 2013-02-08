using System;
using System.Collections.ObjectModel;

namespace Pillar.Common.Tests.Fixture
{
    public class TestPersonDto
    {
        public TestPersonDto ()
        {
            Phones = new ObservableCollection<TestPhoneDto> ();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime? NullableBirthDate { get; set; }

        public ObservableCollection<TestPhoneDto> Phones { get; set; }
    }
}
