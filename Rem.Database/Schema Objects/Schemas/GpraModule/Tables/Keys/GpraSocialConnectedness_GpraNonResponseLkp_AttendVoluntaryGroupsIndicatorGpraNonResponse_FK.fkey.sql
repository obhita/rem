ALTER TABLE [GpraModule].[GpraSocialConnectedness]
    ADD CONSTRAINT [GpraSocialConnectedness_GpraNonResponseLkp_AttendVoluntaryGroupsIndicatorGpraNonResponse_FK] FOREIGN KEY ([AttendVoluntaryGroupsIndicatorGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

