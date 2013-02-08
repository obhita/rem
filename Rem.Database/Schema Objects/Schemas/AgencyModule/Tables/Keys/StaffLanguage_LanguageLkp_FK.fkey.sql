ALTER TABLE [AgencyModule].[StaffLanguage]
    ADD CONSTRAINT [StaffLanguage_LanguageLkp_FK] FOREIGN KEY ([LanguageLkpKey]) REFERENCES [CommonModule].[LanguageLkp] ([LanguageLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

