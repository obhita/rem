ALTER TABLE [DensAsiModule].[DensAsiPsychiatricStatus]
    ADD CONSTRAINT [DensAsiPsychiatricStatus_DensAsiNonResponseLkp_PsychologicalTreatmentInHospitalCount_FK] FOREIGN KEY ([PsychologicalTreatmentInHospitalCountDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

