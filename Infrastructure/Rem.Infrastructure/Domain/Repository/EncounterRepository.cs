using System.Linq;
using Rem.Domain.Billing.EncounterModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// This class provides repository services for the <see cref="T:Rem.Domain.Billing.EncounterModule.Encounter">Encounter</see>.
    /// </summary>
    public class EncounterRepository : NHibernateRepositoryBase<Encounter>, IEncounterRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EncounterRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public EncounterRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        /// <summary>
        /// Gets the service by tracking number.
        /// </summary>
        /// <param name="trackingNumber">The tracking number.</param>
        /// <returns>
        /// An Encounter instance.
        /// </returns>
        public Encounter GetByTrackingNumber(long trackingNumber)
        {
            var encounter =
                Session.QueryOver<Encounter>()
                .Where(p => p.TrackingNumber == trackingNumber)
                .List()
                .FirstOrDefault();
            return encounter;
        }

        /// <summary>
        /// Gets the by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>An Encounter instance.</returns>
        public Encounter GetByKey(long key)
        {
            return Helper.GetEntityByKey(key);
        }

        /// <summary>
        /// Makes an Encounter persistent.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>An Encounter instance.</returns>
        public Encounter MakePersistent(Encounter entity)
        {
            return Helper.MakePersistent(entity);
        }

        /// <summary>
        /// Makes an Encounter transient.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void MakeTransient(Encounter entity)
        {
            Helper.MakeTransient ( entity );
        }
    }
}