using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// CodingContextAggregateNodeBase provides a base aggregrate node implementation for coding context aggregate.
    /// </summary>
    public abstract class CodingContextAggregateNodeBase: AuditableAggregateNodeBase
    {
        #region Private Members

        private readonly CodingContext _codingContext;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CodingContextAggregateNodeBase"/> class.
        /// </summary>
        protected CodingContextAggregateNodeBase()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodingContextAggregateNodeBase"/> class.
        /// </summary>
        /// <param name="codingContext">The coding context.</param>
        protected CodingContextAggregateNodeBase(CodingContext codingContext)
        {
            Check.IsNotNull(codingContext, "Coding context is required.");
            _codingContext = codingContext;
        }

        /// <summary>
        /// Gets or sets the coding context.
        /// </summary>
        /// <value>
        /// The coding context.
        /// </value>
        [NotNull]
        public virtual CodingContext CodingContext
        {
            get { return _codingContext; }
            protected set { }
        }

        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        public override IAggregateRoot AggregateRoot
        {
            get { return _codingContext; }
        }
    }
}