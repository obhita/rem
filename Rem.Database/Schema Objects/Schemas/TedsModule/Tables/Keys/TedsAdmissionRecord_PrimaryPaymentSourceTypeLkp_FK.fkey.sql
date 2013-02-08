ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_PrimaryPaymentSourceTypeLkp_FK] FOREIGN KEY ([PrimaryPaymentSourceTypeLkpKey]) REFERENCES [TedsModule].[PrimaryPaymentSourceTypeLkp] ([PrimaryPaymentSourceTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

