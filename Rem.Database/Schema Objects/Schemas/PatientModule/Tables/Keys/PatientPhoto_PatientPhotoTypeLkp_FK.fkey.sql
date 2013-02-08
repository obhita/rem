ALTER TABLE [PatientModule].[PatientPhoto]
    ADD CONSTRAINT [PatientPhoto_PatientPhotoTypeLkp_FK] FOREIGN KEY ([PatientPhotoTypeLkpKey]) REFERENCES [PatientModule].[PatientPhotoTypeLkp] ([PatientPhotoTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

