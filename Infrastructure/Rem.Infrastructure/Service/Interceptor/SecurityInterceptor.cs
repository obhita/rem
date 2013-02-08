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
using Pillar.Security.AccessControl;

namespace Rem.Infrastructure.Service.Interceptor
{
    /// <summary>
    /// This class deals with security concern per Agatha request handler.
    /// </summary>
    public class SecurityInterceptor : IRequestHandlerInterceptor
    {
        private readonly IAccessControlManager _accessControlManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityInterceptor"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        public SecurityInterceptor(IAccessControlManager accessControlManager)
        {
            _accessControlManager = accessControlManager;
        }

        /// <summary>
        /// Afters the handling request.
        /// </summary>
        /// <param name="context">The context.</param>
        public void AfterHandlingRequest(RequestProcessingContext context)
        {
        }

        /// <summary>
        /// Befores the handling request.
        /// </summary>
        /// <param name="context">The context.</param>
        public void BeforeHandlingRequest(RequestProcessingContext context)
        {
            if (context != null && context.Request != null)
            {
                var request = context.Request;
                var resource = new ResourceRequest { request.GetType ().FullName };
                if ( !_accessControlManager.CanAccess ( resource ) )
                {
                    throw new InvalidOperationException ( "You do not have permission to access: " + resource );
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
