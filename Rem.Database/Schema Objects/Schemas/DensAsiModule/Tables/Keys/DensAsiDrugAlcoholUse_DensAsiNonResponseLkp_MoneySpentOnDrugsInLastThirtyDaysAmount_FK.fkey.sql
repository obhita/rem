ALTER TABLE [DensAsiModule].[DensAsiDrugAlcoholUse]
    ADD CONSTRAINT [DensAsiDrugAlcoholUse_DensAsiNonResponseLkp_MoneySpentOnDrugsInLastThirtyDaysAmount_FK] FOREIGN KEY ([MoneySpentOnDrugsInLastThirtyDaysAmountDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

