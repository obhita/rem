ALTER TABLE [VisitModule].[Procedure]
    ADD CONSTRAINT [Procedure_CodingContext_FK] FOREIGN KEY ([CodingContextKey]) REFERENCES [VisitModule].[CodingContext] ([CodingContextKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

