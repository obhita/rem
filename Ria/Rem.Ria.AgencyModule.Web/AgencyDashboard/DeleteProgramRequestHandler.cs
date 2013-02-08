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

using System.Collections.Generic;
using AutoMapper;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.ProgramModule;
using Rem.Infrastructure.Service;

namespace Rem.Ria.AgencyModule.Web.AgencyDashboard
{
    /// <summary>
    /// Class for handling delete program request.
    /// </summary>
    public class DeleteProgramRequestHandler :
        CommandRequestHandler<DeleteProgramRequest, DeleteProgramResponse, ProgramDto>
    {
        #region Constants and Fields

        private readonly IAgencyRepository _agencyRepository;
        private readonly IProgramFactory _programFactory;
        private readonly IProgramRepository _programRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProgramRequestHandler"/> class.
        /// </summary>
        /// <param name="programRepository">The program repository.</param>
        /// <param name="programFactory">The program factory.</param>
        /// <param name="agencyRepository">The agency repository.</param>
        public DeleteProgramRequestHandler (
            IProgramRepository programRepository,
            IProgramFactory programFactory,
            IAgencyRepository agencyRepository )
        {
            _programRepository = programRepository;
            _programFactory = programFactory;
            _agencyRepository = agencyRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the dto from request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Rem.Ria.AgencyModule.Web.AgencyDashboard.ProgramDto"/></returns>
        protected override ProgramDto CreateDtoFromRequest ( DeleteProgramRequest request )
        {
            return new ProgramDto { Key = request.ProgramKey };
        }

        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        protected override void HandleRequest ( DeleteProgramRequest request, DeleteProgramResponse response )
        {
            var programKey = request.ProgramKey;
            var program = _programRepository.GetByKey ( programKey );

            if ( program != null )
            {
                _programFactory.DestroyProgram ( program );

                // Refresh the session to obtain the up-to-date Program list.
                if ( Success )
                {
                    FlushSession ();

                    var agencyKey = request.AgencyKey;

                    var programs = _programRepository.GetAllProgramsByAgencyKey ( agencyKey );
                    var dtos = Mapper.Map<IList<Program>, IList<ProgramDto>> ( programs );
                    response.ProgramDtos = dtos;
                }
            }
        }

        #endregion
    }
}
