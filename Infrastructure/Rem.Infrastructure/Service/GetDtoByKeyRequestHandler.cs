﻿#region License

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
using Rem.Infrastructure.Service.DataTransferObject;
using StructureMap.Attributes;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// Gets the dto by its Key.
    /// </summary>
    /// <typeparam name="TDto">
    /// The type of the dto. 
    /// </typeparam>
    public class GetDtoByKeyRequestHandler<TDto> : NHibernateSessionRequestHandler<GetDtoRequest<TDto>, DtoResponse<TDto>>
        where TDto : KeyedDataTransferObject
    {
        #region Public Properties

        /// <summary>
        ///   Gets or sets the dto factory.
        /// </summary>
        /// <value> The dto factory. </value>
        [SetterProperty]
        public IKeyedDtoFactory<TDto> DtoFactory { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request ie., getting the dto based of the provided key.
        /// </summary>
        /// <param name="request">
        /// The request. 
        /// </param>
        /// <returns>
        /// Returns <see cref="Response"/> that contains the dto that was requested. 
        /// </returns>
        public override Response Handle ( GetDtoRequest<TDto> request )
        {
            var response = CreateTypedResponse ();
            response.DataTransferObject = DtoFactory.CreateKeyedDto ( request.Key );

            return response;
        }

        #endregion
    }
}
