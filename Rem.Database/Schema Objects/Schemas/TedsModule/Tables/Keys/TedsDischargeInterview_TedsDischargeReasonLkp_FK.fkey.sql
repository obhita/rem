ALTER TABLE [TedsModule].[TedsDischargeInterview]
    ADD CONSTRAINT [TedsDischargeInterview_TedsDischargeReasonLkp_FK] FOREIGN KEY ([TedsDischargeReasonLkpKey]) REFERENCES [TedsModule].[TedsDischargeReasonLkp] ([TedsDischargeReasonLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

