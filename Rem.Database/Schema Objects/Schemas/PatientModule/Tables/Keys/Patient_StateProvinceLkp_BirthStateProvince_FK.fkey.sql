ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_StateProvinceLkp_BirthStateProvince_FK] FOREIGN KEY ([BirthStateProvinceLkpKey]) REFERENCES [CommonModule].[StateProvinceLkp] ([StateProvinceLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

