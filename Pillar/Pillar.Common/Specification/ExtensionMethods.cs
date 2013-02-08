namespace Pillar.Common.Specification
{
    /// <summary>
    /// Extension methods for <see cref="ISpecification{TEntity}">ISpecification&lt;TEntity&gt;</see>.
    /// </summary>
    public static class ExtensionMethods
    {
        #region Public Methods

        /// <summary>
        /// Creates an <see cref="AndSpecification{TEntity}">AndSpecification</see>.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity the specification is written for.</typeparam>
        /// <param name="s1">First <see cref="ISpecification{TEntity}">ISpecification&lt;TEntity&gt;</see> to and.</param>
        /// <param name="s2">Second <see cref="ISpecification{TEntity}">ISpecification&lt;TEntity&gt;</see> to and.</param>
        /// <returns>An <see cref="ISpecification{TEntity}">ISpecification&lt;TEntity&gt;</see></returns>
        public static ISpecification<TEntity> And<TEntity> ( this ISpecification<TEntity> s1, ISpecification<TEntity> s2 )
        {
            return new AndSpecification<TEntity> ( s1, s2 );
        }

        /// <summary>
        /// Creates a <see cref="NotSpecification{TEntity}">NotSpecification</see>
        /// </summary>
        /// <typeparam name="TEntity">Type of entity the specification is written for.</typeparam>
        /// <param name="s"><see cref="ISpecification{TEntity}">ISpecification&lt;TEntity&gt;</see> to not.</param>
        /// <returns>An <see cref="ISpecification{TEntity}">ISpecification&lt;TEntity&gt;</see></returns>
        public static ISpecification<TEntity> Not<TEntity> ( this ISpecification<TEntity> s )
        {
            return new NotSpecification<TEntity> ( s );
        }

        /// <summary>
        /// Creates an <see cref="OrSpecification{TEntity}">OrSpecification</see>.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity the specification is written for.</typeparam>
        /// <param name="s1">First <see cref="ISpecification{TEntity}">ISpecification&lt;TEntity&gt;</see> to or.</param>
        /// <param name="s2">Second <see cref="ISpecification{TEntity}">ISpecification&lt;TEntity&gt;</see> to or.</param>
        /// <returns>An <see cref="ISpecification{TEntity}">ISpecification&lt;TEntity&gt;</see></returns>
        public static ISpecification<TEntity> Or<TEntity> ( this ISpecification<TEntity> s1, ISpecification<TEntity> s2 )
        {
            return new OrSpecification<TEntity> ( s1, s2 );
        }

        #endregion
    }
}
