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
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Agatha.Common;
using Agatha.ServiceLayer;
using AutoMapper;
using NHibernate.Criterion;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Domain;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientSearch
{
    /// <summary>
    /// Class for handling get patients by keywords request.
    /// </summary>
    public class GetPatientsByKeywordsRequestHandler : RequestHandler<GetPatientsByKeywordsRequest, GetPatientsByKeywordsResponse>
    {
        #region Constants and Fields

        private readonly IKeywordsSearchService _keywordsSearchService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPatientsByKeywordsRequestHandler"/> class.
        /// </summary>
        /// <param name="keywordsSearchService">The keywords search service.</param>
        public GetPatientsByKeywordsRequestHandler ( IKeywordsSearchService keywordsSearchService )
        {
            _keywordsSearchService = keywordsSearchService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetPatientsByKeywordsRequest request )
        {
            IList<Expression<Func<Patient, object>>> propertiesToSearch = new List<Expression<Func<Patient, object>>>
                {
                    p => p.Name.First,
                    p => p.Name.Middle,
                    p => p.Name.Last
                };

            IList<Order> orders = new List<Order>
                {
                    Order.Asc ( Projections.Property<Patient> ( p => p.Name.Last ) ),
                    Order.Asc ( Projections.Property<Patient> ( p => p.Name.First ) )
                };
            IList<Expression<Func<Patient, IEnumerable>>> eagerLoadingAssociations = new List<Expression<Func<Patient, IEnumerable>>>
                {
                    p => p.PhoneNumbers,
                    p => p.Addresses
                };

            var result = _keywordsSearchService.FindPagedEntityListByKeywords (
                request.SearchCriteria, propertiesToSearch, request.PageIndex, request.PageSize, orders, eagerLoadingAssociations );

            var pagedPatientSearchResultDto = new PagedPatientSearchResultDto
                {
                    TotalCount = result.TotalCount,
                    PageIndex = result.PageIndex,
                    PageSize = result.PageSize,
                    PagedList = Mapper.Map<IList<Patient>, IList<PatientSearchResultDto>> ( result.ItemList )
                };

            var response = CreateTypedResponse ();
            response.SearchCriteria = request.SearchCriteria;
            response.PagedPatientSearchResultDto = pagedPatientSearchResultDto;

            return response;
        }

        #endregion
    }
}
