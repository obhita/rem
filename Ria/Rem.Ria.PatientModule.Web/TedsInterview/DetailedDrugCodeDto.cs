using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <summary>
    /// This class defines DetailedDrugCode data transfer object.
    /// </summary>
    public class DetailedDrugCodeDto : LookupValueDto
    {
        /// <summary>
        /// Gets or sets the substance problem type key.
        /// </summary>
        /// <value>
        /// The substance problem type key.
        /// </value>
        public long SubstanceProblemTypeKey { get; set; }
    }
}
