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
using Rem.Infrastructure.Service;

namespace Rem.Ria.BillingModule.Web.BillingOfficeEditor
{
    /// <summary>
    /// Class for handling save billing office profile dto request.
    /// </summary>
    public class SaveBillingOfficeProfileDtoRequestHandler :
        SaveAggregateDtoRequestHandlerBase
            <SaveDtoRequest<BillingOfficeProfileDto>, DtoResponse<BillingOfficeProfileDto>, BillingOfficeProfileDto, BillingOffice>
    {
        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="billingOffice">The billing office.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( BillingOfficeProfileDto dto, BillingOffice billingOffice )
        {
            var billingOfficeProfile = new BillingOfficeProfile (
                dto.Name, new DateRange ( dto.EffectiveDate, dto.EndDate ), dto.EmailAddress == null ? null : new EmailAddress ( dto.EmailAddress ) );

            billingOffice.ReviseProfile ( billingOfficeProfile );

            return true;
        }

        #endregion
    }
}
