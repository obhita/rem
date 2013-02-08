namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiClosure exposes the DensAsiClosureSection which contains closing questions.
    /// </summary>
    public class DensAsiClosure : DensAsiInterviewSectionAggregateNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiClosure"/> class.
        /// </summary>
        protected DensAsiClosure()
        {
        }

        internal DensAsiClosure(DensAsiInterview densAsiInterview)
            : base(densAsiInterview)
        {
        }

        /// <summary>
        /// Gets or sets the DensAsi closure section.
        /// </summary>
        /// <value>
        /// The DensAsi closure section.
        /// </value>
        public virtual DensAsiClosureSection DensAsiClosureSection { get; protected internal set; }
    }
}