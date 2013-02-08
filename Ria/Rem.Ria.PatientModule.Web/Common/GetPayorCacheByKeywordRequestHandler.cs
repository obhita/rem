using System.Collections.Generic;
using Agatha.Common;
using AutoMapper;
using NHibernate.Criterion;
using Pillar.Domain.NHibernate;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.FrontDeskDashboard;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Class for handling get payor cache by keyword request.
    /// </summary>
    public class GetPayorCacheByKeywordRequestHandler :
        NHibernateSessionRequestHandler<GetPayorCacheByKeywordRequest, GetPayorCacheByKeywordResponse>
    {
        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle(GetPayorCacheByKeywordRequest request)
        {
            var response = CreateTypedResponse();

            int totalCount;

            var selfPayments = Session.GetPagedResults<PayorCache>(
                Restrictions.Like ( "Name", request.SearchCriteria, MatchMode.Start ),
                request.PageIndex,
                request.PageSize,
                null,
                out totalCount);

            response.PagedPayorCache = new PagedPayorCacheDto
            {
                PagedList = Mapper.Map<IList<PayorCache>, IList<PayorCacheSummaryDto>>(selfPayments),
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };

            response.SearchCriteria = request.SearchCriteria;

            return response;
        }

        #endregion
    }
}
