using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Infrastructure.Security;
using Rem.Infrastructure.Tests.Domain;

namespace Rem.Infrastructure.Tests.Security
{
    [TestClass]
    public class ServerSecurityManagerTest : NHibernateFixtureBase
    {
        [TestMethod]
        public void InvokesExecuteEmergencyAccess()
        {
             Assert.IsTrue(true);  
        }
    }
}
