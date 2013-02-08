ALTER TABLE [DensAsiModule].[DensAsiPatientProfile]
    ADD CONSTRAINT [DensAsiPatientProfile_AdministrativeGenderLkp_PatientAdministrativeGender_FK] FOREIGN KEY ([PatientAdministrativeGenderLkpKey]) REFERENCES [CommonModule].[AdministrativeGenderLkp] ([AdministrativeGenderLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

