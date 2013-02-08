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

namespace Rem.Domain.Core.SecurityModule
{
    /// <summary>
    /// SystemPermissionService defines a service for managing system permissions.
    /// </summary>
    public class SystemPermissionService : ISystemPermissionService
    {
        #region Implementation of ISystemPermissionService

        /// <summary>
        /// Finds the granted system permissions.
        /// </summary>
        /// <param name="systemRoles">The system roles.</param>
        /// <returns>An IEnumerable&lt;SystemPermission&gt;.</returns>
        public IEnumerable<SystemPermission> FindGrantedSystemPermissions ( IEnumerable<SystemRole> systemRoles )
        {
            // Roles can be granted explicity; meaning directly granted to an account, and they 
            // can be granted implicitly; meaning granted to a role that has been explicitly granted 
            // or granted to another implicit role.
            //
            // Because of this we need to search through all of these relationships to determine the
            // sum of all granted permissions.

            // Create a stack used to keep track of the 'unprocessed' roles.
            var unprocessedRoleStack = new Stack<SystemRole>();

            // Create a list of SystemRoles to keep track of roles we have already processed.  This
            // is important because a role can be implicitly granted multiple times and we don't want
            // to process it again.  Also, it is possible to get into infinite loops.
            var processedRoleList = new List<SystemRole>();

            // Create a list of permissions that will contain the 'sum' of all granted permissions.
            var permissionList = new List<SystemPermission>();

            // Initialize the unprocessed list with the 'explicitly' granted roles.
            foreach (var explicitlyGrantedSystemRole in systemRoles)
            {
                unprocessedRoleStack.Push(explicitlyGrantedSystemRole);
            }

            while (unprocessedRoleStack.Count > 0)
            {
                var role = unprocessedRoleStack.Pop();
                foreach (var systemRolePermission in role.GrantedSystemPermissions)
                {
                    if (!permissionList.Contains(systemRolePermission.SystemPermission))
                    {
                        permissionList.Add(systemRolePermission.SystemPermission);
                    }
                }

                foreach (var grantedSystemRoleRelationship in role.GrantedSystemRoleRelationships)
                {
                    if (!processedRoleList.Contains(grantedSystemRoleRelationship.SystemRole))
                    {
                        unprocessedRoleStack.Push(grantedSystemRoleRelationship.GrantedSystemRole);
                    }
                }

                processedRoleList.Add ( role );
            }

            return permissionList;
        }

        #endregion
    }
}