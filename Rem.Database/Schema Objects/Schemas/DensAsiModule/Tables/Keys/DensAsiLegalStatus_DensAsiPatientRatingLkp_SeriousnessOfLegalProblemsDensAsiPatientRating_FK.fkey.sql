ALTER TABLE [DensAsiModule].[DensAsiLegalStatus]
    ADD CONSTRAINT [DensAsiLegalStatus_DensAsiPatientRatingLkp_SeriousnessOfLegalProblemsDensAsiPatientRating_FK] FOREIGN KEY ([SeriousnessOfLegalProblemsDensAsiPatientRatingLkpKey]) REFERENCES [DensAsiModule].[DensAsiPatientRatingLkp] ([DensAsiPatientRatingLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

