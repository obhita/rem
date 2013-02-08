ALTER TABLE [ClinicalCaseModule].[DischargeReasonLkp]
    ADD CONSTRAINT [DischargeReasonLkp_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

