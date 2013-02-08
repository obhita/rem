using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.Tests.Fixture;

namespace Rem.Ria.Infrastructure.Tests.DataTransferObject
{
    [TestClass]
    public class DtoFactoryTest
    {
        /// <summary>
        /// Tests the fact that the Factory adds the INPC Mixin
        /// </summary>
        [TestMethod]
        public void CreateDto_CreatesINotifyPropertyChanged ()
        {
            IDtoFactory factory = new DtoFactory ();

            IDataTransferObject dto = factory.CreateDto ( typeof ( TestPersonDto ) ) as IDataTransferObject;

            Assert.IsInstanceOfType ( dto, typeof ( INotifyPropertyChanged ) );
        }

        [TestMethod]
        public void CreateDtoGeneric_CreatesINotifyPropertyChanged ()
        {
            IDtoFactory factory = new DtoFactory ();

            IDataTransferObject dto = factory.CreateDto<TestPersonDto> ();

            Assert.IsInstanceOfType ( dto, typeof ( INotifyPropertyChanged ) );
        }
    }
}
