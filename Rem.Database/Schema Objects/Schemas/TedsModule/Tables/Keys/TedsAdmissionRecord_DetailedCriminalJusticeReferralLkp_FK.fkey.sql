ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_DetailedCriminalJusticeReferralLkp_FK] FOREIGN KEY ([DetailedCriminalJusticeReferralLkpKey]) REFERENCES [TedsModule].[DetailedCriminalJusticeReferralLkp] ([DetailedCriminalJusticeReferralLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

