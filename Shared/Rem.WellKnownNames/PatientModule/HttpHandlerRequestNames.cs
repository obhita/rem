namespace Rem.WellKnownNames.PatientModule
{
    /// <summary>
    /// These are well known Request Names for RequestName QueryString of Patient Module HttpHandler
    /// </summary>
    public static class HttpHandlerRequestNames
    {
        public static readonly string DownloadPatientDocument = "DownloadPatientDocument";
        public static readonly string DownloadC32Document = "DownloadC32Document";
        public static readonly string DownloadGreenC32Document = "DownloadGreenC32Document";
        public static readonly string DownloadHl7ImmunizationDocument = "DownloadHL7ImmunizationDocument";
        public static readonly string DownloadHl7SyndromicSurveillanceDocument = "DownloadHl7SyndromicSurveillanceDocument";
        public static readonly string ViewC32Document = "ViewC32Document";
        public static readonly string DownloadMailAttachment = "DownloadMailAttachment";
    }
}