﻿ALTER TABLE [PatientModule].[PatientContact]
    ADD CONSTRAINT [PatientContact_CountryLkp_FK] FOREIGN KEY ([CountryLkpKey]) REFERENCES [CommonModule].[CountryLkp] ([CountryLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

