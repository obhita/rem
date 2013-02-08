ALTER TABLE [DensAsiModule].[DensAsiPsychiatricStatus]
    ADD CONSTRAINT [DensAsiPsychiatricStatus_DensAsiPatientRatingLkp_ImportanceOfPsychologicalProblemCounselingDensAsiPatientRating_FK] FOREIGN KEY ([ImportanceOfPsychologicalProblemCounselingDensAsiPatientRatingLkpKey]) REFERENCES [DensAsiModule].[DensAsiPatientRatingLkp] ([DensAsiPatientRatingLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

