ALTER TABLE [VisitModule].[Activity]
    ADD CONSTRAINT [Activity_ActivityTypeLkp_FK] FOREIGN KEY ([ActivityTypeLkpKey]) REFERENCES [VisitModule].[ActivityTypeLkp] ([ActivityTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

