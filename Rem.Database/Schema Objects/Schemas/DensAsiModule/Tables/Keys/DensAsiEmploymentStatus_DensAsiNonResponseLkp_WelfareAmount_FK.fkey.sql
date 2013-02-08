ALTER TABLE [DensAsiModule].[DensAsiEmploymentStatus]
    ADD CONSTRAINT [DensAsiEmploymentStatus_DensAsiNonResponseLkp_WelfareAmount_FK] FOREIGN KEY ([WelfareAmountDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

