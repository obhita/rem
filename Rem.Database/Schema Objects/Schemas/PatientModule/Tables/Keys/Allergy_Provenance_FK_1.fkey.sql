ALTER TABLE [PatientModule].[Allergy]
    ADD CONSTRAINT [Allergy_Provenance_FK] FOREIGN KEY ([ProvenanceKey]) REFERENCES [PatientModule].[Provenance] ([ProvenanceKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

