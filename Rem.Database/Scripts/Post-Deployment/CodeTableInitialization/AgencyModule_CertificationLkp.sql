
print '------------------------------------------------------------------------------------------'
print 'AgencyModule_CertificationLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [AgencyModule].[CertificationLkp] ( [CertificationLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 100, current_timestamp, 1, current_timestamp, 1, 1, N'First Aid', NULL, NULL, NULL, NULL, '3/31/2011', NULL, 0, 0 )
INSERT INTO [AgencyModule].[CertificationLkp] ( [CertificationLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 101, current_timestamp, 1, current_timestamp, 1, 1,       N'CPR', NULL, NULL, NULL, NULL, '3/31/2011', NULL, 0, 0 )
INSERT INTO [AgencyModule].[CertificationLkp] ( [CertificationLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 102, current_timestamp, 1, current_timestamp, 1, 1,       N'CAC', NULL, NULL, NULL, NULL, '3/31/2011', NULL, 0, 0 )

GO
