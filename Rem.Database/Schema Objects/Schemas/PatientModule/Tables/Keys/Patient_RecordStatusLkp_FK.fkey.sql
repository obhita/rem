ALTER TABLE [PatientModule].[Patient]
    ADD CONSTRAINT [Patient_RecordStatusLkp_FK] FOREIGN KEY ([RecordStatusLkpKey]) REFERENCES [CommonModule].[RecordStatusLkp] ([RecordStatusLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

