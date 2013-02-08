ALTER TABLE [LabModule].[LabResult]
    ADD CONSTRAINT [LabResult_LabTest_FK] FOREIGN KEY ([LabTestKey]) REFERENCES [LabModule].[LabTest] ([LabTestKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

