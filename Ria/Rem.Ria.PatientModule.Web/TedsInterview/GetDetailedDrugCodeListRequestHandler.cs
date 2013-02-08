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

using Agatha.Common;
using NHibernate.Criterion;
using NHibernate.Transform;
using Pillar.Domain.NHibernate.Extensions;
using Rem.Domain.Clinical.TedsModule;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <summary>
    /// Class for handling get detailed drug code list request.
    /// </summary>
    public class GetDetailedDrugCodeListRequestHandler :
        NHibernateSessionRequestHandler<GetDetailedDrugCodeListRequest, GetDetailedDrugCodeListResponse>
    {
        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetDetailedDrugCodeListRequest request )
        {
            var response = CreateTypedResponse ();

            var criteria = Session.CreateCriteria ( typeof(DetailedDrugCode) );

            criteria.SetProjection (
                Projections.ProjectionList ()
                    .Add<LookupBase, LookupValueDto, long> ( p => p.Key, pDto => pDto.Key )
                    .Add<LookupBase, LookupValueDto, string> (
                        p => p.WellKnownName,
                        pDto => pDto.WellKnownName )
                    .Add<LookupBase, LookupValueDto, string> ( p => p.Name, pDto => pDto.Name )
                    .Add<LookupBase, LookupValueDto, string> ( p => p.ShortName, pDto => pDto.ShortName )
                    .Add<LookupBase, LookupValueDto, int?> (
                        p => p.SortOrderNumber,
                        pDto => pDto.SortOrderNumber )
                    .Add<DetailedDrugCode, DetailedDrugCodeDto, long>(p => p.SubstanceProblemType.Key, pDto => pDto.SubstanceProblemTypeKey));

            criteria.SetResultTransformer ( Transformers.AliasToBean ( typeof( DetailedDrugCodeDto ) ) );

            criteria.AddOrder ( Order.Asc ( Projections.Property<LookupBase> ( p => p.SortOrderNumber ) ) )
                .AddOrder ( Order.Asc ( Projections.Property<LookupBase> ( p => p.Name ) ) );

            response.DetailedDrugCodeList = criteria.List<DetailedDrugCodeDto> ();

            return response;
        }
    }
}
