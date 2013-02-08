ALTER TABLE [TedsModule].[DetailedCriminalJusticeReferralLkp]
    ADD CONSTRAINT [DetailedCriminalJusticeReferralLkp_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

