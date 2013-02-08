namespace Pillar.Common.Specification
{
    /// <summary>
    /// Or Specification class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    internal class OrSpecification<TEntity> : ISpecification<TEntity>
    {
        #region Constants and Fields

        private readonly ISpecification<TEntity> _spec1;
        private readonly ISpecification<TEntity> _spec2;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrSpecification&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="s1">The first specification.</param>
        /// <param name="s2">The second specification.</param>
        internal OrSpecification ( ISpecification<TEntity> s1, ISpecification<TEntity> s2 )
        {
            _spec1 = s1;
            _spec2 = s2;
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
            return _spec1.IsSatisfiedBy ( candidate ) || _spec2.IsSatisfiedBy ( candidate );
        }

        #endregion
    }
}
