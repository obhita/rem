namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiLegalStatus exposes the DensAsiLegalStatusSection which contains general legal information.
    /// </summary>
    public class DensAsiLegalStatus : DensAsiInterviewSectionAggregateNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiLegalStatus"/> class.
        /// </summary>
        protected DensAsiLegalStatus()
        {
        }

        internal DensAsiLegalStatus(DensAsiInterview densAsiInterview)
            : base(densAsiInterview)
        {
        }

        /// <summary>
        /// Gets or sets the DensAsi legal status section.
        /// </summary>
        /// <value>
        /// The DensAsi legal status section.
        /// </value>
        public virtual DensAsiLegalStatusSection DensAsiLegalStatusSection { get; protected internal set; }
    }
}