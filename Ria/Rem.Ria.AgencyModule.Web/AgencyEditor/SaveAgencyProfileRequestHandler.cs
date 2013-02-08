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
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.AgencyModule.Web.AgencyEditor
{
    /// <summary>
    /// Class for handling save agency profile request.
    /// </summary>
    public class SaveAgencyProfileRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<AgencyProfileDto>, DtoResponse<AgencyProfileDto>, AgencyProfileDto, Agency>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveAgencyProfileRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveAgencyProfileRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="agencyProfileDto">The agency profile dto.</param>
        /// <param name="agency">The agency.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( AgencyProfileDto agencyProfileDto, Agency agency )
        {
            var processSucceeded = true;

            var geographicalRegion = _mappingHelper.MapLookupField<GeographicalRegion> ( agencyProfileDto.GeographicalRegion );
            var agencyType = _mappingHelper.MapLookupField<AgencyType> ( agencyProfileDto.AgencyType );
            var parentAgency = agencyProfileDto.ParentAgency != null ? Session.Get<Agency> ( agencyProfileDto.ParentAgency.Key ) : null;

            agency.ReviseAgencyProfile (
                new AgencyProfileBuilder ().WithAgencyType ( agencyType ).WithAgencyName (
                    new AgencyNameBuilder ().WithLegalName ( agencyProfileDto.LegalName ).WithDisplayName ( agencyProfileDto.DisplayName )
                    .WithDoingBusinessAsName ( agencyProfileDto.DoingBusinessAsName ).Build () ).WithEffectiveDateRange (
                            new DateRange ( agencyProfileDto.StartDate, agencyProfileDto.EndDate ) ).WithGeographicalRegion ( geographicalRegion ) );

            agency.ReviseParentAgency ( parentAgency );

            processSucceeded &= MapAliasCollection ( agency, agencyProfileDto );
            processSucceeded &= MapEmailCollection ( agency, agencyProfileDto );
            return processSucceeded;
        }

        private static void AddAgencyAlias ( AgencyAliasDto agencyAliasDto, Agency agency )
        {
            agency.AddAgencyAlias ( new AgencyAliasBuilder ().WithName ( agencyAliasDto.Name ).WithNote ( agencyAliasDto.Note ).Build () );
        }

        private static void ChangeAgencyAlias ( AgencyAliasDto agencyAliasDto, Agency agency, AgencyAlias agencyAlias )
        {
            RemoveAgencyAlias ( agencyAliasDto, agency, agencyAlias );
            AddAgencyAlias ( agencyAliasDto, agency );
        }

        private static bool MapAliasCollection ( Agency agency, AgencyProfileDto agencyProfileDto )
        {
            var result =
                new AggregateNodeCollectionMapper<AgencyAliasDto, Agency, AgencyAlias> (
                    agencyProfileDto.AgencyAliases, agency, agency.AgencyAliases )
                    .MapRemovedItem ( RemoveAgencyAlias )
                    .MapAddedItem ( AddAgencyAlias )
                    .MapChangedItem ( ChangeAgencyAlias )
                    .Map ();

            return result;
        }

        private static void RemoveAgencyAlias ( AgencyAliasDto agencyAliasDto, Agency agency, AgencyAlias agencyAlias )
        {
            agency.RemoveAgencyAlias ( agencyAlias );
        }

        private static void RemoveAgencyEmailAddress (
            AgencyEmailAddressDto agencyEmailAddressDto, Agency agency, AgencyEmailAddress agencyEmailAddress )
        {
            agency.RemoveEmailAddress ( agencyEmailAddress );
        }

        private void AddAgencyEmailAddress ( AgencyEmailAddressDto agencyEmailAddressDto, Agency agency )
        {
            var emailAddressType = _mappingHelper.MapLookupField<AgencyEmailAddressType> ( agencyEmailAddressDto.AgencyEmailAddressType );

            agency.AddEmailAddress ( new AgencyEmailAddress ( new EmailAddress ( agencyEmailAddressDto.EmailAddress ), emailAddressType ) );
        }

        private void ChangeAgencyEmailAddress ( AgencyEmailAddressDto agencyEmailAddressDto, Agency agency, AgencyEmailAddress agencyEmailAddress )
        {
            RemoveAgencyEmailAddress ( agencyEmailAddressDto, agency, agencyEmailAddress );
            AddAgencyEmailAddress ( agencyEmailAddressDto, agency );
        }

        private bool MapEmailCollection ( Agency agency, AgencyProfileDto agencyProfileDto )
        {
            var result =
                new AggregateNodeCollectionMapper<AgencyEmailAddressDto, Agency, AgencyEmailAddress> (
                    agencyProfileDto.EmailAddresses, agency, agency.EmailAddresses )
                    .MapRemovedItem ( RemoveAgencyEmailAddress )
                    .MapAddedItem ( AddAgencyEmailAddress )
                    .MapChangedItem ( ChangeAgencyEmailAddress )
                    .Map ();

            return result;
        }

        #endregion
    }
}
