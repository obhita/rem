ALTER TABLE [DensAsiModule].[DensAsiDrugAlcoholUse]
    ADD CONSTRAINT [DensAsiDrugAlcoholUse_DensAsiPatientRatingLkp_ImportanceOfDrugProblemTreatmentDensAsiPatientRating_FK] FOREIGN KEY ([ImportanceOfDrugProblemTreatmentDensAsiPatientRatingLkpKey]) REFERENCES [DensAsiModule].[DensAsiPatientRatingLkp] ([DensAsiPatientRatingLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

