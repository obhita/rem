ALTER TABLE [DensAsiModule].[DensAsiLegalStatus]
    ADD CONSTRAINT [DensAsiLegalStatus_DensAsiViolationTypeLkp_PresentlyAwaitingChargesForDensAsiViolationType_FK] FOREIGN KEY ([PresentlyAwaitingChargesForDensAsiViolationTypeLkpKey]) REFERENCES [DensAsiModule].[DensAsiViolationTypeLkp] ([DensAsiViolationTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

