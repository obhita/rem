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
    /// Class for handling save dens asi drug alcohol use request.
    /// </summary>
    public class SaveDensAsiDrugAlcoholUseRequestHandler :
        SaveAggregateNodeDtoRequestHandlerBase<SaveDtoRequest<DensAsiDrugAlcoholUseDto>, DtoResponse<DensAsiDrugAlcoholUseDto>, DensAsiDrugAlcoholUseDto,
            Domain.Clinical.DensAsiModule.DensAsiInterview, DensAsiDrugAlcoholUse>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveDensAsiDrugAlcoholUseRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveDensAsiDrugAlcoholUseRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="densAsiDrugAlcoholUseDto">The dens asi drug alcohol use dto.</param>
        /// <param name="densAsiDrugAlcoholUse">The dens asi drug alcohol use.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate (
            DensAsiDrugAlcoholUseDto densAsiDrugAlcoholUseDto, DensAsiDrugAlcoholUse densAsiDrugAlcoholUse )
        {
            var patientAlcoholTreatmentDensAsiInterviewerRating =
                _mappingHelper.MapLookupField<DensAsiInterviewerRating> ( densAsiDrugAlcoholUseDto.PatientAlcoholTreatmentDensAsiInterviewerRating );
            var patientDrugTreatmentDensAsiInterviewerRating =
                _mappingHelper.MapLookupField<DensAsiInterviewerRating> ( densAsiDrugAlcoholUseDto.PatientAlcoholTreatmentDensAsiInterviewerRating );

            var densAsiDrugAlcoholUseNew = new DensAsiDrugAlcoholUseSectionBuilder ()
                .WithAnyAlcoholUseInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.AnyAlcoholUseInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithAnyAlcoholUseInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.AnyAlcoholUseInLifetimeYearCount, _mappingHelper ) )
                .WithAnyAlcoholDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.AnyAlcoholDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithAnyAlcoholUseNote ( densAsiDrugAlcoholUseDto.AnyAlcoholUseNote )
                .WithAlcoholIntoxicationInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.AlcoholIntoxicationInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithAlcoholIntoxicationUseInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.AlcoholIntoxicationUseInLifetimeYearCount, _mappingHelper ) )
                .WithAlcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.AlcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithAlcoholIntoxicationNote ( densAsiDrugAlcoholUseDto.AlcoholIntoxicationNote )
                .WithHeroinInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.HeroinInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithHeroinInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDrugAlcoholUseDto.HeroinInLifetimeYearCount, _mappingHelper ) )
                .WithHeroinDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.HeroinDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithHeroinNote ( densAsiDrugAlcoholUseDto.HeroinNote )
                .WithMethadoneInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.MethadoneInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithMethadoneInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDrugAlcoholUseDto.MethadoneInLifetimeYearCount, _mappingHelper ) )
                .WithMethadoneDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.MethadoneDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithMethadoneNote ( densAsiDrugAlcoholUseDto.MethadoneNote )
                .WithOtherOpiatesInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.OtherOpiatesInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithOtherOpiatesInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.OtherOpiatesInLifetimeYearCount, _mappingHelper ) )
                .WithOtherOpiatesDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.OtherOpiatesDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithOtherOpiatesNote ( densAsiDrugAlcoholUseDto.OtherOpiatesNote )
                .WithBarbituratesInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.BarbituratesInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithBarbituratesInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.BarbituratesInLifetimeYearCount, _mappingHelper ) )
                .WithBarbituratesDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.BarbituratesDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithBarbituratesNote ( densAsiDrugAlcoholUseDto.BarbituratesNote )
                .WithOtherSedativesInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.OtherSedativesInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithOtherSedativesInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.OtherSedativesInLifetimeYearCount, _mappingHelper ) )
                .WithOtherSedativesDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.OtherSedativesDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithOtherSedativesNote ( densAsiDrugAlcoholUseDto.OtherSedativesNote )
                .WithCocaineInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.CocaineInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithCocaineInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDrugAlcoholUseDto.CocaineInLifetimeYearCount, _mappingHelper ) )
                .WithCocaineDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.CocaineDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithCocaineNote ( densAsiDrugAlcoholUseDto.CocaineNote )
                .WithAmphetaminesInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.AmphetaminesInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithAmphetaminesInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.AmphetaminesInLifetimeYearCount, _mappingHelper ) )
                .WithAmphetaminesDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.AmphetaminesDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithAmphetaminesNote ( densAsiDrugAlcoholUseDto.AmphetaminesNote )
                .WithCannabisInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.CannabisInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithCannabisInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDrugAlcoholUseDto.CannabisInLifetimeYearCount, _mappingHelper ) )
                .WithCannabisDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.CannabisDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithCannabisNote ( densAsiDrugAlcoholUseDto.CannabisNote )
                .WithHallucinogensInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.HallucinogensInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithHallucinogensInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.HallucinogensInLifetimeYearCount, _mappingHelper ) )
                .WithHallucinogensDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.HallucinogensDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithHallucinogensNote ( densAsiDrugAlcoholUseDto.HallucinogensNote )
                .WithInhalantsInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.InhalantsInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithInhalantsInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDrugAlcoholUseDto.InhalantsInLifetimeYearCount, _mappingHelper ) )
                .WithInhalantsDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.InhalantsDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithInhalantsNote ( densAsiDrugAlcoholUseDto.InhalantsNote )
                .WithMoreThanOneSubstancePerDayInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.MoreThanOneSubstancePerDayInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithMoreThanOneSubstancePerDayInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.MoreThanOneSubstancePerDayInLifetimeYearCount, _mappingHelper ) )
                .WithMoreThanOneSubstancePerDayNote ( densAsiDrugAlcoholUseDto.MoreThanOneSubstancePerDayNote )
                .WithMajorDensAsiProblematicSubstance (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiProblematicSubstance> (
                        densAsiDrugAlcoholUseDto.MajorDensAsiProblematicSubstance, _mappingHelper ) )
                .WithMajorDensAsiProblematicSubstanceNote ( densAsiDrugAlcoholUseDto.MajorDensAsiProblematicSubstanceNote )
                .WithVoluntaryAbstinenceFromProblematicSubstanceMonthCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.VoluntaryAbstinenceFromProblematicSubstanceMonthCount, _mappingHelper ) )
                .WithVoluntaryAbstinenceFromProblematicSubstanceMonthCountNote (
                    densAsiDrugAlcoholUseDto.VoluntaryAbstinenceFromProblematicSubstanceMonthCountNote )
                .WithEndOfProblematicSubstanceAbstinenceMonthCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.EndOfProblematicSubstanceAbstinenceMonthCount, _mappingHelper ) )
                .WithEndOfProblematicSubstanceAbstinenceMonthCountNote ( densAsiDrugAlcoholUseDto.EndOfProblematicSubstanceAbstinenceMonthCountNote )
                .WithAlcoholDtCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDrugAlcoholUseDto.AlcoholDtCount, _mappingHelper ) )
                .WithAlcoholDtCountNote ( densAsiDrugAlcoholUseDto.AlcoholDtCountNote )
                .WithOverdosedOnDrugsCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDrugAlcoholUseDto.OverdosedOnDrugsCount, _mappingHelper ) )
                .WithOverdosedOnDrugsCountNote ( densAsiDrugAlcoholUseDto.OverdosedOnDrugsCountNote )
                .WithAlcoholAbuseTreatmentCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDrugAlcoholUseDto.AlcoholAbuseTreatmentCount, _mappingHelper ) )
                .WithAlcoholAbuseTreatmentCountNote ( densAsiDrugAlcoholUseDto.AlcoholAbuseTreatmentCountNote )
                .WithAlcoholDetoxTreatmentOnlyCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.AlcoholDetoxTreatmentOnlyCount, _mappingHelper ) )
                .WithAlcoholDetoxTreatmentOnlyCountNote ( densAsiDrugAlcoholUseDto.AlcoholDetoxTreatmentOnlyCountNote )
                .WithMoneySpentOnAlcoholInLastThirtyDaysAmount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.MoneySpentOnAlcoholInLastThirtyDaysAmount, _mappingHelper ) )
                .WithMoneySpentOnAlcoholInLastThirtyDaysAmountNote ( densAsiDrugAlcoholUseDto.MoneySpentOnAlcoholInLastThirtyDaysAmountNote )
                .WithDrugAbuseTreatmentCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDrugAlcoholUseDto.DrugAbuseTreatmentCount, _mappingHelper ) )
                .WithDrugAbuseTreatmentCountNote ( densAsiDrugAlcoholUseDto.DrugAbuseTreatmentCountNote )
                .WithDrugDetoxTreatmentOnlyCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDrugAlcoholUseDto.DrugDetoxTreatmentOnlyCount, _mappingHelper ) )
                .WithDrugDetoxTreatmentOnlyCountNote ( densAsiDrugAlcoholUseDto.DrugDetoxTreatmentOnlyCountNote )
                .WithMoneySpentOnDrugsInLastThirtyDaysAmount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.MoneySpentOnDrugsInLastThirtyDaysAmount, _mappingHelper ) )
                .WithMoneySpentOnDrugsInLastThirtyDaysAmountNote ( densAsiDrugAlcoholUseDto.MoneySpentOnDrugsInLastThirtyDaysAmountNote )
                .WithOutpatientTreatmentInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.OutpatientTreatmentInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithOutpatientTreatmentInLastThirtyDaysDayCountNote ( densAsiDrugAlcoholUseDto.OutpatientTreatmentInLastThirtyDaysDayCountNote )
                .WithAlcoholProblemInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.AlcoholProblemInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithAlcoholProblemInLastThirtyDaysDayCountNote ( densAsiDrugAlcoholUseDto.AlcoholProblemInLastThirtyDaysDayCountNote )
                .WithTroubledByAlcoholProblemsDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiDrugAlcoholUseDto.TroubledByAlcoholProblemsDensAsiPatientRating, _mappingHelper ) )
                .WithTroubledByAlcoholProblemsDensAsiPatientRatingNote ( densAsiDrugAlcoholUseDto.TroubledByAlcoholProblemsDensAsiPatientRatingNote )
                .WithImportanceOfAlcoholProblemTreatmentDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiDrugAlcoholUseDto.ImportanceOfAlcoholProblemTreatmentDensAsiPatientRating, _mappingHelper ) )
                .WithImportanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote (
                    densAsiDrugAlcoholUseDto.ImportanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote )
                .WithDrugProblemInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.DrugProblemInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithDrugProblemInLastThirtyDaysDayCountNote ( densAsiDrugAlcoholUseDto.DrugProblemInLastThirtyDaysDayCountNote )
                .WithTroubledByDrugProblemsDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiDrugAlcoholUseDto.TroubledByDrugProblemsDensAsiPatientRating, _mappingHelper ) )
                .WithTroubledByDrugProblemsDensAsiPatientRatingNote ( densAsiDrugAlcoholUseDto.TroubledByDrugProblemsDensAsiPatientRatingNote )
                .WithImportanceOfDrugProblemTreatmentDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiDrugAlcoholUseDto.ImportanceOfDrugProblemTreatmentDensAsiPatientRating, _mappingHelper ) )
                .WithImportanceOfDrugProblemTreatmentDensAsiPatientRatingNote (
                    densAsiDrugAlcoholUseDto.ImportanceOfDrugProblemTreatmentDensAsiPatientRatingNote )
                .WithPatientAlcoholTreatmentDensAsiInterviewerRating ( patientAlcoholTreatmentDensAsiInterviewerRating )
                .WithPatientAlcoholTreatmentDensAsiInterviewerRatingNote (
                    densAsiDrugAlcoholUseDto.PatientAlcoholTreatmentDensAsiInterviewerRatingNote )
                .WithPatientDrugTreatmentDensAsiInterviewerRating ( patientDrugTreatmentDensAsiInterviewerRating )
                .WithPatientDrugTreatmentDensAsiInterviewerRatingNote ( densAsiDrugAlcoholUseDto.PatientDrugTreatmentDensAsiInterviewerRatingNote )
                .WithConfidenceDistortedByPatientMisrepresentationIndicator (
                    densAsiDrugAlcoholUseDto.ConfidenceDistortedByPatientMisrepresentationIndicator )
                .WithConfidenceDistortedByPatientMisrepresentationIndicatorNote (
                    densAsiDrugAlcoholUseDto.ConfidenceDistortedByPatientMisrepresentationIndicatorNote )
                .WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicator (
                    densAsiDrugAlcoholUseDto.ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator )
                .WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote (
                    densAsiDrugAlcoholUseDto.ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote )
                .WithHydromorphoneInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.HydromorphoneInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithHydromorphoneInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.HydromorphoneInLifetimeYearCount, _mappingHelper ) )
                .WithHydromorphoneDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.HydromorphoneDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithHydromorphoneNote ( densAsiDrugAlcoholUseDto.HydromorphoneNote )
                .WithOxycodoneInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.OxycodoneInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithOxycodoneInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDrugAlcoholUseDto.OxycodoneInLifetimeYearCount, _mappingHelper ) )
                .WithOxycodoneDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.OxycodoneDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithOxycodoneNote ( densAsiDrugAlcoholUseDto.OxycodoneNote )
                .WithHydrocodoneInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.HydrocodoneInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithHydrocodoneInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.HydrocodoneInLifetimeYearCount, _mappingHelper ) )
                .WithHydrocodoneDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.HydrocodoneDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithHydrocodoneNote ( densAsiDrugAlcoholUseDto.HydrocodoneNote )
                .WithBuprenorphineInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.BuprenorphineInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithBuprenorphineInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.BuprenorphineInLifetimeYearCount, _mappingHelper ) )
                .WithBuprenorphineDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.BuprenorphineDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithBuprenorphineNote ( densAsiDrugAlcoholUseDto.BuprenorphineNote )
                .WithOxyContinInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.OxyContinInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithOxyContinInLifetimeYearCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiDrugAlcoholUseDto.OxyContinInLifetimeYearCount, _mappingHelper ) )
                .WithOxyContinDensAsiDrugAlcoholAdministrationRoute (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> (
                        densAsiDrugAlcoholUseDto.OxyContinDensAsiDrugAlcoholAdministrationRoute, _mappingHelper ) )
                .WithOxyContinNote ( densAsiDrugAlcoholUseDto.OxyContinNote )
                .WithOxyContinPrescribedForMedicalReasonIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.OxyContinPrescribedForMedicalReasonIndicator, _mappingHelper ) )
                .WithOxyContinPrescribedForMedicalReasonIndicatorNote ( densAsiDrugAlcoholUseDto.OxyContinPrescribedForMedicalReasonIndicatorNote )
                .WithOxyContinUseToGetHighIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.OxyContinUseToGetHighIndicator, _mappingHelper ) )
                .WithOxyContinUseToGetHighIndicatorNote ( densAsiDrugAlcoholUseDto.OxyContinUseToGetHighIndicatorNote )
                .WithOxyContinTakenWithOtherOpiatesIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.OxyContinTakenWithOtherOpiatesIndicator, _mappingHelper ) )
                .WithOxyContinTakenWithOtherOpiatesIndicatorNote ( densAsiDrugAlcoholUseDto.OxyContinTakenWithOtherOpiatesIndicatorNote )
                .WithAfterOxyContinFirstUseMonthCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.AfterOxyContinFirstUseMonthCount, _mappingHelper ) )
                .WithAfterOxyContinFirstUseMonthCountNote ( densAsiDrugAlcoholUseDto.AfterOxyContinFirstUseMonthCountNote )
                .WithOxyContinFromFriendFamilyStreetIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiDrugAlcoholUseDto.OxyContinFromFriendFamilyStreetIndicator, _mappingHelper ) )
                .WithOxyContinFromFriendFamilyStreetIndicatorNote ( densAsiDrugAlcoholUseDto.OxyContinFromFriendFamilyStreetIndicatorNote )
                .WithSectionNote ( densAsiDrugAlcoholUseDto.SectionNote )
                .Build ();

            AggregateRoot.ReviseDensAsiDrugAlcoholUse ( densAsiDrugAlcoholUseNew );

            densAsiDrugAlcoholUseDto.Key = AggregateRoot.DensAsiDrugAlcoholUse.Key;

            return true;
        }

        #endregion
    }
}
