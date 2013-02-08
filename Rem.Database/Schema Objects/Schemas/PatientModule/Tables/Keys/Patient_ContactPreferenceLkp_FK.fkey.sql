ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_ContactPreferenceLkp_FK] FOREIGN KEY ([ContactPreferenceLkpKey]) REFERENCES [PatientModule].[ContactPreferenceLkp] ([ContactPreferenceLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

