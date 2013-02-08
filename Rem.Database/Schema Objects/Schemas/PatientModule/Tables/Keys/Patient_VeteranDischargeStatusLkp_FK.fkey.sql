ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_VeteranDischargeStatusLkp_FK] FOREIGN KEY ([VeteranDischargeStatusLkpKey]) REFERENCES [PatientModule].[VeteranDischargeStatusLkp] ([VeteranDischargeStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

