ALTER TABLE [DensAsiModule].[DensAsiLegalStatus]
    ADD CONSTRAINT [DensAsiLegalStatus_DensAsiNonResponseLkp_SeriousnessOfLegalProblemsDensAsiPatientRating_FK] FOREIGN KEY ([SeriousnessOfLegalProblemsDensAsiPatientRatingDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

