ALTER TABLE [DensAsiModule].[DensAsiMedicalStatus]
    ADD CONSTRAINT [DensAsiMedicalStatus_DensAsiPatientRatingLkp_ImportanceOfMedicalProblemTreatmentDensAsiPatientRating_FK] FOREIGN KEY ([ImportanceOfMedicalProblemTreatmentDensAsiPatientRatingLkpKey]) REFERENCES [DensAsiModule].[DensAsiPatientRatingLkp] ([DensAsiPatientRatingLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

