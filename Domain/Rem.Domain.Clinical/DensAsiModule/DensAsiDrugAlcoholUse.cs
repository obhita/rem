namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiDrugAlcoholUse exposes the DensAsiDrugAlcoholUseSection which contains drug and alcohol history related information.
    /// </summary>
    public class DensAsiDrugAlcoholUse : DensAsiInterviewSectionAggregateNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiDrugAlcoholUse"/> class.
        /// </summary>
        protected DensAsiDrugAlcoholUse()
        {
        }

        internal DensAsiDrugAlcoholUse(DensAsiInterview densAsiInterview)
            : base(densAsiInterview)
        {
        }

        /// <summary>
        /// Gets or sets the DensAsi drug alcohol use section.
        /// </summary>
        /// <value>
        /// The DensAsi drug alcohol use section.
        /// </value>
        public virtual DensAsiDrugAlcoholUseSection DensAsiDrugAlcoholUseSection { get; protected internal set; }
    }
}