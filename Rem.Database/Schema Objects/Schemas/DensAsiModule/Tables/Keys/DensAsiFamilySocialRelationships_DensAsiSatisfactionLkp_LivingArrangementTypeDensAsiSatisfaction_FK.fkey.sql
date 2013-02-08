ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiSatisfactionLkp_LivingArrangementTypeDensAsiSatisfaction_FK] FOREIGN KEY ([LivingArrangementTypeDensAsiSatisfactionLkpKey]) REFERENCES [DensAsiModule].[DensAsiSatisfactionLkp] ([DensAsiSatisfactionLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

