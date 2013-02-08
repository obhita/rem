ALTER TABLE [AgencyModule].[AgencyAddressAndPhone]
    ADD CONSTRAINT [AgencyAddressAndPhone_CountyAreaLkp_FK] FOREIGN KEY ([CountyAreaLkpKey]) REFERENCES [CommonModule].[CountyAreaLkp] ([CountyAreaLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

