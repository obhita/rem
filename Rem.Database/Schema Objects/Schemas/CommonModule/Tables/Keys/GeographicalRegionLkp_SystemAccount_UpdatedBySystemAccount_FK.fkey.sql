ALTER TABLE [CommonModule].[GeographicalRegionLkp]
    ADD CONSTRAINT [GeographicalRegionLkp_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

