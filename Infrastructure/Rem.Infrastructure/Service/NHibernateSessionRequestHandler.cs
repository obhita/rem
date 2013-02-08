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
using NHibernate;
using Rem.Infrastructure.Domain;
using StructureMap.Attributes;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// The NHibernateSessionRequestHandler is an abstract base class used by request handlers that require an NHibernate session in order to operate.
    /// </summary>
    /// <typeparam name="TRequest">
    /// The type of the request. 
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// The type of the response. 
    /// </typeparam>
    public abstract class NHibernateSessionRequestHandler<TRequest, TResponse> : RequestHandler<TRequest, TResponse>
        where TRequest : Request
        where TResponse : Response, new ()
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the session provider.
        /// </summary>
        /// <value> The session provider. </value>
        [SetterProperty]
        public ISessionProvider SessionProvider { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the session.
        /// </summary>
        protected ISession Session
        {
            get { return SessionProvider.GetSession (); }
        }

        #endregion
    }
}
