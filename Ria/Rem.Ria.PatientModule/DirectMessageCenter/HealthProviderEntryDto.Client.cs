namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// Data transfer object for HealthProviderEntry class.
    /// </summary>
    public partial class HealthProviderEntryDto
    {
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName { get { return string.Format ( "{1} {0}", LastName, FirstName ); } }
    }
}
