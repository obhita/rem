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

using NHibernate;
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
using StructureMap.Attributes;

namespace Rem.Ria.AgencyModule.Web.RoleManagement
{
    /// <summary>
    /// Class for handling system role command request.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    public abstract class SystemRoleCommandRequestHandler<TRequest> : CommandRequestHandler<TRequest, SystemRoleCommandResponse, ValidationFailureDto>
        where TRequest : SystemRoleCommandRequestBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the system role dto factory.
        /// </summary>
        /// <value>The system role dto factory.</value>
        [SetterProperty]
        public IKeyedDtoFactory<SystemRoleDto> SystemRoleDtoFactory { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the dto from request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Rem.Infrastructure.Service.DataTransferObject.ValidationFailureDto"/></returns>
        protected override ValidationFailureDto CreateDtoFromRequest ( TRequest request )
        {
            return new ValidationFailureDto ();
        }

        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        protected override void HandleRequest ( TRequest request, SystemRoleCommandResponse response )
        {
            var session = SessionProvider.GetSession ();
            var systemRoleKey = request.SystemRoleKey;
            var systemRole = session.Get<SystemRole> ( systemRoleKey );

            ProcessSystemRoleCommand ( systemRole, request );

            if ( Success )
            {
                FlushSession ();

                var systemRoleDto = SystemRoleDtoFactory.CreateKeyedDto ( systemRoleKey );
                response.SystemRole = systemRoleDto;
            }
        }

        /// <summary>
        /// Processes the system role command.
        /// </summary>
        /// <param name="systemRole">The system role.</param>
        /// <param name="request">The request.</param>
        protected abstract void ProcessSystemRoleCommand ( SystemRole systemRole, TRequest request );

        #endregion
    }
}
