ALTER TABLE [DensAsiModule].[DensAsiDsmIv]
    ADD CONSTRAINT [DensAsiDsmIv_DensAsiNonResponseLkp_DrugAbuseInabilityToMeetResponsibilitiesIndicator_FK] FOREIGN KEY ([DrugAbuseInabilityToMeetResponsibilitiesIndicatorDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

