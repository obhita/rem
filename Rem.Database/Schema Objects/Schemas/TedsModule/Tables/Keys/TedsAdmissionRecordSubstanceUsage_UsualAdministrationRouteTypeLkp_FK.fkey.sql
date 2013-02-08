ALTER TABLE [TedsModule].[TedsAdmissionRecordSubstanceUsage]
    ADD CONSTRAINT [TedsAdmissionRecordSubstanceUsage_UsualAdministrationRouteTypeLkp_FK] FOREIGN KEY ([UsualAdministrationRouteTypeLkpKey]) REFERENCES [TedsModule].[UsualAdministrationRouteTypeLkp] ([UsualAdministrationRouteTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

