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
    /// SystemAccountRole defines a grouping by system level account user type or function in order to manage access on a per-group basis.
    /// </summary>
    public class SystemAccountRole : AuditableAggregateNodeBase
    {
        private SystemAccount _systemAccount;
        private SystemRole _systemRole;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemAccountRole"/> class.
        /// </summary>
        protected SystemAccountRole()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemAccountRole"/> class.
        /// </summary>
        /// <param name="systemAccount">The system account.</param>
        /// <param name="systemRole">The system role.</param>
        protected internal SystemAccountRole ( SystemAccount systemAccount, SystemRole systemRole )
        {
            Check.IsNotNull ( systemAccount, "System account is required." );
            Check.IsNotNull ( systemRole, "System role is required." );

            _systemAccount = systemAccount;
            _systemRole = systemRole;
        }

        /// <summary>
        /// Gets the system account.
        /// </summary>
        [NotNull]
        public virtual SystemAccount SystemAccount
        {
            get { return _systemAccount; }
            private set { _systemAccount = value; }
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

        #region Overrides of AbstractAggregateNode

        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        public override IAggregateRoot AggregateRoot
        {
            get { return SystemAccount; }
        }

        #endregion
    }
}