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

using Pillar.Domain;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// The <see cref="SaveAggregateNodeDtoRequestHandlerBase&lt;TRequest, TResponse, TDto, TAggregateRoot, TAggregateNode&gt;"/> is base class that can be used to persist an generic aggegate node.
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
    /// <typeparam name="TAggregateRoot">
    /// The type of the aggregate root. 
    /// </typeparam>
    /// <typeparam name="TAggregateNode">
    /// The type of the aggregate node. 
    /// </typeparam>
    public abstract class SaveAggregateNodeDtoRequestHandlerBase<TRequest, TResponse, TDto, TAggregateRoot, TAggregateNode> :
        SaveDtoRequestHandlerBase<TRequest, TResponse, TDto>
        where TRequest : SaveDtoRequest<TDto>
        where TResponse : DtoResponse<TDto>, new ()
        where TDto : KeyedDataTransferObject
        where TAggregateRoot : class, IAggregateRoot
        where TAggregateNode : class, IAggregateNode
    {
        #region Constants and Fields

        /// <summary>
        /// The aggregate root.
        /// </summary>
        private TAggregateRoot _aggregateRoot;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        protected TAggregateRoot AggregateRoot
        {
            get { return _aggregateRoot; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the new aggegate node.
        /// </summary>
        /// <param name="dto">The dto for which aggegate root is created.</param>
        /// <returns>The aggregate root.</returns>
        protected virtual TAggregateRoot CreateNew ( TDto dto )
        {
            return null;
        }

        /// <summary>
        /// Processes the given dto.
        /// </summary>
        /// <param name="dto">The dto to be processed.</param>
        /// <returns>Returns true, if the processing is successful; else returns false.</returns>
        protected override bool Process ( TDto dto )
        {
            var key = dto.Key;
            var aggregateNode = Session.Get<TAggregateNode> ( key );
            _aggregateRoot = aggregateNode.AggregateRoot as TAggregateRoot;

            var processSucceded = ProcessSingleAggregate ( dto, aggregateNode );

            return processSucceded;
        }

        /// <summary>
        /// Processes a single aggregate.
        /// </summary>
        /// <param name="dto">The dto to be processed.</param>
        /// <param name="aggregateNode">The aggregate root.</param>
        /// <returns>Returns true, if the processing is successful; else returns false.</returns>
        protected abstract bool ProcessSingleAggregate ( TDto dto, TAggregateNode aggregateNode );

        #endregion
    } 
}
