﻿ALTER TABLE [GpraModule].[GpraDrugAlcoholUse]
    ADD CONSTRAINT [GpraDrugAlcoholUse_GpraNonResponseLkp_HeroinGpraDrugRouteGpraNonResponse_FK] FOREIGN KEY ([HeroinGpraDrugRouteGpraNonResponseLkpKey]) REFERENCES [GpraModule].[GpraNonResponseLkp] ([GpraNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

