ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_CountyAreaLkp_BirthCountyArea_FK] FOREIGN KEY ([BirthCountyAreaLkpKey]) REFERENCES [CommonModule].[CountyAreaLkp] ([CountyAreaLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

