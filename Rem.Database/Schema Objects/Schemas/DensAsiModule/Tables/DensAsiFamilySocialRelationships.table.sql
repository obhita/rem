﻿CREATE TABLE [DensAsiModule].[DensAsiFamilySocialRelationships] (
    [DensAsiInterviewKey]                                                             BIGINT             NOT NULL,
    [CreatedTimestamp]                                                                DATETIMEOFFSET (7) NOT NULL,
    [UpdatedTimestamp]                                                                DATETIMEOFFSET (7) NOT NULL,
    [CreatedBySystemAccountKey]                                                       BIGINT             NOT NULL,
    [UpdatedBySystemAccountKey]                                                       BIGINT             NOT NULL,
    [DensAsiMaritalStatusNote]                                                        NVARCHAR (MAX)     NULL,
    [YearsAndMonthsWithMaritalStatusTimeSpanNote]                                     NVARCHAR (MAX)     NULL,
    [MaritalStatusDensAsiSatisfactionNote]                                            NVARCHAR (MAX)     NULL,
    [PastThreeYearsDensAsiLivingArrangementTypeNote]                                  NVARCHAR (MAX)     NULL,
    [YearsAndMonthsInLivingArrangementTypeTimeSpanNote]                               NVARCHAR (MAX)     NULL,
    [LivingArrangementTypeDensAsiSatisfactionNote]                                    NVARCHAR (MAX)     NULL,
    [LivingWithAnyoneWhoHasAlcoholProblemIndicatorNote]                               NVARCHAR (MAX)     NULL,
    [LivingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote]                          NVARCHAR (MAX)     NULL,
    [DensAsiFreeTimeSpentTypeNote]                                                    NVARCHAR (MAX)     NULL,
    [FreeTimeSpentTypeDensAsiSatisfactionNote]                                        NVARCHAR (MAX)     NULL,
    [CloseFriendsCountNote]                                                           NVARCHAR (MAX)     NULL,
    [MotherDensAsiHasParentalRelationshipOptionNote]                                  NVARCHAR (MAX)     NULL,
    [FatherDensAsiHasParentalRelationshipOptionNote]                                  NVARCHAR (MAX)     NULL,
    [BrotherSisterDensAsiHasRelationshipOptionNote]                                   NVARCHAR (MAX)     NULL,
    [SexualPartnerDensAsiHasRelationshipOptionNote]                                   NVARCHAR (MAX)     NULL,
    [ChildrenDensAsiHasRelationshipOptionNote]                                        NVARCHAR (MAX)     NULL,
    [FriendsDensAsiHasRelationshipOptionNote]                                         NVARCHAR (MAX)     NULL,
    [ProblemsMotherNote]                                                              NVARCHAR (MAX)     NULL,
    [ProblemsFatherNote]                                                              NVARCHAR (MAX)     NULL,
    [ProblemsBrotherSisterNote]                                                       NVARCHAR (MAX)     NULL,
    [ProblemsSexualPartnerNote]                                                       NVARCHAR (MAX)     NULL,
    [ProblemsChildrenNote]                                                            NVARCHAR (MAX)     NULL,
    [ProblemsOtherSignificantFamilyDescription]                                       NVARCHAR (500)     NULL,
    [ProblemsOtherSignificantFamilyNote]                                              NVARCHAR (MAX)     NULL,
    [ProblemsCloseFriendsNote]                                                        NVARCHAR (MAX)     NULL,
    [ProblemsNeighborsNote]                                                           NVARCHAR (MAX)     NULL,
    [ProblemsCoworkersNote]                                                           NVARCHAR (MAX)     NULL,
    [AbusedEmotionallyNote]                                                           NVARCHAR (MAX)     NULL,
    [AbusedPhysicallyNote]                                                            NVARCHAR (MAX)     NULL,
    [AbusedSexuallyNote]                                                              NVARCHAR (MAX)     NULL,
    [SeriousFamilyConflictsInLastThirtyDaysDayCountNote]                              NVARCHAR (MAX)     NULL,
    [TroubledByFamilyProblemsDensAsiPatientRatingNote]                                NVARCHAR (MAX)     NULL,
    [ImportanceOfFamilyProblemCounselingDensAsiPatientRatingNote]                     NVARCHAR (MAX)     NULL,
    [ConflictsWithOthersInLastThirtyDaysDayCountNote]                                 NVARCHAR (MAX)     NULL,
    [TroubledBySocialProblemsDensAsiPatientRatingNote]                                NVARCHAR (MAX)     NULL,
    [ImportanceOfSocialProblemCounselingDensAsiPatientRatingNote]                     NVARCHAR (MAX)     NULL,
    [PatientFamilySocialCounselingDensAsiInterviewerRatingNote]                       NVARCHAR (MAX)     NULL,
    [ConfidenceDistortedByPatientMisrepresentationIndicator]                          BIT                NULL,
    [ConfidenceDistortedByPatientMisrepresentationIndicatorNote]                      NVARCHAR (MAX)     NULL,
    [ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator]                  BIT                NULL,
    [ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote]              NVARCHAR (MAX)     NULL,
    [SectionNote]                                                                     NVARCHAR (MAX)     NULL,
    [HomelessInLastThirtyDaysDayCountNote]                                            NVARCHAR (MAX)     NULL,
    [ShelterInLastThirtyDaysDayCountNote]                                             NVARCHAR (MAX)     NULL,
    [NotOwnedHouseInLastThirtyDaysDayCountNote]                                       NVARCHAR (MAX)     NULL,
    [HospitalJailInLastThirtyDaysDayCountNote]                                        NVARCHAR (MAX)     NULL,
    [MotherDensAsiHasParentalRelationshipOptionLkpKey]                                BIGINT             NULL,
    [FatherDensAsiHasParentalRelationshipOptionLkpKey]                                BIGINT             NULL,
    [BrotherSisterDensAsiHasRelationshipOptionLkpKey]                                 BIGINT             NULL,
    [SexualPartnerDensAsiHasRelationshipOptionLkpKey]                                 BIGINT             NULL,
    [ChildrenDensAsiHasRelationshipOptionLkpKey]                                      BIGINT             NULL,
    [FriendsDensAsiHasRelationshipOptionLkpKey]                                       BIGINT             NULL,
    [PatientFamilySocialCounselingDensAsiInterviewerRatingLkpKey]                     BIGINT             NULL,
    [DensAsiMaritalStatusLkpKey]                                                      BIGINT             NULL,
    [DensAsiMaritalStatusDensAsiNonResponseLkpKey]                                    BIGINT             NULL,
    [YearsAndMonthsWithMaritalStatusTimeSpan]                                         BIGINT             NULL,
    [YearsAndMonthsWithMaritalStatusTimeSpanDensAsiNonResponseLkpKey]                 BIGINT             NULL,
    [MaritalStatusDensAsiSatisfactionLkpKey]                                          BIGINT             NULL,
    [MaritalStatusDensAsiSatisfactionDensAsiNonResponseLkpKey]                        BIGINT             NULL,
    [PastThreeYearsDensAsiLivingArrangementTypeLkpKey]                                BIGINT             NULL,
    [PastThreeYearsDensAsiLivingArrangementTypeDensAsiNonResponseLkpKey]              BIGINT             NULL,
    [YearsAndMonthsInLivingArrangementTypeTimeSpan]                                   BIGINT             NULL,
    [YearsAndMonthsInLivingArrangementTypeTimeSpanDensAsiNonResponseLkpKey]           BIGINT             NULL,
    [LivingArrangementTypeDensAsiSatisfactionLkpKey]                                  BIGINT             NULL,
    [LivingArrangementTypeDensAsiSatisfactionDensAsiNonResponseLkpKey]                BIGINT             NULL,
    [LivingWithAnyoneWhoHasAlcoholProblemIndicator]                                   BIT                NULL,
    [LivingWithAnyoneWhoHasAlcoholProblemIndicatorDensAsiNonResponseLkpKey]           BIGINT             NULL,
    [LivingWithAnyoneWhoUsesNonPrescribedDrugsIndicator]                              BIT                NULL,
    [LivingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorDensAsiNonResponseLkpKey]      BIGINT             NULL,
    [DensAsiFreeTimeSpentTypeLkpKey]                                                  BIGINT             NULL,
    [DensAsiFreeTimeSpentTypeDensAsiNonResponseLkpKey]                                BIGINT             NULL,
    [FreeTimeSpentTypeDensAsiSatisfactionLkpKey]                                      BIGINT             NULL,
    [FreeTimeSpentTypeDensAsiSatisfactionDensAsiNonResponseLkpKey]                    BIGINT             NULL,
    [CloseFriendsCount]                                                               INT                NULL,
    [CloseFriendsCountDensAsiNonResponseLkpKey]                                       BIGINT             NULL,
    [ProblemsMotherInLastThirtyDaysIndicator]                                         BIT                NULL,
    [ProblemsMotherInLastThirtyDaysIndicatorDensAsiNonResponseLkpKey]                 BIGINT             NULL,
    [ProblemsMotherInLifetimeIndicator]                                               BIT                NULL,
    [ProblemsMotherInLifetimeIndicatorDensAsiNonResponseLkpKey]                       BIGINT             NULL,
    [ProblemsFatherInLastThirtyDaysIndicator]                                         BIT                NULL,
    [ProblemsFatherInLastThirtyDaysIndicatorDensAsiNonResponseLkpKey]                 BIGINT             NULL,
    [ProblemsFatherInLifetimeIndicator]                                               BIT                NULL,
    [ProblemsFatherInLifetimeIndicatorDensAsiNonResponseLkpKey]                       BIGINT             NULL,
    [ProblemsBrotherSisterInLastThirtyDaysIndicator]                                  BIT                NULL,
    [ProblemsBrotherSisterInLastThirtyDaysIndicatorDensAsiNonResponseLkpKey]          BIGINT             NULL,
    [ProblemsBrotherSisterInLifetimeIndicator]                                        BIT                NULL,
    [ProblemsBrotherSisterInLifetimeIndicatorDensAsiNonResponseLkpKey]                BIGINT             NULL,
    [ProblemsSexualPartnerInLastThirtyDaysIndicator]                                  BIT                NULL,
    [ProblemsSexualPartnerInLastThirtyDaysIndicatorDensAsiNonResponseLkpKey]          BIGINT             NULL,
    [ProblemsSexualPartnerInLifetimeIndicator]                                        BIT                NULL,
    [ProblemsSexualPartnerInLifetimeIndicatorDensAsiNonResponseLkpKey]                BIGINT             NULL,
    [ProblemsChildrenInLastThirtyDaysIndicator]                                       BIT                NULL,
    [ProblemsChildrenInLastThirtyDaysIndicatorDensAsiNonResponseLkpKey]               BIGINT             NULL,
    [ProblemsChildrenInLifetimeIndicator]                                             BIT                NULL,
    [ProblemsChildrenInLifetimeIndicatorDensAsiNonResponseLkpKey]                     BIGINT             NULL,
    [ProblemsOtherSignificantFamilyInLastThirtyDaysIndicator]                         BIT                NULL,
    [ProblemsOtherSignificantFamilyInLastThirtyDaysIndicatorDensAsiNonResponseLkpKey] BIGINT             NULL,
    [ProblemsOtherSignificantFamilyInLifetimeIndicator]                               BIT                NULL,
    [ProblemsOtherSignificantFamilyInLifetimeIndicatorDensAsiNonResponseLkpKey]       BIGINT             NULL,
    [ProblemsCloseFriendsInLastThirtyDaysIndicator]                                   BIT                NULL,
    [ProblemsCloseFriendsInLastThirtyDaysIndicatorDensAsiNonResponseLkpKey]           BIGINT             NULL,
    [ProblemsCloseFriendsInLifetimeIndicator]                                         BIT                NULL,
    [ProblemsCloseFriendsInLifetimeIndicatorDensAsiNonResponseLkpKey]                 BIGINT             NULL,
    [ProblemsNeighborsInLastThirtyDaysIndicator]                                      BIT                NULL,
    [ProblemsNeighborsInLastThirtyDaysIndicatorDensAsiNonResponseLkpKey]              BIGINT             NULL,
    [ProblemsNeighborsInLifetimeIndicator]                                            BIT                NULL,
    [ProblemsNeighborsInLifetimeIndicatorDensAsiNonResponseLkpKey]                    BIGINT             NULL,
    [ProblemsCoworkersInLastThirtyDaysIndicator]                                      BIT                NULL,
    [ProblemsCoworkersInLastThirtyDaysIndicatorDensAsiNonResponseLkpKey]              BIGINT             NULL,
    [ProblemsCoworkersInLifetimeIndicator]                                            BIT                NULL,
    [ProblemsCoworkersInLifetimeIndicatorDensAsiNonResponseLkpKey]                    BIGINT             NULL,
    [AbusedEmotionallyInLastThirtyDaysIndicator]                                      BIT                NULL,
    [AbusedEmotionallyInLastThirtyDaysIndicatorDensAsiNonResponseLkpKey]              BIGINT             NULL,
    [AbusedEmotionallyInLifetimeIndicator]                                            BIT                NULL,
    [AbusedEmotionallyInLifetimeIndicatorDensAsiNonResponseLkpKey]                    BIGINT             NULL,
    [AbusedPhysicallyInLastThirtyDaysIndicator]                                       BIT                NULL,
    [AbusedPhysicallyInLastThirtyDaysIndicatorDensAsiNonResponseLkpKey]               BIGINT             NULL,
    [AbusedPhysicallyInLifetimeIndicator]                                             BIT                NULL,
    [AbusedPhysicallyInLifetimeIndicatorDensAsiNonResponseLkpKey]                     BIGINT             NULL,
    [AbusedSexuallyInLastThirtyDaysIndicator]                                         BIT                NULL,
    [AbusedSexuallyInLastThirtyDaysIndicatorDensAsiNonResponseLkpKey]                 BIGINT             NULL,
    [AbusedSexuallyInLifetimeIndicator]                                               BIT                NULL,
    [AbusedSexuallyInLifetimeIndicatorDensAsiNonResponseLkpKey]                       BIGINT             NULL,
    [SeriousFamilyConflictsInLastThirtyDaysDayCount]                                  INT                NULL,
    [SeriousFamilyConflictsInLastThirtyDaysDayCountDensAsiNonResponseLkpKey]          BIGINT             NULL,
    [TroubledByFamilyProblemsDensAsiPatientRatingLkpKey]                              BIGINT             NULL,
    [TroubledByFamilyProblemsDensAsiPatientRatingDensAsiNonResponseLkpKey]            BIGINT             NULL,
    [ImportanceOfFamilyProblemCounselingDensAsiPatientRatingLkpKey]                   BIGINT             NULL,
    [ImportanceOfFamilyProblemCounselingDensAsiPatientRatingDensAsiNonResponseLkpKey] BIGINT             NULL,
    [ConflictsWithOthersInLastThirtyDaysDayCount]                                     INT                NULL,
    [ConflictsWithOthersInLastThirtyDaysDayCountDensAsiNonResponseLkpKey]             BIGINT             NULL,
    [TroubledBySocialProblemsDensAsiPatientRatingLkpKey]                              BIGINT             NULL,
    [TroubledBySocialProblemsDensAsiPatientRatingDensAsiNonResponseLkpKey]            BIGINT             NULL,
    [ImportanceOfSocialProblemCounselingDensAsiPatientRatingLkpKey]                   BIGINT             NULL,
    [ImportanceOfSocialProblemCounselingDensAsiPatientRatingDensAsiNonResponseLkpKey] BIGINT             NULL,
    [HomelessInLastThirtyDaysDayCount]                                                INT                NULL,
    [HomelessInLastThirtyDaysDayCountDensAsiNonResponseLkpKey]                        BIGINT             NULL,
    [ShelterInLastThirtyDaysDayCount]                                                 INT                NULL,
    [ShelterInLastThirtyDaysDayCountDensAsiNonResponseLkpKey]                         BIGINT             NULL,
    [NotOwnedHouseInLastThirtyDaysDayCount]                                           INT                NULL,
    [NotOwnedHouseInLastThirtyDaysDayCountDensAsiNonResponseLkpKey]                   BIGINT             NULL,
    [HospitalJailInLastThirtyDaysDayCount]                                            INT                NULL,
    [HospitalJailInLastThirtyDaysDayCountDensAsiNonResponseLkpKey]                    BIGINT             NULL,
    PRIMARY KEY CLUSTERED ([DensAsiInterviewKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);















