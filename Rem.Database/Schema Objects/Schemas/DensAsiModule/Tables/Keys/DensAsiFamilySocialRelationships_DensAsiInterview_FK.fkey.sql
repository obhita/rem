ALTER TABLE [DensAsiModule].[DensAsiFamilySocialRelationships]
    ADD CONSTRAINT [DensAsiFamilySocialRelationships_DensAsiInterview_FK] FOREIGN KEY ([DensAsiInterviewKey]) REFERENCES [DensAsiModule].[DensAsiInterview] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

