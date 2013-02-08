ALTER TABLE [VisitModule].[HeartRate]
    ADD CONSTRAINT [HeartRate_VitalSign_FK] FOREIGN KEY ([VitalSignKey]) REFERENCES [VisitModule].[VitalSign] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



