ALTER TABLE [SbirtModule].[Phq9]
    ADD CONSTRAINT [Phq9_Activity_FK] FOREIGN KEY ([ActivityKey]) REFERENCES [VisitModule].[Activity] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

