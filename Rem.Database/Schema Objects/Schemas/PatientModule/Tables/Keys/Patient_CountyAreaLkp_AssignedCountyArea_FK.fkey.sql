ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_CountyAreaLkp_AssignedCountyArea_FK] FOREIGN KEY ([AssignedCountyAreaLkpKey]) REFERENCES [CommonModule].[CountyAreaLkp] ([CountyAreaLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

