ALTER TABLE [TedsModule].[TedsBatch]
    ADD CONSTRAINT [TedsBatch_TedsBatchStatusLkp_FK] FOREIGN KEY ([TedsBatchStatusLkpKey]) REFERENCES [TedsModule].[TedsBatchStatusLkp] ([TedsBatchStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

