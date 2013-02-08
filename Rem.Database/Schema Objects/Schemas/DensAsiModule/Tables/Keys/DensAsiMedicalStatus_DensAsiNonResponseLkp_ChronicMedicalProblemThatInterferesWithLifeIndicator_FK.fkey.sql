ALTER TABLE [DensAsiModule].[DensAsiMedicalStatus]
    ADD CONSTRAINT [DensAsiMedicalStatus_DensAsiNonResponseLkp_ChronicMedicalProblemThatInterferesWithLifeIndicator_FK] FOREIGN KEY ([ChronicMedicalProblemThatInterferesWithLifeIndicatorDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

