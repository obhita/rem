ALTER TABLE [ClinicalCaseModule].[ClinicalCase]
    ADD CONSTRAINT [ClinicalCase_ReferralTypeLkp_FK] FOREIGN KEY ([ReferralTypeLkpKey]) REFERENCES [ClinicalCaseModule].[ReferralTypeLkp] ([ReferralTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

