ALTER TABLE [AgencyModule].[AgencyAddressAndPhone]
    ADD CONSTRAINT [AgencyAddressAndPhone_Agency_FK] FOREIGN KEY ([AgencyKey]) REFERENCES [AgencyModule].[Agency] ([AgencyKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

