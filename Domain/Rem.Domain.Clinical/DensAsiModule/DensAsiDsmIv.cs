namespace Rem.Domain.Clinical.DensAsiModule
{    
    /// <summary>
    /// The DensAsiDsmIv exposes the DensAsiDsmIvSection which contains DSM-IV dependence questions.
    /// </summary>
    public class DensAsiDsmIv : DensAsiInterviewSectionAggregateNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiDsmIv"/> class.
        /// </summary>
        protected DensAsiDsmIv()
        {
        }

        internal DensAsiDsmIv(DensAsiInterview densAsiInterview)
            : base(densAsiInterview)
        {
        }

        /// <summary>
        /// Gets or sets the DensAsi DSM IV section.
        /// </summary>
        /// <value>
        /// The DensAsi DSM IV section.
        /// </value>
        public virtual DensAsiDsmIvSection DensAsiDsmIvSection { get; protected internal set; }
    }
}