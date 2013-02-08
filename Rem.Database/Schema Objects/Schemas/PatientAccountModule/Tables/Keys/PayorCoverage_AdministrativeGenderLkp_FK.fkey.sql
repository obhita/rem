ALTER TABLE [PatientAccountModule].[PayorCoverage]
    ADD CONSTRAINT [PayorCoverage_AdministrativeGenderLkp_FK] FOREIGN KEY ([AdministrativeGenderLkpKey]) REFERENCES [CommonModule].[AdministrativeGenderLkp] ([AdministrativeGenderLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

