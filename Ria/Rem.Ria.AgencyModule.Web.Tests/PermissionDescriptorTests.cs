using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Security.AccessControl;

namespace Rem.Ria.AgencyModule.Web.Tests
{
    [TestClass]
    public class PermissionDescriptorTests
    {
        [TestMethod]
        public void RegisterPermissionDescriptorShouldWork()
        {
            var currentUserPermissionService = new Mock<ICurrentUserPermissionService>();
            var accessControlManager = new AccessControlManager(currentUserPermissionService.Object);
            var permissionDescriptor = new PermissionDescriptor();
            accessControlManager.RegisterPermissionDescriptor(permissionDescriptor);
        }
    }
}
