using Rem.Domain.Clinical.TedsModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    ///  Initializes a new instance of the <see cref="TedsDischargeInterviewRepository"/> class.
    /// </summary>
    public class TedsDischargeInterviewRepository : NHibernateRepositoryBase<TedsDischargeInterview>, ITedsDischargeInterviewRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TedsDischargeInterviewRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public TedsDischargeInterviewRepository(ISessionProvider sessionProvider)
            : base ( sessionProvider )
        {
        }

        #region IDensAsiInterviewRepository Members

        /// <summary>
        /// Gets a TedsDischargeInterview by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A TedsDischargeInterview.</returns>
        public TedsDischargeInterview GetByKey(long key)
        {
            return Helper.GetEntityByKey(key);
        }

        /// <summary>
        /// Saves a TedsDischargeInterview.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <returns>A TedsDischargeInterview.</returns>
        public TedsDischargeInterview MakePersistent(TedsDischargeInterview entity)
        {
            return Helper.MakePersistent(entity);
        }

        /// <summary>
        /// Delete a TedsDischargeInterview.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void MakeTransient(TedsDischargeInterview entity)
        {
            Helper.MakeTransient(entity);
        }

        #endregion
    }
}
