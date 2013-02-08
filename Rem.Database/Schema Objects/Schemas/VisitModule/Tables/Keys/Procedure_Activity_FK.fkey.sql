ALTER TABLE [VisitModule].[Procedure]
    ADD CONSTRAINT [Procedure_Activity_FK] FOREIGN KEY ([ActivityKey]) REFERENCES [VisitModule].[Activity] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

