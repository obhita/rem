ALTER TABLE [DensAsiModule].[DensAsiEmploymentStatus]
    ADD CONSTRAINT [DensAsiEmploymentStatus_DensAsiOccupationTypeLkp_UsualOrLastDensAsiOccupationType_FK] FOREIGN KEY ([UsualOrLastDensAsiOccupationTypeLkpKey]) REFERENCES [DensAsiModule].[DensAsiOccupationTypeLkp] ([DensAsiOccupationTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

