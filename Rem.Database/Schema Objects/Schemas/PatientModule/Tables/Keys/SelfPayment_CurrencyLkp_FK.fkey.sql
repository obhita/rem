ALTER TABLE [PatientModule].[SelfPayment]
    ADD CONSTRAINT [SelfPayment_CurrencyLkp_FK] FOREIGN KEY ([CurrencyLkpKey]) REFERENCES [CommonModule].[CurrencyLkp] ([CurrencyLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

