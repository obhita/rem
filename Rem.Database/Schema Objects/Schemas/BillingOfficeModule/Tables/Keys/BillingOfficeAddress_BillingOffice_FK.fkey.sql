ALTER TABLE [BillingOfficeModule].[BillingOfficeAddress]
    ADD CONSTRAINT [BillingOfficeAddress_BillingOffice_FK] FOREIGN KEY ([BillingOfficeKey]) REFERENCES [BillingOfficeModule].[BillingOffice] ([BillingOfficeKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

