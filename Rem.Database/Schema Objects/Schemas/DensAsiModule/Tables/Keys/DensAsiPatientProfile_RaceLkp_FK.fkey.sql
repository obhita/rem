ALTER TABLE [DensAsiModule].[DensAsiPatientProfile]
    ADD CONSTRAINT [DensAsiPatientProfile_RaceLkp_FK] FOREIGN KEY ([RaceLkpKey]) REFERENCES [PatientModule].[RaceLkp] ([RaceLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

