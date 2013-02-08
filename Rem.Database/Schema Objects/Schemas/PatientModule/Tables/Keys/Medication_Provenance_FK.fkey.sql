ALTER TABLE [PatientModule].[Medication]
    ADD CONSTRAINT [Medication_Provenance_FK] FOREIGN KEY ([ProvenanceKey]) REFERENCES [PatientModule].[Provenance] ([ProvenanceKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

