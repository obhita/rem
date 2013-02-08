using System;
using System.Linq;
using Pillar.Domain;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiEmploymentStatusSection contains patient employment status information from the Employment Information section of the DensAsi. 
    /// <remarks>
    /// Included in each of these sections is the interviewer's severity rating, suggesting the client's need for treatment or additional treatment. 
    /// This is based on the information provided by the client. 
    /// </remarks>
    /// </summary>
    [Component]
    public class DensAsiEmploymentStatusSection : DensAsiInterviewSectionBase
    {
        private readonly DensAsiNonResponseType<bool?> _automobileAvailableforUseIndicator;
        private readonly string _automobileAvailableforUseIndicatorNote;
        private readonly bool? _confidenceDistortedByPatientMisrepresentationIndicator;
        private readonly string _confidenceDistortedByPatientMisrepresentationIndicatorNote;
        private readonly bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private readonly string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private readonly DensAsiNonResponseType<bool?> _contributionConstituteMajorityOfYourSupportIndicator;
        private readonly string _contributionConstituteMajorityOfYourSupportIndicatorNote;
        private readonly DensAsiNonResponseType<bool?> _contributionOfSomeoneToSupportIndicator;
        private readonly string _contributionOfSomeoneToSupportIndicatorNote;
        private readonly DensAsiNonResponseType<int?> _dependentPeopleCount;
        private readonly string _dependentPeopleCountNote;
        private readonly DensAsiNonResponseType<int?> _employmentProblemsDayCount;
        private readonly string _employmentProblemsDayCountNote;
        private readonly DensAsiNonResponseType<int?> _illegalAmount;
        private readonly string _illegalAmountNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _importanceOfEmploymentProblemCounselingDensAsiPatientRating;
        private readonly string _importanceOfEmploymentProblemCounselingDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<int?> _mateFamilyFriendsAmount;
        private readonly string _mateFamilyFriendsAmountNote;
        private readonly DensAsiNonResponseType<int?> _netIncomeAmount;
        private readonly string _netIncomeAmountNote;
        private readonly DensAsiNonResponseType<DensAsiEmploymentPattern> _pastThreeYearsDensAsiEmploymentPattern;
        private readonly string _pastThreeYearsDensAsiEmploymentPatternNote;
        private readonly DensAsiInterviewerRating _patientCounselingDensAsiInterviewerRating;
        private readonly string _patientCounselingDensAsiInterviewerRatingNote;
        private readonly DensAsiNonResponseType<int?> _pensionBenefitsSocialSecurityAmount;
        private readonly string _pensionBenefitsSocialSecurityAmountNote;
        private readonly string _professionTradeSkillDescription;
        private readonly DensAsiNonResponseType<bool?> _professionTradeSkillIndicator;
        private readonly string _professionTradeSkillNote;
        private readonly string _sectionNote;
        private readonly DensAsiNonResponseType<int?> _technicalEducationCompletedMonthCount;
        private readonly string _technicalEducationCompletedMonthCountNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _troubledByEmploymentProblemsDensAsiPatientRating;
        private readonly string _troubledByEmploymentProblemsDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<int?> _unemploymentCompensationAmount;
        private readonly string _unemploymentCompensationAmountNote;
        private readonly DensAsiNonResponseType<DensAsiOccupationType> _usualOrLastDensAsiOccupationType;
        private readonly string _usualOrLastOccupationDescription;
        private readonly string _usualOrLastOccupationNote;
        private readonly DensAsiNonResponseType<bool?> _validDriversLicenseIndicator;
        private readonly string _validDriversLicenseIndicatorNote;
        private readonly DensAsiNonResponseType<int?> _welfareAmount;
        private readonly string _welfareAmountNote;
        private readonly DensAsiNonResponseType<int?> _workInLastThirtyDaysPaidDayCount;
        private readonly string _workInLastThirtyDaysPaidDayCountNote;
        private readonly DensAsiNonResponseType<TimeSpan?> _yearsAndMonthsEducationCompletedTimeSpan;
        private readonly string _yearsAndMonthsEducationCompletedTimeSpanNote;
        private readonly DensAsiNonResponseType<TimeSpan?> _yearsAndMonthsOfLongestFullTimeJobTimeSpan;
        private readonly string _yearsAndMonthsOfLongestFullTimeJobTimeSpanNote;

        private DensAsiEmploymentStatusSection ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiEmploymentStatusSection"/> class.
        /// </summary>
        /// <param name="yearsAndMonthsEducationCompletedTimeSpan">The years and months education completed time span.</param>
        /// <param name="yearsAndMonthsEducationCompletedTimeSpanNote">The years and months education completed time span note.</param>
        /// <param name="technicalEducationCompletedMonthCount">The technical education completed month count.</param>
        /// <param name="technicalEducationCompletedMonthCountNote">The technical education completed month count note.</param>
        /// <param name="professionTradeSkillIndicator">The profession trade skill indicator.</param>
        /// <param name="professionTradeSkillDescription">The profession trade skill description.</param>
        /// <param name="professionTradeSkillNote">The profession trade skill note.</param>
        /// <param name="validDriversLicenseIndicator">The valid drivers license indicator.</param>
        /// <param name="validDriversLicenseIndicatorNote">The valid drivers license indicator note.</param>
        /// <param name="automobileAvailableforUseIndicator">The automobile availablefor use indicator.</param>
        /// <param name="automobileAvailableforUseIndicatorNote">The automobile availablefor use indicator note.</param>
        /// <param name="yearsAndMonthsOfLongestFullTimeJobTimeSpan">The years and months of longest full time job time span.</param>
        /// <param name="yearsAndMonthsOfLongestFullTimeJobTimeSpanNote">The years and months of longest full time job time span note.</param>
        /// <param name="usualOrLastDensAsiOccupationType">Type of the usual or last dens asi occupation.</param>
        /// <param name="usualOrLastOccupationDescription">The usual or last occupation description.</param>
        /// <param name="usualOrLastOccupationNote">The usual or last occupation note.</param>
        /// <param name="contributionOfSomeoneToSupportIndicator">The contribution of someone to support indicator.</param>
        /// <param name="contributionOfSomeoneToSupportIndicatorNote">The contribution of someone to support indicator note.</param>
        /// <param name="contributionConstituteMajorityOfYourSupportIndicator">The contribution constitute majority of your support indicator.</param>
        /// <param name="contributionConstituteMajorityOfYourSupportIndicatorNote">The contribution constitute majority of your support indicator note.</param>
        /// <param name="pastThreeYearsDensAsiEmploymentPattern">The past three years dens asi employment pattern.</param>
        /// <param name="pastThreeYearsDensAsiEmploymentPatternNote">The past three years dens asi employment pattern note.</param>
        /// <param name="workInLastThirtyDaysPaidDayCount">The work in last thirty days paid day count.</param>
        /// <param name="workInLastThirtyDaysPaidDayCountNote">The work in last thirty days paid day count note.</param>
        /// <param name="netIncomeAmount">The net income amount.</param>
        /// <param name="netIncomeAmountNote">The net income amount note.</param>
        /// <param name="unemploymentCompensationAmount">The unemployment compensation amount.</param>
        /// <param name="unemploymentCompensationAmountNote">The unemployment compensation amount note.</param>
        /// <param name="welfareAmount">The welfare amount.</param>
        /// <param name="welfareAmountNote">The welfare amount note.</param>
        /// <param name="pensionBenefitsSocialSecurityAmount">The pension benefits social security amount.</param>
        /// <param name="pensionBenefitsSocialSecurityAmountNote">The pension benefits social security amount note.</param>
        /// <param name="mateFamilyFriendsAmount">The mate family friends amount.</param>
        /// <param name="mateFamilyFriendsAmountNote">The mate family friends amount note.</param>
        /// <param name="illegalAmount">The illegal amount.</param>
        /// <param name="illegalAmountNote">The illegal amount note.</param>
        /// <param name="dependentPeopleCount">The dependent people count.</param>
        /// <param name="dependentPeopleCountNote">The dependent people count note.</param>
        /// <param name="employmentProblemsDayCount">The employment problems day count.</param>
        /// <param name="employmentProblemsDayCountNote">The employment problems day count note.</param>
        /// <param name="troubledByEmploymentProblemsDensAsiPatientRating">The troubled by employment problems dens asi patient rating.</param>
        /// <param name="troubledByEmploymentProblemsDensAsiPatientRatingNote">The troubled by employment problems dens asi patient rating note.</param>
        /// <param name="importanceOfEmploymentProblemCounselingDensAsiPatientRating">The importance of employment problem counseling dens asi patient rating.</param>
        /// <param name="importanceOfEmploymentProblemCounselingDensAsiPatientRatingNote">The importance of employment problem counseling dens asi patient rating note.</param>
        /// <param name="patientCounselingDensAsiInterviewerRating">The patient counseling dens asi interviewer rating.</param>
        /// <param name="patientCounselingDensAsiInterviewerRatingNote">The patient counseling dens asi interviewer rating note.</param>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicator">The confidence distorted by patient misrepresentation indicator.</param>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicatorNote">The confidence distorted by patient misrepresentation indicator note.</param>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicator">The confidence rate distorted by patient inability to understand indicator.</param>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote">The confidence rate distorted by patient inability to understand indicator note.</param>
        /// <param name="sectionNote">The section note.</param>
        public DensAsiEmploymentStatusSection ( DensAsiNonResponseType<TimeSpan?> yearsAndMonthsEducationCompletedTimeSpan,
                                                    string yearsAndMonthsEducationCompletedTimeSpanNote,
                                                    DensAsiNonResponseType<int?> technicalEducationCompletedMonthCount,
                                                    string technicalEducationCompletedMonthCountNote,
                                                    DensAsiNonResponseType<bool?> professionTradeSkillIndicator,
                                                    string professionTradeSkillDescription,
                                                    string professionTradeSkillNote,
                                                    DensAsiNonResponseType<bool?> validDriversLicenseIndicator,
                                                    string validDriversLicenseIndicatorNote,
                                                    DensAsiNonResponseType<bool?> automobileAvailableforUseIndicator,
                                                    string automobileAvailableforUseIndicatorNote,
                                                    DensAsiNonResponseType<TimeSpan?> yearsAndMonthsOfLongestFullTimeJobTimeSpan,
                                                    string yearsAndMonthsOfLongestFullTimeJobTimeSpanNote,
                                                    DensAsiNonResponseType<DensAsiOccupationType> usualOrLastDensAsiOccupationType,
                                                    string usualOrLastOccupationDescription,
                                                    string usualOrLastOccupationNote,
                                                    DensAsiNonResponseType<bool?> contributionOfSomeoneToSupportIndicator,
                                                    string contributionOfSomeoneToSupportIndicatorNote,
                                                    DensAsiNonResponseType<bool?> contributionConstituteMajorityOfYourSupportIndicator,
                                                    string contributionConstituteMajorityOfYourSupportIndicatorNote,
                                                    DensAsiNonResponseType<DensAsiEmploymentPattern> pastThreeYearsDensAsiEmploymentPattern,
                                                    string pastThreeYearsDensAsiEmploymentPatternNote,
                                                    DensAsiNonResponseType<int?> workInLastThirtyDaysPaidDayCount,
                                                    string workInLastThirtyDaysPaidDayCountNote,
                                                    DensAsiNonResponseType<int?> netIncomeAmount,
                                                    string netIncomeAmountNote,
                                                    DensAsiNonResponseType<int?> unemploymentCompensationAmount,
                                                    string unemploymentCompensationAmountNote,
                                                    DensAsiNonResponseType<int?> welfareAmount,
                                                    string welfareAmountNote,
                                                    DensAsiNonResponseType<int?> pensionBenefitsSocialSecurityAmount,
                                                    string pensionBenefitsSocialSecurityAmountNote,
                                                    DensAsiNonResponseType<int?> mateFamilyFriendsAmount,
                                                    string mateFamilyFriendsAmountNote,
                                                    DensAsiNonResponseType<int?> illegalAmount,
                                                    string illegalAmountNote,
                                                    DensAsiNonResponseType<int?> dependentPeopleCount,
                                                    string dependentPeopleCountNote,
                                                    DensAsiNonResponseType<int?> employmentProblemsDayCount,
                                                    string employmentProblemsDayCountNote,
                                                    DensAsiNonResponseType<DensAsiPatientRating> troubledByEmploymentProblemsDensAsiPatientRating,
                                                    string troubledByEmploymentProblemsDensAsiPatientRatingNote,
                                                    DensAsiNonResponseType<DensAsiPatientRating> importanceOfEmploymentProblemCounselingDensAsiPatientRating,
                                                    string importanceOfEmploymentProblemCounselingDensAsiPatientRatingNote,
                                                    DensAsiInterviewerRating patientCounselingDensAsiInterviewerRating,
                                                    string patientCounselingDensAsiInterviewerRatingNote,
                                                    bool? confidenceDistortedByPatientMisrepresentationIndicator,
                                                    string confidenceDistortedByPatientMisrepresentationIndicatorNote,
                                                    bool? confidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                                                    string confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                                                    string sectionNote )
        {
            if ( yearsAndMonthsEducationCompletedTimeSpan.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => YearsAndMonthsEducationCompletedTimeSpan ).Contains ( yearsAndMonthsEducationCompletedTimeSpan.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "YearsAndMonthsEducationCompletedTimeSpan DensAsiNonResponse value '" + yearsAndMonthsEducationCompletedTimeSpan.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( technicalEducationCompletedMonthCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TechnicalEducationCompletedMonthCount ).Contains ( technicalEducationCompletedMonthCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TechnicalEducationCompletedMonthCount DensAsiNonResponse value '" + technicalEducationCompletedMonthCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( professionTradeSkillIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProfessionTradeSkillIndicator ).Contains ( professionTradeSkillIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProfessionTradeSkillIndicator DensAsiNonResponse value '" + professionTradeSkillIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( validDriversLicenseIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ValidDriversLicenseIndicator ).Contains ( validDriversLicenseIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ValidDriversLicenseIndicator DensAsiNonResponse value '" + validDriversLicenseIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( automobileAvailableforUseIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AutomobileAvailableforUseIndicator ).Contains ( automobileAvailableforUseIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AutomobileAvailableforUseIndicator DensAsiNonResponse value '" + automobileAvailableforUseIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( yearsAndMonthsOfLongestFullTimeJobTimeSpan.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => YearsAndMonthsOfLongestFullTimeJobTimeSpan ).Contains ( yearsAndMonthsOfLongestFullTimeJobTimeSpan.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "YearsAndMonthsOfLongestFullTimeJobTimeSpan DensAsiNonResponse value '" + yearsAndMonthsOfLongestFullTimeJobTimeSpan.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( usualOrLastDensAsiOccupationType.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => UsualOrLastDensAsiOccupationType ).Contains ( usualOrLastDensAsiOccupationType.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "UsualOrLastDensAsiOccupationType DensAsiNonResponse value '" + usualOrLastDensAsiOccupationType.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( contributionOfSomeoneToSupportIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ContributionOfSomeoneToSupportIndicator ).Contains ( contributionOfSomeoneToSupportIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ContributionOfSomeoneToSupportIndicator DensAsiNonResponse value '" + contributionOfSomeoneToSupportIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( contributionConstituteMajorityOfYourSupportIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ContributionConstituteMajorityOfYourSupportIndicator ).Contains ( contributionConstituteMajorityOfYourSupportIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ContributionConstituteMajorityOfYourSupportIndicator DensAsiNonResponse value '" + contributionConstituteMajorityOfYourSupportIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( pastThreeYearsDensAsiEmploymentPattern.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PastThreeYearsDensAsiEmploymentPattern ).Contains ( pastThreeYearsDensAsiEmploymentPattern.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PastThreeYearsDensAsiEmploymentPattern DensAsiNonResponse value '" + pastThreeYearsDensAsiEmploymentPattern.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( workInLastThirtyDaysPaidDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => WorkInLastThirtyDaysPaidDayCount ).Contains ( workInLastThirtyDaysPaidDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "WorkInLastThirtyDaysPaidDayCount DensAsiNonResponse value '" + workInLastThirtyDaysPaidDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( netIncomeAmount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => NetIncomeAmount ).Contains ( netIncomeAmount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "NetIncomeAmount DensAsiNonResponse value '" + netIncomeAmount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( unemploymentCompensationAmount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => UnemploymentCompensationAmount ).Contains ( unemploymentCompensationAmount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "UnemploymentCompensationAmount DensAsiNonResponse value '" + unemploymentCompensationAmount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( welfareAmount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => WelfareAmount ).Contains ( welfareAmount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "WelfareAmount DensAsiNonResponse value '" + welfareAmount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( pensionBenefitsSocialSecurityAmount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PensionBenefitsSocialSecurityAmount ).Contains ( pensionBenefitsSocialSecurityAmount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PensionBenefitsSocialSecurityAmount DensAsiNonResponse value '" + pensionBenefitsSocialSecurityAmount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( mateFamilyFriendsAmount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => MateFamilyFriendsAmount ).Contains ( mateFamilyFriendsAmount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "MateFamilyFriendsAmount DensAsiNonResponse value '" + mateFamilyFriendsAmount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( illegalAmount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => IllegalAmount ).Contains ( illegalAmount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "IllegalAmount DensAsiNonResponse value '" + illegalAmount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( dependentPeopleCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DependentPeopleCount ).Contains ( dependentPeopleCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DependentPeopleCount DensAsiNonResponse value '" + dependentPeopleCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( employmentProblemsDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => EmploymentProblemsDayCount ).Contains ( employmentProblemsDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "EmploymentProblemsDayCount DensAsiNonResponse value '" + employmentProblemsDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( troubledByEmploymentProblemsDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TroubledByEmploymentProblemsDensAsiPatientRating ).Contains ( troubledByEmploymentProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TroubledByEmploymentProblemsDensAsiPatientRating DensAsiNonResponse value '" + troubledByEmploymentProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( importanceOfEmploymentProblemCounselingDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ImportanceOfEmploymentProblemCounselingDensAsiPatientRating ).Contains ( importanceOfEmploymentProblemCounselingDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ImportanceOfEmployementProblemCounselingDensAsiPatientRating DensAsiNonResponse value '" + importanceOfEmploymentProblemCounselingDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            _yearsAndMonthsEducationCompletedTimeSpan = yearsAndMonthsEducationCompletedTimeSpan;
            _yearsAndMonthsEducationCompletedTimeSpanNote = yearsAndMonthsEducationCompletedTimeSpanNote;
            _technicalEducationCompletedMonthCount = technicalEducationCompletedMonthCount;
            _technicalEducationCompletedMonthCountNote = technicalEducationCompletedMonthCountNote;
            _professionTradeSkillIndicator = professionTradeSkillIndicator;
            _professionTradeSkillDescription = professionTradeSkillDescription;
            _professionTradeSkillNote = professionTradeSkillNote;
            _validDriversLicenseIndicator = validDriversLicenseIndicator;
            _validDriversLicenseIndicatorNote = validDriversLicenseIndicatorNote;
            _automobileAvailableforUseIndicator = automobileAvailableforUseIndicator;
            _automobileAvailableforUseIndicatorNote = automobileAvailableforUseIndicatorNote;
            _yearsAndMonthsOfLongestFullTimeJobTimeSpan = yearsAndMonthsOfLongestFullTimeJobTimeSpan;
            _yearsAndMonthsOfLongestFullTimeJobTimeSpanNote = yearsAndMonthsOfLongestFullTimeJobTimeSpanNote;
            _usualOrLastDensAsiOccupationType = usualOrLastDensAsiOccupationType;
            _usualOrLastOccupationDescription = usualOrLastOccupationDescription;
            _usualOrLastOccupationNote = usualOrLastOccupationNote;
            _contributionOfSomeoneToSupportIndicator = contributionOfSomeoneToSupportIndicator;
            _contributionOfSomeoneToSupportIndicatorNote = contributionOfSomeoneToSupportIndicatorNote;
            _contributionConstituteMajorityOfYourSupportIndicator = contributionConstituteMajorityOfYourSupportIndicator;
            _contributionConstituteMajorityOfYourSupportIndicatorNote = contributionConstituteMajorityOfYourSupportIndicatorNote;
            _pastThreeYearsDensAsiEmploymentPattern = pastThreeYearsDensAsiEmploymentPattern;
            _pastThreeYearsDensAsiEmploymentPatternNote = pastThreeYearsDensAsiEmploymentPatternNote;
            _workInLastThirtyDaysPaidDayCount = workInLastThirtyDaysPaidDayCount;
            _workInLastThirtyDaysPaidDayCountNote = workInLastThirtyDaysPaidDayCountNote;
            _netIncomeAmount = netIncomeAmount;
            _netIncomeAmountNote = netIncomeAmountNote;
            _unemploymentCompensationAmount = unemploymentCompensationAmount;
            _unemploymentCompensationAmountNote = unemploymentCompensationAmountNote;
            _welfareAmount = welfareAmount;
            _welfareAmountNote = welfareAmountNote;
            _pensionBenefitsSocialSecurityAmount = pensionBenefitsSocialSecurityAmount;
            _pensionBenefitsSocialSecurityAmountNote = pensionBenefitsSocialSecurityAmountNote;
            _mateFamilyFriendsAmount = mateFamilyFriendsAmount;
            _mateFamilyFriendsAmountNote = mateFamilyFriendsAmountNote;
            _illegalAmount = illegalAmount;
            _illegalAmountNote = illegalAmountNote;
            _dependentPeopleCount = dependentPeopleCount;
            _dependentPeopleCountNote = dependentPeopleCountNote;
            _employmentProblemsDayCount = employmentProblemsDayCount;
            _employmentProblemsDayCountNote = employmentProblemsDayCountNote;
            _troubledByEmploymentProblemsDensAsiPatientRating = troubledByEmploymentProblemsDensAsiPatientRating;
            _troubledByEmploymentProblemsDensAsiPatientRatingNote = troubledByEmploymentProblemsDensAsiPatientRatingNote;
            this._importanceOfEmploymentProblemCounselingDensAsiPatientRating = importanceOfEmploymentProblemCounselingDensAsiPatientRating;
            this._importanceOfEmploymentProblemCounselingDensAsiPatientRatingNote = importanceOfEmploymentProblemCounselingDensAsiPatientRatingNote;
            _patientCounselingDensAsiInterviewerRating = patientCounselingDensAsiInterviewerRating;
            _patientCounselingDensAsiInterviewerRatingNote = patientCounselingDensAsiInterviewerRatingNote;
            _confidenceDistortedByPatientMisrepresentationIndicator = confidenceDistortedByPatientMisrepresentationIndicator;
            _confidenceDistortedByPatientMisrepresentationIndicatorNote = confidenceDistortedByPatientMisrepresentationIndicatorNote;
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicator = confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote = confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
            _sectionNote = sectionNote;
        }

        /// <summary>
        /// Gets the years and months of education completed.
        /// Question Number: E1
        /// </summary>
        public virtual DensAsiNonResponseType<TimeSpan?> YearsAndMonthsEducationCompletedTimeSpan
        {
            get { return _yearsAndMonthsEducationCompletedTimeSpan; }
            private set { }
        }

        /// <summary>
        /// Gets the years and months of education completed.
        /// Question Number: E1
        /// </summary>
        public virtual string YearsAndMonthsEducationCompletedTimeSpanNote
        {
            get { return _yearsAndMonthsEducationCompletedTimeSpanNote; }
            private set { }
        }

        /// <summary>
        /// Gets the months of technical education completed.
        /// Question Number: E2
        /// </summary>
        public virtual DensAsiNonResponseType<int?> TechnicalEducationCompletedMonthCount
        {
            get { return _technicalEducationCompletedMonthCount; }
            private set { }
        }

        /// <summary>
        /// Gets the months of technical education completed note.
        /// Question Number: E2
        /// </summary>
        public virtual string TechnicalEducationCompletedMonthCountNote
        {
            get { return _technicalEducationCompletedMonthCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient has a profession, trade or skill.
        /// Question Number: E3
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProfessionTradeSkillIndicator
        {
            get { return _professionTradeSkillIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient has a profession, trade or skill description.
        /// Question Number: E3
        /// </summary>
        public virtual string ProfessionTradeSkillDescription
        {
            get { return _professionTradeSkillDescription; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient has a profession, trade or skill note.
        /// Question Number: E3
        /// </summary>
        public virtual string ProfessionTradeSkillNote
        {
            get { return _professionTradeSkillNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient holds a valid drivers license.
        /// Question Number: E4
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ValidDriversLicenseIndicator
        {
            get { return _validDriversLicenseIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient holds a valid drivers license note.
        /// Question Number: E4
        /// </summary>
        public virtual string ValidDriversLicenseIndicatorNote
        {
            get { return _validDriversLicenseIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient has an automobile that is available for use.
        /// Question Number: E5
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AutomobileAvailableforUseIndicator
        {
            get { return _automobileAvailableforUseIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient has an automobile that is available for use note.
        /// Question Number: E5
        /// </summary>
        public virtual string AutomobileAvailableforUseIndicatorNote
        {
            get { return _automobileAvailableforUseIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the years and months of the patient's longest full time job.
        /// Question Number: E6
        /// </summary>
        public virtual DensAsiNonResponseType<TimeSpan?> YearsAndMonthsOfLongestFullTimeJobTimeSpan
        {
            get { return _yearsAndMonthsOfLongestFullTimeJobTimeSpan; }
            private set { }
        }

        /// <summary>
        /// Gets the years and months of the patient's longest full time job note.
        /// Question Number: E6
        /// </summary>
        public virtual string YearsAndMonthsOfLongestFullTimeJobTimeSpanNote
        {
            get { return _yearsAndMonthsOfLongestFullTimeJobTimeSpanNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiOccupationType">UsualOrLastDensAsiOccupationType</see>
        /// patient occupation type. Question Number: E7
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiOccupationType> UsualOrLastDensAsiOccupationType
        {
            get { return _usualOrLastDensAsiOccupationType; }
            private set { }
        }

        /// <summary>
        /// Gets the patient occupation type description.
        /// Question Number: E7
        /// </summary>
        public virtual string UsualOrLastOccupationDescription
        {
            get { return _usualOrLastOccupationDescription; }
            private set { }
        }

        /// <summary>
        /// Gets the patient occupation type note.
        /// Question Number: E7
        /// </summary>
        public virtual string UsualOrLastOccupationNote
        {
            get { return _usualOrLastOccupationNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether someone contributes to the patient's support.
        /// Question Number: E8
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ContributionOfSomeoneToSupportIndicator
        {
            get { return _contributionOfSomeoneToSupportIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the someone contributes to the patient's support note.
        /// Question Number: E8
        /// </summary>
        public virtual string ContributionOfSomeoneToSupportIndicatorNote
        {
            get { return _contributionOfSomeoneToSupportIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating that a contribution constitutes a majority of the patient's support.
        /// Question Number: E9
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ContributionConstituteMajorityOfYourSupportIndicator
        {
            get { return _contributionConstituteMajorityOfYourSupportIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the contribution constitute majority of your support indicator note.
        /// Question Number: E9
        /// </summary>
        public virtual string ContributionConstituteMajorityOfYourSupportIndicatorNote
        {
            get { return _contributionConstituteMajorityOfYourSupportIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentPattern">PastThreeYearsDensAsiEmploymentPattern</see>
        /// patient's usual employment pattern for the past three years. Question Number: E10
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiEmploymentPattern> PastThreeYearsDensAsiEmploymentPattern
        {
            get { return _pastThreeYearsDensAsiEmploymentPattern; }
            private set { }
        }

        /// <summary>
        /// Gets the patient's usual employment pattern for the past three years note.
        /// Question Number: E10
        /// </summary>
        public virtual string PastThreeYearsDensAsiEmploymentPatternNote
        {
            get { return _pastThreeYearsDensAsiEmploymentPatternNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in in past 30 that the patient was paid for work.
        /// Question Number: E11
        /// </summary>
        public virtual DensAsiNonResponseType<int?> WorkInLastThirtyDaysPaidDayCount
        {
            get { return _workInLastThirtyDaysPaidDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in in past 30 that the patient was paid for work note.
        /// Question Number: E11
        /// </summary>
        public virtual string WorkInLastThirtyDaysPaidDayCountNote
        {
            get { return _workInLastThirtyDaysPaidDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the patient net income amount.
        /// Question Number: E12
        /// </summary>
        public virtual DensAsiNonResponseType<int?> NetIncomeAmount
        {
            get { return _netIncomeAmount; }
            private set { }
        }

        /// <summary>
        /// Gets the patient net income amount note.
        /// Question Number: E12
        /// </summary>
        public virtual string NetIncomeAmountNote
        {
            get { return _netIncomeAmountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the patient unemployment compensation amount.
        /// Question Number: E13
        /// </summary>
        public virtual DensAsiNonResponseType<int?> UnemploymentCompensationAmount
        {
            get { return _unemploymentCompensationAmount; }
            private set { }
        }

        /// <summary>
        /// Gets the patient unemployment compensation amount note.
        /// Question Number: E13
        /// </summary>
        public virtual string UnemploymentCompensationAmountNote
        {
            get { return _unemploymentCompensationAmountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the patient welfare amount.
        /// Question Number: E14
        /// </summary>
        public virtual DensAsiNonResponseType<int?> WelfareAmount
        {
            get { return _welfareAmount; }
            private set { }
        }

        /// <summary>
        /// Gets the patient welfare amount note.
        /// Question Number: E14
        /// </summary>
        public virtual string WelfareAmountNote
        {
            get { return _welfareAmountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the patient pension benefits social security amount.
        /// Question Number: E15
        /// </summary>
        public virtual DensAsiNonResponseType<int?> PensionBenefitsSocialSecurityAmount
        {
            get { return _pensionBenefitsSocialSecurityAmount; }
            private set { }
        }

        /// <summary>
        /// Gets the patient pension benefits social security amount note.
        /// Question Number: E15
        /// </summary>
        public virtual string PensionBenefitsSocialSecurityAmountNote
        {
            get { return _pensionBenefitsSocialSecurityAmountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the cash amount provided from mate, family or friends for personal expenses.
        /// Question Number: E16
        /// </summary>
        public virtual DensAsiNonResponseType<int?> MateFamilyFriendsAmount
        {
            get { return _mateFamilyFriendsAmount; }
            private set { }
        }

        /// <summary>
        /// Gets the cash amount provided from mate, family or friends for personal expenses note.
        /// Question Number: E16
        /// </summary>
        public virtual string MateFamilyFriendsAmountNote
        {
            get { return _mateFamilyFriendsAmountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the cash amount illegally obtained from drug dealing, stealing, fencing stolen goods, illegal gambling, prostitution etc.
        /// Question Number: E17
        /// </summary>
        public virtual DensAsiNonResponseType<int?> IllegalAmount
        {
            get { return _illegalAmount; }
            private set { }
        }

        /// <summary>
        /// Gets the cash amount illegally obtained from drug dealing, stealing, fencing stolen goods, illegal gambling, prostitution note.
        /// Question Number: E17
        /// </summary>
        public virtual string IllegalAmountNote
        {
            get { return _illegalAmountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of of individuals regularly depending on patient financially.
        /// Question Number: E18
        /// </summary>
        public virtual DensAsiNonResponseType<int?> DependentPeopleCount
        {
            get { return _dependentPeopleCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of of individuals regularly depending on patient financially note.
        /// Question Number: E18
        /// </summary>
        public virtual string DependentPeopleCountNote
        {
            get { return _dependentPeopleCountNote; }
            private set { }
        }


        /// <summary>
        /// Gets the number of days in the last thirty that the patient has experienced employment problems.
        /// Question Number: E19
        /// </summary>
        public virtual DensAsiNonResponseType<int?> EmploymentProblemsDayCount
        {
            get { return _employmentProblemsDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in the last thirty that the patient has experienced employment problems note.
        /// Question Number: E19
        /// </summary>
        public virtual string EmploymentProblemsDayCountNote
        {
            get { return _employmentProblemsDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">TroubledByEmploymentProblemsDensAsiPatientRating</see>
        /// for how bothered the patient has been by employment problems in the past thirty days. Question Number: E20
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> TroubledByEmploymentProblemsDensAsiPatientRating
        {
            get { return _troubledByEmploymentProblemsDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the how bothered the patient has been by employment problems in the past thirty days note.
        /// Question Number: E20
        /// </summary>
        public virtual string TroubledByEmploymentProblemsDensAsiPatientRatingNote
        {
            get { return _troubledByEmploymentProblemsDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">ImportanceOfEmploymentProblemCounselingDensAsiPatientRating</see>
        /// importance of patient employment problem counseling. Question Number: E21
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> ImportanceOfEmploymentProblemCounselingDensAsiPatientRating
        {
            get { return this._importanceOfEmploymentProblemCounselingDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Get the importance of patient employment problem counseling note.
        /// Question Number: E21
        /// </summary>
        public virtual string ImportanceOfEmploymentProblemCounselingDensAsiPatientRatingNote
        {
            get { return this._importanceOfEmploymentProblemCounselingDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">PatientCounselingDensAsiInterviewerRating</see> interviewer 
        /// rating denoting that the patient requires employment counseling.
        /// Question Number: E22
        /// </summary>
        public virtual DensAsiInterviewerRating PatientCounselingDensAsiInterviewerRating
        {
            get { return _patientCounselingDensAsiInterviewerRating; }
            private set { }
        }

        /// <summary>
        /// Gets the interviewer rating denoting that the patient requires employment counseling note.
        /// Question Number: E22
        /// </summary>
        public virtual string PatientCounselingDensAsiInterviewerRatingNote
        {
            get { return _patientCounselingDensAsiInterviewerRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that medical information was distorted by patient misrepresentation.
        /// Question Number: E23
        /// </summary>
        public virtual bool? ConfidenceDistortedByPatientMisrepresentationIndicator
        {
            get { return _confidenceDistortedByPatientMisrepresentationIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that medical information was distorted by patient misrepresentation note.
        /// Question Number: E23
        /// </summary>
        public virtual string ConfidenceDistortedByPatientMisrepresentationIndicatorNote
        {
            get { return _confidenceDistortedByPatientMisrepresentationIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that medical information was distorted by patient inability to understand.
        /// Question Number: E24
        /// </summary>
        public virtual bool? ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator
        {
            get { return _confidenceRateDistortedByPatientInabilityToUnderstandIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that medical information was distorted by patient inability to understand note.
        /// Question Number: E24
        /// </summary>
        public virtual string ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote
        {
            get { return _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the medical status section note.
        /// </summary>
        public virtual string SectionNote
        {
            get { return _sectionNote; }
            private set { }
        }
    }
}