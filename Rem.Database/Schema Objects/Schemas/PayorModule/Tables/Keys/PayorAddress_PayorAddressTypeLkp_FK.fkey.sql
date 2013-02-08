ALTER TABLE [PayorModule].[PayorAddress]
    ADD CONSTRAINT [PayorAddress_PayorAddressTypeLkp_FK] FOREIGN KEY ([PayorAddressTypeLkpKey]) REFERENCES [PayorModule].[PayorAddressTypeLkp] ([PayorAddressTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

