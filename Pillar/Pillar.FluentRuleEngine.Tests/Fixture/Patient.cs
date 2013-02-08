using System;
using System.Collections.Generic;

namespace Pillar.FluentRuleEngine.Tests.Fixture
{
    public class Patient
    {
        public Patient ()
        {
            Addresses = new List<Address> ();
        }

        public Name Name { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsPregnant { get; set; }

        public List<Address> Addresses { get; set; }
    }
}
