ALTER TABLE [DensAsiModule].[DensAsiPatientProfile]
    ADD CONSTRAINT [DensAsiPatientProfile_DensAsiInterviewContactTypeLkp_FK] FOREIGN KEY ([DensAsiInterviewContactTypeLkpKey]) REFERENCES [DensAsiModule].[DensAsiInterviewContactTypeLkp] ([DensAsiInterviewContactTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

