ALTER TABLE [DensAsiModule].[DensAsiEmploymentStatus]
    ADD CONSTRAINT [DensAsiEmploymentStatus_DensAsiNonResponseLkp_IllegalAmount_FK] FOREIGN KEY ([IllegalAmountDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

