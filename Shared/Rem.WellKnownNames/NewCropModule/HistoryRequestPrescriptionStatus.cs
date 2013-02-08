
namespace Rem.WellKnownNames.NewCropModule
{
    /// <summary>
    /// Source:http://preproduction.newcropaccounts.com/V7/WEBSERVICESDEMO/update1.aspx#GetPatientFullMedicationHistory6
    /// C = Completed Prescription
    /// P = Pending Medication 
    /// % = Both, Complete and Pending 
    /// 
    /// 
    /// </summary>
    public class HistoryRequestPrescriptionStatus
    {
        public static readonly HistoryRequestPrescriptionStatus Completed = new HistoryRequestPrescriptionStatus("Completed", "C");
        public static readonly HistoryRequestPrescriptionStatus Pending = new HistoryRequestPrescriptionStatus ( "Pending", "P" );
        public static readonly HistoryRequestPrescriptionStatus All = new HistoryRequestPrescriptionStatus("All", "%");

        public HistoryRequestPrescriptionStatus(string wellKnownName, string code)
        {
            WellKnownName = wellKnownName;
            Code = code;
        }

        public string WellKnownName { get; private set; }
        public string Code { get; private set; }
    }
}
