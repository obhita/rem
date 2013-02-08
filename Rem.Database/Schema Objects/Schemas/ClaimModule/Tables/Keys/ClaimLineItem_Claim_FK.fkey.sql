ALTER TABLE [ClaimModule].[ClaimLineItem]
    ADD CONSTRAINT [ClaimLineItem_Claim_FK] FOREIGN KEY ([ClaimKey]) REFERENCES [ClaimModule].[Claim] ([ClaimKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

