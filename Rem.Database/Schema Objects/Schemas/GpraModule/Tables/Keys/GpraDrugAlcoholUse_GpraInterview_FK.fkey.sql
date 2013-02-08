ALTER TABLE [GpraModule].[GpraDrugAlcoholUse]
    ADD CONSTRAINT [GpraDrugAlcoholUse_GpraInterview_FK] FOREIGN KEY ([GpraInterviewKey]) REFERENCES [GpraModule].[GpraInterview] ([ActivityKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

