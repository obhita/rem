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
using Agatha.Common;
using AutoMapper;
using Rem.Domain.Billing.PayorModule;
using Rem.Infrastructure.Service;
using Rem.Ria.BillingModule.Web.PayorEditor;

namespace Rem.Ria.BillingModule.Web.Common
{
    /// <summary>
    /// Class for handling get payor types by billing office key request.
    /// </summary>
    public class GetPayorTypesByBillingOfficeKeyRequestHandler :
        NHibernateSessionRequestHandler<GetPayorTypesByBillingOfficeKeyRequest, GetPayorTypesByBillingOfficeKeyResponse>
    {
        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetPayorTypesByBillingOfficeKeyRequest request )
        {
            var response = CreateTypedResponse ();

            var payorTypes = Session.QueryOver<PayorType> ().Where ( p => p.BillingOffice.Key == request.BillingOfficeKey ).List ();

            var payorTypeProfileDtos = Mapper.Map<IEnumerable<PayorType>, IEnumerable<PayorTypeDto>> ( payorTypes );

            response.PayorTypes = payorTypeProfileDtos;

            return response;
        }

        #endregion
    }
}
