using System.Linq;
using Rem.Domain.Billing.EncounterModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// This class provides repository services for the <see cref="T:Rem.Domain.Billing.EncounterModule.Service">Service</see>.
    /// </summary>
    public class ServiceRepository : NHibernateRepositoryBase<Rem.Domain.Billing.EncounterModule.Service>, IServiceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public ServiceRepository ( ISessionProvider sessionProvider )
            : base ( sessionProvider )
        {
        }

        /// <summary>
        /// Gets the service by tracking number.
        /// </summary>
        /// <param name="trackingNumber">The tracking number.</param>
        /// <returns>
        /// A service instance.
        /// </returns>
        public Rem.Domain.Billing.EncounterModule.Service GetByTrackingNumber(long trackingNumber)
        {
            var service =
                Session.QueryOver<Rem.Domain.Billing.EncounterModule.Service> ()
                .Where ( p => p.TrackingNumber == trackingNumber )
                .List ()
                .FirstOrDefault ();
            return service;
        }

        /// <summary>
        /// Gets the by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A Service instance.</returns>
        public Rem.Domain.Billing.EncounterModule.Service GetByKey(long key)
        {
            return Helper.GetEntityByKey(key);
        }

        /// <summary>
        /// Makes the persistent.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A Service instance.</returns>
        public Rem.Domain.Billing.EncounterModule.Service MakePersistent(Rem.Domain.Billing.EncounterModule.Service entity)
        {
            return Helper.MakePersistent(entity);
        }

        /// <summary>
        /// Makes the transient.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void MakeTransient(Rem.Domain.Billing.EncounterModule.Service entity)
        {
            Helper.MakeTransient(entity);
        }
    }
}
