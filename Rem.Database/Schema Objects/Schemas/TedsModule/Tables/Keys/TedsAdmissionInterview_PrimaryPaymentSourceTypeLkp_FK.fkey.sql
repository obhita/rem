ALTER TABLE [TedsModule].[TedsAdmissionInterview]
    ADD CONSTRAINT [TedsAdmissionInterview_PrimaryPaymentSourceTypeLkp_FK] FOREIGN KEY ([PrimaryPaymentSourceTypeLkpKey]) REFERENCES [TedsModule].[PrimaryPaymentSourceTypeLkp] ([PrimaryPaymentSourceTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

