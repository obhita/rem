#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using NLog;
using Pillar.Common.Configuration;
using Pillar.Common.InversionOfControl;
using Rem.Infrastructure.Configuration;
using uNhAddIns.SessionEasier;
using uNhAddIns.SessionEasier.Contexts;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// Manage the NHibernate's session using session-per-call pattern.
    /// </summary>
    /// <remarks>
    /// The session-per-call has idetical behavior of the most famous session-per-request where, in this case, the NH's session and NH's transaction has the same life-cycle of a call.
    /// </remarks>
    public class NHibernateSessionCallContextInitializer : ICallContextInitializer
    {
        #region Constants and Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();
        private static readonly Logger PerformanceLogger = LogManager.GetLogger ( "WebServicePerformance" );

        private readonly ISessionFactoryProvider _sessionFactoryProvider;
        private readonly Stopwatch _stopwatch;
        private readonly long _webServiceCallPerformanceLimitInMilliseconds;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="NHibernateSessionCallContextInitializer" /> class.
        /// </summary>
        public NHibernateSessionCallContextInitializer ()
            : this ( IoC.CurrentContainer.Resolve<ISessionFactoryProvider> () )
        {
            var configurationPropertiesProvider = IoC.CurrentContainer.Resolve<IConfigurationPropertiesProvider> ();
            _webServiceCallPerformanceLimitInMilliseconds =
                configurationPropertiesProvider.GetPropertyInt ( SettingKeyNames.WebServiceCallPerformanceLimitInMilliseconds );

            _stopwatch = new Stopwatch ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateSessionCallContextInitializer"/> class.
        /// </summary>
        /// <param name="sessionFactoryProvider">The session factory provider.</param>
        public NHibernateSessionCallContextInitializer ( ISessionFactoryProvider sessionFactoryProvider )
        {
            _sessionFactoryProvider = sessionFactoryProvider;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Implement to participate in cleaning up the thread that invoked the operation.
        /// </summary>
        /// <param name="correlationState">The correlation object returned from the <see cref="M:System.ServiceModel.Dispatcher.ICallContextInitializer.BeforeInvoke(System.ServiceModel.InstanceContext,System.ServiceModel.IClientChannel,System.ServiceModel.Channels.Message)"/> method.</param>
        public void AfterInvoke ( object correlationState )
        {
            foreach ( var sessionFactory in _sessionFactoryProvider )
            {
                var session = CurrentSessionContext.Unbind ( sessionFactory );
                if ( !session.IsOpen )
                {
                    continue;
                }

                try
                {
                    session.Flush ();
                    if ( session.Transaction.IsActive )
                    {
                        Logger.Debug ( "Commiting Transaction" );

                        session.Transaction.Commit ();
                    }
                }
                catch ( Exception e )
                {
                    Logger.Fatal ( e.Message, e );

                    if ( session.Transaction.IsActive )
                    {
                        session.Transaction.Rollback ();
                    }
                }
                finally
                {
                    session.Close ();
                    session.Dispose ();
                }
            }

            _stopwatch.Stop ();

            if ( _stopwatch.ElapsedMilliseconds > _webServiceCallPerformanceLimitInMilliseconds )
            {
                try
                {
                    var operationContext = OperationContext.Current;

                    // The line below could throw System.ObjectDisposedException if requesting service meta data
                    var incomingMessageHeaders = operationContext.IncomingMessageHeaders;

                    var instance = operationContext.InstanceContext.GetServiceInstance();
                    var methodName = incomingMessageHeaders.Action;
                    if (instance != null && methodName != null)
                    {
                        var type = instance.GetType();
                        var serviceName = type.Namespace + "." + type.Name;

                        var index = methodName.LastIndexOf("/", StringComparison.Ordinal);
                        methodName = methodName.Substring(index + 1);
                        PerformanceLogger.Warn(
                            "Performance warning: {0,4}ms for {1}.{2}",
                            _stopwatch.ElapsedMilliseconds,
                            serviceName,
                            methodName);
                    }
                }
                catch (ObjectDisposedException)
                {
                    // Swallow it
                }
            }
        }

        /// <summary>
        /// Implement to participate in the initialization of the operation thread.
        /// </summary>
        /// <param name="instanceContext">The service instance for the operation.</param>
        /// <param name="channel">The client channel.</param>
        /// <param name="message">The incoming message.</param>
        /// <returns>
        /// A correlation object passed back as the parameter of the <see cref="M:System.ServiceModel.Dispatcher.ICallContextInitializer.AfterInvoke(System.Object)"/> method.
        /// </returns>
        public object BeforeInvoke ( InstanceContext instanceContext, IClientChannel channel, Message message )
        {
            _stopwatch.Start ();
            foreach ( var sessionFactory in _sessionFactoryProvider )
            {
                var session = sessionFactory.OpenSession ();
                CurrentSessionContext.Bind ( session );
                session.BeginTransaction ();
            }

            return null;
        }

        #endregion
    }
}
