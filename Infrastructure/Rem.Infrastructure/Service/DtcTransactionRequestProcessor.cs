#region License
// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Agatha.Common.Caching;
using Agatha.ServiceLayer;
using Pillar.Domain.Event;
using Rem.Infrastructure.Domain;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// This class defines an Agatha request processor to deal with transaction using DTC.
    /// </summary>
    public class DtcTransactionRequestProcessor : RequestProcessorBase
    {
        private readonly ISessionProvider _sessionProvider;
        private TransactionScope _transactionScope;
        private bool _validationFailureOccurred;

        /// <summary>
        /// Initializes a new instance of the <see cref="DtcTransactionRequestProcessor"/> class.
        /// </summary>
        /// <param name="serviceLayerConfiguration">The service layer configuration.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="sessionProvider">The session provider.</param>
        public DtcTransactionRequestProcessor (
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
            requests = requests.ToList ();

            base.BeforeProcessing(requests);

            bool allRequestsAreQuery = requests.All ( request => ( request is IQueryRequest ) );

            if (!allRequestsAreQuery)
            {
                _transactionScope = CreateTransactionScope ();

                // This line's sole purpose is to force to use DTC at the very beginning.. 
                // Since when use NHibernate transaction (SQL Server RM) first, then use NServiceBus (MSMQ RM), the Transaction Manager (TM) could not be automatically swiched from LTM to DTC.
                Transaction.Current.EnlistDurable ( DummyEnlistmentNotification.Id, new DummyEnlistmentNotification (), EnlistmentOptions.None );

                DomainEvent.Register<RuleViolationEvent> ( failure => { _validationFailureOccurred = true; } );
            }

            var session = _sessionProvider.GetSession();
            session.BeginTransaction();
        }

        /// <summary>
        /// Afters the processing.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <param name="responses">The responses.</param>
        protected override void AfterProcessing ( IEnumerable<Agatha.Common.Request> requests, IEnumerable<Agatha.Common.Response> responses )
        {
            responses = responses.ToList();
            base.AfterProcessing(requests, responses);

            var session  = _sessionProvider.GetSession ();
                
            if ( !_validationFailureOccurred )
            {
                if ( !IsExceptionOccurred ( responses ) )
                {
                    session.Flush ();
                    if (_transactionScope != null)
                    {
                        _transactionScope.Complete ();
                    }
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
                    // Don't do this in DTC transaction otherwise throws exception, I believe it is a bug in NHibernate.
                    //session.Transaction.Dispose();

                    session.Dispose();
                }
            }

            if (_transactionScope != null)
            {
                _transactionScope.Dispose();
            }
        }

        private static TransactionScope CreateTransactionScope ()
        {
            var transactionOptions = new TransactionOptions ();
            transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;

            transactionOptions.Timeout = TimeSpan.MaxValue;
            return new TransactionScope ( TransactionScopeOption.Required, transactionOptions );
        }

        private static bool IsExceptionOccurred ( IEnumerable<Agatha.Common.Response> responses )
        {
            return responses.Any ( p => p.Exception != null );
        }
    }
}
