ALTER TABLE [GpraModule].[GpraProfessionalInformation]
    ADD CONSTRAINT [GpraProfessionalInformation_GpraNonResponseLkp_DisabilityPretaxIncomeAmountGpraNonResponse_FK] FOREIGN KEY ([DisabilityPretaxIncomeAmountGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

