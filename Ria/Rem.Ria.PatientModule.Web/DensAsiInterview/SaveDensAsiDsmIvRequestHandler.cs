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
    /// Class for handling save dens asi DSM iv request.
    /// </summary>
    public class SaveDensAsiDsmIvRequestHandler :
        SaveAggregateNodeDtoRequestHandlerBase<SaveDtoRequest<DensAsiDsmIvDto>, DtoResponse<DensAsiDsmIvDto>, DensAsiDsmIvDto, Domain.Clinical.DensAsiModule.DensAsiInterview, DensAsiDsmIv>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveDensAsiDsmIvRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveDensAsiDsmIvRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="densAsiDsmIvDto">The dens asi DSM iv dto.</param>
        /// <param name="densAsiDsmIv">The dens asi DSM iv.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( DensAsiDsmIvDto densAsiDsmIvDto, DensAsiDsmIv densAsiDsmIv )
        {
            var densiAsiDsmIvNew = new DensAsiDsmIvSectionBuilder ()
                .WithAlcoholDependenceLessEffectIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDsmIvDto.AlcoholDependenceLessEffectIndicator, _mappingHelper ) )
                .WithDrugDependenceLessEffectIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDsmIvDto.DrugDependenceLessEffectIndicator, _mappingHelper ) )
                .WithLessEffectIndicatorNote ( densAsiDsmIvDto.LessEffectIndicatorNote )
                .WithAlcoholDependencePhysicalDistressOnQuittingIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDsmIvDto.AlcoholDependencePhysicalDistressOnQuittingIndicator, _mappingHelper ) )
                .WithDrugDependencePhysicalDistressOnQuittingIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDsmIvDto.DrugDependencePhysicalDistressOnQuittingIndicator, _mappingHelper ) )
                .WithPhysicalDistressOnQuittingNote ( densAsiDsmIvDto.PhysicalDistressOnQuittingNote )
                .WithAlcoholDependenceUsedMoreIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDsmIvDto.AlcoholDependenceUsedMoreIndicator, _mappingHelper ) )
                .WithDrugDependenceUsedMoreIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDsmIvDto.DrugDependenceUsedMoreIndicator, _mappingHelper ) )
                .WithUsedMoreIndicatorNote ( densAsiDsmIvDto.UsedMoreIndicatorNote )
                .WithAlcoholDependenceWantedToCutDownIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDsmIvDto.AlcoholDependenceWantedToCutDownIndicator, _mappingHelper ) )
                .WithDrugDependenceWantedToCutDownIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDsmIvDto.DrugDependenceWantedToCutDownIndicator, _mappingHelper ) )
                .WithWantedToCutDownIndicatorNote ( densAsiDsmIvDto.WantedToCutDownIndicatorNote )
                .WithAlcoholDependenceTimeSpentObtainingIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDsmIvDto.AlcoholDependenceTimeSpentObtainingIndicator, _mappingHelper ) )
                .WithDrugDependenceTimeSpentObtainingIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDsmIvDto.DrugDependenceTimeSpentObtainingIndicator, _mappingHelper ) )
                .WithTimeSpentObtainingIndicatorNote ( densAsiDsmIvDto.TimeSpentObtainingIndicatorNote )
                .WithAlcoholDependenceGiveUpWorkFamilyActivitiesIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDsmIvDto.AlcoholDependenceGiveUpWorkFamilyActivitiesIndicator, _mappingHelper ) )
                .WithDrugDependenceGiveUpWorkFamilyActivitiesIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDsmIvDto.DrugDependenceGiveUpWorkFamilyActivitiesIndicator, _mappingHelper ) )
                .WithGiveUpWorkFamilyActivitiesIndicatorNote ( densAsiDsmIvDto.GiveUpWorkFamilyActivitiesIndicatorNote )
                .WithAlcoholDependenceContinuedToUseIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDsmIvDto.AlcoholDependenceContinuedToUseIndicator, _mappingHelper ) )
                .WithDrugDependenceContinuedToUseIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDsmIvDto.DrugDependenceContinuedToUseIndicator, _mappingHelper ) )
                .WithContinuedToUseIndicatorNote ( densAsiDsmIvDto.ContinuedToUseIndicatorNote )
                .WithSectionNote ( densAsiDsmIvDto.SectionNote )
                .WithAlcoholAbuseInabilityToMeetResponsibilitiesIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDsmIvDto.AlcoholAbuseInabilityToMeetResponsibilitiesIndicator, _mappingHelper ) )
                .WithDrugAbuseInabilityToMeetResponsibilitiesIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDsmIvDto.DrugAbuseInabilityToMeetResponsibilitiesIndicator, _mappingHelper ) )
                .WithInabilityToMeetResponsibilitiesIndicatorNote ( densAsiDsmIvDto.InabilityToMeetResponsibilitiesIndicatorNote )
                .WithAlcoholAbuseVoluntaryPhysicalDangerIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDsmIvDto.AlcoholAbuseVoluntaryPhysicalDangerIndicator, _mappingHelper ) )
                .WithDrugAbuseVoluntaryPhysicalDangerIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDsmIvDto.DrugAbuseVoluntaryPhysicalDangerIndicator, _mappingHelper ) )
                .WithVoluntaryPhysicalDangerIndicatorNote ( densAsiDsmIvDto.VoluntaryPhysicalDangerIndicatorNote )
                .WithAlcoholAbuseLegalProblemsIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDsmIvDto.AlcoholAbuseLegalProblemsIndicator, _mappingHelper ) )
                .WithDrugAbuseLegalProblemsIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDsmIvDto.DrugAbuseLegalProblemsIndicator, _mappingHelper ) )
                .WithLegalProblemsIndicatorNote ( densAsiDsmIvDto.LegalProblemsIndicatorNote )
                .WithAlcoholAbuseSocialProblemsIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDsmIvDto.AlcoholAbuseSocialProblemsIndicator, _mappingHelper ) )
                .WithDrugAbuseSocialProblemsIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDsmIvDto.DrugAbuseSocialProblemsIndicator, _mappingHelper ) )
                .WithSocialProblemsIndicatorNote ( densAsiDsmIvDto.SocialProblemsIndicatorNote )
                .Build ();
            AggregateRoot.ReviseDensAsiDsmIv ( densiAsiDsmIvNew );

            densAsiDsmIvDto.Key = AggregateRoot.DensAsiDsmIv.Key;

            return true;
        }

        #endregion
    }
}
