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

using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.SecurityModule
{
    /// <summary>
    /// SystemRolePermission defines an access right that can be attached to a system object for a system role.
    /// </summary>
    public class SystemRolePermission : AuditableAggregateNodeBase
    {
        private SystemRole _systemRole;
        private SystemPermission _systemPermission;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRolePermission"/> class.
        /// </summary>
        protected SystemRolePermission()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRolePermission"/> class.
        /// </summary>
        /// <param name="systemRole">The system role.</param>
        /// <param name="systemPermission">The system permission.</param>
        protected internal SystemRolePermission ( SystemRole systemRole, SystemPermission systemPermission )
        {
            Check.IsNotNull ( systemRole, "System role is required." );
            Check.IsNotNull ( systemPermission, "System permission is required." );

            _systemRole = systemRole;
            _systemPermission = systemPermission;
        }

        /// <summary>
        /// Gets the system role.
        /// </summary>
        [NotNull]
        public virtual SystemRole SystemRole
        {
            get { return _systemRole; }
            private set { _systemRole = value; }
        }

        /// <summary>
        /// Gets the system permission.
        /// </summary>
        [NotNull]
        public virtual SystemPermission SystemPermission
        {
            get { return _systemPermission; }
            private set { _systemPermission = value; }
        }

        #region Overrides of AbstractAggregateNode

        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        public override IAggregateRoot AggregateRoot
        {
            get { return SystemRole; }
        }

        #endregion
    }
}
