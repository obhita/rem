ALTER TABLE [GpraModule].[GpraDrugAlcoholUse]
    ADD CONSTRAINT [GpraDrugAlcoholUse_GpraNonResponseLkp_MarijuanaHashishDayCountGpraNonResponse_FK] FOREIGN KEY ([MarijuanaHashishDayCountGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

