namespace Pillar.Common.Specification
{
    /// <summary>
    /// Interface for class that specifies a Specification that can be satisfied by a <typeparamref name="TEntity">Entity</typeparamref>.
    /// </summary>
    /// <typeparam name="TEntity">Type specification is written for.</typeparam>
    public interface ISpecification<in TEntity>
    {
        #region Public Methods

        /// <summary>
        /// Gets whether <paramref name="entity">Entity</paramref> meets specification.
        /// </summary>
        /// <param name="entity">Entity to test.</param>
        /// <returns>A <see cref="bool">Boolean</see>.</returns>
        bool IsSatisfiedBy ( TEntity entity );

        #endregion
    }
}
