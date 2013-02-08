ALTER TABLE [AgencyModule].[Location]
    ADD CONSTRAINT [Location_GeographicalRegionLkp_FK] FOREIGN KEY ([GeographicalRegionLkpKey]) REFERENCES [CommonModule].[GeographicalRegionLkp] ([GeographicalRegionLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

