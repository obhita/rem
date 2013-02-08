ALTER TABLE [GpraModule].[GpraDrugAlcoholUse]
    ADD CONSTRAINT [GpraDrugAlcoholUse_GpraDrugRouteLkp_NonPrescriptionGhbGpraDrugRoute_FK] FOREIGN KEY ([NonPrescriptionGhbGpraDrugRouteLkpKey]) REFERENCES [GpraModule].[GpraDrugRouteLkp] ([GpraDrugRouteLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

