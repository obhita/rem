ALTER TABLE [DensAsiModule].[DensAsiDrugAlcoholUse]
    ADD CONSTRAINT [DensAsiDrugAlcoholUse_DensAsiNonResponseLkp_OtherSedativesDensAsiDrugAlcoholAdministrationRoute_FK] FOREIGN KEY ([OtherSedativesDensAsiDrugAlcoholAdministrationRouteDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

