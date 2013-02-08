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
using Rem.Domain.Billing.PayorModule;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.BillingModule.Web.PayorEditor
{
    /// <summary>
    /// Class for handling save payor phone numbers dto request.
    /// </summary>
    public class SavePayorPhoneNumbersDtoRequestHandler :
        SaveAggregateDtoRequestHandlerBase
            <SaveDtoRequest<PayorPhoneNumbersDto>, DtoResponse<PayorPhoneNumbersDto>, PayorPhoneNumbersDto, Payor>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SavePayorPhoneNumbersDtoRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SavePayorPhoneNumbersDtoRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>A payor.</returns>
        protected override Payor CreateNew ( PayorPhoneNumbersDto dto )
        {
            throw new ApplicationException ( "Cannot save phone numbers for a payor which doesn’t exist yet." );
        }

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="payor">The payor.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( PayorPhoneNumbersDto dto, Payor payor )
        {
            _mappingResult &=
                new AggregateNodeCollectionMapper<PayorPhoneDto, Payor, PayorPhone> (
                    dto.PhoneNumbers, payor, payor.PhoneNumbers )
                    .MapRemovedItem ( RemovePayorPhone )
                    .MapAddedItem ( AddPayorPhone )
                    .MapChangedItem ( ChangePayorPhone )
                    .Map ();

            return _mappingResult;
        }

        private static void RemovePayorPhone (
            PayorPhoneDto payorPhoneDto, Payor payor, PayorPhone payorPhone )
        {
            payor.RemovePayorPhoneNumber ( payorPhone );
        }

        private void AddPayorPhone ( PayorPhoneDto payorPhoneDto, Payor payor )
        {
            var phoneType = _mappingHelper.MapLookupField<PayorPhoneType> ( payorPhoneDto.PayorPhoneType );

            var phoneNumber = new PayorPhone ( phoneType, new Phone ( payorPhoneDto.PhoneNumber, payorPhoneDto.PhoneExtensionNumber ) );

            payor.AddPayorPhoneNumber ( phoneNumber );
        }

        private void ChangePayorPhone (
            PayorPhoneDto payorPhoneDto, Payor payor, PayorPhone payorPhone )
        {
            RemovePayorPhone ( payorPhoneDto, payor, payorPhone );
            AddPayorPhone ( payorPhoneDto, payor );
        }

        #endregion
    }
}
