ALTER TABLE [AgencyModule].[Staff]
    ADD CONSTRAINT [Staff_SystemAccount_FK] FOREIGN KEY ([SystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

