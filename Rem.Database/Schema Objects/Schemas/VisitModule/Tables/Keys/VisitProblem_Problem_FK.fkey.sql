ALTER TABLE [VisitModule].[VisitProblem]
    ADD CONSTRAINT [VisitProblem_Problem_FK] FOREIGN KEY ([ProblemKey]) REFERENCES [ClinicalCaseModule].[Problem] ([ProblemKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

