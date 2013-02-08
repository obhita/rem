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

using Pillar.Domain.Primitives;
using Rem.Domain.Billing.BillingOfficeModule;
using Rem.Domain.Billing.PayorModule;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.BillingModule.Web.PayorEditor
{
    /// <summary>
    /// Class for handling save payor type dto request.
    /// </summary>
    public class SavePayorTypeDtoRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<PayorTypeDto>, DtoResponse<PayorTypeDto>, PayorTypeDto, PayorType>
    {
        #region Constants and Fields

        private readonly IBillingOfficeRepository _billingOfficeRepository;
        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SavePayorTypeDtoRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <param name="billingOfficeRepository">The billing office repository.</param>
        public SavePayorTypeDtoRequestHandler (
            IDtoToDomainMappingHelper mappingHelper,
            IBillingOfficeRepository billingOfficeRepository )
        {
            _mappingHelper = mappingHelper;
            _billingOfficeRepository = billingOfficeRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>A payor type.</returns>
        protected override PayorType CreateNew ( PayorTypeDto dto )
        {
            var billingOffice = _billingOfficeRepository.GetByKey ( dto.BillingOfficeKey );
            var payorType = billingOffice.AddPayorType ( dto.Name, dto.BillingForm );
            return payorType;
        }

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="payorType">Type of the payor.</param>
        /// <returns>A Boolean flag.</returns>
        protected override bool ProcessSingleAggregate ( PayorTypeDto dto, PayorType payorType )
        {
            payorType.ReviseName ( dto.Name );
            payorType.ReviseBillingForm ( dto.BillingForm );
            payorType.ReviseSubmitterIdentifier ( dto.SubmitterIdentifier );

            payorType.ReviseBillingFtpAddress ( string.IsNullOrWhiteSpace ( dto.FtpAddress ) ? null : new Ftp ( dto.FtpAddress ) );

            if ( !string.IsNullOrWhiteSpace ( dto.InterchangeReceiverNumber ) ||
                 !string.IsNullOrWhiteSpace ( dto.InterchangeSenderNumber ) ||
                 !string.IsNullOrWhiteSpace ( dto.CompositeDelimiter ) ||
                 !string.IsNullOrWhiteSpace ( dto.ElementDelimiter ) ||
                 !string.IsNullOrWhiteSpace ( dto.SegmentDelimiter ) ||
                 !string.IsNullOrWhiteSpace ( dto.RepetitionDelimiter ) )
            {
                payorType.ReviseHealthCareClaim837Setup (
                    new HealthCareClaim837Setup (
                        dto.InterchangeReceiverNumber,
                        dto.InterchangeSenderNumber,
                        new X12Delimiters (
                            dto.CompositeDelimiter,
                            dto.ElementDelimiter,
                            dto.SegmentDelimiter,
                            dto.RepetitionDelimiter ) ) );
            }
            else
            {
                payorType.ReviseHealthCareClaim837Setup ( null );
            }

            var stateProvinceLookup = _mappingHelper.MapLookupField<StateProvince> ( dto.StateProvince );

            if ( !string.IsNullOrWhiteSpace ( dto.FirstStreetAddress ) ||
                 !string.IsNullOrWhiteSpace ( dto.SecondStreetAddress ) ||
                 !string.IsNullOrWhiteSpace ( dto.CityName ) ||
                 stateProvinceLookup != null )
            {
                var address = new AddressBuilder ()
                    .WithFirstStreetAddress ( dto.FirstStreetAddress )
                    .WithSecondStreetAddress ( dto.SecondStreetAddress )
                    .WithCityName ( dto.CityName )
                    .WithStateProvince ( stateProvinceLookup )
                    .WithPostalCode (
                        string.IsNullOrWhiteSpace ( dto.PostalCode )
                            ? null
                            : new PostalCode ( dto.PostalCode ) )
                    .Build ();

                payorType.ReviseBillingAddress ( address );
            }
            else
            {
                payorType.ReviseBillingAddress ( null );
            }

            payorType.ReviseBillingPhone (
                string.IsNullOrWhiteSpace ( dto.PhoneNumber ) && string.IsNullOrWhiteSpace ( dto.PhoneExtensionNumber )
                    ? null
                    : new Phone ( dto.PhoneNumber, dto.PhoneExtensionNumber ) );

            return true;
        }

        #endregion
    }
}
