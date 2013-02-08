ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiFreeTimeSpentTypeLkp_FK] FOREIGN KEY ([DensAsiFreeTimeSpentTypeLkpKey]) REFERENCES [DensAsiModule].[DensAsiFreeTimeSpentTypeLkp] ([DensAsiFreeTimeSpentTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

