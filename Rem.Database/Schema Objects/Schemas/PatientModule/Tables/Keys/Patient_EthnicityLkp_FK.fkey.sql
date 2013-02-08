ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_EthnicityLkp_FK] FOREIGN KEY ([EthnicityLkpKey]) REFERENCES [PatientModule].[EthnicityLkp] ([EthnicityLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

