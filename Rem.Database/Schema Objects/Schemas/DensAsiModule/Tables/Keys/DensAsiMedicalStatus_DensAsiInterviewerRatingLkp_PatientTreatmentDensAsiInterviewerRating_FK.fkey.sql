ALTER TABLE [DensAsiModule].[DensAsiMedicalStatus]
    ADD CONSTRAINT [DensAsiMedicalStatus_DensAsiInterviewerRatingLkp_PatientTreatmentDensAsiInterviewerRating_FK] FOREIGN KEY ([PatientTreatmentDensAsiInterviewerRatingLkpKey]) REFERENCES [DensAsiModule].[DensAsiInterviewerRatingLkp] ([DensAsiInterviewerRatingLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

