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

using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.DensAsiInterview
{
    /// <summary>
    /// Data transfer object for DensAsiDrugAlcoholUse class.
    /// </summary>
    public class DensAsiDrugAlcoholUseDto : DensAsiDtoBase
    {
        #region Constants and Fields

        private DensAsiNonResponseTypeDto<int?> _afterOxyContinFirstUseMonthCount;
        private string _afterOxyContinFirstUseMonthCountNote;
        private DensAsiNonResponseTypeDto<int?> _alcoholAbuseTreatmentCount;
        private string _alcoholAbuseTreatmentCountNote;
        private DensAsiNonResponseTypeDto<int?> _alcoholDetoxTreatmentOnlyCount;
        private string _alcoholDetoxTreatmentOnlyCountNote;
        private DensAsiNonResponseTypeDto<int?> _alcoholDtCount;
        private string _alcoholDtCountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _alcoholIntoxicationInLastThirtyDaysDayCount;
        private string _alcoholIntoxicationNote;
        private DensAsiNonResponseTypeDto<int?> _alcoholIntoxicationUseInLifetimeYearCount;
        private DensAsiNonResponseTypeDto<int?> _alcoholProblemInLastThirtyDaysDayCount;
        private string _alcoholProblemInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _amphetaminesDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _amphetaminesInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _amphetaminesInLifetimeYearCount;
        private string _amphetaminesNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _anyAlcoholDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _anyAlcoholUseInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _anyAlcoholUseInLifetimeYearCount;
        private string _anyAlcoholUseNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _barbituratesDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _barbituratesInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _barbituratesInLifetimeYearCount;
        private string _barbituratesNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _buprenorphineDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _buprenorphineInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _buprenorphineInLifetimeYearCount;
        private string _buprenorphineNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _cannabisDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _cannabisInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _cannabisInLifetimeYearCount;
        private string _cannabisNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _cocaineDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _cocaineInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _cocaineInLifetimeYearCount;
        private string _cocaineNote;
        private bool? _confidenceDistortedByPatientMisrepresentationIndicator;
        private string _confidenceDistortedByPatientMisrepresentationIndicatorNote;
        private bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private DensAsiNonResponseTypeDto<int?> _drugAbuseTreatmentCount;
        private string _drugAbuseTreatmentCountNote;
        private DensAsiNonResponseTypeDto<int?> _drugDetoxTreatmentOnlyCount;
        private string _drugDetoxTreatmentOnlyCountNote;
        private DensAsiNonResponseTypeDto<int?> _drugProblemInLastThirtyDaysDayCount;
        private string _drugProblemInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseTypeDto<int?> _endOfProblematicSubstanceAbstinenceMonthCount;
        private string _endOfProblematicSubstanceAbstinenceMonthCountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _hallucinogensDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _hallucinogensInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _hallucinogensInLifetimeYearCount;
        private string _hallucinogensNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _heroinDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _heroinInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _heroinInLifetimeYearCount;
        private string _heroinNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _hydrocodoneDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _hydrocodoneInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _hydrocodoneInLifetimeYearCount;
        private string _hydrocodoneNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _hydromorphoneDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _hydromorphoneInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _hydromorphoneInLifetimeYearCount;
        private string _hydromorphoneNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _importanceOfAlcoholProblemTreatmentDensAsiPatientRating;
        private string _importanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _importanceOfDrugProblemTreatmentDensAsiPatientRating;
        private string _importanceOfDrugProblemTreatmentDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _inhalantsDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _inhalantsInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _inhalantsInLifetimeYearCount;
        private string _inhalantsNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _majorDensAsiProblematicSubstance;
        private string _majorDensAsiProblematicSubstanceNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _methadoneDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _methadoneInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _methadoneInLifetimeYearCount;
        private string _methadoneNote;
        private DensAsiNonResponseTypeDto<int?> _moneySpentOnAlcoholInLastThirtyDaysAmount;
        private string _moneySpentOnAlcoholInLastThirtyDaysAmountNote;
        private DensAsiNonResponseTypeDto<int?> _moneySpentOnDrugsInLastThirtyDaysAmount;
        private string _moneySpentOnDrugsInLastThirtyDaysAmountNote;
        private DensAsiNonResponseTypeDto<int?> _moreThanOneSubstancePerDayInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _moreThanOneSubstancePerDayInLifetimeYearCount;
        private string _moreThanOneSubstancePerDayNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _otherOpiatesDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _otherOpiatesInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _otherOpiatesInLifetimeYearCount;
        private string _otherOpiatesNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _otherSedativesDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _otherSedativesInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _otherSedativesInLifetimeYearCount;
        private string _otherSedativesNote;
        private DensAsiNonResponseTypeDto<int?> _outpatientTreatmentInLastThirtyDaysDayCount;
        private string _outpatientTreatmentInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseTypeDto<int?> _overdosedOnDrugsCount;
        private string _overdosedOnDrugsCountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _oxyContinDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<bool?> _oxyContinFromFriendFamilyStreetIndicator;
        private string _oxyContinFromFriendFamilyStreetIndicatorNote;
        private DensAsiNonResponseTypeDto<int?> _oxyContinInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _oxyContinInLifetimeYearCount;
        private string _oxyContinNote;
        private DensAsiNonResponseTypeDto<bool?> _oxyContinPrescribedForMedicalReasonIndicator;
        private string _oxyContinPrescribedForMedicalReasonIndicatorNote;
        private DensAsiNonResponseTypeDto<bool?> _oxyContinTakenWithOtherOpiatesIndicator;
        private string _oxyContinTakenWithOtherOpiatesIndicatorNote;
        private DensAsiNonResponseTypeDto<bool?> _oxyContinUseToGetHighIndicator;
        private string _oxyContinUseToGetHighIndicatorNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _oxycodoneDensAsiDrugAlcoholAdministrationRoute;
        private DensAsiNonResponseTypeDto<int?> _oxycodoneInLastThirtyDaysDayCount;
        private DensAsiNonResponseTypeDto<int?> _oxycodoneInLifetimeYearCount;
        private string _oxycodoneNote;
        private LookupValueDto _patientAlcoholTreatmentDensAsiInterviewerRating;
        private string _patientAlcoholTreatmentDensAsiInterviewerRatingNote;
        private LookupValueDto _patientDrugTreatmentDensAsiInterviewerRating;
        private string _patientDrugTreatmentDensAsiInterviewerRatingNote;
        private string _sectionNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _troubledByAlcoholProblemsDensAsiPatientRating;
        private string _troubledByAlcoholProblemsDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _troubledByDrugProblemsDensAsiPatientRating;
        private string _troubledByDrugProblemsDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<int?> _voluntaryAbstinenceFromProblematicSubstanceMonthCount;
        private string _voluntaryAbstinenceFromProblematicSubstanceMonthCountNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// Question Number: D109
        /// </summary>
        /// <value>The after oxy contin first use month count.</value>
        public DensAsiNonResponseTypeDto<int?> AfterOxyContinFirstUseMonthCount
        {
            get { return _afterOxyContinFirstUseMonthCount; }
            set { ApplyPropertyChange ( ref _afterOxyContinFirstUseMonthCount, () => AfterOxyContinFirstUseMonthCount, value ); }
        }

        /// <summary>
        /// Question Number: D109
        /// </summary>
        /// <value>The after oxy contin first use month count note.</value>
        public string AfterOxyContinFirstUseMonthCountNote
        {
            get { return _afterOxyContinFirstUseMonthCountNote; }
            set { ApplyPropertyChange ( ref _afterOxyContinFirstUseMonthCountNote, () => AfterOxyContinFirstUseMonthCountNote, value ); }
        }

        /// <summary>
        /// Question Number: D19
        /// </summary>
        /// <value>The alcohol abuse treatment count.</value>
        public DensAsiNonResponseTypeDto<int?> AlcoholAbuseTreatmentCount
        {
            get { return _alcoholAbuseTreatmentCount; }
            set { ApplyPropertyChange ( ref _alcoholAbuseTreatmentCount, () => AlcoholAbuseTreatmentCount, value ); }
        }

        /// <summary>
        /// Question Number: D19
        /// </summary>
        /// <value>The alcohol abuse treatment count note.</value>
        public string AlcoholAbuseTreatmentCountNote
        {
            get { return _alcoholAbuseTreatmentCountNote; }
            set { ApplyPropertyChange ( ref _alcoholAbuseTreatmentCountNote, () => AlcoholAbuseTreatmentCountNote, value ); }
        }

        /// <summary>
        /// Question Number: D21
        /// </summary>
        /// <value>The alcohol detox treatment only count.</value>
        public DensAsiNonResponseTypeDto<int?> AlcoholDetoxTreatmentOnlyCount
        {
            get { return _alcoholDetoxTreatmentOnlyCount; }
            set { ApplyPropertyChange ( ref _alcoholDetoxTreatmentOnlyCount, () => AlcoholDetoxTreatmentOnlyCount, value ); }
        }

        /// <summary>
        /// Question Number: D21
        /// </summary>
        /// <value>The alcohol detox treatment only count note.</value>
        public string AlcoholDetoxTreatmentOnlyCountNote
        {
            get { return _alcoholDetoxTreatmentOnlyCountNote; }
            set { ApplyPropertyChange ( ref _alcoholDetoxTreatmentOnlyCountNote, () => AlcoholDetoxTreatmentOnlyCountNote, value ); }
        }

        /// <summary>
        /// Question Number: D17
        /// </summary>
        /// <value>The alcohol dt count.</value>
        public DensAsiNonResponseTypeDto<int?> AlcoholDtCount
        {
            get { return _alcoholDtCount; }
            set { ApplyPropertyChange ( ref _alcoholDtCount, () => AlcoholDtCount, value ); }
        }

        /// <summary>
        /// Question Number: D17
        /// </summary>
        /// <value>The alcohol dt count note.</value>
        public string AlcoholDtCountNote
        {
            get { return _alcoholDtCountNote; }
            set { ApplyPropertyChange ( ref _alcoholDtCountNote, () => AlcoholDtCountNote, value ); }
        }

        /// <summary>
        /// Question Number: D2
        /// </summary>
        /// <value>The alcohol intoxication dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> AlcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute; }
            set
            {
                ApplyPropertyChange (
                    ref _alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute,
                    () => AlcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute,
                    value );
            }
        }

        /// <summary>
        /// Question Number: D2
        /// </summary>
        /// <value>The alcohol intoxication in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> AlcoholIntoxicationInLastThirtyDaysDayCount
        {
            get { return _alcoholIntoxicationInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _alcoholIntoxicationInLastThirtyDaysDayCount, () => AlcoholIntoxicationInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: D2
        /// </summary>
        /// <value>The alcohol intoxication note.</value>
        public string AlcoholIntoxicationNote
        {
            get { return _alcoholIntoxicationNote; }
            set { ApplyPropertyChange ( ref _alcoholIntoxicationNote, () => AlcoholIntoxicationNote, value ); }
        }

        /// <summary>
        /// Question Number: D2
        /// </summary>
        /// <value>The alcohol intoxication use in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> AlcoholIntoxicationUseInLifetimeYearCount
        {
            get { return _alcoholIntoxicationUseInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _alcoholIntoxicationUseInLifetimeYearCount, () => AlcoholIntoxicationUseInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: D26
        /// </summary>
        /// <value>The alcohol problem in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> AlcoholProblemInLastThirtyDaysDayCount
        {
            get { return _alcoholProblemInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _alcoholProblemInLastThirtyDaysDayCount, () => AlcoholProblemInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: D26
        /// </summary>
        /// <value>The alcohol problem in last thirty days day count note.</value>
        public string AlcoholProblemInLastThirtyDaysDayCountNote
        {
            get { return _alcoholProblemInLastThirtyDaysDayCountNote; }
            set { ApplyPropertyChange ( ref _alcoholProblemInLastThirtyDaysDayCountNote, () => AlcoholProblemInLastThirtyDaysDayCountNote, value ); }
        }

        /// <summary>
        /// Question Number: D9
        /// </summary>
        /// <value>The amphetamines dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> AmphetaminesDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _amphetaminesDensAsiDrugAlcoholAdministrationRoute; }
            set
            {
                ApplyPropertyChange (
                    ref _amphetaminesDensAsiDrugAlcoholAdministrationRoute, () => AmphetaminesDensAsiDrugAlcoholAdministrationRoute, value );
            }
        }

        /// <summary>
        /// Question Number: D9
        /// </summary>
        /// <value>The amphetamines in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> AmphetaminesInLastThirtyDaysDayCount
        {
            get { return _amphetaminesInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _amphetaminesInLastThirtyDaysDayCount, () => AmphetaminesInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: D9
        /// </summary>
        /// <value>The amphetamines in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> AmphetaminesInLifetimeYearCount
        {
            get { return _amphetaminesInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _amphetaminesInLifetimeYearCount, () => AmphetaminesInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: D9
        /// </summary>
        /// <value>The amphetamines note.</value>
        public string AmphetaminesNote
        {
            get { return _amphetaminesNote; }
            set { ApplyPropertyChange ( ref _amphetaminesNote, () => AmphetaminesNote, value ); }
        }

        /// <summary>
        /// Question Number: D1
        /// </summary>
        /// <value>Any alcohol dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> AnyAlcoholDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _anyAlcoholDensAsiDrugAlcoholAdministrationRoute; }
            set
            {
                ApplyPropertyChange (
                    ref _anyAlcoholDensAsiDrugAlcoholAdministrationRoute, () => AnyAlcoholDensAsiDrugAlcoholAdministrationRoute, value );
            }
        }

        /// <summary>
        /// Question Number: D1
        /// </summary>
        /// <value>Any alcohol use in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> AnyAlcoholUseInLastThirtyDaysDayCount
        {
            get { return _anyAlcoholUseInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _anyAlcoholUseInLastThirtyDaysDayCount, () => AnyAlcoholUseInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: D1
        /// </summary>
        /// <value>Any alcohol use in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> AnyAlcoholUseInLifetimeYearCount
        {
            get { return _anyAlcoholUseInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _anyAlcoholUseInLifetimeYearCount, () => AnyAlcoholUseInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: D1
        /// </summary>
        /// <value>Any alcohol use note.</value>
        public string AnyAlcoholUseNote
        {
            get { return _anyAlcoholUseNote; }
            set { ApplyPropertyChange ( ref _anyAlcoholUseNote, () => AnyAlcoholUseNote, value ); }
        }

        /// <summary>
        /// Question Number: D6
        /// </summary>
        /// <value>The barbiturates dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> BarbituratesDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _barbituratesDensAsiDrugAlcoholAdministrationRoute; }
            set
            {
                ApplyPropertyChange (
                    ref _barbituratesDensAsiDrugAlcoholAdministrationRoute, () => BarbituratesDensAsiDrugAlcoholAdministrationRoute, value );
            }
        }

        /// <summary>
        /// Question Number: D6
        /// </summary>
        /// <value>The barbiturates in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> BarbituratesInLastThirtyDaysDayCount
        {
            get { return _barbituratesInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _barbituratesInLastThirtyDaysDayCount, () => BarbituratesInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: D6
        /// </summary>
        /// <value>The barbiturates in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> BarbituratesInLifetimeYearCount
        {
            get { return _barbituratesInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _barbituratesInLifetimeYearCount, () => BarbituratesInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: D6
        /// </summary>
        /// <value>The barbiturates note.</value>
        public string BarbituratesNote
        {
            get { return _barbituratesNote; }
            set { ApplyPropertyChange ( ref _barbituratesNote, () => BarbituratesNote, value ); }
        }

        /// <summary>
        /// Question Number: DRG17
        /// </summary>
        /// <value>The buprenorphine dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> BuprenorphineDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _buprenorphineDensAsiDrugAlcoholAdministrationRoute; }
            set
            {
                ApplyPropertyChange (
                    ref _buprenorphineDensAsiDrugAlcoholAdministrationRoute, () => BuprenorphineDensAsiDrugAlcoholAdministrationRoute, value );
            }
        }

        /// <summary>
        /// Question Number: DRG17
        /// </summary>
        /// <value>The buprenorphine in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> BuprenorphineInLastThirtyDaysDayCount
        {
            get { return _buprenorphineInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _buprenorphineInLastThirtyDaysDayCount, () => BuprenorphineInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: DRG17
        /// </summary>
        /// <value>The buprenorphine in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> BuprenorphineInLifetimeYearCount
        {
            get { return _buprenorphineInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _buprenorphineInLifetimeYearCount, () => BuprenorphineInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: DRG17
        /// </summary>
        /// <value>The buprenorphine note.</value>
        public string BuprenorphineNote
        {
            get { return _buprenorphineNote; }
            set { ApplyPropertyChange ( ref _buprenorphineNote, () => BuprenorphineNote, value ); }
        }

        /// <summary>
        /// Question Number: D10
        /// </summary>
        /// <value>The cannabis dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> CannabisDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _cannabisDensAsiDrugAlcoholAdministrationRoute; }
            set { ApplyPropertyChange ( ref _cannabisDensAsiDrugAlcoholAdministrationRoute, () => CannabisDensAsiDrugAlcoholAdministrationRoute, value ); }
        }

        /// <summary>
        /// Question Number: D10
        /// </summary>
        /// <value>The cannabis in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> CannabisInLastThirtyDaysDayCount
        {
            get { return _cannabisInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _cannabisInLastThirtyDaysDayCount, () => CannabisInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: D10
        /// </summary>
        /// <value>The cannabis in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> CannabisInLifetimeYearCount
        {
            get { return _cannabisInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _cannabisInLifetimeYearCount, () => CannabisInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: D10
        /// </summary>
        /// <value>The cannabis note.</value>
        public string CannabisNote
        {
            get { return _cannabisNote; }
            set { ApplyPropertyChange ( ref _cannabisNote, () => CannabisNote, value ); }
        }

        /// <summary>
        /// Question Number: D8
        /// </summary>
        /// <value>The cocaine dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> CocaineDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _cocaineDensAsiDrugAlcoholAdministrationRoute; }
            set { ApplyPropertyChange ( ref _cocaineDensAsiDrugAlcoholAdministrationRoute, () => CocaineDensAsiDrugAlcoholAdministrationRoute, value ); }
        }

        /// <summary>
        /// Question Number: D8
        /// </summary>
        /// <value>The cocaine in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> CocaineInLastThirtyDaysDayCount
        {
            get { return _cocaineInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _cocaineInLastThirtyDaysDayCount, () => CocaineInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: D8
        /// </summary>
        /// <value>The cocaine in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> CocaineInLifetimeYearCount
        {
            get { return _cocaineInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _cocaineInLifetimeYearCount, () => CocaineInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: D8
        /// </summary>
        /// <value>The cocaine note.</value>
        public string CocaineNote
        {
            get { return _cocaineNote; }
            set { ApplyPropertyChange ( ref _cocaineNote, () => CocaineNote, value ); }
        }

        /// <summary>
        /// Question Number: D34
        /// </summary>
        /// <value>The confidence distorted by patient misrepresentation indicator.</value>
        public bool? ConfidenceDistortedByPatientMisrepresentationIndicator
        {
            get { return _confidenceDistortedByPatientMisrepresentationIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _confidenceDistortedByPatientMisrepresentationIndicator, () => ConfidenceDistortedByPatientMisrepresentationIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: D34
        /// </summary>
        /// <value>The confidence distorted by patient misrepresentation indicator note.</value>
        public string ConfidenceDistortedByPatientMisrepresentationIndicatorNote
        {
            get { return _confidenceDistortedByPatientMisrepresentationIndicatorNote; }
            set
            {
                ApplyPropertyChange (
                    ref _confidenceDistortedByPatientMisrepresentationIndicatorNote,
                    () => ConfidenceDistortedByPatientMisrepresentationIndicatorNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: D35
        /// </summary>
        /// <value>The confidence rate distorted by patient inability to understand indicator.</value>
        public bool? ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator
        {
            get { return _confidenceRateDistortedByPatientInabilityToUnderstandIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _confidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                    () => ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                    value );
            }
        }

        /// <summary>
        /// Question Number: D35
        /// </summary>
        /// <value>The confidence rate distorted by patient inability to understand indicator note.</value>
        public string ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote
        {
            get { return _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote; }
            set
            {
                ApplyPropertyChange (
                    ref _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                    () => ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: D20
        /// </summary>
        /// <value>The drug abuse treatment count.</value>
        public DensAsiNonResponseTypeDto<int?> DrugAbuseTreatmentCount
        {
            get { return _drugAbuseTreatmentCount; }
            set { ApplyPropertyChange ( ref _drugAbuseTreatmentCount, () => DrugAbuseTreatmentCount, value ); }
        }

        /// <summary>
        /// Question Number: D20
        /// </summary>
        /// <value>The drug abuse treatment count note.</value>
        public string DrugAbuseTreatmentCountNote
        {
            get { return _drugAbuseTreatmentCountNote; }
            set { ApplyPropertyChange ( ref _drugAbuseTreatmentCountNote, () => DrugAbuseTreatmentCountNote, value ); }
        }

        /// <summary>
        /// Question Number: D22
        /// </summary>
        /// <value>The drug detox treatment only count.</value>
        public DensAsiNonResponseTypeDto<int?> DrugDetoxTreatmentOnlyCount
        {
            get { return _drugDetoxTreatmentOnlyCount; }
            set { ApplyPropertyChange ( ref _drugDetoxTreatmentOnlyCount, () => DrugDetoxTreatmentOnlyCount, value ); }
        }

        /// <summary>
        /// Question Number: D22
        /// </summary>
        /// <value>The drug detox treatment only count note.</value>
        public string DrugDetoxTreatmentOnlyCountNote
        {
            get { return _drugDetoxTreatmentOnlyCountNote; }
            set { ApplyPropertyChange ( ref _drugDetoxTreatmentOnlyCountNote, () => DrugDetoxTreatmentOnlyCountNote, value ); }
        }

        /// <summary>
        /// Question Number: D27
        /// </summary>
        /// <value>The drug problem in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> DrugProblemInLastThirtyDaysDayCount
        {
            get { return _drugProblemInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _drugProblemInLastThirtyDaysDayCount, () => DrugProblemInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: D27
        /// </summary>
        /// <value>The drug problem in last thirty days day count note.</value>
        public string DrugProblemInLastThirtyDaysDayCountNote
        {
            get { return _drugProblemInLastThirtyDaysDayCountNote; }
            set { ApplyPropertyChange ( ref _drugProblemInLastThirtyDaysDayCountNote, () => DrugProblemInLastThirtyDaysDayCountNote, value ); }
        }

        /// <summary>
        /// Question Number: D16
        /// </summary>
        /// <value>The end of problematic substance abstinence month count.</value>
        public DensAsiNonResponseTypeDto<int?> EndOfProblematicSubstanceAbstinenceMonthCount
        {
            get { return _endOfProblematicSubstanceAbstinenceMonthCount; }
            set { ApplyPropertyChange ( ref _endOfProblematicSubstanceAbstinenceMonthCount, () => EndOfProblematicSubstanceAbstinenceMonthCount, value ); }
        }

        /// <summary>
        /// Question Number: D16
        /// </summary>
        /// <value>The end of problematic substance abstinence month count note.</value>
        public string EndOfProblematicSubstanceAbstinenceMonthCountNote
        {
            get { return _endOfProblematicSubstanceAbstinenceMonthCountNote; }
            set
            {
                ApplyPropertyChange (
                    ref _endOfProblematicSubstanceAbstinenceMonthCountNote, () => EndOfProblematicSubstanceAbstinenceMonthCountNote, value );
            }
        }

        /// <summary>
        /// Question Number: D11
        /// </summary>
        /// <value>The hallucinogens dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> HallucinogensDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _hallucinogensDensAsiDrugAlcoholAdministrationRoute; }
            set
            {
                ApplyPropertyChange (
                    ref _hallucinogensDensAsiDrugAlcoholAdministrationRoute, () => HallucinogensDensAsiDrugAlcoholAdministrationRoute, value );
            }
        }

        /// <summary>
        /// Question Number: D11
        /// </summary>
        /// <value>The hallucinogens in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> HallucinogensInLastThirtyDaysDayCount
        {
            get { return _hallucinogensInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _hallucinogensInLastThirtyDaysDayCount, () => HallucinogensInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: D11
        /// </summary>
        /// <value>The hallucinogens in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> HallucinogensInLifetimeYearCount
        {
            get { return _hallucinogensInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _hallucinogensInLifetimeYearCount, () => HallucinogensInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: D11
        /// </summary>
        /// <value>The hallucinogens note.</value>
        public string HallucinogensNote
        {
            get { return _hallucinogensNote; }
            set { ApplyPropertyChange ( ref _hallucinogensNote, () => HallucinogensNote, value ); }
        }

        /// <summary>
        /// Question Number: D3
        /// </summary>
        /// <value>The heroin dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> HeroinDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _heroinDensAsiDrugAlcoholAdministrationRoute; }
            set { ApplyPropertyChange ( ref _heroinDensAsiDrugAlcoholAdministrationRoute, () => HeroinDensAsiDrugAlcoholAdministrationRoute, value ); }
        }

        /// <summary>
        /// Question Number: D3
        /// </summary>
        /// <value>The heroin in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> HeroinInLastThirtyDaysDayCount
        {
            get { return _heroinInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _heroinInLastThirtyDaysDayCount, () => HeroinInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: D3
        /// </summary>
        /// <value>The heroin in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> HeroinInLifetimeYearCount
        {
            get { return _heroinInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _heroinInLifetimeYearCount, () => HeroinInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: D3
        /// </summary>
        /// <value>The heroin note.</value>
        public string HeroinNote
        {
            get { return _heroinNote; }
            set { ApplyPropertyChange ( ref _heroinNote, () => HeroinNote, value ); }
        }

        /// <summary>
        /// Question Number: DRG14
        /// </summary>
        /// <value>The hydrocodone dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> HydrocodoneDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _hydrocodoneDensAsiDrugAlcoholAdministrationRoute; }
            set
            {
                ApplyPropertyChange (
                    ref _hydrocodoneDensAsiDrugAlcoholAdministrationRoute, () => HydrocodoneDensAsiDrugAlcoholAdministrationRoute, value );
            }
        }

        /// <summary>
        /// Question Number: DRG14
        /// </summary>
        /// <value>The hydrocodone in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> HydrocodoneInLastThirtyDaysDayCount
        {
            get { return _hydrocodoneInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _hydrocodoneInLastThirtyDaysDayCount, () => HydrocodoneInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: DRG14
        /// </summary>
        /// <value>The hydrocodone in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> HydrocodoneInLifetimeYearCount
        {
            get { return _hydrocodoneInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _hydrocodoneInLifetimeYearCount, () => HydrocodoneInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: DRG14
        /// </summary>
        /// <value>The hydrocodone note.</value>
        public string HydrocodoneNote
        {
            get { return _hydrocodoneNote; }
            set { ApplyPropertyChange ( ref _hydrocodoneNote, () => HydrocodoneNote, value ); }
        }

        /// <summary>
        /// Question Number: DRG12
        /// </summary>
        /// <value>The hydromorphone dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> HydromorphoneDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _hydromorphoneDensAsiDrugAlcoholAdministrationRoute; }
            set
            {
                ApplyPropertyChange (
                    ref _hydromorphoneDensAsiDrugAlcoholAdministrationRoute, () => HydromorphoneDensAsiDrugAlcoholAdministrationRoute, value );
            }
        }

        /// <summary>
        /// Question Number: DRG12
        /// </summary>
        /// <value>The hydromorphone in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> HydromorphoneInLastThirtyDaysDayCount
        {
            get { return _hydromorphoneInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _hydromorphoneInLastThirtyDaysDayCount, () => HydromorphoneInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: DRG12
        /// </summary>
        /// <value>The hydromorphone in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> HydromorphoneInLifetimeYearCount
        {
            get { return _hydromorphoneInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _hydromorphoneInLifetimeYearCount, () => HydromorphoneInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: DRG12
        /// </summary>
        /// <value>The hydromorphone note.</value>
        public string HydromorphoneNote
        {
            get { return _hydromorphoneNote; }
            set { ApplyPropertyChange ( ref _hydromorphoneNote, () => HydromorphoneNote, value ); }
        }

        /// <summary>
        /// Question Number: D30
        /// </summary>
        /// <value>The importance of alcohol problem treatment dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> ImportanceOfAlcoholProblemTreatmentDensAsiPatientRating
        {
            get { return _importanceOfAlcoholProblemTreatmentDensAsiPatientRating; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfAlcoholProblemTreatmentDensAsiPatientRating, () => ImportanceOfAlcoholProblemTreatmentDensAsiPatientRating, value );
            }
        }

        /// <summary>
        /// Question Number: D30
        /// </summary>
        /// <value>The importance of alcohol problem treatment dens asi patient rating note.</value>
        public string ImportanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote
        {
            get { return _importanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote,
                    () => ImportanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: D31
        /// </summary>
        /// <value>The importance of drug problem treatment dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> ImportanceOfDrugProblemTreatmentDensAsiPatientRating
        {
            get { return _importanceOfDrugProblemTreatmentDensAsiPatientRating; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfDrugProblemTreatmentDensAsiPatientRating, () => ImportanceOfDrugProblemTreatmentDensAsiPatientRating, value );
            }
        }

        /// <summary>
        /// Question Number: D31
        /// </summary>
        /// <value>The importance of drug problem treatment dens asi patient rating note.</value>
        public string ImportanceOfDrugProblemTreatmentDensAsiPatientRatingNote
        {
            get { return _importanceOfDrugProblemTreatmentDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfDrugProblemTreatmentDensAsiPatientRatingNote,
                    () => ImportanceOfDrugProblemTreatmentDensAsiPatientRatingNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: D12
        /// </summary>
        /// <value>The inhalants dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> InhalantsDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _inhalantsDensAsiDrugAlcoholAdministrationRoute; }
            set
            {
                ApplyPropertyChange (
                    ref _inhalantsDensAsiDrugAlcoholAdministrationRoute, () => InhalantsDensAsiDrugAlcoholAdministrationRoute, value );
            }
        }

        /// <summary>
        /// Question Number: D12
        /// </summary>
        /// <value>The inhalants in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> InhalantsInLastThirtyDaysDayCount
        {
            get { return _inhalantsInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _inhalantsInLastThirtyDaysDayCount, () => InhalantsInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: D12
        /// </summary>
        /// <value>The inhalants in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> InhalantsInLifetimeYearCount
        {
            get { return _inhalantsInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _inhalantsInLifetimeYearCount, () => InhalantsInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: D12
        /// </summary>
        /// <value>The inhalants note.</value>
        public string InhalantsNote
        {
            get { return _inhalantsNote; }
            set { ApplyPropertyChange ( ref _inhalantsNote, () => InhalantsNote, value ); }
        }

        /// <summary>
        /// Question Number: D14
        /// </summary>
        /// <value>The major dens asi problematic substance.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> MajorDensAsiProblematicSubstance
        {
            get { return _majorDensAsiProblematicSubstance; }
            set { ApplyPropertyChange ( ref _majorDensAsiProblematicSubstance, () => MajorDensAsiProblematicSubstance, value ); }
        }

        /// <summary>
        /// Question Number: D14
        /// </summary>
        /// <value>The major dens asi problematic substance note.</value>
        public string MajorDensAsiProblematicSubstanceNote
        {
            get { return _majorDensAsiProblematicSubstanceNote; }
            set { ApplyPropertyChange ( ref _majorDensAsiProblematicSubstanceNote, () => MajorDensAsiProblematicSubstanceNote, value ); }
        }

        /// <summary>
        /// Question Number: D4
        /// </summary>
        /// <value>The methadone dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> MethadoneDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _methadoneDensAsiDrugAlcoholAdministrationRoute; }
            set
            {
                ApplyPropertyChange (
                    ref _methadoneDensAsiDrugAlcoholAdministrationRoute, () => MethadoneDensAsiDrugAlcoholAdministrationRoute, value );
            }
        }

        /// <summary>
        /// Question Number: D4
        /// </summary>
        /// <value>The methadone in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> MethadoneInLastThirtyDaysDayCount
        {
            get { return _methadoneInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _methadoneInLastThirtyDaysDayCount, () => MethadoneInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: D4
        /// </summary>
        /// <value>The methadone in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> MethadoneInLifetimeYearCount
        {
            get { return _methadoneInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _methadoneInLifetimeYearCount, () => MethadoneInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: D4
        /// </summary>
        /// <value>The methadone note.</value>
        public string MethadoneNote
        {
            get { return _methadoneNote; }
            set { ApplyPropertyChange ( ref _methadoneNote, () => MethadoneNote, value ); }
        }

        /// <summary>
        /// Question Number: D23
        /// </summary>
        /// <value>The money spent on alcohol in last thirty days amount.</value>
        public DensAsiNonResponseTypeDto<int?> MoneySpentOnAlcoholInLastThirtyDaysAmount
        {
            get { return _moneySpentOnAlcoholInLastThirtyDaysAmount; }
            set { ApplyPropertyChange ( ref _moneySpentOnAlcoholInLastThirtyDaysAmount, () => MoneySpentOnAlcoholInLastThirtyDaysAmount, value ); }
        }

        /// <summary>
        /// Question Number: D23
        /// </summary>
        /// <value>The money spent on alcohol in last thirty days amount note.</value>
        public string MoneySpentOnAlcoholInLastThirtyDaysAmountNote
        {
            get { return _moneySpentOnAlcoholInLastThirtyDaysAmountNote; }
            set { ApplyPropertyChange ( ref _moneySpentOnAlcoholInLastThirtyDaysAmountNote, () => MoneySpentOnAlcoholInLastThirtyDaysAmountNote, value ); }
        }

        /// <summary>
        /// Question Number: D24
        /// </summary>
        /// <value>The money spent on drugs in last thirty days amount.</value>
        public DensAsiNonResponseTypeDto<int?> MoneySpentOnDrugsInLastThirtyDaysAmount
        {
            get { return _moneySpentOnDrugsInLastThirtyDaysAmount; }
            set { ApplyPropertyChange ( ref _moneySpentOnDrugsInLastThirtyDaysAmount, () => MoneySpentOnDrugsInLastThirtyDaysAmount, value ); }
        }

        /// <summary>
        /// Question Number: D24
        /// </summary>
        /// <value>The money spent on drugs in last thirty days amount note.</value>
        public string MoneySpentOnDrugsInLastThirtyDaysAmountNote
        {
            get { return _moneySpentOnDrugsInLastThirtyDaysAmountNote; }
            set { ApplyPropertyChange ( ref _moneySpentOnDrugsInLastThirtyDaysAmountNote, () => MoneySpentOnDrugsInLastThirtyDaysAmountNote, value ); }
        }

        /// <summary>
        /// Question Number: D13
        /// </summary>
        /// <value>The more than one substance per day in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> MoreThanOneSubstancePerDayInLastThirtyDaysDayCount
        {
            get { return _moreThanOneSubstancePerDayInLastThirtyDaysDayCount; }
            set
            {
                ApplyPropertyChange (
                    ref _moreThanOneSubstancePerDayInLastThirtyDaysDayCount, () => MoreThanOneSubstancePerDayInLastThirtyDaysDayCount, value );
            }
        }

        /// <summary>
        /// Question Number: D13
        /// </summary>
        /// <value>The more than one substance per day in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> MoreThanOneSubstancePerDayInLifetimeYearCount
        {
            get { return _moreThanOneSubstancePerDayInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _moreThanOneSubstancePerDayInLifetimeYearCount, () => MoreThanOneSubstancePerDayInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: D13
        /// </summary>
        /// <value>The more than one substance per day note.</value>
        public string MoreThanOneSubstancePerDayNote
        {
            get { return _moreThanOneSubstancePerDayNote; }
            set { ApplyPropertyChange ( ref _moreThanOneSubstancePerDayNote, () => MoreThanOneSubstancePerDayNote, value ); }
        }

        /// <summary>
        /// Question Number: D5
        /// </summary>
        /// <value>The other opiates dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> OtherOpiatesDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _otherOpiatesDensAsiDrugAlcoholAdministrationRoute; }
            set
            {
                ApplyPropertyChange (
                    ref _otherOpiatesDensAsiDrugAlcoholAdministrationRoute, () => OtherOpiatesDensAsiDrugAlcoholAdministrationRoute, value );
            }
        }

        /// <summary>
        /// Question Number: D5
        /// </summary>
        /// <value>The other opiates in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> OtherOpiatesInLastThirtyDaysDayCount
        {
            get { return _otherOpiatesInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _otherOpiatesInLastThirtyDaysDayCount, () => OtherOpiatesInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: D5
        /// </summary>
        /// <value>The other opiates in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> OtherOpiatesInLifetimeYearCount
        {
            get { return _otherOpiatesInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _otherOpiatesInLifetimeYearCount, () => OtherOpiatesInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: D5
        /// </summary>
        /// <value>The other opiates note.</value>
        public string OtherOpiatesNote
        {
            get { return _otherOpiatesNote; }
            set { ApplyPropertyChange ( ref _otherOpiatesNote, () => OtherOpiatesNote, value ); }
        }

        /// <summary>
        /// Question Number: D7
        /// </summary>
        /// <value>The other sedatives dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> OtherSedativesDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _otherSedativesDensAsiDrugAlcoholAdministrationRoute; }
            set
            {
                ApplyPropertyChange (
                    ref _otherSedativesDensAsiDrugAlcoholAdministrationRoute, () => OtherSedativesDensAsiDrugAlcoholAdministrationRoute, value );
            }
        }

        /// <summary>
        /// Question Number: D7
        /// </summary>
        /// <value>The other sedatives in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> OtherSedativesInLastThirtyDaysDayCount
        {
            get { return _otherSedativesInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _otherSedativesInLastThirtyDaysDayCount, () => OtherSedativesInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: D7
        /// </summary>
        /// <value>The other sedatives in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> OtherSedativesInLifetimeYearCount
        {
            get { return _otherSedativesInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _otherSedativesInLifetimeYearCount, () => OtherSedativesInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: D7
        /// </summary>
        /// <value>The other sedatives note.</value>
        public string OtherSedativesNote
        {
            get { return _otherSedativesNote; }
            set { ApplyPropertyChange ( ref _otherSedativesNote, () => OtherSedativesNote, value ); }
        }

        /// <summary>
        /// Question Number: D25
        /// </summary>
        /// <value>The outpatient treatment in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> OutpatientTreatmentInLastThirtyDaysDayCount
        {
            get { return _outpatientTreatmentInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _outpatientTreatmentInLastThirtyDaysDayCount, () => OutpatientTreatmentInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: D25
        /// </summary>
        /// <value>The outpatient treatment in last thirty days day count note.</value>
        public string OutpatientTreatmentInLastThirtyDaysDayCountNote
        {
            get { return _outpatientTreatmentInLastThirtyDaysDayCountNote; }
            set
            {
                ApplyPropertyChange (
                    ref _outpatientTreatmentInLastThirtyDaysDayCountNote, () => OutpatientTreatmentInLastThirtyDaysDayCountNote, value );
            }
        }

        /// <summary>
        /// Question Number: D18
        /// </summary>
        /// <value>The overdosed on drugs count.</value>
        public DensAsiNonResponseTypeDto<int?> OverdosedOnDrugsCount
        {
            get { return _overdosedOnDrugsCount; }
            set { ApplyPropertyChange ( ref _overdosedOnDrugsCount, () => OverdosedOnDrugsCount, value ); }
        }

        /// <summary>
        /// Question Number: D18
        /// </summary>
        /// <value>The overdosed on drugs count note.</value>
        public string OverdosedOnDrugsCountNote
        {
            get { return _overdosedOnDrugsCountNote; }
            set { ApplyPropertyChange ( ref _overdosedOnDrugsCountNote, () => OverdosedOnDrugsCountNote, value ); }
        }

        /// <summary>
        /// Question Number: DRG16
        /// </summary>
        /// <value>The oxy contin dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> OxyContinDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _oxyContinDensAsiDrugAlcoholAdministrationRoute; }
            set
            {
                ApplyPropertyChange (
                    ref _oxyContinDensAsiDrugAlcoholAdministrationRoute, () => OxyContinDensAsiDrugAlcoholAdministrationRoute, value );
            }
        }

        /// <summary>
        /// Question Number: D110
        /// </summary>
        /// <value>The oxy contin from friend family street indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> OxyContinFromFriendFamilyStreetIndicator
        {
            get { return _oxyContinFromFriendFamilyStreetIndicator; }
            set { ApplyPropertyChange ( ref _oxyContinFromFriendFamilyStreetIndicator, () => OxyContinFromFriendFamilyStreetIndicator, value ); }
        }

        /// <summary>
        /// Question Number: D110
        /// </summary>
        /// <value>The oxy contin from friend family street indicator note.</value>
        public string OxyContinFromFriendFamilyStreetIndicatorNote
        {
            get { return _oxyContinFromFriendFamilyStreetIndicatorNote; }
            set { ApplyPropertyChange ( ref _oxyContinFromFriendFamilyStreetIndicatorNote, () => OxyContinFromFriendFamilyStreetIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: DRG16
        /// </summary>
        /// <value>The oxy contin in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> OxyContinInLastThirtyDaysDayCount
        {
            get { return _oxyContinInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _oxyContinInLastThirtyDaysDayCount, () => OxyContinInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: DRG16
        /// </summary>
        /// <value>The oxy contin in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> OxyContinInLifetimeYearCount
        {
            get { return _oxyContinInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _oxyContinInLifetimeYearCount, () => OxyContinInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: DRG16
        /// </summary>
        /// <value>The oxy contin note.</value>
        public string OxyContinNote
        {
            get { return _oxyContinNote; }
            set { ApplyPropertyChange ( ref _oxyContinNote, () => OxyContinNote, value ); }
        }

        /// <summary>
        /// Question Number: D106
        /// </summary>
        /// <value>The oxy contin prescribed for medical reason indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> OxyContinPrescribedForMedicalReasonIndicator
        {
            get { return _oxyContinPrescribedForMedicalReasonIndicator; }
            set { ApplyPropertyChange ( ref _oxyContinPrescribedForMedicalReasonIndicator, () => OxyContinPrescribedForMedicalReasonIndicator, value ); }
        }

        /// <summary>
        /// Question Number: D106
        /// </summary>
        /// <value>The oxy contin prescribed for medical reason indicator note.</value>
        public string OxyContinPrescribedForMedicalReasonIndicatorNote
        {
            get { return _oxyContinPrescribedForMedicalReasonIndicatorNote; }
            set
            {
                ApplyPropertyChange (
                    ref _oxyContinPrescribedForMedicalReasonIndicatorNote, () => OxyContinPrescribedForMedicalReasonIndicatorNote, value );
            }
        }

        /// <summary>
        /// Question Number: D108
        /// </summary>
        /// <value>The oxy contin taken with other opiates indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> OxyContinTakenWithOtherOpiatesIndicator
        {
            get { return _oxyContinTakenWithOtherOpiatesIndicator; }
            set { ApplyPropertyChange ( ref _oxyContinTakenWithOtherOpiatesIndicator, () => OxyContinTakenWithOtherOpiatesIndicator, value ); }
        }

        /// <summary>
        /// Question Number: D108
        /// </summary>
        /// <value>The oxy contin taken with other opiates indicator note.</value>
        public string OxyContinTakenWithOtherOpiatesIndicatorNote
        {
            get { return _oxyContinTakenWithOtherOpiatesIndicatorNote; }
            set { ApplyPropertyChange ( ref _oxyContinTakenWithOtherOpiatesIndicatorNote, () => OxyContinTakenWithOtherOpiatesIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: D107
        /// </summary>
        /// <value>The oxy contin use to get high indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> OxyContinUseToGetHighIndicator
        {
            get { return _oxyContinUseToGetHighIndicator; }
            set { ApplyPropertyChange ( ref _oxyContinUseToGetHighIndicator, () => OxyContinUseToGetHighIndicator, value ); }
        }

        /// <summary>
        /// Question Number: D107
        /// </summary>
        /// <value>The oxy contin use to get high indicator note.</value>
        public string OxyContinUseToGetHighIndicatorNote
        {
            get { return _oxyContinUseToGetHighIndicatorNote; }
            set { ApplyPropertyChange ( ref _oxyContinUseToGetHighIndicatorNote, () => OxyContinUseToGetHighIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: DRG13
        /// </summary>
        /// <value>The oxycodone dens asi drug alcohol administration route.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> OxycodoneDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _oxycodoneDensAsiDrugAlcoholAdministrationRoute; }
            set
            {
                ApplyPropertyChange (
                    ref _oxycodoneDensAsiDrugAlcoholAdministrationRoute, () => OxycodoneDensAsiDrugAlcoholAdministrationRoute, value );
            }
        }

        /// <summary>
        /// Question Number: DRG13
        /// </summary>
        /// <value>The oxycodone in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> OxycodoneInLastThirtyDaysDayCount
        {
            get { return _oxycodoneInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _oxycodoneInLastThirtyDaysDayCount, () => OxycodoneInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: DRG13
        /// </summary>
        /// <value>The oxycodone in lifetime year count.</value>
        public DensAsiNonResponseTypeDto<int?> OxycodoneInLifetimeYearCount
        {
            get { return _oxycodoneInLifetimeYearCount; }
            set { ApplyPropertyChange ( ref _oxycodoneInLifetimeYearCount, () => OxycodoneInLifetimeYearCount, value ); }
        }

        /// <summary>
        /// Question Number: DRG13
        /// </summary>
        /// <value>The oxycodone note.</value>
        public string OxycodoneNote
        {
            get { return _oxycodoneNote; }
            set { ApplyPropertyChange ( ref _oxycodoneNote, () => OxycodoneNote, value ); }
        }

        /// <summary>
        /// Question Number: D32
        /// </summary>
        /// <value>The patient alcohol treatment dens asi interviewer rating.</value>
        public LookupValueDto PatientAlcoholTreatmentDensAsiInterviewerRating
        {
            get { return _patientAlcoholTreatmentDensAsiInterviewerRating; }
            set
            {
                ApplyPropertyChange (
                    ref _patientAlcoholTreatmentDensAsiInterviewerRating, () => PatientAlcoholTreatmentDensAsiInterviewerRating, value );
            }
        }

        /// <summary>
        /// Question Number: D32
        /// </summary>
        /// <value>The patient alcohol treatment dens asi interviewer rating note.</value>
        public string PatientAlcoholTreatmentDensAsiInterviewerRatingNote
        {
            get { return _patientAlcoholTreatmentDensAsiInterviewerRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _patientAlcoholTreatmentDensAsiInterviewerRatingNote, () => PatientAlcoholTreatmentDensAsiInterviewerRatingNote, value );
            }
        }

        /// <summary>
        /// Question Number: D33
        /// </summary>
        /// <value>The patient drug treatment dens asi interviewer rating.</value>
        public LookupValueDto PatientDrugTreatmentDensAsiInterviewerRating
        {
            get { return _patientDrugTreatmentDensAsiInterviewerRating; }
            set { ApplyPropertyChange ( ref _patientDrugTreatmentDensAsiInterviewerRating, () => PatientDrugTreatmentDensAsiInterviewerRating, value ); }
        }

        /// <summary>
        /// Question Number: D33
        /// </summary>
        /// <value>The patient drug treatment dens asi interviewer rating note.</value>
        public string PatientDrugTreatmentDensAsiInterviewerRatingNote
        {
            get { return _patientDrugTreatmentDensAsiInterviewerRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _patientDrugTreatmentDensAsiInterviewerRatingNote, () => PatientDrugTreatmentDensAsiInterviewerRatingNote, value );
            }
        }

        /// <summary>
        /// Gets or sets the section note.
        /// </summary>
        /// <value>The section note.</value>
        public string SectionNote
        {
            get { return _sectionNote; }
            set { ApplyPropertyChange ( ref _sectionNote, () => SectionNote, value ); }
        }

        /// <summary>
        /// Question Number: D28
        /// </summary>
        /// <value>The troubled by alcohol problems dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> TroubledByAlcoholProblemsDensAsiPatientRating
        {
            get { return _troubledByAlcoholProblemsDensAsiPatientRating; }
            set { ApplyPropertyChange ( ref _troubledByAlcoholProblemsDensAsiPatientRating, () => TroubledByAlcoholProblemsDensAsiPatientRating, value ); }
        }

        /// <summary>
        /// Question Number: D28
        /// </summary>
        /// <value>The troubled by alcohol problems dens asi patient rating note.</value>
        public string TroubledByAlcoholProblemsDensAsiPatientRatingNote
        {
            get { return _troubledByAlcoholProblemsDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _troubledByAlcoholProblemsDensAsiPatientRatingNote, () => TroubledByAlcoholProblemsDensAsiPatientRatingNote, value );
            }
        }

        /// <summary>
        /// Question Number: D29
        /// </summary>
        /// <value>The troubled by drug problems dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> TroubledByDrugProblemsDensAsiPatientRating
        {
            get { return _troubledByDrugProblemsDensAsiPatientRating; }
            set { ApplyPropertyChange ( ref _troubledByDrugProblemsDensAsiPatientRating, () => TroubledByDrugProblemsDensAsiPatientRating, value ); }
        }

        /// <summary>
        /// Question Number: D29
        /// </summary>
        /// <value>The troubled by drug problems dens asi patient rating note.</value>
        public string TroubledByDrugProblemsDensAsiPatientRatingNote
        {
            get { return _troubledByDrugProblemsDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _troubledByDrugProblemsDensAsiPatientRatingNote, () => TroubledByDrugProblemsDensAsiPatientRatingNote, value );
            }
        }

        /// <summary>
        /// Question Number: D15
        /// </summary>
        /// <value>The voluntary abstinence from problematic substance month count.</value>
        public DensAsiNonResponseTypeDto<int?> VoluntaryAbstinenceFromProblematicSubstanceMonthCount
        {
            get { return _voluntaryAbstinenceFromProblematicSubstanceMonthCount; }
            set
            {
                ApplyPropertyChange (
                    ref _voluntaryAbstinenceFromProblematicSubstanceMonthCount, () => VoluntaryAbstinenceFromProblematicSubstanceMonthCount, value );
            }
        }

        /// <summary>
        /// Question Number: D15
        /// </summary>
        /// <value>The voluntary abstinence from problematic substance month count note.</value>
        public string VoluntaryAbstinenceFromProblematicSubstanceMonthCountNote
        {
            get { return _voluntaryAbstinenceFromProblematicSubstanceMonthCountNote; }
            set
            {
                ApplyPropertyChange (
                    ref _voluntaryAbstinenceFromProblematicSubstanceMonthCountNote,
                    () => VoluntaryAbstinenceFromProblematicSubstanceMonthCountNote,
                    value );
            }
        }

        #endregion
    }
}
