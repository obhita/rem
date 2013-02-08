ALTER TABLE [DensAsiModule].[DensAsiEmploymentStatus]
    ADD CONSTRAINT [DensAsiEmploymentStatus_DensAsiNonResponseLkp_NetIncomeAmount_FK] FOREIGN KEY ([NetIncomeAmountDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

