ALTER TABLE [GpraModule].[GpraDrugAlcoholUse]
    ADD CONSTRAINT [GpraDrugAlcoholUse_GpraNonResponseLkp_AlcoholIntoxicationFivePlusDrinksDayCountGpraNonResponse_FK] FOREIGN KEY ([AlcoholIntoxicationFivePlusDrinksDayCountGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

