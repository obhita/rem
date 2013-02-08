ALTER TABLE [GpraModule].[GpraDrugAlcoholUse]
    ADD CONSTRAINT [GpraDrugAlcoholUse_GpraDrugRouteLkp_OxycontinOxycodoneGpraDrugRoute_FK] FOREIGN KEY ([OxycontinOxycodoneGpraDrugRouteLkpKey]) REFERENCES [GpraModule].[GpraDrugRouteLkp] ([GpraDrugRouteLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

