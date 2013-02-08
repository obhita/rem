ALTER TABLE [ClinicalCaseModule].[ReferralTypeLkp]
    ADD CONSTRAINT [ReferralTypeLkp_SystemAccount_CreatedBySystemAccount_FK] FOREIGN KEY ([CreatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

