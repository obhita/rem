namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiPatientProfile exposes the DensAsiPatientProfileSection which contains general patient information.
    /// </summary>
    public class DensAsiPatientProfile : DensAsiInterviewSectionAggregateNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiPatientProfile"/> class.
        /// </summary>
        protected DensAsiPatientProfile()
        {
        }

        internal DensAsiPatientProfile(DensAsiInterview densAsiInterview)
            : base(densAsiInterview)

        {
        }

        /// <summary>
        /// Gets or sets the DensAsi patient profile section.
        /// </summary>
        /// <value>
        /// The DensAsi patient profile section.
        /// </value>
        public virtual DensAsiPatientProfileSection DensAsiPatientProfileSection { get; protected internal set; }
    }
}