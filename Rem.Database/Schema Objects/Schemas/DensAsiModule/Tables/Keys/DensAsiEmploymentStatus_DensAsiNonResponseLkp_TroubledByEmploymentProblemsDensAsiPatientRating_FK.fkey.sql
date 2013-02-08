ALTER TABLE [DensAsiModule].[DensAsiEmploymentStatus]
    ADD CONSTRAINT [DensAsiEmploymentStatus_DensAsiNonResponseLkp_TroubledByEmploymentProblemsDensAsiPatientRating_FK] FOREIGN KEY ([TroubledByEmploymentProblemsDensAsiPatientRatingDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

