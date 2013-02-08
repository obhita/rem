using Rem.Ria.NewCropModule.Web.Client.Common;

namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", ConfigurationName="IUpdate1Soap")]
    public interface IUpdate1Soap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetDailyScriptRepo" +
                                                               "rt", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetDailyScriptRepo" +
                                                                                 "rtResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result GetDailyScriptReport(Credentials credentials, AccountRequest accountRequest, string reportDateCCYYMMDD, string includeSchema, string sortOrder);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetDailyScriptRepo" +
                                                                                  "rt", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetDailyScriptRepo" +
                                                                                                    "rtResponse")]
        System.IAsyncResult BeginGetDailyScriptReport(Credentials credentials, AccountRequest accountRequest, string reportDateCCYYMMDD, string includeSchema, string sortOrder, System.AsyncCallback callback, object asyncState);
        
        Result EndGetDailyScriptReport(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetDailyScriptRepo" +
                                                               "rtV2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetDailyScriptRepo" +
                                                                                   "rtV2Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result GetDailyScriptReportV2(Credentials credentials, AccountRequest accountRequest, string reportDateCCYYMMDD, int startHour, int endHour, string status, string transmissionType, string includeSchema);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetDailyScriptRepo" +
                                                                                  "rtV2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetDailyScriptRepo" +
                                                                                                      "rtV2Response")]
        System.IAsyncResult BeginGetDailyScriptReportV2(Credentials credentials, AccountRequest accountRequest, string reportDateCCYYMMDD, int startHour, int endHour, string status, string transmissionType, string includeSchema, System.AsyncCallback callback, object asyncState);
        
        Result EndGetDailyScriptReportV2(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetDailyScriptRepo" +
                                                               "rtV3", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetDailyScriptRepo" +
                                                                                   "rtV3Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result GetDailyScriptReportV3(Credentials credentials, AccountRequest accountRequest, string reportDateCCYYMMDD, int startHour, int endHour, string status, string transmissionType, string prescriptionType, string prescriptionSubType, string includeSchema);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetDailyScriptRepo" +
                                                                                  "rtV3", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetDailyScriptRepo" +
                                                                                                      "rtV3Response")]
        System.IAsyncResult BeginGetDailyScriptReportV3(Credentials credentials, AccountRequest accountRequest, string reportDateCCYYMMDD, int startHour, int endHour, string status, string transmissionType, string prescriptionType, string prescriptionSubType, string includeSchema, System.AsyncCallback callback, object asyncState);
        
        Result EndGetDailyScriptReportV3(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetCompleteMedicat" +
                                                               "ionHistory", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetCompleteMedicat" +
                                                                                         "ionHistoryResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result GetCompleteMedicationHistory(Credentials credentials, AccountRequest accountRequest, string reportDateStartCCYYMMDD, string reportDateEndCCYYMMDD, string includeSchema, string sortOrder);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetCompleteMedicat" +
                                                                                  "ionHistory", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetCompleteMedicat" +
                                                                                                            "ionHistoryResponse")]
        System.IAsyncResult BeginGetCompleteMedicationHistory(Credentials credentials, AccountRequest accountRequest, string reportDateStartCCYYMMDD, string reportDateEndCCYYMMDD, string includeSchema, string sortOrder, System.AsyncCallback callback, object asyncState);
        
        Result EndGetCompleteMedicationHistory(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFullMedi" +
                                                               "cationHistory4", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFullMedi" +
                                                                                             "cationHistory4Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        PatientDrugDetailResult4 GetPatientFullMedicationHistory4(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester, string patientIdType);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFullMedi" +
                                                                                  "cationHistory4", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFullMedi" +
                                                                                                                "cationHistory4Response")]
        System.IAsyncResult BeginGetPatientFullMedicationHistory4(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester, string patientIdType, System.AsyncCallback callback, object asyncState);
        
        PatientDrugDetailResult4 EndGetPatientFullMedicationHistory4(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFullMedi" +
                                                               "cationHistory5", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFullMedi" +
                                                                                             "cationHistory5Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        PatientDrugDetailResult5 GetPatientFullMedicationHistory5(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester, string patientIdType);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFullMedi" +
                                                                                  "cationHistory5", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFullMedi" +
                                                                                                                "cationHistory5Response")]
        System.IAsyncResult BeginGetPatientFullMedicationHistory5(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester, string patientIdType, System.AsyncCallback callback, object asyncState);
        
        PatientDrugDetailResult5 EndGetPatientFullMedicationHistory5(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFullMedi" +
                                                               "cationHistory6", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFullMedi" +
                                                                                             "cationHistory6Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result GetPatientFullMedicationHistory6(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester, string patientIdType, string includeSchema);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFullMedi" +
                                                                                  "cationHistory6", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFullMedi" +
                                                                                                                "cationHistory6Response")]
        System.IAsyncResult BeginGetPatientFullMedicationHistory6(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester, string patientIdType, string includeSchema, System.AsyncCallback callback, object asyncState);
        
        Result EndGetPatientFullMedicationHistory6(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientAllergyH" +
                                                               "istory2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientAllergyH" +
                                                                                      "istory2Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        PatientAllergyExtendedDetailResult GetPatientAllergyHistory2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientAllergyH" +
                                                                                  "istory2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientAllergyH" +
                                                                                                         "istory2Response")]
        System.IAsyncResult BeginGetPatientAllergyHistory2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, System.AsyncCallback callback, object asyncState);
        
        PatientAllergyExtendedDetailResult EndGetPatientAllergyHistory2(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientAllergyH" +
                                                               "istoryV3", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientAllergyH" +
                                                                                       "istoryV3Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result GetPatientAllergyHistoryV3(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientAllergyH" +
                                                                                  "istoryV3", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientAllergyH" +
                                                                                                          "istoryV3Response")]
        System.IAsyncResult BeginGetPatientAllergyHistoryV3(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, System.AsyncCallback callback, object asyncState);
        
        Result EndGetPatientAllergyHistoryV3(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFreeForm" +
                                                               "AllergyHistory", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFreeForm" +
                                                                                             "AllergyHistoryResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        PatientFreeFormAllergyExtendedDetailResult GetPatientFreeFormAllergyHistory(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFreeForm" +
                                                                                  "AllergyHistory", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPatientFreeForm" +
                                                                                                                "AllergyHistoryResponse")]
        System.IAsyncResult BeginGetPatientFreeFormAllergyHistory(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, System.AsyncCallback callback, object asyncState);
        
        PatientFreeFormAllergyExtendedDetailResult EndGetPatientFreeFormAllergyHistory(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPrescriptionTra" +
                                                               "nsmissionStatus", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPrescriptionTra" +
                                                                                              "nsmissionStatusResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        TransmissionSummaryResult GetPrescriptionTransmissionStatus(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string prescriptionIds);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPrescriptionTra" +
                                                                                  "nsmissionStatus", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPrescriptionTra" +
                                                                                                                 "nsmissionStatusResponse")]
        System.IAsyncResult BeginGetPrescriptionTransmissionStatus(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string prescriptionIds, System.AsyncCallback callback, object asyncState);
        
        TransmissionSummaryResult EndGetPrescriptionTransmissionStatus(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPrescriptionTra" +
                                                               "nsmissionStatusByPatient", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPrescriptionTra" +
                                                                                                       "nsmissionStatusByPatientResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        TransmissionSummaryResult GetPrescriptionTransmissionStatusByPatient(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string queryMode, string status, string subStatus, string archiveStatus);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPrescriptionTra" +
                                                                                  "nsmissionStatusByPatient", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPrescriptionTra" +
                                                                                                                          "nsmissionStatusByPatientResponse")]
        System.IAsyncResult BeginGetPrescriptionTransmissionStatusByPatient(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string queryMode, string status, string subStatus, string archiveStatus, System.AsyncCallback callback, object asyncState);
        
        TransmissionSummaryResult EndGetPrescriptionTransmissionStatusByPatient(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GenerateTestRenewa" +
                                                               "lRequest", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GenerateTestRenewa" +
                                                                                       "lRequestResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result GenerateTestRenewalRequest(Credentials credentials, AccountRequest accountRequest, string xmlIn, bool createCurrentMedFromRxInfo, string originalPrescriptionFillDate);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GenerateTestRenewa" +
                                                                                  "lRequest", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GenerateTestRenewa" +
                                                                                                          "lRequestResponse")]
        System.IAsyncResult BeginGenerateTestRenewalRequest(Credentials credentials, AccountRequest accountRequest, string xmlIn, bool createCurrentMedFromRxInfo, string originalPrescriptionFillDate, System.AsyncCallback callback, object asyncState);
        
        Result EndGenerateTestRenewalRequest(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetAllRenewalReque" +
                                                               "sts", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetAllRenewalReque" +
                                                                                  "stsResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        RenewalSummaryResult GetAllRenewalRequests(Credentials credentials, AccountRequest accountRequest, string locationId, string licensedPrescriberId);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetAllRenewalReque" +
                                                                                  "sts", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetAllRenewalReque" +
                                                                                                     "stsResponse")]
        System.IAsyncResult BeginGetAllRenewalRequests(Credentials credentials, AccountRequest accountRequest, string locationId, string licensedPrescriberId, System.AsyncCallback callback, object asyncState);
        
        RenewalSummaryResult EndGetAllRenewalRequests(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetAllRenewalReque" +
                                                               "stsV2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetAllRenewalReque" +
                                                                                    "stsV2Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        RenewalSummaryResultV2 GetAllRenewalRequestsV2(Credentials credentials, AccountRequest accountRequest, string locationId, string licensedPrescriberId);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetAllRenewalReque" +
                                                                                  "stsV2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetAllRenewalReque" +
                                                                                                       "stsV2Response")]
        System.IAsyncResult BeginGetAllRenewalRequestsV2(Credentials credentials, AccountRequest accountRequest, string locationId, string licensedPrescriberId, System.AsyncCallback callback, object asyncState);
        
        RenewalSummaryResultV2 EndGetAllRenewalRequestsV2(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetRenewalRequestD" +
                                                               "etail", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetRenewalRequestD" +
                                                                                    "etailResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        RenewalDetailResult GetRenewalRequestDetail(Credentials credentials, AccountRequest accountRequest, string renewalRequestIdentifier);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetRenewalRequestD" +
                                                                                  "etail", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetRenewalRequestD" +
                                                                                                       "etailResponse")]
        System.IAsyncResult BeginGetRenewalRequestDetail(Credentials credentials, AccountRequest accountRequest, string renewalRequestIdentifier, System.AsyncCallback callback, object asyncState);
        
        RenewalDetailResult EndGetRenewalRequestDetail(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/ProcessRenewalRequ" +
                                                               "est", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/ProcessRenewalRequ" +
                                                                                  "estResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result ProcessRenewalRequest(Credentials credentials, AccountRequest accountRequest, string xmlIn);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/ProcessRenewalRequ" +
                                                                                  "est", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/ProcessRenewalRequ" +
                                                                                                     "estResponse")]
        System.IAsyncResult BeginProcessRenewalRequest(Credentials credentials, AccountRequest accountRequest, string xmlIn, System.AsyncCallback callback, object asyncState);
        
        Result EndProcessRenewalRequest(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetRenewalResponse" +
                                                               "TransmissionStatus", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetRenewalResponse" +
                                                                                                 "TransmissionStatusResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        RenewalResponseDetailResult GetRenewalResponseTransmissionStatus(Credentials credentials, AccountRequest accountRequest, string renewalRequestIdentifier);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetRenewalResponse" +
                                                                                  "TransmissionStatus", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetRenewalResponse" +
                                                                                                                    "TransmissionStatusResponse")]
        System.IAsyncResult BeginGetRenewalResponseTransmissionStatus(Credentials credentials, AccountRequest accountRequest, string renewalRequestIdentifier, System.AsyncCallback callback, object asyncState);
        
        RenewalResponseDetailResult EndGetRenewalResponseTransmissionStatus(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetRenewalResponse" +
                                                               "Status", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetRenewalResponse" +
                                                                                     "StatusResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result GetRenewalResponseStatus(Credentials credentials, AccountRequest accountRequest, string renewalRequestIdentifier, string includeSchema);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetRenewalResponse" +
                                                                                  "Status", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetRenewalResponse" +
                                                                                                        "StatusResponse")]
        System.IAsyncResult BeginGetRenewalResponseStatus(Credentials credentials, AccountRequest accountRequest, string renewalRequestIdentifier, string includeSchema, System.AsyncCallback callback, object asyncState);
        
        Result EndGetRenewalResponseStatus(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/FormularyAlternati" +
                                                               "vesWithDrugInfo2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/FormularyAlternati" +
                                                                                               "vesWithDrugInfo2Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        DrugFormularyDetailResult FormularyAlternativesWithDrugInfo2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string healthplanID, string healthplanTypeID, string eligibilityIndex, string drugConcept, string drugConceptType);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/FormularyAlternati" +
                                                                                  "vesWithDrugInfo2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/FormularyAlternati" +
                                                                                                                  "vesWithDrugInfo2Response")]
        System.IAsyncResult BeginFormularyAlternativesWithDrugInfo2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string healthplanID, string healthplanTypeID, string eligibilityIndex, string drugConcept, string drugConceptType, System.AsyncCallback callback, object asyncState);
        
        DrugFormularyDetailResult EndFormularyAlternativesWithDrugInfo2(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/SendMissingHealthp" +
                                                               "lanInformation", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/SendMissingHealthp" +
                                                                                             "lanInformationResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result SendMissingHealthplanInformation(Credentials credentials, AccountRequest accountRequest, string healthplanName, string healthplanId, string healthplanAddress1, string healthplanAddress2, string healthplanCity, string healthplanStateCode, string healthplanZip, string healthplanZip4, string healthplanPhoneNumber);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/SendMissingHealthp" +
                                                                                  "lanInformation", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/SendMissingHealthp" +
                                                                                                                "lanInformationResponse")]
        System.IAsyncResult BeginSendMissingHealthplanInformation(Credentials credentials, AccountRequest accountRequest, string healthplanName, string healthplanId, string healthplanAddress1, string healthplanAddress2, string healthplanCity, string healthplanStateCode, string healthplanZip, string healthplanZip4, string healthplanPhoneNumber, System.AsyncCallback callback, object asyncState);
        
        Result EndSendMissingHealthplanInformation(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPBMDrugHistoryV" +
                                                               "2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPBMDrugHistoryV" +
                                                                                "2Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        DrugHistoryDetailResult GetPBMDrugHistoryV2(Credentials credentials, AccountRequest accountRequest, string eligibilityTransactionId, int monthsPrior, string includeSchema);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPBMDrugHistoryV" +
                                                                                  "2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPBMDrugHistoryV" +
                                                                                                   "2Response")]
        System.IAsyncResult BeginGetPBMDrugHistoryV2(Credentials credentials, AccountRequest accountRequest, string eligibilityTransactionId, int monthsPrior, string includeSchema, System.AsyncCallback callback, object asyncState);
        
        DrugHistoryDetailResult EndGetPBMDrugHistoryV2(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetMostRecentDownl" +
                                                               "oadUrl", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetMostRecentDownl" +
                                                                                     "oadUrlResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        DownloadDetailResult GetMostRecentDownloadUrl(Credentials credentials, AccountRequest accountRequest, int desiredData);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetMostRecentDownl" +
                                                                                  "oadUrl", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetMostRecentDownl" +
                                                                                                        "oadUrlResponse")]
        System.IAsyncResult BeginGetMostRecentDownloadUrl(Credentials credentials, AccountRequest accountRequest, int desiredData, System.AsyncCallback callback, object asyncState);
        
        DownloadDetailResult EndGetMostRecentDownloadUrl(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugDiseaseInterac" +
                                                               "tion", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugDiseaseInterac" +
                                                                                   "tionResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        DrugDiseaseDetailResult DrugDiseaseInteraction(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string[] diseaseList, string diseaseCodeType, string[] proposedMedications, string drugStandardType);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugDiseaseInterac" +
                                                                                  "tion", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugDiseaseInterac" +
                                                                                                      "tionResponse")]
        System.IAsyncResult BeginDrugDiseaseInteraction(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string[] diseaseList, string diseaseCodeType, string[] proposedMedications, string drugStandardType, System.AsyncCallback callback, object asyncState);
        
        DrugDiseaseDetailResult EndDrugDiseaseInteraction(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/PharmacySearchV3", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/PharmacySearchV3Re" +
                                                                                                                                                              "sponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        PharmacyDetailResultV2 PharmacySearchV3(Credentials credentials, AccountRequest accountRequest, string postalCode, string phoneNumber, string streetName, string pharmacyName, string city, string state, string ncpdpID, string healthplanID, string healthplanTypeID, string eligibilityIndex);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/PharmacySearchV3", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/PharmacySearchV3Re" +
                                                                                                                                                                                 "sponse")]
        System.IAsyncResult BeginPharmacySearchV3(Credentials credentials, AccountRequest accountRequest, string postalCode, string phoneNumber, string streetName, string pharmacyName, string city, string state, string ncpdpID, string healthplanID, string healthplanTypeID, string eligibilityIndex, System.AsyncCallback callback, object asyncState);
        
        PharmacyDetailResultV2 EndPharmacySearchV3(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugSearchWithForm" +
                                                               "ularyWithFavoritesV2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugSearchWithForm" +
                                                                                                   "ularyWithFavoritesV2Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        DrugFormularyFavoriteDetailResult DrugSearchWithFormularyWithFavoritesV2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string healthplanID, string healthplanTypeID, string eligibilityIndex, string drugName, string includeObsolete, string searchBrandGeneric, string searchRxOTC, string searchDrugSupply, string locationId, string providerId);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugSearchWithForm" +
                                                                                  "ularyWithFavoritesV2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugSearchWithForm" +
                                                                                                                      "ularyWithFavoritesV2Response")]
        System.IAsyncResult BeginDrugSearchWithFormularyWithFavoritesV2(
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
            object asyncState);
        
        DrugFormularyFavoriteDetailResult EndDrugSearchWithFormularyWithFavoritesV2(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugSearchWithForm" +
                                                               "ularyWithFavoritesV3", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugSearchWithForm" +
                                                                                                   "ularyWithFavoritesV3Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result DrugSearchWithFormularyWithFavoritesV3(
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
            string includeSchema);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugSearchWithForm" +
                                                                                  "ularyWithFavoritesV3", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugSearchWithForm" +
                                                                                                                      "ularyWithFavoritesV3Response")]
        System.IAsyncResult BeginDrugSearchWithFormularyWithFavoritesV3(
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
            object asyncState);
        
        Result EndDrugSearchWithFormularyWithFavoritesV3(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/HealthplanSearchV2" +
                                                               "", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/HealthplanSearchV2" +
                                                                               "Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        HealthplanDetailResult HealthplanSearchV2(Credentials credentials, AccountRequest accountRequest, string healthplan, string state, string searchType, string resultOrder);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/HealthplanSearchV2" +
                                                                                  "", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/HealthplanSearchV2" +
                                                                                                  "Response")]
        System.IAsyncResult BeginHealthplanSearchV2(Credentials credentials, AccountRequest accountRequest, string healthplan, string state, string searchType, string resultOrder, System.AsyncCallback callback, object asyncState);
        
        HealthplanDetailResult EndHealthplanSearchV2(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/FormularyCoverageV" +
                                                               "3", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/FormularyCoverageV" +
                                                                                "3Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        FormularyCoverageDetailResultV3 FormularyCoverageV3(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string healthplanID, string healthplanTypeID, string eligibilityIndex, string[] drugConcept, string drugConceptType);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/FormularyCoverageV" +
                                                                                  "3", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/FormularyCoverageV" +
                                                                                                   "3Response")]
        System.IAsyncResult BeginFormularyCoverageV3(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string healthplanID, string healthplanTypeID, string eligibilityIndex, string[] drugConcept, string drugConceptType, System.AsyncCallback callback, object asyncState);
        
        FormularyCoverageDetailResultV3 EndFormularyCoverageV3(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/ReportPrescribingC" +
                                                               "ount", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/ReportPrescribingC" +
                                                                                   "ountResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result ReportPrescribingCount(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string reportType, string reportStartDate, string reportEndDate, string prescriptionType, string prescriptionCount);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/ReportPrescribingC" +
                                                                                  "ount", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/ReportPrescribingC" +
                                                                                                      "ountResponse")]
        System.IAsyncResult BeginReportPrescribingCount(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string reportType, string reportStartDate, string reportEndDate, string prescriptionType, string prescriptionCount, System.AsyncCallback callback, object asyncState);
        
        Result EndReportPrescribingCount(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetCounselingMessa" +
                                                               "ges", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetCounselingMessa" +
                                                                                  "gesResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        CounselingMessageDetailResult GetCounselingMessages(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string drugConcept, string drugStandardType);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetCounselingMessa" +
                                                                                  "ges", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetCounselingMessa" +
                                                                                                     "gesResponse")]
        System.IAsyncResult BeginGetCounselingMessages(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string drugConcept, string drugStandardType, System.AsyncCallback callback, object asyncState);
        
        CounselingMessageDetailResult EndGetCounselingMessages(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetSideEffects", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetSideEffectsResp" +
                                                                                                                                                            "onse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result GetSideEffects(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string drugConcept, string drugStandardType, string diseaseDescriptionType, string includeSchema, string sortOrder);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetSideEffects", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetSideEffectsResp" +
                                                                                                                                                                               "onse")]
        System.IAsyncResult BeginGetSideEffects(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string drugConcept, string drugStandardType, string diseaseDescriptionType, string includeSchema, string sortOrder, System.AsyncCallback callback, object asyncState);
        
        Result EndGetSideEffects(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetAccountStatusDe" +
                                                               "tail", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetAccountStatusDe" +
                                                                                   "tailResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result GetAccountStatusDetail(Credentials credentials, AccountRequest accountRequest, string locationId, string licensedPrescriberId, string statusSectionType, string includeSchema, string sortOrder);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetAccountStatusDe" +
                                                                                  "tail", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetAccountStatusDe" +
                                                                                                      "tailResponse")]
        System.IAsyncResult BeginGetAccountStatusDetail(Credentials credentials, AccountRequest accountRequest, string locationId, string licensedPrescriberId, string statusSectionType, string includeSchema, string sortOrder, System.AsyncCallback callback, object asyncState);
        
        Result EndGetAccountStatusDetail(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPBMEligibilityV" +
                                                               "2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPBMEligibilityV" +
                                                                                "2Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        EligibilityDetailResult GetPBMEligibilityV2(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string xmlIn, string includeSchema);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPBMEligibilityV" +
                                                                                  "2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPBMEligibilityV" +
                                                                                                   "2Response")]
        System.IAsyncResult BeginGetPBMEligibilityV2(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string xmlIn, string includeSchema, System.AsyncCallback callback, object asyncState);
        
        EligibilityDetailResult EndGetPBMEligibilityV2(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPBMEligibilityV" +
                                                               "3", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPBMEligibilityV" +
                                                                                "3Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        EligibilityDetailResultV3 GetPBMEligibilityV3(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string xmlIn, string includeSchema);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPBMEligibilityV" +
                                                                                  "3", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPBMEligibilityV" +
                                                                                                   "3Response")]
        System.IAsyncResult BeginGetPBMEligibilityV3(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string xmlIn, string includeSchema, System.AsyncCallback callback, object asyncState);
        
        EligibilityDetailResultV3 EndGetPBMEligibilityV3(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPrescriptionTra" +
                                                               "nsmissionStatusV2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPrescriptionTra" +
                                                                                                "nsmissionStatusV2Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        TransmissionSummaryResult GetPrescriptionTransmissionStatusV2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, PrescriptionTransmissionQueryType queryType, string queryId);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPrescriptionTra" +
                                                                                  "nsmissionStatusV2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetPrescriptionTra" +
                                                                                                                   "nsmissionStatusV2Response")]
        System.IAsyncResult BeginGetPrescriptionTransmissionStatusV2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, PrescriptionTransmissionQueryType queryType, string queryId, System.AsyncCallback callback, object asyncState);
        
        TransmissionSummaryResult EndGetPrescriptionTransmissionStatusV2(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetSubmittedMessag" +
                                                               "eTransactionStatus", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetSubmittedMessag" +
                                                                                                 "eTransactionStatusResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        MessageTransactionStatusResult GetSubmittedMessageTransactionStatus(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string messageTransactionId, string messageTransactionSource);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetSubmittedMessag" +
                                                                                  "eTransactionStatus", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetSubmittedMessag" +
                                                                                                                    "eTransactionStatusResponse")]
        System.IAsyncResult BeginGetSubmittedMessageTransactionStatus(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string messageTransactionId, string messageTransactionSource, System.AsyncCallback callback, object asyncState);
        
        MessageTransactionStatusResult EndGetSubmittedMessageTransactionStatus(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugAllergyInterac" +
                                                               "tionV2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugAllergyInterac" +
                                                                                     "tionV2Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        DrugAllergyDetailResultV2 DrugAllergyInteractionV2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string[] allergies, string[] proposedMedications, string drugStandardType);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugAllergyInterac" +
                                                                                  "tionV2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DrugAllergyInterac" +
                                                                                                        "tionV2Response")]
        System.IAsyncResult BeginDrugAllergyInteractionV2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string[] allergies, string[] proposedMedications, string drugStandardType, System.AsyncCallback callback, object asyncState);
        
        DrugAllergyDetailResultV2 EndDrugAllergyInteractionV2(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/ResolveFailedPresc" +
                                                               "riptionTransmission", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/ResolveFailedPresc" +
                                                                                                  "riptionTransmissionResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result ResolveFailedPrescriptionTransmission(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string prescriptionType, string transactionId);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/ResolveFailedPresc" +
                                                                                  "riptionTransmission", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/ResolveFailedPresc" +
                                                                                                                     "riptionTransmissionResponse")]
        System.IAsyncResult BeginResolveFailedPrescriptionTransmission(Credentials credentials, AccountRequest accountRequest, PatientInformationRequester patientInformationRequester, string prescriptionType, string transactionId, System.AsyncCallback callback, object asyncState);
        
        Result EndResolveFailedPrescriptionTransmission(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetMeaningfulUsePa" +
                                                               "tientEncounterInfo", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetMeaningfulUsePa" +
                                                                                                 "tientEncounterInfoResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result GetMeaningfulUsePatientEncounterInfo(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string encounterId, string includeSchema);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetMeaningfulUsePa" +
                                                                                  "tientEncounterInfo", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/GetMeaningfulUsePa" +
                                                                                                                    "tientEncounterInfoResponse")]
        System.IAsyncResult BeginGetMeaningfulUsePatientEncounterInfo(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string encounterId, string includeSchema, System.AsyncCallback callback, object asyncState);
        
        Result EndGetMeaningfulUsePatientEncounterInfo(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DoseCheck", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DoseCheckResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Result DoseCheck(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester, string drugId, string drugTypeId, string birthDateCCYYMMDD, string gender, string diagnosis, string doseType, string dose, string doseUOM, string weightInKg, string includeSchema);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DoseCheck", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IUpdate1Soap/DoseCheckResponse")]
        System.IAsyncResult BeginDoseCheck(
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
            object asyncState);
        
        Result EndDoseCheck(System.IAsyncResult result);
    }
}