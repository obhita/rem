﻿ALTER TABLE [AgencyModule].[AgencyAddressAndPhone]
    ADD CONSTRAINT [AgencyAddressAndPhone_CountryLkp_FK] FOREIGN KEY ([CountryLkpKey]) REFERENCES [CommonModule].[CountryLkp] ([CountryLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

