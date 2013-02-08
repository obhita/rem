ALTER TABLE [DensAsiModule].[DensAsiLegalStatus]
    ADD CONSTRAINT [DensAsiLegalStatus_DensAsiViolationTypeLkp_IncarcerationForDensAsiViolationType_FK] FOREIGN KEY ([IncarcerationForDensAsiViolationTypeLkpKey]) REFERENCES [DensAsiModule].[DensAsiViolationTypeLkp] ([DensAsiViolationTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

