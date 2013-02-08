ALTER TABLE [DensAsiModule].[DensAsiEmploymentStatus]
    ADD CONSTRAINT [DensAsiEmploymentStatus_DensAsiNonResponseLkp_ImportanceOfEmploymentProblemCounselingDensAsiPatientRating_FK] FOREIGN KEY ([ImportanceOfEmploymentProblemCounselingDensAsiPatientRatingDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

