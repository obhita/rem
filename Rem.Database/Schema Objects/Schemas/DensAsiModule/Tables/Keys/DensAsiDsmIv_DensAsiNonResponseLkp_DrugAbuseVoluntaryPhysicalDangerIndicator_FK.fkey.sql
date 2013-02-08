ALTER TABLE [DensAsiModule].[DensAsiDsmIv]
    ADD CONSTRAINT [DensAsiDsmIv_DensAsiNonResponseLkp_DrugAbuseVoluntaryPhysicalDangerIndicator_FK] FOREIGN KEY ([DrugAbuseVoluntaryPhysicalDangerIndicatorDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

