
print '----------------------------------------------------------------------------------------------------'
print 'SecurityModule'
print '----------------------------------------------------------------------------------------------------'

print '----------------------------------------------------------------------------------------------------'
print 'SystemAccount'
print '----------------------------------------------------------------------------------------------------'
			
INSERT INTO [SecurityModule].[SystemAccount] ( [SystemAccountKey], [Version], [Identifier], [EmailAddress], [IdentityProviderUriIdentifier], [IdentityProviderName], [EnabledIndicator], [DisplayName]) VALUES (1, 1,'http://identityserver/someuniqueidentifierissuedbysecurityprovider', 'admin.account@admin.com', 'Uri:FakeProvider','FakeIdentityProvider', 1, 'Administrator Account')
GO

print '----------------------------------------------------------------------------------------------------'
print 'SystemPermission'
print '----------------------------------------------------------------------------------------------------'

declare @SystemPermissionKey    BIGINT = 1000;

print '-----'
print 'Agency Permissions'
print '-----'

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'agencymodule/agencyview',
    @DisplayName                = 'Agency View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'agencymodule/agencyedit',
    @DisplayName                = 'Agency Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'agencymodule/locationview',
    @DisplayName                = 'Location View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'agencymodule/locationedit',
    @DisplayName                = 'Location Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'agencymodule/staffview',
    @DisplayName                = 'Staff View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'agencymodule/staffedit',
    @DisplayName                = 'Staff Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'agencymodule/labresultview',
    @DisplayName                = 'Lab Result View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'agencymodule/labresultedit',
    @DisplayName                = 'Lab Result Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'agencymodule/labresultupload',
    @DisplayName                = 'Lab Result Upload',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'agencymodule/meaningfuluseview',
    @DisplayName                = 'Meaningful Use View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'agencymodule/meaningfuluseedit',
    @DisplayName                = 'Meaningful Use Edit',
    @Description                = '',
    @SystemAccountKey           = 1	
	
set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'agencymodule/logininformationview',
    @DisplayName                = 'Login Information (Context) View',
    @Description                = 'This Permission gives ability to change location context',
    @SystemAccountKey           = 1

print '-----'
print 'Infrastructure Permissions'
print '-----'

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'infrastructuremodule/accessuserinterface',
    @DisplayName                = 'Access User Interface',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'infrastructuremodule/executeemergencyaccess',
    @DisplayName                = 'Execute Emergency Access',
    @Description                = '',
    @SystemAccountKey           = 1

print '-----'
print 'New Crop Permissions'
print '-----'

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'newcropmodule/newcropaccess',
    @DisplayName                = 'New Crop Access',
    @Description                = '',
    @SystemAccountKey           = 1

print '-----'
print 'Report Permissions'
print '-----'

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'reportmodule/reportaccess',
    @DisplayName                = 'Report Access',
    @Description                = '',
    @SystemAccountKey           = 1

print '-----'
print 'Patient Permissions'
print '-----'

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/cdsrulesview',
    @DisplayName                = 'Cds Rules View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/cdsrulesedit',
    @DisplayName                = 'Cds Rules Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/clinicalcaseview',
    @DisplayName                = 'Clinical Case View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/clinicalcaseedit',
    @DisplayName                = 'Clinical Case Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/cliniciandashboardview',
    @DisplayName                = 'Clinician Dashboard View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/cliniciandashboardedit',
    @DisplayName                = 'Clinician Dashboard Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/cliniciandashboardalertsview',
    @DisplayName                = 'Clinician Dashboard Alerts View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/cliniciandashboardlabresultsview',
    @DisplayName                = 'Clinician Dashboard Lab Results View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/cliniciandashboardmedicationordersview',
    @DisplayName                = 'Clinician Dashboard Medication Orders View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/patientview',
    @DisplayName                = 'Patient View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/patientedit',
    @DisplayName                = 'Patient Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/patientexternalhistoryview',
    @DisplayName                = 'Patient External History View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/patientexternalhistoryedit',
    @DisplayName                = 'Patient External History Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/problemview',
    @DisplayName                = 'Problem View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/problemedit',
    @DisplayName                = 'Problem Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/visitview',
    @DisplayName                = 'Visit View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/visitedit',
    @DisplayName                = 'Visit Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/densasiview',
    @DisplayName                = 'Dens Asi View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/densasiedit',
    @DisplayName                = 'Dens Asi Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/tedsview',
    @DisplayName                = 'TEDS View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/tedsedit',
    @DisplayName                = 'TEDS Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/frontdeskdashboardview',
    @DisplayName                = 'Front Desk Dashboard View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/frontdeskdashboardedit',
    @DisplayName                = 'Front Desk Dashboard Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/gainshortscreenerview',
    @DisplayName                = 'Gain Short Screener View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/gainshortscreeneredit',
    @DisplayName                = 'Gain Short Screener Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/gprainterviewview',
    @DisplayName                = 'Gpra Interview View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/gprainterviewedit',
    @DisplayName                = 'Gpra Interview Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/patientaccesshistoryview',
    @DisplayName                = 'Patient Access History View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/auditcview',
    @DisplayName                = 'Audit C View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/auditcedit',
    @DisplayName                = 'Audit C Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/auditview',
    @DisplayName                = 'Audit View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/auditedit',
    @DisplayName                = 'Audit Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/briefinterventionview',
    @DisplayName                = 'Brief Intervention View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/briefinterventionedit',
    @DisplayName                = 'Brief Intervention Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/casesummaryview',
    @DisplayName                = 'Case Summary View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/casesummaryedit',
    @DisplayName                = 'Case Summary Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/casealertsview',
    @DisplayName                = 'Case Alerts View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/casealertsedit',
    @DisplayName                = 'Case Alerts Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/dast10view',
    @DisplayName                = 'Dast 10 View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/dast10edit',
    @DisplayName                = 'Dast 10 Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/immunizationview',
    @DisplayName                = 'Immunization View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/immunizationedit',
    @DisplayName                = 'Immunization Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/individualcounselingview',
    @DisplayName                = 'Individual Counseling View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/individualcounselingedit',
    @DisplayName                = 'Individual Counseling Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/labresultsview',
    @DisplayName                = 'Lab Results View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/labresultsedit',
    @DisplayName                = 'Lab Results Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/medicationview',
    @DisplayName                = 'Medication View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/medicationedit',
    @DisplayName                = 'Medication Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/nidaview',
    @DisplayName                = 'Nida View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/nidaedit',
    @DisplayName                = 'Nida Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/patientdashboardview',
    @DisplayName                = 'Patient Dashboard View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/patientdashboardedit',
    @DisplayName                = 'Patient Dashboard Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/phq9view',
    @DisplayName                = 'Phq 9 View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/phq9edit',
    @DisplayName                = 'Phq 9 Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/radiologyordersview',
    @DisplayName                = 'Radiology Orders View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/radiologyordersedit',
    @DisplayName                = 'Radiology Orders Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/scheduled&recentactivitiesview',
    @DisplayName                = 'Scheduled & Recent Activities View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/scheduled&recentactivitiesedit',
    @DisplayName                = 'Scheduled & Recent Activities Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/socialhistoryview',
    @DisplayName                = 'Social History View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/socialhistoryedit',
    @DisplayName                = 'Social History Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/vitalsignsview',
    @DisplayName                = 'Vital Signs View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/vitalsignsedit',
    @DisplayName                = 'Vital Signs Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/patientlistview',
    @DisplayName                = 'Patient List View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/patientremindersview',
    @DisplayName                = 'Patient Reminders View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/patientsearchview',
    @DisplayName                = 'Patient Search View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/patientworkspaceview',
    @DisplayName                = 'Patient Workspace View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/c32view',
    @DisplayName                = 'C32 View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/c32import',
    @DisplayName                = 'C32 Import',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/systemaccountview',
    @DisplayName                = 'System Account View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'patientmodule/interoperabilityworkspaceview',
    @DisplayName                = 'Interoperability Workspace View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'billingmodule/billingworkspaceview',
    @DisplayName                = 'Billing Workspace View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'billingmodule/billingofficeedit',
    @DisplayName                = 'Billing Office Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'billingmodule/billingofficeview',
    @DisplayName                = 'Billing Office View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'billingmodule/payorview',
    @DisplayName                = 'Payor View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'billingmodule/payoredit',
    @DisplayName                = 'Payor Edit',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'billingmodule/claimsview',
    @DisplayName                = 'Claims View',
    @Description                = '',
    @SystemAccountKey           = 1

set @SystemPermissionKey += 1;

exec SecurityModule.AddSystemPermission
    @SystemPermissionKey        = @SystemPermissionKey,
    @WellKnownName              = 'billingmodule/claimsedit',
    @DisplayName                = 'Claims Edit',
    @Description                = '',
    @SystemAccountKey           = 1
GO

print '----------------------------------------------------------------------------------------------------'
print 'SystemRole'
print '----------------------------------------------------------------------------------------------------'

declare @JobFunctionRole						NVARCHAR (255) = 'JobFunction';
declare @TaskRole								NVARCHAR (255) = 'Task';
declare @TaskGroupRole							NVARCHAR (255) = 'TaskGroup';

-- primary
declare @SystemRoleRemAdministrator             BIGINT = 100010080000;
declare @SystemRoleDemoUser                     BIGINT = 100010080001;
declare @SystemRoleFrontDeskUser                BIGINT = 100010080002;
declare @SystemRoleClinicianUser                BIGINT = 100010080003;

-- secondary
declare @SystemRoleExecuteEmergencyAccess       BIGINT = 100010080004;
declare @SystemRoleBasicUserInterfaceAccess     BIGINT = 100010080005;
declare @SystemRolePatientWorkspaceAccess       BIGINT = 100010080006;
declare @SystemRolePatientFinderAccess          BIGINT = 100010080007;
declare @SystemRoleClinicianDashboardAccess     BIGINT = 100010080008;
declare @SystemRoleFrontDeskDashboardAccess     BIGINT = 100010080009;
declare @SystemRoleAdministrativeAccess         BIGINT = 100010080010;
declare @SystemRoleReportAccess                 BIGINT = 100010080011;
declare @SystemRoleBillingAccess                BIGINT = 100010080012;

-- JobFunction Roles

exec SecurityModule.AddSystemRole
    @SystemRoleKey              = @SystemRoleRemAdministrator,
    @Name                       = 'REM Administrator', 
    @WellKnownName              = null,
    @Description                = 'Access to all system features.', 
    @SystemRoleType             = @JobFunctionRole,
    @SystemAccountKey           = 1

exec SecurityModule.AddSystemRole
    @SystemRoleKey              = @SystemRoleDemoUser,
    @Name                       = 'Demo User', 
    @WellKnownName              = null,
    @Description                = 'Access to all demo-only features.', 
    @SystemRoleType             = @JobFunctionRole,
    @SystemAccountKey           = 1
    
exec SecurityModule.AddSystemRole
    @SystemRoleKey              = @SystemRoleFrontDeskUser,
    @Name                       = 'Front Desk User', 
    @WellKnownName              = null,
    @Description                = 'Access to the front desk dashboard.', 
    @SystemRoleType             = @JobFunctionRole,
    @SystemAccountKey           = 1
    
exec SecurityModule.AddSystemRole
    @SystemRoleKey              = @SystemRoleClinicianUser,
    @Name                       = 'Clinician', 
    @WellKnownName              = null,
    @Description                = 'This role provides access to the clinician and patient workspaces.', 
    @SystemRoleType             = @JobFunctionRole,
    @SystemAccountKey           = 1

-- Task Roles
    
exec SecurityModule.AddSystemRole
    @SystemRoleKey              = @SystemRoleExecuteEmergencyAccess,
    @Name                       = 'Execute Emergency Access',     
    @WellKnownName              = 'executeemergencyaccess',
    @Description                = 'Execute Emergency access.', 
    @SystemRoleType             = @TaskRole,
    @SystemAccountKey           = 1
    
exec SecurityModule.AddSystemRole
    @SystemRoleKey              = @SystemRoleBasicUserInterfaceAccess,
    @Name                       = 'Basic Access', 
    @WellKnownName              = null,
    @Description                = 'The basic permissions that all users need in order to log into the system.', 
    @SystemRoleType             = @TaskRole,
    @SystemAccountKey           = 1

exec SecurityModule.AddSystemRole
    @SystemRoleKey              = @SystemRolePatientWorkspaceAccess,
    @Name                       = 'Patient Workspace Access', 
    @WellKnownName              = null,
    @Description                = 'Access to all features of the patient workspace including patient dashboard and patient editor.', 
    @SystemRoleType             = @TaskRole,
    @SystemAccountKey           = 1

exec SecurityModule.AddSystemRole
    @SystemRoleKey              = @SystemRolePatientFinderAccess,
    @Name                       = 'Patient Finder Access', 
    @WellKnownName              = null,
    @Description                = 'Access to the patient finder including advanced search.', 
    @SystemRoleType             = @TaskRole,
    @SystemAccountKey           = 1

exec SecurityModule.AddSystemRole
    @SystemRoleKey              = @SystemRoleClinicianDashboardAccess,
    @Name                       = 'Clinician Dashboard Access', 
    @WellKnownName              = null,
    @Description                = 'Access to the clinician dashboard.', 
    @SystemRoleType             = @TaskRole,
    @SystemAccountKey           = 1

exec SecurityModule.AddSystemRole
    @SystemRoleKey              = @SystemRoleFrontDeskDashboardAccess,
    @Name                       = 'Front Desk Dashboard Access', 
    @WellKnownName              = null,
    @Description                = 'Access to the front desk dashboard.', 
    @SystemRoleType             = @TaskRole,
    @SystemAccountKey           = 1

exec SecurityModule.AddSystemRole
    @SystemRoleKey              = @SystemRoleAdministrativeAccess,
    @Name                       = 'Administrative Access', 
    @WellKnownName              = null,
    @Description                = 'Access to all administrative screens including agency, location, and staff administrative screens.', 
    @SystemRoleType             = @TaskRole,
    @SystemAccountKey           = 1

exec SecurityModule.AddSystemRole
    @SystemRoleKey              = @SystemRoleReportAccess,
    @Name                       = 'Report Access', 
    @WellKnownName              = null,
    @Description                = 'Access to report workspace.', 
    @SystemRoleType             = @TaskRole,
    @SystemAccountKey           = 1

exec SecurityModule.AddSystemRole
    @SystemRoleKey              = @SystemRoleBillingAccess,
    @Name                       = 'Billing Access', 
    @WellKnownName              = null,
    @Description                = 'Access to billing workspace.', 
    @SystemRoleType             = @TaskRole,
    @SystemAccountKey           = 1

print '----------------------------------------------------------------------------------------------------'
print 'SystemRoleRelationship'
print '----------------------------------------------------------------------------------------------------'

-- Front Desk User is granted Basic User Interface Access Role, Patient Finder Role, and Front Desk Dashboard Access Role
exec SecurityModule.GrantRoleRelationship 11001, @SystemRoleBasicUserInterfaceAccess, @SystemRoleFrontDeskUser, 1
exec SecurityModule.GrantRoleRelationship 11002, @SystemRolePatientFinderAccess,      @SystemRoleFrontDeskUser, 1
exec SecurityModule.GrantRoleRelationship 11003, @SystemRoleFrontDeskDashboardAccess, @SystemRoleFrontDeskUser, 1

-- REM Administrator gets all roles
exec SecurityModule.GrantRoleRelationship 12001, @SystemRoleBasicUserInterfaceAccess, @SystemRoleRemAdministrator, 1
exec SecurityModule.GrantRoleRelationship 12002, @SystemRolePatientWorkspaceAccess,   @SystemRoleRemAdministrator, 1
exec SecurityModule.GrantRoleRelationship 12003, @SystemRolePatientFinderAccess,      @SystemRoleRemAdministrator, 1
exec SecurityModule.GrantRoleRelationship 12004, @SystemRoleClinicianDashboardAccess, @SystemRoleRemAdministrator, 1
exec SecurityModule.GrantRoleRelationship 12005, @SystemRoleFrontDeskDashboardAccess, @SystemRoleRemAdministrator, 1
exec SecurityModule.GrantRoleRelationship 12006, @SystemRoleAdministrativeAccess,     @SystemRoleRemAdministrator, 1
exec SecurityModule.GrantRoleRelationship 12007, @SystemRoleReportAccess,             @SystemRoleRemAdministrator, 1
exec SecurityModule.GrantRoleRelationship 12008, @SystemRoleBillingAccess,            @SystemRoleRemAdministrator, 1

-- Clinicians
-- Child Roles should be able to access the Patient Workspace granted 
exec SecurityModule.GrantRoleRelationship 13001, @SystemRoleBasicUserInterfaceAccess, @SystemRoleClinicianUser, 1
exec SecurityModule.GrantRoleRelationship 13002, @SystemRolePatientWorkspaceAccess,   @SystemRoleClinicianUser, 1 

print '----------------------------------------------------------------------------------------------------'
print 'SystemRolePermission'
print '----------------------------------------------------------------------------------------------------'

declare @PermissionToGrant          BIGINT
declare @SystemRolePermissionKey    BIGINT = 1000

print '------'
print 'Set Up Execute Emergency Access Permissions'
print '------'

set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'infrastructuremodule/executeemergencyaccess' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleExecuteEmergencyAccess, 1

print '------'
print 'Set Up Basic User Interface Access Permissions'
print '------'

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'infrastructuremodule/accessuserinterface' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleBasicUserInterfaceAccess, 1


print '------'
print 'Set Up Patient Workspace Access Permissions'
print '------'

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/cdsrulesview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/cdsrulesedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/clinicalcaseview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/clinicalcaseedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/patientview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/patientedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/patientexternalhistoryview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/patientexternalhistoryedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/problemview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/problemedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/visitview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/visitedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/densasiview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/densasiedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/tedsview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/tedsedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/gainshortscreenerview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/gainshortscreeneredit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/gprainterviewview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/gprainterviewedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/patientaccesshistoryview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/auditcview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/auditcedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/auditview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/auditedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/briefinterventionview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/briefinterventionedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/casesummaryview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/casesummaryedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/casealertsedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/dast10view' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/dast10edit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/immunizationview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/immunizationedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/individualcounselingview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/individualcounselingedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/labresultsview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/labresultsedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/medicationview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/medicationedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/nidaview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/nidaedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/patientdashboardview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/patientdashboardedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/phq9view' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/phq9edit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/radiologyordersview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/radiologyordersedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/scheduled&recentactivitiesview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/scheduled&recentactivitiesedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/socialhistoryview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/socialhistoryedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/vitalsignsview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/vitalsignsedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/patientlistview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/patientremindersview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/patientworkspaceview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/c32view' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/c32import' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1


set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/systemaccountview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/interoperabilityworkspaceview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'newcropmodule/newcropaccess' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'agencymodule/locationview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'agencymodule/staffview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientWorkspaceAccess, 1

print '------'
print 'Set Up Patient Finder Permissions'
print '------'

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/patientsearchview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRolePatientFinderAccess, 1

print '------'
print 'Set Up Clinician Dashboard Permissions'
print '------'

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/cliniciandashboardview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleClinicianDashboardAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/cliniciandashboardedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleClinicianDashboardAccess, 1

print '------'
print 'Set Up Front Desk Dashboard Permissions'
print '------'

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/frontdeskdashboardview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleFrontDeskDashboardAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/frontdeskdashboardedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleFrontDeskDashboardAccess, 1


print '------'
print 'Set Up Administrative Permissions'
print '------'

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'agencymodule/agencyview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleAdministrativeAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'agencymodule/agencyedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleAdministrativeAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'agencymodule/locationview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleAdministrativeAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'agencymodule/locationedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleAdministrativeAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'agencymodule/staffview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleAdministrativeAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'agencymodule/staffedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleAdministrativeAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'agencymodule/labresultview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleAdministrativeAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'agencymodule/labresultedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleAdministrativeAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'agencymodule/labresultupload' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleAdministrativeAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'agencymodule/meaningfuluseview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleAdministrativeAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'agencymodule/meaningfuluseedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleAdministrativeAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'agencymodule/logininformationview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleAdministrativeAccess, 1

print '------'
print 'Set Up Report Access Permissions'
print '------'

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'reportmodule/reportaccess' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleReportAccess, 1

print '------'
print 'Set Up Demo User Permissions'
print '------'

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/cliniciandashboardalertsview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleDemoUser, 1

set @SystemRolePermissionKey += 1

set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/cliniciandashboardlabresultsview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleDemoUser, 1

set @SystemRolePermissionKey += 1

set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/cliniciandashboardmedicationordersview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleDemoUser, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'patientmodule/casealertsview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleDemoUser, 1

print '------'
print 'Set Up Billing Access Permissions'
print '------'

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'billingmodule/billingworkspaceview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleBillingAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'billingmodule/billingofficeedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleBillingAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'billingmodule/billingofficeview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleBillingAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'billingmodule/payorview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleBillingAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'billingmodule/payoredit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleBillingAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'billingmodule/claimsview' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleBillingAccess, 1

set @SystemRolePermissionKey += 1
set @PermissionToGrant = SecurityModule.GetSystemPermissionKeyByWellKnownName ( 'billingmodule/claimsedit' )
exec SecurityModule.GrantPermissionToRole @SystemRolePermissionKey, @PermissionToGrant, @SystemRoleBillingAccess, 1

GO

