ALTER TABLE [DensAsiModule].[DensAsiDrugAlcoholUse]
    ADD CONSTRAINT [DensAsiDrugAlcoholUse_DensAsiPatientRatingLkp_ImportanceOfAlcoholProblemTreatmentDensAsiPatientRating_FK] FOREIGN KEY ([ImportanceOfAlcoholProblemTreatmentDensAsiPatientRatingLkpKey]) REFERENCES [DensAsiModule].[DensAsiPatientRatingLkp] ([DensAsiPatientRatingLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

