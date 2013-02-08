using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Infrastructure.Tests.Service.DataTransferObject
{
    public class PersonDto : AbstractDataTransferObject 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
