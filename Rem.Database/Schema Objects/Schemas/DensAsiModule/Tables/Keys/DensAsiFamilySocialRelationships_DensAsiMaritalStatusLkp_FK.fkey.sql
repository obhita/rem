ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiMaritalStatusLkp_FK] FOREIGN KEY ([DensAsiMaritalStatusLkpKey]) REFERENCES [DensAsiModule].[DensAsiMaritalStatusLkp] ([DensAsiMaritalStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

