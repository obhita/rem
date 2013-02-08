ALTER TABLE [VisitModule].[VitalSign]
    ADD CONSTRAINT [VitalSign_VitalSignPhysicalExamNotDoneReasonLkp_FK] FOREIGN KEY ([VitalSignPhysicalExamNotDoneReasonLkpKey]) REFERENCES [VisitModule].[VitalSignPhysicalExamNotDoneReasonLkp] ([VitalSignPhysicalExamNotDoneReasonLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

