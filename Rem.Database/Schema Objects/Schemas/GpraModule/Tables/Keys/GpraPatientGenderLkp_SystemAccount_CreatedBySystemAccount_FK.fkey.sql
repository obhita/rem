ALTER TABLE [GpraModule].[GpraPatientGenderLkp]
    ADD CONSTRAINT [GpraPatientGenderLkp_SystemAccount_CreatedBySystemAccount_FK] FOREIGN KEY ([CreatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

