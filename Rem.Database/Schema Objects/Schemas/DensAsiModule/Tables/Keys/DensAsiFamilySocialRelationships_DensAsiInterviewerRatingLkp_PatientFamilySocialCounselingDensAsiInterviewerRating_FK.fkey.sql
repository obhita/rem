ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiInterviewerRatingLkp_PatientFamilySocialCounselingDensAsiInterviewerRating_FK] FOREIGN KEY ([PatientFamilySocialCounselingDensAsiInterviewerRatingLkpKey]) REFERENCES [DensAsiModule].[DensAsiInterviewerRatingLkp] ([DensAsiInterviewerRatingLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

