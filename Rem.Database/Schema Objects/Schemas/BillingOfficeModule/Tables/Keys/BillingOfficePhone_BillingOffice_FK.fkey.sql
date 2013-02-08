ALTER TABLE [BillingOfficeModule].[BillingOfficePhone]
    ADD CONSTRAINT [BillingOfficePhone_BillingOffice_FK] FOREIGN KEY ([BillingOfficeKey]) REFERENCES [BillingOfficeModule].[BillingOffice] ([BillingOfficeKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

