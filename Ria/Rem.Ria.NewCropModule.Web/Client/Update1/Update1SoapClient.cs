using Rem.Ria.NewCropModule.Web.Client.Common;

namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Update1SoapClient : System.ServiceModel.ClientBase<IUpdate1Soap>, IUpdate1Soap
    {
        
        public Update1SoapClient()
        {
        }
        
        public Update1SoapClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
        {
        }
        
        public Update1SoapClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public Update1SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public Update1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
        {
        }
        
        public Result GetDailyScriptReport(Credentials credentials, AccountRequest accountRequest, string reportDateCCYYMMDD, string includeSchema, string sortOrder)
        {
            return base.Channel.GetDailyScriptReport(credentials, accountRequest, reportDateCCYYMMDD, includeSchema, sortOrder);
        }
        
        public System.IAsyncResult BeginGetDailyScriptReport(Credentials credentials, AccountRequest accountRequest, string reportDateCCYYMMDD, string includeSchema, string sortOrder, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetDailyScriptReport(credentials, accountRequest, reportDateCCYYMMDD, includeSchema, sortOrder, callback, asyncState);
        }
        
        public Result EndGetDailyScriptReport(System.IAsyncResult result)
        {
            return base.Channel.EndGetDailyScriptReport(result);
        }
        
        public Result GetDailyScriptReportV2(Credentials credentials, AccountRequest accountRequest, string reportDateCCYYMMDD, int startHour, int endHour, string status, string transmissionType, string includeSchema)
        {
            return base.Channel.GetDailyScriptReportV2(credentials, accountRequest, reportDateCCYYMMDD, startHour, endHour, status, transmissionType, includeSchema);
        }
        
        public System.IAsyncResult BeginGetDailyScriptReportV2(Credentials credentials, AccountRequest accountRequest, string reportDateCCYYMMDD, int startHour, int endHour, string status, string transmissionType, string includeSchema, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetDailyScriptReportV2(credentials, accountRequest, reportDateCCYYMMDD, startHour, endHour, status, transmissionType, includeSchema, callback, asyncState);
        }
        
        public Result EndGetDailyScriptReportV2(System.IAsyncResult result)
        {
            return base.Channel.EndGetDailyScriptReportV2(result);
        }
        
        public Result GetDailyScriptReportV3(Credentials credentials, AccountRequest accountRequest, string reportDateCCYYMMDD, int startHour, int endHour, string status, string transmissionType, string prescriptionType, string prescriptionSubType, string includeSchema)
        {
            return base.Channel.GetDailyScriptReportV3(credentials, accountRequest, reportDateCCYYMMDD, startHour, endHour, status, transmissionType, prescriptionType, prescriptionSubType, includeSchema);
        }
        
        public System.IAsyncResult BeginGetDailyScriptReportV3(Credentials credentials, AccountRequest accountRequest, string reportDateCCYYMMDD, int startHour, int endHour, string status, string transmissionType, string prescriptionType, string prescriptionSubType, string includeSchema, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetDailyScriptReportV3(credentials, accountRequest, reportDateCCYYMMDD, startHour, endHour, status, transmissionType, prescriptionType, prescriptionSubType, includeSchema, callback, asyncState);
        }
        
        public Result EndGetDailyScriptReportV3(System.IAsyncResult result)
        {
            return base.Channel.EndGetDailyScriptReportV3(result);
        }
        
        public Result GetCompleteMedicationHistory(Credentials credentials, AccountRequest accountRequest, string reportDateStartCCYYMMDD, string reportDateEndCCYYMMDD, string includeSchema, string sortOrder)
        {
            return base.Channel.GetCompleteMedicationHistory(credentials, accountRequest, reportDateStartCCYYMMDD, reportDateEndCCYYMMDD, includeSchema, sortOrder);
        }
        
        public System.IAsyncResult BeginGetCompleteMedicationHistory(Credentials credentials, AccountRequest accountRequest, string reportDateStartCCYYMMDD, string reportDateEndCCYYMMDD, string includeSchema, string sortOrder, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetCompleteMedicationHistory(credentials, accountRequest, reportDateStartCCYYMMDD, reportDateEndCCYYMMDD, includeSchema, sortOrder, callback, asyncState);
        }
        
        public Result EndGetCompleteMedicationHistory(System.IAsyncResult result)
        {
            return base.Channel.EndGetCompleteMedicationHistory(result);
        }
        
        public PatientDrugDetailResult4 GetPatientFullMedicationHistory4(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester, string patientIdType)
        {
            return base.Channel.GetPatientFullMedicationHistory4(credentials, accountRequest, patientRequest, prescriptionHistoryRequest, patientInformationRequester, patientIdType);
        }
        
        public System.IAsyncResult BeginGetPatientFullMedicationHistory4(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester, string patientIdType, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetPatientFullMedicationHistory4(credentials, accountRequest, patientRequest, prescriptionHistoryRequest, patientInformationRequester, patientIdType, callback, asyncState);
        }
        
        public PatientDrugDetailResult4 EndGetPatientFullMedicationHistory4(System.IAsyncResult result)
        {
            return base.Channel.EndGetPatientFullMedicationHistory4(result);
        }
        
        public PatientDrugDetailResult5 GetPatientFullMedicationHistory5(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester, string patientIdType)
        {
            return base.Channel.GetPatientFullMedicationHistory5(credentials, accountRequest, patientRequest, prescriptionHistoryRequest, patientInformationRequester, patientIdType);
        }
        
        public System.IAsyncResult BeginGetPatientFullMedicationHistory5(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester, string patientIdType, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetPatientFullMedicationHistory5(credentials, accountRequest, patientRequest, prescriptionHistoryRequest, patientInformationRequester, patientIdType, callback, asyncState);
        }
        
        public PatientDrugDetailResult5 EndGetPatientFullMedicationHistory5(System.IAsyncResult result)
        {
            return base.Channel.EndGetPatientFullMedicationHistory5(result);
        }
        
        public Result GetPatientFullMedicationHistory6(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester, string patientIdType, string includeSchema)
        {
            return base.Channel.GetPatientFullMedicationHistory6(credentials, accountRequest, patientRequest, prescriptionHistoryRequest, patientInformationRequester, patientIdType, includeSchema);
        }
        
        public System.IAsyncResult BeginGetPatientFullMedicationHistory6(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester, string patientIdType, string includeSchema, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetPatientFullMedicationHistory6(credentials, accountRequest, patientRequest, prescriptionHistoryRequest, patientInformationRequester, patientIdType, includeSchema, callback, asyncState);
        }
        
        public Result EndGetPatientFullMedicationHistory6(System.IAsyncResult result)
        {
            return base.Channel.EndGetPatientFullMedicationHistory6(result);
        }
        
        public PatientAllergyExtendedDetailResult GetPatientAllergyHistory2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester)
        {
            return base.Channel.GetPatientAllergyHistory2(credentials, accountRequest, patientRequest, patientInformationRequester);
        }
        
        public System.IAsyncResult BeginGetPatientAllergyHistory2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetPatientAllergyHistory2(credentials, accountRequest, patientRequest, patientInformationRequester, callback, asyncState);
        }
        
        public PatientAllergyExtendedDetailResult EndGetPatientAllergyHistory2(System.IAsyncResult result)
        {
            return base.Channel.EndGetPatientAllergyHistory2(result);
        }
        
        public Result GetPatientAllergyHistoryV3(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester)
        {
            return base.Channel.GetPatientAllergyHistoryV3(credentials, accountRequest, patientRequest, patientInformationRequester);
        }
        
        public System.IAsyncResult BeginGetPatientAllergyHistoryV3(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetPatientAllergyHistoryV3(credentials, accountRequest, patientRequest, patientInformationRequester, callback, asyncState);
        }
        
        public Result EndGetPatientAllergyHistoryV3(System.IAsyncResult result)
        {
            return base.Channel.EndGetPatientAllergyHistoryV3(result);
        }
        
        public PatientFreeFormAllergyExtendedDetailResult GetPatientFreeFormAllergyHistory(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester)
        {
            return base.Channel.GetPatientFreeFormAllergyHistory(credentials, accountRequest, patientRequest, patientInformationRequester);
        }
        
        public System.IAsyncResult BeginGetPatientFreeFormAllergyHistory(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetPatientFreeFormAllergyHistory(credentials, accountRequest, patientRequest, patientInformationRequester, callback, asyncState);
        }
        
        public PatientFreeFormAllergyExtendedDetailResult EndGetPatientFreeFormAllergyHistory(System.IAsyncResult result)
        {
            return base.Channel.EndGetPatientFreeFormAllergyHistory(result);
        }
        
        public TransmissionSummaryResult GetPrescriptionTransmissionStatus(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string prescriptionIds)
        {
            return base.Channel.GetPrescriptionTransmissionStatus(credentials, accountRequest, patientRequest, patientInformationRequester, prescriptionIds);
        }
        
        public System.IAsyncResult BeginGetPrescriptionTransmissionStatus(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string prescriptionIds, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetPrescriptionTransmissionStatus(credentials, accountRequest, patientRequest, patientInformationRequester, prescriptionIds, callback, asyncState);
        }
        
        public TransmissionSummaryResult EndGetPrescriptionTransmissionStatus(System.IAsyncResult result)
        {
            return base.Channel.EndGetPrescriptionTransmissionStatus(result);
        }
        
        public TransmissionSummaryResult GetPrescriptionTransmissionStatusByPatient(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string queryMode, string status, string subStatus, string archiveStatus)
        {
            return base.Channel.GetPrescriptionTransmissionStatusByPatient(credentials, accountRequest, patientRequest, patientInformationRequester, queryMode, status, subStatus, archiveStatus);
        }
        
        public System.IAsyncResult BeginGetPrescriptionTransmissionStatusByPatient(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string queryMode, string status, string subStatus, string archiveStatus, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetPrescriptionTransmissionStatusByPatient(credentials, accountRequest, patientRequest, patientInformationRequester, queryMode, status, subStatus, archiveStatus, callback, asyncState);
        }
        
        public TransmissionSummaryResult EndGetPrescriptionTransmissionStatusByPatient(System.IAsyncResult result)
        {
            return base.Channel.EndGetPrescriptionTransmissionStatusByPatient(result);
        }
        
        public Result GenerateTestRenewalRequest(Credentials credentials, AccountRequest accountRequest, string xmlIn, bool createCurrentMedFromRxInfo, string originalPrescriptionFillDate)
        {
            return base.Channel.GenerateTestRenewalRequest(credentials, accountRequest, xmlIn, createCurrentMedFromRxInfo, originalPrescriptionFillDate);
        }
        
        public System.IAsyncResult BeginGenerateTestRenewalRequest(Credentials credentials, AccountRequest accountRequest, string xmlIn, bool createCurrentMedFromRxInfo, string originalPrescriptionFillDate, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGenerateTestRenewalRequest(credentials, accountRequest, xmlIn, createCurrentMedFromRxInfo, originalPrescriptionFillDate, callback, asyncState);
        }
        
        public Result EndGenerateTestRenewalRequest(System.IAsyncResult result)
        {
            return base.Channel.EndGenerateTestRenewalRequest(result);
        }
        
        public RenewalSummaryResult GetAllRenewalRequests(Credentials credentials, AccountRequest accountRequest, string locationId, string licensedPrescriberId)
        {
            return base.Channel.GetAllRenewalRequests(credentials, accountRequest, locationId, licensedPrescriberId);
        }
        
        public System.IAsyncResult BeginGetAllRenewalRequests(Credentials credentials, AccountRequest accountRequest, string locationId, string licensedPrescriberId, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetAllRenewalRequests(credentials, accountRequest, locationId, licensedPrescriberId, callback, asyncState);
        }
        
        public RenewalSummaryResult EndGetAllRenewalRequests(System.IAsyncResult result)
        {
            return base.Channel.EndGetAllRenewalRequests(result);
        }
        
        public RenewalSummaryResultV2 GetAllRenewalRequestsV2(Credentials credentials, AccountRequest accountRequest, string locationId, string licensedPrescriberId)
        {
            return base.Channel.GetAllRenewalRequestsV2(credentials, accountRequest, locationId, licensedPrescriberId);
        }
        
        public System.IAsyncResult BeginGetAllRenewalRequestsV2(Credentials credentials, AccountRequest accountRequest, string locationId, string licensedPrescriberId, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetAllRenewalRequestsV2(credentials, accountRequest, locationId, licensedPrescriberId, callback, asyncState);
        }
        
        public RenewalSummaryResultV2 EndGetAllRenewalRequestsV2(System.IAsyncResult result)
        {
            return base.Channel.EndGetAllRenewalRequestsV2(result);
        }
        
        public RenewalDetailResult GetRenewalRequestDetail(Credentials credentials, AccountRequest accountRequest, string renewalRequestIdentifier)
        {
            return base.Channel.GetRenewalRequestDetail(credentials, accountRequest, renewalRequestIdentifier);
        }
        
        public System.IAsyncResult BeginGetRenewalRequestDetail(Credentials credentials, AccountRequest accountRequest, string renewalRequestIdentifier, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetRenewalRequestDetail(credentials, accountRequest, renewalRequestIdentifier, callback, asyncState);
        }
        
        public RenewalDetailResult EndGetRenewalRequestDetail(System.IAsyncResult result)
        {
            return base.Channel.EndGetRenewalRequestDetail(result);
        }
        
        public Result ProcessRenewalRequest(Credentials credentials, AccountRequest accountRequest, string xmlIn)
        {
            return base.Channel.ProcessRenewalRequest(credentials, accountRequest, xmlIn);
        }
        
        public System.IAsyncResult BeginProcessRenewalRequest(Credentials credentials, AccountRequest accountRequest, string xmlIn, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginProcessRenewalRequest(credentials, accountRequest, xmlIn, callback, asyncState);
        }
        
        public Result EndProcessRenewalRequest(System.IAsyncResult result)
        {
            return base.Channel.EndProcessRenewalRequest(result);
        }
        
        public RenewalResponseDetailResult GetRenewalResponseTransmissionStatus(Credentials credentials, AccountRequest accountRequest, string renewalRequestIdentifier)
        {
            return base.Channel.GetRenewalResponseTransmissionStatus(credentials, accountRequest, renewalRequestIdentifier);
        }
        
        public System.IAsyncResult BeginGetRenewalResponseTransmissionStatus(Credentials credentials, AccountRequest accountRequest, string renewalRequestIdentifier, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetRenewalResponseTransmissionStatus(credentials, accountRequest, renewalRequestIdentifier, callback, asyncState);
        }
        
        public RenewalResponseDetailResult EndGetRenewalResponseTransmissionStatus(System.IAsyncResult result)
        {
            return base.Channel.EndGetRenewalResponseTransmissionStatus(result);
        }
        
        public Result GetRenewalResponseStatus(Credentials credentials, AccountRequest accountRequest, string renewalRequestIdentifier, string includeSchema)
        {
            return base.Channel.GetRenewalResponseStatus(credentials, accountRequest, renewalRequestIdentifier, includeSchema);
        }
        
        public System.IAsyncResult BeginGetRenewalResponseStatus(Credentials credentials, AccountRequest accountRequest, string renewalRequestIdentifier, string includeSchema, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetRenewalResponseStatus(credentials, accountRequest, renewalRequestIdentifier, includeSchema, callback, asyncState);
        }
        
        public Result EndGetRenewalResponseStatus(System.IAsyncResult result)
        {
            return base.Channel.EndGetRenewalResponseStatus(result);
        }
        
        public DrugFormularyDetailResult FormularyAlternativesWithDrugInfo2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string healthplanID, string healthplanTypeID, string eligibilityIndex, string drugConcept, string drugConceptType)
        {
            return base.Channel.FormularyAlternativesWithDrugInfo2(credentials, accountRequest, patientRequest, patientInformationRequester, healthplanID, healthplanTypeID, eligibilityIndex, drugConcept, drugConceptType);
        }
        
        public System.IAsyncResult BeginFormularyAlternativesWithDrugInfo2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string healthplanID, string healthplanTypeID, string eligibilityIndex, string drugConcept, string drugConceptType, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginFormularyAlternativesWithDrugInfo2(credentials, accountRequest, patientRequest, patientInformationRequester, healthplanID, healthplanTypeID, eligibilityIndex, drugConcept, drugConceptType, callback, asyncState);
        }
        
        public DrugFormularyDetailResult EndFormularyAlternativesWithDrugInfo2(System.IAsyncResult result)
        {
            return base.Channel.EndFormularyAlternativesWithDrugInfo2(result);
        }
        
        public Result SendMissingHealthplanInformation(Credentials credentials, AccountRequest accountRequest, string healthplanName, string healthplanId, string healthplanAddress1, string healthplanAddress2, string healthplanCity, string healthplanStateCode, string healthplanZip, string healthplanZip4, string healthplanPhoneNumber)
        {
            return base.Channel.SendMissingHealthplanInformation(credentials, accountRequest, healthplanName, healthplanId, healthplanAddress1, healthplanAddress2, healthplanCity, healthplanStateCode, healthplanZip, healthplanZip4, healthplanPhoneNumber);
        }
        
        public System.IAsyncResult BeginSendMissingHealthplanInformation(Credentials credentials, AccountRequest accountRequest, string healthplanName, string healthplanId, string healthplanAddress1, string healthplanAddress2, string healthplanCity, string healthplanStateCode, string healthplanZip, string healthplanZip4, string healthplanPhoneNumber, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginSendMissingHealthplanInformation(credentials, accountRequest, healthplanName, healthplanId, healthplanAddress1, healthplanAddress2, healthplanCity, healthplanStateCode, healthplanZip, healthplanZip4, healthplanPhoneNumber, callback, asyncState);
        }
        
        public Result EndSendMissingHealthplanInformation(System.IAsyncResult result)
        {
            return base.Channel.EndSendMissingHealthplanInformation(result);
        }
        
        public DrugHistoryDetailResult GetPBMDrugHistoryV2(Credentials credentials, AccountRequest accountRequest, string eligibilityTransactionId, int monthsPrior, string includeSchema)
        {
            return base.Channel.GetPBMDrugHistoryV2(credentials, accountRequest, eligibilityTransactionId, monthsPrior, includeSchema);
        }
        
        public System.IAsyncResult BeginGetPBMDrugHistoryV2(Credentials credentials, AccountRequest accountRequest, string eligibilityTransactionId, int monthsPrior, string includeSchema, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetPBMDrugHistoryV2(credentials, accountRequest, eligibilityTransactionId, monthsPrior, includeSchema, callback, asyncState);
        }
        
        public DrugHistoryDetailResult EndGetPBMDrugHistoryV2(System.IAsyncResult result)
        {
            return base.Channel.EndGetPBMDrugHistoryV2(result);
        }
        
        public DownloadDetailResult GetMostRecentDownloadUrl(Credentials credentials, AccountRequest accountRequest, int desiredData)
        {
            return base.Channel.GetMostRecentDownloadUrl(credentials, accountRequest, desiredData);
        }
        
        public System.IAsyncResult BeginGetMostRecentDownloadUrl(Credentials credentials, AccountRequest accountRequest, int desiredData, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetMostRecentDownloadUrl(credentials, accountRequest, desiredData, callback, asyncState);
        }
        
        public DownloadDetailResult EndGetMostRecentDownloadUrl(System.IAsyncResult result)
        {
            return base.Channel.EndGetMostRecentDownloadUrl(result);
        }
        
        public DrugDiseaseDetailResult DrugDiseaseInteraction(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string[] diseaseList, string diseaseCodeType, string[] proposedMedications, string drugStandardType)
        {
            return base.Channel.DrugDiseaseInteraction(credentials, accountRequest, patientRequest, patientInformationRequester, diseaseList, diseaseCodeType, proposedMedications, drugStandardType);
        }
        
        public System.IAsyncResult BeginDrugDiseaseInteraction(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string[] diseaseList, string diseaseCodeType, string[] proposedMedications, string drugStandardType, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginDrugDiseaseInteraction(credentials, accountRequest, patientRequest, patientInformationRequester, diseaseList, diseaseCodeType, proposedMedications, drugStandardType, callback, asyncState);
        }
        
        public DrugDiseaseDetailResult EndDrugDiseaseInteraction(System.IAsyncResult result)
        {
            return base.Channel.EndDrugDiseaseInteraction(result);
        }
        
        public PharmacyDetailResultV2 PharmacySearchV3(Credentials credentials, AccountRequest accountRequest, string postalCode, string phoneNumber, string streetName, string pharmacyName, string city, string state, string ncpdpID, string healthplanID, string healthplanTypeID, string eligibilityIndex)
        {
            return base.Channel.PharmacySearchV3(credentials, accountRequest, postalCode, phoneNumber, streetName, pharmacyName, city, state, ncpdpID, healthplanID, healthplanTypeID, eligibilityIndex);
        }
        
        public System.IAsyncResult BeginPharmacySearchV3(Credentials credentials, AccountRequest accountRequest, string postalCode, string phoneNumber, string streetName, string pharmacyName, string city, string state, string ncpdpID, string healthplanID, string healthplanTypeID, string eligibilityIndex, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginPharmacySearchV3(credentials, accountRequest, postalCode, phoneNumber, streetName, pharmacyName, city, state, ncpdpID, healthplanID, healthplanTypeID, eligibilityIndex, callback, asyncState);
        }
        
        public PharmacyDetailResultV2 EndPharmacySearchV3(System.IAsyncResult result)
        {
            return base.Channel.EndPharmacySearchV3(result);
        }
        
        public DrugFormularyFavoriteDetailResult DrugSearchWithFormularyWithFavoritesV2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string healthplanID, string healthplanTypeID, string eligibilityIndex, string drugName, string includeObsolete, string searchBrandGeneric, string searchRxOTC, string searchDrugSupply, string locationId, string providerId)
        {
            return base.Channel.DrugSearchWithFormularyWithFavoritesV2(credentials, accountRequest, patientRequest, patientInformationRequester, healthplanID, healthplanTypeID, eligibilityIndex, drugName, includeObsolete, searchBrandGeneric, searchRxOTC, searchDrugSupply, locationId, providerId);
        }
        
        public System.IAsyncResult BeginDrugSearchWithFormularyWithFavoritesV2(
            Credentials credentials, 
            AccountRequest accountRequest, 
            PatientRequest patientRequest, 
            PatientInformationRequester patientInformationRequester, 
            string healthplanID, 
            string healthplanTypeID, 
            string eligibilityIndex, 
            string drugName, 
            string includeObsolete, 
            string searchBrandGeneric, 
            string searchRxOTC, 
            string searchDrugSupply, 
            string locationId, 
            string providerId, 
            System.AsyncCallback callback, 
            object asyncState)
        {
            return base.Channel.BeginDrugSearchWithFormularyWithFavoritesV2(credentials, accountRequest, patientRequest, patientInformationRequester, healthplanID, healthplanTypeID, eligibilityIndex, drugName, includeObsolete, searchBrandGeneric, searchRxOTC, searchDrugSupply, locationId, providerId, callback, asyncState);
        }
        
        public DrugFormularyFavoriteDetailResult EndDrugSearchWithFormularyWithFavoritesV2(System.IAsyncResult result)
        {
            return base.Channel.EndDrugSearchWithFormularyWithFavoritesV2(result);
        }
        
        public Result DrugSearchWithFormularyWithFavoritesV3(
            Credentials credentials, 
            AccountRequest accountRequest, 
            PatientRequest patientRequest, 
            PatientInformationRequester patientInformationRequester, 
            string healthplanId, 
            string healthplanTypeId, 
            string eligibilityIndex, 
            string drugName, 
            string drugTypeId, 
            string includeObsolete, 
            string searchBrandGeneric, 
            string searchRxOTC, 
            string searchDrugSupply, 
            string locationId, 
            string providerId, 
            string includeCopay, 
            string includeSchema)
        {
            return base.Channel.DrugSearchWithFormularyWithFavoritesV3(credentials, accountRequest, patientRequest, patientInformationRequester, healthplanId, healthplanTypeId, eligibilityIndex, drugName, drugTypeId, includeObsolete, searchBrandGeneric, searchRxOTC, searchDrugSupply, locationId, providerId, includeCopay, includeSchema);
        }
        
        public System.IAsyncResult BeginDrugSearchWithFormularyWithFavoritesV3(
            Credentials credentials, 
            AccountRequest accountRequest, 
            PatientRequest patientRequest, 
            PatientInformationRequester patientInformationRequester, 
            string healthplanId, 
            string healthplanTypeId, 
            string eligibilityIndex, 
            string drugName, 
            string drugTypeId, 
            string includeObsolete, 
            string searchBrandGeneric, 
            string searchRxOTC, 
            string searchDrugSupply, 
            string locationId, 
            string providerId, 
            string includeCopay, 
            string includeSchema, 
            System.AsyncCallback callback, 
            object asyncState)
        {
            return base.Channel.BeginDrugSearchWithFormularyWithFavoritesV3(credentials, accountRequest, patientRequest, patientInformationRequester, healthplanId, healthplanTypeId, eligibilityIndex, drugName, drugTypeId, includeObsolete, searchBrandGeneric, searchRxOTC, searchDrugSupply, locationId, providerId, includeCopay, includeSchema, callback, asyncState);
        }
        
        public Result EndDrugSearchWithFormularyWithFavoritesV3(System.IAsyncResult result)
        {
            return base.Channel.EndDrugSearchWithFormularyWithFavoritesV3(result);
        }
        
        public HealthplanDetailResult HealthplanSearchV2(Credentials credentials, AccountRequest accountRequest, string healthplan, string state, string searchType, string resultOrder)
        {
            return base.Channel.HealthplanSearchV2(credentials, accountRequest, healthplan, state, searchType, resultOrder);
        }
        
        public System.IAsyncResult BeginHealthplanSearchV2(Credentials credentials, AccountRequest accountRequest, string healthplan, string state, string searchType, string resultOrder, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginHealthplanSearchV2(credentials, accountRequest, healthplan, state, searchType, resultOrder, callback, asyncState);
        }
        
        public HealthplanDetailResult EndHealthplanSearchV2(System.IAsyncResult result)
        {
            return base.Channel.EndHealthplanSearchV2(result);
        }
        
        public FormularyCoverageDetailResultV3 FormularyCoverageV3(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string healthplanID, string healthplanTypeID, string eligibilityIndex, string[] drugConcept, string drugConceptType)
        {
            return base.Channel.FormularyCoverageV3(credentials, accountRequest, patientRequest, patientInformationRequester, healthplanID, healthplanTypeID, eligibilityIndex, drugConcept, drugConceptType);
        }
        
        public System.IAsyncResult BeginFormularyCoverageV3(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string healthplanID, string healthplanTypeID, string eligibilityIndex, string[] drugConcept, string drugConceptType, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginFormularyCoverageV3(credentials, accountRequest, patientRequest, patientInformationRequester, healthplanID, healthplanTypeID, eligibilityIndex, drugConcept, drugConceptType, callback, asyncState);
        }
        
        public FormularyCoverageDetailResultV3 EndFormularyCoverageV3(System.IAsyncResult result)
        {
            return base.Channel.EndFormularyCoverageV3(result);
        }
        
        public Result ReportPrescribingCount(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string reportType, string reportStartDate, string reportEndDate, string prescriptionType, string prescriptionCount)
        {
            return base.Channel.ReportPrescribingCount(credentials, accountRequest, patientInformationRequester, reportType, reportStartDate, reportEndDate, prescriptionType, prescriptionCount);
        }
        
        public System.IAsyncResult BeginReportPrescribingCount(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string reportType, string reportStartDate, string reportEndDate, string prescriptionType, string prescriptionCount, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginReportPrescribingCount(credentials, accountRequest, patientInformationRequester, reportType, reportStartDate, reportEndDate, prescriptionType, prescriptionCount, callback, asyncState);
        }
        
        public Result EndReportPrescribingCount(System.IAsyncResult result)
        {
            return base.Channel.EndReportPrescribingCount(result);
        }
        
        public CounselingMessageDetailResult GetCounselingMessages(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string drugConcept, string drugStandardType)
        {
            return base.Channel.GetCounselingMessages(credentials, accountRequest, patientInformationRequester, drugConcept, drugStandardType);
        }
        
        public System.IAsyncResult BeginGetCounselingMessages(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string drugConcept, string drugStandardType, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetCounselingMessages(credentials, accountRequest, patientInformationRequester, drugConcept, drugStandardType, callback, asyncState);
        }
        
        public CounselingMessageDetailResult EndGetCounselingMessages(System.IAsyncResult result)
        {
            return base.Channel.EndGetCounselingMessages(result);
        }
        
        public Result GetSideEffects(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string drugConcept, string drugStandardType, string diseaseDescriptionType, string includeSchema, string sortOrder)
        {
            return base.Channel.GetSideEffects(credentials, accountRequest, patientRequest, patientInformationRequester, drugConcept, drugStandardType, diseaseDescriptionType, includeSchema, sortOrder);
        }
        
        public System.IAsyncResult BeginGetSideEffects(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string drugConcept, string drugStandardType, string diseaseDescriptionType, string includeSchema, string sortOrder, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetSideEffects(credentials, accountRequest, patientRequest, patientInformationRequester, drugConcept, drugStandardType, diseaseDescriptionType, includeSchema, sortOrder, callback, asyncState);
        }
        
        public Result EndGetSideEffects(System.IAsyncResult result)
        {
            return base.Channel.EndGetSideEffects(result);
        }
        
        public Result GetAccountStatusDetail(Credentials credentials, AccountRequest accountRequest, string locationId, string licensedPrescriberId, string statusSectionType, string includeSchema, string sortOrder)
        {
            return base.Channel.GetAccountStatusDetail(credentials, accountRequest, locationId, licensedPrescriberId, statusSectionType, includeSchema, sortOrder);
        }
        
        public System.IAsyncResult BeginGetAccountStatusDetail(Credentials credentials, AccountRequest accountRequest, string locationId, string licensedPrescriberId, string statusSectionType, string includeSchema, string sortOrder, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetAccountStatusDetail(credentials, accountRequest, locationId, licensedPrescriberId, statusSectionType, includeSchema, sortOrder, callback, asyncState);
        }
        
        public Result EndGetAccountStatusDetail(System.IAsyncResult result)
        {
            return base.Channel.EndGetAccountStatusDetail(result);
        }
        
        public EligibilityDetailResult GetPBMEligibilityV2(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string xmlIn, string includeSchema)
        {
            return base.Channel.GetPBMEligibilityV2(credentials, accountRequest, patientInformationRequester, xmlIn, includeSchema);
        }
        
        public System.IAsyncResult BeginGetPBMEligibilityV2(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string xmlIn, string includeSchema, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetPBMEligibilityV2(credentials, accountRequest, patientInformationRequester, xmlIn, includeSchema, callback, asyncState);
        }
        
        public EligibilityDetailResult EndGetPBMEligibilityV2(System.IAsyncResult result)
        {
            return base.Channel.EndGetPBMEligibilityV2(result);
        }
        
        public EligibilityDetailResultV3 GetPBMEligibilityV3(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string xmlIn, string includeSchema)
        {
            return base.Channel.GetPBMEligibilityV3(credentials, accountRequest, patientInformationRequester, xmlIn, includeSchema);
        }
        
        public System.IAsyncResult BeginGetPBMEligibilityV3(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string xmlIn, string includeSchema, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetPBMEligibilityV3(credentials, accountRequest, patientInformationRequester, xmlIn, includeSchema, callback, asyncState);
        }
        
        public EligibilityDetailResultV3 EndGetPBMEligibilityV3(System.IAsyncResult result)
        {
            return base.Channel.EndGetPBMEligibilityV3(result);
        }
        
        public TransmissionSummaryResult GetPrescriptionTransmissionStatusV2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, PrescriptionTransmissionQueryType queryType, string queryId)
        {
            return base.Channel.GetPrescriptionTransmissionStatusV2(credentials, accountRequest, patientRequest, patientInformationRequester, queryType, queryId);
        }
        
        public System.IAsyncResult BeginGetPrescriptionTransmissionStatusV2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, PrescriptionTransmissionQueryType queryType, string queryId, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetPrescriptionTransmissionStatusV2(credentials, accountRequest, patientRequest, patientInformationRequester, queryType, queryId, callback, asyncState);
        }
        
        public TransmissionSummaryResult EndGetPrescriptionTransmissionStatusV2(System.IAsyncResult result)
        {
            return base.Channel.EndGetPrescriptionTransmissionStatusV2(result);
        }
        
        public MessageTransactionStatusResult GetSubmittedMessageTransactionStatus(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string messageTransactionId, string messageTransactionSource)
        {
            return base.Channel.GetSubmittedMessageTransactionStatus(credentials, accountRequest, patientRequest, patientInformationRequester, messageTransactionId, messageTransactionSource);
        }
        
        public System.IAsyncResult BeginGetSubmittedMessageTransactionStatus(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string messageTransactionId, string messageTransactionSource, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetSubmittedMessageTransactionStatus(credentials, accountRequest, patientRequest, patientInformationRequester, messageTransactionId, messageTransactionSource, callback, asyncState);
        }
        
        public MessageTransactionStatusResult EndGetSubmittedMessageTransactionStatus(System.IAsyncResult result)
        {
            return base.Channel.EndGetSubmittedMessageTransactionStatus(result);
        }
        
        public DrugAllergyDetailResultV2 DrugAllergyInteractionV2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string[] allergies, string[] proposedMedications, string drugStandardType)
        {
            return base.Channel.DrugAllergyInteractionV2(credentials, accountRequest, patientRequest, patientInformationRequester, allergies, proposedMedications, drugStandardType);
        }
        
        public System.IAsyncResult BeginDrugAllergyInteractionV2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string[] allergies, string[] proposedMedications, string drugStandardType, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginDrugAllergyInteractionV2(credentials, accountRequest, patientRequest, patientInformationRequester, allergies, proposedMedications, drugStandardType, callback, asyncState);
        }
        
        public DrugAllergyDetailResultV2 EndDrugAllergyInteractionV2(System.IAsyncResult result)
        {
            return base.Channel.EndDrugAllergyInteractionV2(result);
        }
        
        public Result ResolveFailedPrescriptionTransmission(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string prescriptionType, string transactionId)
        {
            return base.Channel.ResolveFailedPrescriptionTransmission(credentials, accountRequest, patientInformationRequester, prescriptionType, transactionId);
        }
        
        public System.IAsyncResult BeginResolveFailedPrescriptionTransmission(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string prescriptionType, string transactionId, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginResolveFailedPrescriptionTransmission(credentials, accountRequest, patientInformationRequester, prescriptionType, transactionId, callback, asyncState);
        }
        
        public Result EndResolveFailedPrescriptionTransmission(System.IAsyncResult result)
        {
            return base.Channel.EndResolveFailedPrescriptionTransmission(result);
        }
        
        public Result GetMeaningfulUsePatientEncounterInfo(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string encounterId, string includeSchema)
        {
            return base.Channel.GetMeaningfulUsePatientEncounterInfo(credentials, accountRequest, patientRequest, patientInformationRequester, encounterId, includeSchema);
        }
        
        public System.IAsyncResult BeginGetMeaningfulUsePatientEncounterInfo(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string encounterId, string includeSchema, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetMeaningfulUsePatientEncounterInfo(credentials, accountRequest, patientRequest, patientInformationRequester, encounterId, includeSchema, callback, asyncState);
        }
        
        public Result EndGetMeaningfulUsePatientEncounterInfo(System.IAsyncResult result)
        {
            return base.Channel.EndGetMeaningfulUsePatientEncounterInfo(result);
        }
        
        public Result DoseCheck(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string drugId, string drugTypeId, string birthDateCCYYMMDD, string gender, string diagnosis, string doseType, string dose, string doseUOM, string weightInKg, string includeSchema)
        {
            return base.Channel.DoseCheck(credentials, accountRequest, patientRequest, patientInformationRequester, drugId, drugTypeId, birthDateCCYYMMDD, gender, diagnosis, doseType, dose, doseUOM, weightInKg, includeSchema);
        }
        
        public System.IAsyncResult BeginDoseCheck(
            Credentials credentials, 
            AccountRequest accountRequest, 
            PatientRequest patientRequest, 
            PatientInformationRequester patientInformationRequester, 
            string drugId, 
            string drugTypeId, 
            string birthDateCCYYMMDD, 
            string gender, 
            string diagnosis, 
            string doseType, 
            string dose, 
            string doseUOM, 
            string weightInKg, 
            string includeSchema, 
            System.AsyncCallback callback, 
            object asyncState)
        {
            return base.Channel.BeginDoseCheck(credentials, accountRequest, patientRequest, patientInformationRequester, drugId, drugTypeId, birthDateCCYYMMDD, gender, diagnosis, doseType, dose, doseUOM, weightInKg, includeSchema, callback, asyncState);
        }
        
        public Result EndDoseCheck(System.IAsyncResult result)
        {
            return base.Channel.EndDoseCheck(result);
        }
    }
}