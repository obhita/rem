ALTER TABLE [DensAsiModule].[DensAsiDrugAlcoholUse]
    ADD CONSTRAINT [DensAsiDrugAlcoholUse_DensAsiDrugAlcoholAdministrationRouteLkp_AmphetaminesDensAsiDrugAlcoholAdministrationRoute_FK] FOREIGN KEY ([AmphetaminesDensAsiDrugAlcoholAdministrationRouteLkpKey]) REFERENCES [DensAsiModule].[DensAsiDrugAlcoholAdministrationRouteLkp] ([DensAsiDrugAlcoholAdministrationRouteLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

