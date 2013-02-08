using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Infrastructure.Tests.Service.Fixtures
{
    public class ValidatedObjectFixture : AbstractDataTransferObject
    {
        public string NotNullableProperty { get; set; }
    }
}
