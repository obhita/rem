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

using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// System Role Dto class
    /// </summary>
    [DataContract]
    public partial class SystemRoleDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private DateTimeOffset _createdTimestamp;
        private string _description;
        private ObservableCollection<SystemPermissionDto> _grantedSystemPermissions;
        private ObservableCollection<SystemRoleDto> _grantedSystemRoles;
        private string _name;
        private SystemRoleType _systemRoleType;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the created timestamp.
        /// </summary>
        /// <value>The created timestamp.</value>
        [DataMember]
        public DateTimeOffset CreatedTimestamp
        {
            get { return _createdTimestamp; }
            set { ApplyPropertyChange ( ref _createdTimestamp, () => CreatedTimestamp, value ); }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataMember]
        public string Description
        {
            get { return _description; }
            set { ApplyPropertyChange ( ref _description, () => Description, value ); }
        }

        /// <summary>
        /// Gets or sets the granted system permissions.
        /// </summary>
        /// <value>The granted system permissions.</value>
        [DataMember]
        public ObservableCollection<SystemPermissionDto> GrantedSystemPermissions
        {
            get { return _grantedSystemPermissions; }
            set { ApplyCollectionChange ( ref _grantedSystemPermissions, () => GrantedSystemPermissions, value ); }
        }

        /// <summary>
        /// Gets or sets the granted system roles.
        /// </summary>
        /// <value>The granted system roles.</value>
        [DataMember]
        public ObservableCollection<SystemRoleDto> GrantedSystemRoles
        {
            get { return _grantedSystemRoles; }
            set { ApplyCollectionChange ( ref _grantedSystemRoles, () => GrantedSystemRoles, value ); }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the role.</value>
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the system role.
        /// </summary>
        /// <value>The type of the system role.</value>
        [DataMember]
        public SystemRoleType SystemRoleType
        {
            get { return _systemRoleType; }
            set { ApplyPropertyChange ( ref _systemRoleType, () => SystemRoleType, value ); }
        }

        #endregion
    }
}
