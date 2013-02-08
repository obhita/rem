namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Interface of a repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the T entity.</typeparam>
    public interface IRepository<TEntity>
    {
        #region Public Methods

        /// <summary>
        /// Gets the entity by id.
        /// </summary>
        /// <param name="id">The id to get.</param>
        /// <returns>The entity.</returns>
        TEntity GetById ( long id );

        /// <summary>
        /// Makes the entity persistent.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void MakePersistent ( TEntity entity );

        /// <summary>
        /// Makes the entity transient.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void MakeTransient ( TEntity entity );

        /// <summary>
        /// Saves the changes.
        /// TODO: put it here temporary, for long term we should consider to define a unit of work to handle this
        /// </summary>
        void SaveChanges ();

        #endregion
    }
}
