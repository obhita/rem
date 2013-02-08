﻿CREATE NONCLUSTERED INDEX [TedsAdmissionInterview_TedsNonResponseLkp_LivingArrangementsType_FK_IDX]
    ON [TedsModule].[TedsAdmissionInterview]([LivingArrangementsTypeTedsNonResponseLkpKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];


