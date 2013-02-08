ALTER TABLE [SecurityModule].[SystemAccountRole]
    ADD CONSTRAINT [SystemAccountRole_SystemAccount_FK] FOREIGN KEY ([SystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

