using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rem.Domain.Core.SecurityModule;

namespace Rem.Domain.Core.Tests.SecurityModule
{
    [TestClass]
    public class SystemRoleFactoryTests
    {
        [TestMethod]
        public void CreateSystemRole_GivenValidArguments_Succeeds()
        {
            var systemRoleRepository = new Mock<ISystemRoleRepository>();
            var systemRoleFactory = new SystemRoleFactory(
                systemRoleRepository.Object);

            var systemRole = systemRoleFactory.CreateSystemRole("RoleName", "Role description.", SystemRoleType.JobFunction );

            Assert.IsNotNull(systemRole);
        }

        [TestMethod]
        public void DestroySystemRole_GivenASystemRole_RoleIsMadeTransient()
        {
            var isTransient = false;
            var systemRoleRepository = new Mock<ISystemRoleRepository>();

            systemRoleRepository.Setup(sr => sr.MakeTransient(It.IsAny<SystemRole>())).Callback(() => isTransient = true);
            var systemRoleFactory = new SystemRoleFactory(systemRoleRepository.Object);
            var systemRole = new Mock<SystemRole>();

            systemRoleFactory.DestroySystemRole(systemRole.Object);

            Assert.IsTrue(isTransient);
        }
    }
}