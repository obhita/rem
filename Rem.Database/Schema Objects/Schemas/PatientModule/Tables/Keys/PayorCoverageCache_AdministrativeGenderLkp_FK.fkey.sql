ALTER TABLE [PatientModule].[PayorCoverageCache]
    ADD CONSTRAINT [PayorCoverageCache_AdministrativeGenderLkp_FK] FOREIGN KEY ([AdministrativeGenderLkpKey]) REFERENCES [CommonModule].[AdministrativeGenderLkp] ([AdministrativeGenderLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

