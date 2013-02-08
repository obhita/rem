ALTER TABLE [ClinicalCaseModule].[InitialContactMethodLkp]
    ADD CONSTRAINT [InitialContactMethodLkp_SystemAccount_CreatedBySystemAccount_FK] FOREIGN KEY ([CreatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

