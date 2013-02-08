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
using Agatha.Common;
using AutoMapper;
using NHibernate;
using NHibernate.Criterion;
using Pillar.Domain.NHibernate.Extensions;
using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.Common;

namespace Rem.Ria.AgencyModule.Web.AgencyEditor
{
    /// <summary>
    /// Class for handling get agency by key request.
    /// </summary>
    public class GetAgencyByKeyRequestHandler : NHibernateSessionRequestHandler<GetAgencyByKeyRequest, GetAgencyByKeyResponse>
    {
        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetAgencyByKeyRequest request )
        {
            var key = request.Key;

            ICriterion rootCriterion = Restrictions.Eq ( Projections.Property<Agency> ( p => p.Key ), key );

            var multiCriteria = Session.CreateMultiCriteria ()
                .AddDetachedCriteriaForChild<Agency, IEnumerable<AgencyAddressAndPhone>> (
                    rootCriterion,
                    p => p.AddressesAndPhones,
                    null,
                    true )
                .AddDetachedCriteriaForChild<Agency, IEnumerable<AgencyAlias>> (
                    rootCriterion,
                    p => p.AgencyAliases )
                .AddDetachedCriteriaForChild<Agency, IEnumerable<AgencyContact>> (
                    rootCriterion,
                    p => p.AgencyContacts )
                .AddDetachedCriteriaForChild<Agency, IEnumerable<AgencyIdentifier>> (
                    rootCriterion,
                    p => p.AgencyIdentifiers )
                .AddDetachedCriteriaForChild<Agency, IEnumerable<AgencyFrequentlyAskedQuestion>> (
                    rootCriterion,
                    p => p.AgencyFrequentlyAskedQuestions )
                .AddDetachedCriteriaForChild<Agency, IEnumerable<AgencyCharacteristic>> (
                    rootCriterion,
                    p => p.AgencyCharacteristics );

            var results = multiCriteria.List ();

            var agencies = ( IList )( results[0] );

            // TODO: Work-around to address the return of multiple root entities if multiple child entities exist. 
            var agency = ( Agency )agencies[0];

            var agencyDto = Mapper.Map<Agency, AgencyDto> ( agency );

            var response = CreateTypedResponse ();
            response.AgencyDto = agencyDto;

            return response;
        }

        #endregion
    }
}
