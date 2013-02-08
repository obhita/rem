ALTER TABLE [DensAsiModule].[DensAsiDrugAlcoholUse]
    ADD CONSTRAINT [DensAsiDrugAlcoholUse_DensAsiProblematicSubstanceLkp_MajorDensAsiProblematicSubstance_FK] FOREIGN KEY ([MajorDensAsiProblematicSubstanceLkpKey]) REFERENCES [DensAsiModule].[DensAsiProblematicSubstanceLkp] ([DensAsiProblematicSubstanceLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

