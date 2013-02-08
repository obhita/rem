
print '------------------------------------------------------------------------------------------'
print 'AgencyModule_LocationEmailAddressTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [AgencyModule].[LocationEmailAddressTypeLkp] ( [LocationEmailAddressTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 5695326, current_timestamp, 1, current_timestamp, 1, 0,  N'Office', NULL, N'OFC', N'OFC', 0, '1/1/1900', NULL, 0, 0 )
INSERT INTO [AgencyModule].[LocationEmailAddressTypeLkp] ( [LocationEmailAddressTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 7752229, current_timestamp, 1, current_timestamp, 1, 0, N'Billing', NULL, N'BIL', N'BIL', 1, '1/1/1900', NULL, 0, 0 )

GO
