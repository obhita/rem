
print '------------------------------------------------------------------------------------------'
print 'ProgramModule_GenderSpecificationLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [ProgramModule].[GenderSpecificationLkp] ( [GenderSpecificationLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 100, current_timestamp, 1, current_timestamp, 1, 1, N'Male', NULL, NULL, NULL, NULL, '3/31/2011', NULL, 0, 0 )
INSERT INTO [ProgramModule].[GenderSpecificationLkp] ( [GenderSpecificationLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 101, current_timestamp, 1, current_timestamp, 1, 1,      N'Female', NULL, NULL, NULL, NULL, '3/31/2011', NULL, 0, 0 )
INSERT INTO [ProgramModule].[GenderSpecificationLkp] ( [GenderSpecificationLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 102, current_timestamp, 1, current_timestamp, 1, 1,  N'Not Specific', NULL, NULL, NULL, NULL, '3/31/2011', NULL, 0, 0 )

GO
