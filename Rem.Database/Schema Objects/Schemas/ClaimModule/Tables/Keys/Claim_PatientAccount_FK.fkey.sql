﻿ALTER TABLE [ClaimModule].[Claim]
    ADD CONSTRAINT [Claim_PatientAccount_FK] FOREIGN KEY ([PatientAccountKey]) REFERENCES [PatientAccountModule].[PatientAccount] ([PatientAccountKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

