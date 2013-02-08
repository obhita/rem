ALTER TABLE [PatientModule].[PatientContact]
    ADD CONSTRAINT [PatientContact_PatientContactRelationshipTypeLkp_FK] FOREIGN KEY ([PatientContactRelationshipTypeLkpKey]) REFERENCES [PatientModule].[PatientContactRelationshipTypeLkp] ([PatientContactRelationshipTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

