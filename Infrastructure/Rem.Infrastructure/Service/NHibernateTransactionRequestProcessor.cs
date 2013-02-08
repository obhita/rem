using System.Collections.Generic;
using System.Linq;
using Agatha.Common.Caching;
using Agatha.ServiceLayer;
using Pillar.Domain.Event;
using Rem.Infrastructure.Domain;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// This class defines an Agatha request processor to deal with NHibernate transaction using LTM.
    /// </summary>
    public class NHibernateTransactionRequestProcessor: RequestProcessorBase
    {
        private readonly ISessionProvider _sessionProvider;
        private bool _validationFailureOccurred;

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateTransactionRequestProcessor"/> class.
        /// </summary>
        /// <param name="serviceLayerConfiguration">The service layer configuration.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="sessionProvider">The session provider.</param>
        public NHibernateTransactionRequestProcessor(
            ServiceLayerConfiguration serviceLayerConfiguration,
            ICacheManager cacheManager,
            ISessionProvider sessionProvider )
            : base ( serviceLayerConfiguration, cacheManager)
        {
            _sessionProvider = sessionProvider;
        }

        /// <summary>
        /// Befores the processing.
        /// </summary>
        /// <param name="requests">The requests.</param>
        protected override void BeforeProcessing ( IEnumerable<Agatha.Common.Request> requests )
        {
            DomainEvent.Register<RuleViolationEvent> ( failure => { _validationFailureOccurred = true; } );

            var session = _sessionProvider.GetSession ();
            session.BeginTransaction ();

            base.BeforeProcessing ( requests );
        }

        /// <summary>
        /// Afters the processing.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <param name="responses">The responses.</param>
        protected override void AfterProcessing ( IEnumerable<Agatha.Common.Request> requests, IEnumerable<Agatha.Common.Response> responses )
        {
            var session = _sessionProvider.GetSession ();
            responses = responses.ToList ();

            base.AfterProcessing ( requests, responses );

            if ( !_validationFailureOccurred && session.Transaction.IsActive )
            {
                if ( !IsExceptionOccurred ( responses ) )
                {
                    session.Flush ();
                    session.Transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Disposes the unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanagedResources()
        {
            base.DisposeUnmanagedResources();

            if (_sessionProvider != null)
            {
                var session = _sessionProvider.GetSession();

                if (session != null)
                {
                    var transaction = session.Transaction;
                    if ( transaction.IsActive )
                    {
                        transaction.Dispose ();
                    }

                    session.Dispose ();
                }
            }
        }

        private static bool IsExceptionOccurred ( IEnumerable<Agatha.Common.Response> responses )
        {
            return responses.Any ( p => p.Exception != null );
        }
    }
}