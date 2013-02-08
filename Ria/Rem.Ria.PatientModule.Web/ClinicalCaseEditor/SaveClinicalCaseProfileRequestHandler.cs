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
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.ClinicalCaseEditor
{
    /// <summary>
    /// Class for handling save clinical case profile request.
    /// </summary>
    public class SaveClinicalCaseProfileRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<ClinicalCaseProfileDto>, DtoResponse<ClinicalCaseProfileDto>, ClinicalCaseProfileDto, ClinicalCase>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveClinicalCaseProfileRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveClinicalCaseProfileRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="clinicalCase">The clinical case.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( ClinicalCaseProfileDto dto, ClinicalCase clinicalCase )
        {
            _mappingResult &= ReviseProfile ( dto, clinicalCase );
            _mappingResult &= MapSignedComments ( dto, clinicalCase );
            _mappingResult &= MapSpecialInitiatives ( dto, clinicalCase );
            _mappingResult &= MapPriorityPopulations ( dto, clinicalCase );

            return _mappingResult;
        }

        private void AddClinicalCaseSignedComment ( ClinicalCaseSignedCommentDto dto, ClinicalCase clinicalCase )
        {
            var staff = Session.Load<Staff> ( dto.Staff.Key );
            clinicalCase.AddSignedComment ( new ClinicalCaseSignedComment ( staff, DateTime.UtcNow, dto.SignedNote ) );
        }

        private void AddPriorityPopulation ( LookupValueDto priorityPopulationDto, ClinicalCase clinicalCase )
        {
            var priorityPopulation = _mappingHelper.MapLookupField<PriorityPopulation> ( priorityPopulationDto );
            var clinicalCasePriorityPopulation = new ClinicalCasePriorityPopulation ( priorityPopulation );
            clinicalCase.AddPriorityPopulation ( clinicalCasePriorityPopulation );
        }

        private void AddSpecialInitiative ( LookupValueDto specialInitiativeDto, ClinicalCase clinicalCase )
        {
            var specialInitiative = _mappingHelper.MapLookupField<SpecialInitiative> ( specialInitiativeDto );
            var clinicalCaseSpecialInitiative = new ClinicalCaseSpecialInitiative ( specialInitiative );
            clinicalCase.AddSpecialInitiative ( clinicalCaseSpecialInitiative );
        }

        private void ChangeClinicalCaseSignedComment (
            ClinicalCaseSignedCommentDto dto,
            ClinicalCase clinicalCase,
            ClinicalCaseSignedComment clinicalCaseSignedComment )
        {
            RemoveClinicalCaseSignedComment ( dto, clinicalCase, clinicalCaseSignedComment );
            AddClinicalCaseSignedComment ( dto, clinicalCase );
        }

        private bool MapPriorityPopulations ( ClinicalCaseProfileDto dto, ClinicalCase clinicalCase )
        {
            var priorityPopulationResult = new AggregateNodeLookupCollectionMapper<LookupValueDto, ClinicalCase, ClinicalCasePriorityPopulation> (
                dto.PriorityPopulations,
                clinicalCase,
                clinicalCase.PriorityPopulations )
                .MapAddedItem ( AddPriorityPopulation )
                .MapRemovedItem (
                    ( priorityPopulationDto, localClinicalCase, priorityPopulation ) =>
                    localClinicalCase.RemovePriorityPopulation ( priorityPopulation ) )
                .FindCollectionEntity ( ( pr, key ) => clinicalCase.PriorityPopulations.FirstOrDefault ( r => r.PriorityPopulation.Key == key ) )
                .Map ();

            return priorityPopulationResult;
        }

        private bool MapSignedComments ( ClinicalCaseProfileDto dto, ClinicalCase clinicalCase )
        {
            var result =
                new AggregateNodeCollectionMapper<ClinicalCaseSignedCommentDto, ClinicalCase, ClinicalCaseSignedComment> (
                    dto.SignedComments, clinicalCase, clinicalCase.SignedComments )
                    .MapRemovedItem ( RemoveClinicalCaseSignedComment )
                    .MapAddedItem ( AddClinicalCaseSignedComment )
                    .MapChangedItem ( ChangeClinicalCaseSignedComment )
                    .Map ();

            return result;
        }

        private bool MapSpecialInitiatives ( ClinicalCaseProfileDto dto, ClinicalCase clinicalCase )
        {
            var specialInitiativeResult = new AggregateNodeLookupCollectionMapper<LookupValueDto, ClinicalCase, ClinicalCaseSpecialInitiative> (
                dto.SpecialInitiatives,
                clinicalCase,
                clinicalCase.SpecialInitiatives )
                .MapAddedItem ( AddSpecialInitiative )
                .MapRemovedItem (
                    ( specialInitiativeDto, localClinicalCase, specialInitiative ) => localClinicalCase.RemoveSpecialInitiative ( specialInitiative ) )
                .FindCollectionEntity ( ( pr, key ) => clinicalCase.SpecialInitiatives.FirstOrDefault ( r => r.SpecialInitiative.Key == key ) )
                .Map ();

            return specialInitiativeResult;
        }

        private void RemoveClinicalCaseSignedComment (
            ClinicalCaseSignedCommentDto dto,
            ClinicalCase clinicalCase,
            ClinicalCaseSignedComment clinicalCaseSignedComment )
        {
            clinicalCase.DeleteSignedComment ( clinicalCaseSignedComment );
        }

        private bool ReviseProfile ( ClinicalCaseProfileDto dto, ClinicalCase clinicalCase )
        {
            var initialLocation = Session.Load<Location> ( dto.InitialLocation.Key );
            var performedByStaff =
                dto.PerformedByStaff != null ? Session.Load<Staff> ( dto.PerformedByStaff.Key ) : null;
            var referralType = _mappingHelper.MapLookupField<ReferralType> ( dto.ReferralType );
            var initialContactMethod = _mappingHelper.MapLookupField<InitialContactMethod> ( dto.InitialContactMethod );

            var profile = new ClinicalCaseProfileBuilder ()
                .WithClinicalCaseStartDate ( dto.ClinicalCaseStartDate )
                .WithInitialContactMethod ( initialContactMethod )
                .WithInitialLocation ( initialLocation )
                .WithPatientPresentingProblemNote ( dto.PatientPresentingProblemNote )
                .WithPerformedByStaff ( performedByStaff )
                .WithReferralType ( referralType );

            clinicalCase.ReviseClinicalCaseProfile ( profile );

            return true;
        }

        #endregion
    }
}
