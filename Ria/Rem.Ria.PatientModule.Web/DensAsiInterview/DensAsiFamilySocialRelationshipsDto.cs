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
    /// Data transfer object for DensAsiFamilySocialRelationships class.
    /// </summary>
    public class DensAsiFamilySocialRelationshipsDto : DensAsiDtoBase
    {
        #region Constants and Fields

        private DensAsiNonResponseTypeDto<bool?> _abusedEmotionallyInLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _abusedEmotionallyInLifetimeIndicator;
        private string _abusedEmotionallyNote;
        private DensAsiNonResponseTypeDto<bool?> _abusedPhysicallyInLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _abusedPhysicallyInLifetimeIndicator;
        private string _abusedPhysicallyNote;
        private DensAsiNonResponseTypeDto<bool?> _abusedSexuallyInLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _abusedSexuallyInLifetimeIndicator;
        private string _abusedSexuallyNote;
        private DensAsiNonResponseTypeDto<int?> _closeFriendsCount;
        private string _closeFriendsCountNote;
        private bool? _confidenceDistortedByPatientMisrepresentationIndicator;
        private string _confidenceDistortedByPatientMisrepresentationIndicatorNote;
        private bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private DensAsiNonResponseTypeDto<int?> _conflictsWithOthersInLastThirtyDaysDayCount;
        private string _conflictsWithOthersInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _densAsiFreeTimeSpentType;
        private string _densAsiFreeTimeSpentTypeNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _densAsiMaritalStatus;
        private string _densAsiMaritalStatusNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _freeTimeSpentTypeDensAsiSatisfaction;
        private string _freeTimeSpentTypeDensAsiSatisfactionNote;
        private DensAsiNonResponseTypeDto<int?> _homelessInLastThirtyDaysDayCount;
        private string _homelessInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseTypeDto<int?> _hospitalJailInLastThirtyDaysDayCount;
        private string _hospitalJailInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _importanceOfFamilyProblemCounselingDensAsiPatientRating;
        private string _importanceOfFamilyProblemCounselingDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _importanceOfSocialProblemCounselingDensAsiPatientRating;
        private string _importanceOfSocialProblemCounselingDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _livingArrangementTypeDensAsiSatisfaction;
        private string _livingArrangementTypeDensAsiSatisfactionNote;
        private DensAsiNonResponseTypeDto<bool?> _livingWithAnyoneWhoHasAlcoholProblemIndicator;
        private string _livingWithAnyoneWhoHasAlcoholProblemIndicatorNote;
        private DensAsiNonResponseTypeDto<bool?> _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator;
        private string _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _maritalStatusDensAsiSatisfaction;
        private string _maritalStatusDensAsiSatisfactionNote;
        private DensAsiNonResponseTypeDto<int?> _notOwnedHouseInLastThirtyDaysDayCount;
        private string _notOwnedHouseInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _pastThreeYearsDensAsiLivingArrangementType;
        private string _pastThreeYearsDensAsiLivingArrangementTypeNote;
        private LookupValueDto _patientFamilySocialCounselingDensAsiInterviewerRating;
        private string _patientFamilySocialCounselingDensAsiInterviewerRatingNote;
        private DensAsiNonResponseTypeDto<bool?> _problemsBrotherSisterInLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _problemsBrotherSisterInLifetimeIndicator;
        private string _problemsBrotherSisterNote;
        private DensAsiNonResponseTypeDto<bool?> _problemsChildrenInLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _problemsChildrenInLifetimeIndicator;
        private string _problemsChildrenNote;
        private DensAsiNonResponseTypeDto<bool?> _problemsCloseFriendsInLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _problemsCloseFriendsInLifetimeIndicator;
        private string _problemsCloseFriendsNote;
        private DensAsiNonResponseTypeDto<bool?> _problemsCoworkersInLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _problemsCoworkersInLifetimeIndicator;
        private string _problemsCoworkersNote;
        private DensAsiNonResponseTypeDto<bool?> _problemsFatherInLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _problemsFatherInLifetimeIndicator;
        private string _problemsFatherNote;
        private DensAsiNonResponseTypeDto<bool?> _problemsMotherInLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _problemsMotherInLifetimeIndicator;
        private string _problemsMotherNote;
        private DensAsiNonResponseTypeDto<bool?> _problemsNeighborsInLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _problemsNeighborsInLifetimeIndicator;
        private string _problemsNeighborsNote;
        private string _problemsOtherSignificantFamilyDescription;
        private DensAsiNonResponseTypeDto<bool?> _problemsOtherSignificantFamilyInLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _problemsOtherSignificantFamilyInLifetimeIndicator;
        private string _problemsOtherSignificantFamilyNote;
        private DensAsiNonResponseTypeDto<bool?> _problemsSexualPartnerInLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _problemsSexualPartnerInLifetimeIndicator;
        private string _problemsSexualPartnerNote;
        private LookupValueDto _brotherSisterDensAsiHasRelationshipOption;
        private string _brotherSisterDensAsiHasRelationshipOptionNote;
        private LookupValueDto _childrenDensAsiHasRelationshipOption;
        private string _childrenDensAsiHasRelationshipOptionNote;
        private LookupValueDto _fatherDensAsiHasParentalRelationshipOption;
        private string _fatherDensAsiHasParentalRelationshipOptionNote;
        private LookupValueDto _friendsDensAsiHasRelationshipOption;
        private string _friendsDensAsiHasRelationshipOptionNote;
        private LookupValueDto _motherDensAsiHasParentalRelationshipOption;
        private string _motherDensAsiHasParentalRelationshipOptionNote;
        private LookupValueDto _sexualPartnerDensAsiHasRelationshipOption;
        private string _sexualPartnerDensAsiHasRelationshipOptionNote;
        private string _sectionNote;
        private DensAsiNonResponseTypeDto<int?> _seriousFamilyConflictsInLastThirtyDaysDayCount;
        private string _seriousFamilyConflictsInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseTypeDto<int?> _shelterInLastThirtyDaysDayCount;
        private string _shelterInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _troubledByFamilyProblemsDensAsiPatientRating;
        private string _troubledByFamilyProblemsDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _troubledBySocialProblemsDensAsiPatientRating;
        private string _troubledBySocialProblemsDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<TimeSpan?> _yearsAndMonthsInLivingArrangementTypeTimeSpan;
        private string _yearsAndMonthsInLivingArrangementTypeTimeSpanNote;
        private DensAsiNonResponseTypeDto<TimeSpan?> _yearsAndMonthsWithMaritalStatusTimeSpan;
        private string _yearsAndMonthsWithMaritalStatusTimeSpanNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// Question Number: F27
        /// </summary>
        /// <value>The abused emotionally in last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AbusedEmotionallyInLastThirtyDaysIndicator
        {
            get { return _abusedEmotionallyInLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _abusedEmotionallyInLastThirtyDaysIndicator, () => AbusedEmotionallyInLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F27
        /// </summary>
        /// <value>The abused emotionally in lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AbusedEmotionallyInLifetimeIndicator
        {
            get { return _abusedEmotionallyInLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _abusedEmotionallyInLifetimeIndicator, () => AbusedEmotionallyInLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F27
        /// </summary>
        /// <value>The abused emotionally note.</value>
        public string AbusedEmotionallyNote
        {
            get { return _abusedEmotionallyNote; }
            set { ApplyPropertyChange ( ref _abusedEmotionallyNote, () => AbusedEmotionallyNote, value ); }
        }

        /// <summary>
        /// Question Number: F28
        /// </summary>
        /// <value>The abused physically in last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AbusedPhysicallyInLastThirtyDaysIndicator
        {
            get { return _abusedPhysicallyInLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _abusedPhysicallyInLastThirtyDaysIndicator, () => AbusedPhysicallyInLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F28
        /// </summary>
        /// <value>The abused physically in lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AbusedPhysicallyInLifetimeIndicator
        {
            get { return _abusedPhysicallyInLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _abusedPhysicallyInLifetimeIndicator, () => AbusedPhysicallyInLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F28
        /// </summary>
        /// <value>The abused physically note.</value>
        public string AbusedPhysicallyNote
        {
            get { return _abusedPhysicallyNote; }
            set { ApplyPropertyChange ( ref _abusedPhysicallyNote, () => AbusedPhysicallyNote, value ); }
        }

        /// <summary>
        /// Question Number: F29
        /// </summary>
        /// <value>The abused sexually in last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AbusedSexuallyInLastThirtyDaysIndicator
        {
            get { return _abusedSexuallyInLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _abusedSexuallyInLastThirtyDaysIndicator, () => AbusedSexuallyInLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F29
        /// </summary>
        /// <value>The abused sexually in lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AbusedSexuallyInLifetimeIndicator
        {
            get { return _abusedSexuallyInLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _abusedSexuallyInLifetimeIndicator, () => AbusedSexuallyInLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F29
        /// </summary>
        /// <value>The abused sexually note.</value>
        public string AbusedSexuallyNote
        {
            get { return _abusedSexuallyNote; }
            set { ApplyPropertyChange ( ref _abusedSexuallyNote, () => AbusedSexuallyNote, value ); }
        }

        /// <summary>
        /// Question Number: F11
        /// </summary>
        /// <value>The close friends count.</value>
        public DensAsiNonResponseTypeDto<int?> CloseFriendsCount
        {
            get { return _closeFriendsCount; }
            set { ApplyPropertyChange ( ref _closeFriendsCount, () => CloseFriendsCount, value ); }
        }

        /// <summary>
        /// Question Number: F11
        /// </summary>
        /// <value>The close friends count note.</value>
        public string CloseFriendsCountNote
        {
            get { return _closeFriendsCountNote; }
            set { ApplyPropertyChange ( ref _closeFriendsCountNote, () => CloseFriendsCountNote, value ); }
        }

        /// <summary>
        /// Question Number: F37
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
        /// Question Number: F37
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
        /// Question Number: F38
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
        /// Question Number: F38
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
        /// Question Number: F31
        /// </summary>
        /// <value>The conflicts with others in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> ConflictsWithOthersInLastThirtyDaysDayCount
        {
            get { return _conflictsWithOthersInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _conflictsWithOthersInLastThirtyDaysDayCount, () => ConflictsWithOthersInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: F31
        /// </summary>
        /// <value>The conflicts with others in last thirty days day count note.</value>
        public string ConflictsWithOthersInLastThirtyDaysDayCountNote
        {
            get { return _conflictsWithOthersInLastThirtyDaysDayCountNote; }
            set
            {
                ApplyPropertyChange (
                    ref _conflictsWithOthersInLastThirtyDaysDayCountNote, () => ConflictsWithOthersInLastThirtyDaysDayCountNote, value );
            }
        }

        /// <summary>
        /// Question Number: F9
        /// </summary>
        /// <value>The type of the dens asi free time spent.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> DensAsiFreeTimeSpentType
        {
            get { return _densAsiFreeTimeSpentType; }
            set { ApplyPropertyChange ( ref _densAsiFreeTimeSpentType, () => DensAsiFreeTimeSpentType, value ); }
        }

        /// <summary>
        /// Question Number: F9
        /// </summary>
        /// <value>The dens asi free time spent type note.</value>
        public string DensAsiFreeTimeSpentTypeNote
        {
            get { return _densAsiFreeTimeSpentTypeNote; }
            set { ApplyPropertyChange ( ref _densAsiFreeTimeSpentTypeNote, () => DensAsiFreeTimeSpentTypeNote, value ); }
        }

        /// <summary>
        /// Question Number: F1
        /// </summary>
        /// <value>The dens asi marital status.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> DensAsiMaritalStatus
        {
            get { return _densAsiMaritalStatus; }
            set { ApplyPropertyChange ( ref _densAsiMaritalStatus, () => DensAsiMaritalStatus, value ); }
        }

        /// <summary>
        /// Question Number: F1
        /// </summary>
        /// <value>The dens asi marital status note.</value>
        public string DensAsiMaritalStatusNote
        {
            get { return _densAsiMaritalStatusNote; }
            set { ApplyPropertyChange ( ref _densAsiMaritalStatusNote, () => DensAsiMaritalStatusNote, value ); }
        }

        /// <summary>
        /// Question Number: F10
        /// </summary>
        /// <value>The free time spent type satisfaction indicator.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> FreeTimeSpentTypeDensAsiSatisfaction
        {
            get { return _freeTimeSpentTypeDensAsiSatisfaction; }
            set { ApplyPropertyChange ( ref _freeTimeSpentTypeDensAsiSatisfaction, () => FreeTimeSpentTypeDensAsiSatisfaction, value ); }
        }

        /// <summary>
        /// Question Number: F10
        /// </summary>
        /// <value>The free time spent type satisfaction indicator note.</value>
        public string FreeTimeSpentTypeDensAsiSatisfactionNote
        {
            get { return _freeTimeSpentTypeDensAsiSatisfactionNote; }
            set { ApplyPropertyChange ( ref _freeTimeSpentTypeDensAsiSatisfactionNote, () => FreeTimeSpentTypeDensAsiSatisfactionNote, value ); }
        }

        /// <summary>
        /// Question Number: F113
        /// </summary>
        /// <value>The homeless in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> HomelessInLastThirtyDaysDayCount
        {
            get { return _homelessInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _homelessInLastThirtyDaysDayCount, () => HomelessInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: F113
        /// </summary>
        /// <value>The homeless in last thirty days day count note.</value>
        public string HomelessInLastThirtyDaysDayCountNote
        {
            get { return _homelessInLastThirtyDaysDayCountNote; }
            set { ApplyPropertyChange ( ref _homelessInLastThirtyDaysDayCountNote, () => HomelessInLastThirtyDaysDayCountNote, value ); }
        }

        /// <summary>
        /// Question Number: F116
        /// </summary>
        /// <value>The hospital jail in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> HospitalJailInLastThirtyDaysDayCount
        {
            get { return _hospitalJailInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _hospitalJailInLastThirtyDaysDayCount, () => HospitalJailInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: F116
        /// </summary>
        /// <value>The hospital jail in last thirty days day count note.</value>
        public string HospitalJailInLastThirtyDaysDayCountNote
        {
            get { return _hospitalJailInLastThirtyDaysDayCountNote; }
            set { ApplyPropertyChange ( ref _hospitalJailInLastThirtyDaysDayCountNote, () => HospitalJailInLastThirtyDaysDayCountNote, value ); }
        }

        /// <summary>
        /// Question Number: F34
        /// </summary>
        /// <value>The importance of family problem counseling dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> ImportanceOfFamilyProblemCounselingDensAsiPatientRating
        {
            get { return _importanceOfFamilyProblemCounselingDensAsiPatientRating; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfFamilyProblemCounselingDensAsiPatientRating, () => ImportanceOfFamilyProblemCounselingDensAsiPatientRating, value );
            }
        }

        /// <summary>
        /// Question Number: F34
        /// </summary>
        /// <value>The importance of family problem counseling dens asi patient rating note.</value>
        public string ImportanceOfFamilyProblemCounselingDensAsiPatientRatingNote
        {
            get { return _importanceOfFamilyProblemCounselingDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfFamilyProblemCounselingDensAsiPatientRatingNote,
                    () => ImportanceOfFamilyProblemCounselingDensAsiPatientRatingNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: F35
        /// </summary>
        /// <value>The importance of social problem counseling dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> ImportanceOfSocialProblemCounselingDensAsiPatientRating
        {
            get { return _importanceOfSocialProblemCounselingDensAsiPatientRating; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfSocialProblemCounselingDensAsiPatientRating, () => ImportanceOfSocialProblemCounselingDensAsiPatientRating, value );
            }
        }

        /// <summary>
        /// Question Number: F35
        /// </summary>
        /// <value>The importance of social problem counseling dens asi patient rating note.</value>
        public string ImportanceOfSocialProblemCounselingDensAsiPatientRatingNote
        {
            get { return _importanceOfSocialProblemCounselingDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfSocialProblemCounselingDensAsiPatientRatingNote,
                    () => ImportanceOfSocialProblemCounselingDensAsiPatientRatingNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: F6
        /// </summary>
        /// <value>The living arrangement type satisfaction indicator.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> LivingArrangementTypeDensAsiSatisfaction
        {
            get { return _livingArrangementTypeDensAsiSatisfaction; }
            set { ApplyPropertyChange ( ref _livingArrangementTypeDensAsiSatisfaction, () => LivingArrangementTypeDensAsiSatisfaction, value ); }
        }

        /// <summary>
        /// Question Number: F6
        /// </summary>
        /// <value>The living arrangement type satisfaction indicator note.</value>
        public string LivingArrangementTypeDensAsiSatisfactionNote
        {
            get { return _livingArrangementTypeDensAsiSatisfactionNote; }
            set
            {
                ApplyPropertyChange (
                    ref _livingArrangementTypeDensAsiSatisfactionNote, () => LivingArrangementTypeDensAsiSatisfactionNote, value );
            }
        }

        /// <summary>
        /// Question Number: F7
        /// </summary>
        /// <value>The living with anyone who has alcohol problem indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> LivingWithAnyoneWhoHasAlcoholProblemIndicator
        {
            get { return _livingWithAnyoneWhoHasAlcoholProblemIndicator; }
            set { ApplyPropertyChange ( ref _livingWithAnyoneWhoHasAlcoholProblemIndicator, () => LivingWithAnyoneWhoHasAlcoholProblemIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F7
        /// </summary>
        /// <value>The living with anyone who has alcohol problem indicator note.</value>
        public string LivingWithAnyoneWhoHasAlcoholProblemIndicatorNote
        {
            get { return _livingWithAnyoneWhoHasAlcoholProblemIndicatorNote; }
            set
            {
                ApplyPropertyChange (
                    ref _livingWithAnyoneWhoHasAlcoholProblemIndicatorNote, () => LivingWithAnyoneWhoHasAlcoholProblemIndicatorNote, value );
            }
        }

        /// <summary>
        /// Question Number: F8
        /// </summary>
        /// <value>The living with anyone who uses non prescribed drugs indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> LivingWithAnyoneWhoUsesNonPrescribedDrugsIndicator
        {
            get { return _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator, () => LivingWithAnyoneWhoUsesNonPrescribedDrugsIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: F8
        /// </summary>
        /// <value>The living with anyone who uses non prescribed drugs indicator note.</value>
        public string LivingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote
        {
            get { return _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote; }
            set
            {
                ApplyPropertyChange (
                    ref _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote, () => LivingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote, value );
            }
        }

        /// <summary>
        /// Question Number: F3
        /// </summary>
        /// <value>The marital status satisfaction indicator.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> MaritalStatusDensAsiSatisfaction
        {
            get { return _maritalStatusDensAsiSatisfaction; }
            set { ApplyPropertyChange ( ref _maritalStatusDensAsiSatisfaction, () => MaritalStatusDensAsiSatisfaction, value ); }
        }

        /// <summary>
        /// Question Number: F3
        /// </summary>
        /// <value>The marital status satisfaction indicator note.</value>
        public string MaritalStatusDensAsiSatisfactionNote
        {
            get { return _maritalStatusDensAsiSatisfactionNote; }
            set { ApplyPropertyChange ( ref _maritalStatusDensAsiSatisfactionNote, () => MaritalStatusDensAsiSatisfactionNote, value ); }
        }

        /// <summary>
        /// Question Number: F115
        /// </summary>
        /// <value>The not owned house in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> NotOwnedHouseInLastThirtyDaysDayCount
        {
            get { return _notOwnedHouseInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _notOwnedHouseInLastThirtyDaysDayCount, () => NotOwnedHouseInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: F115
        /// </summary>
        /// <value>The not owned house in last thirty days day count note.</value>
        public string NotOwnedHouseInLastThirtyDaysDayCountNote
        {
            get { return _notOwnedHouseInLastThirtyDaysDayCountNote; }
            set { ApplyPropertyChange ( ref _notOwnedHouseInLastThirtyDaysDayCountNote, () => NotOwnedHouseInLastThirtyDaysDayCountNote, value ); }
        }

        /// <summary>
        /// Question Number: F4
        /// </summary>
        /// <value>The type of the past three years dens asi living arrangement.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> PastThreeYearsDensAsiLivingArrangementType
        {
            get { return _pastThreeYearsDensAsiLivingArrangementType; }
            set { ApplyPropertyChange ( ref _pastThreeYearsDensAsiLivingArrangementType, () => PastThreeYearsDensAsiLivingArrangementType, value ); }
        }

        /// <summary>
        /// Question Number: F4
        /// </summary>
        /// <value>The past three years dens asi living arrangement type note.</value>
        public string PastThreeYearsDensAsiLivingArrangementTypeNote
        {
            get { return _pastThreeYearsDensAsiLivingArrangementTypeNote; }
            set
            {
                ApplyPropertyChange (
                    ref _pastThreeYearsDensAsiLivingArrangementTypeNote, () => PastThreeYearsDensAsiLivingArrangementTypeNote, value );
            }
        }

        /// <summary>
        /// Question Number: F36
        /// </summary>
        /// <value>The patient family social counseling dens asi interviewer rating.</value>
        public LookupValueDto PatientFamilySocialCounselingDensAsiInterviewerRating
        {
            get { return _patientFamilySocialCounselingDensAsiInterviewerRating; }
            set
            {
                ApplyPropertyChange (
                    ref _patientFamilySocialCounselingDensAsiInterviewerRating, () => PatientFamilySocialCounselingDensAsiInterviewerRating, value );
            }
        }

        /// <summary>
        /// Question Number: F36
        /// </summary>
        /// <value>The patient family social counseling dens asi interviewer rating note.</value>
        public string PatientFamilySocialCounselingDensAsiInterviewerRatingNote
        {
            get { return _patientFamilySocialCounselingDensAsiInterviewerRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _patientFamilySocialCounselingDensAsiInterviewerRatingNote,
                    () => PatientFamilySocialCounselingDensAsiInterviewerRatingNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: F20
        /// </summary>
        /// <value>The problems brother sister in last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsBrotherSisterInLastThirtyDaysIndicator
        {
            get { return _problemsBrotherSisterInLastThirtyDaysIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _problemsBrotherSisterInLastThirtyDaysIndicator, () => ProblemsBrotherSisterInLastThirtyDaysIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: F20
        /// </summary>
        /// <value>The problems brother sister in lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsBrotherSisterInLifetimeIndicator
        {
            get { return _problemsBrotherSisterInLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _problemsBrotherSisterInLifetimeIndicator, () => ProblemsBrotherSisterInLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F20
        /// </summary>
        /// <value>The problems brother sister note.</value>
        public string ProblemsBrotherSisterNote
        {
            get { return _problemsBrotherSisterNote; }
            set { ApplyPropertyChange ( ref _problemsBrotherSisterNote, () => ProblemsBrotherSisterNote, value ); }
        }

        /// <summary>
        /// Question Number: F22
        /// </summary>
        /// <value>The problems children in last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsChildrenInLastThirtyDaysIndicator
        {
            get { return _problemsChildrenInLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _problemsChildrenInLastThirtyDaysIndicator, () => ProblemsChildrenInLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F22
        /// </summary>
        /// <value>The problems children in lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsChildrenInLifetimeIndicator
        {
            get { return _problemsChildrenInLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _problemsChildrenInLifetimeIndicator, () => ProblemsChildrenInLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F22
        /// </summary>
        /// <value>The problems children note.</value>
        public string ProblemsChildrenNote
        {
            get { return _problemsChildrenNote; }
            set { ApplyPropertyChange ( ref _problemsChildrenNote, () => ProblemsChildrenNote, value ); }
        }

        /// <summary>
        /// Question Number: F24
        /// </summary>
        /// <value>The problems close friends in last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsCloseFriendsInLastThirtyDaysIndicator
        {
            get { return _problemsCloseFriendsInLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _problemsCloseFriendsInLastThirtyDaysIndicator, () => ProblemsCloseFriendsInLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F24
        /// </summary>
        /// <value>The problems close friends in lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsCloseFriendsInLifetimeIndicator
        {
            get { return _problemsCloseFriendsInLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _problemsCloseFriendsInLifetimeIndicator, () => ProblemsCloseFriendsInLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F24
        /// </summary>
        /// <value>The problems close friends note.</value>
        public string ProblemsCloseFriendsNote
        {
            get { return _problemsCloseFriendsNote; }
            set { ApplyPropertyChange ( ref _problemsCloseFriendsNote, () => ProblemsCloseFriendsNote, value ); }
        }

        /// <summary>
        /// Question Number: F26
        /// </summary>
        /// <value>The problems coworkers in last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsCoworkersInLastThirtyDaysIndicator
        {
            get { return _problemsCoworkersInLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _problemsCoworkersInLastThirtyDaysIndicator, () => ProblemsCoworkersInLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F26
        /// </summary>
        /// <value>The problems coworkers in lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsCoworkersInLifetimeIndicator
        {
            get { return _problemsCoworkersInLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _problemsCoworkersInLifetimeIndicator, () => ProblemsCoworkersInLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F26
        /// </summary>
        /// <value>The problems coworkers note.</value>
        public string ProblemsCoworkersNote
        {
            get { return _problemsCoworkersNote; }
            set { ApplyPropertyChange ( ref _problemsCoworkersNote, () => ProblemsCoworkersNote, value ); }
        }

        /// <summary>
        /// Question Number: F19
        /// </summary>
        /// <value>The problems father in last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsFatherInLastThirtyDaysIndicator
        {
            get { return _problemsFatherInLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _problemsFatherInLastThirtyDaysIndicator, () => ProblemsFatherInLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F19
        /// </summary>
        /// <value>The problems father in lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsFatherInLifetimeIndicator
        {
            get { return _problemsFatherInLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _problemsFatherInLifetimeIndicator, () => ProblemsFatherInLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F19
        /// </summary>
        /// <value>The problems father note.</value>
        public string ProblemsFatherNote
        {
            get { return _problemsFatherNote; }
            set { ApplyPropertyChange ( ref _problemsFatherNote, () => ProblemsFatherNote, value ); }
        }

        /// <summary>
        /// Question Number: F18
        /// </summary>
        /// <value>The problems mother in last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsMotherInLastThirtyDaysIndicator
        {
            get { return _problemsMotherInLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _problemsMotherInLastThirtyDaysIndicator, () => ProblemsMotherInLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F18
        /// </summary>
        /// <value>The problems mother in lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsMotherInLifetimeIndicator
        {
            get { return _problemsMotherInLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _problemsMotherInLifetimeIndicator, () => ProblemsMotherInLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F18
        /// </summary>
        /// <value>The problems mother note.</value>
        public string ProblemsMotherNote
        {
            get { return _problemsMotherNote; }
            set { ApplyPropertyChange ( ref _problemsMotherNote, () => ProblemsMotherNote, value ); }
        }

        /// <summary>
        /// Question Number: F25
        /// </summary>
        /// <value>The problems neighbors in last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsNeighborsInLastThirtyDaysIndicator
        {
            get { return _problemsNeighborsInLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _problemsNeighborsInLastThirtyDaysIndicator, () => ProblemsNeighborsInLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F25
        /// </summary>
        /// <value>The problems neighbors in lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsNeighborsInLifetimeIndicator
        {
            get { return _problemsNeighborsInLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _problemsNeighborsInLifetimeIndicator, () => ProblemsNeighborsInLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F25
        /// </summary>
        /// <value>The problems neighbors note.</value>
        public string ProblemsNeighborsNote
        {
            get { return _problemsNeighborsNote; }
            set { ApplyPropertyChange ( ref _problemsNeighborsNote, () => ProblemsNeighborsNote, value ); }
        }

        /// <summary>
        /// Question Number: F23
        /// </summary>
        /// <value>The problems other significant family description.</value>
        public string ProblemsOtherSignificantFamilyDescription
        {
            get { return _problemsOtherSignificantFamilyDescription; }
            set { ApplyPropertyChange ( ref _problemsOtherSignificantFamilyDescription, () => ProblemsOtherSignificantFamilyDescription, value ); }
        }

        /// <summary>
        /// Question Number: F23
        /// </summary>
        /// <value>The problems other significant family in last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsOtherSignificantFamilyInLastThirtyDaysIndicator
        {
            get { return _problemsOtherSignificantFamilyInLastThirtyDaysIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _problemsOtherSignificantFamilyInLastThirtyDaysIndicator, () => ProblemsOtherSignificantFamilyInLastThirtyDaysIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: F23
        /// </summary>
        /// <value>The problems other significant family in lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsOtherSignificantFamilyInLifetimeIndicator
        {
            get { return _problemsOtherSignificantFamilyInLifetimeIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _problemsOtherSignificantFamilyInLifetimeIndicator, () => ProblemsOtherSignificantFamilyInLifetimeIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: F23
        /// </summary>
        /// <value>The problems other significant family note.</value>
        public string ProblemsOtherSignificantFamilyNote
        {
            get { return _problemsOtherSignificantFamilyNote; }
            set { ApplyPropertyChange ( ref _problemsOtherSignificantFamilyNote, () => ProblemsOtherSignificantFamilyNote, value ); }
        }

        /// <summary>
        /// Question Number: F21
        /// </summary>
        /// <value>The problems sexual partner in last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsSexualPartnerInLastThirtyDaysIndicator
        {
            get { return _problemsSexualPartnerInLastThirtyDaysIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _problemsSexualPartnerInLastThirtyDaysIndicator, () => ProblemsSexualPartnerInLastThirtyDaysIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: F21
        /// </summary>
        /// <value>The problems sexual partner in lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProblemsSexualPartnerInLifetimeIndicator
        {
            get { return _problemsSexualPartnerInLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _problemsSexualPartnerInLifetimeIndicator, () => ProblemsSexualPartnerInLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: F21
        /// </summary>
        /// <value>The problems sexual partner note.</value>
        public string ProblemsSexualPartnerNote
        {
            get { return _problemsSexualPartnerNote; }
            set { ApplyPropertyChange ( ref _problemsSexualPartnerNote, () => ProblemsSexualPartnerNote, value ); }
        }

        /// <summary>
        /// Question Number: F14
        /// </summary>
        /// <value>The reciprocal relationship brother sister indicator.</value>
        public LookupValueDto BrotherSisterDensAsiHasRelationshipOption
        {
            get { return _brotherSisterDensAsiHasRelationshipOption; }
            set { ApplyPropertyChange ( ref _brotherSisterDensAsiHasRelationshipOption, () => BrotherSisterDensAsiHasRelationshipOption, value ); }
        }

        /// <summary>
        /// Question Number: F14
        /// </summary>
        /// <value>The reciprocal relationship brother sister indicator note.</value>
        public string BrotherSisterDensAsiHasRelationshipOptionNote
        {
            get { return _brotherSisterDensAsiHasRelationshipOptionNote; }
            set
            {
                ApplyPropertyChange (
                    ref _brotherSisterDensAsiHasRelationshipOptionNote, () => BrotherSisterDensAsiHasRelationshipOptionNote, value );
            }
        }

        /// <summary>
        /// Question Number: F16
        /// </summary>
        /// <value>The reciprocal relationship children indicator.</value>
        public LookupValueDto ChildrenDensAsiHasRelationshipOption
        {
            get { return _childrenDensAsiHasRelationshipOption; }
            set { ApplyPropertyChange ( ref _childrenDensAsiHasRelationshipOption, () => ChildrenDensAsiHasRelationshipOption, value ); }
        }

        /// <summary>
        /// Question Number: F16
        /// </summary>
        /// <value>The reciprocal relationship children indicator note.</value>
        public string ChildrenDensAsiHasRelationshipOptionNote
        {
            get { return _childrenDensAsiHasRelationshipOptionNote; }
            set { ApplyPropertyChange ( ref _childrenDensAsiHasRelationshipOptionNote, () => ChildrenDensAsiHasRelationshipOptionNote, value ); }
        }

        /// <summary>
        /// Question Number: F13
        /// </summary>
        /// <value>The reciprocal relationship father indicator.</value>
        public LookupValueDto FatherDensAsiHasParentalRelationshipOption
        {
            get { return _fatherDensAsiHasParentalRelationshipOption; }
            set { ApplyPropertyChange ( ref _fatherDensAsiHasParentalRelationshipOption, () => FatherDensAsiHasParentalRelationshipOption, value ); }
        }

        /// <summary>
        /// Question Number: F13
        /// </summary>
        /// <value>The reciprocal relationship father indicator note.</value>
        public string FatherDensAsiHasParentalRelationshipOptionNote
        {
            get { return _fatherDensAsiHasParentalRelationshipOptionNote; }
            set { ApplyPropertyChange ( ref _fatherDensAsiHasParentalRelationshipOptionNote, () => FatherDensAsiHasParentalRelationshipOptionNote, value ); }
        }

        /// <summary>
        /// Question Number: F17
        /// </summary>
        /// <value>The reciprocal relationship friends indicator.</value>
        public LookupValueDto FriendsDensAsiHasRelationshipOption
        {
            get { return _friendsDensAsiHasRelationshipOption; }
            set { ApplyPropertyChange ( ref _friendsDensAsiHasRelationshipOption, () => FriendsDensAsiHasRelationshipOption, value ); }
        }

        /// <summary>
        /// Question Number: F17
        /// </summary>
        /// <value>The reciprocal relationship friends indicator note.</value>
        public string FriendsDensAsiHasRelationshipOptionNote
        {
            get { return _friendsDensAsiHasRelationshipOptionNote; }
            set { ApplyPropertyChange ( ref _friendsDensAsiHasRelationshipOptionNote, () => FriendsDensAsiHasRelationshipOptionNote, value ); }
        }

        /// <summary>
        /// Question Number: F12
        /// </summary>
        /// <value>The reciprocal relationship mother indicator.</value>
        public LookupValueDto MotherDensAsiHasParentalRelationshipOption
        {
            get { return _motherDensAsiHasParentalRelationshipOption; }
            set { ApplyPropertyChange ( ref _motherDensAsiHasParentalRelationshipOption, () => MotherDensAsiHasParentalRelationshipOption, value ); }
        }

        /// <summary>
        /// Question Number: F12
        /// </summary>
        /// <value>The reciprocal relationship mother indicator note.</value>
        public string MotherDensAsiHasParentalRelationshipOptionNote
        {
            get { return _motherDensAsiHasParentalRelationshipOptionNote; }
            set { ApplyPropertyChange ( ref _motherDensAsiHasParentalRelationshipOptionNote, () => MotherDensAsiHasParentalRelationshipOptionNote, value ); }
        }

        /// <summary>
        /// Question Number: F15
        /// </summary>
        /// <value>The reciprocal relationship sexual partner indicator.</value>
        public LookupValueDto SexualPartnerDensAsiHasRelationshipOption
        {
            get { return _sexualPartnerDensAsiHasRelationshipOption; }
            set { ApplyPropertyChange ( ref _sexualPartnerDensAsiHasRelationshipOption, () => SexualPartnerDensAsiHasRelationshipOption, value ); }
        }

        /// <summary>
        /// Question Number: F15
        /// </summary>
        /// <value>The reciprocal relationship sexual partner indicator note.</value>
        public string SexualPartnerDensAsiHasRelationshipOptionNote
        {
            get { return _sexualPartnerDensAsiHasRelationshipOptionNote; }
            set
            {
                ApplyPropertyChange (
                    ref _sexualPartnerDensAsiHasRelationshipOptionNote, () => SexualPartnerDensAsiHasRelationshipOptionNote, value );
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
        /// Question Number: F30
        /// </summary>
        /// <value>The serious family conflicts in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> SeriousFamilyConflictsInLastThirtyDaysDayCount
        {
            get { return _seriousFamilyConflictsInLastThirtyDaysDayCount; }
            set
            {
                ApplyPropertyChange (
                    ref _seriousFamilyConflictsInLastThirtyDaysDayCount, () => SeriousFamilyConflictsInLastThirtyDaysDayCount, value );
            }
        }

        /// <summary>
        /// Question Number: F30
        /// </summary>
        /// <value>The serious family conflicts in last thirty days day count note.</value>
        public string SeriousFamilyConflictsInLastThirtyDaysDayCountNote
        {
            get { return _seriousFamilyConflictsInLastThirtyDaysDayCountNote; }
            set
            {
                ApplyPropertyChange (
                    ref _seriousFamilyConflictsInLastThirtyDaysDayCountNote, () => SeriousFamilyConflictsInLastThirtyDaysDayCountNote, value );
            }
        }

        /// <summary>
        /// Question Number: F114
        /// </summary>
        /// <value>The shelter in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> ShelterInLastThirtyDaysDayCount
        {
            get { return _shelterInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _shelterInLastThirtyDaysDayCount, () => ShelterInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: F114
        /// </summary>
        /// <value>The shelter in last thirty days day count note.</value>
        public string ShelterInLastThirtyDaysDayCountNote
        {
            get { return _shelterInLastThirtyDaysDayCountNote; }
            set { ApplyPropertyChange ( ref _shelterInLastThirtyDaysDayCountNote, () => ShelterInLastThirtyDaysDayCountNote, value ); }
        }

        /// <summary>
        /// Question Number: F32
        /// </summary>
        /// <value>The troubled by family problems dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> TroubledByFamilyProblemsDensAsiPatientRating
        {
            get { return _troubledByFamilyProblemsDensAsiPatientRating; }
            set { ApplyPropertyChange ( ref _troubledByFamilyProblemsDensAsiPatientRating, () => TroubledByFamilyProblemsDensAsiPatientRating, value ); }
        }

        /// <summary>
        /// Question Number: F32
        /// </summary>
        /// <value>The troubled by family problems dens asi patient rating note.</value>
        public string TroubledByFamilyProblemsDensAsiPatientRatingNote
        {
            get { return _troubledByFamilyProblemsDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _troubledByFamilyProblemsDensAsiPatientRatingNote, () => TroubledByFamilyProblemsDensAsiPatientRatingNote, value );
            }
        }

        /// <summary>
        /// Question Number: F33
        /// </summary>
        /// <value>The troubled by social problems dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> TroubledBySocialProblemsDensAsiPatientRating
        {
            get { return _troubledBySocialProblemsDensAsiPatientRating; }
            set { ApplyPropertyChange ( ref _troubledBySocialProblemsDensAsiPatientRating, () => TroubledBySocialProblemsDensAsiPatientRating, value ); }
        }

        /// <summary>
        /// Question Number: F33
        /// </summary>
        /// <value>The troubled by social problems dens asi patient rating note.</value>
        public string TroubledBySocialProblemsDensAsiPatientRatingNote
        {
            get { return _troubledBySocialProblemsDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _troubledBySocialProblemsDensAsiPatientRatingNote, () => TroubledBySocialProblemsDensAsiPatientRatingNote, value );
            }
        }

        /// <summary>
        /// Question Number: F5
        /// </summary>
        /// <value>The years and months in living arrangement type time span.</value>
        public DensAsiNonResponseTypeDto<TimeSpan?> YearsAndMonthsInLivingArrangementTypeTimeSpan
        {
            get { return _yearsAndMonthsInLivingArrangementTypeTimeSpan; }
            set { ApplyPropertyChange ( ref _yearsAndMonthsInLivingArrangementTypeTimeSpan, () => YearsAndMonthsInLivingArrangementTypeTimeSpan, value ); }
        }

        /// <summary>
        /// Question Number: F5
        /// </summary>
        /// <value>The years and months in living arrangement type time span note.</value>
        public string YearsAndMonthsInLivingArrangementTypeTimeSpanNote
        {
            get { return _yearsAndMonthsInLivingArrangementTypeTimeSpanNote; }
            set
            {
                ApplyPropertyChange (
                    ref _yearsAndMonthsInLivingArrangementTypeTimeSpanNote, () => YearsAndMonthsInLivingArrangementTypeTimeSpanNote, value );
            }
        }

        /// <summary>
        /// Question Number: F2
        /// </summary>
        /// <value>The years and months with marital status time span.</value>
        public DensAsiNonResponseTypeDto<TimeSpan?> YearsAndMonthsWithMaritalStatusTimeSpan
        {
            get { return _yearsAndMonthsWithMaritalStatusTimeSpan; }
            set { ApplyPropertyChange ( ref _yearsAndMonthsWithMaritalStatusTimeSpan, () => YearsAndMonthsWithMaritalStatusTimeSpan, value ); }
        }

        /// <summary>
        /// Question Number: F2
        /// </summary>
        /// <value>The years and months with marital status time span note.</value>
        public string YearsAndMonthsWithMaritalStatusTimeSpanNote
        {
            get { return _yearsAndMonthsWithMaritalStatusTimeSpanNote; }
            set { ApplyPropertyChange ( ref _yearsAndMonthsWithMaritalStatusTimeSpanNote, () => YearsAndMonthsWithMaritalStatusTimeSpanNote, value ); }
        }

        #endregion
    }
}
