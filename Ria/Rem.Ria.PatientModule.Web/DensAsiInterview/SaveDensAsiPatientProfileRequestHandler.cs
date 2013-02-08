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

using Rem.Domain.Clinical.DensAsiModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.DensAsiInterview
{
    /// <summary>
    /// Class for handling save dens asi patient profile request.
    /// </summary>
    public class SaveDensAsiPatientProfileRequestHandler :
        SaveAggregateNodeDtoRequestHandlerBase<SaveDtoRequest<DensAsiPatientProfileDto>, DtoResponse<DensAsiPatientProfileDto>, DensAsiPatientProfileDto,
            Domain.Clinical.DensAsiModule.DensAsiInterview, DensAsiPatientProfile>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveDensAsiPatientProfileRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveDensAsiPatientProfileRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="densAsiPatientProfileDto">The dens asi patient profile dto.</param>
        /// <param name="densAsiPatientProfile">The dens asi patient profile.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate (
            DensAsiPatientProfileDto densAsiPatientProfileDto, DensAsiPatientProfile densAsiPatientProfile )
        {
            var densAsiInterviewClass = _mappingHelper.MapLookupField<DensAsiInterviewClass> ( densAsiPatientProfileDto.DensAsiInterviewClass );

            var densAsiPatietnProfileNew = new DensAsiPatientProfileSectionBuilder ()
                .WithInterviewDate ( densAsiPatientProfileDto.InterviewDate )
                .WithInterviewDateNote ( densAsiPatientProfileDto.InterviewDateNote )
                .WithDensAsiInterviewClass ( densAsiInterviewClass )
                .WithDensAsiInterviewClassNote ( densAsiPatientProfileDto.DensAsiInterviewClassNote )
                .WithDensAsiInterviewContactType (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiInterviewContactType> (
                        densAsiPatientProfileDto.DensAsiInterviewContactType, _mappingHelper ) )
                .WithDensAsiInterviewContactTypeNote ( densAsiPatientProfileDto.DensAsiInterviewContactTypeNote )
                .WithYearsAndMonthsAtCurrentAddressTimeSpan (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPatientProfileDto.YearsAndMonthsAtCurrentAddressTimeSpan, _mappingHelper ) )
                .WithYearsAndMonthsAtCurrentAddressTimeSpanNote ( densAsiPatientProfileDto.YearsAndMonthsAtCurrentAddressTimeSpanNote )
                .WithResidenceOwnedByYouOrFamilyIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPatientProfileDto.ResidenceOwnedByYouOrFamilyIndicator, _mappingHelper ) )
                .WithResidenceOwnedByYouOrFamilyIndicatorNote ( densAsiPatientProfileDto.ResidenceOwnedByYouOrFamilyIndicatorNote )
                .WithPreferredDensAsiReligion (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiReligion> (
                        densAsiPatientProfileDto.PreferredDensAsiReligion, _mappingHelper ) )
                .WithPreferredDensAsiReligionNote ( densAsiPatientProfileDto.PreferredDensAsiReligionNote )
                .WithLastThirtyDaysDensAsiControlledEnvironment (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiControlledEnvironment> (
                        densAsiPatientProfileDto.LastThirtyDaysDensAsiControlledEnvironment, _mappingHelper ) )
                .WithLastThirtyDaysDensAsiControlledEnvironmentNote ( densAsiPatientProfileDto.LastThirtyDaysDensAsiControlledEnvironmentNote )
                .WithLastThirtyDaysControlledEnvironmentDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPatientProfileDto.LastThirtyDaysControlledEnvironmentDayCount, _mappingHelper ) )
                .WithLastThirtyDaysControlledEnvironmentDayCountNote ( densAsiPatientProfileDto.LastThirtyDaysControlledEnvironmentDayCountNote )
                .WithChristianReligionIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiPatientProfileDto.ChristianReligionIndicator, _mappingHelper ) )
                .WithChristianReligionIndicatorNote ( densAsiPatientProfileDto.ChristianReligionIndicatorNote )
                .WithBuddhismReligionIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiPatientProfileDto.BuddhismReligionIndicator, _mappingHelper ) )
                .WithBuddhismReligionIndicatorNote ( densAsiPatientProfileDto.BuddhismReligionIndicatorNote )
                .WithNoParticularReligiousSectIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPatientProfileDto.NoParticularReligiousSectIndicator, _mappingHelper ) )
                .WithNoParticularReligiousSectIndicatorNote ( densAsiPatientProfileDto.NoParticularReligiousSectIndicatorNote )
                .Build ();

            AggregateRoot.ReviseDensAsiPatientProfile ( densAsiPatietnProfileNew );
            densAsiPatientProfileDto.Key = AggregateRoot.DensAsiPatientProfile.Key;

            return true;
        }

        #endregion
    }
}
