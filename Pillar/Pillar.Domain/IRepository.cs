namespace Pillar.Domain
{
    /// <summary>
    /// IRepository interface defines basic repository services.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity>
        where TEntity : IAggregateRoot
    {
        /// <summary>
        /// Gets the by key.
        /// </summary>
        /// <param name="key">The primary key.</param>
        /// <returns>A TEntity.</returns>
        TEntity GetByKey ( long key );

        /// <summary>
        /// Makes the persistent.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A TEntity.</returns>
        TEntity MakePersistent ( TEntity entity );

        /// <summary>
        /// Makes the transient.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void MakeTransient ( TEntity entity );
    }
}
