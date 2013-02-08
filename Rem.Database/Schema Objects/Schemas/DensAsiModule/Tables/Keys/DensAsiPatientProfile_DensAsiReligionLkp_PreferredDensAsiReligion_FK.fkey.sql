ALTER TABLE [DensAsiModule].[DensAsiPatientProfile]
    ADD CONSTRAINT [DensAsiPatientProfile_DensAsiReligionLkp_PreferredDensAsiReligion_FK] FOREIGN KEY ([PreferredDensAsiReligionLkpKey]) REFERENCES [DensAsiModule].[DensAsiReligionLkp] ([DensAsiReligionLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

