ALTER TABLE [SbirtModule].[NidaDrugQuestionnaire]
    ADD CONSTRAINT [NidaDrugQuestionnaire_Activity_FK] FOREIGN KEY ([ActivityKey]) REFERENCES [VisitModule].[Activity] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

