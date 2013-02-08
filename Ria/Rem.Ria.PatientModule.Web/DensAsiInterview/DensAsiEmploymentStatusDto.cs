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
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.DensAsiInterview
{
    /// <summary>
    /// Data transfer object for DensAsiEmploymentStatus class.
    /// </summary>
    public class DensAsiEmploymentStatusDto : DensAsiDtoBase
    {
        #region Constants and Fields

        private DensAsiNonResponseTypeDto<bool?> _automobileAvailableforUseIndicator;
        private string _automobileAvailableforUseIndicatorNote;
        private bool? _confidenceDistortedByPatientMisrepresentationIndicator;
        private string _confidenceDistortedByPatientMisrepresentationIndicatorNote;
        private bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private DensAsiNonResponseTypeDto<bool?> _contributionConstituteMajorityOfYourSupportIndicator;
        private string _contributionConstituteMajorityOfYourSupportIndicatorNote;
        private DensAsiNonResponseTypeDto<bool?> _contributionOfSomeoneToSupportIndicator;
        private string _contributionOfSomeoneToSupportIndicatorNote;
        private DensAsiNonResponseTypeDto<int?> _dependentPeopleCount;
        private string _dependentPeopleCountNote;
        private DensAsiNonResponseTypeDto<int?> _employmentProblemsDayCount;
        private string _employmentProblemsDayCountNote;
        private DensAsiNonResponseTypeDto<int?> _illegalAmount;
        private string _illegalAmountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _importanceOfEmploymentProblemCounselingDensAsiPatientRating;
        private string _importanceOfEmploymentProblemCounselingDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<int?> _mateFamilyFriendsAmount;
        private string _mateFamilyFriendsAmountNote;
        private DensAsiNonResponseTypeDto<int?> _netIncomeAmount;
        private string _netIncomeAmountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _pastThreeYearsDensAsiEmploymentPattern;
        private string _pastThreeYearsDensAsiEmploymentPatternNote;
        private LookupValueDto _patientCounselingDensAsiInterviewerRating;
        private string _patientCounselingDensAsiInterviewerRatingNote;
        private DensAsiNonResponseTypeDto<int?> _pensionBenefitsSocialSecurityAmount;
        private string _pensionBenefitsSocialSecurityAmountNote;
        private string _professionTradeSkillDescription;
        private DensAsiNonResponseTypeDto<bool?> _professionTradeSkillIndicator;
        private string _professionTradeSkillNote;
        private string _sectionNote;
        private DensAsiNonResponseTypeDto<int?> _technicalEducationCompletedMonthCount;
        private string _technicalEducationCompletedMonthCountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _troubledByEmploymentProblemsDensAsiPatientRating;
        private string _troubledByEmploymentProblemsDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<int?> _unemploymentCompensationAmount;
        private string _unemploymentCompensationAmountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _usualOrLastDensAsiOccupationType;
        private string _usualOrLastOccupationDescription;
        private string _usualOrLastOccupationNote;
        private DensAsiNonResponseTypeDto<bool?> _validDriversLicenseIndicator;
        private string _validDriversLicenseIndicatorNote;
        private DensAsiNonResponseTypeDto<int?> _welfareAmount;
        private string _welfareAmountNote;
        private DensAsiNonResponseTypeDto<int?> _workInLastThirtyDaysPaidDayCount;
        private string _workInLastThirtyDaysPaidDayCountNote;
        private DensAsiNonResponseTypeDto<TimeSpan?> _yearsAndMonthsEducationCompletedTimeSpan;
        private string _yearsAndMonthsEducationCompletedTimeSpanNote;
        private DensAsiNonResponseTypeDto<TimeSpan?> _yearsAndMonthsOfLongestFullTimeJobTimeSpan;
        private string _yearsAndMonthsOfLongestFullTimeJobTimeSpanNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// Question Number: E5
        /// </summary>
        /// <value>The automobile availablefor use indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AutomobileAvailableforUseIndicator
        {
            get { return _automobileAvailableforUseIndicator; }
            set { ApplyPropertyChange ( ref _automobileAvailableforUseIndicator, () => AutomobileAvailableforUseIndicator, value ); }
        }

        /// <summary>
        /// Question Number: E5
        /// </summary>
        /// <value>The automobile availablefor use indicator note.</value>
        public string AutomobileAvailableforUseIndicatorNote
        {
            get { return _automobileAvailableforUseIndicatorNote; }
            set { ApplyPropertyChange ( ref _automobileAvailableforUseIndicatorNote, () => AutomobileAvailableforUseIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: E23
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
        /// Question Number: E23
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
        /// Question Number: E24
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
        /// Question Number: E24
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
        /// Question Number: E9
        /// </summary>
        /// <value>The contribution constitute majority of your support indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ContributionConstituteMajorityOfYourSupportIndicator
        {
            get { return _contributionConstituteMajorityOfYourSupportIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _contributionConstituteMajorityOfYourSupportIndicator, () => ContributionConstituteMajorityOfYourSupportIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: E9
        /// </summary>
        /// <value>The contribution constitute majority of your support indicator note.</value>
        public string ContributionConstituteMajorityOfYourSupportIndicatorNote
        {
            get { return _contributionConstituteMajorityOfYourSupportIndicatorNote; }
            set
            {
                ApplyPropertyChange (
                    ref _contributionConstituteMajorityOfYourSupportIndicatorNote,
                    () => ContributionConstituteMajorityOfYourSupportIndicatorNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: E8
        /// </summary>
        /// <value>The contribution of someone to support indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ContributionOfSomeoneToSupportIndicator
        {
            get { return _contributionOfSomeoneToSupportIndicator; }
            set { ApplyPropertyChange ( ref _contributionOfSomeoneToSupportIndicator, () => ContributionOfSomeoneToSupportIndicator, value ); }
        }

        /// <summary>
        /// Question Number: E8
        /// </summary>
        /// <value>The contribution of someone to support indicator note.</value>
        public string ContributionOfSomeoneToSupportIndicatorNote
        {
            get { return _contributionOfSomeoneToSupportIndicatorNote; }
            set { ApplyPropertyChange ( ref _contributionOfSomeoneToSupportIndicatorNote, () => ContributionOfSomeoneToSupportIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: E18
        /// </summary>
        /// <value>The dependent people count.</value>
        public DensAsiNonResponseTypeDto<int?> DependentPeopleCount
        {
            get { return _dependentPeopleCount; }
            set { ApplyPropertyChange ( ref _dependentPeopleCount, () => DependentPeopleCount, value ); }
        }

        /// <summary>
        /// Question Number: E18
        /// </summary>
        /// <value>The dependent people count note.</value>
        public string DependentPeopleCountNote
        {
            get { return _dependentPeopleCountNote; }
            set { ApplyPropertyChange ( ref _dependentPeopleCountNote, () => DependentPeopleCountNote, value ); }
        }

        /// <summary>
        /// Question Number: E19
        /// </summary>
        /// <value>The employment problems day count.</value>
        public DensAsiNonResponseTypeDto<int?> EmploymentProblemsDayCount
        {
            get { return _employmentProblemsDayCount; }
            set { ApplyPropertyChange ( ref _employmentProblemsDayCount, () => EmploymentProblemsDayCount, value ); }
        }

        /// <summary>
        /// Question Number: E19
        /// </summary>
        /// <value>The employment problems day count note.</value>
        public string EmploymentProblemsDayCountNote
        {
            get { return _employmentProblemsDayCountNote; }
            set { ApplyPropertyChange ( ref _employmentProblemsDayCountNote, () => EmploymentProblemsDayCountNote, value ); }
        }

        /// <summary>
        /// Question Number: E17
        /// </summary>
        /// <value>The illegal amount.</value>
        public DensAsiNonResponseTypeDto<int?> IllegalAmount
        {
            get { return _illegalAmount; }
            set { ApplyPropertyChange ( ref _illegalAmount, () => IllegalAmount, value ); }
        }

        /// <summary>
        /// Question Number: E17
        /// </summary>
        /// <value>The illegal amount note.</value>
        public string IllegalAmountNote
        {
            get { return _illegalAmountNote; }
            set { ApplyPropertyChange ( ref _illegalAmountNote, () => IllegalAmountNote, value ); }
        }

        /// <summary>
        /// Question Number: E21
        /// </summary>
        /// <value>The importance of employment problem counseling dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> ImportanceOfEmploymentProblemCounselingDensAsiPatientRating
        {
            get { return _importanceOfEmploymentProblemCounselingDensAsiPatientRating; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfEmploymentProblemCounselingDensAsiPatientRating,
                    () => ImportanceOfEmploymentProblemCounselingDensAsiPatientRating,
                    value );
            }
        }

        /// <summary>
        /// Question Number: E21
        /// </summary>
        /// <value>The importance of employment problem counseling dens asi patient rating note.</value>
        public string ImportanceOfEmploymentProblemCounselingDensAsiPatientRatingNote
        {
            get { return _importanceOfEmploymentProblemCounselingDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfEmploymentProblemCounselingDensAsiPatientRatingNote,
                    () => ImportanceOfEmploymentProblemCounselingDensAsiPatientRatingNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: E16
        /// </summary>
        /// <value>The mate family friends amount.</value>
        public DensAsiNonResponseTypeDto<int?> MateFamilyFriendsAmount
        {
            get { return _mateFamilyFriendsAmount; }
            set { ApplyPropertyChange ( ref _mateFamilyFriendsAmount, () => MateFamilyFriendsAmount, value ); }
        }

        /// <summary>
        /// Question Number: E16
        /// </summary>
        /// <value>The mate family friends amount note.</value>
        public string MateFamilyFriendsAmountNote
        {
            get { return _mateFamilyFriendsAmountNote; }
            set { ApplyPropertyChange ( ref _mateFamilyFriendsAmountNote, () => MateFamilyFriendsAmountNote, value ); }
        }

        /// <summary>
        /// Question Number: E12
        /// </summary>
        /// <value>The net income amount.</value>
        public DensAsiNonResponseTypeDto<int?> NetIncomeAmount
        {
            get { return _netIncomeAmount; }
            set { ApplyPropertyChange ( ref _netIncomeAmount, () => NetIncomeAmount, value ); }
        }

        /// <summary>
        /// Question Number: E12
        /// </summary>
        /// <value>The net income amount note.</value>
        public string NetIncomeAmountNote
        {
            get { return _netIncomeAmountNote; }
            set { ApplyPropertyChange ( ref _netIncomeAmountNote, () => NetIncomeAmountNote, value ); }
        }

        /// <summary>
        /// Question Number: E10
        /// </summary>
        /// <value>The past three years dens asi employment pattern.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> PastThreeYearsDensAsiEmploymentPattern
        {
            get { return _pastThreeYearsDensAsiEmploymentPattern; }
            set { ApplyPropertyChange ( ref _pastThreeYearsDensAsiEmploymentPattern, () => PastThreeYearsDensAsiEmploymentPattern, value ); }
        }

        /// <summary>
        /// Question Number: E10
        /// </summary>
        /// <value>The past three years dens asi employment pattern note.</value>
        public string PastThreeYearsDensAsiEmploymentPatternNote
        {
            get { return _pastThreeYearsDensAsiEmploymentPatternNote; }
            set { ApplyPropertyChange ( ref _pastThreeYearsDensAsiEmploymentPatternNote, () => PastThreeYearsDensAsiEmploymentPatternNote, value ); }
        }

        /// <summary>
        /// Question Number: E22
        /// </summary>
        /// <value>The patient counseling dens asi interviewer rating.</value>
        public LookupValueDto PatientCounselingDensAsiInterviewerRating
        {
            get { return _patientCounselingDensAsiInterviewerRating; }
            set { ApplyPropertyChange ( ref _patientCounselingDensAsiInterviewerRating, () => PatientCounselingDensAsiInterviewerRating, value ); }
        }

        /// <summary>
        /// Question Number: E22
        /// </summary>
        /// <value>The patient counseling dens asi interviewer rating note.</value>
        public string PatientCounselingDensAsiInterviewerRatingNote
        {
            get { return _patientCounselingDensAsiInterviewerRatingNote; }
            set { ApplyPropertyChange ( ref _patientCounselingDensAsiInterviewerRatingNote, () => PatientCounselingDensAsiInterviewerRatingNote, value ); }
        }

        /// <summary>
        /// Question Number: E15
        /// </summary>
        /// <value>The pension benefits social security amount.</value>
        public DensAsiNonResponseTypeDto<int?> PensionBenefitsSocialSecurityAmount
        {
            get { return _pensionBenefitsSocialSecurityAmount; }
            set { ApplyPropertyChange ( ref _pensionBenefitsSocialSecurityAmount, () => PensionBenefitsSocialSecurityAmount, value ); }
        }

        /// <summary>
        /// Question Number: E15
        /// </summary>
        /// <value>The pension benefits social security amount note.</value>
        public string PensionBenefitsSocialSecurityAmountNote
        {
            get { return _pensionBenefitsSocialSecurityAmountNote; }
            set { ApplyPropertyChange ( ref _pensionBenefitsSocialSecurityAmountNote, () => PensionBenefitsSocialSecurityAmountNote, value ); }
        }

        /// <summary>
        /// Question Number: E3
        /// </summary>
        /// <value>The profession trade skill description.</value>
        public string ProfessionTradeSkillDescription
        {
            get { return _professionTradeSkillDescription; }
            set { ApplyPropertyChange ( ref _professionTradeSkillDescription, () => ProfessionTradeSkillDescription, value ); }
        }

        /// <summary>
        /// Question Number: E3
        /// </summary>
        /// <value>The profession trade skill indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProfessionTradeSkillIndicator
        {
            get { return _professionTradeSkillIndicator; }
            set { ApplyPropertyChange ( ref _professionTradeSkillIndicator, () => ProfessionTradeSkillIndicator, value ); }
        }

        /// <summary>
        /// Question Number: E3
        /// </summary>
        /// <value>The profession trade skill note.</value>
        public string ProfessionTradeSkillNote
        {
            get { return _professionTradeSkillNote; }
            set { ApplyPropertyChange ( ref _professionTradeSkillNote, () => ProfessionTradeSkillNote, value ); }
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
        /// Question Number: E2
        /// </summary>
        /// <value>The technical education completed month count.</value>
        public DensAsiNonResponseTypeDto<int?> TechnicalEducationCompletedMonthCount
        {
            get { return _technicalEducationCompletedMonthCount; }
            set { ApplyPropertyChange ( ref _technicalEducationCompletedMonthCount, () => TechnicalEducationCompletedMonthCount, value ); }
        }

        /// <summary>
        /// Question Number: E2
        /// </summary>
        /// <value>The technical education completed month count note.</value>
        public string TechnicalEducationCompletedMonthCountNote
        {
            get { return _technicalEducationCompletedMonthCountNote; }
            set { ApplyPropertyChange ( ref _technicalEducationCompletedMonthCountNote, () => TechnicalEducationCompletedMonthCountNote, value ); }
        }

        /// <summary>
        /// Question Number: E20
        /// </summary>
        /// <value>The troubled by employment problems dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> TroubledByEmploymentProblemsDensAsiPatientRating
        {
            get { return _troubledByEmploymentProblemsDensAsiPatientRating; }
            set
            {
                ApplyPropertyChange (
                    ref _troubledByEmploymentProblemsDensAsiPatientRating, () => TroubledByEmploymentProblemsDensAsiPatientRating, value );
            }
        }

        /// <summary>
        /// Question Number: E20
        /// </summary>
        /// <value>The troubled by employment problems dens asi patient rating note.</value>
        public string TroubledByEmploymentProblemsDensAsiPatientRatingNote
        {
            get { return _troubledByEmploymentProblemsDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _troubledByEmploymentProblemsDensAsiPatientRatingNote, () => TroubledByEmploymentProblemsDensAsiPatientRatingNote, value );
            }
        }

        /// <summary>
        /// Question Number: E13
        /// </summary>
        /// <value>The unemployment compensation amount.</value>
        public DensAsiNonResponseTypeDto<int?> UnemploymentCompensationAmount
        {
            get { return _unemploymentCompensationAmount; }
            set { ApplyPropertyChange ( ref _unemploymentCompensationAmount, () => UnemploymentCompensationAmount, value ); }
        }

        /// <summary>
        /// Question Number: E13
        /// </summary>
        /// <value>The unemployment compensation amount note.</value>
        public string UnemploymentCompensationAmountNote
        {
            get { return _unemploymentCompensationAmountNote; }
            set { ApplyPropertyChange ( ref _unemploymentCompensationAmountNote, () => UnemploymentCompensationAmountNote, value ); }
        }

        /// <summary>
        /// Question Number: E7
        /// </summary>
        /// <value>The type of the usual or last dens asi occupation.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> UsualOrLastDensAsiOccupationType
        {
            get { return _usualOrLastDensAsiOccupationType; }
            set { ApplyPropertyChange ( ref _usualOrLastDensAsiOccupationType, () => UsualOrLastDensAsiOccupationType, value ); }
        }

        /// <summary>
        /// Question Number: E7
        /// </summary>
        /// <value>The usual or last occupation description.</value>
        public string UsualOrLastOccupationDescription
        {
            get { return _usualOrLastOccupationDescription; }
            set { ApplyPropertyChange ( ref _usualOrLastOccupationDescription, () => UsualOrLastOccupationDescription, value ); }
        }

        /// <summary>
        /// Question Number: E7
        /// </summary>
        /// <value>The usual or last occupation note.</value>
        public string UsualOrLastOccupationNote
        {
            get { return _usualOrLastOccupationNote; }
            set { ApplyPropertyChange ( ref _usualOrLastOccupationNote, () => UsualOrLastOccupationNote, value ); }
        }

        /// <summary>
        /// Question Number: E4
        /// </summary>
        /// <value>The valid drivers license indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ValidDriversLicenseIndicator
        {
            get { return _validDriversLicenseIndicator; }
            set { ApplyPropertyChange ( ref _validDriversLicenseIndicator, () => ValidDriversLicenseIndicator, value ); }
        }

        /// <summary>
        /// Question Number: E4
        /// </summary>
        /// <value>The valid drivers license indicator note.</value>
        public string ValidDriversLicenseIndicatorNote
        {
            get { return _validDriversLicenseIndicatorNote; }
            set { ApplyPropertyChange ( ref _validDriversLicenseIndicatorNote, () => ValidDriversLicenseIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: E14
        /// </summary>
        /// <value>The welfare amount.</value>
        public DensAsiNonResponseTypeDto<int?> WelfareAmount
        {
            get { return _welfareAmount; }
            set { ApplyPropertyChange ( ref _welfareAmount, () => WelfareAmount, value ); }
        }

        /// <summary>
        /// Question Number: E14
        /// </summary>
        /// <value>The welfare amount note.</value>
        public string WelfareAmountNote
        {
            get { return _welfareAmountNote; }
            set { ApplyPropertyChange ( ref _welfareAmountNote, () => WelfareAmountNote, value ); }
        }

        /// <summary>
        /// Question Number: E11
        /// </summary>
        /// <value>The work in last thirty days paid day count.</value>
        public DensAsiNonResponseTypeDto<int?> WorkInLastThirtyDaysPaidDayCount
        {
            get { return _workInLastThirtyDaysPaidDayCount; }
            set { ApplyPropertyChange ( ref _workInLastThirtyDaysPaidDayCount, () => WorkInLastThirtyDaysPaidDayCount, value ); }
        }

        /// <summary>
        /// Question Number: E11
        /// </summary>
        /// <value>The work in last thirty days paid day count note.</value>
        public string WorkInLastThirtyDaysPaidDayCountNote
        {
            get { return _workInLastThirtyDaysPaidDayCountNote; }
            set { ApplyPropertyChange ( ref _workInLastThirtyDaysPaidDayCountNote, () => WorkInLastThirtyDaysPaidDayCountNote, value ); }
        }

        /// <summary>
        /// Question Number: E1
        /// </summary>
        /// <value>The years and months education completed time span.</value>
        public DensAsiNonResponseTypeDto<TimeSpan?> YearsAndMonthsEducationCompletedTimeSpan
        {
            get { return _yearsAndMonthsEducationCompletedTimeSpan; }
            set { ApplyPropertyChange ( ref _yearsAndMonthsEducationCompletedTimeSpan, () => YearsAndMonthsEducationCompletedTimeSpan, value ); }
        }

        /// <summary>
        /// Question Number: E1
        /// </summary>
        /// <value>The years and months education completed time span note.</value>
        public string YearsAndMonthsEducationCompletedTimeSpanNote
        {
            get { return _yearsAndMonthsEducationCompletedTimeSpanNote; }
            set { ApplyPropertyChange ( ref _yearsAndMonthsEducationCompletedTimeSpanNote, () => YearsAndMonthsEducationCompletedTimeSpanNote, value ); }
        }

        /// <summary>
        /// Question Number: E6
        /// </summary>
        /// <value>The years and months of longest full time job time span.</value>
        public DensAsiNonResponseTypeDto<TimeSpan?> YearsAndMonthsOfLongestFullTimeJobTimeSpan
        {
            get { return _yearsAndMonthsOfLongestFullTimeJobTimeSpan; }
            set { ApplyPropertyChange ( ref _yearsAndMonthsOfLongestFullTimeJobTimeSpan, () => YearsAndMonthsOfLongestFullTimeJobTimeSpan, value ); }
        }

        /// <summary>
        /// Question Number: E6
        /// </summary>
        /// <value>The years and months of longest full time job time span note.</value>
        public string YearsAndMonthsOfLongestFullTimeJobTimeSpanNote
        {
            get { return _yearsAndMonthsOfLongestFullTimeJobTimeSpanNote; }
            set
            {
                ApplyPropertyChange (
                    ref _yearsAndMonthsOfLongestFullTimeJobTimeSpanNote, () => YearsAndMonthsOfLongestFullTimeJobTimeSpanNote, value );
            }
        }

        #endregion
    }
}
