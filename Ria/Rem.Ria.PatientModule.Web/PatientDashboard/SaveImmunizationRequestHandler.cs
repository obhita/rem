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

using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.ImmunizationModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Extension;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling save immunization request.
    /// </summary>
    public class SaveImmunizationRequestHandler :
        SaveDtoRequestHandlerBase<SaveDtoRequest<ImmunizationDto>, DtoResponse<ImmunizationDto>, ImmunizationDto>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveImmunizationRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveImmunizationRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the specified immunization dto.
        /// </summary>
        /// <param name="immunizationDto">The immunization dto.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool Process ( ImmunizationDto immunizationDto )
        {
            var immunization = Session.Get<Immunization> ( immunizationDto.Key );

            CodedConcept vaccineCodedConcept = null;
            if ( immunizationDto.VaccineCodedConcept != null )
            {
                vaccineCodedConcept = new CodedConceptBuilder ().WithCodedConceptDto ( immunizationDto.VaccineCodedConcept );
            }

            var unitOfMeasure = _mappingHelper.MapLookupField<ImmunizationUnitOfMeasure> ( immunizationDto.ImmunizationUnitOfMeasure );
            var notGivenReason = _mappingHelper.MapLookupField<ImmunizationNotGivenReason> ( immunizationDto.ImmunizationNotGivenReason );

            immunization.ReviseImmunizationVaccineInfo (
                new ImmunizationVaccineInfo (
                    vaccineCodedConcept,
                    immunizationDto.VaccineLotNumber,
                    new ImmunizationVaccineManufacturer ( immunizationDto.VaccineManufacturerCode, immunizationDto.VaccineManufacturerName ) ) );
            immunization.ReviseImmunizationAdministration ( new ImmunizationAdministration ( immunizationDto.AdministeredAmount, unitOfMeasure ) );
            immunization.ReviseImmunizationNotGivenReason ( notGivenReason );

            return true;
        }

        #endregion
    }
}
