ALTER TABLE [DensAsiModule].[DensAsiDrugAlcoholUse]
    ADD CONSTRAINT [DensAsiDrugAlcoholUse_DensAsiDrugAlcoholAdministrationRouteLkp_CannabisDensAsiDrugAlcoholAdministrationRoute_FK] FOREIGN KEY ([CannabisDensAsiDrugAlcoholAdministrationRouteLkpKey]) REFERENCES [DensAsiModule].[DensAsiDrugAlcoholAdministrationRouteLkp] ([DensAsiDrugAlcoholAdministrationRouteLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

