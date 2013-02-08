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
using Agatha.Common;
using AutoMapper;
using Rem.Domain.Billing.BillingOfficeModule;
using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Service;

namespace Rem.Ria.BillingModule.Web.BillingOfficeEditor
{
    /// <summary>
    /// Class for handling create billing office request.
    /// </summary>
    public class CreateBillingOfficeRequestHandler : NHibernateSessionRequestHandler<CreateBillingOfficeRequest, DtoResponse<BillingOfficeDto>>
    {
        #region Constants and Fields

        private readonly IAgencyRepository _agencyRepository;
        private readonly IBillingOfficeFactory _billingOfficeFactory;
        private readonly IStaffRepository _staffRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBillingOfficeRequestHandler"/> class.
        /// </summary>
        /// <param name="billingOfficeFactory">The billing office factory.</param>
        /// <param name="agencyRepository">The agency repository.</param>
        /// <param name="staffRepository">The staff repository.</param>
        public CreateBillingOfficeRequestHandler (
            IBillingOfficeFactory billingOfficeFactory, IAgencyRepository agencyRepository, IStaffRepository staffRepository )
        {
            _billingOfficeFactory = billingOfficeFactory;
            _agencyRepository = agencyRepository;
            _staffRepository = staffRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( CreateBillingOfficeRequest request )
        {
            var response = CreateTypedResponse ();

            var agency = _agencyRepository.GetByKey ( request.AgencyKey );
            var staff = _staffRepository.GetByKey ( request.StaffKey );

            var billingOffice = _billingOfficeFactory.CreateBillingOffice (
                agency, staff, Guid.NewGuid ().ToString (), new BillingOfficeProfile ( request.Name, null, null ) );

            var billingOfficeDto = Mapper.Map<BillingOffice, BillingOfficeDto> ( billingOffice );

            response.DataTransferObject = billingOfficeDto;

            return response;
        }

        #endregion
    }
}
