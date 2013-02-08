ALTER TABLE [SbirtModule].[Dast10]
    ADD CONSTRAINT [Dast10_Activity_FK] FOREIGN KEY ([ActivityKey]) REFERENCES [VisitModule].[Activity] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

