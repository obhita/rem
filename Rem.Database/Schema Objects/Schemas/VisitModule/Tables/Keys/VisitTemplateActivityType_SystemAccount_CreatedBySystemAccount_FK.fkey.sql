ALTER TABLE [VisitModule].[VisitTemplateActivityType]
    ADD CONSTRAINT [VisitTemplateActivityType_SystemAccount_CreatedBySystemAccount_FK] FOREIGN KEY ([CreatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

