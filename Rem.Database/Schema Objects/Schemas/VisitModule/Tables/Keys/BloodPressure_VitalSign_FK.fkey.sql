ALTER TABLE [VisitModule].[BloodPressure]
    ADD CONSTRAINT [BloodPressure_VitalSign_FK] FOREIGN KEY ([VitalSignKey]) REFERENCES [VisitModule].[VitalSign] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



