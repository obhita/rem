using Pillar.Common.Utility;

namespace Rem.Domain.Core.SecurityModule
{
    /// <summary>
    /// The SystemRoleFactory implements lifetime management services for an <see cref="SystemRole">SystemRole</see>.
    /// </summary>
    public class SystemRoleFactory : ISystemRoleFactory
    {  
        private readonly ISystemRoleRepository _systemRoleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRoleFactory"/> class.
        /// </summary>
        /// <param name="systemRoleRepository">The program enrollment repository.</param>
        public SystemRoleFactory(ISystemRoleRepository systemRoleRepository)
        {
            _systemRoleRepository = systemRoleRepository;
        }


        #region Implementation of ISystemRoleFactory

        /// <summary>
        /// Creates the system role.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="systemRoleType">Type of the system role.</param>
        /// <returns>
        /// A SystemRole
        /// </returns>
        public SystemRole CreateSystemRole ( string name, string description, SystemRoleType systemRoleType )
        {
            Check.IsNotNullOrWhitespace(name, "Name is required.");
            Check.IsNotNull(systemRoleType, "System role type is required.");

            SystemRole systemRole;

            var existingSystemRole = _systemRoleRepository.GetByName( name );

            if (existingSystemRole != null)
            {
                systemRole = existingSystemRole;
            }
            else
            {
                systemRole = new SystemRole( name, description, systemRoleType );
                _systemRoleRepository.MakePersistent( systemRole );
            }

            return systemRole;
        }

        /// <summary>
        /// Destroys the system role.
        /// </summary>
        /// <param name="systemRole">The system role.</param>
        public void DestroySystemRole ( SystemRole systemRole )
        {
            Check.IsNotNull(systemRole, "System role is required");

            _systemRoleRepository.MakeTransient ( systemRole );
        }

        #endregion
    }
}
