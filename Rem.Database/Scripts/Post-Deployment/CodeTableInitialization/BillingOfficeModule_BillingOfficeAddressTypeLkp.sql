print '------------------------------------------------------------------------------------------'
print 'BillingOfficeModule_BillingOfficeAddressTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [BillingOfficeModule].[BillingOfficeAddressTypeLkp] ( [BillingOfficeAddressTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1456680, current_timestamp, 1, current_timestamp, 1, 0,           N'Physical Address',   NULL,  N'PH',  N'PH', 1000, '1/1/1900',        NULL, 0, 0 )
INSERT INTO [BillingOfficeModule].[BillingOfficeAddressTypeLkp] ( [BillingOfficeAddressTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 3664656, current_timestamp, 1, current_timestamp, 1, 0,           N'Payment Address',    NULL,  N'PA',  N'PA',    2, '1/1/1900',        NULL, 0, 0 )

GO