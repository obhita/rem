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
using Agatha.Common;
using Agatha.Common.Caching;
using NLog;

namespace Rem.Ria.Infrastructure.Service
{
    /// <summary>
    /// Class for dispatching async request.
    /// </summary>
    public class AsyncRequestDispatcher : Agatha.Common.AsyncRequestDispatcher
    {
        #region Constants and Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();

        private DateTime _requestStartTime;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRequestDispatcher"/> class.
        /// </summary>
        /// <param name="requestProcessor">The request processor.</param>
        /// <param name="cacheManager">The cache manager.</param>
        public AsyncRequestDispatcher (
            IAsyncRequestProcessor requestProcessor,
            ICacheManager cacheManager )
            : base ( requestProcessor, cacheManager )
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when [process requests completed].
        /// </summary>
        /// <param name="processRequestsAsyncCompletedArgs">The process requests async completed args.</param>
        /// <param name="responseReciever">The response reciever.</param>
        /// <param name="tempResponseArray">The temp response array.</param>
        /// <param name="requestsToSendAsArray">The requests to send as array.</param>
        public override void OnProcessRequestsCompleted (
            ProcessRequestsAsyncCompletedArgs processRequestsAsyncCompletedArgs,
            ResponseReceiver responseReciever,
            Response[] tempResponseArray,
            Request[] requestsToSendAsArray )
        {
            base.OnProcessRequestsCompleted ( processRequestsAsyncCompletedArgs, responseReciever, tempResponseArray, requestsToSendAsArray );

            var requestEndTime = DateTime.Now;
            var timespan = requestEndTime - _requestStartTime;
            var requestString = GetRequestString ( requestsToSendAsArray );

            Logger.Debug (
                "Response received for the following agatha batch: [{0}].  Elapsed milliseconds ({1})", requestString, timespan.TotalMilliseconds );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Befores the sending requests.
        /// </summary>
        /// <param name="requestsToProcess">The requests to process.</param>
        protected override void BeforeSendingRequests ( IEnumerable<Request> requestsToProcess )
        {
            base.BeforeSendingRequests ( requestsToProcess );

            _requestStartTime = DateTime.Now;

            var requestString = GetRequestString ( requestsToProcess );

            Logger.Debug ( "Sending request for the following agatha batch: [{0}]", requestString );
        }

        private static string GetRequestString ( IEnumerable<Request> requestsToProcess )
        {
            return string.Join ( ", ", requestsToProcess );
        }

        #endregion
    }
}
