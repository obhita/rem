ALTER TABLE [GpraModule].[GpraProfessionalInformation]
    ADD CONSTRAINT [GpraProfessionalInformation_GpraNonResponseLkp_WagesPretaxIncomeAmountGpraNonResponse_FK] FOREIGN KEY ([WagesPretaxIncomeAmountGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

