using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.ClinicalCaseModule
{
    /// <summary>
    /// ProblemType lookup contains a list of coded concept problem types (e.g. Finding, Symptom).
    /// </summary>
    public class ProblemType : CodedConceptLookupBase
    {
        private const string CODE_SYSTEM_IDENTIFIER = "2.16.840.1.113883.6.96";
        private const string CODE_SYSTEM_NAME_CONST = "SNOMED-CT";
        private const string CODE_SYSTEM_VERSION_NUMBER_CONST = "Unknown";

        /// <summary>
        /// Gets the code system identifier.
        /// </summary>
        [IgnoreMapping]
        public override string CodeSystemIdentifier
        {
            get { return CODE_SYSTEM_IDENTIFIER; }
        }

        /// <summary>
        /// Gets the name of the code system.
        /// </summary>
        /// <value>
        /// The name of the code system.
        /// </value>
        [IgnoreMapping]
        public override string CodeSystemName
        {
            get { return CODE_SYSTEM_NAME_CONST; }
        }

        /// <summary>
        /// Gets the code system version number.
        /// </summary>
        [IgnoreMapping]
        public override string CodeSystemVersionNumber
        {
            get { return CODE_SYSTEM_VERSION_NUMBER_CONST; }
        }
    }
}
