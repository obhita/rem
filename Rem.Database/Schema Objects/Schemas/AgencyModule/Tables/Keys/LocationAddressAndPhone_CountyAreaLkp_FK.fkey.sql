ALTER TABLE [AgencyModule].[LocationAddressAndPhone]
    ADD CONSTRAINT [LocationAddressAndPhone_CountyAreaLkp_FK] FOREIGN KEY ([CountyAreaLkpKey]) REFERENCES [CommonModule].[CountyAreaLkp] ([CountyAreaLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

