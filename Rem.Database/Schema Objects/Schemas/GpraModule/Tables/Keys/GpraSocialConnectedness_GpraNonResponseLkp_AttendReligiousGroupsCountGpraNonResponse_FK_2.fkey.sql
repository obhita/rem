ALTER TABLE [GpraModule].[GpraSocialConnectedness]
    ADD CONSTRAINT [GpraSocialConnectedness_GpraNonResponseLkp_AttendReligiousGroupsCountGpraNonResponse_FK] FOREIGN KEY ([AttendReligiousGroupsCountGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

