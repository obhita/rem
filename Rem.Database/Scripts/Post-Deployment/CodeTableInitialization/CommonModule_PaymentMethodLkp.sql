
print '------------------------------------------------------------------------------------------'
print 'CommonModule_PaymentMethodLkp.sql'
print '------------------------------------------------------------------------------------------'

INSERT INTO [CommonModule].[PaymentMethodLkp] ( [PaymentMethodLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES (   1, current_timestamp, 1, current_timestamp, 1, 0,         N'Cash', NULL, N'CA', N'CA', 1000, '1/1/1900',  '1/1/1900', 0, 0 )
INSERT INTO [CommonModule].[PaymentMethodLkp] ( [PaymentMethodLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES (   2, current_timestamp, 1, current_timestamp, 1, 0,        N'Check', NULL, N'CK', N'CK', 1000, '1/1/1900',  '1/1/1900', 0, 0 )
INSERT INTO [CommonModule].[PaymentMethodLkp] ( [PaymentMethodLkpKey], [CreatedTimestamp], [CreatedBySystemAccountKey], [UpdatedTimestamp], [UpdatedBySystemAccountKey], [Version], [Name], [Description], [WellKnownName], [ShortName], [SortOrderNumber], [EffectiveStartDate], [EffectiveEndDate], [SystemOwnedIndicator], [DefaultIndicator]  ) VALUES (   3, current_timestamp, 1, current_timestamp, 1, 0,  N'Credit Card', NULL, N'CC', N'CC', 1000, '1/1/1900',  '1/1/1900', 0, 0 )

GO
