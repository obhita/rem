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

using System.Linq;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Extension;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.PatientEditor
{
    /// <summary>
    /// Class for handling save patient allergies request.
    /// </summary>
    public class SavePatientAllergiesRequestHandler :
        SaveDtoRequestHandlerBase<SaveDtoRequest<PatientAllergiesDto>, DtoResponse<PatientAllergiesDto>, PatientAllergiesDto>
    {
        #region Constants and Fields

        private readonly IAllergyFactory _allergyFactory;
        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;
        private long _patientKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SavePatientAllergiesRequestHandler"/> class.
        /// </summary>
        /// <param name="allergyFactory">The allergy factory.</param>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SavePatientAllergiesRequestHandler (
            IAllergyFactory allergyFactory,
            IDtoToDomainMappingHelper mappingHelper )
        {
            _allergyFactory = allergyFactory;
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the specified dto.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool Process ( PatientAllergiesDto dto )
        {
            _patientKey = dto.Key;

            _mappingResult &= new AggregateRootCollectionMapper<AllergyDto, Allergy> ( dto.Allergies )
                .MapAddedItem ( AddAllergy )
                .MapChangedItem ( ChangeAllergy )
                .MapRemovedItem ( RemoveAllergy )
                .FindCollectionEntity ( FindAllergy )
                .Map ();

            return _mappingResult;
        }

        private void AddAllergy ( AllergyDto allergyDto )
        {
            var patient = Session.Get<Patient> ( _patientKey );
            var allergyStatus = _mappingHelper.MapLookupField<AllergyStatus> ( allergyDto.AllergyStatus );

            CodedConcept allergen = null;
            if ( allergyDto.AllergenCodedConcept != null )
            {
                allergen = new CodedConceptBuilder ().WithCodedConceptDto ( allergyDto.AllergenCodedConcept );
            }

            var allergy = _allergyFactory.CreateAllergy ( patient, allergyStatus, allergen );

            var mapResult = MapAllergyProperties ( allergyDto, allergy );

            _mappingResult &= mapResult;
        }

        private void ChangeAllergy ( AllergyDto allergyDto, Allergy allergy )
        {
            var mapResult = MapAllergyProperties ( allergyDto, allergy );

            _mappingResult &= mapResult;
        }

        private Allergy FindAllergy ( long key )
        {
            var allergy = Session.Get<Allergy> ( key );
            return allergy;
        }

        private bool MapAllergyProperties ( AllergyDto allergyDto, Allergy allergy )
        {
            var allergySeverityType = _mappingHelper.MapLookupField<AllergySeverityType> ( allergyDto.AllergySeverityType );
            var allergyType = _mappingHelper.MapLookupField<AllergyType> ( allergyDto.AllergyType );
            var allergyStatus = _mappingHelper.MapLookupField<AllergyStatus> ( allergyDto.AllergyStatus );

            CodedConcept allergen = null;
            if ( allergyDto.AllergenCodedConcept != null )
            {
                allergen = new CodedConceptBuilder ().WithCodedConceptDto ( allergyDto.AllergenCodedConcept );
            }

            allergy.ReviseAllergySeverityType ( allergySeverityType );
            allergy.ReviseAllergyType ( allergyType );
            allergy.ReviseOnsetDateRange ( new DateRange ( allergyDto.OnsetStartDate, allergyDto.OnsetEndDate ) );
            allergy.ReviseAllergyStatus ( allergyStatus );
            allergy.ReviseCodedConcept ( allergen );

            // Map reactions
            var deletedReactions = allergy.AllergyReactions.Where (
                a => !allergyDto.AllergyReactions.Any ( ad => ad.Key == a.Reaction.Key ) ).ToList ();
            deletedReactions.ForEach ( allergy.DeleteReaction );

            var addedReactions = allergyDto.AllergyReactions.Where (
                a => !allergy.AllergyReactions.Any ( ad => ad.Reaction.Key == a.Key ) ).ToList ();
            addedReactions.ForEach ( r => allergy.AddReaction ( _mappingHelper.MapLookupField<Reaction> ( r ) ) );

            return true;
        }

        private void RemoveAllergy ( AllergyDto allergyDto, Allergy allergy )
        {
            _allergyFactory.DestroyAllergy ( allergy );
        }

        #endregion
    }
}
