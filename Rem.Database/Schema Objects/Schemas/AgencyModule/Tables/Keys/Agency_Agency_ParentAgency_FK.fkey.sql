ALTER TABLE [AgencyModule].[Agency]
    ADD CONSTRAINT [Agency_Agency_ParentAgency_FK] FOREIGN KEY ([ParentAgencyKey]) REFERENCES [AgencyModule].[Agency] ([AgencyKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

