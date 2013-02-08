ALTER TABLE [DensAsiModule].[DensAsiPatientProfile]
    ADD CONSTRAINT [DensAsiPatientProfile_DensAsiNonResponseLkp_NoParticularReligiousSectIndicator_FK] FOREIGN KEY ([NoParticularReligiousSectIndicatorDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

