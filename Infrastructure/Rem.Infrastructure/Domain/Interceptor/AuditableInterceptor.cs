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
using NHibernate;
using NHibernate.Type;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Core.SecurityModule;

namespace Rem.Infrastructure.Domain.Interceptor
{
    /// <summary>
    /// The <see cref="T:Rem.Infrastructure.Domain.Interceptor.AuditableInterceptor"/> contains common functionalities for auditable interceptor.
    /// </summary>
    [Serializable]
    public class AuditableInterceptor : EmptyInterceptor
    {
        private readonly Func<ISystemAccountProvider> _systemAccountProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Rem.Infrastructure.Domain.Interceptor.AuditableInterceptor"/> class.
        /// </summary>
        /// <param name="systemAccountProvider">The system account provider.</param>
        public AuditableInterceptor (Func<ISystemAccountProvider> systemAccountProvider)
        {
            _systemAccountProvider = systemAccountProvider;
        }

        /// <summary>
        /// Called when [save].
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <param name="id">The id of the entity.</param>
        /// <param name="state">The state information.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <param name="types">The type information.</param>
        /// <returns><c>true</c> if it is an auditable entity.</returns>
        public override bool OnSave (
            object entity,
            object id,
            object[] state,
            string[] propertyNames,
            IType[] types )
        {
            bool entityModified = false;

            if ( entity is IAuditable )
            {
                SystemAccount systemAccount = GetSystemAccount ();

                DateTimeOffset dateTime = DateTimeOffset.Now;

                SetStateValue ( propertyNames, state, "CreatedTimestamp", dateTime );
                SetStateValue ( propertyNames, state, "UpdatedTimestamp", dateTime );
                SetStateValue ( propertyNames, state, "CreatedBySystemAccount", systemAccount );
                SetStateValue ( propertyNames, state, "UpdatedBySystemAccount", systemAccount );

                entityModified = true;
            }

            return entityModified;
        }

        /// <summary>
        /// Called when [flush dirty].
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <param name="id">The id of the entity.</param>
        /// <param name="currentState">State of the current.</param>
        /// <param name="previousState">State of the previous.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <param name="types">The type information.</param>
        /// <returns><c>true</c> if the entity is modified.</returns>
        public override bool OnFlushDirty (
            object entity,
            object id,
            object[] currentState,
            object[] previousState,
            string[] propertyNames,
            IType[] types )
        {
            bool entityModified = false;

            if ( entity is IAuditable )
            {
                SystemAccount systemAccount = GetSystemAccount();

                DateTimeOffset dateTime = DateTimeOffset.Now;

                SetStateValue ( propertyNames, currentState, "UpdatedTimestamp", dateTime );
                SetStateValue ( propertyNames, currentState, "UpdatedBySystemAccount", systemAccount );

                entityModified = true;
            }

            return entityModified;
        }

        private static void SetStateValue (
            string[] propertyNames,
            object[] state,
            string propertyName,
            object value )
        {
            int index = Array.IndexOf ( propertyNames, propertyName );
            if ( index == -1 )
            {
                throw new AggregateException ( string.Format ( "Property {0} not found.", propertyName ) );
            }

            state[ index ] = value;
        }

        private SystemAccount GetSystemAccount()
        {
            var account = _systemAccountProvider().SystemAccount;
            if (account == null)
            {
                throw new SystemException ( "The system account is missing from SystemAccountProvider." );
            }

            return account;
        }
    }
}