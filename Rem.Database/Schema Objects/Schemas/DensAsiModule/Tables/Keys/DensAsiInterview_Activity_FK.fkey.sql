ALTER TABLE [DensAsiModule].[DensAsiInterview]
    ADD CONSTRAINT [DensAsiInterview_Activity_FK] FOREIGN KEY ([ActivityKey]) REFERENCES [VisitModule].[Activity] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

