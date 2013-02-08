ALTER TABLE [AgencyModule].[StaffLanguage]
    ADD CONSTRAINT [StaffLanguage_LanguageFluencyLkp_FK] FOREIGN KEY ([LanguageFluencyLkpKey]) REFERENCES [AgencyModule].[LanguageFluencyLkp] ([LanguageFluencyLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

