ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_PatientRace_PrimaryPatientRace_FK] FOREIGN KEY ([PrimaryPatientRaceKey]) REFERENCES [PatientModule].[PatientRace] ([PatientRaceKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

