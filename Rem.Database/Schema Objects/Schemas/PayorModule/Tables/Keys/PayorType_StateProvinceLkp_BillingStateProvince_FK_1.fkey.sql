ALTER TABLE [PayorModule].[PayorType]
    ADD CONSTRAINT [PayorType_StateProvinceLkp_BillingStateProvince_FK] FOREIGN KEY ([BillingStateProvinceLkpKey]) REFERENCES [CommonModule].[StateProvinceLkp] ([StateProvinceLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

