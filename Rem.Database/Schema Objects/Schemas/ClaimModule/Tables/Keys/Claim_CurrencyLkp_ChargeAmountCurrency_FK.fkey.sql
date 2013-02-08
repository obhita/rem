ALTER TABLE [ClaimModule].[Claim]
    ADD CONSTRAINT [Claim_CurrencyLkp_ChargeAmountCurrency_FK] FOREIGN KEY ([ChargeAmountCurrencyLkpKey]) REFERENCES [CommonModule].[CurrencyLkp] ([CurrencyLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

