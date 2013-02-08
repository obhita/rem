ALTER TABLE [PayorModule].[PayorTypeMember]
    ADD CONSTRAINT [PayorTypeMember_Payor_FK] FOREIGN KEY ([PayorKey]) REFERENCES [PayorModule].[Payor] ([PayorKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

