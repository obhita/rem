ALTER TABLE [PayorModule].[PayorType]
    ADD CONSTRAINT [PayorType_CountyAreaLkp_BillingCountyArea_FK] FOREIGN KEY ([BillingCountyAreaLkpKey]) REFERENCES [CommonModule].[CountyAreaLkp] ([CountyAreaLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

