ALTER TABLE [SecurityModule].[SystemRolePermission]
    ADD CONSTRAINT [SystemRolePermission_SystemPermission_FK] FOREIGN KEY ([SystemPermissionKey]) REFERENCES [SecurityModule].[SystemPermission] ([SystemPermissionKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

