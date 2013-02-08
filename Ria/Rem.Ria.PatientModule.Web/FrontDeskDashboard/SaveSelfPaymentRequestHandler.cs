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
using Pillar.Common.Utility;
using Pillar.Domain.Event;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Extension;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.FrontDeskDashboard
{
    /// <summary>
    /// Class for handling save self payment requests.
    /// </summary>
    public class SaveSelfPaymentRequestHandler :
        NHibernateSessionRequestHandler<SaveDtoRequest<SelfPaymentDto>, DtoResponse<SelfPaymentDto>>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private readonly ISelfPaymentRepository _selfPaymentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly ISelfPaymentFactory _selfPaymentFactory;
        private readonly IStaffRepository _staffRepository;
        private bool _validationFailureOccurred;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveSelfPaymentRequestHandler"/> class.
        /// </summary>
        /// <param name="selfPaymentFactory">The self payment factory.</param>
        /// <param name="patientRepository">The patient repository.</param>
        /// <param name="staffRepository">The staff repository.</param>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <param name="selfPaymentRepository">The self payment repository.</param>
        public SaveSelfPaymentRequestHandler (
            ISelfPaymentFactory selfPaymentFactory,
            IPatientRepository patientRepository,
            IStaffRepository staffRepository,
            IDtoToDomainMappingHelper mappingHelper,
            ISelfPaymentRepository selfPaymentRepository)
        {
            _selfPaymentFactory = selfPaymentFactory;
            _patientRepository = patientRepository;
            _staffRepository = staffRepository;
            _mappingHelper = mappingHelper;
            _selfPaymentRepository = selfPaymentRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="dto">The dto to create new for.</param>
        /// <returns>A new <see cref="Rem.Domain.Clinical.PatientModule.SelfPayment"/>.</returns>
        protected SelfPayment CreateNew ( SelfPaymentDto dto )
        {
            var currency = _mappingHelper.MapLookupField<Currency> ( dto.CurrencyWellKnownName );
            var paymentMethod = _mappingHelper.MapLookupField<PaymentMethod> ( dto.PaymentMethod );

            var patient = _patientRepository.GetByKeyOrThrow ( dto.PatientKey, "Patient" );
            var staff = _staffRepository.GetByKeyOrThrow ( dto.CollectedByStaffKey, "Staff" );

            var selfPayment = _selfPaymentFactory.CreateSelfPayment (
                patient, staff, new Money ( currency, dto.Amount ), paymentMethod, dto.CollectedDate );

            return selfPayment;
        }

        /// <summary>
        /// Deletes the aggregate
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root.</param>
        private void Delete ( SelfPayment aggregateRoot )
        {
             _selfPaymentFactory.DestroySelfPayment ( aggregateRoot );
        }

        #endregion

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( SaveDtoRequest<SelfPaymentDto> request )
        {
            var requestDto = request.DataTransferObject;

            DomainEvent.Register<RuleViolationEvent>(failure => _validationFailureOccurred = true);

            LogicalTreeWalker.Walk<IDataTransferObject>(requestDto, dto => dto.ClearAllDataErrorInfo());

            var response = CreateTypedResponse();
            response.DataTransferObject = requestDto;


            if(requestDto.EditStatus == EditStatus.Create)
            {
                var selfPayment = CreateNew(requestDto);
                response.DataTransferObject = AutoMapper.Mapper.Map<SelfPayment, SelfPaymentDto> ( selfPayment );
            }
            else if(requestDto.EditStatus == EditStatus.Delete)
            {
                var selfPayment = _selfPaymentRepository.GetByKey(requestDto.Key);
                if (selfPayment != null)
                {
                    Delete ( selfPayment );
                }
                response.DataTransferObject = requestDto;
            }

            var processSucceeded = !_validationFailureOccurred;

            if (processSucceeded)
            {
                Session.Flush();
                Session.Clear();
            }

            return response;
        }
    }
}
