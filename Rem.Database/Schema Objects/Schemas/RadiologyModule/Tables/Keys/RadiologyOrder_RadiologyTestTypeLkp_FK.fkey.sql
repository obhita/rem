ALTER TABLE [RadiologyModule].[RadiologyOrder]
    ADD CONSTRAINT [RadiologyOrder_RadiologyTestTypeLkp_FK] FOREIGN KEY ([RadiologyTestTypeLkpKey]) REFERENCES [RadiologyModule].[RadiologyTestTypeLkp] ([RadiologyTestTypeLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

