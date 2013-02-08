ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_VeteranServiceBranchLkp_FK] FOREIGN KEY ([VeteranServiceBranchLkpKey]) REFERENCES [PatientModule].[VeteranServiceBranchLkp] ([VeteranServiceBranchLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

