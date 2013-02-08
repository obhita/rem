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
using Agatha.Common;
using Rem.Ria.Infrastructure.Web.Service;

namespace Rem.Ria.Infrastructure.Service
{
    /// <summary>
    /// RequestDispatcherExtensions class.
    /// </summary>
    public static class RequestDispatcherExtensions
    {
        #region Public Methods

        /// <summary>
        /// Adds the specified async request dispatcher.
        /// </summary>
        /// <param name="asyncRequestDispatcher">The async request dispatcher.</param>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.IAsyncRequestDispatcher"/></returns>
        public static IAsyncRequestDispatcher Add (
            this IAsyncRequestDispatcher asyncRequestDispatcher,
            Request request )
        {
            asyncRequestDispatcher.Add ( request );
            return asyncRequestDispatcher;
        }

        /// <summary>
        /// Adds the specified async request dispatcher.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="asyncRequestDispatcher">The async request dispatcher.</param>
        /// <param name="action">The action.</param>
        /// <returns>A <see cref="Agatha.Common.IAsyncRequestDispatcher"/></returns>
        public static IAsyncRequestDispatcher Add<TRequest> (
            this IAsyncRequestDispatcher asyncRequestDispatcher,
            Action<TRequest> action )
            where TRequest : Request, new ()
        {
            asyncRequestDispatcher.Add ( action );
            return asyncRequestDispatcher;
        }

        /// <summary>
        /// Adds the specified async request dispatcher.
        /// </summary>
        /// <param name="asyncRequestDispatcher">The async request dispatcher.</param>
        /// <param name="requestsToAdd">The requests to add.</param>
        /// <returns>A <see cref="Agatha.Common.IAsyncRequestDispatcher"/></returns>
        public static IAsyncRequestDispatcher Add (
            this IAsyncRequestDispatcher asyncRequestDispatcher,
            params Request[] requestsToAdd )
        {
            asyncRequestDispatcher.Add ( requestsToAdd );
            return asyncRequestDispatcher;
        }

        /// <summary>
        /// Adds the specified async request dispatcher.
        /// </summary>
        /// <param name="asyncRequestDispatcher">The async request dispatcher.</param>
        /// <param name="key">The key for the request.</param>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.IAsyncRequestDispatcher"/></returns>
        public static IAsyncRequestDispatcher Add (
            this IAsyncRequestDispatcher asyncRequestDispatcher,
            string key,
            Request request )
        {
            asyncRequestDispatcher.Add ( key, request );
            return asyncRequestDispatcher;
        }

        /// <summary>
        /// Adds the lookup values request.
        /// </summary>
        /// <param name="asyncRequestDispatcher">The async request dispatcher.</param>
        /// <param name="lookupName">Name of the lookup.</param>
        /// <returns>A <see cref="Agatha.Common.IAsyncRequestDispatcher"/></returns>
        public static IAsyncRequestDispatcher AddLookupValuesRequest (
            this IAsyncRequestDispatcher asyncRequestDispatcher,
            string lookupName )
        {
            asyncRequestDispatcher.Add ( lookupName, new GetLookupValuesRequest { Name = lookupName } );
            return asyncRequestDispatcher;
        }

        /// <summary>
        /// Adds the lookup values request.
        /// </summary>
        /// <param name="asyncRequestDispatcher">The async request dispatcher.</param>
        /// <param name="lookupName">Name of the lookup.</param>
        /// <param name="derivedDtoType">Type of the derived dto.</param>
        /// <returns>A <see cref="Agatha.Common.IAsyncRequestDispatcher"/></returns>
        public static IAsyncRequestDispatcher AddLookupValuesRequest(
            this IAsyncRequestDispatcher asyncRequestDispatcher,
            string lookupName,
            Type derivedDtoType)
        {
            asyncRequestDispatcher.Add ( lookupName, new GetLookupValuesRequest { Name = lookupName, DerivedDtoType = derivedDtoType.Name } );
            return asyncRequestDispatcher;
        }

        #endregion
    }
}
