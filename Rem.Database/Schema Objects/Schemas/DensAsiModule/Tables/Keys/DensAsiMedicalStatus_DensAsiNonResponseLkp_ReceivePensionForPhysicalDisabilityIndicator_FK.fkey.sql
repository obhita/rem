ALTER TABLE [DensAsiModule].[DensAsiMedicalStatus]
    ADD CONSTRAINT [DensAsiMedicalStatus_DensAsiNonResponseLkp_ReceivePensionForPhysicalDisabilityIndicator_FK] FOREIGN KEY ([ReceivePensionForPhysicalDisabilityIndicatorDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

