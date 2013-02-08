ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_TedsNonResponseLkp_DetailedCriminalJusticeReferral_FK] FOREIGN KEY ([DetailedCriminalJusticeReferralTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

