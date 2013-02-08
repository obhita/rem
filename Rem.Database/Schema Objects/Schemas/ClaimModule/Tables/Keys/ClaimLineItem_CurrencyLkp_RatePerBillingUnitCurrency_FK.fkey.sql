ALTER TABLE [ClaimModule].[ClaimLineItem]
    ADD CONSTRAINT [ClaimLineItem_CurrencyLkp_RatePerBillingUnitCurrency_FK] FOREIGN KEY ([RatePerBillingUnitCurrencyLkpKey]) REFERENCES [CommonModule].[CurrencyLkp] ([CurrencyLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

