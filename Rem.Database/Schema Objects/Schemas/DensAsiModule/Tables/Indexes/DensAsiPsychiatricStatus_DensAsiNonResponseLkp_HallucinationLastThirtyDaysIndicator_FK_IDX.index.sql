﻿CREATE NONCLUSTERED INDEX [DensAsiPsychiatricStatus_DensAsiNonResponseLkp_HallucinationLastThirtyDaysIndicator_FK_IDX]
    ON [DensAsiModule].[DensAsiPsychiatricStatus]([HallucinationLastThirtyDaysIndicatorDensAsiNonResponseLkpKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];

