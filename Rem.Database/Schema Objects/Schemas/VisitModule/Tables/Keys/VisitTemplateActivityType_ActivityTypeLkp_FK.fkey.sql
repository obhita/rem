ALTER TABLE [VisitModule].[VisitTemplateActivityType]
    ADD CONSTRAINT [VisitTemplateActivityType_ActivityTypeLkp_FK] FOREIGN KEY ([ActivityTypeLkpKey]) REFERENCES [VisitModule].[ActivityTypeLkp] ([ActivityTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

