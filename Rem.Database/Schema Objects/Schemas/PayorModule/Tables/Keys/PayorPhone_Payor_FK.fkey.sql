ALTER TABLE [PayorModule].[PayorPhone]
    ADD CONSTRAINT [PayorPhone_Payor_FK] FOREIGN KEY ([PayorKey]) REFERENCES [PayorModule].[Payor] ([PayorKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

