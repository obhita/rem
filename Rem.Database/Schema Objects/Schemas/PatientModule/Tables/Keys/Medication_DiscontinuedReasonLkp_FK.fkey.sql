ALTER TABLE [PatientModule].[Medication]
    ADD CONSTRAINT [Medication_DiscontinuedReasonLkp_FK] FOREIGN KEY ([DiscontinuedReasonLkpKey]) REFERENCES [PatientModule].[DiscontinuedReasonLkp] ([DiscontinuedReasonLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

