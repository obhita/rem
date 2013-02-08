#region License
// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

using System.Collections.Generic;
using System.Linq;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.SecurityModule
{
    /// <summary>
    /// SystemRole defines a grouping by user level account user type or function.
    /// </summary>
    public class SystemRole : AuditableAggregateRootBase
    {
        private readonly IList<SystemRolePermission> _grantedSystemPermissions;
        private readonly IList<SystemRoleRelationship> _grantedSystemRoleRelationships;
        private string _description;
        private string _name;
        private string _wellKnownName;
        private SystemRoleType _systemRoleType;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRole"/> class.
        /// </summary>
        protected internal SystemRole ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRole"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="systemRoleType">Type of the system role.</param>
        protected internal SystemRole ( string name, string description, SystemRoleType systemRoleType )
        {
            Check.IsNotNullOrWhitespace ( name, "Name is required." );
            Check.IsNotNull ( systemRoleType, "System role type is required." );

            _name = name;
            _description = description;
            _systemRoleType = systemRoleType;
            _grantedSystemPermissions = new List<SystemRolePermission> ();
            _grantedSystemRoleRelationships = new List<SystemRoleRelationship> ();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        [NotNull]
        public virtual string Name
        {
            get { return _name; }
            private set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        /// Gets the name of the well known.
        /// </summary>
        /// <value>
        /// The name of the well known.
        /// </value>
        public virtual string WellKnownName
        {
            get { return _wellKnownName; }
            private set { ApplyPropertyChange(ref _wellKnownName, () => WellKnownName, value); }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public virtual string Description
        {
            get { return _description; }
            private set { ApplyPropertyChange ( ref _description, () => Description, value ); }
        }

        /// <summary>
        /// Gets the type of the system role.
        /// </summary>
        /// <value>
        /// The type of the system role.
        /// </value>
        [NotNull]
        public virtual SystemRoleType SystemRoleType
        {
            get { return _systemRoleType; }
            private set { ApplyPropertyChange ( ref _systemRoleType, () => SystemRoleType, value ); }
        }

        /// <summary>
        /// Gets the granted system permissions.
        /// </summary>
        public virtual IList<SystemRolePermission> GrantedSystemPermissions
        {
            get { return _grantedSystemPermissions.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the granted system role relationships.
        /// </summary>
        public virtual IList<SystemRoleRelationship> GrantedSystemRoleRelationships
        {
            get { return _grantedSystemRoleRelationships.ToList ().AsReadOnly (); }
            private set { }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Renames the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public virtual void Rename (string name, string description)
        {
            Check.IsNotNullOrWhitespace(name, "Name is required.");

            Name = name;
            Description = description;
        }

        /// <summary>
        /// Grants the system role.
        /// </summary>
        /// <remarks>
        ///  Grants the given role to this SystemRole. Roles can be hierarchical enabling adminstrators
        ///  to organize the authorization system around sets of grouped permissions.
        /// </remarks>
        /// <param name="systemRole">The role to be granted.</param>
        public virtual void GrantSystemRole ( SystemRole systemRole )
        {
            Check.IsNotNull ( systemRole, "System role is required." );

            DomainRuleEngine.CreateRuleEngine<SystemRole, SystemRole> ( this, () => GrantSystemRole )
                .WithContext (systemRole )                
                .Execute (
                    () =>
                        {
                            var systemRoleRelationship = new SystemRoleRelationship ( systemRole, this );
                            _grantedSystemRoleRelationships.Add ( systemRoleRelationship );
                            NotifyItemAdded ( () => GrantedSystemRoleRelationships, systemRoleRelationship );
                        }
                );
        }

        /// <summary>
        /// Revokes the system role.
        /// </summary>
        /// <param name="systemRole">The system role.</param>
        public virtual void RevokeSystemRole ( SystemRole systemRole )
        {
            Check.IsNotNull ( systemRole, "System role is required." );

            var systemRoleRelationship = _grantedSystemRoleRelationships.FirstOrDefault ( sr => sr.GrantedSystemRole.Key == systemRole.Key );
            if (systemRoleRelationship != null)
            {
                _grantedSystemRoleRelationships.Remove ( systemRoleRelationship );
                NotifyItemRemoved ( () => GrantedSystemRoleRelationships, systemRoleRelationship );
            }
        }

        /// <summary>
        /// Grants the system permission.
        /// </summary>
        /// <param name="systemPermission">The system permission.</param>
        public virtual void GrantSystemPermission ( SystemPermission systemPermission )
        {
            Check.IsNotNull ( systemPermission, "System permission is required." );

            DomainRuleEngine.CreateRuleEngine<SystemRole, SystemPermission> ( this, () => GrantSystemPermission )
                .WithContext ( systemPermission )
                .Execute (
                    () =>
                        {
                            var systemRolePermission = new SystemRolePermission ( this, systemPermission );
                            _grantedSystemPermissions.Add ( systemRolePermission );
                            NotifyItemAdded ( () => GrantedSystemPermissions, systemRolePermission );
                        }
                );
        }

        /// <summary>
        /// Revokes the system permission.
        /// </summary>
        /// <param name="systemPermission">The system permission.</param>
        public virtual void RevokeSystemPermission ( SystemPermission systemPermission )
        {
            Check.IsNotNull ( systemPermission, "System permission is required." );

            var existingSystemPermission = ( from sp in _grantedSystemPermissions where sp.SystemPermission.Key == systemPermission.Key select sp ).FirstOrDefault ();
            if ( existingSystemPermission != null )
            {
                _grantedSystemPermissions.Remove ( existingSystemPermission ); 
                NotifyItemRemoved(() => GrantedSystemPermissions, existingSystemPermission);
            }
        }

        /// <summary>
        /// Clones the current system role.
        /// </summary>
        /// <returns>The cloned system role.</returns>
        public virtual SystemRole Clone ()
        {
            var systemRoleFactory = IoC.CurrentContainer.Resolve<ISystemRoleFactory> ();
            var clonedSystemRole = systemRoleFactory.CreateSystemRole ( "Copy of " + Name, Description, SystemRoleType );

            foreach ( var permissionLink in _grantedSystemPermissions)
            {
                var systemRolePermission = new SystemRolePermission ( clonedSystemRole, permissionLink.SystemPermission );
                clonedSystemRole._grantedSystemPermissions.Add ( systemRolePermission );
            }

            foreach ( var roleLink in _grantedSystemRoleRelationships)
            {
                var systemRoleRelationship = new SystemRoleRelationship ( roleLink.GrantedSystemRole, clonedSystemRole );
                clonedSystemRole._grantedSystemRoleRelationships.Add(systemRoleRelationship);
            }

            return clonedSystemRole;
        }

        #endregion
    }
}
