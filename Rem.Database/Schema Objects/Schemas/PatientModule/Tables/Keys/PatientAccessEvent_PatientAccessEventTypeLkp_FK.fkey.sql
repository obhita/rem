ALTER TABLE [PatientModule].[PatientAccessEvent]
    ADD CONSTRAINT [PatientAccessEvent_PatientAccessEventTypeLkp_FK] FOREIGN KEY ([PatientAccessEventTypeLkpKey]) REFERENCES [PatientModule].[PatientAccessEventTypeLkp] ([PatientAccessEventTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

