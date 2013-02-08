ALTER TABLE [GpraModule].[GpraProfessionalInformation]
    ADD CONSTRAINT [GpraProfessionalInformation_GpraNonResponseLkp_FamilyFriendsPretaxIncomeAmountGpraNonResponse_FK] FOREIGN KEY ([FamilyFriendsPretaxIncomeAmountGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

