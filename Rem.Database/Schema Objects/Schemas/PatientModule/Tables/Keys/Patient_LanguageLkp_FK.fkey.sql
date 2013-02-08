ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_LanguageLkp_FK] FOREIGN KEY ([LanguageLkpKey]) REFERENCES [CommonModule].[LanguageLkp] ([LanguageLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



