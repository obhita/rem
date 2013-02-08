ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiSatisfactionLkp_FreeTimeSpentTypeDensAsiSatisfaction_FK] FOREIGN KEY ([FreeTimeSpentTypeDensAsiSatisfactionLkpKey]) REFERENCES [DensAsiModule].[DensAsiSatisfactionLkp] ([DensAsiSatisfactionLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

