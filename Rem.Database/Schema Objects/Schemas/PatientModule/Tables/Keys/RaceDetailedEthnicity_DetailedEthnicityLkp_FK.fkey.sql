ALTER TABLE [PatientModule].[RaceDetailedEthnicity]
    ADD CONSTRAINT [RaceDetailedEthnicity_DetailedEthnicityLkp_FK] FOREIGN KEY ([DetailedEthnicityLkpKey]) REFERENCES [PatientModule].[DetailedEthnicityLkp] ([DetailedEthnicityLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

