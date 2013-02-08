ALTER TABLE [PatientModule].[RaceDetailedEthnicity]
    ADD CONSTRAINT [RaceDetailedEthnicity_RaceLkp_FK] FOREIGN KEY ([RaceLkpKey]) REFERENCES [PatientModule].[RaceLkp] ([RaceLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

