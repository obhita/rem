ALTER TABLE [LabModule].[LabTest]
    ADD CONSTRAINT [LabTest_LabSpecimen_FK] FOREIGN KEY ([LabSpecimenKey]) REFERENCES [LabModule].[LabSpecimen] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

