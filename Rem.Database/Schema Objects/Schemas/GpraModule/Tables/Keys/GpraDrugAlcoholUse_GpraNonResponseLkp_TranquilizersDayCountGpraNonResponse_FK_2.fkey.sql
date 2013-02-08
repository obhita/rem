ALTER TABLE [GpraModule].[GpraDrugAlcoholUse]
    ADD CONSTRAINT [GpraDrugAlcoholUse_GpraNonResponseLkp_TranquilizersDayCountGpraNonResponse_FK] FOREIGN KEY ([TranquilizersDayCountGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

