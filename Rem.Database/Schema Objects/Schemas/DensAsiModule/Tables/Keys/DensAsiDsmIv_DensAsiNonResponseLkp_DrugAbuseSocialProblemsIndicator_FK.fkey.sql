ALTER TABLE [DensAsiModule].[DensAsiDsmIv]
    ADD CONSTRAINT [DensAsiDsmIv_DensAsiNonResponseLkp_DrugAbuseSocialProblemsIndicator_FK] FOREIGN KEY ([DrugAbuseSocialProblemsIndicatorDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

