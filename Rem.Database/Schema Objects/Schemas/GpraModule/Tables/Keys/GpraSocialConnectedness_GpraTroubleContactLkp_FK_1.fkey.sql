ALTER TABLE [GpraModule].[GpraSocialConnectedness]
    ADD CONSTRAINT [GpraSocialConnectedness_GpraTroubleContactLkp_FK] FOREIGN KEY ([GpraTroubleContactLkpKey]) REFERENCES [GpraModule].[GpraTroubleContactLkp] ([GpraTroubleContactLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

