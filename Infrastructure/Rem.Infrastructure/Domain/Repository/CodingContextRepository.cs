using System.Linq;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// This class defines basic repository services for the 
    /// <see cref="T:Rem.Domain.Clinical.VisitModule.CodingContext">CodingContext</see>.
    /// </summary>
    public class CodingContextRepository : NHibernateRepositoryBase<CodingContext>, ICodingContextRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodingContextRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public CodingContextRepository ( ISessionProvider sessionProvider )
            : base ( sessionProvider )
        {
        }

        /// <summary>
        /// Gets the coding context by visit key.
        /// </summary>
        /// <param name="visitKey">The visit key.</param>
        /// <returns>
        /// A CodingContext instance.
        /// </returns>
        public CodingContext GetByVisitKey(long visitKey)
        {
            var codingContext =
                Session.QueryOver<CodingContext>()
                .Where(p => p.Visit.Key == visitKey)
                .List()
                .FirstOrDefault();
            return codingContext;
        }

        /// <summary>
        /// Gets the by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A <see cref="CodingContext">CodingContext</see> instance.</returns>
        public CodingContext GetByKey(long key)
        {
            return Helper.GetEntityByKey(key);
        }

        /// <summary>
        /// Makes a CodingContext persistent.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A <see cref="CodingContext">CodingContext</see></returns>
        public CodingContext MakePersistent(CodingContext entity)
        {
            return Helper.MakePersistent(entity);
        }

        /// <summary>
        /// Makes a CodingContext transient.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void MakeTransient(CodingContext entity)
        {
            Helper.MakeTransient(entity);
        }
    }
}
