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

using System.Collections.Generic;
using System.Diagnostics;
using Agatha.Common;
using Agatha.Common.Caching;
using Agatha.ServiceLayer;
using NLog;
using Pillar.Common.Configuration;
using Rem.Infrastructure.Configuration;
using Rem.Infrastructure.Domain;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// The <see cref="PerformanceLoggingRequestProcessor"/> is used to record the performance information of the application while processing the web requests.
    /// </summary>
    public class PerformanceLoggingRequestProcessor : DtcTransactionRequestProcessor
    {
        #region Constants and Fields

        private readonly IConfigurationPropertiesProvider _configurationPropertiesProvider;
        private readonly Logger _performanceLogger = LogManager.GetLogger ( "WebServicePerformance" );
        private readonly long _requestHandlerPerformanceLimitInMilliseconds;
        private readonly long _webServiceCallPerformanceLimitInMilliseconds;

        private Stopwatch _batchStopwatch;
        private Stopwatch _requestStopwatch;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceLoggingRequestProcessor"/> class.
        /// </summary>
        /// <param name="serviceLayerConfiguration">The service layer configuration.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="configurationPropertiesProvider">The configuration properties provider.</param>
        /// <param name="sessionProvider">The session provider.</param>
        public PerformanceLoggingRequestProcessor (
            ServiceLayerConfiguration serviceLayerConfiguration, 
            ICacheManager cacheManager, 
            IConfigurationPropertiesProvider configurationPropertiesProvider, 
            ISessionProvider sessionProvider )
            : base ( serviceLayerConfiguration, cacheManager, sessionProvider )
        {
            _configurationPropertiesProvider = configurationPropertiesProvider;
            _webServiceCallPerformanceLimitInMilliseconds =
                _configurationPropertiesProvider.GetPropertyInt ( SettingKeyNames.WebServiceCallPerformanceLimitInMilliseconds );
            _requestHandlerPerformanceLimitInMilliseconds =
                _configurationPropertiesProvider.GetPropertyInt ( SettingKeyNames.AgathaRequestHandlerPerformanceLimitInMilliseconds );
        }

        #endregion

        #region Methods

        /// <summary>
        /// After handling the web request.
        /// </summary>
        /// <param name="request">The request.</param>
        protected override void AfterHandle ( Request request )
        {
            base.AfterHandle ( request );
            _requestStopwatch.Stop ();

            if ( _requestStopwatch.ElapsedMilliseconds > _requestHandlerPerformanceLimitInMilliseconds )
            {
                _performanceLogger.Warn (
                    "Performance warning: {0,4}ms for [{1}]", 
                    _requestStopwatch.ElapsedMilliseconds, 
                    request );
            }
        }

        /// <summary>
        /// After the processing the web requests.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <param name="responses">The responses.</param>
        protected override void AfterProcessing ( IEnumerable<Request> requests, IEnumerable<Response> responses )
        {
            if ( requests != null )
            {
                base.AfterProcessing ( requests, responses );
            }
            _batchStopwatch.Stop ();

            if ( _batchStopwatch.ElapsedMilliseconds > _webServiceCallPerformanceLimitInMilliseconds )
            {
                var requestString = string.Join ( ", ", requests );

                _performanceLogger.Warn (
                    "Performance warning: {0,4}ms for the following agatha batch: [{1}]", 
                    _batchStopwatch.ElapsedMilliseconds, 
                    requestString );
            }
        }

        /// <summary>
        /// Before the handling the web request.
        /// </summary>
        /// <param name="request">The request.</param>
        protected override void BeforeHandle ( Request request )
        {
            _requestStopwatch = Stopwatch.StartNew();
            base.BeforeHandle ( request );
        }

        /// <summary>
        /// Before the processing the web requests.
        /// </summary>
        /// <param name="requests">The requests.</param>
        protected override void BeforeProcessing ( IEnumerable<Request> requests )
        {
            _batchStopwatch = Stopwatch.StartNew();
            base.BeforeProcessing ( requests );
        }

        #endregion
    }
}
