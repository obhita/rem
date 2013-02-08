ALTER TABLE [ClinicalCaseModule].[ClinicalCase]
    ADD CONSTRAINT [ClinicalCase_Location_InitialLocation_FK] FOREIGN KEY ([InitialLocationKey]) REFERENCES [AgencyModule].[Location] ([LocationKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

