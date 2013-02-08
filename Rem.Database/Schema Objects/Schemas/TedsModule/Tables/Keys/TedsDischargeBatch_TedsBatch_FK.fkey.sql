ALTER TABLE [TedsModule].[TedsDischargeBatch]
    ADD CONSTRAINT [TedsDischargeBatch_TedsBatch_FK] FOREIGN KEY ([TedsBatchKey]) REFERENCES [TedsModule].[TedsBatch] ([TedsBatchKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

