ALTER TABLE [PatientModule].[CdsRule]
    ADD CONSTRAINT [CdsRule_LabTestNameLkp_FK] FOREIGN KEY ([LabTestNameLkpKey]) REFERENCES [LabModule].[LabTestNameLkp] ([LabTestNameLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

