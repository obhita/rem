using System;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// DensAsiEmploymentStatusSectionBuilder provides a fluent interface for creating a EmploymentStatus section.
    /// </summary>
    public class DensAsiEmploymentStatusSectionBuilder
    {
        private DensAsiNonResponseType<TimeSpan?> _yearsAndMonthsEducationCompletedTimeSpan;
        private string _yearsAndMonthsEducationCompletedTimeSpanNote;
        private DensAsiNonResponseType<int?> _technicalEducationCompletedMonthCount;
        private string _technicalEducationCompletedMonthCountNote;
        private DensAsiNonResponseType<bool?> _professionTradeSkillIndicator;
        private string _professionTradeSkillDescription;
        private string _professionTradeSkillNote;
        private DensAsiNonResponseType<bool?> _validDriversLicenseIndicator;
        private string _validDriversLicenseIndicatorNote;
        private DensAsiNonResponseType<bool?> _automobileAvailableforUseIndicator;
        private string _automobileAvailableforUseIndicatorNote;
        private DensAsiNonResponseType<TimeSpan?> _yearsAndMonthsOfLongestFullTimeJobTimeSpan;
        private string _yearsAndMonthsOfLongestFullTimeJobTimeSpanNote;
        private DensAsiNonResponseType<DensAsiOccupationType> _usualOrLastDensAsiOccupationType;
        private string _usualOrLastOccupationDescription;
        private string _usualOrLastOccupationNote;
        private DensAsiNonResponseType<bool?> _contributionOfSomeoneToSupportIndicator;
        private string _contributionOfSomeoneToSupportIndicatorNote;
        private DensAsiNonResponseType<bool?> _contributionConstituteMajorityOfYourSupportIndicator;
        private string _contributionConstituteMajorityOfYourSupportIndicatorNote;
        private DensAsiNonResponseType<DensAsiEmploymentPattern> _pastThreeYearsDensAsiEmploymentPattern;
        private string _pastThreeYearsDensAsiEmploymentPatternNote;
        private DensAsiNonResponseType<int?> _workInLastThirtyDaysPaidDayCount;
        private string _workInLastThirtyDaysPaidDayCountNote;
        private DensAsiNonResponseType<int?> _netIncomeAmount;
        private string _netIncomeAmountNote;
        private DensAsiNonResponseType<int?> _unemploymentCompensationAmount;
        private string _unemploymentCompensationAmountNote;
        private DensAsiNonResponseType<int?> _welfareAmount;
        private string _welfareAmountNote;
        private DensAsiNonResponseType<int?> _pensionBenefitsSocialSecurityAmount;
        private string _pensionBenefitsSocialSecurityAmountNote;
        private DensAsiNonResponseType<int?> _mateFamilyFriendsAmount;
        private string _mateFamilyFriendsAmountNote;
        private DensAsiNonResponseType<int?> _illegalAmount;
        private string _illegalAmountNote;
        private DensAsiNonResponseType<int?> _dependentPeopleCount;
        private string _dependentPeopleCountNote;
        private DensAsiNonResponseType<int?> _employmentProblemsDayCount;
        private string _employmentProblemsDayCountNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _troubledByEmploymentProblemsDensAsiPatientRating;
        private string _troubledByEmploymentProblemsDensAsiPatientRatingNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _importanceOfEmployementProblemCounselingDensAsiPatientRating;
        private string _importanceOfEmployementProblemCounselingDensAsiPatientRatingNote;
        private DensAsiInterviewerRating _patientCounselingDensAsiInterviewerRating;
        private string _patientCounselingDensAsiInterviewerRatingNote;
        private bool? _confidenceDistortedByPatientMisrepresentationIndicator;
        private string _confidenceDistortedByPatientMisrepresentationIndicatorNote;
        private bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private string _sectionNote;


        /// <summary>
        /// Assigns the years and months education completed time span.
        /// </summary>
        /// <param name="yearsAndMonthsEducationCompletedTimeSpan">The years and months education completed time span.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithYearsAndMonthsEducationCompletedTimeSpan(DensAsiNonResponseType<TimeSpan?> yearsAndMonthsEducationCompletedTimeSpan)
        {
            _yearsAndMonthsEducationCompletedTimeSpan = yearsAndMonthsEducationCompletedTimeSpan;
            return this;
        }

        /// <summary>
        /// Assigns the years and months education completed time span note.
        /// </summary>
        /// <param name="yearsAndMonthsEducationCompletedTimeSpanNote">The years and months education completed time span note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithYearsAndMonthsEducationCompletedTimeSpanNote(string yearsAndMonthsEducationCompletedTimeSpanNote)
        {
            _yearsAndMonthsEducationCompletedTimeSpanNote = yearsAndMonthsEducationCompletedTimeSpanNote;
            return this;
        }

        /// <summary>
        /// Assigns the technical education completed month count.
        /// </summary>
        /// <param name="technicalEducationCompletedMonthCount">The technical education completed month count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithTechnicalEducationCompletedMonthCount(DensAsiNonResponseType<int?> technicalEducationCompletedMonthCount)
        {
            _technicalEducationCompletedMonthCount = technicalEducationCompletedMonthCount;
            return this;
        }

        /// <summary>
        /// Assigns the technical education completed month count note.
        /// </summary>
        /// <param name="technicalEducationCompletedMonthCountNote">The technical education completed month count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithTechnicalEducationCompletedMonthCountNote(string technicalEducationCompletedMonthCountNote)
        {
            _technicalEducationCompletedMonthCountNote = technicalEducationCompletedMonthCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the profession trade skill indicator.
        /// </summary>
        /// <param name="professionTradeSkillIndicator">The profession trade skill indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithProfessionTradeSkillIndicator(DensAsiNonResponseType<bool?> professionTradeSkillIndicator)
        {
            _professionTradeSkillIndicator = professionTradeSkillIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the profession trade skill description.
        /// </summary>
        /// <param name="professionTradeSkillDescription">The profession trade skill description.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithProfessionTradeSkillDescription(string professionTradeSkillDescription)
        {
            _professionTradeSkillDescription = professionTradeSkillDescription;
            return this;
        }

        /// <summary>
        /// Assigns the profession trade skill note.
        /// </summary>
        /// <param name="professionTradeSkillNote">The profession trade skill note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithProfessionTradeSkillNote(string professionTradeSkillNote)
        {
            _professionTradeSkillNote = professionTradeSkillNote;
            return this;
        }

        /// <summary>
        /// Assigns the valid drivers license indicator.
        /// </summary>
        /// <param name="validDriversLicenseIndicator">The valid drivers license indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithValidDriversLicenseIndicator(DensAsiNonResponseType<bool?> validDriversLicenseIndicator)
        {
            _validDriversLicenseIndicator = validDriversLicenseIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the valid drivers license indicator note.
        /// </summary>
        /// <param name="validDriversLicenseIndicatorNote">The valid drivers license indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithValidDriversLicenseIndicatorNote(string validDriversLicenseIndicatorNote)
        {
            _validDriversLicenseIndicatorNote = validDriversLicenseIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the automobile availablefor use indicator.
        /// </summary>
        /// <param name="automobileAvailableforUseIndicator">The automobile availablefor use indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithAutomobileAvailableforUseIndicator(DensAsiNonResponseType<bool?> automobileAvailableforUseIndicator)
        {
            _automobileAvailableforUseIndicator = automobileAvailableforUseIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the automobile availablefor use indicator note.
        /// </summary>
        /// <param name="automobileAvailableforUseIndicatorNote">The automobile availablefor use indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithAutomobileAvailableforUseIndicatorNote(string automobileAvailableforUseIndicatorNote)
        {
            _automobileAvailableforUseIndicatorNote = automobileAvailableforUseIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the years and months of longest full time job time span.
        /// </summary>
        /// <param name="yearsAndMonthsOfLongestFullTimeJobTimeSpan">The years and months of longest full time job time span.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithYearsAndMonthsOfLongestFullTimeJobTimeSpan(DensAsiNonResponseType<TimeSpan?> yearsAndMonthsOfLongestFullTimeJobTimeSpan)
        {
            _yearsAndMonthsOfLongestFullTimeJobTimeSpan = yearsAndMonthsOfLongestFullTimeJobTimeSpan;
            return this;
        }

        /// <summary>
        /// Assigns the years and months of longest full time job time span note.
        /// </summary>
        /// <param name="yearsAndMonthsOfLongestFullTimeJobTimeSpanNote">The years and months of longest full time job time span note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithYearsAndMonthsOfLongestFullTimeJobTimeSpanNote(string yearsAndMonthsOfLongestFullTimeJobTimeSpanNote)
        {
            _yearsAndMonthsOfLongestFullTimeJobTimeSpanNote = yearsAndMonthsOfLongestFullTimeJobTimeSpanNote;
            return this;
        }

        /// <summary>
        /// Assigns the type of the usual or last DensAsi occupation.
        /// </summary>
        /// <param name="usualOrLastDensAsiOccupationType">Type of the usual or last DensAsi occupation.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithUsualOrLastDensAsiOccupationType(DensAsiNonResponseType<DensAsiOccupationType> usualOrLastDensAsiOccupationType)
        {
            _usualOrLastDensAsiOccupationType = usualOrLastDensAsiOccupationType;
            return this;
        }

        /// <summary>
        /// Assigns the usual or last occupation description.
        /// </summary>
        /// <param name="usualOrLastOccupationDescription">The usual or last occupation description.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithUsualOrLastOccupationDescription(string usualOrLastOccupationDescription)
        {
            _usualOrLastOccupationDescription = usualOrLastOccupationDescription;
            return this;
        }

        /// <summary>
        /// Assigns the usual or last occupation note.
        /// </summary>
        /// <param name="usualOrLastOccupationNote">The usual or last occupation note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithUsualOrLastOccupationNote(string usualOrLastOccupationNote)
        {
            _usualOrLastOccupationNote = usualOrLastOccupationNote;
            return this;
        }

        /// <summary>
        /// Assigns the contribution of someone to support indicator.
        /// </summary>
        /// <param name="contributionOfSomeoneToSupportIndicator">The contribution of someone to support indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithContributionOfSomeoneToSupportIndicator(DensAsiNonResponseType<bool?> contributionOfSomeoneToSupportIndicator)
        {
            _contributionOfSomeoneToSupportIndicator = contributionOfSomeoneToSupportIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the contribution of someone to support indicator note.
        /// </summary>
        /// <param name="contributionOfSomeoneToSupportIndicatorNote">The contribution of someone to support indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithContributionOfSomeoneToSupportIndicatorNote(string contributionOfSomeoneToSupportIndicatorNote)
        {
            _contributionOfSomeoneToSupportIndicatorNote = contributionOfSomeoneToSupportIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the contribution constitute majority of your support indicator.
        /// </summary>
        /// <param name="contributionConstituteMajorityOfYourSupportIndicator">The contribution constitute majority of your support indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithContributionConstituteMajorityOfYourSupportIndicator(DensAsiNonResponseType<bool?> contributionConstituteMajorityOfYourSupportIndicator)
        {
            _contributionConstituteMajorityOfYourSupportIndicator = contributionConstituteMajorityOfYourSupportIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the contribution constitute majority of your support indicator note.
        /// </summary>
        /// <param name="contributionConstituteMajorityOfYourSupportIndicatorNote">The contribution constitute majority of your support indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithContributionConstituteMajorityOfYourSupportIndicatorNote(string contributionConstituteMajorityOfYourSupportIndicatorNote)
        {
            _contributionConstituteMajorityOfYourSupportIndicatorNote = contributionConstituteMajorityOfYourSupportIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the past three years DensAsi employment pattern.
        /// </summary>
        /// <param name="pastThreeYearsDensAsiEmploymentPattern">The past three years DensAsi employment pattern.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithPastThreeYearsDensAsiEmploymentPattern(DensAsiNonResponseType<DensAsiEmploymentPattern> pastThreeYearsDensAsiEmploymentPattern)
        {
            _pastThreeYearsDensAsiEmploymentPattern = pastThreeYearsDensAsiEmploymentPattern;
            return this;
        }

        /// <summary>
        /// Assigns the past three years DensAsi employment pattern note.
        /// </summary>
        /// <param name="pastThreeYearsDensAsiEmploymentPatternNote">The past three years DensAsi employment pattern note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithPastThreeYearsDensAsiEmploymentPatternNote(string pastThreeYearsDensAsiEmploymentPatternNote)
        {
            _pastThreeYearsDensAsiEmploymentPatternNote = pastThreeYearsDensAsiEmploymentPatternNote;
            return this;
        }

        /// <summary>
        /// Assigns the work in last thirty days paid day count.
        /// </summary>
        /// <param name="workInLastThirtyDaysPaidDayCount">The work in last thirty days paid day count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithWorkInLastThirtyDaysPaidDayCount(DensAsiNonResponseType<int?> workInLastThirtyDaysPaidDayCount)
        {
            _workInLastThirtyDaysPaidDayCount = workInLastThirtyDaysPaidDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the work in last thirty days paid day count note.
        /// </summary>
        /// <param name="workInLastThirtyDaysPaidDayCountNote">The work in last thirty days paid day count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithWorkInLastThirtyDaysPaidDayCountNote(string workInLastThirtyDaysPaidDayCountNote)
        {
            _workInLastThirtyDaysPaidDayCountNote = workInLastThirtyDaysPaidDayCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the net income amount.
        /// </summary>
        /// <param name="netIncomeAmount">The net income amount.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithNetIncomeAmount(DensAsiNonResponseType<int?> netIncomeAmount)
        {
            _netIncomeAmount = netIncomeAmount;
            return this;
        }

        /// <summary>
        /// Assigns the net income amount note.
        /// </summary>
        /// <param name="netIncomeAmountNote">The net income amount note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithNetIncomeAmountNote(string netIncomeAmountNote)
        {
            _netIncomeAmountNote = netIncomeAmountNote;
            return this;
        }

        /// <summary>
        /// Assigns the unemployment compensation amount.
        /// </summary>
        /// <param name="unemploymentCompensationAmount">The unemployment compensation amount.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithUnemploymentCompensationAmount(DensAsiNonResponseType<int?> unemploymentCompensationAmount)
        {
            _unemploymentCompensationAmount = unemploymentCompensationAmount;
            return this;
        }

        /// <summary>
        /// Assigns the unemployment compensation amount note.
        /// </summary>
        /// <param name="unemploymentCompensationAmountNote">The unemployment compensation amount note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithUnemploymentCompensationAmountNote(string unemploymentCompensationAmountNote)
        {
            _unemploymentCompensationAmountNote = unemploymentCompensationAmountNote;
            return this;
        }

        /// <summary>
        /// Assigns the welfare amount.
        /// </summary>
        /// <param name="welfareAmount">The welfare amount.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithWelfareAmount(DensAsiNonResponseType<int?> welfareAmount)
        {
            _welfareAmount = welfareAmount;
            return this;
        }

        /// <summary>
        /// Assigns the welfare amount note.
        /// </summary>
        /// <param name="welfareAmountNote">The welfare amount note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithWelfareAmountNote(string welfareAmountNote)
        {
            _welfareAmountNote = welfareAmountNote;
            return this;
        }

        /// <summary>
        /// Assigns the pension benefits social security amount.
        /// </summary>
        /// <param name="pensionBenefitsSocialSecurityAmount">The pension benefits social security amount.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithPensionBenefitsSocialSecurityAmount(DensAsiNonResponseType<int?> pensionBenefitsSocialSecurityAmount)
        {
            _pensionBenefitsSocialSecurityAmount = pensionBenefitsSocialSecurityAmount;
            return this;
        }

        /// <summary>
        /// Assigns the pension benefits social security amount note.
        /// </summary>
        /// <param name="pensionBenefitsSocialSecurityAmountNote">The pension benefits social security amount note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithPensionBenefitsSocialSecurityAmountNote(string pensionBenefitsSocialSecurityAmountNote)
        {
            _pensionBenefitsSocialSecurityAmountNote = pensionBenefitsSocialSecurityAmountNote;
            return this;
        }

        /// <summary>
        /// Assigns the mate family friends amount.
        /// </summary>
        /// <param name="mateFamilyFriendsAmount">The mate family friends amount.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithMateFamilyFriendsAmount(DensAsiNonResponseType<int?> mateFamilyFriendsAmount)
        {
            _mateFamilyFriendsAmount = mateFamilyFriendsAmount;
            return this;
        }

        /// <summary>
        /// Assigns the mate family friends amount note.
        /// </summary>
        /// <param name="mateFamilyFriendsAmountNote">The mate family friends amount note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithMateFamilyFriendsAmountNote(string mateFamilyFriendsAmountNote)
        {
            _mateFamilyFriendsAmountNote = mateFamilyFriendsAmountNote;
            return this;
        }

        /// <summary>
        /// Assigns the illegal amount.
        /// </summary>
        /// <param name="illegalAmount">The illegal amount.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithIllegalAmount(DensAsiNonResponseType<int?> illegalAmount)
        {
            _illegalAmount = illegalAmount;
            return this;
        }

        /// <summary>
        /// Assigns the illegal amount note.
        /// </summary>
        /// <param name="illegalAmountNote">The illegal amount note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithIllegalAmountNote(string illegalAmountNote)
        {
            _illegalAmountNote = illegalAmountNote;
            return this;
        }

        /// <summary>
        /// Assigns the dependent people count.
        /// </summary>
        /// <param name="dependentPeopleCount">The dependent people count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithDependentPeopleCount(DensAsiNonResponseType<int?> dependentPeopleCount)
        {
            _dependentPeopleCount = dependentPeopleCount;
            return this;
        }

        /// <summary>
        /// Assigns the dependent people count note.
        /// </summary>
        /// <param name="dependentPeopleCountNote">The dependent people count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithDependentPeopleCountNote(string dependentPeopleCountNote)
        {
            _dependentPeopleCountNote = dependentPeopleCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the employment problems day count.
        /// </summary>
        /// <param name="employmentProblemsDayCount">The employment problems day count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithEmploymentProblemsDayCount(DensAsiNonResponseType<int?> employmentProblemsDayCount)
        {
            _employmentProblemsDayCount = employmentProblemsDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the employment problems day count note.
        /// </summary>
        /// <param name="employmentProblemsDayCountNote">The employment problems day count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithEmploymentProblemsDayCountNote(string employmentProblemsDayCountNote)
        {
            _employmentProblemsDayCountNote = employmentProblemsDayCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the troubled by employment problems DensAsi patient rating.
        /// </summary>
        /// <param name="troubledByEmploymentProblemsDensAsiPatientRating">The troubled by employment problems DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithTroubledByEmploymentProblemsDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> troubledByEmploymentProblemsDensAsiPatientRating)
        {
            _troubledByEmploymentProblemsDensAsiPatientRating = troubledByEmploymentProblemsDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the troubled by employment problems DensAsi patient rating note.
        /// </summary>
        /// <param name="troubledByEmploymentProblemsDensAsiPatientRatingNote">The troubled by employment problems DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithTroubledByEmploymentProblemsDensAsiPatientRatingNote(string troubledByEmploymentProblemsDensAsiPatientRatingNote)
        {
            _troubledByEmploymentProblemsDensAsiPatientRatingNote = troubledByEmploymentProblemsDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the importance of employement problem counseling DensAsi patient rating.
        /// </summary>
        /// <param name="importanceOfEmployementProblemCounselingDensAsiPatientRating">The importance of employement problem counseling DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithImportanceOfEmployementProblemCounselingDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> importanceOfEmployementProblemCounselingDensAsiPatientRating)
        {
            _importanceOfEmployementProblemCounselingDensAsiPatientRating = importanceOfEmployementProblemCounselingDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the importance of employement problem counseling DensAsi patient rating note.
        /// </summary>
        /// <param name="importanceOfEmployementProblemCounselingDensAsiPatientRatingNote">The importance of employement problem counseling DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithImportanceOfEmployementProblemCounselingDensAsiPatientRatingNote(string importanceOfEmployementProblemCounselingDensAsiPatientRatingNote)
        {
            _importanceOfEmployementProblemCounselingDensAsiPatientRatingNote = importanceOfEmployementProblemCounselingDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the patient counseling DensAsi interviewer rating.
        /// </summary>
        /// <param name="patientCounselingDensAsiInterviewerRating">The patient counseling DensAsi interviewer rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithPatientCounselingDensAsiInterviewerRating(DensAsiInterviewerRating patientCounselingDensAsiInterviewerRating)
        {
            _patientCounselingDensAsiInterviewerRating = patientCounselingDensAsiInterviewerRating;
            return this;
        }

        /// <summary>
        /// Assigns the patient counseling DensAsi interviewer rating note.
        /// </summary>
        /// <param name="patientCounselingDensAsiInterviewerRatingNote">The patient counseling DensAsi interviewer rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithPatientCounselingDensAsiInterviewerRatingNote(string patientCounselingDensAsiInterviewerRatingNote)
        {
            _patientCounselingDensAsiInterviewerRatingNote = patientCounselingDensAsiInterviewerRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the confidence distorted by patient misrepresentation indicator.
        /// </summary>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicator">The confidence distorted by patient misrepresentation indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithConfidenceDistortedByPatientMisrepresentationIndicator(bool? confidenceDistortedByPatientMisrepresentationIndicator)
        {
            _confidenceDistortedByPatientMisrepresentationIndicator = confidenceDistortedByPatientMisrepresentationIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the confidence distorted by patient misrepresentation indicator note.
        /// </summary>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicatorNote">The confidence distorted by patient misrepresentation indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithConfidenceDistortedByPatientMisrepresentationIndicatorNote(string confidenceDistortedByPatientMisrepresentationIndicatorNote)
        {
            _confidenceDistortedByPatientMisrepresentationIndicatorNote = confidenceDistortedByPatientMisrepresentationIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the confidence rate distorted by patient inability to understand indicator.
        /// </summary>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicator">The confidence rate distorted by patient inability to understand indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicator(bool? confidenceRateDistortedByPatientInabilityToUnderstandIndicator)
        {
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicator = confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the confidence rate distorted by patient inability to understand indicator note.
        /// </summary>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote">The confidence rate distorted by patient inability to understand indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote(string confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote)
        {
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote = confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the section note.
        /// </summary>
        /// <param name="sectionNote">The section note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSectionBuilder WithSectionNote(string sectionNote)
        {
            _sectionNote = sectionNote;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiEmploymentStatusSectionBuilder">A DensAsiEmploymentStatusSectionBuilder.</see></returns>
        public DensAsiEmploymentStatusSection Build()
        {
            return new DensAsiEmploymentStatusSection(
                _yearsAndMonthsEducationCompletedTimeSpan,
                _yearsAndMonthsEducationCompletedTimeSpanNote,
                _technicalEducationCompletedMonthCount,
                _technicalEducationCompletedMonthCountNote,
                _professionTradeSkillIndicator,
                _professionTradeSkillDescription,
                _professionTradeSkillNote,
                _validDriversLicenseIndicator,
                _validDriversLicenseIndicatorNote,
                _automobileAvailableforUseIndicator,
                _automobileAvailableforUseIndicatorNote,
                _yearsAndMonthsOfLongestFullTimeJobTimeSpan,
                _yearsAndMonthsOfLongestFullTimeJobTimeSpanNote,
                _usualOrLastDensAsiOccupationType,
                _usualOrLastOccupationDescription,
                _usualOrLastOccupationNote,
                _contributionOfSomeoneToSupportIndicator,
                _contributionOfSomeoneToSupportIndicatorNote,
                _contributionConstituteMajorityOfYourSupportIndicator,
                _contributionConstituteMajorityOfYourSupportIndicatorNote,
                _pastThreeYearsDensAsiEmploymentPattern,
                _pastThreeYearsDensAsiEmploymentPatternNote,
                _workInLastThirtyDaysPaidDayCount,
                _workInLastThirtyDaysPaidDayCountNote,
                _netIncomeAmount,
                _netIncomeAmountNote,
                _unemploymentCompensationAmount,
                _unemploymentCompensationAmountNote,
                _welfareAmount,
                _welfareAmountNote,
                _pensionBenefitsSocialSecurityAmount,
                _pensionBenefitsSocialSecurityAmountNote,
                _mateFamilyFriendsAmount,
                _mateFamilyFriendsAmountNote,
                _illegalAmount,
                _illegalAmountNote,
                _dependentPeopleCount,
                _dependentPeopleCountNote,
                _employmentProblemsDayCount,
                _employmentProblemsDayCountNote,
                _troubledByEmploymentProblemsDensAsiPatientRating,
                _troubledByEmploymentProblemsDensAsiPatientRatingNote,
                _importanceOfEmployementProblemCounselingDensAsiPatientRating,
                _importanceOfEmployementProblemCounselingDensAsiPatientRatingNote,
                _patientCounselingDensAsiInterviewerRating,
                _patientCounselingDensAsiInterviewerRatingNote,
                _confidenceDistortedByPatientMisrepresentationIndicator,
                _confidenceDistortedByPatientMisrepresentationIndicatorNote,
                _confidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                _sectionNote
                );
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="DensAsiEmploymentStatusSectionBuilder"/> to <see cref="DensAsiEmploymentStatusSection"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator DensAsiEmploymentStatusSection(DensAsiEmploymentStatusSectionBuilder builder)
        {
            return builder.Build();
        }
    }
}
