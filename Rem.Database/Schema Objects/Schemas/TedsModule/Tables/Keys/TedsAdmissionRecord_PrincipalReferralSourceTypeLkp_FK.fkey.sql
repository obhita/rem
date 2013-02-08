ALTER TABLE [TedsModule].[TedsAdmissionRecord]
    ADD CONSTRAINT [TedsAdmissionRecord_PrincipalReferralSourceTypeLkp_FK] FOREIGN KEY ([PrincipalReferralSourceTypeLkpKey]) REFERENCES [TedsModule].[PrincipalReferralSourceTypeLkp] ([PrincipalReferralSourceTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

