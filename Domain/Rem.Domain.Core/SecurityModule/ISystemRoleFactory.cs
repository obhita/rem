namespace Rem.Domain.Core.SecurityModule
{
    /// <summary>
    /// The ISystemRoleFactory interface defines lifetime management services for an <see cref="SystemRole">SystemRole</see>.
    /// </summary>
    public interface ISystemRoleFactory
    {
        /// <summary>
        /// Creates the system role.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="systemRoleType">Type of the system role.</param>
        /// <returns>A SystemRole</returns>
        SystemRole CreateSystemRole ( string name, string description, SystemRoleType systemRoleType );

        /// <summary>
        /// Destroys the system role.
        /// </summary>
        /// <param name="systemRole">The system role.</param>
        void DestroySystemRole ( SystemRole systemRole );
    }
}
