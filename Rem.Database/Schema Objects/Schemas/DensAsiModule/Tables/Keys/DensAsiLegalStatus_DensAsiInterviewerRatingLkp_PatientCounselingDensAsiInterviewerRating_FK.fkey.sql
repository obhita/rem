ALTER TABLE [DensAsiModule].[DensAsiLegalStatus]
    ADD CONSTRAINT [DensAsiLegalStatus_DensAsiInterviewerRatingLkp_PatientCounselingDensAsiInterviewerRating_FK] FOREIGN KEY ([PatientCounselingDensAsiInterviewerRatingLkpKey]) REFERENCES [DensAsiModule].[DensAsiInterviewerRatingLkp] ([DensAsiInterviewerRatingLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

