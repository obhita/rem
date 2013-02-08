ALTER TABLE [AgencyModule].[LocationFrequentlyAskedQuestion]
    ADD CONSTRAINT [LocationFrequentlyAskedQuestion_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

