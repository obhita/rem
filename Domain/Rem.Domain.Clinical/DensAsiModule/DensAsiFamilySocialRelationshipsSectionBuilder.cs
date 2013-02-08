using System;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// DensAsiFamilySocialRelationshipsSectionBuilder provides a fluent interface for creating a FamilySocialRelationships section.
    /// </summary>
    public class DensAsiFamilySocialRelationshipsSectionBuilder
    {
        private DensAsiNonResponseType<DensAsiMaritalStatus> _densAsiMaritalStatus;
        private string _densAsiMaritalStatusNote;
        private DensAsiNonResponseType<TimeSpan?> _yearsAndMonthsWithMaritalStatusTimeSpan;
        private string _yearsAndMonthsWithMaritalStatusTimeSpanNote;
        private DensAsiNonResponseType<DensAsiSatisfaction> _maritalStatusSatisfactionIndicator;
        private string _maritalStatusSatisfactionIndicatorNote;
        private DensAsiNonResponseType<DensAsiLivingArrangementType> _pastThreeYearsDensAsiLivingArrangementType;
        private string _pastThreeYearsDensAsiLivingArrangementTypeNote;
        private DensAsiNonResponseType<TimeSpan?> _yearsAndMonthsInLivingArrangementTypeTimeSpan;
        private string _yearsAndMonthsInLivingArrangementTypeTimeSpanNote;
        private DensAsiNonResponseType<DensAsiSatisfaction> _livingArrangementTypeSatisfaction;
        private string _livingArrangementTypeSatisfactionNote;
        private DensAsiNonResponseType<bool?> _livingWithAnyoneWhoHasAlcoholProblemIndicator;
        private string _livingWithAnyoneWhoHasAlcoholProblemIndicatorNote;
        private DensAsiNonResponseType<bool?> _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator;
        private string _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote;
        private DensAsiNonResponseType<DensAsiFreeTimeSpentType> _densAsiFreeTimeSpentType;
        private string _densAsiFreeTimeSpentTypeNote;
        private DensAsiNonResponseType<DensAsiSatisfaction> _freeTimeSpentTypeSatisfaction;
        private string _freeTimeSpentTypeSatisfactionNote;
        private DensAsiNonResponseType<int?> _closeFriendsCount;
        private string _closeFriendsCountNote;
        private DensAsiHasParentalRelationshipOption _reciprocalRelationshipMother;
        private string _reciprocalRelationshipMotherNote;
        private DensAsiHasParentalRelationshipOption _reciprocalRelationshipFather;
        private string _reciprocalRelationshipFatherNote;
        private DensAsiHasRelationshipOption _reciprocalRelationshipBrotherSister;
        private string _reciprocalRelationshipBrotherSisterNote;
        private DensAsiHasRelationshipOption _reciprocalRelationshipSexualPartner;
        private string _reciprocalRelationshipSexualPartnerNote;
        private DensAsiHasRelationshipOption _reciprocalRelationshipChildren;
        private string _reciprocalRelationshipChildrenNote;
        private DensAsiHasRelationshipOption _reciprocalRelationshipFriends;
        private string _reciprocalRelationshipFriendsNote;
        private DensAsiNonResponseType<bool?> _problemsMotherInLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _problemsMotherInLifetimeIndicator;
        private string _problemsMotherNote;
        private DensAsiNonResponseType<bool?> _problemsFatherInLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _problemsFatherInLifetimeIndicator;
        private string _problemsFatherNote;
        private DensAsiNonResponseType<bool?> _problemsBrotherSisterInLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _problemsBrotherSisterInLifetimeIndicator;
        private string _problemsBrotherSisterNote;
        private DensAsiNonResponseType<bool?> _problemsSexualPartnerInLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _problemsSexualPartnerInLifetimeIndicator;
        private string _problemsSexualPartnerNote;
        private DensAsiNonResponseType<bool?> _problemsChildrenInLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _problemsChildrenInLifetimeIndicator;
        private string _problemsChildrenNote;
        private DensAsiNonResponseType<bool?> _problemsOtherSignificantFamilyInLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _problemsOtherSignificantFamilyInLifetimeIndicator;
        private string _problemsOtherSignificantFamilyDescription;
        private string _problemsOtherSignificantFamilyNote;
        private DensAsiNonResponseType<bool?> _problemsCloseFriendsInLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _problemsCloseFriendsInLifetimeIndicator;
        private string _problemsCloseFriendsNote;
        private DensAsiNonResponseType<bool?> _problemsNeighborsInLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _problemsNeighborsInLifetimeIndicator;
        private string _problemsNeighborsNote;
        private DensAsiNonResponseType<bool?> _problemsCoworkersInLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _problemsCoworkersInLifetimeIndicator;
        private string _problemsCoworkersNote;
        private DensAsiNonResponseType<bool?> _abusedEmotionallyInLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _abusedEmotionallyInLifetimeIndicator;
        private string _abusedEmotionallyNote;
        private DensAsiNonResponseType<bool?> _abusedPhysicallyInLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _abusedPhysicallyInLifetimeIndicator;
        private string _abusedPhysicallyNote;
        private DensAsiNonResponseType<bool?> _abusedSexuallyInLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _abusedSexuallyInLifetimeIndicator;
        private string _abusedSexuallyNote;
        private DensAsiNonResponseType<int?> _seriousFamilyConflictsInLastThirtyDaysDayCount;
        private string _seriousFamilyConflictsInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _troubledByFamilyProblemsDensAsiPatientRating;
        private string _troubledByFamilyProblemsDensAsiPatientRatingNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _importanceOfFamilyProblemCounselingDensAsiPatientRating;
        private string _importanceOfFamilyProblemCounselingDensAsiPatientRatingNote;
        private DensAsiNonResponseType<int?> _conflictsWithOthersInLastThirtyDaysDayCount;
        private string _conflictsWithOthersInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _troubledBySocialProblemsDensAsiPatientRating;
        private string _troubledBySocialProblemsDensAsiPatientRatingNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _importanceOfSocialProblemCounselingDensAsiPatientRating;
        private string _importanceOfSocialProblemCounselingDensAsiPatientRatingNote;
        private DensAsiInterviewerRating _patientFamilySocialCounselingDensAsiInterviewerRating;
        private string _patientFamilySocialCounselingDensAsiInterviewerRatingNote;
        private bool? _confidenceDistortedByPatientMisrepresentationIndicator;
        private string _confidenceDistortedByPatientMisrepresentationIndicatorNote;
        private bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private string _sectionNote;
        private DensAsiNonResponseType<int?> _homelessInLastThirtyDaysDayCount;
        private string _homelessInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseType<int?> _shelterInLastThirtyDaysDayCount;
        private string _shelterInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseType<int?> _notOwnedHouseInLastThirtyDaysDayCount;
        private string _notOwnedHouseInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseType<int?> _hospitalJailInLastThirtyDaysDayCount;
        private string _hospitalJailInLastThirtyDaysDayCountNote;


        /// <summary>
        /// Assigns the DensAsi marital status.
        /// </summary>
        /// <param name="densAsiMaritalStatus">The DensAsi marital status.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithDensAsiMaritalStatus(DensAsiNonResponseType<DensAsiMaritalStatus> densAsiMaritalStatus)
        {
            _densAsiMaritalStatus = densAsiMaritalStatus;
            return this;
        }

        /// <summary>
        /// Assigns the DensAsi marital status note.
        /// </summary>
        /// <param name="densAsiMaritalStatusNote">The DensAsi marital status note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithDensAsiMaritalStatusNote(string densAsiMaritalStatusNote)
        {
            _densAsiMaritalStatusNote = densAsiMaritalStatusNote;
            return this;
        }

        /// <summary>
        /// Assigns the years and months with marital status time span.
        /// </summary>
        /// <param name="yearsAndMonthsWithMaritalStatusTimeSpan">The years and months with marital status time span.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithYearsAndMonthsWithMaritalStatusTimeSpan(DensAsiNonResponseType<TimeSpan?> yearsAndMonthsWithMaritalStatusTimeSpan)
        {
            _yearsAndMonthsWithMaritalStatusTimeSpan = yearsAndMonthsWithMaritalStatusTimeSpan;
            return this;
        }

        /// <summary>
        /// Assigns the years and months with marital status time span note.
        /// </summary>
        /// <param name="yearsAndMonthsWithMaritalStatusTimeSpanNote">The years and months with marital status time span note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithYearsAndMonthsWithMaritalStatusTimeSpanNote(string yearsAndMonthsWithMaritalStatusTimeSpanNote)
        {
            _yearsAndMonthsWithMaritalStatusTimeSpanNote = yearsAndMonthsWithMaritalStatusTimeSpanNote;
            return this;
        }

        /// <summary>
        /// Assigns the marital status satisfaction indicator.
        /// </summary>
        /// <param name="maritalStatusSatisfactionIndicator">The marital status satisfaction indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithMaritalStatusSatisfactionIndicator(DensAsiNonResponseType<DensAsiSatisfaction> maritalStatusSatisfactionIndicator)
        {
            _maritalStatusSatisfactionIndicator = maritalStatusSatisfactionIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the marital status satisfaction indicator note.
        /// </summary>
        /// <param name="maritalStatusSatisfactionIndicatorNote">The marital status satisfaction indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithMaritalStatusSatisfactionIndicatorNote(string maritalStatusSatisfactionIndicatorNote)
        {
            _maritalStatusSatisfactionIndicatorNote = maritalStatusSatisfactionIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the type of the past three years DensAsi living arrangement.
        /// </summary>
        /// <param name="pastThreeYearsDensAsiLivingArrangementType">Type of the past three years DensAsi living arrangement.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithPastThreeYearsDensAsiLivingArrangementType(DensAsiNonResponseType<DensAsiLivingArrangementType> pastThreeYearsDensAsiLivingArrangementType)
        {
            _pastThreeYearsDensAsiLivingArrangementType = pastThreeYearsDensAsiLivingArrangementType;
            return this;
        }

        /// <summary>
        /// Assigns the past three years DensAsi living arrangement type note.
        /// </summary>
        /// <param name="pastThreeYearsDensAsiLivingArrangementTypeNote">The past three years DensAsi living arrangement type note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithPastThreeYearsDensAsiLivingArrangementTypeNote(string pastThreeYearsDensAsiLivingArrangementTypeNote)
        {
            _pastThreeYearsDensAsiLivingArrangementTypeNote = pastThreeYearsDensAsiLivingArrangementTypeNote;
            return this;
        }

        /// <summary>
        /// Assigns the years and months in living arrangement type time span.
        /// </summary>
        /// <param name="yearsAndMonthsInLivingArrangementTypeTimeSpan">The years and months in living arrangement type time span.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithYearsAndMonthsInLivingArrangementTypeTimeSpan(DensAsiNonResponseType<TimeSpan?> yearsAndMonthsInLivingArrangementTypeTimeSpan)
        {
            _yearsAndMonthsInLivingArrangementTypeTimeSpan = yearsAndMonthsInLivingArrangementTypeTimeSpan;
            return this;
        }

        /// <summary>
        /// Assigns the years and months in living arrangement type time span note.
        /// </summary>
        /// <param name="yearsAndMonthsInLivingArrangementTypeTimeSpanNote">The years and months in living arrangement type time span note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithYearsAndMonthsInLivingArrangementTypeTimeSpanNote(string yearsAndMonthsInLivingArrangementTypeTimeSpanNote)
        {
            _yearsAndMonthsInLivingArrangementTypeTimeSpanNote = yearsAndMonthsInLivingArrangementTypeTimeSpanNote;
            return this;
        }

        /// <summary>
        /// Assigns the living arrangement type satisfaction indicator.
        /// </summary>
        /// <param name="livingArrangementTypeSatisfaction">The living arrangement type satisfaction indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithLivingArrangementTypeSatisfactionIndicator(DensAsiNonResponseType<DensAsiSatisfaction> livingArrangementTypeSatisfaction)
        {
            _livingArrangementTypeSatisfaction = livingArrangementTypeSatisfaction;
            return this;
        }

        /// <summary>
        /// Assigns the living arrangement type satisfaction indicator note.
        /// </summary>
        /// <param name="livingArrangementTypeSatisfactionIndicatorNote">The living arrangement type satisfaction indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithLivingArrangementTypeSatisfactionIndicatorNote(string livingArrangementTypeSatisfactionIndicatorNote)
        {
            _livingArrangementTypeSatisfactionNote = livingArrangementTypeSatisfactionIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the living with anyone who has alcohol problem indicator.
        /// </summary>
        /// <param name="livingWithAnyoneWhoHasAlcoholProblemIndicator">The living with anyone who has alcohol problem indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithLivingWithAnyoneWhoHasAlcoholProblemIndicator(DensAsiNonResponseType<bool?> livingWithAnyoneWhoHasAlcoholProblemIndicator)
        {
            _livingWithAnyoneWhoHasAlcoholProblemIndicator = livingWithAnyoneWhoHasAlcoholProblemIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the living with anyone who has alcohol problem indicator note.
        /// </summary>
        /// <param name="livingWithAnyoneWhoHasAlcoholProblemIndicatorNote">The living with anyone who has alcohol problem indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithLivingWithAnyoneWhoHasAlcoholProblemIndicatorNote(string livingWithAnyoneWhoHasAlcoholProblemIndicatorNote)
        {
            _livingWithAnyoneWhoHasAlcoholProblemIndicatorNote = livingWithAnyoneWhoHasAlcoholProblemIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the living with anyone who uses non prescribed drugs indicator.
        /// </summary>
        /// <param name="livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator">The living with anyone who uses non prescribed drugs indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithLivingWithAnyoneWhoUsesNonPrescribedDrugsIndicator(DensAsiNonResponseType<bool?> livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator)
        {
            _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator = livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the living with anyone who uses non prescribed drugs indicator note.
        /// </summary>
        /// <param name="livingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote">The living with anyone who uses non prescribed drugs indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithLivingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote(string livingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote)
        {
            _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote = livingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the type of the DensAsi free time spent.
        /// </summary>
        /// <param name="densAsiFreeTimeSpentType">Type of the DensAsi free time spent.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithDensAsiFreeTimeSpentType(DensAsiNonResponseType<DensAsiFreeTimeSpentType> densAsiFreeTimeSpentType)
        {
            _densAsiFreeTimeSpentType = densAsiFreeTimeSpentType;
            return this;
        }

        /// <summary>
        /// Assigns the DensAsi free time spent type note.
        /// </summary>
        /// <param name="densAsiFreeTimeSpentTypeNote">The DensAsi free time spent type note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithDensAsiFreeTimeSpentTypeNote(string densAsiFreeTimeSpentTypeNote)
        {
            _densAsiFreeTimeSpentTypeNote = densAsiFreeTimeSpentTypeNote;
            return this;
        }

        /// <summary>
        /// Assigns the free time spent type satisfaction indicator.
        /// </summary>
        /// <param name="freeTimeSpentTypeSatisfaction">The free time spent type satisfaction indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithFreeTimeSpentTypeSatisfactionIndicator(DensAsiNonResponseType<DensAsiSatisfaction> freeTimeSpentTypeSatisfaction)
        {
            _freeTimeSpentTypeSatisfaction = freeTimeSpentTypeSatisfaction;
            return this;
        }

        /// <summary>
        /// Assigns the free time spent type satisfaction indicator note.
        /// </summary>
        /// <param name="freeTimeSpentTypeSatisfactionIndicatorNote">The free time spent type satisfaction indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithFreeTimeSpentTypeSatisfactionIndicatorNote(string freeTimeSpentTypeSatisfactionIndicatorNote)
        {
            _freeTimeSpentTypeSatisfactionNote = freeTimeSpentTypeSatisfactionIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the close friends count.
        /// </summary>
        /// <param name="closeFriendsCount">The close friends count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithCloseFriendsCount(DensAsiNonResponseType<int?> closeFriendsCount)
        {
            _closeFriendsCount = closeFriendsCount;
            return this;
        }

        /// <summary>
        /// Assigns the close friends count note.
        /// </summary>
        /// <param name="closeFriendsCountNote">The close friends count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithCloseFriendsCountNote(string closeFriendsCountNote)
        {
            _closeFriendsCountNote = closeFriendsCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the reciprocal relationship mother indicator.
        /// </summary>
        /// <param name="reciprocalRelationshipMother">The reciprocal relationship mother indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithReciprocalRelationshipMother(DensAsiHasParentalRelationshipOption reciprocalRelationshipMother)
        {
            _reciprocalRelationshipMother = reciprocalRelationshipMother;
            return this;
        }

        /// <summary>
        /// Assigns the reciprocal relationship mother indicator note.
        /// </summary>
        /// <param name="reciprocalRelationshipMotherNote">The reciprocal relationship mother indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithReciprocalRelationshipMotherNote(string reciprocalRelationshipMotherNote)
        {
            _reciprocalRelationshipMotherNote = reciprocalRelationshipMotherNote;
            return this;
        }

        /// <summary>
        /// Assigns the reciprocal relationship father indicator.
        /// </summary>
        /// <param name="reciprocalRelationshipFather">The reciprocal relationship father indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithReciprocalRelationshipFather(DensAsiHasParentalRelationshipOption reciprocalRelationshipFather)
        {
            _reciprocalRelationshipFather = reciprocalRelationshipFather;
            return this;
        }

        /// <summary>
        /// Assigns the reciprocal relationship father indicator note.
        /// </summary>
        /// <param name="reciprocalRelationshipFatherNote">The reciprocal relationship father indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithReciprocalRelationshipFatherNote(string reciprocalRelationshipFatherNote)
        {
            _reciprocalRelationshipFatherNote = reciprocalRelationshipFatherNote;
            return this;
        }

        /// <summary>
        /// Assigns the reciprocal relationship brother sister indicator.
        /// </summary>
        /// <param name="reciprocalRelationshipBrotherSister">The reciprocal relationship brother sister indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithReciprocalRelationshipBrotherSister(DensAsiHasRelationshipOption reciprocalRelationshipBrotherSister)
        {
            _reciprocalRelationshipBrotherSister = reciprocalRelationshipBrotherSister;
            return this;
        }

        /// <summary>
        /// Assigns the reciprocal relationship brother sister indicator note.
        /// </summary>
        /// <param name="reciprocalRelationshipBrotherSisterNote">The reciprocal relationship brother sister indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithReciprocalRelationshipBrotherSisterNote(string reciprocalRelationshipBrotherSisterNote)
        {
            _reciprocalRelationshipBrotherSisterNote = reciprocalRelationshipBrotherSisterNote;
            return this;
        }

        /// <summary>
        /// Assigns the reciprocal relationship sexual partner indicator.
        /// </summary>
        /// <param name="reciprocalRelationshipSexualPartner">The reciprocal relationship sexual partner indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithReciprocalRelationshipSexualPartner(DensAsiHasRelationshipOption reciprocalRelationshipSexualPartner)
        {
            _reciprocalRelationshipSexualPartner = reciprocalRelationshipSexualPartner;
            return this;
        }

        /// <summary>
        /// Assigns the reciprocal relationship sexual partner indicator note.
        /// </summary>
        /// <param name="reciprocalRelationshipSexualPartnerNote">The reciprocal relationship sexual partner indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithReciprocalRelationshipSexualPartnerNote(string reciprocalRelationshipSexualPartnerNote)
        {
            _reciprocalRelationshipSexualPartnerNote = reciprocalRelationshipSexualPartnerNote;
            return this;
        }

        /// <summary>
        /// Assigns the reciprocal relationship children indicator.
        /// </summary>
        /// <param name="reciprocalRelationshipChildren">The reciprocal relationship children indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithReciprocalRelationshipChildren(DensAsiHasRelationshipOption reciprocalRelationshipChildren)
        {
            _reciprocalRelationshipChildren = reciprocalRelationshipChildren;
            return this;
        }

        /// <summary>
        /// Assigns the reciprocal relationship children indicator note.
        /// </summary>
        /// <param name="reciprocalRelationshipChildrenNote">The reciprocal relationship children indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithReciprocalRelationshipChildrenNote(string reciprocalRelationshipChildrenNote)
        {
            _reciprocalRelationshipChildrenNote = reciprocalRelationshipChildrenNote;
            return this;
        }

        /// <summary>
        /// Assigns the reciprocal relationship friends indicator.
        /// </summary>
        /// <param name="reciprocalRelationshipFriends">The reciprocal relationship friends indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithReciprocalRelationshipFriends(DensAsiHasRelationshipOption reciprocalRelationshipFriends)
        {
            _reciprocalRelationshipFriends = reciprocalRelationshipFriends;
            return this;
        }

        /// <summary>
        /// Assigns the reciprocal relationship friends indicator note.
        /// </summary>
        /// <param name="reciprocalRelationshipFriendsNote">The reciprocal relationship friends indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithReciprocalRelationshipFriendsNote(string reciprocalRelationshipFriendsNote)
        {
            _reciprocalRelationshipFriendsNote = reciprocalRelationshipFriendsNote;
            return this;
        }

        /// <summary>
        /// Assigns the problems mother in last thirty days indicator.
        /// </summary>
        /// <param name="problemsMotherInLastThirtyDaysIndicator">The problems mother in last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsMotherInLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> problemsMotherInLastThirtyDaysIndicator)
        {
            _problemsMotherInLastThirtyDaysIndicator = problemsMotherInLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems mother in lifetime indicator.
        /// </summary>
        /// <param name="problemsMotherInLifetimeIndicator">The problems mother in lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsMotherInLifetimeIndicator(DensAsiNonResponseType<bool?> problemsMotherInLifetimeIndicator)
        {
            _problemsMotherInLifetimeIndicator = problemsMotherInLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems mother note.
        /// </summary>
        /// <param name="problemsMotherNote">The problems mother note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsMotherNote(string problemsMotherNote)
        {
            _problemsMotherNote = problemsMotherNote;
            return this;
        }

        /// <summary>
        /// Assigns the problems father in last thirty days indicator.
        /// </summary>
        /// <param name="problemsFatherInLastThirtyDaysIndicator">The problems father in last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsFatherInLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> problemsFatherInLastThirtyDaysIndicator)
        {
            _problemsFatherInLastThirtyDaysIndicator = problemsFatherInLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems father in lifetime indicator.
        /// </summary>
        /// <param name="problemsFatherInLifetimeIndicator">The problems father in lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsFatherInLifetimeIndicator(DensAsiNonResponseType<bool?> problemsFatherInLifetimeIndicator)
        {
            _problemsFatherInLifetimeIndicator = problemsFatherInLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems father note.
        /// </summary>
        /// <param name="problemsFatherNote">The problems father note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsFatherNote(string problemsFatherNote)
        {
            _problemsFatherNote = problemsFatherNote;
            return this;
        }

        /// <summary>
        /// Assigns the problems brother sister in last thirty days indicator.
        /// </summary>
        /// <param name="problemsBrotherSisterInLastThirtyDaysIndicator">The problems brother sister in last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsBrotherSisterInLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> problemsBrotherSisterInLastThirtyDaysIndicator)
        {
            _problemsBrotherSisterInLastThirtyDaysIndicator = problemsBrotherSisterInLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems brother sister in lifetime indicator.
        /// </summary>
        /// <param name="problemsBrotherSisterInLifetimeIndicator">The problems brother sister in lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsBrotherSisterInLifetimeIndicator(DensAsiNonResponseType<bool?> problemsBrotherSisterInLifetimeIndicator)
        {
            _problemsBrotherSisterInLifetimeIndicator = problemsBrotherSisterInLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems brother sister note.
        /// </summary>
        /// <param name="problemsBrotherSisterNote">The problems brother sister note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsBrotherSisterNote(string problemsBrotherSisterNote)
        {
            _problemsBrotherSisterNote = problemsBrotherSisterNote;
            return this;
        }

        /// <summary>
        /// Assigns the problems sexual partner in last thirty days indicator.
        /// </summary>
        /// <param name="problemsSexualPartnerInLastThirtyDaysIndicator">The problems sexual partner in last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsSexualPartnerInLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> problemsSexualPartnerInLastThirtyDaysIndicator)
        {
            _problemsSexualPartnerInLastThirtyDaysIndicator = problemsSexualPartnerInLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems sexual partner in lifetime indicator.
        /// </summary>
        /// <param name="problemsSexualPartnerInLifetimeIndicator">The problems sexual partner in lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsSexualPartnerInLifetimeIndicator(DensAsiNonResponseType<bool?> problemsSexualPartnerInLifetimeIndicator)
        {
            _problemsSexualPartnerInLifetimeIndicator = problemsSexualPartnerInLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems sexual partner note.
        /// </summary>
        /// <param name="problemsSexualPartnerNote">The problems sexual partner note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsSexualPartnerNote(string problemsSexualPartnerNote)
        {
            _problemsSexualPartnerNote = problemsSexualPartnerNote;
            return this;
        }

        /// <summary>
        /// Assigns the problems children in last thirty days indicator.
        /// </summary>
        /// <param name="problemsChildrenInLastThirtyDaysIndicator">The problems children in last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsChildrenInLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> problemsChildrenInLastThirtyDaysIndicator)
        {
            _problemsChildrenInLastThirtyDaysIndicator = problemsChildrenInLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems children in lifetime indicator.
        /// </summary>
        /// <param name="problemsChildrenInLifetimeIndicator">The problems children in lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsChildrenInLifetimeIndicator(DensAsiNonResponseType<bool?> problemsChildrenInLifetimeIndicator)
        {
            _problemsChildrenInLifetimeIndicator = problemsChildrenInLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems children note.
        /// </summary>
        /// <param name="problemsChildrenNote">The problems children note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsChildrenNote(string problemsChildrenNote)
        {
            _problemsChildrenNote = problemsChildrenNote;
            return this;
        }

        /// <summary>
        /// Assigns the problems other significant family in last thirty days indicator.
        /// </summary>
        /// <param name="problemsOtherSignificantFamilyInLastThirtyDaysIndicator">The problems other significant family in last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsOtherSignificantFamilyInLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> problemsOtherSignificantFamilyInLastThirtyDaysIndicator)
        {
            _problemsOtherSignificantFamilyInLastThirtyDaysIndicator = problemsOtherSignificantFamilyInLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems other significant family in lifetime indicator.
        /// </summary>
        /// <param name="problemsOtherSignificantFamilyInLifetimeIndicator">The problems other significant family in lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsOtherSignificantFamilyInLifetimeIndicator(DensAsiNonResponseType<bool?> problemsOtherSignificantFamilyInLifetimeIndicator)
        {
            _problemsOtherSignificantFamilyInLifetimeIndicator = problemsOtherSignificantFamilyInLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems other significant family description.
        /// </summary>
        /// <param name="problemsOtherSignificantFamilyDescription">The problems other significant family description.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsOtherSignificantFamilyDescription(string problemsOtherSignificantFamilyDescription)
        {
            _problemsOtherSignificantFamilyDescription = problemsOtherSignificantFamilyDescription;
            return this;
        }

        /// <summary>
        /// Assigns the problems other significant family note.
        /// </summary>
        /// <param name="problemsOtherSignificantFamilyNote">The problems other significant family note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsOtherSignificantFamilyNote(string problemsOtherSignificantFamilyNote)
        {
            _problemsOtherSignificantFamilyNote = problemsOtherSignificantFamilyNote;
            return this;
        }

        /// <summary>
        /// Assigns the problems close friends in last thirty days indicator.
        /// </summary>
        /// <param name="problemsCloseFriendsInLastThirtyDaysIndicator">The problems close friends in last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsCloseFriendsInLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> problemsCloseFriendsInLastThirtyDaysIndicator)
        {
            _problemsCloseFriendsInLastThirtyDaysIndicator = problemsCloseFriendsInLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems close friends in lifetime indicator.
        /// </summary>
        /// <param name="problemsCloseFriendsInLifetimeIndicator">The problems close friends in lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsCloseFriendsInLifetimeIndicator(DensAsiNonResponseType<bool?> problemsCloseFriendsInLifetimeIndicator)
        {
            _problemsCloseFriendsInLifetimeIndicator = problemsCloseFriendsInLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems close friends note.
        /// </summary>
        /// <param name="problemsCloseFriendsNote">The problems close friends note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsCloseFriendsNote(string problemsCloseFriendsNote)
        {
            _problemsCloseFriendsNote = problemsCloseFriendsNote;
            return this;
        }

        /// <summary>
        /// Assigns the problems neighbors in last thirty days indicator.
        /// </summary>
        /// <param name="problemsNeighborsInLastThirtyDaysIndicator">The problems neighbors in last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsNeighborsInLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> problemsNeighborsInLastThirtyDaysIndicator)
        {
            _problemsNeighborsInLastThirtyDaysIndicator = problemsNeighborsInLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems neighbors in lifetime indicator.
        /// </summary>
        /// <param name="problemsNeighborsInLifetimeIndicator">The problems neighbors in lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsNeighborsInLifetimeIndicator(DensAsiNonResponseType<bool?> problemsNeighborsInLifetimeIndicator)
        {
            _problemsNeighborsInLifetimeIndicator = problemsNeighborsInLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems neighbors note.
        /// </summary>
        /// <param name="problemsNeighborsNote">The problems neighbors note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsNeighborsNote(string problemsNeighborsNote)
        {
            _problemsNeighborsNote = problemsNeighborsNote;
            return this;
        }

        /// <summary>
        /// Assigns the problems coworkers in last thirty days indicator.
        /// </summary>
        /// <param name="problemsCoworkersInLastThirtyDaysIndicator">The problems coworkers in last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsCoworkersInLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> problemsCoworkersInLastThirtyDaysIndicator)
        {
            _problemsCoworkersInLastThirtyDaysIndicator = problemsCoworkersInLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems coworkers in lifetime indicator.
        /// </summary>
        /// <param name="problemsCoworkersInLifetimeIndicator">The problems coworkers in lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsCoworkersInLifetimeIndicator(DensAsiNonResponseType<bool?> problemsCoworkersInLifetimeIndicator)
        {
            _problemsCoworkersInLifetimeIndicator = problemsCoworkersInLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the problems coworkers note.
        /// </summary>
        /// <param name="problemsCoworkersNote">The problems coworkers note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithProblemsCoworkersNote(string problemsCoworkersNote)
        {
            _problemsCoworkersNote = problemsCoworkersNote;
            return this;
        }

        /// <summary>
        /// Assigns the abused emotionally in last thirty days indicator.
        /// </summary>
        /// <param name="abusedEmotionallyInLastThirtyDaysIndicator">The abused emotionally in last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithAbusedEmotionallyInLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> abusedEmotionallyInLastThirtyDaysIndicator)
        {
            _abusedEmotionallyInLastThirtyDaysIndicator = abusedEmotionallyInLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the abused emotionally in lifetime indicator.
        /// </summary>
        /// <param name="abusedEmotionallyInLifetimeIndicator">The abused emotionally in lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithAbusedEmotionallyInLifetimeIndicator(DensAsiNonResponseType<bool?> abusedEmotionallyInLifetimeIndicator)
        {
            _abusedEmotionallyInLifetimeIndicator = abusedEmotionallyInLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the abused emotionally note.
        /// </summary>
        /// <param name="abusedEmotionallyNote">The abused emotionally note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithAbusedEmotionallyNote(string abusedEmotionallyNote)
        {
            _abusedEmotionallyNote = abusedEmotionallyNote;
            return this;
        }

        /// <summary>
        /// Assigns the abused physically in last thirty days indicator.
        /// </summary>
        /// <param name="abusedPhysicallyInLastThirtyDaysIndicator">The abused physically in last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithAbusedPhysicallyInLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> abusedPhysicallyInLastThirtyDaysIndicator)
        {
            _abusedPhysicallyInLastThirtyDaysIndicator = abusedPhysicallyInLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the abused physically in lifetime indicator.
        /// </summary>
        /// <param name="abusedPhysicallyInLifetimeIndicator">The abused physically in lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithAbusedPhysicallyInLifetimeIndicator(DensAsiNonResponseType<bool?> abusedPhysicallyInLifetimeIndicator)
        {
            _abusedPhysicallyInLifetimeIndicator = abusedPhysicallyInLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the abused physically note.
        /// </summary>
        /// <param name="abusedPhysicallyNote">The abused physically note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithAbusedPhysicallyNote(string abusedPhysicallyNote)
        {
            _abusedPhysicallyNote = abusedPhysicallyNote;
            return this;
        }

        /// <summary>
        /// Assigns the abused sexually in last thirty days indicator.
        /// </summary>
        /// <param name="abusedSexuallyInLastThirtyDaysIndicator">The abused sexually in last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithAbusedSexuallyInLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> abusedSexuallyInLastThirtyDaysIndicator)
        {
            _abusedSexuallyInLastThirtyDaysIndicator = abusedSexuallyInLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the abused sexually in lifetime indicator.
        /// </summary>
        /// <param name="abusedSexuallyInLifetimeIndicator">The abused sexually in lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithAbusedSexuallyInLifetimeIndicator(DensAsiNonResponseType<bool?> abusedSexuallyInLifetimeIndicator)
        {
            _abusedSexuallyInLifetimeIndicator = abusedSexuallyInLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the abused sexually note.
        /// </summary>
        /// <param name="abusedSexuallyNote">The abused sexually note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithAbusedSexuallyNote(string abusedSexuallyNote)
        {
            _abusedSexuallyNote = abusedSexuallyNote;
            return this;
        }

        /// <summary>
        /// Assigns the serious family conflicts in last thirty days day count.
        /// </summary>
        /// <param name="seriousFamilyConflictsInLastThirtyDaysDayCount">The serious family conflicts in last thirty days day count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithSeriousFamilyConflictsInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> seriousFamilyConflictsInLastThirtyDaysDayCount)
        {
            _seriousFamilyConflictsInLastThirtyDaysDayCount = seriousFamilyConflictsInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the serious family conflicts in last thirty days day count note.
        /// </summary>
        /// <param name="seriousFamilyConflictsInLastThirtyDaysDayCountNote">The serious family conflicts in last thirty days day count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithSeriousFamilyConflictsInLastThirtyDaysDayCountNote(string seriousFamilyConflictsInLastThirtyDaysDayCountNote)
        {
            _seriousFamilyConflictsInLastThirtyDaysDayCountNote = seriousFamilyConflictsInLastThirtyDaysDayCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the troubled by family problems DensAsi patient rating.
        /// </summary>
        /// <param name="troubledByFamilyProblemsDensAsiPatientRating">The troubled by family problems DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithTroubledByFamilyProblemsDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> troubledByFamilyProblemsDensAsiPatientRating)
        {
            _troubledByFamilyProblemsDensAsiPatientRating = troubledByFamilyProblemsDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the troubled by family problems DensAsi patient rating note.
        /// </summary>
        /// <param name="troubledByFamilyProblemsDensAsiPatientRatingNote">The troubled by family problems DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithTroubledByFamilyProblemsDensAsiPatientRatingNote(string troubledByFamilyProblemsDensAsiPatientRatingNote)
        {
            _troubledByFamilyProblemsDensAsiPatientRatingNote = troubledByFamilyProblemsDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the importance of family problem counseling DensAsi patient rating.
        /// </summary>
        /// <param name="importanceOfFamilyProblemCounselingDensAsiPatientRating">The importance of family problem counseling DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithImportanceOfFamilyProblemCounselingDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> importanceOfFamilyProblemCounselingDensAsiPatientRating)
        {
            _importanceOfFamilyProblemCounselingDensAsiPatientRating = importanceOfFamilyProblemCounselingDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the importance of family problem counseling DensAsi patient rating note.
        /// </summary>
        /// <param name="importanceOfFamilyProblemCounselingDensAsiPatientRatingNote">The importance of family problem counseling DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithImportanceOfFamilyProblemCounselingDensAsiPatientRatingNote(string importanceOfFamilyProblemCounselingDensAsiPatientRatingNote)
        {
            _importanceOfFamilyProblemCounselingDensAsiPatientRatingNote = importanceOfFamilyProblemCounselingDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the conflicts with others in last thirty days day count.
        /// </summary>
        /// <param name="conflictsWithOthersInLastThirtyDaysDayCount">The conflicts with others in last thirty days day count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithConflictsWithOthersInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> conflictsWithOthersInLastThirtyDaysDayCount)
        {
            _conflictsWithOthersInLastThirtyDaysDayCount = conflictsWithOthersInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the conflicts with others in last thirty days day count note.
        /// </summary>
        /// <param name="conflictsWithOthersInLastThirtyDaysDayCountNote">The conflicts with others in last thirty days day count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithConflictsWithOthersInLastThirtyDaysDayCountNote(string conflictsWithOthersInLastThirtyDaysDayCountNote)
        {
            _conflictsWithOthersInLastThirtyDaysDayCountNote = conflictsWithOthersInLastThirtyDaysDayCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the troubled by social problems DensAsi patient rating.
        /// </summary>
        /// <param name="troubledBySocialProblemsDensAsiPatientRating">The troubled by social problems DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithTroubledBySocialProblemsDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> troubledBySocialProblemsDensAsiPatientRating)
        {
            _troubledBySocialProblemsDensAsiPatientRating = troubledBySocialProblemsDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the troubled by social problems DensAsi patient rating note.
        /// </summary>
        /// <param name="troubledBySocialProblemsDensAsiPatientRatingNote">The troubled by social problems DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithTroubledBySocialProblemsDensAsiPatientRatingNote(string troubledBySocialProblemsDensAsiPatientRatingNote)
        {
            _troubledBySocialProblemsDensAsiPatientRatingNote = troubledBySocialProblemsDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the importance of social problem counseling DensAsi patient rating.
        /// </summary>
        /// <param name="importanceOfSocialProblemCounselingDensAsiPatientRating">The importance of social problem counseling DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithImportanceOfSocialProblemCounselingDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> importanceOfSocialProblemCounselingDensAsiPatientRating)
        {
            _importanceOfSocialProblemCounselingDensAsiPatientRating = importanceOfSocialProblemCounselingDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the importance of social problem counseling DensAsi patient rating note.
        /// </summary>
        /// <param name="importanceOfSocialProblemCounselingDensAsiPatientRatingNote">The importance of social problem counseling DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithImportanceOfSocialProblemCounselingDensAsiPatientRatingNote(string importanceOfSocialProblemCounselingDensAsiPatientRatingNote)
        {
            _importanceOfSocialProblemCounselingDensAsiPatientRatingNote = importanceOfSocialProblemCounselingDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the patient family social counseling DensAsi interviewer rating.
        /// </summary>
        /// <param name="patientFamilySocialCounselingDensAsiInterviewerRating">The patient family social counseling DensAsi interviewer rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithPatientFamilySocialCounselingDensAsiInterviewerRating(DensAsiInterviewerRating patientFamilySocialCounselingDensAsiInterviewerRating)
        {
            _patientFamilySocialCounselingDensAsiInterviewerRating = patientFamilySocialCounselingDensAsiInterviewerRating;
            return this;
        }

        /// <summary>
        /// Assigns the patient family social counseling DensAsi interviewer rating note.
        /// </summary>
        /// <param name="patientFamilySocialCounselingDensAsiInterviewerRatingNote">The patient family social counseling DensAsi interviewer rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithPatientFamilySocialCounselingDensAsiInterviewerRatingNote(string patientFamilySocialCounselingDensAsiInterviewerRatingNote)
        {
            _patientFamilySocialCounselingDensAsiInterviewerRatingNote = patientFamilySocialCounselingDensAsiInterviewerRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the confidence distorted by patient misrepresentation indicator.
        /// </summary>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicator">The confidence distorted by patient misrepresentation indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithConfidenceDistortedByPatientMisrepresentationIndicator(bool? confidenceDistortedByPatientMisrepresentationIndicator)
        {
            _confidenceDistortedByPatientMisrepresentationIndicator = confidenceDistortedByPatientMisrepresentationIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the confidence distorted by patient misrepresentation indicator note.
        /// </summary>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicatorNote">The confidence distorted by patient misrepresentation indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithConfidenceDistortedByPatientMisrepresentationIndicatorNote(string confidenceDistortedByPatientMisrepresentationIndicatorNote)
        {
            _confidenceDistortedByPatientMisrepresentationIndicatorNote = confidenceDistortedByPatientMisrepresentationIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the confidence rate distorted by patient inability to understand indicator.
        /// </summary>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicator">The confidence rate distorted by patient inability to understand indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicator(bool? confidenceRateDistortedByPatientInabilityToUnderstandIndicator)
        {
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicator = confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the confidence rate distorted by patient inability to understand indicator note.
        /// </summary>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote">The confidence rate distorted by patient inability to understand indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote(string confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote)
        {
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote = confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the section note.
        /// </summary>
        /// <param name="sectionNote">The section note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithSectionNote(string sectionNote)
        {
            _sectionNote = sectionNote;
            return this;
        }

        /// <summary>
        /// Assigns the homeless in last thirty days day count.
        /// </summary>
        /// <param name="homelessInLastThirtyDaysDayCount">The homeless in last thirty days day count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithHomelessInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> homelessInLastThirtyDaysDayCount)
        {
            _homelessInLastThirtyDaysDayCount = homelessInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the homeless in last thirty days day count note.
        /// </summary>
        /// <param name="homelessInLastThirtyDaysDayCountNote">The homeless in last thirty days day count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithHomelessInLastThirtyDaysDayCountNote(string homelessInLastThirtyDaysDayCountNote)
        {
            _homelessInLastThirtyDaysDayCountNote = homelessInLastThirtyDaysDayCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the shelter in last thirty days day count.
        /// </summary>
        /// <param name="shelterInLastThirtyDaysDayCount">The shelter in last thirty days day count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithShelterInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> shelterInLastThirtyDaysDayCount)
        {
            _shelterInLastThirtyDaysDayCount = shelterInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the shelter in last thirty days day count note.
        /// </summary>
        /// <param name="shelterInLastThirtyDaysDayCountNote">The shelter in last thirty days day count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithShelterInLastThirtyDaysDayCountNote(string shelterInLastThirtyDaysDayCountNote)
        {
            _shelterInLastThirtyDaysDayCountNote = shelterInLastThirtyDaysDayCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the not owned house in last thirty days day count.
        /// </summary>
        /// <param name="notOwnedHouseInLastThirtyDaysDayCount">The not owned house in last thirty days day count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithNotOwnedHouseInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> notOwnedHouseInLastThirtyDaysDayCount)
        {
            _notOwnedHouseInLastThirtyDaysDayCount = notOwnedHouseInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the not owned house in last thirty days day count note.
        /// </summary>
        /// <param name="notOwnedHouseInLastThirtyDaysDayCountNote">The not owned house in last thirty days day count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithNotOwnedHouseInLastThirtyDaysDayCountNote(string notOwnedHouseInLastThirtyDaysDayCountNote)
        {
            _notOwnedHouseInLastThirtyDaysDayCountNote = notOwnedHouseInLastThirtyDaysDayCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the hospital jail in last thirty days day count.
        /// </summary>
        /// <param name="hospitalJailInLastThirtyDaysDayCount">The hospital jail in last thirty days day count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithHospitalJailInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> hospitalJailInLastThirtyDaysDayCount)
        {
            _hospitalJailInLastThirtyDaysDayCount = hospitalJailInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the hospital jail in last thirty days day count note.
        /// </summary>
        /// <param name="hospitalJailInLastThirtyDaysDayCountNote">The hospital jail in last thirty days day count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSectionBuilder WithHospitalJailInLastThirtyDaysDayCountNote(string hospitalJailInLastThirtyDaysDayCountNote)
        {
            _hospitalJailInLastThirtyDaysDayCountNote = hospitalJailInLastThirtyDaysDayCountNote;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFamilySocialRelationshipsSectionBuilder">A DensAsiFamilySocialRelationshipsSectionBuilder.</see></returns>
        public DensAsiFamilySocialRelationshipsSection Build()
        {
            return new DensAsiFamilySocialRelationshipsSection(
                _densAsiMaritalStatus,
                _densAsiMaritalStatusNote,
                _yearsAndMonthsWithMaritalStatusTimeSpan,
                _yearsAndMonthsWithMaritalStatusTimeSpanNote,
                _maritalStatusSatisfactionIndicator,
                _maritalStatusSatisfactionIndicatorNote,
                _pastThreeYearsDensAsiLivingArrangementType,
                _pastThreeYearsDensAsiLivingArrangementTypeNote,
                _yearsAndMonthsInLivingArrangementTypeTimeSpan,
                _yearsAndMonthsInLivingArrangementTypeTimeSpanNote,
                _livingArrangementTypeSatisfaction,
                _livingArrangementTypeSatisfactionNote,
                _livingWithAnyoneWhoHasAlcoholProblemIndicator,
                _livingWithAnyoneWhoHasAlcoholProblemIndicatorNote,
                _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator,
                _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote,
                _densAsiFreeTimeSpentType,
                _densAsiFreeTimeSpentTypeNote,
                _freeTimeSpentTypeSatisfaction,
                _freeTimeSpentTypeSatisfactionNote,
                _closeFriendsCount,
                _closeFriendsCountNote,
                _reciprocalRelationshipMother,
                _reciprocalRelationshipMotherNote,
                _reciprocalRelationshipFather,
                _reciprocalRelationshipFatherNote,
                _reciprocalRelationshipBrotherSister,
                _reciprocalRelationshipBrotherSisterNote,
                _reciprocalRelationshipSexualPartner,
                _reciprocalRelationshipSexualPartnerNote,
                _reciprocalRelationshipChildren,
                _reciprocalRelationshipChildrenNote,
                _reciprocalRelationshipFriends,
                _reciprocalRelationshipFriendsNote,
                _problemsMotherInLastThirtyDaysIndicator,
                _problemsMotherInLifetimeIndicator,
                _problemsMotherNote,
                _problemsFatherInLastThirtyDaysIndicator,
                _problemsFatherInLifetimeIndicator,
                _problemsFatherNote,
                _problemsBrotherSisterInLastThirtyDaysIndicator,
                _problemsBrotherSisterInLifetimeIndicator,
                _problemsBrotherSisterNote,
                _problemsSexualPartnerInLastThirtyDaysIndicator,
                _problemsSexualPartnerInLifetimeIndicator,
                _problemsSexualPartnerNote,
                _problemsChildrenInLastThirtyDaysIndicator,
                _problemsChildrenInLifetimeIndicator,
                _problemsChildrenNote,
                _problemsOtherSignificantFamilyInLastThirtyDaysIndicator,
                _problemsOtherSignificantFamilyInLifetimeIndicator,
                _problemsOtherSignificantFamilyDescription,
                _problemsOtherSignificantFamilyNote,
                _problemsCloseFriendsInLastThirtyDaysIndicator,
                _problemsCloseFriendsInLifetimeIndicator,
                _problemsCloseFriendsNote,
                _problemsNeighborsInLastThirtyDaysIndicator,
                _problemsNeighborsInLifetimeIndicator,
                _problemsNeighborsNote,
                _problemsCoworkersInLastThirtyDaysIndicator,
                _problemsCoworkersInLifetimeIndicator,
                _problemsCoworkersNote,
                _abusedEmotionallyInLastThirtyDaysIndicator,
                _abusedEmotionallyInLifetimeIndicator,
                _abusedEmotionallyNote,
                _abusedPhysicallyInLastThirtyDaysIndicator,
                _abusedPhysicallyInLifetimeIndicator,
                _abusedPhysicallyNote,
                _abusedSexuallyInLastThirtyDaysIndicator,
                _abusedSexuallyInLifetimeIndicator,
                _abusedSexuallyNote,
                _seriousFamilyConflictsInLastThirtyDaysDayCount,
                _seriousFamilyConflictsInLastThirtyDaysDayCountNote,
                _troubledByFamilyProblemsDensAsiPatientRating,
                _troubledByFamilyProblemsDensAsiPatientRatingNote,
                _importanceOfFamilyProblemCounselingDensAsiPatientRating,
                _importanceOfFamilyProblemCounselingDensAsiPatientRatingNote,
                _conflictsWithOthersInLastThirtyDaysDayCount,
                _conflictsWithOthersInLastThirtyDaysDayCountNote,
                _troubledBySocialProblemsDensAsiPatientRating,
                _troubledBySocialProblemsDensAsiPatientRatingNote,
                _importanceOfSocialProblemCounselingDensAsiPatientRating,
                _importanceOfSocialProblemCounselingDensAsiPatientRatingNote,
                _patientFamilySocialCounselingDensAsiInterviewerRating,
                _patientFamilySocialCounselingDensAsiInterviewerRatingNote,
                _confidenceDistortedByPatientMisrepresentationIndicator,
                _confidenceDistortedByPatientMisrepresentationIndicatorNote,
                _confidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                _sectionNote,
                _homelessInLastThirtyDaysDayCount,
                _homelessInLastThirtyDaysDayCountNote,
                _shelterInLastThirtyDaysDayCount,
                _shelterInLastThirtyDaysDayCountNote,
                _notOwnedHouseInLastThirtyDaysDayCount,
                _notOwnedHouseInLastThirtyDaysDayCountNote,
                _hospitalJailInLastThirtyDaysDayCount,
                _hospitalJailInLastThirtyDaysDayCountNote
                );
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="DensAsiFamilySocialRelationshipsSectionBuilder"/> to <see cref="DensAsiFamilySocialRelationshipsSection"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator DensAsiFamilySocialRelationshipsSection(DensAsiFamilySocialRelationshipsSectionBuilder builder)
        {
            return builder.Build();
        }
    }
}
