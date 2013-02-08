ALTER TABLE [DensAsiModule].[DensAsiDrugAlcoholUse]
    ADD CONSTRAINT [DensAsiDrugAlcoholUse_DensAsiDrugAlcoholAdministrationRouteLkp_HallucinogensDensAsiDrugAlcoholAdministrationRoute_FK] FOREIGN KEY ([HallucinogensDensAsiDrugAlcoholAdministrationRouteLkpKey]) REFERENCES [DensAsiModule].[DensAsiDrugAlcoholAdministrationRouteLkp] ([DensAsiDrugAlcoholAdministrationRouteLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

