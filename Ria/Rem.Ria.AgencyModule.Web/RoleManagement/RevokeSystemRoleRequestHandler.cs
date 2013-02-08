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

using NHibernate;
using Rem.Domain.Core.SecurityModule;

namespace Rem.Ria.AgencyModule.Web.RoleManagement
{
    /// <summary>
    /// Class for handling revoke system role request.
    /// </summary>
    public class RevokeSystemRoleRequestHandler : SystemRoleCommandRequestHandler<RevokeSystemRoleRequest>
    {
        #region Methods

        /// <summary>
        /// Processes the system role command.
        /// </summary>
        /// <param name="systemRole">The system role.</param>
        /// <param name="request">The request.</param>
        protected override void ProcessSystemRoleCommand ( SystemRole systemRole, RevokeSystemRoleRequest request )
        {
            var session = SessionProvider.GetSession ();
            foreach ( var systemRoleKeyToBeRevoked in request.SystemRoleKeysToBeRevoked )
            {
                var revokeSystemRole = session.Get<SystemRole> ( systemRoleKeyToBeRevoked );
                systemRole.RevokeSystemRole ( revokeSystemRole );
            }
        }

        #endregion
    }
}
