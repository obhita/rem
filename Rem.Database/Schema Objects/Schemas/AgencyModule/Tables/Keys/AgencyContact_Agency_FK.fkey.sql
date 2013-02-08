ALTER TABLE [AgencyModule].[AgencyContact]
    ADD CONSTRAINT [AgencyContact_Agency_FK] FOREIGN KEY ([AgencyKey]) REFERENCES [AgencyModule].[Agency] ([AgencyKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

