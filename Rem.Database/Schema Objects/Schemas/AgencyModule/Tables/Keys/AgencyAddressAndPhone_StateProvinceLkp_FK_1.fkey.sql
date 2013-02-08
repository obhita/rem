ALTER TABLE [AgencyModule].[AgencyAddressAndPhone]
    ADD CONSTRAINT [AgencyAddressAndPhone_StateProvinceLkp_FK] FOREIGN KEY ([StateProvinceLkpKey]) REFERENCES [CommonModule].[StateProvinceLkp] ([StateProvinceLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

