ALTER TABLE [DensAsiModule].[DensAsiLegalStatus]
    ADD CONSTRAINT [DensAsiLegalStatus_DensAsiPatientRatingLkp_ImportanceOfLegalProblemCounselingDensAsiPatientRating_FK] FOREIGN KEY ([ImportanceOfLegalProblemCounselingDensAsiPatientRatingLkpKey]) REFERENCES [DensAsiModule].[DensAsiPatientRatingLkp] ([DensAsiPatientRatingLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

