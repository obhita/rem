namespace Rem.WellKnownNames.NewCropModule
{
    /// <summary>
    /// Source: http://preproduction.newcropaccounts.com/V7/WEBSERVICESDEMO/update1.aspx#GetPatientFullMedicationHistory6
    /// N = Not archived (i.e. Current Medication) Y = Archived (i.e. Previous Medication)
    /// Note: This field will contain values other than Y,N in future releases.
    /// </summary>
    public class HistoryRequestPrescriptionArchiveStatus : WellKnownNameBase
    {
        public static readonly HistoryRequestPrescriptionArchiveStatus NotArchived = new HistoryRequestPrescriptionArchiveStatus ( "NotArchived", "N",
                                                                                                                                   " Not archived (i.e. Current Medication)" );
        public static readonly HistoryRequestPrescriptionArchiveStatus Archived = new HistoryRequestPrescriptionArchiveStatus("Archived", "Y",
                                                                                                                                 "Archived (i.e. Previous Medication)");

        public HistoryRequestPrescriptionArchiveStatus ( string wellKnownName, string code, string description )
            : base ( wellKnownName, code, description )
        {
        }
    }
}