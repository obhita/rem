using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Pillar.Domain.Event;
using Pillar.FluentRuleEngine;
using Rem.Domain.Core.SecurityModule;

namespace Rem.Domain.Core.Tests.SecurityModule
{
    [TestClass]
    public class SystemRoleValidationTests
    {
        /// <summary>
        /// Cannot grant a duplicate by wellknownName string a role. Validation failure event is raised. 
        /// </summary>
        [TestMethod]
        public void CreateSystemPermission_NoDuplicateGrantSystemPermissionWithContext_ValidationFailureEventIsRaised ()
        {
            using ( var serviceLocatorFixture = new ServiceLocatorFixture () )
            {
                // Setup
                SetupServiceLocatorFixture ( serviceLocatorFixture );

                // Register
                var eventRaised = false;
                DomainEvent.Register<RuleViolationEvent> ( p => eventRaised = true );

                var systemRoleRepositoryMock = new Mock<ISystemRoleRepository> ();
                var systemRoleFactory = new SystemRoleFactory ( systemRoleRepositoryMock.Object );

                // Exercise
                var systemRole = systemRoleFactory.CreateSystemRole ( "RoleName", "Role description.", SystemRoleType.Task );

                var systemPermission1 = new SystemPermission ( "WellKnownName1", "Permission display name.", "Permission description." );
                systemRole.GrantSystemPermission ( systemPermission1 );

                var systemPermission2 = new SystemPermission ( "WellKnownName1", "Permission display name", "Permission description." );
                systemRole.GrantSystemPermission ( systemPermission2 );

                // Verify
                Assert.IsTrue ( eventRaised );
            }
        }

        /// <summary>
        /// Cannot grant system permission to a job function role. Validation failure event is raised. 
        /// </summary>
        [TestMethod]
        public void CreateSystemPermission_CannotGrantSystemPermissionToJobFunction_ValidationFailureEventIsRaised ()
        {
            using ( var serviceLocatorFixture = new ServiceLocatorFixture () )
            {
                // Setup
                SetupServiceLocatorFixture ( serviceLocatorFixture );

                // Register
                var eventRaised = false;
                DomainEvent.Register<RuleViolationEvent> ( p => eventRaised = true );

                var systemRoleRepositoryMock = new Mock<ISystemRoleRepository> ();
                var systemRoleFactory = new SystemRoleFactory ( systemRoleRepositoryMock.Object );

                // Exercise
                var systemRole = systemRoleFactory.CreateSystemRole ( "RoleName", "Role description.", SystemRoleType.JobFunction );

                var systemPermission = new SystemPermission ( "WellKnownName", "MyPermission", "My permission description." );
                systemRole.GrantSystemPermission ( systemPermission );

                // Verify
                Assert.IsTrue ( eventRaised );
            }
        }

        /// <summary>
        /// Cannot grant system permission to a task group role. Validation failure event is raised. 
        /// </summary>
        [TestMethod]
        public void CreateSystemPermission_CannotGrantSystemPermissionToTaskGroup_ValidationFailureEventIsRaised ()
        {
            using ( var serviceLocatorFixture = new ServiceLocatorFixture () )
            {
                // Setup
                SetupServiceLocatorFixture ( serviceLocatorFixture );

                // Register
                var eventRaised = false;
                DomainEvent.Register<RuleViolationEvent> ( p => eventRaised = true );

                var systemRoleRepositoryMock = new Mock<ISystemRoleRepository> ();
                var systemRoleFactory = new SystemRoleFactory ( systemRoleRepositoryMock.Object );

                // Exercise
                var systemRole = systemRoleFactory.CreateSystemRole ( "RoleName", "Role description.", SystemRoleType.TaskGroup );

                var systemPermission = new SystemPermission ( "WellKnownName", "MyPermission", "My permission description." );
                systemRole.GrantSystemPermission ( systemPermission );

                // Verify
                Assert.IsTrue ( eventRaised );
            }
        }

        /// <summary>
        /// Can grant system permission to a task role. Validation failure event is not raised. 
        /// </summary>
        [TestMethod]
        public void CreateSystemPermission_CanGrantSystemPermissionToTask_ValidationFailureEventIsNotRaised ()
        {
            using ( var serviceLocatorFixture = new ServiceLocatorFixture () )
            {
                // Setup
                SetupServiceLocatorFixture ( serviceLocatorFixture );

                // Register
                var eventRaised = false;
                DomainEvent.Register<RuleViolationEvent> ( p => eventRaised = true );

                var systemRoleRepositoryMock = new Mock<ISystemRoleRepository> ();
                var systemRoleFactory = new SystemRoleFactory ( systemRoleRepositoryMock.Object );

                // Exercise
                var systemRole = systemRoleFactory.CreateSystemRole ( "RoleName", "Role description.", SystemRoleType.Task );

                var systemPermission = new SystemPermission ( "WellKnownName", "MyPermission", "My permission description." );
                systemRole.GrantSystemPermission ( systemPermission );

                // Verify
                Assert.IsFalse ( eventRaised );
            }
        }

        /// <summary>
        /// Cannot grant/assign a duplicate role by id to another role. Validation failure event is raised. 
        /// </summary>
        [TestMethod]
        public void CreateSystemRole_NoDuplicateGrantSystemRoleWithContext_ValidationFailureEventIsRaised()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                // Register
                var eventRaised = false;
                DomainEvent.Register<RuleViolationEvent>(p => eventRaised = true);

                var systemRoleRepositoryMock = new Mock<ISystemRoleRepository>();
                var systemRoleFactory = new SystemRoleFactory(systemRoleRepositoryMock.Object);

                // Exercise
                var systemRoleDuplicate1 = systemRoleFactory.CreateSystemRole("RoleName1", "Role description.", SystemRoleType.Task);

                var assignedToSystemRole = systemRoleFactory.CreateSystemRole("AssignedToRoleName", "Role description.", SystemRoleType.JobFunction);
                assignedToSystemRole.GrantSystemRole(systemRoleDuplicate1);
                assignedToSystemRole.GrantSystemRole(systemRoleDuplicate1);

                // Verify
                Assert.IsTrue(eventRaised);
            }
        }

        /// <summary>
        /// Can grant/assign a non duplicate role by id to a job function role. Validation failure event is not raised. 
        /// </summary>
        [TestMethod]
        public void CreateSystemRole_GrantNonDuplicateSystemRolesToJobFunction_ValidationFailureEventIsNotRaised()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                // Register
                var eventRaised = false;
                DomainEvent.Register<RuleViolationEvent>(p => eventRaised = true);

                // Exercise
                var systemRoleDuplicate1 = new Mock<SystemRole> ();
                systemRoleDuplicate1.SetupGet(p => p.Key).Returns(1);
                var systemRoleDuplicate2 = new Mock<SystemRole>();
                systemRoleDuplicate1.SetupGet(p => p.Key).Returns(2);

                var assignedToSystemRole = new Mock<SystemRole>();
                assignedToSystemRole.SetupGet(p => p.Key).Returns(3);

                assignedToSystemRole.Object.GrantSystemRole(systemRoleDuplicate1.Object);
                assignedToSystemRole.Object.GrantSystemRole(systemRoleDuplicate2.Object);

                // Verify
                Assert.IsFalse(eventRaised);
            }
        }

        /// <summary>
        /// Cannot grant a task group to a task. Validation failure event is raised. 
        /// </summary>
        [TestMethod]
        public void CreateSystemRole_CannotGrantTaskGroupToTask_ValidationFailureEventIsRaised ()
        {
            using ( var serviceLocatorFixture = new ServiceLocatorFixture () )
            {
                // Setup
                SetupServiceLocatorFixture ( serviceLocatorFixture );

                // Register
                var eventRaised = false;
                DomainEvent.Register<RuleViolationEvent> ( p => eventRaised = true );

                var systemRoleRepositoryMock = new Mock<ISystemRoleRepository> ();
                var systemRoleFactory = new SystemRoleFactory ( systemRoleRepositoryMock.Object );

                // Exercise
                var systemRoleTask = systemRoleFactory.CreateSystemRole ( "RoleNameSystemTask", "Role description.", SystemRoleType.Task );
                var systemRoleTaskGroup = systemRoleFactory.CreateSystemRole ( "RoleNameTaskGroup", "Role description.", SystemRoleType.TaskGroup );

                systemRoleTask.GrantSystemRole ( systemRoleTaskGroup );

                // Verify
                Assert.IsTrue ( eventRaised );
            }
        }

        /// <summary>
        /// Cannot grant a job function to a task group. Validation failure event is raised. 
        /// </summary>
        [TestMethod]
        public void CreateSystemRole_CannotGrantJobFunctionToTaskGroup_ValidationFailureEventIsRaised ()
        {
            using ( var serviceLocatorFixture = new ServiceLocatorFixture () )
            {
                // Setup
                SetupServiceLocatorFixture ( serviceLocatorFixture );

                // Register
                var eventRaised = false;
                DomainEvent.Register<RuleViolationEvent> ( p => eventRaised = true );

                var systemRoleRepositoryMock = new Mock<ISystemRoleRepository> ();
                var systemRoleFactory = new SystemRoleFactory ( systemRoleRepositoryMock.Object );

                // Exercise
                var systemRoleJobFunction = systemRoleFactory.CreateSystemRole (
                    "RoleNameJobFunction", "Role description.", SystemRoleType.JobFunction );
                var systemRoleTaskGroup = systemRoleFactory.CreateSystemRole ( "RoleNameTaskGroup", "Role description.", SystemRoleType.TaskGroup );

                systemRoleTaskGroup.GrantSystemRole ( systemRoleJobFunction );

                // Verify
                Assert.IsTrue ( eventRaised );
            }
        }

        /// <summary>
        /// Cannot grant a job function to a task. Validation failure event is raised. 
        /// </summary>
        [TestMethod]
        public void CreateSystemRole_CannotGrantJobFunctionToTask_ValidationFailureEventIsRaised ()
        {
            using ( var serviceLocatorFixture = new ServiceLocatorFixture () )
            {
                // Setup
                SetupServiceLocatorFixture ( serviceLocatorFixture );

                // Register
                var eventRaised = false;
                DomainEvent.Register<RuleViolationEvent> ( p => eventRaised = true );

                var systemRoleRepositoryMock = new Mock<ISystemRoleRepository> ();
                var systemRoleFactory = new SystemRoleFactory ( systemRoleRepositoryMock.Object );

                // Exercise
                var systemRoleJobFunction = systemRoleFactory.CreateSystemRole (
                    "RoleNameJobFunction", "Role description.", SystemRoleType.JobFunction );
                var systemRoleTask = systemRoleFactory.CreateSystemRole ( "RoleNameTask", "Role description.", SystemRoleType.Task );

                systemRoleTask.GrantSystemRole ( systemRoleJobFunction );

                // Verify
                Assert.IsTrue ( eventRaised );
            }
        }

        private static void SetupServiceLocatorFixture ( ServiceLocatorFixture serviceLocatorFixture )
        {
            serviceLocatorFixture.StructureMapContainer.Configure (
                c => c.For<IDomainEventService> ().HybridHttpOrThreadLocalScoped ().Use<DomainEventService> () );

            serviceLocatorFixture.StructureMapContainer.Configure (
                c => c.Scan (
                    x =>
                        {
                            x.AssembliesFromApplicationBaseDirectory ( p => ( p.FullName == null ) ? false : p.FullName.Contains ( "Rem.Domain" ) );
                            x.ConnectImplementationsToTypesClosing ( typeof( IRuleCollection<> ) );
                            x.ConnectImplementationsToTypesClosing ( typeof( IRuleCollectionCustomizer<,> ) );
                        } ) );
        }
    }
}
