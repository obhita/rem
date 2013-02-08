ALTER TABLE [DensAsiModule].[DensAsiDrugAlcoholUse]
    ADD CONSTRAINT [DensAsiDrugAlcoholUse_DensAsiDrugAlcoholAdministrationRouteLkp_BarbituratesDensAsiDrugAlcoholAdministrationRoute_FK] FOREIGN KEY ([BarbituratesDensAsiDrugAlcoholAdministrationRouteLkpKey]) REFERENCES [DensAsiModule].[DensAsiDrugAlcoholAdministrationRouteLkp] ([DensAsiDrugAlcoholAdministrationRouteLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

