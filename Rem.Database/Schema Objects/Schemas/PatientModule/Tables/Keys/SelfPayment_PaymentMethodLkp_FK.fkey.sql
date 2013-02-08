ALTER TABLE [PatientModule].[SelfPayment]
    ADD CONSTRAINT [SelfPayment_PaymentMethodLkp_FK] FOREIGN KEY ([PaymentMethodLkpKey]) REFERENCES [CommonModule].[PaymentMethodLkp] ([PaymentMethodLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

