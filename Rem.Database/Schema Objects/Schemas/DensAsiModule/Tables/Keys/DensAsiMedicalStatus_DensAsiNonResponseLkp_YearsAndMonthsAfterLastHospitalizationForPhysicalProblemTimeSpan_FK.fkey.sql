ALTER TABLE [DensAsiModule].[DensAsiMedicalStatus]
    ADD CONSTRAINT [DensAsiMedicalStatus_DensAsiNonResponseLkp_YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan_FK] FOREIGN KEY ([YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

