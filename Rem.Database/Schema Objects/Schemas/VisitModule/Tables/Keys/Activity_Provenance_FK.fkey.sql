ALTER TABLE [VisitModule].[Activity]
    ADD CONSTRAINT [Activity_Provenance_FK] FOREIGN KEY ([ProvenanceKey]) REFERENCES [PatientModule].[Provenance] ([ProvenanceKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

