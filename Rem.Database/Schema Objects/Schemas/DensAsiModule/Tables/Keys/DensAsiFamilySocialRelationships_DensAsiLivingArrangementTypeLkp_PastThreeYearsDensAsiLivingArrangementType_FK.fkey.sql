ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiLivingArrangementTypeLkp_PastThreeYearsDensAsiLivingArrangementType_FK] FOREIGN KEY ([PastThreeYearsDensAsiLivingArrangementTypeLkpKey]) REFERENCES [DensAsiModule].[DensAsiLivingArrangementTypeLkp] ([DensAsiLivingArrangementTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

