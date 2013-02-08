using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Tests;
using Pillar.Security.AccessControl;

namespace Pillar.Security.Tests.Security
{
    [TestClass]
    public class AccessControlManagerTests : TestFixtureBase
    {
        private IAccessControlManager _accessControlManager;
        private MockCurrentUserPermissionService _mockCurrentUserPermissionService;

        protected override void OnSetup ()
        {
            base.OnSetup ();
            _mockCurrentUserPermissionService = new MockCurrentUserPermissionService();
            _accessControlManager = new AccessControlManager ( _mockCurrentUserPermissionService );
            _accessControlManager.RegisterPermissionDescriptor ( new PermissionDescriptorFixture () );
        }

        [TestMethod]
        public void CanAccess_ResourceThatIsntRegistered_DeniesAccess ()
        {
            var canAccess = _accessControlManager.CanAccess (
                new ResourceRequest
                    {
                        "Foo"
                    } );
            Assert.IsFalse ( canAccess );
        }

        [TestMethod]
        public void CanAccess_ResourceWithPermissionButNoPermissionClaim_DeniesAccess()
        {
            _mockCurrentUserPermissionService.Permissions = PermissionFixtures.NoPermissionPermissions;
            var canAccess = _accessControlManager.CanAccess(
                new ResourceRequest
                    {
                        "Rem.Domain.Patient"
                    });
            Assert.IsFalse(canAccess);
        }

        [TestMethod]
        public void CanAccess_ResourceWithPermissionAndHasPermissionClaim_GrantsAccess()
        {
            _mockCurrentUserPermissionService.Permissions = PermissionFixtures.AllPermissionPermissions;
            var canAccess = _accessControlManager.CanAccess(
                new ResourceRequest
                    {
                        "Rem.Domain.Patient"
                    });
            Assert.IsTrue(canAccess);
        }

        [TestMethod]
        public void CanAccess_SubResourceWithPermissionButNoPermissionClaim_DeniesAccess()
        {
            _mockCurrentUserPermissionService.Permissions = PermissionFixtures.NoPermissionPermissions;
            var canAccess = _accessControlManager.CanAccess(
                new ResourceRequest
                    {
                        "Rem.Domain.Patient",
                        "RenamePatient"
                    });
            Assert.IsFalse(canAccess);
        }

        [TestMethod]
        public void CanAccess_SubResourceWithPermissionHasParentPermissionButNotSubPermission_DeniesAccess()
        {
            _mockCurrentUserPermissionService.Permissions = PermissionFixtures.PatientPermissionPermissionOnly;
            var canAccess = _accessControlManager.CanAccess(
                new ResourceRequest
                    {
                        "Rem.Domain.Patient",
                        "RenamePatient"
                    });
            Assert.IsFalse(canAccess);
        }

        [TestMethod]
        public void CanAccess_SubResourceWithPermissionHasSubPermission_DeniesAccess()
        {
            _mockCurrentUserPermissionService.Permissions = PermissionFixtures.AllPermissionPermissions;
            var canAccess = _accessControlManager.CanAccess(
                new ResourceRequest
                    {
                        "Rem.Domain.Patient",
                        "RenamePatient"
                    });
            Assert.IsTrue(canAccess);
        }
    }
}