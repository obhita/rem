using Pillar.Domain;

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// AdministrativeGender defines an administrative subset of gender designations.
    /// </summary>
    public class AdministrativeGender : CodedConceptLookupBase
    {
        private const string CODE_SYSTEM_IDENTIFIER = "2.16.840.1.113883.5.1";
        private const string CODE_SYSTEM_NAME_CONST = "HL7 AdministrativeGender";
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
