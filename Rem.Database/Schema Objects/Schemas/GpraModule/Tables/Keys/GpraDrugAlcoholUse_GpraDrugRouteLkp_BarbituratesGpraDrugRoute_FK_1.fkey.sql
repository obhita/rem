ALTER TABLE [GpraModule].[GpraDrugAlcoholUse]
    ADD CONSTRAINT [GpraDrugAlcoholUse_GpraDrugRouteLkp_BarbituratesGpraDrugRoute_FK] FOREIGN KEY ([BarbituratesGpraDrugRouteLkpKey]) REFERENCES [GpraModule].[GpraDrugRouteLkp] ([GpraDrugRouteLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

