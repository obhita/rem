ALTER TABLE [DensAsiModule].[DensAsiDrugAlcoholUse]
    ADD CONSTRAINT [DensAsiDrugAlcoholUse_DensAsiDrugAlcoholAdministrationRouteLkp_AnyAlcoholDensAsiDrugAlcoholAdministrationRoute_FK] FOREIGN KEY ([AnyAlcoholDensAsiDrugAlcoholAdministrationRouteLkpKey]) REFERENCES [DensAsiModule].[DensAsiDrugAlcoholAdministrationRouteLkp] ([DensAsiDrugAlcoholAdministrationRouteLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

