ALTER TABLE [EncounterModule].[Service]
    ADD CONSTRAINT [Service_CurrencyLkp_ChargeAmountCurrency_FK] FOREIGN KEY ([ChargeAmountCurrencyLkpKey]) REFERENCES [CommonModule].[CurrencyLkp] ([CurrencyLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

