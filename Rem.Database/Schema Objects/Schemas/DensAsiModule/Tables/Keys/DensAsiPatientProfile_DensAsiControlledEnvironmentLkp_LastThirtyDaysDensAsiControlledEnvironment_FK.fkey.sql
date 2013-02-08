ALTER TABLE [DensAsiModule].[DensAsiPatientProfile]
    ADD CONSTRAINT [DensAsiPatientProfile_DensAsiControlledEnvironmentLkp_LastThirtyDaysDensAsiControlledEnvironment_FK] FOREIGN KEY ([LastThirtyDaysDensAsiControlledEnvironmentLkpKey]) REFERENCES [DensAsiModule].[DensAsiControlledEnvironmentLkp] ([DensAsiControlledEnvironmentLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

