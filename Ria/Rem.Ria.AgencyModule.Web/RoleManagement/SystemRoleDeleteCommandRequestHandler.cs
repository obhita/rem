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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using StructureMap.Attributes;

namespace Rem.Ria.AgencyModule.Web.RoleManagement
{
    /// <summary>
    /// Class for handling system role delete command request.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public abstract class SystemRoleDeleteCommandRequestHandler<TRequest, TResponse> :
        CommandRequestHandler<TRequest, TResponse, ValidationFailureDto>
        where TRequest : SystemRoleCommandRequestBase
        where TResponse : DtoResponse<ValidationFailureDto>, new ()
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the system role dto factory.
        /// </summary>
        /// <value>The system role dto factory.</value>
        [SetterProperty]
        public ISystemRoleFactory SystemRoleDtoFactory { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the dto from request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Rem.Infrastructure.Service.DataTransferObject.ValidationFailureDto"/></returns>
        protected override ValidationFailureDto CreateDtoFromRequest ( TRequest request )
        {
            return new ValidationFailureDto ();
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <param name="response">The response.</param>
        protected abstract void GetResponse ( DtoResponse<ValidationFailureDto> response );

        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        protected override void HandleRequest ( TRequest request, TResponse response )
        {
            var session = SessionProvider.GetSession ();
            var systemRoleKey = request.SystemRoleKey;
            var systemRole = session.Get<SystemRole> ( systemRoleKey );

            var staffDetachCriterial = DetachedCriteria.For<Staff> ()
                .CreateCriteria ( "SystemRoles", "sr", JoinType.LeftOuterJoin )
                .SetFetchMode ( "SystemRoles", FetchMode.Eager )
                .Add ( Restrictions.Eq ( "sr.SystemRole.Key", systemRoleKey ) )
                .SetResultTransformer ( new DistinctRootEntityResultTransformer () );

            var accountDetachCriterial = DetachedCriteria.For<SystemAccount> ()
                .CreateCriteria ( "SystemAccountRoles", "sar", JoinType.LeftOuterJoin )
                .SetFetchMode ( "SystemAccountRoles", FetchMode.Eager )
                .Add ( Restrictions.Eq ( "sar.SystemRole.Key", systemRoleKey ) )
                .SetResultTransformer ( new DistinctRootEntityResultTransformer () );

            var roleDetachCriterial = DetachedCriteria.For<SystemRole> ()
                .CreateCriteria ( "GrantedSystemRoleRelationships", "gsrr", JoinType.LeftOuterJoin )
                .SetFetchMode ( "GrantedSystemRoleRelationships", FetchMode.Eager )
                .Add ( Restrictions.Eq ( "gsrr.GrantedSystemRole.Key", systemRoleKey ) )
                .SetResultTransformer ( new DistinctRootEntityResultTransformer () );

            var multiCriteria = session.CreateMultiCriteria ();
            multiCriteria.Add ( staffDetachCriterial );
            multiCriteria.Add ( accountDetachCriterial );
            multiCriteria.Add ( roleDetachCriterial );

            var multiResults = multiCriteria.List ();

            // Staff list is the 1st result set
            var firstList = ( IList )multiResults[0];
            var staffList = new List<Staff> ();
            staffList.AddRange ( ( from object entity in firstList select entity as Staff ) );

            // Account list is the 2nd result set
            var secondList = ( IList )multiResults[1];
            var accountList = new List<SystemAccount> ();
            accountList.AddRange ( ( from object entity in secondList select entity as SystemAccount ) );

            // Role list is the 3rd result set
            var thirdList = ( IList )multiResults[2];
            var roleList = new List<SystemRole> ();
            roleList.AddRange ( ( from object entity in thirdList select entity as SystemRole ) );

            foreach ( var staff in staffList )
            {
                if ( systemRole.SystemRoleType == SystemRoleType.JobFunction )
                {
                    staff.RemoveJobFunctionRole ();
                }
                else if ( systemRole.SystemRoleType == SystemRoleType.Task )
                {
                    staff.RemoveTaskRole ( systemRole );
                }
                else if ( systemRole.SystemRoleType == SystemRoleType.TaskGroup )
                {
                    staff.RemoveTaskGroupRole ( systemRole );
                }
            }

            foreach ( var account in accountList )
            {
                account.RevokeSystemRole ( systemRole );
            }

            foreach ( var role in roleList )
            {
                role.RevokeSystemRole ( systemRole );
            }

            FlushSession ();

            SystemRoleDtoFactory.DestroySystemRole ( systemRole );

            if ( Success )
            {
                FlushSession ();

                GetResponse ( response );
            }
        }

        #endregion
    }
}
