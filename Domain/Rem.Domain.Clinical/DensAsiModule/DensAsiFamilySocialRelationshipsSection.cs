using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiFamilySocialRelationshipsSection contains patient family and social relationship information from the Family Social Information section of the DensAsi. 
    /// <remarks>
    /// Included in each of these sections is the interviewer's severity rating, suggesting the client's need for treatment or additional treatment. 
    /// This is based on the information provided by the client. 
    /// </remarks>
    /// </summary>
    [Component]
    public class DensAsiFamilySocialRelationshipsSection : DensAsiInterviewSectionBase
    {
        private readonly DensAsiNonResponseType<bool?> _abusedEmotionallyInLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _abusedEmotionallyInLifetimeIndicator;
        private readonly string _abusedEmotionallyNote;
        private readonly DensAsiNonResponseType<bool?> _abusedPhysicallyInLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _abusedPhysicallyInLifetimeIndicator;
        private readonly string _abusedPhysicallyNote;
        private readonly DensAsiNonResponseType<bool?> _abusedSexuallyInLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _abusedSexuallyInLifetimeIndicator;
        private readonly string _abusedSexuallyNote;
        private readonly DensAsiNonResponseType<int?> _closeFriendsCount;
        private readonly string _closeFriendsCountNote;
        private readonly bool? _confidenceDistortedByPatientMisrepresentationIndicator;
        private readonly string _confidenceDistortedByPatientMisrepresentationIndicatorNote;
        private readonly bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private readonly string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private readonly DensAsiNonResponseType<int?> _conflictsWithOthersInLastThirtyDaysDayCount;
        private readonly string _conflictsWithOthersInLastThirtyDaysDayCountNote;
        private readonly DensAsiNonResponseType<DensAsiFreeTimeSpentType> _densAsiFreeTimeSpentType;
        private readonly string _densAsiFreeTimeSpentTypeNote;
        private readonly DensAsiNonResponseType<DensAsiMaritalStatus> _densAsiMaritalStatus;
        private readonly string _densAsiMaritalStatusNote;
        private readonly DensAsiNonResponseType<DensAsiSatisfaction> _freeTimeSpentTypeDensAsiSatisfaction;
        private readonly string _freeTimeSpentTypeDensAsiSatisfactionNote;
        private readonly DensAsiNonResponseType<int?> _homelessInLastThirtyDaysDayCount;
        private readonly string _homelessInLastThirtyDaysDayCountNote;
        private readonly DensAsiNonResponseType<int?> _hospitalJailInLastThirtyDaysDayCount;
        private readonly string _hospitalJailInLastThirtyDaysDayCountNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _importanceOfFamilyProblemCounselingDensAsiPatientRating;
        private readonly string _importanceOfFamilyProblemCounselingDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _importanceOfSocialProblemCounselingDensAsiPatientRating;
        private readonly string _importanceOfSocialProblemCounselingDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<DensAsiSatisfaction> _livingArrangementTypeDensAsiSatisfaction;
        private readonly string _livingArrangementTypeDensAsiSatisfactionNote;
        private readonly DensAsiNonResponseType<bool?> _livingWithAnyoneWhoHasAlcoholProblemIndicator;
        private readonly string _livingWithAnyoneWhoHasAlcoholProblemIndicatorNote;
        private readonly DensAsiNonResponseType<bool?> _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator;
        private readonly string _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote;
        private readonly DensAsiNonResponseType<DensAsiSatisfaction> _maritalStatusDensAsiSatisfaction;
        private readonly string _maritalStatusDensAsiSatisfactionNote;
        private readonly DensAsiNonResponseType<int?> _notOwnedHouseInLastThirtyDaysDayCount;
        private readonly string _notOwnedHouseInLastThirtyDaysDayCountNote;
        private readonly DensAsiNonResponseType<DensAsiLivingArrangementType> _pastThreeYearsDensAsiLivingArrangementType;
        private readonly string _pastThreeYearsDensAsiLivingArrangementTypeNote;
        private readonly DensAsiInterviewerRating _patientFamilySocialCounselingDensAsiInterviewerRating;
        private readonly string _patientFamilySocialCounselingDensAsiInterviewerRatingNote;
        private readonly DensAsiNonResponseType<bool?> _problemsBrotherSisterInLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _problemsBrotherSisterInLifetimeIndicator;
        private readonly string _problemsBrotherSisterNote;
        private readonly DensAsiNonResponseType<bool?> _problemsChildrenInLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _problemsChildrenInLifetimeIndicator;
        private readonly string _problemsChildrenNote;
        private readonly DensAsiNonResponseType<bool?> _problemsCloseFriendsInLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _problemsCloseFriendsInLifetimeIndicator;
        private readonly string _problemsCloseFriendsNote;
        private readonly DensAsiNonResponseType<bool?> _problemsCoworkersInLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _problemsCoworkersInLifetimeIndicator;
        private readonly string _problemsCoworkersNote;
        private readonly DensAsiNonResponseType<bool?> _problemsFatherInLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _problemsFatherInLifetimeIndicator;
        private readonly string _problemsFatherNote;
        private readonly DensAsiNonResponseType<bool?> _problemsMotherInLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _problemsMotherInLifetimeIndicator;
        private readonly string _problemsMotherNote;
        private readonly DensAsiNonResponseType<bool?> _problemsNeighborsInLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _problemsNeighborsInLifetimeIndicator;
        private readonly string _problemsNeighborsNote;
        private readonly string _problemsOtherSignificantFamilyDescription;
        private readonly DensAsiNonResponseType<bool?> _problemsOtherSignificantFamilyInLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _problemsOtherSignificantFamilyInLifetimeIndicator;
        private readonly string _problemsOtherSignificantFamilyNote;
        private readonly DensAsiNonResponseType<bool?> _problemsSexualPartnerInLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _problemsSexualPartnerInLifetimeIndicator;
        private readonly string _problemsSexualPartnerNote;
        private readonly DensAsiHasRelationshipOption _brotherSisterDensAsiHasRelationshipOption;
        private readonly string _brotherSisterDensAsiHasRelationshipOptionNote;
        private readonly DensAsiHasRelationshipOption _childrenDensAsiHasRelationshipOption;
        private readonly string _childrenDensAsiHasRelationshipOptionNote;
        private readonly DensAsiHasParentalRelationshipOption _fatherDensAsiHasParentalRelationshipOption;
        private readonly string _fatherDensAsiHasParentalRelationshipOptionNote;
        private readonly DensAsiHasRelationshipOption _friendsDensAsiHasRelationshipOption;
        private readonly string _friendsDensAsiHasRelationshipOptionNote;
        private readonly DensAsiHasParentalRelationshipOption _motherDensAsiHasParentalRelationshipOption;
        private readonly string _motherDensAsiHasParentalRelationshipOptionNote;
        private readonly DensAsiHasRelationshipOption _sexualPartnerDensAsiHasRelationshipOption;
        private readonly string _sexualPartnerDensAsiHasRelationshipOptionNote;
        private readonly string _sectionNote;
        private readonly DensAsiNonResponseType<int?> _seriousFamilyConflictsInLastThirtyDaysDayCount;
        private readonly string _seriousFamilyConflictsInLastThirtyDaysDayCountNote;
        private readonly DensAsiNonResponseType<int?> _shelterInLastThirtyDaysDayCount;
        private readonly string _shelterInLastThirtyDaysDayCountNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _troubledByFamilyProblemsDensAsiPatientRating;
        private readonly string _troubledByFamilyProblemsDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _troubledBySocialProblemsDensAsiPatientRating;
        private readonly string _troubledBySocialProblemsDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<TimeSpan?> _yearsAndMonthsInLivingArrangementTypeTimeSpan;
        private readonly string _yearsAndMonthsInLivingArrangementTypeTimeSpanNote;
        private readonly DensAsiNonResponseType<TimeSpan?> _yearsAndMonthsWithMaritalStatusTimeSpan;
        private readonly string _yearsAndMonthsWithMaritalStatusTimeSpanNote;

        private DensAsiFamilySocialRelationshipsSection ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiFamilySocialRelationshipsSection"/> class.
        /// </summary>
        /// <param name="densAsiMaritalStatus">The marital status.</param>
        /// <param name="densAsiMaritalStatusNote">The marital status note.</param>
        /// <param name="yearsAndMonthsWithMaritalStatusTimeSpan">The years and months with marital status time span.</param>
        /// <param name="yearsAndMonthsWithMaritalStatusTimeSpanNote">The years and months with marital status time span note.</param>
        /// <param name="maritalStatusDensAsiSatisfaction">The marital status satisfaction indicator.</param>
        /// <param name="maritalStatusDensAsiSatisfactionNote">The marital status satisfaction indicator note.</param>
        /// <param name="pastThreeYearsDensAsiLivingArrangementType">Type of the past three years living arrangement.</param>
        /// <param name="pastThreeYearsDensAsiLivingArrangementTypeNote">The past three years living arrangement type note.</param>
        /// <param name="yearsAndMonthsInLivingArrangementTypeTimeSpan">The years and months in living arrangement type time span.</param>
        /// <param name="yearsAndMonthsInLivingArrangementTypeTimeSpanNote">The years and months in living arrangement type time span note.</param>
        /// <param name="livingArrangementTypeDensAsiSatisfaction">The living arrangement type satisfaction indicator.</param>
        /// <param name="livingArrangementTypeDensAsiSatisfactionNote">The living arrangement type satisfaction indicator note.</param>
        /// <param name="livingWithAnyoneWhoHasAlcoholProblemIndicator">The living with anyone who has alcohol problem indicator.</param>
        /// <param name="livingWithAnyoneWhoHasAlcoholProblemIndicatorNote">The living with anyone who has alcohol problem indicator note.</param>
        /// <param name="livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator">The living with anyone who uses non prescribed drugs indicator.</param>
        /// <param name="livingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote">The living with anyone who uses non prescribed drugs indicator note.</param>
        /// <param name="densAsiFreeTimeSpentType">Type of the free time spent.</param>
        /// <param name="densAsiFreeTimeSpentTypeNote">The free time spent type note.</param>
        /// <param name="freeTimeSpentTypeDensAsiSatisfaction">The free time spent type satisfaction indicator.</param>
        /// <param name="freeTimeSpentTypeDensAsiSatisfactionNote">The free time spent type satisfaction indicator note.</param>
        /// <param name="closeFriendsCount">The close friends count.</param>
        /// <param name="closeFriendsCountNote">The close friends count note.</param>
        /// <param name="motherDensAsiHasParentalRelationshipOption">The reciprocal relationship mother indicator.</param>
        /// <param name="motherDensAsiHasParentalRelationshipOptionNote">The reciprocal relationship mother indicator note.</param>
        /// <param name="fatherDensAsiHasParentalRelationshipOption">The reciprocal relationship father indicator.</param>
        /// <param name="fatherDensAsiHasParentalRelationshipOptionNote">The reciprocal relationship father indicator note.</param>
        /// <param name="brotherSisterDensAsiHasRelationshipOption">The reciprocal relationship brother sister indicator.</param>
        /// <param name="brotherSisterDensAsiHasRelationshipOptionNote">The reciprocal relationship brother sister indicator note.</param>
        /// <param name="sexualPartnerDensAsiHasRelationshipOption">The reciprocal relationship sexual partner indicator.</param>
        /// <param name="sexualPartnerDensAsiHasRelationshipOptionNote">The reciprocal relationship sexual partner indicator note.</param>
        /// <param name="childrenDensAsiHasRelationshipOption">The reciprocal relationship children indicator.</param>
        /// <param name="childrenDensAsiHasRelationshipOptionNote">The reciprocal relationship children indicator note.</param>
        /// <param name="friendsDensAsiHasRelationshipOption">The reciprocal relationship friends indicator.</param>
        /// <param name="friendsDensAsiHasRelationshipOptionNote">The reciprocal relationship friends indicator note.</param>
        /// <param name="problemsMotherInLastThirtyDaysIndicator">The problems mother in last thirty days indicator.</param>
        /// <param name="problemsMotherInLifetimeIndicator">The problems mother in lifetime indicator.</param>
        /// <param name="problemsMotherNote">The problems mother note.</param>
        /// <param name="problemsFatherInLastThirtyDaysIndicator">The problems father in last thirty days indicator.</param>
        /// <param name="problemsFatherInLifetimeIndicator">The problems father in lifetime indicator.</param>
        /// <param name="problemsFatherNote">The problems father note.</param>
        /// <param name="problemsBrotherSisterInLastThirtyDaysIndicator">The problems brother sister in last thirty days indicator.</param>
        /// <param name="problemsBrotherSisterInLifetimeIndicator">The problems brother sister in lifetime indicator.</param>
        /// <param name="problemsBrotherSisterNote">The problems brother sister note.</param>
        /// <param name="problemsSexualPartnerInLastThirtyDaysIndicator">The problems sexual partner in last thirty days indicator.</param>
        /// <param name="problemsSexualPartnerInLifetimeIndicator">The problems sexual partner in lifetime indicator.</param>
        /// <param name="problemsSexualPartnerNote">The problems sexual partner note.</param>
        /// <param name="problemsChildrenInLastThirtyDaysIndicator">The problems children in last thirty days indicator.</param>
        /// <param name="problemsChildrenInLifetimeIndicator">The problems children in lifetime indicator.</param>
        /// <param name="problemsChildrenNote">The problems children note.</param>
        /// <param name="problemsOtherSignificantFamilyInLastThirtyDaysIndicator">The problems other significant family in last thirty days indicator.</param>
        /// <param name="problemsOtherSignificantFamilyInLifetimeIndicator">The problems other significant family in lifetime indicator.</param>
        /// <param name="problemsOtherSignificantFamilyDescription">The problems other significant family description.</param>
        /// <param name="problemsOtherSignificantFamilyNote">The problems other significant family note.</param>
        /// <param name="problemsCloseFriendsInLastThirtyDaysIndicator">The problems close friends in last thirty days indicator.</param>
        /// <param name="problemsCloseFriendsInLifetimeIndicator">The problems close friends in lifetime indicator.</param>
        /// <param name="problemsCloseFriendsNote">The problems close friends note.</param>
        /// <param name="problemsNeighborsInLastThirtyDaysIndicator">The problems neighbors in last thirty days indicator.</param>
        /// <param name="problemsNeighborsInLifetimeIndicator">The problems neighbors in lifetime indicator.</param>
        /// <param name="problemsNeighborsNote">The problems neighbors note.</param>
        /// <param name="problemsCoworkersInLastThirtyDaysIndicator">The problems coworkers in last thirty days indicator.</param>
        /// <param name="problemsCoworkersInLifetimeIndicator">The problems coworkers in lifetime indicator.</param>
        /// <param name="problemsCoworkersNote">The problems coworkers note.</param>
        /// <param name="abusedEmotionallyInLastThirtyDaysIndicator">The abused emotionally in last thirty days indicator.</param>
        /// <param name="abusedEmotionallyInLifetimeIndicator">The abused emotionally in lifetime indicator.</param>
        /// <param name="abusedEmotionallyNote">The abused emotionally note.</param>
        /// <param name="abusedPhysicallyInLastThirtyDaysIndicator">The abused physically in last thirty days indicator.</param>
        /// <param name="abusedPhysicallyInLifetimeIndicator">The abused physically in lifetime indicator.</param>
        /// <param name="abusedPhysicallyNote">The abused physically note.</param>
        /// <param name="abusedSexuallyInLastThirtyDaysIndicator">The abused sexually in last thirty days indicator.</param>
        /// <param name="abusedSexuallyInLifetimeIndicator">The abused sexually in lifetime indicator.</param>
        /// <param name="abusedSexuallyNote">The abused sexually note.</param>
        /// <param name="seriousFamilyConflictsInLastThirtyDaysDayCount">The serious family conflicts in last thirty days day count.</param>
        /// <param name="seriousFamilyConflictsInLastThirtyDaysDayCountNote">The serious family conflicts in last thirty days day count note.</param>
        /// <param name="troubledByFamilyProblemsDensAsiPatientRating">The troubled by family problems patient rating.</param>
        /// <param name="troubledByFamilyProblemsDensAsiPatientRatingNote">The troubled by family problems patient rating note.</param>
        /// <param name="importanceOfFamilyProblemCounselingDensAsiPatientRating">The importance of family problem counseling patient rating.</param>
        /// <param name="importanceOfFamilyProblemCounselingDensAsiPatientRatingNote">The importance of family problem counseling patient rating note.</param>
        /// <param name="conflictsWithOthersInLastThirtyDaysDayCount">The conflicts with others in last thirty days day count.</param>
        /// <param name="conflictsWithOthersInLastThirtyDaysDayCountNote">The conflicts with others in last thirty days day count note.</param>
        /// <param name="troubledBySocialProblemsDensAsiPatientRating">The troubled by social problems patient rating.</param>
        /// <param name="troubledBySocialProblemsDensAsiPatientRatingNote">The troubled by social problems patient rating note.</param>
        /// <param name="importanceOfSocialProblemCounselingDensAsiPatientRating">The importance of social problem counseling patient rating.</param>
        /// <param name="importanceOfSocialProblemCounselingDensAsiPatientRatingNote">The importance of social problem counseling patient rating note.</param>
        /// <param name="patientFamilySocialCounselingDensAsiInterviewerRating">The patient family social counseling interviewer rating.</param>
        /// <param name="patientFamilySocialCounselingDensAsiInterviewerRatingNote">The patient family social counseling interviewer rating note.</param>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicator">The confidence distorted by patient misrepresentation indicator.</param>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicatorNote">The confidence distorted by patient misrepresentation indicator note.</param>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicator">The confidence rate distorted by patient inability to understand indicator.</param>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote">The confidence rate distorted by patient inability to understand indicator note.</param>
        /// <param name="sectionNote">The section note.</param>
        /// <param name="homelessInLastThirtyDaysDayCount">The homeless in last thirty days day count.</param>
        /// <param name="homelessInLastThirtyDaysDayCountNote">The homeless in last thirty days day count note.</param>
        /// <param name="shelterInLastThirtyDaysDayCount">The shelter in last thirty days day count.</param>
        /// <param name="shelterInLastThirtyDaysDayCountNote">The shelter in last thirty days day count note.</param>
        /// <param name="notOwnedHouseInLastThirtyDaysDayCount">The not owned house in last thirty days day count.</param>
        /// <param name="notOwnedHouseInLastThirtyDaysDayCountNote">The not owned house in last thirty days day count note.</param>
        /// <param name="hospitalJailInLastThirtyDaysDayCount">The hospital jail in last thirty days day count.</param>
        /// <param name="hospitalJailInLastThirtyDaysDayCountNote">The hospital jail in last thirty days day count note.</param>
        public DensAsiFamilySocialRelationshipsSection(DensAsiNonResponseType<DensAsiMaritalStatus> densAsiMaritalStatus,
                                                           string densAsiMaritalStatusNote,
                                                           DensAsiNonResponseType<TimeSpan?> yearsAndMonthsWithMaritalStatusTimeSpan,
                                                           string yearsAndMonthsWithMaritalStatusTimeSpanNote,
                                                           DensAsiNonResponseType<DensAsiSatisfaction> maritalStatusDensAsiSatisfaction,
                                                           string maritalStatusDensAsiSatisfactionNote,
                                                           DensAsiNonResponseType<DensAsiLivingArrangementType> pastThreeYearsDensAsiLivingArrangementType,
                                                           string pastThreeYearsDensAsiLivingArrangementTypeNote,
                                                           DensAsiNonResponseType<TimeSpan?> yearsAndMonthsInLivingArrangementTypeTimeSpan,
                                                           string yearsAndMonthsInLivingArrangementTypeTimeSpanNote,
                                                           DensAsiNonResponseType<DensAsiSatisfaction> livingArrangementTypeDensAsiSatisfaction,
                                                           string livingArrangementTypeDensAsiSatisfactionNote,
                                                           DensAsiNonResponseType<bool?> livingWithAnyoneWhoHasAlcoholProblemIndicator,
                                                           string livingWithAnyoneWhoHasAlcoholProblemIndicatorNote,
                                                           DensAsiNonResponseType<bool?> livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator,
                                                           string livingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote,
                                                           DensAsiNonResponseType<DensAsiFreeTimeSpentType> densAsiFreeTimeSpentType,
                                                           string densAsiFreeTimeSpentTypeNote,
                                                           DensAsiNonResponseType<DensAsiSatisfaction> freeTimeSpentTypeDensAsiSatisfaction,
                                                           string freeTimeSpentTypeDensAsiSatisfactionNote,
                                                           DensAsiNonResponseType<int?> closeFriendsCount,
                                                           string closeFriendsCountNote,
                                                           DensAsiHasParentalRelationshipOption motherDensAsiHasParentalRelationshipOption,
                                                           string motherDensAsiHasParentalRelationshipOptionNote,
                                                           DensAsiHasParentalRelationshipOption fatherDensAsiHasParentalRelationshipOption,
                                                           string fatherDensAsiHasParentalRelationshipOptionNote,
                                                           DensAsiHasRelationshipOption brotherSisterDensAsiHasRelationshipOption,
                                                           string brotherSisterDensAsiHasRelationshipOptionNote,
                                                           DensAsiHasRelationshipOption sexualPartnerDensAsiHasRelationshipOption,
                                                           string sexualPartnerDensAsiHasRelationshipOptionNote,
                                                           DensAsiHasRelationshipOption childrenDensAsiHasRelationshipOption,
                                                           string childrenDensAsiHasRelationshipOptionNote,
                                                           DensAsiHasRelationshipOption friendsDensAsiHasRelationshipOption,
                                                           string friendsDensAsiHasRelationshipOptionNote,
                                                           DensAsiNonResponseType<bool?> problemsMotherInLastThirtyDaysIndicator,
                                                           DensAsiNonResponseType<bool?> problemsMotherInLifetimeIndicator,
                                                           string problemsMotherNote,
                                                           DensAsiNonResponseType<bool?> problemsFatherInLastThirtyDaysIndicator,
                                                           DensAsiNonResponseType<bool?> problemsFatherInLifetimeIndicator,
                                                           string problemsFatherNote,
                                                           DensAsiNonResponseType<bool?> problemsBrotherSisterInLastThirtyDaysIndicator,
                                                           DensAsiNonResponseType<bool?> problemsBrotherSisterInLifetimeIndicator,
                                                           string problemsBrotherSisterNote,
                                                           DensAsiNonResponseType<bool?> problemsSexualPartnerInLastThirtyDaysIndicator,
                                                           DensAsiNonResponseType<bool?> problemsSexualPartnerInLifetimeIndicator,
                                                           string problemsSexualPartnerNote,
                                                           DensAsiNonResponseType<bool?> problemsChildrenInLastThirtyDaysIndicator,
                                                           DensAsiNonResponseType<bool?> problemsChildrenInLifetimeIndicator,
                                                           string problemsChildrenNote,
                                                           DensAsiNonResponseType<bool?> problemsOtherSignificantFamilyInLastThirtyDaysIndicator,
                                                           DensAsiNonResponseType<bool?> problemsOtherSignificantFamilyInLifetimeIndicator,
                                                           string problemsOtherSignificantFamilyDescription,
                                                           string problemsOtherSignificantFamilyNote,
                                                           DensAsiNonResponseType<bool?> problemsCloseFriendsInLastThirtyDaysIndicator,
                                                           DensAsiNonResponseType<bool?> problemsCloseFriendsInLifetimeIndicator,
                                                           string problemsCloseFriendsNote,
                                                           DensAsiNonResponseType<bool?> problemsNeighborsInLastThirtyDaysIndicator,
                                                           DensAsiNonResponseType<bool?> problemsNeighborsInLifetimeIndicator,
                                                           string problemsNeighborsNote,
                                                           DensAsiNonResponseType<bool?> problemsCoworkersInLastThirtyDaysIndicator,
                                                           DensAsiNonResponseType<bool?> problemsCoworkersInLifetimeIndicator,
                                                           string problemsCoworkersNote,
                                                           DensAsiNonResponseType<bool?> abusedEmotionallyInLastThirtyDaysIndicator,
                                                           DensAsiNonResponseType<bool?> abusedEmotionallyInLifetimeIndicator,
                                                           string abusedEmotionallyNote,
                                                           DensAsiNonResponseType<bool?> abusedPhysicallyInLastThirtyDaysIndicator,
                                                           DensAsiNonResponseType<bool?> abusedPhysicallyInLifetimeIndicator,
                                                           string abusedPhysicallyNote,
                                                           DensAsiNonResponseType<bool?> abusedSexuallyInLastThirtyDaysIndicator,
                                                           DensAsiNonResponseType<bool?> abusedSexuallyInLifetimeIndicator,
                                                           string abusedSexuallyNote,
                                                           DensAsiNonResponseType<int?> seriousFamilyConflictsInLastThirtyDaysDayCount,
                                                           string seriousFamilyConflictsInLastThirtyDaysDayCountNote,
                                                           DensAsiNonResponseType<DensAsiPatientRating> troubledByFamilyProblemsDensAsiPatientRating,
                                                           string troubledByFamilyProblemsDensAsiPatientRatingNote,
                                                           DensAsiNonResponseType<DensAsiPatientRating> importanceOfFamilyProblemCounselingDensAsiPatientRating,
                                                           string importanceOfFamilyProblemCounselingDensAsiPatientRatingNote,
                                                           DensAsiNonResponseType<int?> conflictsWithOthersInLastThirtyDaysDayCount,
                                                           string conflictsWithOthersInLastThirtyDaysDayCountNote,
                                                           DensAsiNonResponseType<DensAsiPatientRating> troubledBySocialProblemsDensAsiPatientRating,
                                                           string troubledBySocialProblemsDensAsiPatientRatingNote,
                                                           DensAsiNonResponseType<DensAsiPatientRating> importanceOfSocialProblemCounselingDensAsiPatientRating,
                                                           string importanceOfSocialProblemCounselingDensAsiPatientRatingNote,
                                                           DensAsiInterviewerRating patientFamilySocialCounselingDensAsiInterviewerRating,
                                                           string patientFamilySocialCounselingDensAsiInterviewerRatingNote,
                                                           bool? confidenceDistortedByPatientMisrepresentationIndicator,
                                                           string confidenceDistortedByPatientMisrepresentationIndicatorNote,
                                                           bool? confidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                                                           string confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                                                           string sectionNote,
                                                           DensAsiNonResponseType<int?> homelessInLastThirtyDaysDayCount,
                                                           string homelessInLastThirtyDaysDayCountNote,
                                                           DensAsiNonResponseType<int?> shelterInLastThirtyDaysDayCount,
                                                           string shelterInLastThirtyDaysDayCountNote,
                                                           DensAsiNonResponseType<int?> notOwnedHouseInLastThirtyDaysDayCount,
                                                           string notOwnedHouseInLastThirtyDaysDayCountNote,
                                                           DensAsiNonResponseType<int?> hospitalJailInLastThirtyDaysDayCount,
                                                           string hospitalJailInLastThirtyDaysDayCountNote )
        {
            if ( densAsiMaritalStatus.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DensAsiMaritalStatus ).Contains ( densAsiMaritalStatus.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DensAsiMaritalStatus DensAsiNonResponse value '" + densAsiMaritalStatus.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( yearsAndMonthsWithMaritalStatusTimeSpan.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => YearsAndMonthsWithMaritalStatusTimeSpan ).Contains ( yearsAndMonthsWithMaritalStatusTimeSpan.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "YearsAndMonthsWithMaritalStatusTimeSpan DensAsiNonResponse value '" + yearsAndMonthsWithMaritalStatusTimeSpan.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( maritalStatusDensAsiSatisfaction.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => MaritalStatusDensAsiSatisfaction ).Contains ( maritalStatusDensAsiSatisfaction.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "MaritalStatusDensAsiSatisfaction DensAsiNonResponse value '" + maritalStatusDensAsiSatisfaction.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( pastThreeYearsDensAsiLivingArrangementType.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PastThreeYearsDensAsiLivingArrangementType ).Contains ( pastThreeYearsDensAsiLivingArrangementType.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PastThreeYearsDensAsiLivingArrangementType DensAsiNonResponse value '" + pastThreeYearsDensAsiLivingArrangementType.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( yearsAndMonthsInLivingArrangementTypeTimeSpan.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => YearsAndMonthsInLivingArrangementTypeTimeSpan ).Contains ( yearsAndMonthsInLivingArrangementTypeTimeSpan.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "YearsAndMonthsInLivingArrangementTypeTimeSpan DensAsiNonResponse value '" + yearsAndMonthsInLivingArrangementTypeTimeSpan.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( livingArrangementTypeDensAsiSatisfaction.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => LivingArrangementTypeDensAsiSatisfaction ).Contains ( livingArrangementTypeDensAsiSatisfaction.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "LivingArrangementTypeDensAsiSatisfaction DensAsiNonResponse value '" + livingArrangementTypeDensAsiSatisfaction.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( livingWithAnyoneWhoHasAlcoholProblemIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => LivingWithAnyoneWhoHasAlcoholProblemIndicator ).Contains ( livingWithAnyoneWhoHasAlcoholProblemIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "LivingWithAnyoneWhoHasAlcoholProblemIndicator DensAsiNonResponse value '" + livingWithAnyoneWhoHasAlcoholProblemIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => LivingWithAnyoneWhoUsesNonPrescribedDrugsIndicator ).Contains ( livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "LivingWithAnyoneWhoUsesNonPrescribedDrugsIndicator DensAsiNonResponse value '" + livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( densAsiFreeTimeSpentType.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DensAsiFreeTimeSpentType ).Contains ( densAsiFreeTimeSpentType.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DensAsiFreeTimeSpentType DensAsiNonResponse value '" + densAsiFreeTimeSpentType.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( freeTimeSpentTypeDensAsiSatisfaction.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => FreeTimeSpentTypeDensAsiSatisfaction ).Contains ( freeTimeSpentTypeDensAsiSatisfaction.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "FreeTimeSpentTypeDensAsiSatisfaction DensAsiNonResponse value '" + freeTimeSpentTypeDensAsiSatisfaction.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( closeFriendsCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => CloseFriendsCount ).Contains ( closeFriendsCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "CloseFriendsCount DensAsiNonResponse value '" + closeFriendsCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsMotherInLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsMotherInLastThirtyDaysIndicator ).Contains ( problemsMotherInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsMotherInLastThirtyDaysIndicator DensAsiNonResponse value '" + problemsMotherInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsMotherInLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsMotherInLifetimeIndicator ).Contains ( problemsMotherInLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsMotherInLifetimeIndicator DensAsiNonResponse value '" + problemsMotherInLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsFatherInLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsFatherInLastThirtyDaysIndicator ).Contains ( problemsFatherInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsFatherInLastThirtyDaysIndicator DensAsiNonResponse value '" + problemsFatherInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsFatherInLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsFatherInLifetimeIndicator ).Contains ( problemsFatherInLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsFatherInLifetimeIndicator DensAsiNonResponse value '" + problemsFatherInLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsBrotherSisterInLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsBrotherSisterInLastThirtyDaysIndicator ).Contains ( problemsBrotherSisterInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsBrotherSisterInLastThirtyDaysIndicator DensAsiNonResponse value '" + problemsBrotherSisterInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsBrotherSisterInLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsBrotherSisterInLifetimeIndicator ).Contains ( problemsBrotherSisterInLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsBrotherSisterInLifetimeIndicator DensAsiNonResponse value '" + problemsBrotherSisterInLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsSexualPartnerInLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsSexualPartnerInLastThirtyDaysIndicator ).Contains ( problemsSexualPartnerInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsSexualPartnerInLastThirtyDaysIndicator DensAsiNonResponse value '" + problemsSexualPartnerInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsSexualPartnerInLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsSexualPartnerInLifetimeIndicator ).Contains ( problemsSexualPartnerInLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsSexualPartnerInLifetimeIndicator DensAsiNonResponse value '" + problemsSexualPartnerInLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsChildrenInLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsChildrenInLastThirtyDaysIndicator ).Contains ( problemsChildrenInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsChildrenInLastThirtyDaysIndicator DensAsiNonResponse value '" + problemsChildrenInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsChildrenInLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsChildrenInLifetimeIndicator ).Contains ( problemsChildrenInLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsChildrenInLifetimeIndicator DensAsiNonResponse value '" + problemsChildrenInLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsOtherSignificantFamilyInLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsOtherSignificantFamilyInLastThirtyDaysIndicator ).Contains ( problemsOtherSignificantFamilyInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsOtherSignificantFamilyInLastThirtyDaysIndicator DensAsiNonResponse value '" + problemsOtherSignificantFamilyInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsOtherSignificantFamilyInLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsOtherSignificantFamilyInLifetimeIndicator ).Contains ( problemsOtherSignificantFamilyInLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsOtherSignificantFamilyInLifetimeIndicator DensAsiNonResponse value '" + problemsOtherSignificantFamilyInLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsCloseFriendsInLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsCloseFriendsInLastThirtyDaysIndicator ).Contains ( problemsCloseFriendsInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsCloseFriendsInLastThirtyDaysIndicator DensAsiNonResponse value '" + problemsCloseFriendsInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsCloseFriendsInLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsCloseFriendsInLifetimeIndicator ).Contains ( problemsCloseFriendsInLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsCloseFriendsInLifetimeIndicator DensAsiNonResponse value '" + problemsCloseFriendsInLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsNeighborsInLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsNeighborsInLastThirtyDaysIndicator ).Contains ( problemsNeighborsInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsNeighborsInLastThirtyDaysIndicator DensAsiNonResponse value '" + problemsNeighborsInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsNeighborsInLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsNeighborsInLifetimeIndicator ).Contains ( problemsNeighborsInLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsNeighborsInLifetimeIndicator DensAsiNonResponse value '" + problemsNeighborsInLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsCoworkersInLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsCoworkersInLastThirtyDaysIndicator ).Contains ( problemsCoworkersInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsCoworkersInLastThirtyDaysIndicator DensAsiNonResponse value '" + problemsCoworkersInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( problemsCoworkersInLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsCoworkersInLifetimeIndicator ).Contains ( problemsCoworkersInLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProblemsCoworkersInLifetimeIndicator DensAsiNonResponse value '" + problemsCoworkersInLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( abusedEmotionallyInLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AbusedEmotionallyInLastThirtyDaysIndicator ).Contains ( abusedEmotionallyInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AbusedEmotionallyInLastThirtyDaysIndicator DensAsiNonResponse value '" + abusedEmotionallyInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( abusedEmotionallyInLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AbusedEmotionallyInLifetimeIndicator ).Contains ( abusedEmotionallyInLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AbusedEmotionallyInLifetimeIndicator DensAsiNonResponse value '" + abusedEmotionallyInLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( abusedPhysicallyInLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AbusedPhysicallyInLastThirtyDaysIndicator ).Contains ( abusedPhysicallyInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AbusedPhysicallyInLastThirtyDaysIndicator DensAsiNonResponse value '" + abusedPhysicallyInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( abusedPhysicallyInLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AbusedPhysicallyInLifetimeIndicator ).Contains ( abusedPhysicallyInLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AbusedPhysicallyInLifetimeIndicator DensAsiNonResponse value '" + abusedPhysicallyInLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( abusedSexuallyInLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AbusedSexuallyInLastThirtyDaysIndicator ).Contains ( abusedSexuallyInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AbusedSexuallyInLastThirtyDaysIndicator DensAsiNonResponse value '" + abusedSexuallyInLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( abusedSexuallyInLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AbusedSexuallyInLifetimeIndicator ).Contains ( abusedSexuallyInLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AbusedSexuallyInLifetimeIndicator DensAsiNonResponse value '" + abusedSexuallyInLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( seriousFamilyConflictsInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => SeriousFamilyConflictsInLastThirtyDaysDayCount ).Contains ( seriousFamilyConflictsInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "SeriousFamilyConflictsInLastThirtyDaysDayCount DensAsiNonResponse value '" + seriousFamilyConflictsInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( troubledByFamilyProblemsDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TroubledByFamilyProblemsDensAsiPatientRating ).Contains ( troubledByFamilyProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TroubledByFamilyProblemsDensAsiPatientRating DensAsiNonResponse value '" + troubledByFamilyProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( importanceOfFamilyProblemCounselingDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ImportanceOfFamilyProblemCounselingDensAsiPatientRating ).Contains ( importanceOfFamilyProblemCounselingDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ImportanceOfFamilyProblemCounselingDensAsiPatientRating DensAsiNonResponse value '" + importanceOfFamilyProblemCounselingDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( conflictsWithOthersInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ConflictsWithOthersInLastThirtyDaysDayCount ).Contains ( conflictsWithOthersInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ConflictsWithOthersInLastThirtyDaysDayCount DensAsiNonResponse value '" + conflictsWithOthersInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( troubledBySocialProblemsDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TroubledBySocialProblemsDensAsiPatientRating ).Contains ( troubledBySocialProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TroubledBySocialProblemsDensAsiPatientRating DensAsiNonResponse value '" + troubledBySocialProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( importanceOfSocialProblemCounselingDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ImportanceOfSocialProblemCounselingDensAsiPatientRating ).Contains ( importanceOfSocialProblemCounselingDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ImportanceOfSocialProblemCounselingDensAsiPatientRating DensAsiNonResponse value '" + importanceOfSocialProblemCounselingDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( homelessInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HomelessInLastThirtyDaysDayCount ).Contains ( homelessInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HomelessInLastThirtyDaysDayCount DensAsiNonResponse value '" + homelessInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( shelterInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ShelterInLastThirtyDaysDayCount ).Contains ( shelterInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ShelterInLastThirtyDaysDayCount DensAsiNonResponse value '" + shelterInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( notOwnedHouseInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => NotOwnedHouseInLastThirtyDaysDayCount ).Contains ( notOwnedHouseInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "NotOwnedHouseInLastThirtyDaysDayCount DensAsiNonResponse value '" + notOwnedHouseInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( hospitalJailInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HospitalJailInLastThirtyDaysDayCount ).Contains ( hospitalJailInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HospitalJailInLastThirtyDaysDayCount DensAsiNonResponse value '" + hospitalJailInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            _densAsiMaritalStatus = densAsiMaritalStatus;
            _densAsiMaritalStatusNote = densAsiMaritalStatusNote;
            _yearsAndMonthsWithMaritalStatusTimeSpan = yearsAndMonthsWithMaritalStatusTimeSpan;
            _yearsAndMonthsWithMaritalStatusTimeSpanNote = yearsAndMonthsWithMaritalStatusTimeSpanNote;
            _maritalStatusDensAsiSatisfaction = maritalStatusDensAsiSatisfaction;
            _maritalStatusDensAsiSatisfactionNote = maritalStatusDensAsiSatisfactionNote;
            _pastThreeYearsDensAsiLivingArrangementType = pastThreeYearsDensAsiLivingArrangementType;
            _pastThreeYearsDensAsiLivingArrangementTypeNote = pastThreeYearsDensAsiLivingArrangementTypeNote;
            _yearsAndMonthsInLivingArrangementTypeTimeSpan = yearsAndMonthsInLivingArrangementTypeTimeSpan;
            _yearsAndMonthsInLivingArrangementTypeTimeSpanNote = yearsAndMonthsInLivingArrangementTypeTimeSpanNote;
            _livingArrangementTypeDensAsiSatisfaction = livingArrangementTypeDensAsiSatisfaction;
            _livingArrangementTypeDensAsiSatisfactionNote = livingArrangementTypeDensAsiSatisfactionNote;
            _livingWithAnyoneWhoHasAlcoholProblemIndicator = livingWithAnyoneWhoHasAlcoholProblemIndicator;
            _livingWithAnyoneWhoHasAlcoholProblemIndicatorNote = livingWithAnyoneWhoHasAlcoholProblemIndicatorNote;
            _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator = livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator;
            _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote = livingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote;
            _densAsiFreeTimeSpentType = densAsiFreeTimeSpentType;
            _densAsiFreeTimeSpentTypeNote = densAsiFreeTimeSpentTypeNote;
            _freeTimeSpentTypeDensAsiSatisfaction = freeTimeSpentTypeDensAsiSatisfaction;
            _freeTimeSpentTypeDensAsiSatisfactionNote = freeTimeSpentTypeDensAsiSatisfactionNote;
            _closeFriendsCount = closeFriendsCount;
            _closeFriendsCountNote = closeFriendsCountNote;
            _motherDensAsiHasParentalRelationshipOption = motherDensAsiHasParentalRelationshipOption;
            _motherDensAsiHasParentalRelationshipOptionNote = motherDensAsiHasParentalRelationshipOptionNote;
            _fatherDensAsiHasParentalRelationshipOption = fatherDensAsiHasParentalRelationshipOption;
            _fatherDensAsiHasParentalRelationshipOptionNote = fatherDensAsiHasParentalRelationshipOptionNote;
            _brotherSisterDensAsiHasRelationshipOption = brotherSisterDensAsiHasRelationshipOption;
            _brotherSisterDensAsiHasRelationshipOptionNote = brotherSisterDensAsiHasRelationshipOptionNote;
            _sexualPartnerDensAsiHasRelationshipOption = sexualPartnerDensAsiHasRelationshipOption;
            _sexualPartnerDensAsiHasRelationshipOptionNote = sexualPartnerDensAsiHasRelationshipOptionNote;
            _childrenDensAsiHasRelationshipOption = childrenDensAsiHasRelationshipOption;
            _childrenDensAsiHasRelationshipOptionNote = childrenDensAsiHasRelationshipOptionNote;
            _friendsDensAsiHasRelationshipOption = friendsDensAsiHasRelationshipOption;
            _friendsDensAsiHasRelationshipOptionNote = friendsDensAsiHasRelationshipOptionNote;
            _problemsMotherInLastThirtyDaysIndicator = problemsMotherInLastThirtyDaysIndicator;
            _problemsMotherInLifetimeIndicator = problemsMotherInLifetimeIndicator;
            _problemsMotherNote = problemsMotherNote;
            _problemsFatherInLastThirtyDaysIndicator = problemsFatherInLastThirtyDaysIndicator;
            _problemsFatherInLifetimeIndicator = problemsFatherInLifetimeIndicator;
            _problemsFatherNote = problemsFatherNote;
            _problemsBrotherSisterInLastThirtyDaysIndicator = problemsBrotherSisterInLastThirtyDaysIndicator;
            _problemsBrotherSisterInLifetimeIndicator = problemsBrotherSisterInLifetimeIndicator;
            _problemsBrotherSisterNote = problemsBrotherSisterNote;
            _problemsSexualPartnerInLastThirtyDaysIndicator = problemsSexualPartnerInLastThirtyDaysIndicator;
            _problemsSexualPartnerInLifetimeIndicator = problemsSexualPartnerInLifetimeIndicator;
            _problemsSexualPartnerNote = problemsSexualPartnerNote;
            _problemsChildrenInLastThirtyDaysIndicator = problemsChildrenInLastThirtyDaysIndicator;
            _problemsChildrenInLifetimeIndicator = problemsChildrenInLifetimeIndicator;
            _problemsChildrenNote = problemsChildrenNote;
            _problemsOtherSignificantFamilyInLastThirtyDaysIndicator = problemsOtherSignificantFamilyInLastThirtyDaysIndicator;
            _problemsOtherSignificantFamilyInLifetimeIndicator = problemsOtherSignificantFamilyInLifetimeIndicator;
            _problemsOtherSignificantFamilyDescription = problemsOtherSignificantFamilyDescription;
            _problemsOtherSignificantFamilyNote = problemsOtherSignificantFamilyNote;
            _problemsCloseFriendsInLastThirtyDaysIndicator = problemsCloseFriendsInLastThirtyDaysIndicator;
            _problemsCloseFriendsInLifetimeIndicator = problemsCloseFriendsInLifetimeIndicator;
            _problemsCloseFriendsNote = problemsCloseFriendsNote;
            _problemsNeighborsInLastThirtyDaysIndicator = problemsNeighborsInLastThirtyDaysIndicator;
            _problemsNeighborsInLifetimeIndicator = problemsNeighborsInLifetimeIndicator;
            _problemsNeighborsNote = problemsNeighborsNote;
            _problemsCoworkersInLastThirtyDaysIndicator = problemsCoworkersInLastThirtyDaysIndicator;
            _problemsCoworkersInLifetimeIndicator = problemsCoworkersInLifetimeIndicator;
            _problemsCoworkersNote = problemsCoworkersNote;
            _abusedEmotionallyInLastThirtyDaysIndicator = abusedEmotionallyInLastThirtyDaysIndicator;
            _abusedEmotionallyInLifetimeIndicator = abusedEmotionallyInLifetimeIndicator;
            _abusedEmotionallyNote = abusedEmotionallyNote;
            _abusedPhysicallyInLastThirtyDaysIndicator = abusedPhysicallyInLastThirtyDaysIndicator;
            _abusedPhysicallyInLifetimeIndicator = abusedPhysicallyInLifetimeIndicator;
            _abusedPhysicallyNote = abusedPhysicallyNote;
            _abusedSexuallyInLastThirtyDaysIndicator = abusedSexuallyInLastThirtyDaysIndicator;
            _abusedSexuallyInLifetimeIndicator = abusedSexuallyInLifetimeIndicator;
            _abusedSexuallyNote = abusedSexuallyNote;
            _seriousFamilyConflictsInLastThirtyDaysDayCount = seriousFamilyConflictsInLastThirtyDaysDayCount;
            _seriousFamilyConflictsInLastThirtyDaysDayCountNote = seriousFamilyConflictsInLastThirtyDaysDayCountNote;
            _troubledByFamilyProblemsDensAsiPatientRating = troubledByFamilyProblemsDensAsiPatientRating;
            _troubledByFamilyProblemsDensAsiPatientRatingNote = troubledByFamilyProblemsDensAsiPatientRatingNote;
            _importanceOfFamilyProblemCounselingDensAsiPatientRating = importanceOfFamilyProblemCounselingDensAsiPatientRating;
            _importanceOfFamilyProblemCounselingDensAsiPatientRatingNote = importanceOfFamilyProblemCounselingDensAsiPatientRatingNote;
            _conflictsWithOthersInLastThirtyDaysDayCount = conflictsWithOthersInLastThirtyDaysDayCount;
            _conflictsWithOthersInLastThirtyDaysDayCountNote = conflictsWithOthersInLastThirtyDaysDayCountNote;
            _troubledBySocialProblemsDensAsiPatientRating = troubledBySocialProblemsDensAsiPatientRating;
            _troubledBySocialProblemsDensAsiPatientRatingNote = troubledBySocialProblemsDensAsiPatientRatingNote;
            _importanceOfSocialProblemCounselingDensAsiPatientRating = importanceOfSocialProblemCounselingDensAsiPatientRating;
            _importanceOfSocialProblemCounselingDensAsiPatientRatingNote = importanceOfSocialProblemCounselingDensAsiPatientRatingNote;
            _patientFamilySocialCounselingDensAsiInterviewerRating = patientFamilySocialCounselingDensAsiInterviewerRating;
            _patientFamilySocialCounselingDensAsiInterviewerRatingNote = patientFamilySocialCounselingDensAsiInterviewerRatingNote;
            _confidenceDistortedByPatientMisrepresentationIndicator = confidenceDistortedByPatientMisrepresentationIndicator;
            _confidenceDistortedByPatientMisrepresentationIndicatorNote = confidenceDistortedByPatientMisrepresentationIndicatorNote;
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicator = confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote = confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
            _sectionNote = sectionNote;
            _homelessInLastThirtyDaysDayCount = homelessInLastThirtyDaysDayCount;
            _homelessInLastThirtyDaysDayCountNote = homelessInLastThirtyDaysDayCountNote;
            _shelterInLastThirtyDaysDayCount = shelterInLastThirtyDaysDayCount;
            _shelterInLastThirtyDaysDayCountNote = shelterInLastThirtyDaysDayCountNote;
            _notOwnedHouseInLastThirtyDaysDayCount = notOwnedHouseInLastThirtyDaysDayCount;
            _notOwnedHouseInLastThirtyDaysDayCountNote = notOwnedHouseInLastThirtyDaysDayCountNote;
            _hospitalJailInLastThirtyDaysDayCount = hospitalJailInLastThirtyDaysDayCount;
            _hospitalJailInLastThirtyDaysDayCountNote = hospitalJailInLastThirtyDaysDayCountNote;
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMaritalStatus">DensAsiMaritalStatus</see>
        /// patient marital status. Question Number: F1
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiMaritalStatus> DensAsiMaritalStatus
        {
            get { return _densAsiMaritalStatus; }
            private set { }
        }

        /// <summary>
        /// Gets the patient marital status.
        /// Question Number: F1
        /// </summary>
        public virtual string DensAsiMaritalStatusNote
        {
            get { return _densAsiMaritalStatusNote; }
            private set { }
        }

        /// <summary>
        /// Gets the years and months in marital status.
        /// Question Number: F2
        /// </summary>
        public virtual DensAsiNonResponseType<TimeSpan?> YearsAndMonthsWithMaritalStatusTimeSpan
        {
            get { return _yearsAndMonthsWithMaritalStatusTimeSpan; }
            private set { }
        }

        /// <summary>
        /// Gets the years and months in marital status note.
        /// Question Number: F2
        /// </summary>
        public virtual string YearsAndMonthsWithMaritalStatusTimeSpanNote
        {
            get { return _yearsAndMonthsWithMaritalStatusTimeSpanNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating marital status satisfaction.
        /// Question Number: F3
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiSatisfaction> MaritalStatusDensAsiSatisfaction
        {
            get { return _maritalStatusDensAsiSatisfaction; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating marital status satisfaction note.
        /// Question Number: F3
        /// </summary>
        public virtual string MaritalStatusDensAsiSatisfactionNote
        {
            get { return _maritalStatusDensAsiSatisfactionNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLivingArrangementType">PastThreeYearsDensAsiLivingArrangementType</see>
        /// denoting patient living status for the past three years. Question Number: F4
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiLivingArrangementType> PastThreeYearsDensAsiLivingArrangementType
        {
            get { return _pastThreeYearsDensAsiLivingArrangementType; }
            private set { }
        }

        /// <summary>
        /// Gets the patient living status for the past three years note.
        /// Question Number: F4
        /// </summary>
        public virtual string PastThreeYearsDensAsiLivingArrangementTypeNote
        {
            get { return _pastThreeYearsDensAsiLivingArrangementTypeNote; }
            private set { }
        }

        /// <summary>
        /// Gets the years and months in usual living arrangements.
        /// Question Number: F5
        /// </summary>
        public virtual DensAsiNonResponseType<TimeSpan?> YearsAndMonthsInLivingArrangementTypeTimeSpan
        {
            get { return _yearsAndMonthsInLivingArrangementTypeTimeSpan; }
            private set { }
        }

        /// <summary>
        /// Gets the years and months in usual living arrangements note.
        /// Question Number: F5
        /// </summary>
        public virtual string YearsAndMonthsInLivingArrangementTypeTimeSpanNote
        {
            get { return _yearsAndMonthsInLivingArrangementTypeTimeSpanNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the usual living arrangement satisfaction.
        /// Question Number: F6
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiSatisfaction> LivingArrangementTypeDensAsiSatisfaction
        {
            get { return _livingArrangementTypeDensAsiSatisfaction; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the usual living arrangement satisfaction note.
        /// Question Number: F6
        /// </summary>
        public virtual string LivingArrangementTypeDensAsiSatisfactionNote
        {
            get { return _livingArrangementTypeDensAsiSatisfactionNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient is living with anyone who has an alcohol problem.
        /// Question Number: F7
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> LivingWithAnyoneWhoHasAlcoholProblemIndicator
        {
            get { return _livingWithAnyoneWhoHasAlcoholProblemIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient is living with anyone who has an alcohol problem note.
        /// Question Number: F7
        /// </summary>
        public virtual string LivingWithAnyoneWhoHasAlcoholProblemIndicatorNote
        {
            get { return _livingWithAnyoneWhoHasAlcoholProblemIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient is living with anyone who has a drug problem.
        /// Question Number: F8
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> LivingWithAnyoneWhoUsesNonPrescribedDrugsIndicator
        {
            get { return _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient is living with anyone who has a drug problem note.
        /// Question Number: F8
        /// </summary>
        public virtual string LivingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote
        {
            get { return _livingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiFreeTimeSpentType">DensAsiFreeTimeSpentType</see>
        /// denoting with whom the patient spends most of his/her free time. Question Number: F9
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiFreeTimeSpentType> DensAsiFreeTimeSpentType
        {
            get { return _densAsiFreeTimeSpentType; }
            private set { }
        }

        /// <summary>
        /// Get the with whom the patient spends most of his/her free time note.
        /// Question Number: F9
        /// </summary>
        public virtual string DensAsiFreeTimeSpentTypeNote
        {
            get { return _densAsiFreeTimeSpentTypeNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the free time spent with individual satisfaction.
        /// Question Number: F10
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiSatisfaction> FreeTimeSpentTypeDensAsiSatisfaction
        {
            get { return _freeTimeSpentTypeDensAsiSatisfaction; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the free time spent with individual satisfaction note.
        /// Question Number: F10
        /// </summary>
        public virtual string FreeTimeSpentTypeDensAsiSatisfactionNote
        {
            get { return _freeTimeSpentTypeDensAsiSatisfactionNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of close friends.
        /// Question Number: F11
        /// </summary>
        public virtual DensAsiNonResponseType<int?> CloseFriendsCount
        {
            get { return _closeFriendsCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of close friends note.
        /// Question Number: F11
        /// </summary>
        public virtual string CloseFriendsCountNote
        {
            get { return _closeFriendsCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating a reciprocal relationship with mother.
        /// Question Number: F12
        /// </summary>
        public virtual DensAsiHasParentalRelationshipOption MotherDensAsiHasParentalRelationshipOption
        {
            get { return _motherDensAsiHasParentalRelationshipOption; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating a reciprocal relationship with mother note.
        /// Question Number: F12
        /// </summary>
        public virtual string MotherDensAsiHasParentalRelationshipOptionNote
        {
            get { return _motherDensAsiHasParentalRelationshipOptionNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating a reciprocal relationship with father.
        /// Question Number: F13
        /// </summary>
        public virtual DensAsiHasParentalRelationshipOption FatherDensAsiHasParentalRelationshipOption
        {
            get { return _fatherDensAsiHasParentalRelationshipOption; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating a reciprocal relationship with father note.
        /// Question Number: F13
        /// </summary>
        public virtual string FatherDensAsiHasParentalRelationshipOptionNote
        {
            get { return _fatherDensAsiHasParentalRelationshipOptionNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating a reciprocal relationship with sister.
        /// Question Number: F14
        /// </summary>
        public virtual DensAsiHasRelationshipOption BrotherSisterDensAsiHasRelationshipOption
        {
            get { return _brotherSisterDensAsiHasRelationshipOption; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating a reciprocal relationship with sister note.
        /// Question Number: F14
        /// </summary>
        public virtual string BrotherSisterDensAsiHasRelationshipOptionNote
        {
            get { return _brotherSisterDensAsiHasRelationshipOptionNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating a reciprocal relationship with sexual partener.
        /// Question Number: F15
        /// </summary>
        public virtual DensAsiHasRelationshipOption SexualPartnerDensAsiHasRelationshipOption
        {
            get { return _sexualPartnerDensAsiHasRelationshipOption; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating a reciprocal relationship with sexual partener note.
        /// Question Number: F15
        /// </summary>
        public virtual string SexualPartnerDensAsiHasRelationshipOptionNote
        {
            get { return _sexualPartnerDensAsiHasRelationshipOptionNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating a reciprocal relationship with children.
        /// Question Number: F16
        /// </summary>
        public virtual DensAsiHasRelationshipOption ChildrenDensAsiHasRelationshipOption
        {
            get { return _childrenDensAsiHasRelationshipOption; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating a reciprocal relationship with children note.
        /// Question Number: F16
        /// </summary>
        public virtual string ChildrenDensAsiHasRelationshipOptionNote
        {
            get { return _childrenDensAsiHasRelationshipOptionNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating a reciprocal relationship with friends.
        /// Question Number: F17
        /// </summary>
        public virtual DensAsiHasRelationshipOption FriendsDensAsiHasRelationshipOption
        {
            get { return _friendsDensAsiHasRelationshipOption; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating a reciprocal relationship with friends note.
        /// Question Number: F17
        /// </summary>
        public virtual string FriendsDensAsiHasRelationshipOptionNote
        {
            get { return _friendsDensAsiHasRelationshipOptionNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in the past thirty days with mother.
        /// Question Number: F18
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsMotherInLastThirtyDaysIndicator
        {
            get { return _problemsMotherInLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in lifetime with mother.
        /// Question Number: F18
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsMotherInLifetimeIndicator
        {
            get { return _problemsMotherInLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the mother problems note.
        /// Question Number: F18
        /// </summary>
        public virtual string ProblemsMotherNote
        {
            get { return _problemsMotherNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in the past thirty days with father.
        /// Question Number: F19
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsFatherInLastThirtyDaysIndicator
        {
            get { return _problemsFatherInLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in lifetime with father.
        /// Question Number: F19
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsFatherInLifetimeIndicator
        {
            get { return _problemsFatherInLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the father problems note.
        /// Question Number: F19
        /// </summary>
        public virtual string ProblemsFatherNote
        {
            get { return _problemsFatherNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in the past thirty days with brother or sister.
        /// Question Number: F20
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsBrotherSisterInLastThirtyDaysIndicator
        {
            get { return _problemsBrotherSisterInLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in lifetime with brother or sister.
        /// Question Number: F20
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsBrotherSisterInLifetimeIndicator
        {
            get { return _problemsBrotherSisterInLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the brother or sister problems note.
        /// Question Number: F20
        /// </summary>
        public virtual string ProblemsBrotherSisterNote
        {
            get { return _problemsBrotherSisterNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in the past thirty days with sexual partner.
        /// Question Number: F21
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsSexualPartnerInLastThirtyDaysIndicator
        {
            get { return _problemsSexualPartnerInLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in lifetime with sexual partner.
        /// Question Number: F21
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsSexualPartnerInLifetimeIndicator
        {
            get { return _problemsSexualPartnerInLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the sexual partner problems note.
        /// Question Number: F21
        /// </summary>
        public virtual string ProblemsSexualPartnerNote
        {
            get { return _problemsSexualPartnerNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in the past thirty days with children.
        /// Question Number: F22
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsChildrenInLastThirtyDaysIndicator
        {
            get { return _problemsChildrenInLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in lifetime with children.
        /// Question Number: F22
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsChildrenInLifetimeIndicator
        {
            get { return _problemsChildrenInLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the children problems note.
        /// Question Number: F22
        /// </summary>
        public virtual string ProblemsChildrenNote
        {
            get { return _problemsChildrenNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in the past thirty days with other significant family.
        /// Question Number: F23
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsOtherSignificantFamilyInLastThirtyDaysIndicator
        {
            get { return _problemsOtherSignificantFamilyInLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in lifetime with other significant family.
        /// Question Number: F23
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsOtherSignificantFamilyInLifetimeIndicator
        {
            get { return _problemsOtherSignificantFamilyInLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the other significant family description.
        /// Question Number: F23
        /// </summary>
        public virtual string ProblemsOtherSignificantFamilyDescription
        {
            get { return _problemsOtherSignificantFamilyDescription; }
            private set { }
        }

        /// <summary>
        /// Gets the other significant family note.
        /// Question Number: F23
        /// </summary>
        public virtual string ProblemsOtherSignificantFamilyNote
        {
            get { return _problemsOtherSignificantFamilyNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in the past thirty days with close friends.
        /// Question Number: F24
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsCloseFriendsInLastThirtyDaysIndicator
        {
            get { return _problemsCloseFriendsInLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in lifetime with close friends.
        /// Question Number: F24
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsCloseFriendsInLifetimeIndicator
        {
            get { return _problemsCloseFriendsInLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the friends problems close note.
        /// Question Number: F24
        /// </summary>
        public virtual string ProblemsCloseFriendsNote
        {
            get { return _problemsCloseFriendsNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in the past thirty days with neighbors.
        /// Question Number: F25
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsNeighborsInLastThirtyDaysIndicator
        {
            get { return _problemsNeighborsInLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in lifetime with neighbors.
        /// Question Number: F25
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsNeighborsInLifetimeIndicator
        {
            get { return _problemsNeighborsInLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the neighbors problems note.
        /// Question Number: F25
        /// </summary>
        public virtual string ProblemsNeighborsNote
        {
            get { return _problemsNeighborsNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in the past thirty days with coworkers.
        /// Question Number: F26
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsCoworkersInLastThirtyDaysIndicator
        {
            get { return _problemsCoworkersInLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating serious problems in lifetime with coworkers.
        /// Question Number: F26
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProblemsCoworkersInLifetimeIndicator
        {
            get { return _problemsCoworkersInLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the coworkers problems note.
        /// Question Number: F26
        /// </summary>
        public virtual string ProblemsCoworkersNote
        {
            get { return _problemsCoworkersNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient was in last thirty days abused emotionally.
        /// Question Number: F27
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AbusedEmotionallyInLastThirtyDaysIndicator
        {
            get { return _abusedEmotionallyInLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient was in lifetime abused emotionally.
        /// Question Number: F27
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AbusedEmotionallyInLifetimeIndicator
        {
            get { return _abusedEmotionallyInLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the abused emotionally note.
        /// Question Number: F27
        /// </summary>
        public virtual string AbusedEmotionallyNote
        {
            get { return _abusedEmotionallyNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient was in last thirty days abused physically.
        /// Question Number: F28
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AbusedPhysicallyInLastThirtyDaysIndicator
        {
            get { return _abusedPhysicallyInLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient was in lifetime abused physically.
        /// Question Number: F28
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AbusedPhysicallyInLifetimeIndicator
        {
            get { return _abusedPhysicallyInLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the abused physically note.
        /// Question Number: F28
        /// </summary>
        public virtual string AbusedPhysicallyNote
        {
            get { return _abusedPhysicallyNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient was in last thirty days abused sexually.
        /// Question Number: F29
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AbusedSexuallyInLastThirtyDaysIndicator
        {
            get { return _abusedSexuallyInLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient was in lifetime abused sexually.
        /// Question Number: F29
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AbusedSexuallyInLifetimeIndicator
        {
            get { return _abusedSexuallyInLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the abused sexually note.
        /// Question Number: F29
        /// </summary>
        public virtual string AbusedSexuallyNote
        {
            get { return _abusedSexuallyNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days of serious family conflicts in last thirty days.
        /// Question Number: F30
        /// </summary>
        public virtual DensAsiNonResponseType<int?> SeriousFamilyConflictsInLastThirtyDaysDayCount
        {
            get { return _seriousFamilyConflictsInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days of serious family conflicts in last thirty days note.
        /// Question Number: F30
        /// </summary>
        public virtual string SeriousFamilyConflictsInLastThirtyDaysDayCountNote
        {
            get { return _seriousFamilyConflictsInLastThirtyDaysDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">TroubledByFamilyProblemsDensAsiPatientRating</see>
        /// denoting how troubled the patient has been by family problems in the last thirty days. Question Number: F32
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> TroubledByFamilyProblemsDensAsiPatientRating
        {
            get { return _troubledByFamilyProblemsDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the how troubled the patient has been by family problems in the last thirty days note.
        /// Question Number: F32
        /// </summary>
        public virtual string TroubledByFamilyProblemsDensAsiPatientRatingNote
        {
            get { return _troubledByFamilyProblemsDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">ImportanceOfFamilyProblemCounselingDensAsiPatientRating</see>
        /// denoting the importance of family problem counseling. Question Number: F34
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> ImportanceOfFamilyProblemCounselingDensAsiPatientRating
        {
            get { return _importanceOfFamilyProblemCounselingDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the importance of family problem counseling note.
        /// Question Number: F34
        /// </summary>
        public virtual string ImportanceOfFamilyProblemCounselingDensAsiPatientRatingNote
        {
            get { return _importanceOfFamilyProblemCounselingDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days of conflicts with others in last thirty days.
        /// Question Number: F31
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ConflictsWithOthersInLastThirtyDaysDayCount
        {
            get { return _conflictsWithOthersInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days of conflicts with others in last thirty days note.
        /// Question Number: F31
        /// </summary>
        public virtual string ConflictsWithOthersInLastThirtyDaysDayCountNote
        {
            get { return _conflictsWithOthersInLastThirtyDaysDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">TroubledBySocialProblemsDensAsiPatientRating</see>
        /// denoting how troubled By social problems in last thirty days. Question Number: F33
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> TroubledBySocialProblemsDensAsiPatientRating
        {
            get { return _troubledBySocialProblemsDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets how troubled By social problems in last thirty days note.
        /// Question Number: F33
        /// </summary>
        public virtual string TroubledBySocialProblemsDensAsiPatientRatingNote
        {
            get { return _troubledBySocialProblemsDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">ImportanceOfSocialProblemCounselingDensAsiPatientRating</see>
        /// denoting importance of social problem counseling. Question Number: F35
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> ImportanceOfSocialProblemCounselingDensAsiPatientRating
        {
            get { return _importanceOfSocialProblemCounselingDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the importance of social problem counseling note.
        /// Question Number: F35
        /// </summary>
        public virtual string ImportanceOfSocialProblemCounselingDensAsiPatientRatingNote
        {
            get { return _importanceOfSocialProblemCounselingDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">PatientFamilySocialCounselingDensAsiInterviewerRating</see>
        /// denoting interviewer rating of patient need for family social counseling. Question Number: F36
        /// </summary>
        public virtual DensAsiInterviewerRating PatientFamilySocialCounselingDensAsiInterviewerRating
        {
            get { return _patientFamilySocialCounselingDensAsiInterviewerRating; }
            private set { }
        }

        /// <summary>
        /// Gets interviewer rating of patient need for family social counseling note.
        /// Question Number: F36
        /// </summary>
        public virtual string PatientFamilySocialCounselingDensAsiInterviewerRatingNote
        {
            get { return _patientFamilySocialCounselingDensAsiInterviewerRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient misrepresentation.
        /// Question Number: F37
        /// </summary>
        public virtual bool? ConfidenceDistortedByPatientMisrepresentationIndicator
        {
            get { return _confidenceDistortedByPatientMisrepresentationIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient misrepresentation note.
        /// Question Number: F37
        /// </summary>
        public virtual string ConfidenceDistortedByPatientMisrepresentationIndicatorNote
        {
            get { return _confidenceDistortedByPatientMisrepresentationIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient inability to understand.
        /// Question Number: F38
        /// </summary>
        public virtual bool? ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator
        {
            get { return _confidenceRateDistortedByPatientInabilityToUnderstandIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient inability to understand note.
        /// Question Number: F38
        /// </summary>
        public virtual string ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote
        {
            get { return _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the section note.
        /// </summary>
        public virtual string SectionNote
        {
            get { return _sectionNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days homeless in last thirty days.
        /// Question Number: F113
        /// </summary>
        public virtual DensAsiNonResponseType<int?> HomelessInLastThirtyDaysDayCount
        {
            get { return _homelessInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days homeless in last thirty days note.
        /// Question Number: F113
        /// </summary>
        public virtual string HomelessInLastThirtyDaysDayCountNote
        {
            get { return _homelessInLastThirtyDaysDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in shelter in last thirty days.
        /// Question Number: F114
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ShelterInLastThirtyDaysDayCount
        {
            get { return _shelterInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in shelter in last thirty days note.
        /// Question Number: F114
        /// </summary>
        public virtual string ShelterInLastThirtyDaysDayCountNote
        {
            get { return _shelterInLastThirtyDaysDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days stayed in house not owned in last thirty days.
        /// Question Number: F115
        /// </summary>
        public virtual DensAsiNonResponseType<int?> NotOwnedHouseInLastThirtyDaysDayCount
        {
            get { return _notOwnedHouseInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days stayed in house not owned in last thirty days note.
        /// Question Number: F115
        /// </summary>
        public virtual string NotOwnedHouseInLastThirtyDaysDayCountNote
        {
            get { return _notOwnedHouseInLastThirtyDaysDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days stayed in hospital jail in last thirty days.
        /// Question Number: F116
        /// </summary>
        public virtual DensAsiNonResponseType<int?> HospitalJailInLastThirtyDaysDayCount
        {
            get { return _hospitalJailInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days stayed in hospital jail in last thirty days note.
        /// Question Number: F116
        /// </summary>
        public virtual string HospitalJailInLastThirtyDaysDayCountNote
        {
            get { return _hospitalJailInLastThirtyDaysDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the possible dens asi non response well known names.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1">
        /// IEnumerable&lt;string&gt;</see>.
        /// </returns>
        public override IEnumerable<string> GetPossibleDensAsiNonResponseWellKnownNames<TProperty> ( Expression<Func<TProperty>> propertyExpression )
        {
            IEnumerable<string> possibleDensAsiNonResponseWellKnownNames;
            string propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );

            if ( propertyName == PropertyUtil.ExtractPropertyName ( () => HomelessInLastThirtyDaysDayCountNote )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => ShelterInLastThirtyDaysDayCount )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => NotOwnedHouseInLastThirtyDaysDayCount )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => HospitalJailInLastThirtyDaysDayCount ) )
            {
                possibleDensAsiNonResponseWellKnownNames = new List<string>
                                                               {
                                                                   WellKnownNames.DensAsiModule.DensAsiNonResponse.NotAnswered,
                                                                   WellKnownNames.DensAsiModule.DensAsiNonResponse.NotApplicable
                                                               };
            }
            else if ( propertyName == PropertyUtil.ExtractPropertyName ( () => MaritalStatusDensAsiSatisfaction )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => LivingArrangementTypeDensAsiSatisfaction )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => FreeTimeSpentTypeDensAsiSatisfaction ) )

            {
                possibleDensAsiNonResponseWellKnownNames = new List<string>
                                                               {
                                                                   WellKnownNames.DensAsiModule.DensAsiNonResponse.NotAnswered
                                                               };
            }
            else if ( propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsMotherInLastThirtyDaysIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsMotherInLifetimeIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsFatherInLastThirtyDaysIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsFatherInLifetimeIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsBrotherSisterInLastThirtyDaysIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsBrotherSisterInLifetimeIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsSexualPartnerInLastThirtyDaysIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsSexualPartnerInLifetimeIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsChildrenInLastThirtyDaysIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsChildrenInLifetimeIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsOtherSignificantFamilyInLastThirtyDaysIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsOtherSignificantFamilyInLifetimeIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsCloseFriendsInLastThirtyDaysIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsCloseFriendsInLifetimeIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsNeighborsInLastThirtyDaysIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsNeighborsInLifetimeIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsCoworkersInLastThirtyDaysIndicator )
                      || propertyName == PropertyUtil.ExtractPropertyName ( () => ProblemsCoworkersInLifetimeIndicator ) )
            {
                possibleDensAsiNonResponseWellKnownNames = new List<string>
                                                               {
                                                                   WellKnownNames.DensAsiModule.DensAsiNonResponse.NotApplicable
                                                               };
            }
            else
            {
                possibleDensAsiNonResponseWellKnownNames = base.GetPossibleDensAsiNonResponseWellKnownNames ( propertyExpression );
            }

            return possibleDensAsiNonResponseWellKnownNames;
        }

        /// <summary>
        /// Gets the well known names filters dictionary.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IDictionary`2">Dictionary&lt;string,
        /// IEnumerable&lt;string&gt;</see>
        /// </returns>
        public override Dictionary<string, IEnumerable<string>> GetFiltersDictionary ()
        {
            return new Dictionary<string, IEnumerable<string>>
                       {
                           { PropertyUtil.ExtractPropertyName ( () => HomelessInLastThirtyDaysDayCountNote ), GetPossibleDensAsiNonResponseWellKnownNames ( () => HomelessInLastThirtyDaysDayCountNote ) },
                           { PropertyUtil.ExtractPropertyName ( () => ShelterInLastThirtyDaysDayCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ShelterInLastThirtyDaysDayCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => NotOwnedHouseInLastThirtyDaysDayCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => NotOwnedHouseInLastThirtyDaysDayCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => HospitalJailInLastThirtyDaysDayCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => HospitalJailInLastThirtyDaysDayCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => MaritalStatusDensAsiSatisfaction ), GetPossibleDensAsiNonResponseWellKnownNames ( () => MaritalStatusDensAsiSatisfaction ) },
                           { PropertyUtil.ExtractPropertyName ( () => LivingArrangementTypeDensAsiSatisfaction ), GetPossibleDensAsiNonResponseWellKnownNames ( () => LivingArrangementTypeDensAsiSatisfaction ) },
                           { PropertyUtil.ExtractPropertyName ( () => FreeTimeSpentTypeDensAsiSatisfaction ), GetPossibleDensAsiNonResponseWellKnownNames ( () => FreeTimeSpentTypeDensAsiSatisfaction ) },
                           { PropertyUtil.ExtractPropertyName ( () => MotherDensAsiHasParentalRelationshipOption ), GetPossibleDensAsiNonResponseWellKnownNames ( () => MotherDensAsiHasParentalRelationshipOption ) },
                           { PropertyUtil.ExtractPropertyName ( () => FatherDensAsiHasParentalRelationshipOption ), GetPossibleDensAsiNonResponseWellKnownNames ( () => FatherDensAsiHasParentalRelationshipOption ) },
                           { PropertyUtil.ExtractPropertyName ( () => BrotherSisterDensAsiHasRelationshipOption ), GetPossibleDensAsiNonResponseWellKnownNames ( () => BrotherSisterDensAsiHasRelationshipOption ) },
                           { PropertyUtil.ExtractPropertyName ( () => SexualPartnerDensAsiHasRelationshipOption ), GetPossibleDensAsiNonResponseWellKnownNames ( () => SexualPartnerDensAsiHasRelationshipOption ) },
                           { PropertyUtil.ExtractPropertyName ( () => ChildrenDensAsiHasRelationshipOption ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ChildrenDensAsiHasRelationshipOption ) },
                           { PropertyUtil.ExtractPropertyName ( () => FriendsDensAsiHasRelationshipOption ), GetPossibleDensAsiNonResponseWellKnownNames ( () => FriendsDensAsiHasRelationshipOption ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsMotherInLastThirtyDaysIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsMotherInLastThirtyDaysIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsMotherInLifetimeIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsMotherInLifetimeIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsFatherInLastThirtyDaysIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsFatherInLastThirtyDaysIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsFatherInLifetimeIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsFatherInLifetimeIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsBrotherSisterInLastThirtyDaysIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsBrotherSisterInLastThirtyDaysIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsBrotherSisterInLifetimeIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsBrotherSisterInLifetimeIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsSexualPartnerInLastThirtyDaysIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsSexualPartnerInLastThirtyDaysIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsSexualPartnerInLifetimeIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsSexualPartnerInLifetimeIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsChildrenInLastThirtyDaysIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsChildrenInLastThirtyDaysIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsChildrenInLifetimeIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsChildrenInLifetimeIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsOtherSignificantFamilyInLastThirtyDaysIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsOtherSignificantFamilyInLastThirtyDaysIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsOtherSignificantFamilyInLifetimeIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsOtherSignificantFamilyInLifetimeIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsCloseFriendsInLastThirtyDaysIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsCloseFriendsInLastThirtyDaysIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsCloseFriendsInLifetimeIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsCloseFriendsInLifetimeIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsNeighborsInLastThirtyDaysIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsNeighborsInLastThirtyDaysIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsNeighborsInLifetimeIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsNeighborsInLifetimeIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsCoworkersInLastThirtyDaysIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsCoworkersInLastThirtyDaysIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => ProblemsCoworkersInLifetimeIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ProblemsCoworkersInLifetimeIndicator ) },
                       };
        }
    }
}