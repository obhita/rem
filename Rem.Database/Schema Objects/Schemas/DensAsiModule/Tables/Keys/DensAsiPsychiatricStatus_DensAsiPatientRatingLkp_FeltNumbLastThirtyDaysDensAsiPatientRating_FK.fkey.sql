ALTER TABLE [DensAsiModule].[DensAsiPsychiatricStatus]
    ADD CONSTRAINT [DensAsiPsychiatricStatus_DensAsiPatientRatingLkp_FeltNumbLastThirtyDaysDensAsiPatientRating_FK] FOREIGN KEY ([FeltNumbLastThirtyDaysDensAsiPatientRatingLkpKey]) REFERENCES [DensAsiModule].[DensAsiPatientRatingLkp] ([DensAsiPatientRatingLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

