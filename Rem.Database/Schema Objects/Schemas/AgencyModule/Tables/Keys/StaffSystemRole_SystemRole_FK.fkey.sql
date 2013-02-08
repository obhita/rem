ALTER TABLE [AgencyModule].[StaffSystemRole]
    ADD CONSTRAINT [StaffSystemRole_SystemRole_FK] FOREIGN KEY ([SystemRoleKey]) REFERENCES [SecurityModule].[SystemRole] ([SystemRoleKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

