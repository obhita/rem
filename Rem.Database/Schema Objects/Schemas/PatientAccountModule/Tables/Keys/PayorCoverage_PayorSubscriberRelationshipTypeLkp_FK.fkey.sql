ALTER TABLE [PatientAccountModule].[PayorCoverage]
    ADD CONSTRAINT [PayorCoverage_PayorSubscriberRelationshipTypeLkp_FK] FOREIGN KEY ([PayorSubscriberRelationshipTypeLkpKey]) REFERENCES [PatientAccountModule].[PayorSubscriberRelationshipTypeLkp] ([PayorSubscriberRelationshipTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

