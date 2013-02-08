ALTER TABLE [TedsModule].[TedsDischargeRecord]
    ADD CONSTRAINT [TedsDischargeRecord_SystemTransactionTypeLkp_FK] FOREIGN KEY ([SystemTransactionTypeLkpKey]) REFERENCES [TedsModule].[SystemTransactionTypeLkp] ([SystemTransactionTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

