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
using System.Collections.Generic;
using System.Linq.Expressions;
using Agatha.Common;
using AutoMapper;
using NHibernate.Criterion;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.Infrastructure.Web.QuickPickers
{
    /// <summary>
    /// Class for handling get lookup list by keyword request.
    /// </summary>
    public class GetLookupListByKeywordRequestHandler :
        NHibernateSessionRequestHandler<GetLookupListByKeywordRequest, GetLookupListByKeywordResponse>
    {
        #region Constants and Fields

        private readonly IKeywordsSearchService _keywordsSearchService;
        private readonly ILookupTypeService _lookupTypeService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetLookupListByKeywordRequestHandler"/> class.
        /// </summary>
        /// <param name="keywordsSearchService">The keywords search service.</param>
        /// <param name="lookupTypeService">The lookup type service.</param>
        public GetLookupListByKeywordRequestHandler(IKeywordsSearchService keywordsSearchService, ILookupTypeService lookupTypeService)
        {
            _keywordsSearchService = keywordsSearchService;
            _lookupTypeService = lookupTypeService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetLookupListByKeywordRequest request )
        {
            var lookupType = _lookupTypeService.GetLookupType ( request.LookupName );

            IList<Expression<Func<LookupBase, object>>> propertiesToSearch = new List<Expression<Func<LookupBase, object>>>
                {
                    p => p.Name,
                    p => p.Description
                };

            IList<Order> orders = new List<Order>
                {
                    Order.Asc ( Projections.Property<LookupBase> ( p => p.SortOrderNumber ) ),
                    Order.Asc ( Projections.Property<LookupBase> ( p => p.Name ) )
                };

            var result = _keywordsSearchService.FindPagedEntityListByKeywords (
                lookupType, request.SearchCriteria, propertiesToSearch, request.PageIndex, request.PageSize, orders );

            var pagedLookupListDto = new PagedLookupListDto
                {
                    TotalCount = result.TotalCount,
                    PageIndex = result.PageIndex,
                    PageSize = result.PageSize,
                    PagedList = Mapper.Map<IList<LookupBase>, IList<LookupValueDto>> ( result.ItemList )
                };

            var response = CreateTypedResponse ();
            response.SearchCriteria = request.SearchCriteria;
            response.PagedLookupListDto = pagedLookupListDto;

            return response;
        }

        #endregion
    }
}
