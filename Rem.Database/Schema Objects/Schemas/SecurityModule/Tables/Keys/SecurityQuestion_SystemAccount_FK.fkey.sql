ALTER TABLE [SecurityModule].[SecurityQuestion]
    ADD CONSTRAINT [SecurityQuestion_SystemAccount_FK] FOREIGN KEY ([SystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

