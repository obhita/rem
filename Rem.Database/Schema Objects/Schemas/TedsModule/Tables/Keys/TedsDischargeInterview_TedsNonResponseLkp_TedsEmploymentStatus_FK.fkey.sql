ALTER TABLE [TedsModule].[TedsDischargeInterview]
    ADD CONSTRAINT [TedsDischargeInterview_TedsNonResponseLkp_TedsEmploymentStatus_FK] FOREIGN KEY ([TedsEmploymentStatusTedsNonResponseLkpKey]) REFERENCES [TedsModule].[TedsNonResponseLkp] ([TedsNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

