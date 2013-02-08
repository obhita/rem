ALTER TABLE [DensAsiModule].[DensAsiEmploymentStatus]
    ADD CONSTRAINT [DensAsiEmploymentStatus_DensAsiPatientRatingLkp_ImportanceOfEmploymentProblemCounselingDensAsiPatientRating_FK] FOREIGN KEY ([ImportanceOfEmploymentProblemCounselingDensAsiPatientRatingLkpKey]) REFERENCES [DensAsiModule].[DensAsiPatientRatingLkp] ([DensAsiPatientRatingLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

