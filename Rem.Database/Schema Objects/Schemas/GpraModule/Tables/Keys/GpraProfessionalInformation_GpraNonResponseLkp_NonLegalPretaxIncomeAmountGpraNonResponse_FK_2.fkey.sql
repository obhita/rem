ALTER TABLE [GpraModule].[GpraProfessionalInformation]
    ADD CONSTRAINT [GpraProfessionalInformation_GpraNonResponseLkp_NonLegalPretaxIncomeAmountGpraNonResponse_FK] FOREIGN KEY ([NonLegalPretaxIncomeAmountGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

