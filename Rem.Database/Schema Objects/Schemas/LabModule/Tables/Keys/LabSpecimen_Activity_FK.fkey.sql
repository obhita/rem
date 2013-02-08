ALTER TABLE [LabModule].[LabSpecimen]
    ADD CONSTRAINT [LabSpecimen_Activity_FK] FOREIGN KEY ([ActivityKey]) REFERENCES [VisitModule].[Activity] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

