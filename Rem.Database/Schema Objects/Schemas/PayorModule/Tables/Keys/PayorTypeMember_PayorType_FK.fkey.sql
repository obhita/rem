ALTER TABLE [PayorModule].[PayorTypeMember]
    ADD CONSTRAINT [PayorTypeMember_PayorType_FK] FOREIGN KEY ([PayorTypeKey]) REFERENCES [PayorModule].[PayorType] ([PayorTypeKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

