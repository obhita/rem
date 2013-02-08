ALTER TABLE [AgencyModule].[Staff]
    ADD CONSTRAINT [Staff_GenderLkp_FK] FOREIGN KEY ([GenderLkpKey]) REFERENCES [CommonModule].[GenderLkp] ([GenderLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;



