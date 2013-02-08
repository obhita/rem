using System.Collections.Generic;
using Agatha.Common;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// GetHealthcareProviderDirectoryEntriesResponse class
    /// </summary>
    public class GetHealthcareProviderDirectoryEntriesResponse : Response
    {
        /// <summary>
        /// Gets or sets the providers.
        /// </summary>
        /// <value>
        /// The providers.
        /// </value>
        public IEnumerable<HealthProviderEntryDto> Providers { get; set; }
    }
}