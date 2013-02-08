ALTER TABLE [DensAsiModule].[DensAsiPatientProfile]
    ADD CONSTRAINT [DensAsiPatientProfile_DensAsiNonResponseLkp_YearsAndMonthsAtCurrentAddressTimeSpan_FK] FOREIGN KEY ([YearsAndMonthsAtCurrentAddressTimeSpanDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

