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

using Pillar.Common.Metadata;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.GpraInterview;
using Rem.WellKnownNames.GpraModule;

namespace Rem.Ria.PatientModule.GpraInterview
{
    /// <summary>
    /// GpraInterviewViewModelRuleCollection class.
    /// </summary>
    public class GpraInterviewViewModelRuleCollection : AbstractRuleCollection<GpraInterviewViewModel>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraInterviewViewModelRuleCollection"/> class.
        /// </summary>
        public GpraInterviewViewModelRuleCollection ()
        {
            BuildRecordManagementRules ();
            BuildPlannedServicesRules ();
            BuildDemographicsRules ();
            BuildDrugAlcoholUseRules ();
            BuildFamilyLivingConditionsRules ();
            BuildCrimeCriminalJusticeRules ();
            BuildProblemTreatmentRecoveryRules ();
            BuildSocialConnectednessRules ();
            BuildFollowupRules ();
            BuildDischargeRules ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the alcohol intoxication five plus drinks day count cannot be greater then any alcohol day count.
        /// </summary>
        /// <value>The alcohol intoxication five plus drinks day count cannot be greater then any alcohol day count.</value>
        public IRule AlcoholIntoxicationFivePlusDrinksDayCountCannotBeGreaterThanAnyAlcoholDayCount { get; set; }

        /// <summary>
        /// Gets or sets the alcohol intoxication four or fewer drinks day count cannot be greater then any alcohol day count.
        /// </summary>
        /// <value>The alcohol intoxication four or fewer drinks day count cannot be greater then any alcohol day count.</value>
        public IRule AlcoholIntoxicationFourOrFewerDrinksDayCountCannotBeGreaterThanAnyAlcoholDayCount { get; set; }

        /// <summary>
        /// Gets or sets the children in child protection count cannot be greater then child count.
        /// </summary>
        /// <value>The children in child protection count cannot be greater then child count.</value>
        public IRule ChildrenInChildProtectionCountCannotBeGreaterThanChildCount { get; set; }

        /// <summary>
        /// Gets or sets the drug related arrests cannot be greater then number of arrests.
        /// </summary>
        /// <value>The drug related arrests cannot be greater then number of arrests.</value>
        public IRule DrugRelatedArrestsCannotBeGreaterThanNumberOfArrests { get; set; }

        /// <summary>
        /// Gets or sets the filter gpra family living condition lookups.
        /// </summary>
        /// <value>The filter gpra family living condition lookups.</value>
        public IRule FilterGpraFamilyLivingConditionLookups { get; set; }

        /// <summary>
        /// Gets or sets the hide sections if is follow up or discharged and interview conducted.
        /// </summary>
        /// <value>The hide sections if is follow up or discharged and interview conducted.</value>
        public IRule HideSectionsIfIsFollowUpOrDischargedAndInterviewConducted { get; set; }

        /// <summary>
        /// Gets or sets if children in child protection indicator show children in child protection count.
        /// </summary>
        /// <value>If children in child protection indicator show children in child protection count.</value>
        public IPropertyRule IfChildrenInChildProtectionIndicatorShowChildrenInChildProtectionCount { get; set; }

        /// <summary>
        /// Gets or sets if children indicator show child questions.
        /// </summary>
        /// <value>If children indicator show child questions.</value>
        public IPropertyRule IfChildrenIndicatorShowChildQuestions { get; set; }

        /// <summary>
        /// Gets or sets if discharge show section.
        /// </summary>
        /// <value>If discharge show section.</value>
        public IPropertyRule IfDischargeShowSection { get; set; }

        /// <summary>
        /// Gets or sets if follow up or discharge hide show questions.
        /// </summary>
        /// <value>If follow up or discharge hide show questions.</value>
        public IPropertyRule IfFollowUpOrDischargeHideShowQuestions { get; set; }

        /// <summary>
        /// Gets or sets if follow up show section.
        /// </summary>
        /// <value>If follow up show section.</value>
        public IPropertyRule IfFollowUpShowSection { get; set; }

        /// <summary>
        /// Gets or sets if has been arrested show arrested drug count.
        /// </summary>
        /// <value>If has been arrested show arrested drug count.</value>
        public IPropertyRule IfHasBeenArrestedShowArrestedDrugCount { get; set; }

        /// <summary>
        /// Gets or sets if has sexual activity show sexual contact questions.
        /// </summary>
        /// <value>If has sexual activity show sexual contact questions.</value>
        public IPropertyRule IfHasSexualActivityShowSexualContactQuestions { get; set; }

        /// <summary>
        /// Gets or sets if has unprotected sex show unprotected sex questions.
        /// </summary>
        /// <value>If has unprotected sex show unprotected sex questions.</value>
        public IPropertyRule IfHasUnprotectedSexShowUnprotectedSexQuestions { get; set; }

        /// <summary>
        /// Gets or sets if hispanic latino indicator show questions.
        /// </summary>
        /// <value>If hispanic latino indicator show questions.</value>
        public IPropertyRule IfHispanicLatinoIndicatorShowQuestions { get; set; }

        /// <summary>
        /// Gets or sets if more then15 nights in jail then institution must be plave of living.
        /// </summary>
        /// <value>If more then15 nights in jail then institution must be plave of living.</value>
        public IRule IfMoreThen15NightsInJailThenInstitutionMustBePlaveOfLiving { get; set; }

        /// <summary>
        /// Gets or sets the illegal drug count cannot be greater then crime count.
        /// </summary>
        /// <value>The illegal drug count cannot be greater then crime count.</value>
        public IRule IllegalDrugCountCannotBeGreaterThanCrimeCount { get; set; }

        /// <summary>
        /// Gets or sets the injection drug indactor read only if any drug route is injection.
        /// </summary>
        /// <value>The injection drug indactor read only if any drug route is injection.</value>
        public IRule InjectionDrugIndactorReadOnlyIfAnyDrugRouteIsInjection { get; set; }

        /// <summary>
        /// Gets or sets the no drug day counts can be greater then illegal drugs day count.
        /// </summary>
        /// <value>The no drug day counts can be greater then illegal drugs day count.</value>
        public IRule NoDrugDayCountsCanBeGreaterThanIllegalDrugsDayCount { get; set; }

        /// <summary>
        /// Gets or sets the patient lost parental rights child count cannot be greater then child count.
        /// </summary>
        /// <value>The patient lost parental rights child count cannot be greater then child count.</value>
        public IRule PatientLostParentalRightsChildCountCannotBeGreaterThanChildCount { get; set; }

        /// <summary>
        /// Gets or sets the same day alcohol drugs day count cannot be greater then sum of any alcohol day count and illegal drugs day count.
        /// </summary>
        /// <value>The same day alcohol drugs day count cannot be greater then sum of any alcohol day count and illegal drugs day count.</value>
        public IRule SameDayAlcoholDrugsDayCountCannotBeGreaterThanSumOfAnyAlcoholDayCountAndIllegalDrugsDayCount { get; set; }

        /// <summary>
        /// Gets or sets the show after care specification note.
        /// </summary>
        /// <value>The show after care specification note.</value>
        public IPropertyRule ShowAfterCareSpecificationNote { get; set; }

        /// <summary>
        /// Gets or sets the show alcohol day count questions.
        /// </summary>
        /// <value>The show alcohol day count questions.</value>
        public IPropertyRule ShowAlcoholDayCountQuestions { get; set; }

        /// <summary>
        /// Gets or sets the show attend other groups count.
        /// </summary>
        /// <value>The show attend other groups count.</value>
        public IPropertyRule ShowAttendOtherGroupsCount { get; set; }

        /// <summary>
        /// Gets or sets the show attend religious groups count.
        /// </summary>
        /// <value>The show attend religious groups count.</value>
        public IPropertyRule ShowAttendReligiousGroupsCount { get; set; }

        /// <summary>
        /// Gets or sets the show attend voluntary groups count.
        /// </summary>
        /// <value>The show attend voluntary groups count.</value>
        public IPropertyRule ShowAttendVoluntaryGroupsCount { get; set; }

        /// <summary>
        /// Gets or sets the show case MGMT specification note.
        /// </summary>
        /// <value>The show case MGMT specification note.</value>
        public IPropertyRule ShowCaseMgmtSpecificationNote { get; set; }

        /// <summary>
        /// Gets or sets the show education specification note.
        /// </summary>
        /// <value>The show education specification note.</value>
        public IPropertyRule ShowEducationSpecificationNote { get; set; }

        /// <summary>
        /// Gets or sets the show er alcohol substance abuse time count.
        /// </summary>
        /// <value>The show er alcohol substance abuse time count.</value>
        public IPropertyRule ShowErAlcoholSubstanceAbuseTimeCount { get; set; }

        /// <summary>
        /// Gets or sets the show er mental emotional difficulties time count.
        /// </summary>
        /// <value>The show er mental emotional difficulties time count.</value>
        public IPropertyRule ShowErMentalEmotionalDifficultiesTimeCount { get; set; }

        /// <summary>
        /// Gets or sets the show er physical complaint time count.
        /// </summary>
        /// <value>The show er physical complaint time count.</value>
        public IPropertyRule ShowErPhysicalComplaintTimeCount { get; set; }

        /// <summary>
        /// Gets or sets the show ethnic group specification note.
        /// </summary>
        /// <value>The show ethnic group specification note.</value>
        public IPropertyRule ShowEthnicGroupSpecificationNote { get; set; }

        /// <summary>
        /// Gets or sets the show gpra discharge status other description.
        /// </summary>
        /// <value>The show gpra discharge status other description.</value>
        public IPropertyRule ShowGpraDischargeStatusOtherDescription { get; set; }

        /// <summary>
        /// Gets or sets the show gpra discharge termination reason.
        /// </summary>
        /// <value>The show gpra discharge termination reason.</value>
        public IPropertyRule ShowGpraDischargeTerminationReason { get; set; }

        /// <summary>
        /// Gets or sets the show gpra follow up status other description.
        /// </summary>
        /// <value>The show gpra follow up status other description.</value>
        public IPropertyRule ShowGpraFollowUpStatusOtherDescription { get; set; }

        /// <summary>
        /// Gets or sets the type of the show gpra housing.
        /// </summary>
        /// <value>The type of the show gpra housing.</value>
        public IPropertyRule ShowGpraHousingType { get; set; }

        /// <summary>
        /// Gets or sets the show gpra psychological impact.
        /// </summary>
        /// <value>The show gpra psychological impact.</value>
        public IRule ShowGpraPsychologicalImpact { get; set; }

        /// <summary>
        /// Gets or sets the show gpra trouble contact specification note.
        /// </summary>
        /// <value>The show gpra trouble contact specification note.</value>
        public IPropertyRule ShowGpraTroubleContactSpecificationNote { get; set; }

        /// <summary>
        /// Gets or sets the show hiv test results known.
        /// </summary>
        /// <value>The show hiv test results known.</value>
        public IPropertyRule ShowHivTestResultsKnown { get; set; }

        /// <summary>
        /// Gets or sets the show injection gpra frequency of use of used items.
        /// </summary>
        /// <value>The show injection gpra frequency of use of used items.</value>
        public IPropertyRule ShowInjectionGpraFrequencyOfUseOfUsedItems { get; set; }

        /// <summary>
        /// Gets or sets the show inpatient alcohol substance abuse night count.
        /// </summary>
        /// <value>The show inpatient alcohol substance abuse night count.</value>
        public IPropertyRule ShowInpatientAlcoholSubstanceAbuseNightCount { get; set; }

        /// <summary>
        /// Gets or sets the show inpatient mental emotional difficulties night count.
        /// </summary>
        /// <value>The show inpatient mental emotional difficulties night count.</value>
        public IPropertyRule ShowInpatientMentalEmotionalDifficultiesNightCount { get; set; }

        /// <summary>
        /// Gets or sets the show inpatient physical complaint night count.
        /// </summary>
        /// <value>The show inpatient physical complaint night count.</value>
        public IPropertyRule ShowInpatientPhysicalComplaintNightCount { get; set; }

        /// <summary>
        /// Gets or sets the show medical specification note.
        /// </summary>
        /// <value>The show medical specification note.</value>
        public IPropertyRule ShowMedicalSpecificationNote { get; set; }

        /// <summary>
        /// Gets or sets the show modality specification note.
        /// </summary>
        /// <value>The show modality specification note.</value>
        public IPropertyRule ShowModalitySpecificationNote { get; set; }

        /// <summary>
        /// Gets or sets the show other housing type specification note.
        /// </summary>
        /// <value>The show other housing type specification note.</value>
        public IPropertyRule ShowOtherHousingTypeSpecificationNote { get; set; }

        /// <summary>
        /// Gets or sets the show other illegal drugs specification note.
        /// </summary>
        /// <value>The show other illegal drugs specification note.</value>
        public IPropertyRule ShowOtherIllegalDrugsSpecificationNote { get; set; }

        /// <summary>
        /// Gets or sets the show other specification description.
        /// </summary>
        /// <value>The show other specification description.</value>
        public IPropertyRule ShowOtherSpecificationDescription { get; set; }

        /// <summary>
        /// Gets or sets the show outpatient alcohol substance abuse time count.
        /// </summary>
        /// <value>The show outpatient alcohol substance abuse time count.</value>
        public IPropertyRule ShowOutpatientAlcoholSubstanceAbuseTimeCount { get; set; }

        /// <summary>
        /// Gets or sets the show outpatient mental emotional difficulties time count.
        /// </summary>
        /// <value>The show outpatient mental emotional difficulties time count.</value>
        public IPropertyRule ShowOutpatientMentalEmotionalDifficultiesTimeCount { get; set; }

        /// <summary>
        /// Gets or sets the show outpatient physical complaint time count.
        /// </summary>
        /// <value>The show outpatient physical complaint time count.</value>
        public IPropertyRule ShowOutpatientPhysicalComplaintTimeCount { get; set; }

        /// <summary>
        /// Gets or sets the show peer to peer recovery support specification note.
        /// </summary>
        /// <value>The show peer to peer recovery support specification note.</value>
        public IPropertyRule ShowPeerToPeerRecoverySupportSpecificationNote { get; set; }

        /// <summary>
        /// Gets or sets the show positive cooccuring mh sa screener indicator.
        /// </summary>
        /// <value>The show positive cooccuring mh sa screener indicator.</value>
        public IPropertyRule ShowPositiveCooccuringMhSaScreenerIndicator { get; set; }

        /// <summary>
        /// Gets or sets the show pregnancy indicator.
        /// </summary>
        /// <value>The show pregnancy indicator.</value>
        public IPropertyRule ShowPregnancyIndicator { get; set; }

        /// <summary>
        /// Gets or sets the show same day alcohol drugs day count.
        /// </summary>
        /// <value>The show same day alcohol drugs day count.</value>
        public IRule ShowSameDayAlcoholDrugsDayCount { get; set; }

        /// <summary>
        /// Gets or sets the show treatment specification note.
        /// </summary>
        /// <value>The show treatment specification note.</value>
        public IPropertyRule ShowTreatmentSpecificationNote { get; set; }

        /// <summary>
        /// Gets or sets the sub alcohol day counts sum cannot be greater then any alcohol day count.
        /// </summary>
        /// <value>The sub alcohol day counts sum cannot be greater then any alcohol day count.</value>
        public IRule SubAlcoholDayCountsSumCannotBeGreaterThanAnyAlcoholDayCount { get; set; }

        /// <summary>
        /// Gets or sets the un protected high sa sex contacts cannot be greater then unprotected sex contacts.
        /// </summary>
        /// <value>The un protected high sa sex contacts cannot be greater then unprotected sex contacts.</value>
        public IRule UnProtectedHighSaSexContactsCannotBeGreaterThanUnprotectedSexContacts { get; set; }

        /// <summary>
        /// Gets or sets the un protected hiv sex contacts cannot be greater then unprotected sex contacts.
        /// </summary>
        /// <value>The un protected hiv sex contacts cannot be greater then unprotected sex contacts.</value>
        public IRule UnProtectedHivSexContactsCannotBeGreaterThanUnprotectedSexContacts { get; set; }

        /// <summary>
        /// Gets or sets the un protected injection sex contacts cannot be greater then unprotected sex contacts.
        /// </summary>
        /// <value>The un protected injection sex contacts cannot be greater then unprotected sex contacts.</value>
        public IRule UnProtectedInjectionSexContactsCannotBeGreaterThanUnprotectedSexContacts { get; set; }

        /// <summary>
        /// Gets or sets the un protected sex contacts cannot be greater then sex contacts.
        /// </summary>
        /// <value>The un protected sex contacts cannot be greater then sex contacts.</value>
        public IRule UnProtectedSexContactsCannotBeGreaterThanSexContacts { get; set; }

        #endregion

        #region Methods

        private void BuildCrimeCriminalJusticeRules ()
        {
            NewPropertyRule ( () => IfHasBeenArrestedShowArrestedDrugCount )
                .WithProperty ( s => s.EditingDto.GpraCrimeCriminalJustice.ArrestedCount )
                .GreaterThan ( 0 )
                .Then ( s => s.EditingDto.GpraCrimeCriminalJustice.Show ( dto => dto.ArrestedDrugCount ) )
                .ElseThen ( s => s.EditingDto.GpraCrimeCriminalJustice.Hide ( dto => dto.ArrestedDrugCount ) );

            var arrestedError = new DataErrorInfo (
                "Drug related arrests cannot be more than number of arrests entered in Question 1.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<GpraCrimeCriminalJusticeDto, object> ( dto => dto.ArrestedDrugCount ) );
            NewRule ( () => DrugRelatedArrestsCannotBeGreaterThanNumberOfArrests )
                .RunForProperty ( s => s.EditingDto.GpraCrimeCriminalJustice.ArrestedDrugCount )
                .RunForProperty ( s => s.EditingDto.GpraCrimeCriminalJustice.ArrestedCount )
                .When ( s => s.EditingDto.GpraCrimeCriminalJustice.ArrestedDrugCount > s.EditingDto.GpraCrimeCriminalJustice.ArrestedCount )
                .ThenReportRuleViolation ( arrestedError.Message )
                .Then ( s => s.EditingDto.GpraCrimeCriminalJustice.TryAddDataErrorInfo ( arrestedError ) )
                .ElseThen ( s => s.EditingDto.GpraCrimeCriminalJustice.RemoveDataErrorInfo ( arrestedError ) );
        }

        private void BuildDemographicsRules ()
        {
            NewPropertyRule ( () => IfHispanicLatinoIndicatorShowQuestions )
                .WithProperty ( s => s.EditingDto.GpraDemographics.HispanicLatinoIndicator )
                .EqualTo ( true )
                .Then (
                    s =>
                        {
                            s.EditingDto.GpraDemographics.Show ( dto => dto.EthnicGroupCentralAmericanIndicator );
                            s.EditingDto.GpraDemographics.Show ( dto => dto.EthnicGroupCubanIndicator );
                            s.EditingDto.GpraDemographics.Show ( dto => dto.EthnicGroupDominicanIndicator );
                            s.EditingDto.GpraDemographics.Show ( dto => dto.EthnicGroupMexicanIndicator );
                            s.EditingDto.GpraDemographics.Show ( dto => dto.EthnicGroupPuertoRicanIndicator );
                            s.EditingDto.GpraDemographics.Show ( dto => dto.EthnicGroupSouthAmericanIndicator );
                            s.EditingDto.GpraDemographics.Show ( dto => dto.EthnicGroupOtherIndicator );
                        } )
                .ElseThen (
                    s =>
                        {
                            s.EditingDto.GpraDemographics.Hide ( dto => dto.EthnicGroupCentralAmericanIndicator );
                            s.EditingDto.GpraDemographics.Hide ( dto => dto.EthnicGroupCubanIndicator );
                            s.EditingDto.GpraDemographics.Hide ( dto => dto.EthnicGroupDominicanIndicator );
                            s.EditingDto.GpraDemographics.Hide ( dto => dto.EthnicGroupMexicanIndicator );
                            s.EditingDto.GpraDemographics.Hide ( dto => dto.EthnicGroupPuertoRicanIndicator );
                            s.EditingDto.GpraDemographics.Hide ( dto => dto.EthnicGroupSouthAmericanIndicator );
                            s.EditingDto.GpraDemographics.Hide ( dto => dto.EthnicGroupOtherIndicator );
                        } );

            NewPropertyRule ( () => ShowEthnicGroupSpecificationNote )
                .WithProperty ( s => s.EditingDto.GpraDemographics.EthnicGroupOtherIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraDemographics.Show ( dto => dto.EthnicGroupSpecificationNote ) )
                .ElseThen ( s => s.EditingDto.GpraDemographics.Show ( dto => dto.EthnicGroupSpecificationNote ) );

            NewPropertyRule ( () => ShowPregnancyIndicator )
                .WithProperty ( s => s.EditingDto.GpraDemographics.GpraPatientGender )
                .EqualToWellKnownName ( GpraPatientGender.Male )
                .Then ( s => s.EditingDto.GpraFamilyLivingConditions.Show ( dto => dto.PregnancyIndicator ) )
                .ElseThen ( s => s.EditingDto.GpraFamilyLivingConditions.Hide ( dto => dto.PregnancyIndicator ) );
        }

        private void BuildDischargeRules ()
        {
            Infrastructure.FluentRuleEngineExtensions.EqualToWellKnownName (
                NewPropertyRule ( () => ShowGpraDischargeTerminationReason )
                    .WithProperty ( s => s.EditingDto.GpraDischarge.GpraDischargeStatus ),
                GpraDischargeStatus.Termination )
                .Then ( s => s.EditingDto.GpraDischarge.Show ( dto => dto.GpraDischargeTerminationReason ) )
                .ElseThen ( s => s.EditingDto.GpraDischarge.Hide ( dto => dto.GpraDischargeTerminationReason ) );

            Infrastructure.FluentRuleEngineExtensions.EqualToWellKnownName (
                NewPropertyRule ( () => ShowGpraDischargeStatusOtherDescription )
                    .WithProperty ( s => s.EditingDto.GpraDischarge.GpraDischargeTerminationReason ),
                GpraDischargeTerminationReason.Other )
                .Then ( s => s.EditingDto.GpraDischarge.Show ( dto => dto.GpraDischargeStatusOtherDescription ) )
                .ElseThen ( s => s.EditingDto.GpraDischarge.Hide ( dto => dto.GpraDischargeStatusOtherDescription ) );
        }

        private void BuildDrugAlcoholUseRules ()
        {
            NewPropertyRule ( () => ShowAlcoholDayCountQuestions )
                .WithProperty ( s => s.EditingDto.GpraDrugAlcoholUse.AnyAlcoholDayCount )
                .GreaterThan ( 0 )
                .Then (
                    s =>
                        {
                            s.EditingDto.GpraDrugAlcoholUse.Show ( dto => dto.AlcoholIntoxicationFivePlusDrinksDayCount );
                            s.EditingDto.GpraDrugAlcoholUse.Show ( dto => dto.AlcoholIntoxicationFourOrFewerDrinksDayCount );
                        } )
                .ElseThen (
                    s =>
                        {
                            s.EditingDto.GpraDrugAlcoholUse.Hide ( dto => dto.AlcoholIntoxicationFivePlusDrinksDayCount );
                            s.EditingDto.GpraDrugAlcoholUse.Hide ( dto => dto.AlcoholIntoxicationFourOrFewerDrinksDayCount );
                        } );

            NewRule ( () => ShowSameDayAlcoholDrugsDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.AnyAlcoholDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount )
                .When (
                    s =>
                    s.EditingDto.GpraDrugAlcoholUse.AnyAlcoholDayCount > 0 &&
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount > 0
                )
                .Then ( s => s.EditingDto.GpraDrugAlcoholUse.Show ( dto => dto.SameDayAlcoholDrugsDayCount ) )
                .ElseThen ( s => s.EditingDto.GpraDrugAlcoholUse.Hide ( dto => dto.SameDayAlcoholDrugsDayCount ) );

            var alcoholDaysb1Error = new DataErrorInfo (
                "b1 cannot be greater than a.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<GpraDrugAlcoholUseDto, object> ( dto => dto.AnyAlcoholDayCount ) );
            NewRule ( () => AlcoholIntoxicationFivePlusDrinksDayCountCannotBeGreaterThanAnyAlcoholDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.AnyAlcoholDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.AlcoholIntoxicationFivePlusDrinksDayCount )
                .When (
                    s =>
                    s.EditingDto.GpraDrugAlcoholUse.AlcoholIntoxicationFivePlusDrinksDayCount > s.EditingDto.GpraDrugAlcoholUse.AnyAlcoholDayCount )
                .ThenReportRuleViolation ( alcoholDaysb1Error.Message )
                .Then ( s => s.EditingDto.GpraDrugAlcoholUse.TryAddDataErrorInfo ( alcoholDaysb1Error ) )
                .ElseThen ( s => s.EditingDto.GpraDrugAlcoholUse.RemoveDataErrorInfo ( alcoholDaysb1Error ) );

            var alcoholDaysb2Error = new DataErrorInfo (
                "b2 cannot be greater than a.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<GpraDrugAlcoholUseDto, object> ( dto => dto.AnyAlcoholDayCount ) );
            NewRule ( () => AlcoholIntoxicationFourOrFewerDrinksDayCountCannotBeGreaterThanAnyAlcoholDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.AnyAlcoholDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.AlcoholIntoxicationFourOrFewerDrinksDayCount )
                .When (
                    s =>
                    s.EditingDto.GpraDrugAlcoholUse.AlcoholIntoxicationFourOrFewerDrinksDayCount > s.EditingDto.GpraDrugAlcoholUse.AnyAlcoholDayCount )
                .ThenReportRuleViolation ( alcoholDaysb2Error.Message )
                .Then ( s => s.EditingDto.GpraDrugAlcoholUse.TryAddDataErrorInfo ( alcoholDaysb2Error ) )
                .ElseThen ( s => s.EditingDto.GpraDrugAlcoholUse.RemoveDataErrorInfo ( alcoholDaysb2Error ) );

            var alcoholDaysb1b2Error = new DataErrorInfo (
                "b1 + b2 cannot be greater than a.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<GpraDrugAlcoholUseDto, object> ( dto => dto.AnyAlcoholDayCount ) );
            NewRule ( () => SubAlcoholDayCountsSumCannotBeGreaterThanAnyAlcoholDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.AnyAlcoholDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.AlcoholIntoxicationFourOrFewerDrinksDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.AlcoholIntoxicationFivePlusDrinksDayCount )
                .When (
                    s =>
                    ( s.EditingDto.GpraDrugAlcoholUse.AlcoholIntoxicationFourOrFewerDrinksDayCount
                      + s.EditingDto.GpraDrugAlcoholUse.AlcoholIntoxicationFivePlusDrinksDayCount )
                    > s.EditingDto.GpraDrugAlcoholUse.AnyAlcoholDayCount )
                .ThenReportRuleViolation ( alcoholDaysb1b2Error.Message )
                .Then ( s => s.EditingDto.GpraDrugAlcoholUse.TryAddDataErrorInfo ( alcoholDaysb1b2Error ) )
                .ElseThen ( s => s.EditingDto.GpraDrugAlcoholUse.RemoveDataErrorInfo ( alcoholDaysb1b2Error ) );

            var alcoholDrugDaysError = new DataErrorInfo (
                "Cannot be greater than a + c.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<GpraDrugAlcoholUseDto, object> ( dto => dto.SameDayAlcoholDrugsDayCount ) );
            NewRule ( () => SameDayAlcoholDrugsDayCountCannotBeGreaterThanSumOfAnyAlcoholDayCountAndIllegalDrugsDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.SameDayAlcoholDrugsDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.AnyAlcoholDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount )
                .When (
                    s =>
                    s.EditingDto.GpraDrugAlcoholUse.SameDayAlcoholDrugsDayCount >
                    ( s.EditingDto.GpraDrugAlcoholUse.AnyAlcoholDayCount + s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount ) )
                .ThenReportRuleViolation ( alcoholDrugDaysError.Message )
                .Then ( s => s.EditingDto.GpraDrugAlcoholUse.TryAddDataErrorInfo ( alcoholDrugDaysError ) )
                .ElseThen ( s => s.EditingDto.GpraDrugAlcoholUse.RemoveDataErrorInfo ( alcoholDrugDaysError ) );

            var illegalDrugDaysError = new DataErrorInfo (
                "No value in Question 2 is can be greater than Illegal Drugs Number of Days.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<GpraDrugAlcoholUseDto, object> ( dto => dto.IllegalDrugsDayCount ) );
            NewRule ( () => NoDrugDayCountsCanBeGreaterThanIllegalDrugsDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.CocaineCrackDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.MarijuanaHashishDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.HeroinDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.MorphineDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.DiluadidDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.DermerolDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.PercocetDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.DarvonDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.CodeineDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.TylenolDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.OxycontinOxycodoneDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.NonPrescriptionMethadoneDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.HallucinogensDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.MethamphetamineDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.BenzondiazepinesDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.BarbituratesDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.NonPrescriptionGhbDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.KetamineDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.TranquilizersDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.InhalantsDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.OtherIllegalDrugsDayCount )
                .When (
                    s =>
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.CocaineCrackDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.MarijuanaHashishDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.HeroinDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.MorphineDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.DiluadidDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.DermerolDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.PercocetDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.DarvonDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.CodeineDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.TylenolDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.OxycontinOxycodoneDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.NonPrescriptionMethadoneDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.HallucinogensDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.MethamphetamineDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.BenzondiazepinesDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.BarbituratesDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.NonPrescriptionGhbDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.KetamineDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.TranquilizersDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.InhalantsDayCount ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount < s.EditingDto.GpraDrugAlcoholUse.OtherIllegalDrugsDayCount )
                .ThenReportRuleViolation ( illegalDrugDaysError.Message )
                .Then ( s => s.EditingDto.GpraDrugAlcoholUse.TryAddDataErrorInfo ( illegalDrugDaysError ) )
                .ElseThen ( s => s.EditingDto.GpraDrugAlcoholUse.RemoveDataErrorInfo ( illegalDrugDaysError ) );

            NewPropertyRule ( () => ShowOtherIllegalDrugsSpecificationNote )
                .WithProperty ( s => s.EditingDto.GpraDrugAlcoholUse.OtherIllegalDrugsDayCount )
                .GreaterThan ( 0 )
                .Then ( s => s.EditingDto.GpraDrugAlcoholUse.Show ( dto => dto.OtherIllegalDrugsSpecificationNote ) )
                .ElseThen ( s => s.EditingDto.GpraDrugAlcoholUse.Hide ( dto => dto.OtherIllegalDrugsSpecificationNote ) );

            NewRule ( () => InjectionDrugIndactorReadOnlyIfAnyDrugRouteIsInjection )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.CocaineCrackGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.MarijuanaHashishGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.HeroinGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.MorphineGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.DiluadidGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.DermerolGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.PercocetGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.DarvonGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.CodeineGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.TylenolGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.OxycontinOxycodoneGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.NonPrescriptionMethodoneGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.HallucinogensGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.MethamphetamineGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.BenzondiazepinesGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.BarbituratesGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.NonPrescriptionGhbGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.KetamineGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.TranquilizersGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.InhalantsGpraDrugRoute )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.OtherIllegalDrugsGpraDrugRoute )
                .When (
                    s =>
                    s.EditingDto.GpraDrugAlcoholUse.CocaineCrackGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.CocaineCrackGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.MarijuanaHashishGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.MarijuanaHashishGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.HeroinGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.HeroinGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.MorphineGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.MorphineGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.DiluadidGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.DiluadidGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.DermerolGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.DermerolGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.PercocetGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.PercocetGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.DarvonGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.DarvonGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.CodeineGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.CodeineGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.TylenolGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.TylenolGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.OxycontinOxycodoneGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.OxycontinOxycodoneGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.NonPrescriptionMethodoneGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.NonPrescriptionMethodoneGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.HallucinogensGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.HallucinogensGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.MethamphetamineGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.MethamphetamineGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.BenzondiazepinesGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.BenzondiazepinesGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.BarbituratesGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.BarbituratesGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.NonPrescriptionGhbGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.NonPrescriptionGhbGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.KetamineGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.KetamineGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.TranquilizersGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.TranquilizersGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.InhalantsGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.InhalantsGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) ||
                    s.EditingDto.GpraDrugAlcoholUse.OtherIllegalDrugsGpraDrugRoute.EqualsWellKnownName (
                        GpraDrugRoute.NonIVInjections ) ||
                    s.EditingDto.GpraDrugAlcoholUse.OtherIllegalDrugsGpraDrugRoute.EqualsWellKnownName ( GpraDrugRoute.IV ) )
                .Then (
                    s =>
                        {
                            s.EditingDto.GpraDrugAlcoholUse.InjectedDrugsIndicator.Value = true;
                            s.EditingDto.GpraDrugAlcoholUse.IsReadOnly ( dto => dto.InjectedDrugsIndicator );
                        } )
                .ElseThen (
                    s =>
                        {
                            s.EditingDto.GpraDrugAlcoholUse.InjectedDrugsIndicator.Value = null;
                            s.EditingDto.GpraDrugAlcoholUse.IsNotReadOnly ( dto => dto.InjectedDrugsIndicator );
                        } );

            NewPropertyRule ( () => ShowInjectionGpraFrequencyOfUseOfUsedItems )
                .WithProperty ( s => s.EditingDto.GpraDrugAlcoholUse.InjectedDrugsIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraDrugAlcoholUse.Show ( dto => dto.InjectionGpraFrequencyOfUseOfUsedItems ) )
                .ElseThen ( s => s.EditingDto.GpraDrugAlcoholUse.Hide ( dto => dto.InjectionGpraFrequencyOfUseOfUsedItems ) );

            NewRule ( () => FilterGpraFamilyLivingConditionLookups )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.AnyAlcoholDayCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount )
                .When (
                    s =>
                    s.EditingDto.GpraDrugAlcoholUse.AnyAlcoholDayCount > 0 ||
                    s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount > 0 )
                .Then (
                    s =>
                        {
                            s.EditingDto.GpraFamilyLivingConditions.FilterLkp (
                                dto => dto.StressGpraEffectDueToDrugUse, GpraEffectDueToDrugUse.NotApplicable );
                            s.EditingDto.GpraFamilyLivingConditions.FilterLkp (
                                dto => dto.GiveUpImportantActivitiesGpraEffectDueToDrugUse,
                                GpraEffectDueToDrugUse.NotApplicable );
                            s.EditingDto.GpraFamilyLivingConditions.FilterLkp (
                                dto => dto.EmotionalProblemsGpraEffectDueToDrugUse, GpraEffectDueToDrugUse.NotApplicable );
                        } )
                .ElseThen (
                    s =>
                        {
                            s.EditingDto.GpraFamilyLivingConditions.UnFilterLkp ( dto => dto.StressGpraEffectDueToDrugUse );
                            s.EditingDto.GpraFamilyLivingConditions.UnFilterLkp ( dto => dto.GiveUpImportantActivitiesGpraEffectDueToDrugUse );
                            s.EditingDto.GpraFamilyLivingConditions.UnFilterLkp ( dto => dto.EmotionalProblemsGpraEffectDueToDrugUse );
                        } );

            var commitCrimeError =
                new DataErrorInfo (
                    "Crimes commited must be greater then or equal to the number of illegal drugs used, because doing illegal drugs is a crime.",
                    ErrorLevel.Error,
                    PropertyUtil.ExtractPropertyName<GpraCrimeCriminalJusticeDto, object> ( dto => dto.CrimeCount ) );
            NewRule ( () => IllegalDrugCountCannotBeGreaterThanCrimeCount )
                .RunForProperty ( s => s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount )
                .RunForProperty ( s => s.EditingDto.GpraCrimeCriminalJustice.CrimeCount )
                .When ( s => s.EditingDto.GpraDrugAlcoholUse.IllegalDrugsDayCount > s.EditingDto.GpraCrimeCriminalJustice.CrimeCount )
                .ThenReportRuleViolation ( commitCrimeError.Message )
                .Then ( s => s.EditingDto.GpraCrimeCriminalJustice.TryAddDataErrorInfo ( commitCrimeError ) )
                .ElseThen ( s => s.EditingDto.GpraCrimeCriminalJustice.RemoveDataErrorInfo ( commitCrimeError ) );
        }

        private void BuildFamilyLivingConditionsRules ()
        {
            NewPropertyRule ( () => ShowGpraHousingType )
                .WithProperty ( s => s.EditingDto.GpraFamilyLivingConditions.MostTimeGpraPlaceToLive )
                .EqualToWellKnownName ( GpraPlaceToLive.Housed )
                .Then ( s => s.EditingDto.GpraFamilyLivingConditions.Show ( dto => dto.GpraHousingType ) )
                .ElseThen ( s => s.EditingDto.GpraFamilyLivingConditions.Hide ( dto => dto.GpraHousingType ) );

            NewPropertyRule ( () => ShowOtherHousingTypeSpecificationNote )
                .WithProperty ( s => s.EditingDto.GpraFamilyLivingConditions.GpraHousingType )
                .EqualToWellKnownName ( GpraHousingType.Other )
                .Then ( s => s.EditingDto.GpraFamilyLivingConditions.Show ( dto => dto.OtherHousingTypeSpecificationNote ) )
                .ElseThen ( s => s.EditingDto.GpraFamilyLivingConditions.Hide ( dto => dto.OtherHousingTypeSpecificationNote ) );

            NewPropertyRule ( () => IfChildrenIndicatorShowChildQuestions )
                .WithProperty ( s => s.EditingDto.GpraFamilyLivingConditions.ChildrenIndicator )
                .EqualTo ( true )
                .Then (
                    s =>
                        {
                            s.EditingDto.GpraFamilyLivingConditions.Show ( dto => dto.ChildCount );
                            s.EditingDto.GpraFamilyLivingConditions.Show ( dto => dto.ChildrenInChildProtectionIndicator );
                            s.EditingDto.GpraFamilyLivingConditions.Show ( dto => dto.PatientLostParentalRightsChildCount );
                            s.EditingDto.GpraFamilyLivingConditions.Show ( dto => dto.ChildrenInChildProtectionCount );
                        } )
                .ElseThen (
                    s =>
                        {
                            s.EditingDto.GpraFamilyLivingConditions.Hide ( dto => dto.ChildCount );
                            s.EditingDto.GpraFamilyLivingConditions.Hide ( dto => dto.ChildrenInChildProtectionIndicator );
                            s.EditingDto.GpraFamilyLivingConditions.Hide ( dto => dto.PatientLostParentalRightsChildCount );
                            s.EditingDto.GpraFamilyLivingConditions.Hide ( dto => dto.ChildrenInChildProtectionCount );
                        } );

            NewPropertyRule ( () => IfChildrenInChildProtectionIndicatorShowChildrenInChildProtectionCount )
                .WithProperty ( s => s.EditingDto.GpraFamilyLivingConditions.ChildrenInChildProtectionIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraFamilyLivingConditions.Show ( dto => dto.ChildrenInChildProtectionCount ) )
                .ElseThen ( s => s.EditingDto.GpraFamilyLivingConditions.Show ( dto => dto.ChildrenInChildProtectionCount ) );

            var childInProtectionCountError = new DataErrorInfo (
                "Cannot have more children living with someone else than you have.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<GpraFamilyLivingConditionsDto, object> ( dto => dto.ChildrenInChildProtectionCount ) );
            NewRule ( () => ChildrenInChildProtectionCountCannotBeGreaterThanChildCount )
                .RunForProperty ( s => s.EditingDto.GpraFamilyLivingConditions.ChildrenInChildProtectionCount )
                .RunForProperty ( s => s.EditingDto.GpraFamilyLivingConditions.ChildCount )
                .When (
                    s => s.EditingDto.GpraFamilyLivingConditions.ChildrenInChildProtectionCount > s.EditingDto.GpraFamilyLivingConditions.ChildCount )
                .ThenReportRuleViolation ( childInProtectionCountError.Message )
                .Then ( s => s.EditingDto.GpraFamilyLivingConditions.TryAddDataErrorInfo ( childInProtectionCountError ) )
                .ElseThen ( s => s.EditingDto.GpraFamilyLivingConditions.RemoveDataErrorInfo ( childInProtectionCountError ) );

            var childLostCountError = new DataErrorInfo (
                "Cannot have lost more children than you have.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<GpraFamilyLivingConditionsDto, object> ( dto => dto.PatientLostParentalRightsChildCount ) );
            NewRule ( () => PatientLostParentalRightsChildCountCannotBeGreaterThanChildCount )
                .RunForProperty ( s => s.EditingDto.GpraFamilyLivingConditions.PatientLostParentalRightsChildCount )
                .RunForProperty ( s => s.EditingDto.GpraFamilyLivingConditions.ChildCount )
                .When (
                    s =>
                    s.EditingDto.GpraFamilyLivingConditions.PatientLostParentalRightsChildCount > s.EditingDto.GpraFamilyLivingConditions.ChildCount )
                .ThenReportRuleViolation ( childLostCountError.Message )
                .Then ( s => s.EditingDto.GpraFamilyLivingConditions.TryAddDataErrorInfo ( childLostCountError ) )
                .ElseThen ( s => s.EditingDto.GpraFamilyLivingConditions.RemoveDataErrorInfo ( childLostCountError ) );

            var livingCrimeError =
                new DataErrorInfo (
                    "If you have spent more than 15 nights in prison/jail than your living situation must be Institution",
                    ErrorLevel.Error,
                    PropertyUtil.ExtractPropertyName<GpraCrimeCriminalJusticeDto, object> ( dto => dto.NightsConfinedCount ) );
            NewRule ( () => IfMoreThen15NightsInJailThenInstitutionMustBePlaveOfLiving )
                .RunForProperty ( s => s.EditingDto.GpraCrimeCriminalJustice.NightsConfinedCount )
                .RunForProperty ( s => s.EditingDto.GpraFamilyLivingConditions.MostTimeGpraPlaceToLive )
                .When (
                    s => s.EditingDto.GpraCrimeCriminalJustice.NightsConfinedCount > 15 &&
                         !s.EditingDto.GpraFamilyLivingConditions.MostTimeGpraPlaceToLive.EqualsWellKnownName ( GpraPlaceToLive.Institution ) )
                .ThenReportRuleViolation ( livingCrimeError.Message )
                .Then ( s => s.EditingDto.GpraCrimeCriminalJustice.TryAddDataErrorInfo ( livingCrimeError ) )
                .ElseThen ( s => s.EditingDto.GpraCrimeCriminalJustice.RemoveDataErrorInfo ( livingCrimeError ) );
        }

        private void BuildFollowupRules ()
        {
            Infrastructure.FluentRuleEngineExtensions.EqualToWellKnownName (
                NewPropertyRule ( () => ShowGpraFollowUpStatusOtherDescription )
                    .WithProperty ( s => s.EditingDto.GpraFollowUp.GpraFollowUpStatus ),
                GpraFollowupStatus.Other )
                .Then ( s => s.EditingDto.GpraFollowUp.Show ( dto => dto.GpraFollowUpStatusOtherDescription ) )
                .ElseThen ( s => s.EditingDto.GpraFollowUp.Hide ( dto => dto.GpraFollowUpStatusOtherDescription ) );
        }

        private void BuildPlannedServicesRules ()
        {
            NewPropertyRule ( () => ShowModalitySpecificationNote )
                .WithProperty ( s => s.EditingDto.GpraPlannedServices.ModalityOtherSpecificationIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraPlannedServices.Show ( dto => dto.ModalitySpecificationNote ) )
                .ElseThen ( s => s.EditingDto.GpraPlannedServices.Hide ( dto => dto.ModalitySpecificationNote ) );

            NewPropertyRule ( () => ShowTreatmentSpecificationNote )
                .WithProperty ( s => s.EditingDto.GpraPlannedServices.TreatmentOtherSpecificationIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraPlannedServices.Show ( dto => dto.TreatmentSpecificationNote ) )
                .ElseThen ( s => s.EditingDto.GpraPlannedServices.Hide ( dto => dto.TreatmentSpecificationNote ) );

            NewPropertyRule ( () => ShowCaseMgmtSpecificationNote )
                .WithProperty ( s => s.EditingDto.GpraPlannedServices.CaseMgmtOtherIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraPlannedServices.Show ( dto => dto.CaseMgmtSpecificationNote ) )
                .ElseThen ( s => s.EditingDto.GpraPlannedServices.Hide ( dto => dto.CaseMgmtSpecificationNote ) );

            NewPropertyRule ( () => ShowMedicalSpecificationNote )
                .WithProperty ( s => s.EditingDto.GpraPlannedServices.MedicalOtherIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraPlannedServices.Show ( dto => dto.MedicalSpecificationNote ) )
                .ElseThen ( s => s.EditingDto.GpraPlannedServices.Hide ( dto => dto.MedicalSpecificationNote ) );

            NewPropertyRule ( () => ShowAfterCareSpecificationNote )
                .WithProperty ( s => s.EditingDto.GpraPlannedServices.AfterCareOtherIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraPlannedServices.Show ( dto => dto.AfterCareSpecificationNote ) )
                .ElseThen ( s => s.EditingDto.GpraPlannedServices.Hide ( dto => dto.AfterCareSpecificationNote ) );

            NewPropertyRule ( () => ShowEducationSpecificationNote )
                .WithProperty ( s => s.EditingDto.GpraPlannedServices.EducationOtherIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraPlannedServices.Show ( dto => dto.EducationSpecificationNote ) )
                .ElseThen ( s => s.EditingDto.GpraPlannedServices.Hide ( dto => dto.EducationSpecificationNote ) );

            NewPropertyRule ( () => ShowPeerToPeerRecoverySupportSpecificationNote )
                .WithProperty ( s => s.EditingDto.GpraPlannedServices.PeerToPeerRecoverySupportOtherIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraPlannedServices.Show ( dto => dto.PeerToPeerRecoverySupportSpecificationNote ) )
                .ElseThen ( s => s.EditingDto.GpraPlannedServices.Hide ( dto => dto.PeerToPeerRecoverySupportSpecificationNote ) );
        }

        private void BuildProblemTreatmentRecoveryRules ()
        {
            NewPropertyRule ( () => IfHasSexualActivityShowSexualContactQuestions )
                .WithProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.GpraSexualActivity )
                .EqualToWellKnownName ( GpraSexualActivity.Yes )
                .Then (
                    s =>
                        {
                            s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.SexualContactsCount );
                            s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.UnprotectedSexualContactsCount );
                        } )
                .ElseThen (
                    s =>
                        {
                            s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.SexualContactsCount );
                            s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.UnprotectedSexualContactsCount );
                        } );

            NewPropertyRule ( () => IfHasUnprotectedSexShowUnprotectedSexQuestions )
                .WithProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.UnprotectedSexualContactsCount )
                .GreaterThan ( 0 )
                .Then (
                    s =>
                        {
                            s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.UnprotectedSexualHivContactsCount );
                            s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.UnprotectedSexualInjectionDrugContactsCount );
                            s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.UnprotectedSexualHighSaContactsCount );
                        } )
                .ElseThen (
                    s =>
                        {
                            s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.UnprotectedSexualHivContactsCount );
                            s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.UnprotectedSexualInjectionDrugContactsCount );
                            s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.UnprotectedSexualHighSaContactsCount );
                        } );

            var unprotectContactsError = new DataErrorInfo (
                "Value cannot be greater than total sexual contacts.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<GpraProblemsTreatmentRecoveryDto, object> ( dto => dto.UnprotectedSexualContactsCount ) );
            NewRule ( () => UnProtectedSexContactsCannotBeGreaterThanSexContacts )
                .RunForProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.UnprotectedSexualContactsCount )
                .RunForProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.SexualContactsCount )
                .When (
                    s =>
                    s.EditingDto.GpraProblemsTreatmentRecovery.UnprotectedSexualContactsCount
                    > s.EditingDto.GpraProblemsTreatmentRecovery.SexualContactsCount )
                .ThenReportRuleViolation ( unprotectContactsError.Message )
                .Then ( s => s.EditingDto.GpraProblemsTreatmentRecovery.TryAddDataErrorInfo ( unprotectContactsError ) )
                .ElseThen ( s => s.EditingDto.GpraProblemsTreatmentRecovery.RemoveDataErrorInfo ( unprotectContactsError ) );

            var unprotectHivContactsError = new DataErrorInfo (
                "Value cannot be greater than total unprotected sexual contacts.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<GpraProblemsTreatmentRecoveryDto, object> ( dto => dto.UnprotectedSexualHivContactsCount ) );
            NewRule ( () => UnProtectedHivSexContactsCannotBeGreaterThanUnprotectedSexContacts )
                .RunForProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.UnprotectedSexualContactsCount )
                .RunForProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.UnprotectedSexualHivContactsCount )
                .When (
                    s =>
                    s.EditingDto.GpraProblemsTreatmentRecovery.UnprotectedSexualHivContactsCount
                    > s.EditingDto.GpraProblemsTreatmentRecovery.UnprotectedSexualContactsCount )
                .ThenReportRuleViolation ( unprotectHivContactsError.Message )
                .Then ( s => s.EditingDto.GpraProblemsTreatmentRecovery.TryAddDataErrorInfo ( unprotectHivContactsError ) )
                .ElseThen ( s => s.EditingDto.GpraProblemsTreatmentRecovery.RemoveDataErrorInfo ( unprotectHivContactsError ) );

            var unprotectInjectionContactsError = new DataErrorInfo (
                "Value cannot be greater than total unprotected sexual contacts.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<GpraProblemsTreatmentRecoveryDto, object> ( dto => dto.UnprotectedSexualInjectionDrugContactsCount ) );
            NewRule ( () => UnProtectedInjectionSexContactsCannotBeGreaterThanUnprotectedSexContacts )
                .RunForProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.UnprotectedSexualContactsCount )
                .RunForProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.UnprotectedSexualInjectionDrugContactsCount )
                .When (
                    s =>
                    s.EditingDto.GpraProblemsTreatmentRecovery.UnprotectedSexualInjectionDrugContactsCount
                    > s.EditingDto.GpraProblemsTreatmentRecovery.UnprotectedSexualContactsCount )
                .ThenReportRuleViolation ( unprotectInjectionContactsError.Message )
                .Then ( s => s.EditingDto.GpraProblemsTreatmentRecovery.TryAddDataErrorInfo ( unprotectInjectionContactsError ) )
                .ElseThen ( s => s.EditingDto.GpraProblemsTreatmentRecovery.RemoveDataErrorInfo ( unprotectInjectionContactsError ) );

            var unprotectMhSaContactsError = new DataErrorInfo (
                "Value cannot be greater than total unprotected sexual contacts.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<GpraProblemsTreatmentRecoveryDto, object> ( dto => dto.UnprotectedSexualHighSaContactsCount ) );
            NewRule ( () => UnProtectedHighSaSexContactsCannotBeGreaterThanUnprotectedSexContacts )
                .RunForProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.UnprotectedSexualContactsCount )
                .RunForProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.UnprotectedSexualHighSaContactsCount )
                .When (
                    s =>
                    s.EditingDto.GpraProblemsTreatmentRecovery.UnprotectedSexualHighSaContactsCount
                    > s.EditingDto.GpraProblemsTreatmentRecovery.UnprotectedSexualContactsCount )
                .ThenReportRuleViolation ( unprotectMhSaContactsError.Message )
                .Then ( s => s.EditingDto.GpraProblemsTreatmentRecovery.TryAddDataErrorInfo ( unprotectMhSaContactsError ) )
                .ElseThen ( s => s.EditingDto.GpraProblemsTreatmentRecovery.RemoveDataErrorInfo ( unprotectMhSaContactsError ) );

            NewPropertyRule ( () => ShowHivTestResultsKnown )
                .WithProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.HivTestIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.HivTestResultsKnownIndicator ) )
                .ElseThen ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.HivTestResultsKnownIndicator ) );

            NewRule ( () => ShowGpraPsychologicalImpact )
                .RunForProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.DepressionDayCount )
                .RunForProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.AnxietyDayCount )
                .RunForProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.HallucinationsDayCount )
                .RunForProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.BrainMisfunctionDayCount )
                .RunForProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.ViolentBehaviorDayCount )
                .RunForProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.SuicideDayCount )
                .RunForProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.PsychologicalEmotionalMedicationDayCount )
                .When (
                    s =>
                    s.EditingDto.GpraProblemsTreatmentRecovery.DepressionDayCount > 0 ||
                    s.EditingDto.GpraProblemsTreatmentRecovery.AnxietyDayCount > 0 ||
                    s.EditingDto.GpraProblemsTreatmentRecovery.HallucinationsDayCount > 0 ||
                    s.EditingDto.GpraProblemsTreatmentRecovery.BrainMisfunctionDayCount > 0 ||
                    s.EditingDto.GpraProblemsTreatmentRecovery.ViolentBehaviorDayCount > 0 ||
                    s.EditingDto.GpraProblemsTreatmentRecovery.SuicideDayCount > 0 ||
                    s.EditingDto.GpraProblemsTreatmentRecovery.PsychologicalEmotionalMedicationDayCount > 0 )
                .Then ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.GpraPsychologicalImpact ) )
                .ElseThen ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.GpraPsychologicalImpact ) );

            NewPropertyRule ( () => ShowInpatientPhysicalComplaintNightCount )
                .WithProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.InpatientPhysicalComplaintIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.InpatientPhysicalComplaintNightCount ) )
                .ElseThen ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.InpatientPhysicalComplaintNightCount ) );

            NewPropertyRule ( () => ShowInpatientMentalEmotionalDifficultiesNightCount )
                .WithProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.InpatientMentalEmotionalDifficultiesIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.InpatientMentalEmotionalDifficultiesNightCount ) )
                .ElseThen ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.InpatientMentalEmotionalDifficultiesNightCount ) );

            NewPropertyRule ( () => ShowInpatientAlcoholSubstanceAbuseNightCount )
                .WithProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.InpatientAlcoholSubstanceAbuseIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.InpatientAlcoholSubstanceAbuseNightCount ) )
                .ElseThen ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.InpatientAlcoholSubstanceAbuseNightCount ) );

            NewPropertyRule ( () => ShowOutpatientPhysicalComplaintTimeCount )
                .WithProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.OutpatientPhysicalComplaintIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.OutpatientPhysicalComplaintTimeCount ) )
                .ElseThen ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.OutpatientPhysicalComplaintTimeCount ) );

            NewPropertyRule ( () => ShowOutpatientMentalEmotionalDifficultiesTimeCount )
                .WithProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.OutpatientMentalEmotionalDifficultiesIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.OutpatientMentalEmotionalDifficultiesTimeCount ) )
                .ElseThen ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.OutpatientMentalEmotionalDifficultiesTimeCount ) );

            NewPropertyRule ( () => ShowOutpatientAlcoholSubstanceAbuseTimeCount )
                .WithProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.OutpatientAlcoholSubstanceAbuseIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.OutpatientAlcoholSubstanceAbuseTimeCount ) )
                .ElseThen ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.OutpatientAlcoholSubstanceAbuseTimeCount ) );

            NewPropertyRule ( () => ShowErPhysicalComplaintTimeCount )
                .WithProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.ErPhysicalComplaintIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.ErPhysicalComplaintTimeCount ) )
                .ElseThen ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.ErPhysicalComplaintTimeCount ) );

            NewPropertyRule ( () => ShowErMentalEmotionalDifficultiesTimeCount )
                .WithProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.ErMentalEmotionalDifficultiesIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.ErMentalEmotionalDifficultiesTimeCount ) )
                .ElseThen ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.ErMentalEmotionalDifficultiesTimeCount ) );

            NewPropertyRule ( () => ShowErAlcoholSubstanceAbuseTimeCount )
                .WithProperty ( s => s.EditingDto.GpraProblemsTreatmentRecovery.ErAlcoholSubstanceAbuseIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Show ( dto => dto.ErAlcoholSubstanceAbuseTimeCount ) )
                .ElseThen ( s => s.EditingDto.GpraProblemsTreatmentRecovery.Hide ( dto => dto.ErAlcoholSubstanceAbuseTimeCount ) );
        }

        private void BuildRecordManagementRules ()
        {
            ShouldRunWhen (
                ctx =>
                ctx.EditingDto.GpraInterviewInfromationDto.GpraInterviewType != null
                && ctx.EditingDto.GpraInterviewInfromationDto.ConductedInterviewIndicator.HasValue,
                () =>
                NewRule ( () => HideSectionsIfIsFollowUpOrDischargedAndInterviewConducted )
                    .RunForProperty ( s => s.EditingDto.GpraInterviewInfromationDto.ConductedInterviewIndicator )
                    .RunForProperty ( s => s.EditingDto.GpraInterviewInfromationDto.GpraInterviewType )
                    .When (
                        s => s.EditingDto.GpraInterviewInfromationDto.ConductedInterviewIndicator == false &&
                             ( s.EditingDto.GpraInterviewInfromationDto.GpraInterviewType.WellKnownName == GpraInterviewType.ThreeMonthFollowup ||
                               s.EditingDto.GpraInterviewInfromationDto.GpraInterviewType.WellKnownName == GpraInterviewType.SixMonthFollowup ||
                               s.EditingDto.GpraInterviewInfromationDto.GpraInterviewType.WellKnownName == GpraInterviewType.Discharge ) )
                    .Then (
                        s =>
                            {
                                s.EditingDto.GpraCrimeCriminalJustice.Hide ();
                                s.EditingDto.GpraDrugAlcoholUse.Hide ();
                                s.EditingDto.GpraFamilyLivingConditions.Hide ();
                                s.EditingDto.GpraPlannedServices.Hide ();
                                s.EditingDto.GpraProblemsTreatmentRecovery.Hide ();
                                s.EditingDto.GpraProfessionalInformation.Hide ();
                                s.EditingDto.GpraSocialConnectedness.Hide ();
                            } )
                    .ElseThen (
                        s =>
                            {
                                s.EditingDto.GpraCrimeCriminalJustice.Show ();
                                s.EditingDto.GpraDrugAlcoholUse.Show ();
                                s.EditingDto.GpraFamilyLivingConditions.Show ();
                                s.EditingDto.GpraPlannedServices.Show ();
                                s.EditingDto.GpraProblemsTreatmentRecovery.Show ();
                                s.EditingDto.GpraProfessionalInformation.Show ();
                                s.EditingDto.GpraSocialConnectedness.Show ();
                            } )
                );

            ShouldRunWhen (
                ctx => ctx.EditingDto.GpraInterviewInfromationDto.GpraInterviewType != null,
                () =>
                    {
                        NewPropertyRule ( () => IfFollowUpShowSection )
                            .WithProperty ( s => s.EditingDto.GpraInterviewInfromationDto.GpraInterviewType.WellKnownName )
                            .InList (
                                GpraInterviewType.ThreeMonthFollowup,
                                GpraInterviewType.SixMonthFollowup )
                            .Then ( s => s.EditingDto.GpraFollowUp.Show () )
                            .ElseThen ( s => s.EditingDto.GpraFollowUp.Hide () );

                        NewPropertyRule ( () => IfDischargeShowSection )
                            .WithProperty ( s => s.EditingDto.GpraInterviewInfromationDto.GpraInterviewType.WellKnownName )
                            .EqualTo (
                                GpraInterviewType.Discharge )
                            .Then ( s => s.EditingDto.GpraDischarge.Show () )
                            .ElseThen ( s => s.EditingDto.GpraDischarge.Hide () );

                        NewPropertyRule ( () => IfFollowUpOrDischargeHideShowQuestions )
                            .WithProperty ( s => s.EditingDto.GpraInterviewInfromationDto.GpraInterviewType.WellKnownName )
                            .InList (
                                GpraInterviewType.ThreeMonthFollowup,
                                GpraInterviewType.SixMonthFollowup,
                                GpraInterviewType.Discharge )
                            .Then (
                                s =>
                                    {
                                        s.EditingDto.GpraInterviewInfromationDto.Show ( dto => dto.ConductedInterviewIndicator );
                                        s.EditingDto.GpraDemographics.Hide ();
                                        s.EditingDto.GpraPlannedServices.Hide ();
                                        s.EditingDto.GpraInterviewInfromationDto.Hide ( dto => dto.SbirtSbiPositiveIndicator );
                                        s.EditingDto.GpraInterviewInfromationDto.Hide ( dto => dto.AuditCScore );
                                        s.EditingDto.GpraInterviewInfromationDto.Hide ( dto => dto.CageScore );
                                        s.EditingDto.GpraInterviewInfromationDto.Hide ( dto => dto.DastScore );
                                        s.EditingDto.GpraInterviewInfromationDto.Hide ( dto => dto.Dast10Score );
                                        s.EditingDto.GpraInterviewInfromationDto.Hide ( dto => dto.NiaaaGuideScore );
                                        s.EditingDto.GpraInterviewInfromationDto.Hide ( dto => dto.AssistAlcoholSubScore );
                                        s.EditingDto.GpraInterviewInfromationDto.Hide ( dto => dto.OtherScore );
                                        s.EditingDto.GpraInterviewInfromationDto.Hide ( dto => dto.SbirtWillingIndicator );
                                    } )
                            .ElseThen (
                                s =>
                                    {
                                        s.EditingDto.GpraInterviewInfromationDto.Hide ( dto => dto.ConductedInterviewIndicator );
                                        s.EditingDto.GpraDemographics.Show ();
                                        s.EditingDto.GpraPlannedServices.Show ();
                                        s.EditingDto.GpraInterviewInfromationDto.Show ( dto => dto.SbirtSbiPositiveIndicator );
                                        s.EditingDto.GpraInterviewInfromationDto.Show ( dto => dto.AuditCScore );
                                        s.EditingDto.GpraInterviewInfromationDto.Show ( dto => dto.CageScore );
                                        s.EditingDto.GpraInterviewInfromationDto.Show ( dto => dto.DastScore );
                                        s.EditingDto.GpraInterviewInfromationDto.Show ( dto => dto.Dast10Score );
                                        s.EditingDto.GpraInterviewInfromationDto.Show ( dto => dto.NiaaaGuideScore );
                                        s.EditingDto.GpraInterviewInfromationDto.Show ( dto => dto.AssistAlcoholSubScore );
                                        s.EditingDto.GpraInterviewInfromationDto.Show ( dto => dto.OtherScore );
                                        s.EditingDto.GpraInterviewInfromationDto.Show ( dto => dto.SbirtWillingIndicator );
                                    } );
                    } );

            NewPropertyRule ( () => ShowOtherSpecificationDescription )
                .WithProperty ( s => s.EditingDto.GpraInterviewInfromationDto.OtherScore )
                .NotNull ()
                .Then ( s => s.EditingDto.GpraInterviewInfromationDto.Show ( dto => dto.OtherSpecificationDescription ) )
                .ElseThen ( s => s.EditingDto.GpraInterviewInfromationDto.Hide ( dto => dto.OtherSpecificationDescription ) );

            NewPropertyRule ( () => ShowPositiveCooccuringMhSaScreenerIndicator )
                .WithProperty ( s => s.EditingDto.GpraInterviewInfromationDto.CooccuringMhSaScreenerIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraInterviewInfromationDto.Show ( dto => dto.PositiveCooccuringMhSaScreenerIndicator ) )
                .ElseThen ( s => s.EditingDto.GpraInterviewInfromationDto.Hide ( dto => dto.PositiveCooccuringMhSaScreenerIndicator ) );
        }

        private void BuildSocialConnectednessRules ()
        {
            NewPropertyRule ( () => ShowAttendVoluntaryGroupsCount )
                .WithProperty ( s => s.EditingDto.GpraSocialConnectedness.AttendVoluntaryGroupsIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraSocialConnectedness.Show ( dto => dto.AttendVoluntaryGroupsCount ) )
                .ElseThen ( s => s.EditingDto.GpraSocialConnectedness.Hide ( dto => dto.AttendVoluntaryGroupsCount ) );

            NewPropertyRule ( () => ShowAttendReligiousGroupsCount )
                .WithProperty ( s => s.EditingDto.GpraSocialConnectedness.AttendReligiousGroupsIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraSocialConnectedness.Show ( dto => dto.AttendReligiousGroupsCount ) )
                .ElseThen ( s => s.EditingDto.GpraSocialConnectedness.Hide ( dto => dto.AttendReligiousGroupsCount ) );

            NewPropertyRule ( () => ShowAttendOtherGroupsCount )
                .WithProperty ( s => s.EditingDto.GpraSocialConnectedness.AttendOtherGroupsIndicator )
                .EqualTo ( true )
                .Then ( s => s.EditingDto.GpraSocialConnectedness.Show ( dto => dto.AttendOtherGroupsCount ) )
                .ElseThen ( s => s.EditingDto.GpraSocialConnectedness.Hide ( dto => dto.AttendOtherGroupsCount ) );

            NewPropertyRule ( () => ShowGpraTroubleContactSpecificationNote )
                .WithProperty ( s => s.EditingDto.GpraSocialConnectedness.GpraTroubleContact )
                .EqualToWellKnownName ( GpraTroubleContact.Other )
                .Then ( s => s.EditingDto.GpraSocialConnectedness.Show ( dto => dto.GpraTroubleContactSpecificationNote ) )
                .ElseThen ( s => s.EditingDto.GpraSocialConnectedness.Hide ( dto => dto.GpraTroubleContactSpecificationNote ) );
        }

        #endregion
    }
}
