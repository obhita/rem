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
using AutoMapper;
using Pillar.Common.Collections;
using Rem.Domain.Billing.PayorModule;
using Rem.Infrastructure.Domain;
using Rem.Ria.Infrastructure.Web.DataTransferObject;

namespace Rem.Ria.BillingModule.Web.PayorEditor
{
    /// <summary>
    /// Factory for payor phone numbers dto.
    /// </summary>
    public class PayorPhoneNumbersDtoFactory : DtoFactoryBase<Payor, PayorPhoneNumbersDto>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorPhoneNumbersDtoFactory"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public PayorPhoneNumbersDtoFactory ( ISessionProvider sessionProvider )
            : base ( sessionProvider )
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the mapping.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A <see cref="Rem.Ria.BillingModule.Web.PayorEditor.PayorPhoneNumbersDto"/></returns>
        protected override PayorPhoneNumbersDto HandleMapping ( Payor entity )
        {
            var resultDtos =
                Mapper.Map<IList<PayorPhone>, IList<PayorPhoneDto>> ( new List<PayorPhone> ( entity.PhoneNumbers ) );

            var responseDtos = new SoftDeleteObservableCollection<PayorPhoneDto> ( resultDtos );
            var dto = new PayorPhoneNumbersDto { PhoneNumbers = responseDtos, Key = entity.Key };
            return dto;
        }

        #endregion
    }
}
