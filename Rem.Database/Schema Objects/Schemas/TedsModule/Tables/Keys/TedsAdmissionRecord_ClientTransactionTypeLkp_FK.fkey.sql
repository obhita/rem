ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_ClientTransactionTypeLkp_FK] FOREIGN KEY ([ClientTransactionTypeLkpKey]) REFERENCES [TedsModule].[ClientTransactionTypeLkp] ([ClientTransactionTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

