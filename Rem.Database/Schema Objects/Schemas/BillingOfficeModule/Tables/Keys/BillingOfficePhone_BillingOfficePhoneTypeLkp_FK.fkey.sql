ALTER TABLE [BillingOfficeModule].[BillingOfficePhone]
    ADD CONSTRAINT [BillingOfficePhone_BillingOfficePhoneTypeLkp_FK] FOREIGN KEY ([BillingOfficePhoneTypeLkpKey]) REFERENCES [BillingOfficeModule].[BillingOfficePhoneTypeLkp] ([BillingOfficePhoneTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

