namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiEmploymentStatus exposes the DensAsiEmploymentStatusSection which contains employment status information.
    /// </summary>
    public class DensAsiEmploymentStatus : DensAsiInterviewSectionAggregateNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiEmploymentStatus"/> class.
        /// </summary>
        protected DensAsiEmploymentStatus()
        {
        }

        internal DensAsiEmploymentStatus(DensAsiInterview densAsiInterview)
            : base(densAsiInterview)
        {
        }

        /// <summary>
        /// Gets or sets the DensAsi employment status section.
        /// </summary>
        /// <value>
        /// The DensAsi employment status section.
        /// </value>
        public virtual DensAsiEmploymentStatusSection DensAsiEmploymentStatusSection { get; protected internal set; }
    }
}