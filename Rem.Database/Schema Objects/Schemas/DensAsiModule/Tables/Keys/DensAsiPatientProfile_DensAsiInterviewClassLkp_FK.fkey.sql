ALTER TABLE [DensAsiModule].[DensAsiPatientProfile]
    ADD CONSTRAINT [DensAsiPatientProfile_DensAsiInterviewClassLkp_FK] FOREIGN KEY ([DensAsiInterviewClassLkpKey]) REFERENCES [DensAsiModule].[DensAsiInterviewClassLkp] ([DensAsiInterviewClassLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

