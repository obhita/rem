ALTER TABLE [EncounterModule].[Service]
    ADD CONSTRAINT [Service_CurrencyLkp_RatePerBillingUnitCurrency_FK] FOREIGN KEY ([RatePerBillingUnitCurrencyLkpKey]) REFERENCES [CommonModule].[CurrencyLkp] ([CurrencyLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

