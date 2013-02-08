using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Rem.Infrastructure.Domain;
using uNhAddIns.SessionEasier;

namespace GenerateLoadScripts
{
    /// <summary>
    /// The <see cref="SessionProvider"> SessionProvider </see> contains utilities to get a NHiberate session.
    /// </summary>
    public class SessionProvider : ISessionProvider
    {
        private readonly ISessionFactoryProvider _sessionFactoryProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionProvider"/> class.
        /// </summary>
        /// <param name="sessionFactoryProvider">The session factory provider.</param>
        public SessionProvider(ISessionFactoryProvider sessionFactoryProvider)
        {
            _sessionFactoryProvider = sessionFactoryProvider;
        }

        #region ISessionProvider Members

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <returns>An ISession object.</returns>
        public ISession GetSession()
        {
            ISessionFactory sessionFactory = _sessionFactoryProvider.GetFactory(null); // Bind seesion to the context etc.
            ISession session = sessionFactory.GetCurrentSession();
            return session;
        }

        #endregion
    }
}
