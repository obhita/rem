ALTER TABLE [DensAsiModule].[DensAsiClosure]
    ADD CONSTRAINT [DensAsiClosure_DensAsiTreatmentModalityLkp_MostAppropriateDensAsiTreatmentModality_FK] FOREIGN KEY ([MostAppropriateDensAsiTreatmentModalityLkpKey]) REFERENCES [DensAsiModule].[DensAsiTreatmentModalityLkp] ([DensAsiTreatmentModalityLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

