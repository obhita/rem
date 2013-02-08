ALTER TABLE [DensAsiModule].[DensAsiMedicalStatus]
    ADD CONSTRAINT [DensAsiMedicalStatus_DensAsiNonResponseLkp_HopitalizedForMedicalProblemsCount_FK] FOREIGN KEY ([HopitalizedForMedicalProblemsCountDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

