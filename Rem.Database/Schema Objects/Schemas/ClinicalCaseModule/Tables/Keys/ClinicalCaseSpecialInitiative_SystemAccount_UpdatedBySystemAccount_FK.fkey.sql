ALTER TABLE [ClinicalCaseModule].[ClinicalCaseSpecialInitiative]
    ADD CONSTRAINT [ClinicalCaseSpecialInitiative_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

