ALTER TABLE [GpraModule].[GpraFamilyLivingConditions]
    ADD CONSTRAINT [GpraFamilyLivingConditions_GpraHousingTypeLkp_FK] FOREIGN KEY ([GpraHousingTypeLkpKey]) REFERENCES [GpraModule].[GpraHousingTypeLkp] ([GpraHousingTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

