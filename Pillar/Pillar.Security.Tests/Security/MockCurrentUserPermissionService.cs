using System.Collections.Generic;
using Pillar.Security.AccessControl;

namespace Pillar.Security.Tests.Security
{
    public class MockCurrentUserPermissionService : ICurrentUserPermissionService
    {
        public List<Permission> Permissions { get; set; }

        #region Implementation of ICurrentUserPermissionService

        public bool DoesUserHavePermission ( Permission permission )
        {
            return Permissions != null && Permissions.Contains ( permission );
        }

        #endregion
    }
}
