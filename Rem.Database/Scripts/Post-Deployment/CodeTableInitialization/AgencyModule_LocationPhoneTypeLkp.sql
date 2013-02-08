
print '------------------------------------------------------------------------------------------'
print 'AgencyModule_LocationPhoneTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [AgencyModule].[LocationPhoneTypeLkp] ( [LocationPhoneTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 100, current_timestamp, 1, current_timestamp, 1, 1, N'Toll Free', NULL, NULL, NULL, NULL, '3/31/2011', NULL, 0, 0 )
INSERT INTO [AgencyModule].[LocationPhoneTypeLkp] ( [LocationPhoneTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 101, current_timestamp, 1, current_timestamp, 1, 1,       N'Fax', NULL, N'Fax', NULL, NULL, '3/31/2011', NULL, 0, 0 )
INSERT INTO [AgencyModule].[LocationPhoneTypeLkp] ( [LocationPhoneTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 102, current_timestamp, 1, current_timestamp, 1, 1,      N'Main', NULL,  N'Main', NULL, NULL, '3/31/2011', NULL, 0, 0 )
INSERT INTO [AgencyModule].[LocationPhoneTypeLkp] ( [LocationPhoneTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 103, current_timestamp, 1, current_timestamp, 1, 1, N'Emergency', NULL, NULL, NULL, NULL, '3/31/2011', NULL, 0, 0 )

GO
