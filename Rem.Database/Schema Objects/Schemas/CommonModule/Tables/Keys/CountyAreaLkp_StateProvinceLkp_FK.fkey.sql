ALTER TABLE [CommonModule].[CountyAreaLkp]
    ADD CONSTRAINT [CountyAreaLkp_StateProvinceLkp_FK] FOREIGN KEY ([StateProvinceLkpKey]) REFERENCES [CommonModule].[StateProvinceLkp] ([StateProvinceLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

