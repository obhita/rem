ALTER TABLE [AgencyModule].[Agency]
    ADD CONSTRAINT [Agency_GeographicalRegionLkp_FK] FOREIGN KEY ([GeographicalRegionLkpKey]) REFERENCES [CommonModule].[GeographicalRegionLkp] ([GeographicalRegionLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

