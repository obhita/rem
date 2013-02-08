namespace Rem.WellKnownNames.NewCropModule
{
    /// <summary>
    /// Source: http://preproduction.newcropaccounts.com/V7/WEBSERVICESDEMO/update1.aspx#GetPatientFullMedicationHistory6
    /// Indicates if the receiving pharmacy is electronic, fax, or not selected.
    /// 1   =  Electronic
    /// 2   =  Fax
    /// 0   =  No Pharmacy Selected
    /// </summary>
    public class PharmacyType : WellKnownNameBase
    {
        public static readonly PharmacyType Electronic = new PharmacyType("Electronic", "1", "Electronic");
        public static readonly PharmacyType Fax = new PharmacyType("Fax", "2", "Fax");
        public static readonly PharmacyType NoPharmacySelected = new PharmacyType("NoPharmacySelected", "0", "No Pharmacy Selected");
  
        public PharmacyType ( string wellKnownName, string code, string description )
            : base ( wellKnownName, code, description )
        {
        }
    }
}