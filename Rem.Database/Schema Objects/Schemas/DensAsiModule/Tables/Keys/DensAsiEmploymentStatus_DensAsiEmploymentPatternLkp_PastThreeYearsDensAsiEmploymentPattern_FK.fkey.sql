ALTER TABLE [DensAsiModule].[DensAsiEmploymentStatus]
    ADD CONSTRAINT [DensAsiEmploymentStatus_DensAsiEmploymentPatternLkp_PastThreeYearsDensAsiEmploymentPattern_FK] FOREIGN KEY ([PastThreeYearsDensAsiEmploymentPatternLkpKey]) REFERENCES [DensAsiModule].[DensAsiEmploymentPatternLkp] ([DensAsiEmploymentPatternLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

