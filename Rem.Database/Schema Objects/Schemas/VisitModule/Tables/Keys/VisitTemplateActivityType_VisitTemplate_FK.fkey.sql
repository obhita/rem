ALTER TABLE [VisitModule].[VisitTemplateActivityType]
    ADD CONSTRAINT [VisitTemplateActivityType_VisitTemplate_FK] FOREIGN KEY ([VisitTemplateKey]) REFERENCES [VisitModule].[VisitTemplate] ([VisitTemplateKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

