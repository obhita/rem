ALTER TABLE [TedsModule].[DetailedDrugCodeLkp]
    ADD CONSTRAINT [DetailedDrugCodeLkp_SubstanceProblemTypeLkp_FK] FOREIGN KEY ([SubstanceProblemTypeLkpKey]) REFERENCES [TedsModule].[SubstanceProblemTypeLkp] ([SubstanceProblemTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

