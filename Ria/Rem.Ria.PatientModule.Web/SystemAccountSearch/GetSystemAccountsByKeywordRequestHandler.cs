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
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.SystemAccountSearch
{
    /// <summary>
    /// Class for handling get system accounts by keyword request.
    /// </summary>
    public class GetSystemAccountsByKeywordRequestHandler :
        NHibernateSessionRequestHandler<GetSystemAccountsByKeywordRequest, GetSystemAccountsByKeywordResponse>
    {
        #region Constants and Fields

        private readonly ISystemAccountRepository _systemAccountRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSystemAccountsByKeywordRequestHandler"/> class.
        /// </summary>
        /// <param name="systemAccountRepository">The system account repository.</param>
        public GetSystemAccountsByKeywordRequestHandler ( ISystemAccountRepository systemAccountRepository )
        {
            _systemAccountRepository = systemAccountRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetSystemAccountsByKeywordRequest request )
        {
            var searchCriteria = request.SearchCriteria;
            var pageIndex = request.PageIndex;
            var pageSize = request.PageSize;

            var normalizedCriteria = searchCriteria.Trim ();

            var result = _systemAccountRepository.FindSystemAccountsByKeyword (
                normalizedCriteria, pageIndex, pageSize );

            var systemAccountSearchResultDtos = new List<SystemAccountSearchResultDto> ();
            foreach ( var systemAccount in result.Item3 )
            {
                systemAccountSearchResultDtos.Add (
                    new SystemAccountSearchResultDto
                        {
                            Key = systemAccount.Key,
                            AccountName = systemAccount.Identifier,

                            //FirstName = systemAccount.Staff != null ? systemAccount.Staff.FirstName : string.Empty,
                            //LastName = systemAccount.Staff != null ? systemAccount.Staff.LastName : string.Empty,
                            //SuffixName = systemAccount.Staff != null ? systemAccount.Staff.SuffixName : string.Empty,
                            //LocationName =
                            //    systemAccount.Staff != null && systemAccount.Staff.Locations.Count() > 0
                            //        ? systemAccount.Staff.Locations.ElementAt(0).Location.DisplayName
                            //        : string.Empty
                        } );
            }

            var pagedSearchResultDto = new PagedSystemAccountSearchResultDto
                {
                    TotalCount = result.Item1,
                    PageIndex = result.Item2,
                    PageSize = pageSize,
                    PagedList = systemAccountSearchResultDtos
                };

            var response = CreateTypedResponse ();
            response.SearchCriteria = searchCriteria;
            response.PagedSystemAccountSearchResultDto = pagedSearchResultDto;

            return response;
        }

        #endregion
    }
}
