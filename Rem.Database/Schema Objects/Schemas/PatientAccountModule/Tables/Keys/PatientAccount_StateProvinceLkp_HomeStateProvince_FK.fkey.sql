ALTER TABLE [PatientAccountModule].[PatientAccount]
    ADD CONSTRAINT [PatientAccount_StateProvinceLkp_HomeStateProvince_FK] FOREIGN KEY ([HomeStateProvinceLkpKey]) REFERENCES [CommonModule].[StateProvinceLkp] ([StateProvinceLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

