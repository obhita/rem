ALTER TABLE [GpraModule].[GpraDrugAlcoholUse]
    ADD CONSTRAINT [GpraDrugAlcoholUse_GpraDrugRouteLkp_MorphineGpraDrugRoute_FK] FOREIGN KEY ([MorphineGpraDrugRouteLkpKey]) REFERENCES [GpraModule].[GpraDrugRouteLkp] ([GpraDrugRouteLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

