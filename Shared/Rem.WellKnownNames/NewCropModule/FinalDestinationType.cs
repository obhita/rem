namespace Rem.WellKnownNames.NewCropModule
{
    /// <summary>
    /// Source: http://preproduction.newcropaccounts.com/V7/WEBSERVICESDEMO/update1.aspx#GetPatientFullMedicationHistory6
    /// FinalDestinationType: Indicates the transmission method from NewCrop to the receiving entity
    /// 0  =   Not Transmitted
    /// 1  =   Print
    /// 2  =   Fax
    /// 3  =   Electronic/Surescripts Retail
    /// 4  =   Electronic/Surescripts Mail Order
    /// 5  =   Test
    /// </summary>
    public class FinalDestinationType : WellKnownNameBase
    {
        public static readonly FinalDestinationType NotTransmitted = new FinalDestinationType ( "NotTransmitted", "0", "Not Transmitted" );
        public static readonly FinalDestinationType Print = new FinalDestinationType ( "Print", "1", "Print" );
        public static readonly FinalDestinationType Fax = new FinalDestinationType ( "Fax", "2", "Fax" );

        public static readonly FinalDestinationType SureScriptsRetail = new FinalDestinationType ( "SureScriptsRetail", "3",
                                                                                                   "Electronic/Surescripts Retail" );

        public static readonly FinalDestinationType SureScriptsMailOrder = new FinalDestinationType ( "SureScriptsMailOrder", "4",
                                                                                                      "Electronic/Surescripts Mail Order" );

        public static readonly FinalDestinationType Test = new FinalDestinationType ( "Test", "5", "Test" );

        public FinalDestinationType ( string wellKnownName, string code, string description )
            : base ( wellKnownName, code, description )
        {
        }
    }
}