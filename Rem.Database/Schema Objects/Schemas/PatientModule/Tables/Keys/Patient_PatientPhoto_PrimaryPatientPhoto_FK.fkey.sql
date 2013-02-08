ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_PatientPhoto_PrimaryPatientPhoto_FK] FOREIGN KEY ([PrimaryPatientPhotoKey]) REFERENCES [PatientModule].[PatientPhoto] ([PatientPhotoKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

