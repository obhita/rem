ALTER TABLE [BillingOfficeModule].[BillingOfficeAddress]
    ADD CONSTRAINT [BillingOfficeAddress_BillingOfficeAddressTypeLkp_FK] FOREIGN KEY ([BillingOfficeAddressTypeLkpKey]) REFERENCES [BillingOfficeModule].[BillingOfficeAddressTypeLkp] ([BillingOfficeAddressTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

