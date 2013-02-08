namespace Rem.WellKnownNames.X12Codes
{
    /// <summary>
    /// X12 Element Code 1325.
    /// Refer the code set: http://www.nubc.org/FL4forWeb2_RO.pdf
    /// For all code sets: https://www.claredi.com/hipaa/codesets.php
    /// </summary>
    public static class ClaimFrequencyTypeCode
    {
        /// <summary>
        ///  Admit thru Discharge Claim
        /// </summary>
        public static readonly string Original = "1";

        /// <summary>
        ///  Replacement of Prior Claim
        /// </summary>
        public static readonly string Replacement = "7";

        /// <summary>
        ///  Void/Cancel of Prior Claim
        /// </summary>
        public static readonly string Void = "8";
    }
}
