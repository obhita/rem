ALTER TABLE [DensAsiModule].[DensAsiPsychiatricStatus]
    ADD CONSTRAINT [DensAsiPsychiatricStatus_DensAsiPatientRatingLkp_TraumaticEventThoughtsLastThirtyDaysDensAsiPatientRating_FK] FOREIGN KEY ([TraumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingLkpKey]) REFERENCES [DensAsiModule].[DensAsiPatientRatingLkp] ([DensAsiPatientRatingLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

