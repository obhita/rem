ALTER TABLE [ClinicalCaseModule].[Problem]
    ADD CONSTRAINT [Problem_ProblemTypeLkp_FK] FOREIGN KEY ([ProblemTypeLkpKey]) REFERENCES [ClinicalCaseModule].[ProblemTypeLkp] ([ProblemTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

