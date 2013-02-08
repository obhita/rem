ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiSatisfactionLkp_MaritalStatusDensAsiSatisfaction_FK] FOREIGN KEY ([MaritalStatusDensAsiSatisfactionLkpKey]) REFERENCES [DensAsiModule].[DensAsiSatisfactionLkp] ([DensAsiSatisfactionLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

