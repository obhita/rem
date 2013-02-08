
print '------------------------------------------------------------------------------------------'
print 'PatientModule_ContactPreferenceLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [PatientModule].[ContactPreferenceLkp] ( [ContactPreferenceLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 1, current_timestamp, 1, current_timestamp, 1, 0,  N'Email',  N'Email contact preference',  N'Email', N'EM', 1, '5/23/2011', '5/23/2011', 1, 1 )
INSERT INTO [PatientModule].[ContactPreferenceLkp] ( [ContactPreferenceLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 2, current_timestamp, 1, current_timestamp, 1, 0, N'Letter', N'Letter contact preference', N'Letter', N'LE', 2, '5/23/2011', '5/23/2011', 1, 1 )
INSERT INTO [PatientModule].[ContactPreferenceLkp] ( [ContactPreferenceLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 3, current_timestamp, 1, current_timestamp, 1, 0,  N'Phone',  N'Phone contact preference',  N'Phone', N'PH', 3, '5/23/2011', '5/23/2011', 1, 1 )

GO
