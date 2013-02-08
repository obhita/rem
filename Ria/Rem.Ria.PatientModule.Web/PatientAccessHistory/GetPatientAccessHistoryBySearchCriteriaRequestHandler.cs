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
using Agatha.Common;
using AutoMapper;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientAccessHistory
{
    /// <summary>
    /// Class for handling get patient access history by search criteria request.
    /// </summary>
    public class GetPatientAccessHistoryBySearchCriteriaRequestHandler :
        NHibernateSessionRequestHandler<GetPatientAccessHistoryBySearchCriteriaRequest, GetPatientAccessHistoryBySearchCriteriaResponse>
    {
        #region Constants and Fields

        private readonly IPatientAccessEventRepository _patientAccessEventRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPatientAccessHistoryBySearchCriteriaRequestHandler"/> class.
        /// </summary>
        /// <param name="patientAccessEventRepository">The patient access event repository.</param>
        public GetPatientAccessHistoryBySearchCriteriaRequestHandler ( IPatientAccessEventRepository patientAccessEventRepository )
        {
            _patientAccessEventRepository = patientAccessEventRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetPatientAccessHistoryBySearchCriteriaRequest request )
        {
            var patientKey = request.PatientKey;
            var userKey = request.UserKey;
            var startDate = request.StartDate;
            var endDate = request.EndDate;
            var accessType = request.AccessType;
            var pageIndex = request.PageIndex;
            var pageSize = request.PageSize;
            var sortingMemberName = request.SortingMemberName;
            var secondOrderSortBy = request.SecondOrderSortBy;
            var secondOrderSortDirection = request.SecondOrderSortDirection;

            var result = _patientAccessEventRepository.FindPatientAccessEventsBySearchCriteria (
                patientKey,
                userKey,
                startDate,
                endDate,
                accessType,
                pageIndex,
                pageSize,
                sortingMemberName,
                secondOrderSortBy,
                secondOrderSortDirection );

            var patientAccessEventDtos = Mapper.Map<IList<PatientAccessEvent>, IList<PatientAccessEventDto>> ( result.Item3 );

            var pagedSearchResultDto = new PagedPatientAccessEventSearchResultDto
                {
                    TotalCount = result.Item1,
                    PageIndex = result.Item2,
                    PageSize = pageSize,
                    PagedList = patientAccessEventDtos
                };

            var response = CreateTypedResponse ();
            response.PagedPatientAccessEventSearchResultDto = pagedSearchResultDto;

            return response;
        }

        #endregion
    }
}
