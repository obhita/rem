namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiPatientProfile exposes the DensAsiPatientProfileSection which contains medical status related information.
    /// </summary>
    public class DensAsiMedicalStatus : DensAsiInterviewSectionAggregateNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiMedicalStatus"/> class.
        /// </summary>
        protected DensAsiMedicalStatus()
        {
        }

        internal DensAsiMedicalStatus(DensAsiInterview densAsiInterview)
            : base(densAsiInterview)
        {
        }

        /// <summary>
        /// Gets or sets the DensAsi medical status section.
        /// </summary>
        /// <value>
        /// The DensAsi medical status section.
        /// </value>
        public virtual DensAsiMedicalStatusSection DensAsiMedicalStatusSection { get; protected internal set; }
    }
}