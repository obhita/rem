ALTER TABLE [SecurityModule].[SystemUsageEvent]
    ADD CONSTRAINT [SystemUsageEvent_SystemAccount_FK] FOREIGN KEY ([SystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

