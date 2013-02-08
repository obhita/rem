ALTER TABLE [PatientModule].[PatientGenderLkp]
    ADD CONSTRAINT [PatientGenderLkp_AdministrativeGenderLkp_FK] FOREIGN KEY ([AdministrativeGenderLkpKey]) REFERENCES [CommonModule].[AdministrativeGenderLkp] ([AdministrativeGenderLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

