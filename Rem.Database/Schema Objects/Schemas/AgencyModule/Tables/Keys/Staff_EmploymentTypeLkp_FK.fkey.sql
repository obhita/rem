ALTER TABLE [AgencyModule].[Staff]
    ADD CONSTRAINT [Staff_EmploymentTypeLkp_FK] FOREIGN KEY ([EmploymentTypeLkpKey]) REFERENCES [AgencyModule].[EmploymentTypeLkp] ([EmploymentTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

