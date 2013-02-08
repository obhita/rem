ALTER TABLE [AgencyModule].[AgencyPhoneTypeLkp]
    ADD CONSTRAINT [AgencyPhoneTypeLkp_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

