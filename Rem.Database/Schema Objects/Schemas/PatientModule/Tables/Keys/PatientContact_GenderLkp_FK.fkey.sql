ALTER TABLE [PatientModule].[PatientContact]
    ADD CONSTRAINT [PatientContact_GenderLkp_FK] FOREIGN KEY ([GenderLkpKey]) REFERENCES [CommonModule].[GenderLkp] ([GenderLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

