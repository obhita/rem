ALTER TABLE [DensAsiModule].[DensAsiDrugAlcoholUse]
    ADD CONSTRAINT [DensAsiDrugAlcoholUse_DensAsiNonResponseLkp_OtherOpiatesDensAsiDrugAlcoholAdministrationRoute_FK] FOREIGN KEY ([OtherOpiatesDensAsiDrugAlcoholAdministrationRouteDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

