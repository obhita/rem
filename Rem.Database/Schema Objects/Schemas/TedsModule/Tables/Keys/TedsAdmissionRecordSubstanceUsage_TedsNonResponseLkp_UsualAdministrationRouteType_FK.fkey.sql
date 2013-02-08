ALTER TABLE [TedsModule].[TedsAdmissionRecordSubstanceUsage]
    ADD CONSTRAINT [TedsAdmissionRecordSubstanceUsage_TedsNonResponseLkp_UsualAdministrationRouteType_FK] FOREIGN KEY ([UsualAdministrationRouteTypeTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

