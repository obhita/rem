namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiFamilySocialRelationships exposes the DensAsiFamilySocialRelationshipsSection which contains family and social relationship information.
    /// </summary>
    public class DensAsiFamilySocialRelationships : DensAsiInterviewSectionAggregateNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiFamilySocialRelationships"/> class.
        /// </summary>
        protected DensAsiFamilySocialRelationships()
        {
        }

        internal DensAsiFamilySocialRelationships(DensAsiInterview densAsiInterview)
            : base(densAsiInterview)
        {
        }

        /// <summary>
        /// Gets or sets the family social relationships section.
        /// </summary>
        /// <value>
        /// The family social relationships section.
        /// </value>
        public virtual DensAsiFamilySocialRelationshipsSection DensAsiFamilySocialRelationshipsSection { get; protected internal set; }
    }
}