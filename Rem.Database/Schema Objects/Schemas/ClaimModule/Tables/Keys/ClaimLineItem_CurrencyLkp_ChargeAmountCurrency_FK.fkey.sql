ALTER TABLE [ClaimModule].[ClaimLineItem]
    ADD CONSTRAINT [ClaimLineItem_CurrencyLkp_ChargeAmountCurrency_FK] FOREIGN KEY ([ChargeAmountCurrencyLkpKey]) REFERENCES [CommonModule].[CurrencyLkp] ([CurrencyLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

