﻿ALTER TABLE [DensAsiModule].[DensAsiLegalStatus]
    ADD CONSTRAINT [DensAsiLegalStatus_DensAsiNonResponseLkp_ArrestedChargedDrugChargesCount_FK] FOREIGN KEY ([ArrestedChargedDrugChargesCountDensAsiNonResponseLkpKey]) REFERENCES [DensAsiModule].[DensAsiNonResponseLkp] ([DensAsiNonResponseLkpKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

