ALTER TABLE [PatientAccountModule].[PatientAccount]
    ADD CONSTRAINT [PatientAccount_CountyAreaLkp_HomeCountyArea_FK] FOREIGN KEY ([HomeCountyAreaLkpKey]) REFERENCES [CommonModule].[CountyAreaLkp] ([CountyAreaLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

