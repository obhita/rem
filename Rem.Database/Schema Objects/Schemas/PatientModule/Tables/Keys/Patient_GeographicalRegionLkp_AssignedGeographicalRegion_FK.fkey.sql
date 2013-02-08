ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_GeographicalRegionLkp_AssignedGeographicalRegion_FK] FOREIGN KEY ([AssignedGeographicalRegionLkpKey]) REFERENCES [CommonModule].[GeographicalRegionLkp] ([GeographicalRegionLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

