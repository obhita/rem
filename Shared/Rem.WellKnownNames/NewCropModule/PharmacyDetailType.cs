namespace Rem.WellKnownNames.NewCropModule
{
    /// <summary>
    /// Source: http://preproduction.newcropaccounts.com/V7/WEBSERVICESDEMO/update1.aspx#GetPatientFullMedicationHistory6
    /// PharmacyDetailType: Indicates the network to which the receiving pharmacy belongs
    /// 0   =   Not Selected ** Not Documented but present in the Data 
    /// 1   =   Surescripts
    /// 2   =   RxHub (Merged with Surescripts)
    /// 3   =   NewCrop (i.e. Direct Fax or Printed)
    /// </summary>
    public class PharmacyDetailType : WellKnownNameBase
    {
        public static readonly PharmacyDetailType NotSelected = new PharmacyDetailType("NotSelected", "0", "Not Selected");
        public static readonly PharmacyDetailType SureScripts = new PharmacyDetailType ( "SureScripts", "1", "SureScripts" );
        public static readonly PharmacyDetailType RxHub = new PharmacyDetailType("RxHub", "2", "RxHub (Merged with Surescripts)");
        public static readonly PharmacyDetailType NewCrop = new PharmacyDetailType("NewCrop", "3", "NewCrop (i.e. Direct Fax or Printed)");

        public PharmacyDetailType ( string wellKnownName, string code, string description )
            : base ( wellKnownName, code, description )
        {
        }
    }
}