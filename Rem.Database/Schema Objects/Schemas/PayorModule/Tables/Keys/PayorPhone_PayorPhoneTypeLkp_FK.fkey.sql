ALTER TABLE [PayorModule].[PayorPhone]
    ADD CONSTRAINT [PayorPhone_PayorPhoneTypeLkp_FK] FOREIGN KEY ([PayorPhoneTypeLkpKey]) REFERENCES [PayorModule].[PayorPhoneTypeLkp] ([PayorPhoneTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

