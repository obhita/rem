ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_IncomeSourceTypeLkp_FK] FOREIGN KEY ([IncomeSourceTypeLkpKey]) REFERENCES [TedsModule].[IncomeSourceTypeLkp] ([IncomeSourceTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

