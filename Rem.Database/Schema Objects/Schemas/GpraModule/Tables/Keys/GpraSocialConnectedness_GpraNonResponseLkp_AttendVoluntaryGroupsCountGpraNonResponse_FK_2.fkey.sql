ALTER TABLE [GpraModule].[GpraSocialConnectedness]
    ADD CONSTRAINT [GpraSocialConnectedness_GpraNonResponseLkp_AttendVoluntaryGroupsCountGpraNonResponse_FK] FOREIGN KEY ([AttendVoluntaryGroupsCountGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

