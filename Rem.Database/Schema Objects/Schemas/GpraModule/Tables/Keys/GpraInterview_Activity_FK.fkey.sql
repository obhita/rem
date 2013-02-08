ALTER TABLE [GpraModule].[GpraInterview]
    ADD CONSTRAINT [GpraInterview_Activity_FK] FOREIGN KEY ([ActivityKey]) REFERENCES [VisitModule].[Activity] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

