ALTER TABLE [ClinicalCaseModule].[Problem]
    ADD CONSTRAINT [Problem_ProblemStatusLkp_FK] FOREIGN KEY ([ProblemStatusLkpKey]) REFERENCES [ClinicalCaseModule].[ProblemStatusLkp] ([ProblemStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

