ALTER TABLE [DensAsiModule].[DensAsiPsychiatricStatus]
    ADD CONSTRAINT [DensAsiPsychiatricStatus_DensAsiInterviewerRatingLkp_PatientCounselingDensAsiInterviewerRating_FK] FOREIGN KEY ([PatientCounselingDensAsiInterviewerRatingLkpKey]) REFERENCES [DensAsiModule].[DensAsiInterviewerRatingLkp] ([DensAsiInterviewerRatingLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

