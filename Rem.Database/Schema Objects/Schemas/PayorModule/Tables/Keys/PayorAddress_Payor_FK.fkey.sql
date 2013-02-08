ALTER TABLE [PayorModule].[PayorAddress]
    ADD CONSTRAINT [PayorAddress_Payor_FK] FOREIGN KEY ([PayorKey]) REFERENCES [PayorModule].[Payor] ([PayorKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

