ALTER TABLE [PatientAccountModule].[PayorCoverage]
    ADD CONSTRAINT [PayorCoverage_Payor_FK] FOREIGN KEY ([PayorKey]) REFERENCES [PayorModule].[Payor] ([PayorKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

