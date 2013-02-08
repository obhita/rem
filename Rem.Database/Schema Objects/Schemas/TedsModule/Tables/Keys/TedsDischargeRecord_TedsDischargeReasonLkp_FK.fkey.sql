ALTER TABLE [TedsModule].[TedsDischargeRecord]
    ADD CONSTRAINT [TedsDischargeRecord_TedsDischargeReasonLkp_FK] FOREIGN KEY ([TedsDischargeReasonLkpKey]) REFERENCES [TedsModule].[TedsDischargeReasonLkp] ([TedsDischargeReasonLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

