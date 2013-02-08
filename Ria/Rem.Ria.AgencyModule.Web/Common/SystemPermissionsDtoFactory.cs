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
using System.Collections.ObjectModel;
using AutoMapper;
using NHibernate;
using NHibernate.Criterion;
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Service;
using StructureMap.Attributes;

namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// Factory for system permissions dto.
    /// </summary>
    public class SystemPermissionsDtoFactory : IKeyedDtoFactory<SystemPermissionsDto>
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the session provider.
        /// </summary>
        /// <value>The session provider.</value>
        [SetterProperty]
        public ISessionProvider SessionProvider { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the keyed dto.
        /// </summary>
        /// <param name="key">The key to create.</param>
        /// <returns>A <see cref="Rem.Ria.AgencyModule.Web.Common.SystemPermissionsDto"/></returns>
        public SystemPermissionsDto CreateKeyedDto ( long key )
        {
            var dto = new SystemPermissionsDto ();

            var session = SessionProvider.GetSession ();

            var criteria = session.CreateCriteria<SystemPermission> ();
            criteria.AddOrder ( Order.Asc ( Projections.Property<SystemPermission> ( p => p.DisplayName ) ) );

            var systemPermissions = criteria.List<SystemPermission> ();

            var systemPermissionDtos = Mapper.Map<IList<SystemPermission>, IList<SystemPermissionDto>> ( systemPermissions );

            dto.SystemPermissions = new ObservableCollection<SystemPermissionDto> ( systemPermissionDtos );

            return dto;
        }

        #endregion
    }
}
