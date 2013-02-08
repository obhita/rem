ALTER TABLE [EncounterModule].[Encounter]
    ADD CONSTRAINT [Encounter_Staff_ServiceProviderStaff_FK] FOREIGN KEY ([ServiceProviderStaffKey]) REFERENCES [AgencyModule].[Staff] ([StaffKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

