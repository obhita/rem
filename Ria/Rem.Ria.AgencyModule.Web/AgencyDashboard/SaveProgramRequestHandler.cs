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
using Pillar.Common.Utility;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.ProgramModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.AgencyModule.Web.AgencyDashboard
{
    /// <summary>
    /// Class for handling save program request.
    /// </summary>
    public class SaveProgramRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<ProgramDto>, DtoResponse<ProgramDto>, ProgramDto, Program>
    {
        #region Constants and Fields

        private readonly IAgencyRepository _agencyRepository;
        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private readonly IProgramFactory _programFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveProgramRequestHandler"/> class.
        /// </summary>
        /// <param name="agencyRepository">The agency repository.</param>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <param name="programFactory">The program factory.</param>
        public SaveProgramRequestHandler (
            IAgencyRepository agencyRepository, IDtoToDomainMappingHelper mappingHelper, IProgramFactory programFactory )
        {
            _agencyRepository = agencyRepository;
            _mappingHelper = mappingHelper;
            _programFactory = programFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="dto">The dto to create new for.</param>
        /// <returns>A <see cref="Rem.Domain.Clinical.ProgramModule.Program"/></returns>
        protected override Program CreateNew ( ProgramDto dto )
        {
            Check.IsNotNull ( dto.StartDate, "Start Date is required." );

            var agency = _agencyRepository.GetByKey ( dto.AgencyKey );
            if ( agency == null )
            {
                throw new InvalidOperationException ( "Agency does not exist" );
            }

            var ageGroup = _mappingHelper.MapLookupField<AgeGroup> ( dto.AgeGroup );
            var genderSpecification = _mappingHelper.MapLookupField<GenderSpecification> ( dto.GenderSpecification );
            var treatmentApproach = _mappingHelper.MapLookupField<TreatmentApproach> ( dto.TreatmentApproach );
            var programCategory = _mappingHelper.MapLookupField<ProgramCategory> ( dto.ProgramCategory );
            var programCharacteristics = new ProgramCharacteristics ( ageGroup, genderSpecification, treatmentApproach, programCategory );
            var entity = _programFactory.CreateProgram ( agency, dto.Name, dto.StartDate.Value, programCharacteristics );

            return entity;
        }

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="program">The program.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( ProgramDto dto, Program program )
        {
            Check.IsNotNull ( dto.StartDate, "Start Date is required." );

            var ageGroup = _mappingHelper.MapLookupField<AgeGroup> ( dto.AgeGroup );
            var genderSpecification = _mappingHelper.MapLookupField<GenderSpecification> ( dto.GenderSpecification );
            var treatmentApproach = _mappingHelper.MapLookupField<TreatmentApproach> ( dto.TreatmentApproach );
            var programCategory = _mappingHelper.MapLookupField<ProgramCategory> ( dto.ProgramCategory );
            var programCharacteristics = new ProgramCharacteristics ( ageGroup, genderSpecification, treatmentApproach, programCategory );

            var capacityType = _mappingHelper.MapLookupField<CapacityType> ( dto.CapacityType );

            program.ReviseProgramCharacteristics ( programCharacteristics );
            if ( program.Name != dto.Name )
            {
                program.RenameProgram ( dto.Name );
            }
            program.RenameDisplayName ( dto.DisplayName );
            program.ChangeStartDate ( dto.StartDate.Value );
            program.ChangeEndDate ( dto.EndDate );
            program.ChangeCapacityType ( capacityType );

            return true;
        }

        #endregion
    }
}
