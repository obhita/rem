using System.Linq;
using Pillar.Common.Specification;

namespace Pillar.FluentRuleEngine.Tests.Fixture
{
    public class NoDuplicateZipCodeSpecification : ISpecification<Patient>
    {
        #region ISpecification<Patient> Members

        public bool IsSatisfiedBy ( Patient entity )
        {
            return entity.Addresses.Any ( address => entity.Addresses.Any ( address1 => address != address1 && address.Zipcode == address1.Zipcode ) );
        }

        #endregion
    }
}
