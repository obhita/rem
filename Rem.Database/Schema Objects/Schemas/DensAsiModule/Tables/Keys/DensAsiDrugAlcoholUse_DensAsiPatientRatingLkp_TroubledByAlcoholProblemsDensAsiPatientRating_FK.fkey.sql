ALTER TABLE [DensAsiModule].[DensAsiDrugAlcoholUse]
    ADD CONSTRAINT [DensAsiDrugAlcoholUse_DensAsiPatientRatingLkp_TroubledByAlcoholProblemsDensAsiPatientRating_FK] FOREIGN KEY ([TroubledByAlcoholProblemsDensAsiPatientRatingLkpKey]) REFERENCES [DensAsiModule].[DensAsiPatientRatingLkp] ([DensAsiPatientRatingLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

