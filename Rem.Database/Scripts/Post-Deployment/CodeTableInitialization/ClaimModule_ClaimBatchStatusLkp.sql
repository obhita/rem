
print '------------------------------------------------------------------------------------------'
print 'ClaimModule_ClaimBatchStatusLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [ClaimModule].[ClaimBatchStatusLkp] ( [ClaimBatchStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 100, current_timestamp, 1, current_timestamp, 1, 1,             N'Active', N'A new claim batch is just created and ready to add more claims to it.',           N'Active', NULL, NULL, '3/31/2011',        NULL, 0, 0 )
INSERT INTO [ClaimModule].[ClaimBatchStatusLkp] ( [ClaimBatchStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 101, current_timestamp, 1, current_timestamp, 1, 1, N'Hcc 837P Generated',               N'HCC 837 p has been generated and claim batch is sealed.', N'Hcc837PGenerated', NULL, NULL, '3/31/2011',        NULL, 0, 0 )
INSERT INTO [ClaimModule].[ClaimBatchStatusLkp] ( [ClaimBatchStatusLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES ( 102, current_timestamp, 1, current_timestamp, 1, 1,             N'Closed',                   N'A claim batch is closed and cannot add more claims.',           N'Closed', NULL, NULL, '3/31/2011',        NULL, 0, 0 )

GO
