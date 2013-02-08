ALTER TABLE [PayorModule].[Payor]
    ADD CONSTRAINT [Payor_PayorTypeMember_PrimaryPayorTypeMember_FK] FOREIGN KEY ([PrimaryPayorTypeMemberKey]) REFERENCES [PayorModule].[PayorTypeMember] ([PayorTypeMemberKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

