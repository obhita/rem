ALTER TABLE [DensAsiModule].[DensAsiDrugAlcoholUse]
    ADD CONSTRAINT [DensAsiDrugAlcoholUse_DensAsiInterviewerRatingLkp_PatientDrugTreatmentDensAsiInterviewerRating_FK] FOREIGN KEY ([PatientDrugTreatmentDensAsiInterviewerRatingLkpKey]) REFERENCES [DensAsiModule].[DensAsiInterviewerRatingLkp] ([DensAsiInterviewerRatingLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

