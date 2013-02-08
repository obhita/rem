using Rem.Ria.NewCropModule.Web.Client.Common;

namespace Rem.Ria.NewCropModule.Web.Client.Patient
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PatientSoapClient : System.ServiceModel.ClientBase<IPatientSoap>, IPatientSoap
    {
        
        public PatientSoapClient()
        {
        }
        
        public PatientSoapClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
        {
        }
        
        public PatientSoapClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public PatientSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public PatientSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
        {
        }
        
        public PatientDrugDetailResult GetPatientFullMedicationHistory(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester)
        {
            return base.Channel.GetPatientFullMedicationHistory(credentials, accountRequest, patientRequest, prescriptionHistoryRequest, patientInformationRequester);
        }
        
        public PatientDrugDetailResult2 GetPatientFullMedicationHistory2(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester)
        {
            return base.Channel.GetPatientFullMedicationHistory2(credentials, accountRequest, patientRequest, prescriptionHistoryRequest, patientInformationRequester);
        }
        
        public PatientDrugDetailResult3 GetPatientFullMedicationHistory3(Credentials credentials, AccountRequest accountRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester, string patientId, string patientIdType)
        {
            return base.Channel.GetPatientFullMedicationHistory3(credentials, accountRequest, prescriptionHistoryRequest, patientInformationRequester, patientId, patientIdType);
        }
        
        public PatientDrugNameDetailResult GetPatientUniqueMedicationHistory(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PrescriptionHistoryRequest prescriptionHistoryRequest, PatientInformationRequester patientInformationRequester)
        {
            return base.Channel.GetPatientUniqueMedicationHistory(credentials, accountRequest, patientRequest, prescriptionHistoryRequest, patientInformationRequester);
        }
        
        public PatientAllergyDetailResult GetPatientAllergyHistory(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, PatientInformationRequester patientInformationRequester)
        {
            return base.Channel.GetPatientAllergyHistory(credentials, accountRequest, patientRequest, patientInformationRequester);
        }
        
        public PatientPharmacyDetailResult GetPatientPharmacyHistory(Credentials credentials, AccountRequest accountRequest, PatientRequest patientRequest, int maxCount, PatientInformationRequester patientInformationRequester)
        {
            return base.Channel.GetPatientPharmacyHistory(credentials, accountRequest, patientRequest, maxCount, patientInformationRequester);
        }
        
        public AccountStatusDetailResult GetAccountStatus(Credentials credentials, AccountRequest accountRequest, string locationId, string userType, string userId)
        {
            return base.Channel.GetAccountStatus(credentials, accountRequest, locationId, userType, userId);
        }
        
        public EligibilityDetailResult GetPBMEligibility(Credentials credentials, AccountRequest accountRequest, string xmlIn)
        {
            return base.Channel.GetPBMEligibility(credentials, accountRequest, xmlIn);
        }
        
        public DrugHistoryDetailResult GetPBMDrugHistory(Credentials credentials, AccountRequest accountRequest, string EligibilityTransactionID, int loop)
        {
            return base.Channel.GetPBMDrugHistory(credentials, accountRequest, EligibilityTransactionID, loop);
        }
    }
}