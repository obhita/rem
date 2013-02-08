ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_DetailedEthnicityLkp_FK] FOREIGN KEY ([DetailedEthnicityLkpKey]) REFERENCES [PatientModule].[DetailedEthnicityLkp] ([DetailedEthnicityLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

