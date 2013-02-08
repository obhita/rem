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

using Agatha.Common;
using Agatha.ServiceLayer;
using Pillar.Domain.Event;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Service.DataTransferObject;
using StructureMap.Attributes;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// The <see cref="CommandRequestHandler&lt;TRequest, TResponse, TDto&gt;"/> is used perform additional steps required on all the incoming command requests or outgoing responses.
    /// </summary>
    /// <typeparam name="TRequest">
    /// The type of the request. 
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// The type of the response. 
    /// </typeparam>
    /// <typeparam name="TDto">
    /// The type of the dto. 
    /// </typeparam>
    public abstract class CommandRequestHandler<TRequest, TResponse, TDto> : RequestHandler<TRequest, TResponse>
        where TRequest : Request
        where TResponse : DtoResponse<TDto>, new ()
        where TDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private bool _validationFailureOccurred;

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the session provider.
        /// </summary>
        /// <value> The session provider. </value>
        [SetterProperty]
        public ISessionProvider SessionProvider { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets a value indicating whether this <see cref="CommandRequestHandler&lt;TRequest, TResponse, TDto&gt;" /> is success.
        /// </summary>
        /// <value> <c>true</c> if success; otherwise, <c>false</c> . </value>
        protected bool Success
        {
            get { return !_validationFailureOccurred; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">
        /// The request. 
        /// </param>
        /// <returns>
        /// Returns the response. 
        /// </returns>
        public override Response Handle ( TRequest request )
        {
            DomainEvent.Register<RuleViolationEvent> ( failure => { _validationFailureOccurred = true; } );

            // make sure dto is always there no matter the subsequences success or failed
            var response = CreateTypedResponse ();
            response.DataTransferObject = CreateDtoFromRequest ( request );

            HandleRequest ( request, response );

            return response;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the dto from request.
        /// </summary>
        /// <param name="request">
        /// The request. 
        /// </param>
        /// <returns>
        /// Returns the created dto. 
        /// </returns>
        protected abstract TDto CreateDtoFromRequest ( TRequest request );

        /// <summary>
        /// Flushes the session.
        /// </summary>
        protected void FlushSession ()
        {
            var session = SessionProvider.GetSession ();
            session.Flush ();
        }

        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">
        /// The request. 
        /// </param>
        /// <param name="response">
        /// The response. 
        /// </param>
        protected abstract void HandleRequest ( TRequest request, TResponse response );

        #endregion
    }
}
