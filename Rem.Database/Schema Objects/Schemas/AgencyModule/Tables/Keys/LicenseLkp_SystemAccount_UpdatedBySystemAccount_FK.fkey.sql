﻿ALTER TABLE [AgencyModule].[LicenseLkp]
    ADD CONSTRAINT [LicenseLkp_SystemAccount_UpdatedBySystemAccount_FK] FOREIGN KEY ([UpdatedBySystemAccountKey]) REFERENCES [SecurityModule].[SystemAccount] ([SystemAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

