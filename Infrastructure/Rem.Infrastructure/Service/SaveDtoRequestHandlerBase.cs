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
using Pillar.Common.Utility;
using Pillar.Domain.Event;
using Rem.Infrastructure.Service.DataTransferObject;
using StructureMap.Attributes;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// The <see cref="SaveDtoRequestHandlerBase&lt;TRequest, TResponse, TDto&gt;"/> is the base class to handle a generic request to save a generic dto.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <typeparam name="TDto">The type of the dto.</typeparam>
    public abstract class SaveDtoRequestHandlerBase<TRequest, TResponse, TDto> : NHibernateSessionRequestHandler<TRequest, TResponse>
        where TRequest : SaveDtoRequest<TDto>
        where TResponse : DtoResponse<TDto>, new ()
        where TDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private bool _validationFailureOccurred;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the keyed dto factory.
        /// </summary>
        /// <value>
        /// The keyed dto factory.
        /// </value>
        [SetterProperty]
        public IKeyedDtoFactory<TDto> KeyedDtoFactory { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Returns <see cref="Response"/>.</returns>
        public override Response Handle ( TRequest request )
        {
            var requestDto = request.DataTransferObject;

            DomainEvent.Register<RuleViolationEvent> ( failure => _validationFailureOccurred = true );

            LogicalTreeWalker.Walk<IDataTransferObject> ( requestDto, dto => dto.ClearAllDataErrorInfo () );

            var response = CreateTypedResponse ();
            response.DataTransferObject = requestDto;

            var processSucceeded = Process ( requestDto );

            processSucceeded &= !_validationFailureOccurred;

            if ( processSucceeded )
            {
                Session.Flush ();
                Session.Clear ();

                response.DataTransferObject = RefreshDto ( requestDto.Key );

                AfterDtoRefreshed ( response.DataTransferObject );
            }

            return response;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Contains functionality that gets executed after the dto is refreshed.
        /// </summary>
        /// <param name="dto">The dto that has been refreshed.</param>
        protected virtual void AfterDtoRefreshed ( TDto dto )
        {
        }

        /// <summary>
        /// Processes the specified dto.
        /// </summary>
        /// <param name="dto">The dto to be processed.</param>
        /// <returns>Returns true, if the processing is successful; else returns false.</returns>
        protected abstract bool Process ( TDto dto );

        private TDto RefreshDto ( long key )
        {
            return KeyedDtoFactory.CreateKeyedDto ( key );
        }

        #endregion
    }
}
