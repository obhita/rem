using Pillar.Domain;

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// Provides a base implementation of a coded concept deriving from <see cref="T:Rem.Domain.CommonModule.LookupBase">LookupBase</see>.
    /// </summary>
    public abstract class CodedConceptLookupBase : LookupBase
    {
        private string _codedConceptCode;

        /// <summary>
        /// Gets the code system version number.
        /// </summary>
        [IgnoreMapping]
        public abstract string CodeSystemVersionNumber { get; }

        /// <summary>
        /// Gets the name of the code system.
        /// </summary>
        /// <value>
        /// The name of the code system.
        /// </value>
        [IgnoreMapping]
        public abstract string CodeSystemName { get; }

        /// <summary>
        /// Gets the code system identifier.
        /// </summary>
        [IgnoreMapping]
        public abstract string CodeSystemIdentifier { get; }

        /// <summary>
        /// Gets or sets the coded concept code.
        /// </summary>
        /// <value>
        /// The coded concept code.
        /// </value>
        [NotNull]
        public virtual string CodedConceptCode
        {
            get { return _codedConceptCode; }
            set { ApplyPropertyChange(ref _codedConceptCode, () => CodedConceptCode, value); }
        }
    }
}