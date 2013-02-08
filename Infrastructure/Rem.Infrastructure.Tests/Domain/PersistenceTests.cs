using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rem.Infrastructure.Tests.Domain
{
    [TestClass]
    public class PersistenceTests
    {
        [TestMethod]
        public void VerifyThatAllEntitiesArePersistable()
        {
            using (var nHibernateFixture = new NHibernateFixture())
            {
            }
        }
    }
}
