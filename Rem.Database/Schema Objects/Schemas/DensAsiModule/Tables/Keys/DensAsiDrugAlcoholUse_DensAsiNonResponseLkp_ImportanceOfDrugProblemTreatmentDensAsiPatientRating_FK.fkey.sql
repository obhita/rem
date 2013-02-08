ALTER TABLE [DensAsiModule].[DensAsiDrugAlcoholUse]
    ADD CONSTRAINT [DensAsiDrugAlcoholUse_DensAsiNonResponseLkp_ImportanceOfDrugProblemTreatmentDensAsiPatientRating_FK] FOREIGN KEY ([ImportanceOfDrugProblemTreatmentDensAsiPatientRatingDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

