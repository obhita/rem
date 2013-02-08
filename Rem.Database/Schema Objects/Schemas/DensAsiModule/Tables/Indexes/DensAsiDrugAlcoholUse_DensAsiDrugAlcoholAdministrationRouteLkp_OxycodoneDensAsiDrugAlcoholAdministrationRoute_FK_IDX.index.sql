CREATE NONCLUSTERED INDEX [DensAsiDrugAlcoholUse_DensAsiDrugAlcoholAdministrationRouteLkp_OxycodoneDensAsiDrugAlcoholAdministrationRoute_FK_IDX]
    ON [DensAsiModule].[DensAsiDrugAlcoholUse]([OxycodoneDensAsiDrugAlcoholAdministrationRouteLkpKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];

