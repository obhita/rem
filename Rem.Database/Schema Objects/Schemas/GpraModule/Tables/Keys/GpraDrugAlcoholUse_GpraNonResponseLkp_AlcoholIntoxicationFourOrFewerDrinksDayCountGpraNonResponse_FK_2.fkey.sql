ALTER TABLE [GpraModule].[GpraDrugAlcoholUse]
    ADD CONSTRAINT [GpraDrugAlcoholUse_GpraNonResponseLkp_AlcoholIntoxicationFourOrFewerDrinksDayCountGpraNonResponse_FK] FOREIGN KEY ([AlcoholIntoxicationFourOrFewerDrinksDayCountGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

