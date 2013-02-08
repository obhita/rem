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

using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.AgencyModule.Web.AgencyEditor
{
    /// <summary>
    /// Class for handling save agency contacts request.
    /// </summary>
    public class SaveAgencyContactsRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<AgencyContactsDto>, DtoResponse<AgencyContactsDto>, AgencyContactsDto, Agency>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private readonly IStaffRepository _staffRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveAgencyContactsRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <param name="staffRepository">The staff repository.</param>
        public SaveAgencyContactsRequestHandler ( IDtoToDomainMappingHelper mappingHelper, IStaffRepository staffRepository )
        {
            _mappingHelper = mappingHelper;
            _staffRepository = staffRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="agencyContactsDto">The agency contacts dto.</param>
        /// <param name="agency">The agency.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( AgencyContactsDto agencyContactsDto, Agency agency )
        {
            var processSucceeded =
                new AggregateNodeCollectionMapper<AgencyContactDto, Agency, AgencyContact> (
                    agencyContactsDto.AgencyContacts, agency, agency.AgencyContacts ).MapRemovedItem ( RemoveAgencyContact ).MapAddedItem (
                        AddAgencyContact ).MapChangedItem ( ChangeAgencyContact ).Map ();

            return processSucceeded;
        }

        private static void RemoveAgencyContact ( AgencyContactDto agencyContactDto, Agency agency, AgencyContact agencyContact )
        {
            agency.RemoveContacts ( agencyContact );
        }

        private void AddAgencyContact ( AgencyContactDto agencyContactDto, Agency agency )
        {
            var staff = agencyContactDto.ContactStaff != null ? _staffRepository.GetByKey ( agencyContactDto.ContactStaff.Key ) : null;

            var agencyContactType = _mappingHelper.MapLookupField<AgencyContactType> ( agencyContactDto.AgencyContactType );

            var agencyContact =
                new AgencyContactBuilder ().WithAgencyContactType ( agencyContactType ).WithContactStaff ( staff ).WithEffectiveStartDate (
                    agencyContactDto.EffectiveStartDate ).WithStatusIndicator ( agencyContactDto.StatusIndicator ).Build ();

            agency.AddContact ( agencyContact );
        }

        private void ChangeAgencyContact ( AgencyContactDto agencyContactDto, Agency agency, AgencyContact agencyContact )
        {
            RemoveAgencyContact ( agencyContactDto, agency, agencyContact );
            AddAgencyContact ( agencyContactDto, agency );
        }

        #endregion
    }
}
