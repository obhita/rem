namespace Rem.WellKnownNames.NewCropModule
{
    /// <summary>
    /// Source: http://preproduction.newcropaccounts.com/V7/WEBSERVICESDEMO/update1.aspx#GetPatientFullMedicationHistory6
    /// % = All meds (Returns all meds regardless of the sub status)  
    /// A = NS (Returns only meds that have a 'NS' - Needs staff sub status)  
    /// U = DR (Returns only meds that have a 'DR' - Needs doctor review sub status)  
    /// P = Renewal Request that has been selected for processing on the NewCrop screens - it has not yet been denied, denied and re-written or accepted  
    /// S = Standard Rx (Returns only meds that have an 'InProc' - InProcess sub status)  
    /// D = DrugSet source - indicates the prescription was created by selecting the medication from the DrugSet selection box on the ComposeRx page  
    /// O = Outside Prescription - indicates the prescription was created on the MedEntry page  
    /// </summary>
    public class HistoryRequestPrescriptionSubStatus : WellKnownNameBase
    {
        public static readonly HistoryRequestPrescriptionSubStatus All =
            new HistoryRequestPrescriptionSubStatus ( "All", "%", "All meds (Returns all meds regardless of the sub status)" );

        public static readonly HistoryRequestPrescriptionSubStatus StandardRx =
            new HistoryRequestPrescriptionSubStatus ( "StandardRx", "S",
                                                      "Standard Rx (Returns only meds that have an 'InProc' - InProcess sub status)" );

        public static readonly HistoryRequestPrescriptionSubStatus OutsidePrescription =
            new HistoryRequestPrescriptionSubStatus("OutsidePrescription", "O",
                                                      "Outside Prescription - indicates the prescription was created on the MedEntry page.");

        public HistoryRequestPrescriptionSubStatus ( string wellKnownName, string code, string description )
            : base ( wellKnownName, code, description )
        {
        }
    }
}