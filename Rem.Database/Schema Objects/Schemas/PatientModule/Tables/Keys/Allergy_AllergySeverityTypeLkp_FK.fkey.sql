ALTER TABLE [PatientModule].[Allergy]
    ADD CONSTRAINT [Allergy_AllergySeverityTypeLkp_FK] FOREIGN KEY ([AllergySeverityTypeLkpKey]) REFERENCES [PatientModule].[AllergySeverityTypeLkp] ([AllergySeverityTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

