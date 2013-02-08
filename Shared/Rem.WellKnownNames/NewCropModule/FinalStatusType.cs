namespace Rem.WellKnownNames.NewCropModule
{
    /// <summary>
    /// Source: http://preproduction.newcropaccounts.com/V7/WEBSERVICESDEMO/update1.aspx#GetPatientFullMedicationHistory6
    /// FinalStatusType: Indicates the prescription status
    /// 0   =   Not Selected ** Not in documentation, but present in received data. 
    /// 1   =   Success
    /// 2   =   Error
    /// 3   =   Queued
    /// 4   =   Unknown
    /// 5   =   Verified
    /// </summary>
    public class FinalStatusType : WellKnownNameBase
    {
        public static readonly FinalStatusType NotSelected = new FinalStatusType("NotSelected", "0", "Not Selected ** Not in documentation, but present in received data.");
        public static readonly FinalStatusType Success = new FinalStatusType ( "Success", "1", "Success" );
        public static readonly FinalStatusType Error = new FinalStatusType ( "Error", "2", "Error" );
        public static readonly FinalStatusType Queued = new FinalStatusType ( "Queued", "3", "Queued" );
        public static readonly FinalStatusType Unknown = new FinalStatusType ( "Unknown", "4", "Unknown" );
        public static readonly FinalStatusType Verified = new FinalStatusType ( "Verified", "5", "Verified" );

        public FinalStatusType ( string wellKnownName, string code, string description )
            : base ( wellKnownName, code, description )
        {
        }
    }
}