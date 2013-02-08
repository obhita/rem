ALTER TABLE [SecurityModule].[SystemUsageEvent]
    ADD CONSTRAINT [SystemUsageEvent_EventTypeLkp_FK] FOREIGN KEY ([EventTypeLkpKey]) REFERENCES [SecurityModule].[EventTypeLkp] ([EventTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

