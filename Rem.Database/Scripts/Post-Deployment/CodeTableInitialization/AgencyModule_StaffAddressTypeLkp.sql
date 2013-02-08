
print '------------------------------------------------------------------------------------------'
print 'AgencyModule_StaffAddressTypeLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [AgencyModule].[StaffAddressTypeLkp] ( [StaffAddressTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 100, current_timestamp, 1, current_timestamp, 1, 0,    N'Home', NULL,  N'H',  N'H',    1, '3/31/2011', NULL, 0, 0 )
INSERT INTO [AgencyModule].[StaffAddressTypeLkp] ( [StaffAddressTypeLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 101, current_timestamp, 1, current_timestamp, 1, 0, N'Mailing', NULL, N'HL', N'HL', 1000, '3/31/2011', NULL, 0, 0 )

GO
