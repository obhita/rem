ALTER TABLE [SbirtModule].[AuditC]
    ADD CONSTRAINT [AuditC_Activity_FK] FOREIGN KEY ([ActivityKey]) REFERENCES [VisitModule].[Activity] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

