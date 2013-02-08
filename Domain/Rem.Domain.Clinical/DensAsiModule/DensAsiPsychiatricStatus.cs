namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiPsychiatricStatus exposes the DensAsiPsychiatricStatusSection which contains psychiatric status information.
    /// </summary>
    public class DensAsiPsychiatricStatus : DensAsiInterviewSectionAggregateNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiPsychiatricStatus"/> class.
        /// </summary>
        protected DensAsiPsychiatricStatus()
        {
        }

        internal DensAsiPsychiatricStatus(DensAsiInterview densAsiInterview) 
            : base(densAsiInterview)
        {
        }

        /// <summary>
        /// Gets or sets the DenAsi psychiatric status section.
        /// </summary>
        /// <value>
        /// The DenAsi psychiatric status section.
        /// </value>
        public virtual DensAsiPsychiatricStatusSection DensAsiPsychiatricStatusSection { get; protected internal set; }
    }
}