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
using System.Linq;
using Pillar.Domain.Primitives;
using Rem.Domain.Billing.BillingOfficeModule;
using Rem.Domain.Billing.PayorModule;
using Rem.Infrastructure.Service;

namespace Rem.Ria.BillingModule.Web.PayorEditor
{
    /// <summary>
    /// Class for handling save payor profile dto request.
    /// </summary>
    public class SavePayorProfileDtoRequestHandler :
        SaveAggregateDtoRequestHandlerBase
            <SaveDtoRequest<PayorProfileDto>, DtoResponse<PayorProfileDto>, PayorProfileDto, Payor>
    {
        #region Constants and Fields

        private readonly IBillingOfficeRepository _billingOfficeRepository;
        private readonly IPayorTypeRepository _payorTypeRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SavePayorProfileDtoRequestHandler"/> class.
        /// </summary>
        /// <param name="billingOfficeRepository">The billing office repository.</param>
        /// <param name="payorTypeRepository">The payor type repository.</param>
        public SavePayorProfileDtoRequestHandler ( IBillingOfficeRepository billingOfficeRepository, IPayorTypeRepository payorTypeRepository )
        {
            _billingOfficeRepository = billingOfficeRepository;
            _payorTypeRepository = payorTypeRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>A <see cref="Rem.Domain.Billing.PayorModule.Payor"/></returns>
        protected override Payor CreateNew ( PayorProfileDto dto )
        {
            var billingOffice = _billingOfficeRepository.GetByKey ( dto.BillingOfficeKey );
            var payor = billingOffice.AddPayor ( dto.Name, Guid.NewGuid ().ToString () );
            return payor;
        }

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="payor">The payor.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( PayorProfileDto dto, Payor payor )
        {
            payor.ReviseName ( dto.Name );

            if ( dto.EffectiveDate == null && dto.EndDate == null )
            {
                payor.ReviseEffectiveDateRange ( null );
            }
            else
            {
                payor.ReviseEffectiveDateRange ( new DateRange ( dto.EffectiveDate, dto.EndDate ) );
            }

            payor.ReviseEmailAddress ( string.IsNullOrEmpty ( dto.EmailAddress ) ? null : new EmailAddress ( dto.EmailAddress ) );
            payor.RevisePayorIdentifier ( dto.PayorIdentifier );

            if ( dto.PayorTypes != null )
            {
                foreach ( var payorTypeMember in payor.PayorTypeMembers )
                {
                    var found = dto.PayorTypes.Any ( m => payorTypeMember.PayorType.Key == m.Key );
                    if ( !found )
                    {
                        //remove it
                        payor.RemovePayorTypeMember ( payorTypeMember );
                    }
                }

                foreach ( var payorTypeMember in dto.PayorTypes )
                {
                    var found = payor.PayorTypeMembers.Any ( m => payorTypeMember.Key == m.PayorType.Key );
                    if ( !found )
                    {
                        //get the type and add it
                        var payorType = _payorTypeRepository.GetByKey ( payorTypeMember.Key );
                        payor.AddPayorTypeMember ( payorType );
                    }
                }

                payor.RevisePrimaryPayorTypeMember (
                    dto.PrimaryPayorType == null ? null : _payorTypeRepository.GetByKey ( dto.PrimaryPayorType.Key ) );
            }
            return true;
        }

        #endregion
    }
}
