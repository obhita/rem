ALTER TABLE [GpraModule].[GpraDrugAlcoholUse]
    ADD CONSTRAINT [GpraDrugAlcoholUse_GpraDrugRouteLkp_NonPrescriptionMethodoneGpraDrugRoute_FK] FOREIGN KEY ([NonPrescriptionMethodoneGpraDrugRouteLkpKey]) REFERENCES [GpraModule].[GpraDrugRouteLkp] ([GpraDrugRouteLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

