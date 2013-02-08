ALTER TABLE [EncounterModule].[Encounter]
    ADD CONSTRAINT [Encounter_CurrencyLkp_ChargeAmountCurrency_FK] FOREIGN KEY ([ChargeAmountCurrencyLkpKey]) REFERENCES [CommonModule].[CurrencyLkp] ([CurrencyLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

