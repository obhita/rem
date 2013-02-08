ALTER TABLE [TedsModule].[TedsAdmissionBatch]
    ADD CONSTRAINT [TedsAdmissionBatch_TedsBatch_FK] FOREIGN KEY ([TedsBatchKey]) REFERENCES [TedsModule].[TedsBatch] ([TedsBatchKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

