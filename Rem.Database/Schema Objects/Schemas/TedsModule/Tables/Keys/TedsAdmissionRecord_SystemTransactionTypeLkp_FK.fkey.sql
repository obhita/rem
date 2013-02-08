ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_SystemTransactionTypeLkp_FK] FOREIGN KEY ([SystemTransactionTypeLkpKey]) REFERENCES [TedsModule].[SystemTransactionTypeLkp] ([SystemTransactionTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

