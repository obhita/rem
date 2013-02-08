using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Security.AccessControl;

namespace Rem.Ria.PatientModule.Tests
{
    [TestClass]
    public class ClientPermissionDescriptorTests
    {
        [TestMethod]
        public void RegisterPermissionDescriptorShouldWork ()
        {
            var currentUserPermissionService = new Mock<ICurrentUserPermissionService> ();
            var accessControlManager = new AccessControlManager ( currentUserPermissionService.Object );
            var permissionDescriptor = new ClientPermissionDescriptor ();
            accessControlManager.RegisterPermissionDescriptor ( permissionDescriptor );
        }
    }
}