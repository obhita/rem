namespace Pillar.Common.Specification
{
    /// <summary>
    /// Not Specification class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    internal class NotSpecification<TEntity> : ISpecification<TEntity>
    {
        #region Constants and Fields

        private readonly ISpecification<TEntity> _wrapped;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NotSpecification&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="x">The specification to not.</param>
        internal NotSpecification ( ISpecification<TEntity> x )
        {
            _wrapped = x;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether is satisfied by the specified candidate.
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <returns><c>true</c> if is satisfied by the specified candidate; otherwise, <c>false</c>.</returns>
        public bool IsSatisfiedBy ( TEntity candidate )
        {
            return !_wrapped.IsSatisfiedBy ( candidate );
        }

        #endregion
    }
}
