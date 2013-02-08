using Rem.Ria.NewCropModule.Web.Client.Common;

namespace Rem.Ria.NewCropModule.Web.Client.Patient
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://secure.newcropaccounts.com/V7/webservices", ConfigurationName="IPatientSoap")]
    public interface IPatientSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPatientFullMedi" +
                                                               "cationHistory", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPatientFullMedi" +
                                                                                            "cationHistoryResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        PatientDrugDetailResult GetPatientFullMedicationHistory(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPatientFullMedi" +
                                                               "cationHistory2", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPatientFullMedi" +
                                                                                             "cationHistory2Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        PatientDrugDetailResult2 GetPatientFullMedicationHistory2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPatientFullMedi" +
                                                               "cationHistory3", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPatientFullMedi" +
                                                                                             "cationHistory3Response")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        PatientDrugDetailResult3 GetPatientFullMedicationHistory3(Credentials credentials, AccountRequest accountRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester, string patientId, string patientIdType);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPatientUniqueMe" +
                                                               "dicationHistory", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPatientUniqueMe" +
                                                                                              "dicationHistoryResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        PatientDrugNameDetailResult GetPatientUniqueMedicationHistory(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPatientAllergyH" +
                                                               "istory", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPatientAllergyH" +
                                                                                     "istoryResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        PatientAllergyDetailResult GetPatientAllergyHistory(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPatientPharmacy" +
                                                               "History", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPatientPharmacy" +
                                                                                      "HistoryResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        PatientPharmacyDetailResult GetPatientPharmacyHistory(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, int maxCount, PatientInformationRequester patientInformationRequester);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetAccountStatus", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetAccountStatusRe" +
                                                                                                                                                              "sponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AccountStatusDetailResult GetAccountStatus(Credentials credentials, AccountRequest accountRequest, string locationId, string userType, string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPBMEligibility", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPBMEligibilityR" +
                                                                                                                                                               "esponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        EligibilityDetailResult GetPBMEligibility(Credentials credentials, AccountRequest accountRequest, string xmlIn);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPBMDrugHistory", ReplyAction="https://secure.newcropaccounts.com/V7/webservices/IPatientSoap/GetPBMDrugHistoryR" +
                                                                                                                                                               "esponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        DrugHistoryDetailResult GetPBMDrugHistory(Credentials credentials, AccountRequest accountRequest, string EligibilityTransactionID, int loop);
    }
}