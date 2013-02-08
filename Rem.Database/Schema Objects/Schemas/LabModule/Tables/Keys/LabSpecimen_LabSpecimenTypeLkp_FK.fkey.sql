ALTER TABLE [LabModule].[LabSpecimen]
    ADD CONSTRAINT [LabSpecimen_LabSpecimenTypeLkp_FK] FOREIGN KEY ([LabSpecimenTypeLkpKey]) REFERENCES [LabModule].[LabSpecimenTypeLkp] ([LabSpecimenTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

