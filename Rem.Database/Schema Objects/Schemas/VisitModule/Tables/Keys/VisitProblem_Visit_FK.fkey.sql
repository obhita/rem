ALTER TABLE [VisitModule].[VisitProblem]
    ADD CONSTRAINT [VisitProblem_Visit_FK] FOREIGN KEY ([VisitKey]) REFERENCES [VisitModule].[Visit] ([AppointmentKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



