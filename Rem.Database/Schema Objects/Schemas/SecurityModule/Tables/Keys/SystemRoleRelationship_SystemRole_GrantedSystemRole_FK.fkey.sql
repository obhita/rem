ALTER TABLE [SecurityModule].[SystemRoleRelationship]
    ADD CONSTRAINT [SystemRoleRelationship_SystemRole_GrantedSystemRole_FK] FOREIGN KEY ([GrantedSystemRoleKey]) REFERENCES [SecurityModule].[SystemRole] ([SystemRoleKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

